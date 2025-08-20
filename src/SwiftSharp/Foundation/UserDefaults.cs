using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using System.Runtime.InteropServices;

namespace SwiftSharp.Foundation
{
    public class UserDefaults
    {
        private readonly string filePath;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private static readonly Lazy<UserDefaults> _standard = new Lazy<UserDefaults>(() => new UserDefaults());
        private Dictionary<string, object>? storage;

        #region Constructors

        public UserDefaults()
        {
            var bundle = Bundle.Main;
            string[] parts = bundle.BundleIdentifier.Split('.');
            string company = parts.Length > 0 ? parts[0] : "UnknownCompany";
            string product = parts.Length > 1 ? parts[1] : "UnknownProduct";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string dir = Path.Combine(appData, bundle.BundleIdentifier.Split('.')[0], bundle.BundleIdentifier.Split('.')[1]);
                Directory.CreateDirectory(dir);
                filePath = Path.Combine(dir, "UserDefaults.xml");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                string home = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string dir = Path.Combine(home, "Library", "Preferences");
                Directory.CreateDirectory(dir);
                filePath = Path.Combine(dir, $"{company}.{product}.plist");
            }
            else
            {
                string home = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string config = Environment.GetEnvironmentVariable("XDG_CONFIG_HOME")
                                ?? Path.Combine(home, ".config");
                string dir = Path.Combine(config, "swiftsharp", company, product);
                Directory.CreateDirectory(dir);
                filePath = Path.Combine(dir, "UserDefaults.xml");
            }

            Load();
        }

        #endregion

        #region Properties

        public static UserDefaults Standard => _standard.Value;

        #endregion

        #region Methods

        public void Set<T>(string key, T value)
        {
            _lock.EnterWriteLock();
            try
            {
                storage[key] = value!;
                Save();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public T Get<T>(string key, T defaultValue = default!)
        {
            _lock.EnterReadLock();
            try
            {
                if (storage!.TryGetValue(key, out object value))
                {
                    if (value is T tValue)
                        return tValue;
                    try
                    {
                        return (T)Convert.ChangeType(value, typeof(T));
                    }
                    catch { return defaultValue; }
                }
                return defaultValue;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void Remove(string key)
        {
            _lock.EnterWriteLock();
            try
            {
                if (storage!.Remove(key))
                    Save();
            }
            finally { _lock.ExitWriteLock(); }
        }

        #endregion

        #region Utility Methods

        private object? ParseValues(XElement dictElement)
        {
            var valEl = dictElement.Elements().FirstOrDefault(el => el.Name != "key");
            if (valEl == null)
                return null;

            return valEl.Name.LocalName switch
            {
                "string" => valEl.Value,
                "integer" => int.Parse(valEl.Value),
                "real" => double.Parse(valEl.Value),
                "true" => true,
                "false" => false,
                "date" => DateTime.Parse(valEl.Value),
                _ => valEl.Value
            };
        }

        private void Load()
        {
            _lock.EnterWriteLock();
            try
            {
                if (!File.Exists(filePath))
                {
                    storage = new Dictionary<string, object>();
                    Save();
                    return;
                }

                var xml = XDocument.Load(filePath);
                var rootDict = xml.Root;

                if (rootDict == null || rootDict.Name != "dict")
                {
                    storage = new Dictionary<string, object>();
                    return;
                }

                storage = new Dictionary<string, object>();
                XElement? currentKey = null;

                foreach (var el in rootDict.Elements())
                {
                    if (el.Name == "key")
                    {
                        currentKey = el;
                    }
                    else if (currentKey != null)
                    {
                        storage[currentKey.Value] = el.Name.LocalName switch
                        {
                            "string" => el.Value,
                            "integer" => int.Parse(el.Value),
                            "real" => double.Parse(el.Value),
                            "true" => true,
                            "false" => false,
                            "date" => DateTime.Parse(el.Value),
                            _ => el.Value
                        };
                        currentKey = null;
                    }
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }


        private void Save()
        {
            _lock.EnterWriteLock();
            try
            {
                var xml = new XDocument(
                    new XElement("dict",
                        storage.SelectMany(kv =>
                        {
                            XElement valueElement = kv.Value switch
                            {
                                string s => new XElement("string", s),
                                int i => new XElement("integer", i),
                                double d => new XElement("real", d),
                                bool b => new XElement(b ? "true" : "false"),
                                DateTime dt => new XElement("date", dt.ToString("o")),
                                _ => new XElement("string", kv.Value?.ToString() ?? "")
                            };
                            return new XElement[] { new XElement("key", kv.Key), valueElement };
                        })
                    )
                );

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                xml.Save(filePath);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SwiftSharp.Foundation
{
    public class UserDefaults
    {
        private readonly string filePath;
        private Dictionary<string, object>? storage;

        #region Constructors

        /// <summary>
        /// Creates a user defaults object initialized with the defaults for the app and current user.
        /// </summary>
        public UserDefaults()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var fileName = "userdefaults.xml";
            filePath = Path.Combine(folder, fileName);

            if (File.Exists(filePath))
            {
                Load();
            }
            else
            {
                storage = new Dictionary<string, object>();
                Save();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the shared defaults object.
        /// </summary>
        public static UserDefaults Standard { get; } = new UserDefaults();

        #endregion

        #region Methods

        /// <summary>
        /// Returns the string associated with the specified key.
        /// </summary>
        public string? GetString(string key) => storage!.TryGetValue(key, out var value) ? value?.ToString() : null;

        /// <summary>
        /// Sets the value of the specified default key.
        /// </summary>
        /// <param name="value">The object to store in the defaults database.</param>
        /// <param name="key">The key with which to associate the value.</param>
        public void Set(object? value, string key)
        {
            storage![key] = value;
            Save();
        }

        /// <summary>
        /// Sets the value of the specified default key to the specified float value.
        /// </summary>
        /// <param name="value">The float value to store in the defaults database.</param>
        /// <param name="key">The key with which to associate the value.</param>
        public void Set(float value, string key) => Set(value, key);

        /// <summary>
        /// Sets the value of the specified default key to the double value.
        /// </summary>
        /// <param name="value">The double value.</param>
        /// <param name="key">The key with which to associate the value.</param>
        public void Set(double value, string key) => Set(value, key);

        /// <summary>
        /// Sets the value of the specified default key to the specified integer value.
        /// </summary>
        /// <param name="value">The integer value to store in the defaults database.</param>
        /// <param name="key">The key with which to associate the value.</param>
        public void Set(int value, string key) => Set(value, key);

        /// <summary>
        /// Sets the value of the specified default key to the specified Boolean value.
        /// </summary>
        /// <param name="value">The Boolean value to store in the defaults database.</param>
        /// <param name="key">The key with which to associate the value.</param>
        public void Set(bool value, string key) => Set(value, key);
        
        #endregion

        #region Utility Methods

        private void Save()
        {
            var doc = new XDocument(new XElement("dict"));
            foreach (var kvp in storage!)
            {
                doc.Root!.Add(new XElement("key", kvp.Key));

                var valueElement = kvp.Value switch
                {
                    int i => new XElement("integer", i),
                    double d => new XElement("real", d),
                    bool b => new XElement(b ? "true" : "false"),
                    DateTime dt => new XElement("date", dt.ToString("o")),
                    string s => new XElement("string", s),
                    null => new XElement("string", ""), // null as empty string
                    _ => new XElement("string", kvp.Value.ToString() ?? "")
                };

                doc.Root.Add(valueElement);
            }

            doc.Save(filePath);
        }

        private void Load()
        {
            storage = new Dictionary<string, object>();
            var doc = XDocument.Load(filePath);
            var elements = doc.Root!.Elements();

            for (int i = 0; i < elements.Count(); i += 2)
            {
                var keyElement = elements.ElementAt(i);
                var valueElement = elements.ElementAt(i + 1);

                var key = keyElement.Value;
                object? value = valueElement.Name.LocalName switch
                {
                    "integer" => int.TryParse(valueElement.Value, out var intResult) ? intResult : 0,
                    "real" => double.TryParse(valueElement.Value, out var d) ? d : 0.0,
                    "true" => true,
                    "false" => false,
                    "date" => DateTime.TryParse(valueElement.Value, null,
                        System.Globalization.DateTimeStyles.RoundtripKind, out var dt) ? dt : DateTime.MinValue,
                    "string" => valueElement.Value,
                    _ => valueElement.Value
                };

                storage[key] = value;
            }
        }

        #endregion
    }
}
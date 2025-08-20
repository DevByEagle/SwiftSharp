using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SwiftSharp.Foundation
{
    public class Bundle
    {
        private readonly Assembly assembly;
        private readonly Dictionary<string, object> infoDictionary;

        #region Constructors

        public Bundle(Type aClass)
        {
            assembly = aClass.Assembly;
            infoDictionary = LoadInfoDictionary();
        }

        public Bundle(string identifier)
        {
            var asm = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a =>
                {
                    var company = a.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? "";
                    var product = a.GetCustomAttribute<AssemblyProductAttribute>()?.Product ?? "";
                    return $"{company}.{product}" == identifier;
                });

            if (asm == null)
                throw new ArgumentException($"No bundle found with identifier '{identifier}'.");

            assembly = asm;
            infoDictionary = LoadInfoDictionary();
        }

        private Bundle(Assembly asm)
        {
            assembly = asm;
            infoDictionary = LoadInfoDictionary();
        }

        #endregion

        #region Properties

        public static Bundle Main => new(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly());

        public string ResourcePath
        {
            get
            {
                string basePath = Path.GetDirectoryName(assembly.Location)!;
                string resourcePath = Path.Combine(basePath, "Resources");
                if (!Directory.Exists(resourcePath))
                    Directory.CreateDirectory(resourcePath);
                return resourcePath;
            }
        }

        public string BundleIdentifier
        {
            get
            {
                string company = assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? "UnknownCompany";
                string product = assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product ?? "UnknownProduct";
                return $"{company}.{product}";
            }
        }

        public IReadOnlyDictionary<string, object> InfoDictionary => infoDictionary;

        #endregion

        #region Methods

        public static Bundle? FromPath(string path)
        {
            if (!File.Exists(path) && !Directory.Exists(path))
                return null;

            try
            {
                var asm = Assembly.LoadFrom(path);
                return new Bundle(asm);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Utility Methods

        private Dictionary<string, object> LoadInfoDictionary()
        {
            return new Dictionary<string, object>
        {
            { "CFBundleIdentifier", BundleIdentifier }
        };
        }

        #endregion
    }
}
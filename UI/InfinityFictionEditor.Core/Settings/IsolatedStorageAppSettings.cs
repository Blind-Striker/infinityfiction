using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;

using InfinityFiction.UI.InfinityFictionEditor.Core.Foundation;

using Newtonsoft.Json;

namespace InfinityFiction.UI.InfinityFictionEditor.Core.Settings
{
    public class IsolatedStorageAppSettings : IAppSettings
    {
        private const string SettingsFileName = "setting.json";

        public IsolatedStorageAppSettings()
        {
            Settings = Load();
        }

        private Dictionary<string, object> Settings { get; set; }

        public object this[string key]
        {
            get
            {
                object value;
                Settings.TryGetValue(key, out value);
                return value;
            }
            set
            {
                if (Settings.ContainsKey(key))
                {
                    Settings[key] = value;
                }
                else
                {
                    Settings.Add(key, value);
                }
            }
        }

        public void Save()
        {
            try
            {
                IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly();
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(SettingsFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, storage))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        string serializedSettings = JsonConvert.SerializeObject(Settings);
                        writer.Write(serializedSettings);
                    }
                }

            }
            catch
            {
            } 
        }

        private Dictionary<string, object> Load()
        {
            try
            {
                IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly();

                if (!storage.FileExists(SettingsFileName))
                {
                    return new Dictionary<string, object>();
                }

                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(SettingsFileName, FileMode.Open, FileAccess.Read, storage))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string json = reader.ReadToEnd();

                        return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    }
                }

            }
            catch
            {
                return new Dictionary<string, object>();
            } 
        }
    }
}

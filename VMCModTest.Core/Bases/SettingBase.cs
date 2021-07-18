using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VMCModTest.Core.SimpleJSON;

namespace VMCModTest.Core.Bases
{
    public abstract class SettingBase<T> : BindableBase where T : new()
    {
        public static readonly string CurrentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static readonly string ConfigFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.json";
        public static readonly string SettingFile = Path.Combine(CurrentDir, ConfigFileName);
        protected FileSystemWatcher _configJsonWatcher;
        public event Action OnConfigChanged;

        public static T Instance { get; } = new T();
        public SettingBase()
        {
            this._configJsonWatcher = new FileSystemWatcher(CurrentDir, ConfigFileName);
            this._configJsonWatcher.Changed += this.OnConfigJsonWatcherChanged;
            this._configJsonWatcher.EnableRaisingEvents = true;
            this.Load();
        }

        private void OnConfigJsonWatcherChanged(object sender, FileSystemEventArgs e)
        {
            this.Load();
        }

        public virtual void Save()
        {
            var json = new JSONObject();
            foreach (var prop in this.GetType().GetProperties((BindingFlags.Public | BindingFlags.Instance))) {
                try {

                    var valueObj = prop.GetValue(this);
                    if (double.TryParse($"{valueObj}", out var num)) {
                        json.Add(prop.Name, num);
                    }
                    else {
                        json.Add(prop.Name, $"{valueObj}");
                    }
                }
                catch (Exception e) {
                    Debug.WriteLine(e);
                }
            }
            this._configJsonWatcher.EnableRaisingEvents = false;
            try {
                File.WriteAllText(SettingFile, json.ToString());
            }
            catch (Exception e) {
                Debug.WriteLine(e);
            }
            finally {
                this._configJsonWatcher.EnableRaisingEvents = true;
            }
        }

        public virtual void Load()
        {
            if (!File.Exists(SettingFile)) {
                using (var _ = File.Create(SettingFile)) { };
            }
            var jsonText = File.ReadAllText(SettingFile);
            if (string.IsNullOrEmpty(jsonText)) {
                this.Save();
                return;
            }

            var json = JSONNode.Parse(jsonText);

            foreach (var jsonValue in json) {
                var prop = this.GetType().GetProperty(jsonValue.Key);
                if (prop == null) {
                    continue;
                }
                try {
                    if (double.TryParse($"{jsonValue.Value}", out var num)) {
                        prop.SetValue(this, num);
                    }
                    else {
                        prop.SetValue(this, $"{jsonValue.Value.Value}");
                    }
                }
                catch (Exception e) {
                    Debug.WriteLine(e);
                }
            }
            this.OnConfigChanged?.Invoke();
        }
    }
}

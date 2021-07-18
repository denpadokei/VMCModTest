using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VMCModTest.Bases;
using VMCModTest.SimpleJSON;

namespace VMCModTest.Setting.Models
{
    public class GrobalSetting : BindableBase
    {
        public static readonly string CurrentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static readonly string SettingFile = Path.Combine(CurrentDir, $"{Assembly.GetExecutingAssembly().GetName().Name}.json");

        public static GrobalSetting Instance { get; } = new GrobalSetting();
        private GrobalSetting()
        {
            this.Load();
        }

        static GrobalSetting()
        {

        }

        /// <summary>説明 を取得、設定</summary>
        private string filePath_;
        /// <summary>説明 を取得、設定</summary>
        public string FilePath
        {
            get => this.filePath_;

            set => this.SetProperty(ref this.filePath_, value);
        }

        public void Save()
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
                catch (Exception) {
                }
            }
            File.WriteAllText(SettingFile, json.ToString());
        }

        public void Load()
        {
            if (!File.Exists(SettingFile)) {
                return;
            }
            var jsonText = File.ReadAllText(SettingFile);
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
                catch (Exception) {
                }
            }
        }
    }
}

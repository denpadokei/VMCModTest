using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEngine;
using VMCMod;
using VMCModTest.Core.Setting;
using Debug = UnityEngine.Debug;

namespace VMCModTest
{
    [VMCPlugin("VMCModTest", "1.0.0", "denpadokei")]
    public class Plugin : MonoBehaviour
    {
        public static readonly string SettingExePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "VMCModTest.Setting.exe");

        #region // Unity methods
        private void Awake()
        {
            // 生成時最初に呼び出されます。
            GrobalSetting.Instance.OnConfigChanged += this.OnConfigChanged;
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L)) {
                Debug.Log("Hello world.");
            }
        }

        private void OnDestroy()
        {
            GrobalSetting.Instance.OnConfigChanged -= this.OnConfigChanged;
        }
        #endregion

        /// <summary>
        /// 設定ファイルが変わったときに呼び出されます。
        /// </summary>
        private void OnConfigChanged()
        {

        }
        /// <summary>
        /// VMC上でSettingボタンを押したときに実行されるメソッドです。<br>
        /// publicにしておかないと呼び出されないので注意してください。
        /// </summary>
        [OnSetting]
        public void OnSetting()
        {
            Debug.Log(SettingExePath);
            Process.Start(SettingExePath);
        }
    }
}

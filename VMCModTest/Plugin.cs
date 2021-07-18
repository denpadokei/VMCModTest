using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;
using VMCMod;
using VMCModTest.Settings;
using Debug = UnityEngine.Debug;

namespace VMCModTest
{
    [VMCPlugin("VMCModTest", "1.0.0", "denpadokei")]
    public class Plugin : MonoBehaviour
    {
        public static readonly string SettingExePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "VMCModTest.Setting.exe");
        private WebServiceController _service;

        #region // Unity methods
        private void Awake()
        {
            // 生成時最初に呼び出されます。
            this._service = new WebServiceController();
        }

        private void Start()
        {
            this._service?.Start();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L)) {
                Debug.Log("Hello world.");
            }
        }

        private void OnDestroy()
        {
            this._service?.Stop();
        }
        #endregion

        /// <summary>
        /// VMC上でSettingボタンを押したときに実行されるメソッドです。<br>
        /// publicにしておかないと呼び出されないので注意してください。
        /// </summary>
        [OnSetting]
        public void OnSetting()
        {
            Debug.Log(SettingExePath);
            //Process.Start(SettingExePath);
            this._service?.Launch();
        }
    }
}

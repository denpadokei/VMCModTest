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
            // �������ŏ��ɌĂяo����܂��B
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
        /// �ݒ�t�@�C�����ς�����Ƃ��ɌĂяo����܂��B
        /// </summary>
        private void OnConfigChanged()
        {

        }
        /// <summary>
        /// VMC���Setting�{�^�����������Ƃ��Ɏ��s����郁�\�b�h�ł��B<br>
        /// public�ɂ��Ă����Ȃ��ƌĂяo����Ȃ��̂Œ��ӂ��Ă��������B
        /// </summary>
        [OnSetting]
        public void OnSetting()
        {
            Debug.Log(SettingExePath);
            Process.Start(SettingExePath);
        }
    }
}

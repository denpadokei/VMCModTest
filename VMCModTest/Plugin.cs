using System;
using UnityEngine;
using VMCMod;

namespace VMCModTest
{
    [VMCPlugin("VMCModTest", "1.0.0", "denpadokei")]
    public class Plugin : MonoBehaviour
    {
        #region // Unity methods
        private void Awake()
        {
            // �������ŏ��ɌĂяo����܂��B
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
            
        }
        #endregion
    }
}

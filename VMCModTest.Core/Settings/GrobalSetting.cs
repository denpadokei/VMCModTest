using System;
using VMCModTest.Core.Bases;

namespace VMCModTest.Core.Setting
{
    /// <summary>
    /// アプリケーション内からアクセスできる設定用クラスです。
    /// </summary>
    public class GrobalSetting : SettingBase<GrobalSetting>
    {
        /// <summary>説明 を取得、設定</summary>
        private string filePath_;
        /// <summary>説明 を取得、設定</summary>
        public string FilePath
        {
            get => this.filePath_;

            set => this.SetProperty(ref this.filePath_, value);
        }
    }
}

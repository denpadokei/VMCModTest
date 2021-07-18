using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VMCModTest.Core.Setting;

namespace VMCModTest.Setting.ViewModels
{
    /// <summary>
    /// <see cref="VMCModTest.Setting.Views.MainControll"/>のViewModelです。
    /// </summary>
    public class MainControllViewModel : BindableBase
    {
        /////////////////////////////////////////////////////
        #region // プロパティ
        /// <summary>ファイルパス を取得、設定</summary>
        private string filePath_;
        /// <summary>ファイルパス を取得、設定</summary>
        public string FilePath
        {
            get => this.filePath_;

            set => this.SetProperty(ref this.filePath_, value);
        }
        #endregion
        /////////////////////////////////////////////////////
        #region // コマンド
        private DelegateCommand _openFileDialogCommand;
        /// <summary>
        /// ファイルダイアログを開くコマンドです。
        /// </summary>
        public DelegateCommand OpenFileDialogCommand =>
            _openFileDialogCommand ?? (_openFileDialogCommand = new DelegateCommand(ExecuteOpenFileDialogCommand));

        void ExecuteOpenFileDialogCommand()
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() != true) {
                return;
            }

            this.FilePath = dlg.FileName;
        }
        #endregion
        /////////////////////////////////////////////////////
        #region // オーバーライドメソッド
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            GrobalSetting.Instance.FilePath = this.FilePath;
            GrobalSetting.Instance.Save();
        }
        #endregion
        /////////////////////////////////////////////////////
        #region // パブリックメソッド
        #endregion
        /////////////////////////////////////////////////////
        #region // プライベートメソッド
        private void OnSettingChanged(object sender, PropertyChangedEventArgs args)
        {
            this.FilePath = GrobalSetting.Instance.FilePath;
        }
        #endregion
        /////////////////////////////////////////////////////
        #region // メンバ変数
        #endregion
        /////////////////////////////////////////////////////
        #region // 構築・破棄
        public MainControllViewModel()
        {
            this.filePath_ = GrobalSetting.Instance.FilePath;
            WeakEventManager<INotifyPropertyChanged, PropertyChangedEventArgs>.AddHandler(
                GrobalSetting.Instance, nameof(INotifyPropertyChanged.PropertyChanged), this.OnSettingChanged);
        }
        #endregion
    }
}

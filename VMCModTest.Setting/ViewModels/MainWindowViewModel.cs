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
using VMCModTest.Setting.Models;

namespace VMCModTest.Setting.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            this.title_ = "VMCModTest";
            this.filePath_ = GrobalSetting.Instance.FilePath;
            WeakEventManager<INotifyPropertyChanged, PropertyChangedEventArgs>.AddHandler(
                GrobalSetting.Instance, nameof(INotifyPropertyChanged.PropertyChanged), this.OnSettingChanged);
        }

        /// <summary>ウインドウタイトル を取得、設定</summary>
        private string title_;
        /// <summary>ウインドウタイトル を取得、設定</summary>
        public string Title
        {
            get => this.title_;

            set => this.SetProperty(ref this.title_, value);
        }

        /// <summary>ファイルパス を取得、設定</summary>
        private string filePath_;
        /// <summary>ファイルパス を取得、設定</summary>
        public string FilePath
        {
            get => this.filePath_;

            set => this.SetProperty(ref this.filePath_, value);
        }

        private DelegateCommand _openFileDialogCommand;
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

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            GrobalSetting.Instance.FilePath = this.FilePath;
            GrobalSetting.Instance.Save();
        }

        private void OnSettingChanged(object sender, PropertyChangedEventArgs args)
        {
            this.FilePath = GrobalSetting.Instance.FilePath;
        }
    }
}

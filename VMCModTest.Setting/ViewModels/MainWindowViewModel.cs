using Prism.Mvvm;
using System;

namespace VMCModTest.Setting.ViewModels
{
    /// <summary>
    /// <see cref="MainWindow"/>のViewModelです。
    /// ここはあんまり何も書かない方がのちのち幸せになれます。
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            this.title_ = "VMCModTest";
        }

        /// <summary>ウインドウタイトル を取得、設定</summary>
        private string title_;
        /// <summary>ウインドウタイトル を取得、設定</summary>
        public string Title
        {
            get => this.title_;

            set => this.SetProperty(ref this.title_, value);
        }
    }
}

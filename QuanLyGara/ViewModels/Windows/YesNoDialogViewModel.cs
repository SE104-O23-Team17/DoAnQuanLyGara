﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace QuanLyGara.ViewModels.Windows
{
    public class YesNoDialogViewModel : ViewModelBase
    {
        private string _title;
        private string _content;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged("Content");
            }
        }
        public event Action<DialogResult> DialogClosed;
        public ICommand OKCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public YesNoDialogViewModel(string title, string content)
        {
            Title = title;
            Content = content;
            OKCommand = new ViewModelCommand(param =>
            {
                CloseDialog(DialogResult.OK);
                ExecuteCloseCommand(param);
            });
            CancelCommand = new ViewModelCommand(param =>
            {
                CloseDialog(DialogResult.Cancel);
                ExecuteCloseCommand(param);
            });
        }
        public YesNoDialogViewModel() { }
        public void CloseDialog(DialogResult result)
        {
            DialogClosed?.Invoke(result);
        }
        private void ExecuteCloseCommand(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }
    }
}

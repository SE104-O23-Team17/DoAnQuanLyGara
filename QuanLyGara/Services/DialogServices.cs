using QuanLyGara.ViewModels.Windows;
using QuanLyGara.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace QuanLyGara.Services
{
    public interface IDialogService
    {
        void ShowInfoDialog(string title, string content, Action okCommand);
        void ShowYesNoDialog(string title, string content, Action okCommand, Action cancelCommand);
    }

    public class DialogService : IDialogService
    {
        public void ShowInfoDialog(string title, string content, Action okCommand)
        {
            var dialogViewModel = new InfoDialogViewModel(title, content);
            dialogViewModel.OKCommand = new ViewModelCommand(param =>
            {
                okCommand?.Invoke();
                if (param is Window window)
                {
                    window.Close();
                }
                dialogViewModel.CloseDialog(DialogResult.OK);
            });

            var messageBox = new InfoDialog { DataContext = dialogViewModel };
            messageBox.ShowDialog();
        }

        public void ShowYesNoDialog(string title, string content, Action okCommand = null, Action cancelCommand = null)
        {
            var dialogViewModel = new YesNoDialogViewModel(title, content);
            dialogViewModel.OKCommand = new ViewModelCommand(param =>
            {
                okCommand?.Invoke();
                if (param is Window window)
                {
                    window.Close();
                }
                dialogViewModel.CloseDialog(DialogResult.OK);
            });
            dialogViewModel.CancelCommand = new ViewModelCommand(param =>
            {
                cancelCommand?.Invoke();
                if (param is Window window)
                {
                    window.Close();
                }
                dialogViewModel.CloseDialog(DialogResult.Cancel);
            });

            var messageBox = new YesNoDialog { DataContext = dialogViewModel };
            messageBox.ShowDialog();
        }
    }
}

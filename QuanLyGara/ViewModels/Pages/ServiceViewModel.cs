using QuanLyGara.Services;
using QuanLyGara.ViewModels.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.ViewModels.Pages
{
    public class ServiceViewModel : ViewModelBase
    {
        private IDialogService dialogService;
        public ServiceViewModel() {
            dialogService = new DialogService();
        }
    }
}

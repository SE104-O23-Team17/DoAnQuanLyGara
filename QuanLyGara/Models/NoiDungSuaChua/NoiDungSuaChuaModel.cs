using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.NoiDungSuaChua
{
    public class NoiDungSuaChuaModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NoiDungSuaChuaModel() {
            maNDSC = 0;
            tenNDSC = "";
            giaTien = 0;
            isReadOnly = true;
        }
        public int maNDSC { get; set; }
        private string tenNDSC;
        public string TenNDSC
        {
            get { return tenNDSC; }
            set
            {
                if (tenNDSC != value)
                {
                    tenNDSC = value;
                    OnPropertyChanged(nameof(TenNDSC));
                    OnPropertyChanged(nameof(giaTien));
                }
            }
        }
        public double giaTien { get; set; }

        private bool isReadOnly;
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                if (isReadOnly != value)
                {
                    isReadOnly = value;
                    OnPropertyChanged(nameof(IsReadOnly));
                }
            }
        }

        public override string ToString()
        {
            return tenNDSC;
        }
    }
}

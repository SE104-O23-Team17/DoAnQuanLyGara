using System;
using System.ComponentModel;

namespace QuanLyGara.Models.ThamSo
{
    public class ThamSoModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private float tiLeTinhDonGiaBan;
        public float TiLeTinhDonGiaBan
        {
            get { return tiLeTinhDonGiaBan; }
            set
            {
                if (tiLeTinhDonGiaBan != value)
                {
                    tiLeTinhDonGiaBan = value;
                    OnPropertyChanged(nameof(TiLeTinhDonGiaBan));
                }
            }
        }

        private int soXeSuaChuaToiDa;
        public int SoXeSuaChuaToiDa
        {
            get { return soXeSuaChuaToiDa; }
            set
            {
                if (soXeSuaChuaToiDa != value)
                {
                    soXeSuaChuaToiDa = value;
                    OnPropertyChanged(nameof(SoXeSuaChuaToiDa));
                }
            }
        }

        private bool apDungQuyDinhKiemTraSoTienThu;
        public bool ApDungQuyDinhKiemTraSoTienThu
        {
            get { return apDungQuyDinhKiemTraSoTienThu; }
            set
            {
                if (apDungQuyDinhKiemTraSoTienThu != value)
                {
                    apDungQuyDinhKiemTraSoTienThu = value;
                    OnPropertyChanged(nameof(ApDungQuyDinhKiemTraSoTienThu));
                }
            }
        }
    }
}

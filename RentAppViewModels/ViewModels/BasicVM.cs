using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataBase1WPF.ViewModels
{
    public class BasicVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName=null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual bool Set<T>(ref T field , T value, [CallerMemberName]string propertyName=null )
        {
            if (Equals(field, value))
                return false;

            field = value;

            OnPropertyChanged(propertyName);
            return true;
        }
    }
}

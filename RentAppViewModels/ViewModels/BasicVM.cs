using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataBase1WPF.ViewModels
{
    /// <summary>
    /// Базовый класс view model 
    /// </summary>
    public class BasicVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вызов события PropertyChanged
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged([CallerMemberName]string propertyName=null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Установка переданного значения
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
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

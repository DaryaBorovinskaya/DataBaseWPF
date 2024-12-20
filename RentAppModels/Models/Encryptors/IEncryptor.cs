using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataBase1WPF.Models.Encryptors
{
    public interface IEncryptor<ReturnType, ValueType> where ReturnType : notnull where ValueType : notnull
    {
        public ReturnType Encrypt(ValueType value);
    }
}

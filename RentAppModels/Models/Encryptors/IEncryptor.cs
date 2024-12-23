namespace DataBase1WPF.Models.Encryptors
{
    public interface IEncryptor<ReturnType, ValueType> where ReturnType : notnull where ValueType : notnull
    {
        public ReturnType Encrypt(ValueType value);
    }
}

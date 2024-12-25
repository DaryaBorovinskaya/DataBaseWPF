using System.ComponentModel;
using System.Data;

namespace DataBase1WPF.Models.Services
{
    /// <summary>
    /// Преобразователь в таблицу DataTable
    /// </summary>
    public static class DataTableConverter
    {
        /// <summary>
        /// Создание таблицы DataTable по данных типа List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(List<T> list)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new ();

            foreach (PropertyDescriptor prop in props)
            {
                if (prop.DisplayName != null)
                {
                    table.Columns.Add(prop.DisplayName, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
            }
            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in props)
                {
                    if (prop.DisplayName != null)
                    {
                        row[prop.DisplayName] = prop.GetValue(item) ?? DBNull.Value;
                    }
                }
                table.Rows.Add(row);
            }
            return table;
        }
    }
}

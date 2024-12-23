using System.ComponentModel;
using System.Data;

namespace DataBase1WPF.Models.Services
{
    public static class DataTableConverter
    {
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

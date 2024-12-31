using System.Data;

namespace UtilityExtensions
{
    public static class DatatableExtensions
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            var dataTable = new DataTable();
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in data)
            {
                var row = dataTable.NewRow();

                foreach (var prop in properties)
                {
                    var value = prop.GetValue(item, null);
                    row[prop.Name] = value ?? DBNull.Value;
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}
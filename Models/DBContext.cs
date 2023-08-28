using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace PolicyDetails.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions paramOptions) : base(paramOptions)
        {
        }
        public DbSet<PolicyData> PolicyData { get; set; }

        public List<PolicyData> getPolicyDataList(string paramPolicyCode)
        {
            DataTable dt = new DataTable();
            string cnnString = "Server= (local)\\SQLEXPRESS;DataBase=SunnyDB;trusted_connection=True;Encrypt=False;MultipleActiveResultSets=True;";

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CustoemerCode", paramPolicyCode));
            cmd.CommandText = "SP_GetPolicyDetails";
            cnn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            //object o = cmd.ExecuteScalar();
            cnn.Close();
            List<PolicyData> _objPolicyDataList = ConvertDataTable<PolicyData>(dt);

            return _objPolicyDataList;
        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace PolicyDetails.Models
{
    public class DBContext : DbContext
    {
        public static string _strConn = "Server= (local)\\SQLEXPRESS;DataBase=SunnyDB;trusted_connection=True;Encrypt=False;MultipleActiveResultSets=True;";
        public static DataTable _dt = new DataTable();
        public DBContext(DbContextOptions paramOptions) : base(paramOptions)
        {
        }
        public DbSet<PolicyData> PolicyData { get; set; }

        public static DataTable getPolicyData(string paramPolicyCode)
        {
            SqlConnection cnn = new SqlConnection(_strConn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CustoemerCode", paramPolicyCode));
            cmd.CommandText = "SP_GetPolicyDetails";
            cnn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(_dt);
            cnn.Close();
            return _dt;
        }
        public static DataTable getPolicyTransactionData(PolicyTransactionRequest paramReqData)
        {
            SqlConnection cnn = new SqlConnection(_strConn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PolicyNo", paramReqData.PolicyNo));
            cmd.Parameters.Add(new SqlParameter("@StartDate", paramReqData.StartDate));
            cmd.Parameters.Add(new SqlParameter("@EndDate", paramReqData.EndDate));
            cmd.CommandText = "SP_GetPolicyTransaction";
            cnn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(_dt);
            cnn.Close();
            return _dt;
        }
        public static List<T> ConvertDataTable<T>(DataTable dt)
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

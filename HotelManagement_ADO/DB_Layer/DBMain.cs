using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace HotelManagement_ADO.DB_Layer
{
    public class DBMain
    {
        SqlConnection conn = null;
        SqlCommand comm = null;
        SqlDataAdapter da = null;
        public static string username, password;

        public static string ConnStr = "Data Source=DESKTOP-9118KPA;Initial Catalog=HotelManagementSystem;Integrated Security=False;User ID=sa;Password=123";

        public DBMain()
        {
            conn = new SqlConnection(ConnStr);
            comm = conn.CreateCommand();
        }
        public static void SetConnStr(string newConnStr, string User, string Pass)
        {
            ConnStr = newConnStr;
            username = User;
            password = Pass;
        }
        public DataSet ExecuteQueryDataSet(string strSQL, CommandType ct)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            using (SqlCommand comm = new SqlCommand(strSQL, conn))
            {
                comm.CommandType = ct;
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        public DataSet ExecuteQueryDataSet2(string strSQL, CommandType ct, SqlParameter parameter = null)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            comm.CommandText = strSQL;
            comm.CommandType = ct;
            comm.Parameters.Clear(); // Clear any existing parameters
            if (parameter != null)
            {
                comm.Parameters.Add(parameter);
            }
            da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataSet ExecuteQueryDataSet3(string strSQL, CommandType ct, params SqlParameter[] parameters)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            comm.CommandText = strSQL;
            comm.CommandType = ct;
            comm.Parameters.Clear(); // Clear any existing parameters
            if (parameters != null)
            {
                comm.Parameters.AddRange(parameters);
            }
            da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public void MyExecuteNonQuery(string sql)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
        public bool MyExecuteNonQueryParameter(string strSQL, CommandType ct, ref string error, SqlParameter[] parameters)
        {
            bool f = false;
            SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500);
            errorMessageParam.Direction = ParameterDirection.Output;
            comm.Parameters.Add(errorMessageParam);

            if (conn.State == ConnectionState.Open)
                conn.Close();

            try
            {
                conn.Open();
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = strSQL;
                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        comm.Parameters.Add(parameter);
                    }
                }

                int rowsAffected = comm.ExecuteNonQuery();

                if (errorMessageParam.Value != DBNull.Value && !string.IsNullOrEmpty(errorMessageParam.Value.ToString()))
                {
                    error = errorMessageParam.Value.ToString();
                }
                else
                {
                    f = true;
                }
            }
            catch (SqlException ex)
            {
                error = ex.Message;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return f;
        }
        public SqlDataReader ExecuteQueryDataReader(string strSQL, CommandType ct)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            comm.CommandText = strSQL;
            comm.CommandType = ct;
            SqlDataReader MyDtReader = comm.ExecuteReader();
            return MyDtReader;
        }
    }
}

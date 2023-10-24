using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HotelManagement_ADO.DB_Layer;

namespace HotelManagement_ADO.BS_Layer
{
    public class BLUsers
    {
        DBMain db = null;
        public BLUsers()
        {
            db = new DBMain();
        }
        public DataSet TakeUser()
        {
            return db.ExecuteQueryDataSet("SELECT * FROM View_Users", CommandType.Text);
        }
        public DataSet FindUser(int ID, string Name)
        {
            string query = "SELECT * FROM Users WHERE userID = @ID AND Fullname = @Name";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", ID),
                new SqlParameter("@Name", Name)
            };
            return db.ExecuteQueryDataSet3(query, CommandType.Text, parameters);
        }
        public bool AddUser(string Fullname, string password, DateTime Birthday, bool Gender, string Email, string Phone_Number, string Address, int role_id, ref string err)
        {
            try
            {
                string genderValue = Gender ? "1" : "0";
                string sql = $"EXEC SP_ADD_USER N'{Fullname}', '{Birthday.ToString()}', '{genderValue}', '{Email}', '{Phone_Number}', '{Address}', {role_id}, '{password}'";
                db.MyExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
            return true;
        }
        public bool DeleteUser(ref string err, int userID)
        {
            try
            {
                string sql = $"EXEC SP_DELETE_USER {userID}";
                db.MyExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
            return true;
        }
        public bool UpdateUser(int userID, string fullName, string password, DateTime birthday, bool gender, string email, string phoneNumber, string address, int roleID, ref string err)
        {
            try
            {
                string genderValue = gender ? "1" : "0";
                string sql = $"EXEC SP_UPDATE_USER {userID}, N'{fullName}', '{birthday.ToString()}', '{genderValue}', '{email}', '{phoneNumber}', '{address}', {roleID}, '{password}'";
                db.MyExecuteNonQuery(sql);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
        }
    }
}

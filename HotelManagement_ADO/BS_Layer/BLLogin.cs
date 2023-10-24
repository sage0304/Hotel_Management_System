using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement_ADO.DB_Layer;
using System.Web.UI.WebControls;

namespace HotelManagement_ADO.BS_Layer
{
    public class BLLogin
    {
        DBMain db = null;
        public BLLogin()
        {
            db = new DBMain();
        }
        public bool CheckLogin(string username, string password, out string storedUsername, out int role, out string fullName, out int UserID)
        {
            bool result = false;
            storedUsername = null;
            fullName = null;
            role = 0;
            UserID = 0;
            string strSql = "SELECT Email, password, role_id, Fullname, userID FROM Users";
            SqlDataReader read = null;
            read = db.ExecuteQueryDataReader(strSql, CommandType.Text);
            while (read.Read())
            {
                string email = read.GetValue(0).ToString().Trim();
                string storedPassword = read.GetValue(1).ToString().Trim();
                role = Convert.ToInt32(read.GetValue(2).ToString().Trim());
                fullName = read.GetValue(3).ToString().Trim();
                storedUsername = email.Substring(0, email.IndexOf("@"));
                UserID = Convert.ToInt32(read.GetValue(4).ToString().Trim());
                if (storedUsername == username && storedPassword == password)
                {
                    result = true;
                    string newConnect = "Data Source=DESKTOP-9118KPA;Initial Catalog=HotelManagementSystem;User ID=" + username + ";Password=" + password;
                    DBMain.SetConnStr(newConnect, username, password);
                    break;
                }
            }
            return result;
        }
    }
}

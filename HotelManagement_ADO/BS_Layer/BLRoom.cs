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
    public class BLRoom
    {
        DBMain db = null;
        public BLRoom()
        {
            db = new DBMain();
        }
        public DataSet TakeRoom()
        {
            return db.ExecuteQueryDataSet("SELECT * FROM View_Room", CommandType.Text);
        }
        public DataSet FindRoom(string Room_no)
        {
            string query = "SELECT * FROM Room WHERE room_No = @Room_no";
            SqlParameter parameter = new SqlParameter("@Room_no", Room_no);
            return db.ExecuteQueryDataSet2(query, CommandType.Text, parameter);
        }
        public bool AddRoom(string room_No, string Type, int Capacity, double Price, ref string err)
        {
            try
            {
                string sql = $"EXEC SP_ADD_ROOM '{room_No}', '{Type}', {Capacity}, {Price}";
                db.MyExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
            return true;
        }
        public bool DeleteRoom(ref string err, int roomID)
        {
            try
            {
                string sql = $"EXEC SP_DELETE_ROOM {roomID}";
                db.MyExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
            return true;
        }
        public bool UpdateRoom(int roomID, string room_No, string Type, int Capacity, double Price, ref string err)
        {
            try
            {
                string sql = $"EXEC SP_UPDATE_ROOM {roomID}, '{room_No}', '{Type}', {Capacity}, {Price}";
                db.MyExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
            return true;
        }
    }
}

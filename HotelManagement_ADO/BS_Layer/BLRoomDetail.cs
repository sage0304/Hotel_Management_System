using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HotelManagement_ADO.DB_Layer;

namespace HotelManagement_ADO.BS_Layer
{
    public class BLRoomDetail
    {
        DBMain db = null;
        public BLRoomDetail()
        {
            db = new DBMain();
        }
        public DataSet TakeRoomDetail()
        {
            return db.ExecuteQueryDataSet("SELECT * FROM View_RoomDetail", CommandType.Text);
        }
        public bool AddRoomDetail(int book_ID,int room_ID, ref string err)
        {
            try
            {
                string sql = $"EXEC SP_ADD_ROOM_DETAIL {book_ID},{room_ID}";
                db.MyExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
            return true;
        }
        public bool DeleteRoomDetail(ref string err, int book_ID, int room_ID)
        {
            try
            {
                string sql = $"EXEC SP_DELETE_ROOM_DETAIL {book_ID}, {room_ID}";
                db.MyExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
            return true;
        }
        public bool UpdateRoomDetail(int book_ID, int room_ID, ref string err)
        {
            try
            {
                string sql = $"EXEC SP_UPDATE_ROOM_DETAIL {book_ID}, {room_ID}";
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

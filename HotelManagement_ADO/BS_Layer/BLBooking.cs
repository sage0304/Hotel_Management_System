using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement_ADO.DB_Layer;


namespace HotelManagement_ADO.BS_Layer
{
    public class BLBooking
    {
        DBMain db = null;
        public BLBooking()
        {
            db = new DBMain();
        }
        public DataSet TakeBooking()
        {
            return db.ExecuteQueryDataSet("UPDATE Booking SET TotalPrice = 0 SELECT * FROM View_Booking", CommandType.Text);
        }
        public bool AddBooking(int staffID, int cusID,int cusAmount, DateTime checkIn, DateTime checkOut, ref string err)
        {
            try
            {
                string sql = $"EXEC SP_ADD_BOOKING {staffID}, {cusID},{cusAmount}, '{checkIn.ToString()}', '{checkOut.ToString()}'";
                db.MyExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
            return true;
        }
        public bool DeleteBooking(ref string err, int bookID)
        {
            try
            {
                string sql = $"EXEC SP_DELETE_BOOKING {bookID}";
                db.MyExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
            return true;
        }
        public bool UpdateBooking(int bookID,int staffID, int cusID,int cusAmount, DateTime checkIn, DateTime checkOut, ref string err)
        {
            try
            {
                string sql = $"EXEC SP_UPDATE_BOOKING {bookID},{staffID}, {cusID},{cusAmount}, '{checkIn.ToString()}', '{checkOut.ToString()}'";
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

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
    public class BLServiceDetail
    {
        DBMain db = null;
        public BLServiceDetail()
        {
            db = new DBMain();
        }
        public DataSet TakeServiceDetail()
        {
            return db.ExecuteQueryDataSet("SELECT * FROM View_ServiceDetail", CommandType.Text);
        }
        public bool AddServiceDetail(int service_ID, int book_ID, int NumberofUser, int Amount, DateTime Buy_Date, ref string err)
        {
            try
            {
                string sql = $"EXEC SP_ADD_SERVICE_DETAIL {service_ID},{book_ID}, {NumberofUser},{Amount}, '{Buy_Date.ToString()}'";
                db.MyExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
            return true;
        }
        public bool DeleteServiceDetail(ref string err, int service_ID,int book_ID, DateTime Buy_Date)
        {
            try
            {
                string sql = $"exec SP_DELETE_SERVICE_DETAIL {service_ID},{book_ID},'{Buy_Date.ToString()}'";
                db.MyExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
            return true;
        }
        public bool UpdateServiceDetail(int service_ID, int book_ID, int NumberofUser,int Amount, DateTime Buy_Date, ref string err)
        {
            try
            {
                string sql = $"EXEC SP_UPDATE_SERVICE_DETAIL {service_ID},{book_ID}, {NumberofUser},{Amount}, '{Buy_Date.ToString()}'";
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

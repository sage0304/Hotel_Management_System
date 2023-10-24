using HotelManagement_ADO.DB_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement_ADO.BS_Layer
{
    public class BLDamagedItem
    {
        DBMain db = null;
        public BLDamagedItem()
        {
            db = new DBMain();
        }
        public DataSet TakeDamagedItem()
        {
            return db.ExecuteQueryDataSet("SELECT * FROM View_DamagedItem", CommandType.Text);
        }
        public bool AddDamagedItem(string itemName, int book_ID, int DamagedAmount, ref string err)
        {
            try
            {
                string sql = $"EXEC SP_ADD_DAMAGED_ITEM {itemName},{book_ID}, {DamagedAmount}";
                db.MyExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
            return true;
        }
        public bool DeleteDamagedItem(ref string err, int itemID,int book_ID)
        {
            try
            {
                string sql = $"EXEC SP_DELETE_DAMAGED_ITEM {itemID},{book_ID }";
                db.MyExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
            return true;
        }
        public bool UpdateDamagedItem(string itemName, int book_ID, int DamagedAmount,ref string err)
        {
            try
            {
                string sql = $"EXEC SP_UPDATE_DAMAGED_ITEM {itemName},{book_ID}, {DamagedAmount}";
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

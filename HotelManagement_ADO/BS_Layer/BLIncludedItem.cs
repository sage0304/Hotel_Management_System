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
    public class BLIncludedItem
    {
        DBMain db = null;
        public BLIncludedItem()
        {
            db = new DBMain();
        }
        public DataSet TakeIncludedItem()
        {
            return db.ExecuteQueryDataSet("SELECT * FROM View_IncludedItem", CommandType.Text);
        }
        public bool AddIncludedItem(string itemName, string roomType,double iiPrice,int Amount, ref string err)
        {
            try
            {
                string sql = $"EXEC SP_ADD_INCLUDED_ITEM {itemName}, {roomType},{iiPrice}, '{Amount}'";
                db.MyExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
            return true;
        }
        public bool DeleteIncludedItem(ref string err, int itemID)
        {
            try
            {
                string sql = $"EXEC SP_DELETE_INCLUDED_ITEM {itemID}";
                db.MyExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
            return true;
        }
        public bool UpdateIncludedItem(int itemID, string itemName, string roomType, double iiPrice, int Amount, ref string err)
        {
            try
            {
                string sql = $"EXEC SP_UPDATE_INCLUDED_ITEM {itemID},{itemName}, {roomType},{iiPrice}, {Amount}";
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

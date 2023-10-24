using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using HotelManagement_ADO.DB_Layer;

namespace HotelManagement_ADO.BS_Layer
{
    public class BLService
    {
        DBMain db = null;
        public BLService()
        {
            db = new DBMain();
        }
        public DataSet TakeService()
        {
            return db.ExecuteQueryDataSet("SELECT * FROM View_Service", CommandType.Text);
        }
        public bool AddService(string Title,double Price, int Amount,string UnitandNote,ref string err)
        {
            try
            {
                string sql = $"EXEC SP_ADD_SERVICE '{Title}', '{Price}', '{Amount}', {UnitandNote}";
                db.MyExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
            return true;

        }
        public bool DeleteService(ref string err, int serID)
        {
            try
            {
                string sql = $"EXEC SP_DELETE_SERVICE {serID}";
                db.MyExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                return false;
            }
            return true;
        }
        public bool UpdateService(int serID,string Title, double Price, int Amount, string UnitandNote, ref string err)
        {
            try
            {
                string sql = $"EXEC SP_UPDATE_SERVICE '{serID}','{Title}', '{Price}', '{Amount}', {UnitandNote}";
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

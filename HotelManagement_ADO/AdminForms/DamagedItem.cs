using HotelManagement_ADO.BS_Layer;
using HotelManagement_ADO.DB_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HotelManagement_ADO.AdminForms
{
    public partial class DamagedItem : Form
    {
        bool Them;
        string err;
        BLDamagedItem dbDamagedItem = new BLDamagedItem();

        DBMain db = new DBMain();
        int formamount;
        int amount;
        int mouseclick = 0;

        public DamagedItem()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {
                // Transfer data to DataGridView
                DataSet dataSet = dbDamagedItem.TakeDamagedItem();
                DataTable dataTable = dataSet.Tables[0];
                // Set the DataSource of the DataGridView
                dgvDAMAGEDITEM.DataSource = dataTable;
                // Delete all contents of each box in panel
                this.txtitemID.ResetText();
                this.txtitemName.ResetText();
                this.txtbookID.ResetText();
                if (string.IsNullOrEmpty(this.txtdiAmount.Text))
                {
                    formamount = 0;
                }
                else
                {
                    formamount = Convert.ToInt32(this.txtdiAmount.Text);
                }
                this.txtdiAmount.ResetText();
                this.txtdiPrice.ResetText();
                // Ban manipulation on buttons Save / Cancel
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;
                this.panel.Enabled = false;
                // Allow manipulation on buttons Add / Update / Delete / Back
                this.btnAdd.Enabled = true;
                this.btnFix.Enabled = true;
                this.btnDelete.Enabled = true;

                //
                dgvDAMAGEDITEM_CellClick(null, null);
            }
            catch
            {
                MessageBox.Show("Cannot access to table DamagedItem. An error occurred!!!");
            }
        }
        private void dgvDAMAGEDITEM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Order of current record
            int r = dgvDAMAGEDITEM.CurrentCell.RowIndex;
            // Transfer data to panel
            this.txtbookID.Text = dgvDAMAGEDITEM.Rows[r].Cells[0].Value.ToString();
            this.txtitemID.Text = dgvDAMAGEDITEM.Rows[r].Cells[1].Value.ToString();
            this.txtitemName.Text = dgvDAMAGEDITEM.Rows[r].Cells[2].Value.ToString();
            this.txtdiAmount.Text = dgvDAMAGEDITEM.Rows[r].Cells[3].Value.ToString();
            formamount = Convert.ToInt32(this.txtdiAmount.Text);
            this.txtdiPrice.Text = dgvDAMAGEDITEM.Rows[r].Cells[4].Value.ToString();
        }
        private void DamagedItem_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.txtitemID.Enabled = false;
            this.txtdiPrice.Enabled = false;
            // Activate Them variable
            Them = true;
            // Delete all contents of each box in panel
            this.txtitemID.ResetText();
            this.txtitemName.ResetText();
            this.txtbookID.ResetText();
            this.txtdiAmount.ResetText();
            if (string.IsNullOrEmpty(this.txtdiAmount.Text))
            {
                formamount = 0;
            }
            else
            {
                formamount = Convert.ToInt32(this.txtdiAmount.Text);
            }
            this.txtdiPrice.ResetText();
            // Allow manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel.Enabled = true;
            // Ban manipulation on buttons Add / Update / Delete / Back
            this.btnAdd.Enabled = false;
            this.btnFix.Enabled = false;
            this.btnDelete.Enabled = false;

            // Point to textfield txtbookID
            this.txtbookID.Focus();
        }
        private void btnFix_Click(object sender, EventArgs e)
        {
            // Activate Fix variable
            Them = false;
            // Allow manipulation in panel
            this.panel.Enabled = true;
            this.txtitemID.Enabled = false;
            this.txtdiPrice.Enabled = false;
            dgvDAMAGEDITEM_CellClick(null, null);
            // Allow manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel.Enabled = true;
            // Ban manipulation on buttons Add / Fix / Delete / Back
            this.btnAdd.Enabled = false;
            this.btnFix.Enabled = false;
            this.btnDelete.Enabled = false;

            // Point to textfield txtbookID
            this.txtitemID.Enabled = false;
            this.txtbookID.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Execute command
                // Get the order of current record
                int r = dgvDAMAGEDITEM.CurrentCell.RowIndex;
                string strDI1 = dgvDAMAGEDITEM.Rows[r].Cells[1].Value.ToString();
                string strDI2 = dgvDAMAGEDITEM.Rows[r].Cells[0].Value.ToString();
                // Write SQL command
                // Announce delete info confirmation
                // Declare answering variable
                DialogResult ans;
                // Display Q&A box
                ans = MessageBox.Show("Are you sure deleting this?", "Answer",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Check if press OK button
                if (ans == DialogResult.Yes)
                {
                    dbDamagedItem.DeleteDamagedItem(ref err, Convert.ToInt32(strDI1), Convert.ToInt32(strDI2));
                    // Reupdate DataGridView
                    LoadData();
                    // Announce
                    MessageBox.Show("Delete successfully!");
                }
                else
                {
                    // Announce
                    MessageBox.Show("Cancel deleting record!");
                }
            }
            catch
            {
                MessageBox.Show("Cannot delete this. An error occurred!!!");
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            // Declare answering variable
            DialogResult ans;
            // Display Q&A box
            ans = MessageBox.Show("Are you sure?", "Answer",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            // Check if press OK button
            if (ans == DialogResult.OK) this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Delete all contents of each box in panel
            this.txtitemID.ResetText();
            this.txtitemName.ResetText();
            this.txtbookID.ResetText();
            this.txtdiAmount.ResetText();
            this.txtdiPrice.ResetText();
            // Allow manipulation on buttons Add / Update / Delete / Back
            this.btnAdd.Enabled = true;
            this.btnFix.Enabled = true;
            this.btnDelete.Enabled = true;
            // Ban manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;
            this.panel.Enabled = false;
            dgvDAMAGEDITEM_CellClick(null, null);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Open connection
            // Add data
            if (Them)
            {
                BLDamagedItem di = new BLDamagedItem();
                if (di.AddDamagedItem(this.txtitemName.Text,
                                       Convert.ToInt32(this.txtbookID.Text),
                                       Convert.ToInt32(this.txtdiAmount.Text), ref err))
                    MessageBox.Show("Add successfully!");
                LoadData();
            }
            else
            {
                // Execute command
                BLDamagedItem di = new BLDamagedItem();
                di.UpdateDamagedItem(this.txtitemName.Text,
                                       Convert.ToInt32(this.txtbookID.Text),
                                       Convert.ToInt32(this.txtdiAmount.Text), ref err);
                // Reload data to DataGridView
                LoadData();
                // Announce
                MessageBox.Show("Update successfully!");
            }
            // Close connection
        }
        void checkAmount(string diname, int bookID)
        {
            SqlDataReader reader = db.ExecuteQueryDataReader($"SELECT II.iiAmount FROM [dbo].[IncludedItem] II INNER JOIN [dbo].[Room] R ON R.Type = II.roomType INNER JOIN [dbo].[RoomDetail] RD ON R.roomID = RD.room_ID WHERE II.itemName = '{diname}' AND RD.book_ID = {bookID}", CommandType.Text);
            while (reader.Read())
            {
                amount = reader.GetInt32(0);
            }
        }
        private void txtdiAmount_TextChanged(object sender, EventArgs e)
        {
            if (mouseclick == 1)
            {
                if (!string.IsNullOrEmpty(this.txtdiAmount.Text) && Convert.ToInt32(this.txtdiAmount.Text) != 0)
                {
                    checkAmount(this.txtitemName.Text, Convert.ToInt32(this.txtbookID.Text));
                    if (amount < Convert.ToInt32(this.txtdiAmount.Text))
                    {
                        MessageBox.Show("Your input exceeds the limitation!");
                    }
                }
            }
        }
        private void txtdiAmount_MouseClick(object sender, MouseEventArgs e)
        {
            mouseclick = 1;
        }
    }
}

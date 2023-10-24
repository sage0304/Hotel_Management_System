using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement_ADO.BS_Layer;

namespace HotelManagement_ADO.AdminForms
{
    public partial class Room : Form
    {
        bool Them;
        string err;
        BLRoom dbRO = new BLRoom();
        bool bsearch = false;
        public Room()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {
                // Transfer data to DataGridView
                if (bsearch)
                {
                    DataSet dataSet = dbRO.FindRoom(textRoom_no.Text);
                    DataTable dataTable = dataSet.Tables[0];
                    // Set the DataSource of the DataGridView
                    dgvROOM.DataSource = dataTable;
                    bsearch = false;
                }
                else
                {
                    DataSet dataSet = dbRO.TakeRoom();
                    DataTable dataTable = dataSet.Tables[0];
                    // Set the DataSource of the DataGridView
                    dgvROOM.DataSource = dataTable;
                    // Resize column
                    dgvROOM.AutoResizeColumns();
                }
                // Delete all contents of each box in panel
                this.txtroomID.ResetText();
                this.txtroom_No.ResetText();
                this.txtType.ResetText();
                this.txtCapacity.ResetText();
                this.txtPrice.ResetText();
                // Ban manipulation on buttons Save / Cancel
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;
                this.panel.Enabled = false;
                // Allow manipulation on buttons Add / Update / Delete / Back
                this.btnAdd.Enabled = true;
                this.btnFix.Enabled = true;
                this.btnDelete.Enabled = true;

                //
                dgvROOM_CellClick(null, null);
            }
            catch
            {
                MessageBox.Show("Cannot access to table Room. An error occurred!!!");
            }
        }
        private void dgvROOM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Order of current record
            int r = dgvROOM.CurrentCell.RowIndex;
            // Transfer data to panel
            this.txtroomID.Text = dgvROOM.Rows[r].Cells[0].Value.ToString();
            this.txtroom_No.Text = dgvROOM.Rows[r].Cells[1].Value.ToString();
            this.txtType.Text = dgvROOM.Rows[r].Cells[2].Value.ToString();
            this.txtCapacity.Text = dgvROOM.Rows[r].Cells[3].Value.ToString();
            this.txtPrice.Text = dgvROOM.Rows[r].Cells[4].Value.ToString();
        }
        private void FormRoom_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnReLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.txtroomID.Enabled = true;
            // Activate Them variable
            Them = true;
            // Delete all contents of each box in panel
            int newRoomID = Convert.ToInt32(dgvROOM.Rows[dgvROOM.Rows.Count - 2].Cells[0].Value) + 1;

            this.txtroomID.Text = newRoomID.ToString();
            this.txtroom_No.ResetText();
            this.txtType.ResetText();
            this.txtCapacity.ResetText();
            this.txtPrice.ResetText();
            // Allow manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel.Enabled = true;
            // Ban manipulation on buttons Add / Update / Delete / Back
            this.btnAdd.Enabled = false;
            this.btnFix.Enabled = false;
            this.btnDelete.Enabled = false;

            // Point to textfield txtroom_No
            this.txtroom_No.Focus();
        }
        private void btnFix_Click(object sender, EventArgs e)
        {
            // Activate Fix variable
            Them = false;
            // Allow manipulation in panel
            this.panel.Enabled = true;
            dgvROOM_CellClick(null, null);
            // Allow manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel.Enabled = true;
            // Ban manipulation on buttons Add / Fix / Delete / Back
            this.btnAdd.Enabled = false;
            this.btnFix.Enabled = false;
            this.btnDelete.Enabled = false;

            // Point to textfield txtroom_No
            this.txtroomID.Enabled = false;
            this.txtroom_No.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Execute command
                // Get the order of current record
                int r = dgvROOM.CurrentCell.RowIndex;
                string strRO = dgvROOM.Rows[r].Cells[0].Value.ToString();
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
                    dbRO.DeleteRoom(ref err, Convert.ToInt32(strRO));
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
            this.txtroomID.ResetText();
            this.txtroom_No.ResetText();
            this.txtType.ResetText();
            this.txtCapacity.ResetText();
            this.txtPrice.ResetText();
            // Allow manipulation on buttons Add / Update / Delete / Back
            this.btnAdd.Enabled = true;
            this.btnFix.Enabled = true;
            this.btnDelete.Enabled = true;
            // Ban manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;
            this.panel.Enabled = false;
            dgvROOM_CellClick(null, null);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Open connection
            // Add data
            if (Them)
            {
                // Thực hiện lệnh
                BLRoom blRo = new BLRoom();
                if (blRo.AddRoom( this.txtroom_No.Text, 
                                  this.txtType.Text, 
                                  Convert.ToInt32(this.txtCapacity.Text), 
                                  Convert.ToDouble(this.txtPrice.Text), ref err))
                    MessageBox.Show("Add successfully!");
                LoadData();

            }
            else
            {
                // Execute command
                BLRoom blRo = new BLRoom();
                blRo.UpdateRoom( Convert.ToInt32(this.txtroomID.Text), 
                                 this.txtroom_No.Text, 
                                 this.txtType.Text, 
                                 Convert.ToInt32(this.txtCapacity.Text), 
                                 Convert.ToDouble(this.txtPrice.Text), ref err);
                // Reload data to DataGridView
                LoadData();
                // Announce
                MessageBox.Show("Update successfully!");
            }
            // Close connection
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.bsearch = true;
            LoadData();
        }
    }
}

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
    public partial class RoomDetail : Form
    {
        bool Them;
        string err;
        BLRoomDetail dbRD = new BLRoomDetail();
        public RoomDetail()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {
                // Transfer data to DataGridView
                DataSet dataSet = dbRD.TakeRoomDetail();
                DataTable dataTable = dataSet.Tables[0];
                // Set the DataSource of the DataGridView
                dgvROOMDETAIL.DataSource = dataTable;
                // Delete all contents of each box in panel
                this.txtbook_ID.ResetText();
                this.txtroom_ID.ResetText();
                this.txtLengthStay.ResetText();
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
                dgvROOMDETAIL_CellClick(null, null);
            }
            catch
            {
                MessageBox.Show("Cannot access to table RoomDetail. An error occurred!!!");
            }
        }
        private void dgvROOMDETAIL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Order of current record
            int r = dgvROOMDETAIL.CurrentCell.RowIndex;
            // Transfer data to panel
            this.txtbook_ID.Text = dgvROOMDETAIL.Rows[r].Cells[0].Value.ToString();
            this.txtroom_ID.Text = dgvROOMDETAIL.Rows[r].Cells[1].Value.ToString();
            this.txtLengthStay.Text = dgvROOMDETAIL.Rows[r].Cells[2].Value.ToString();
            this.txtPrice.Text = dgvROOMDETAIL.Rows[r].Cells[3].Value.ToString();
        }
        private void FormRoomDetail_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnReLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.txtbook_ID.Enabled = true;
            this.txtroom_ID.Enabled = true;
            // Activate Them variable
            Them = true;
            // Delete all contents of each box in panel
            int newBookingID = Convert.ToInt32(dgvROOMDETAIL.Rows[dgvROOMDETAIL.Rows.Count - 2].Cells[0].Value) + 1;

            this.txtbook_ID.Text = newBookingID.ToString();
            this.txtroom_ID.ResetText();
            this.txtLengthStay.ResetText();
            this.txtPrice.ResetText();
            this.txtLengthStay.Enabled = false;
            this.txtPrice.Enabled = false;
            // Allow manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel.Enabled = true;
            // Ban manipulation on buttons Add / Update / Delete / Back
            this.btnAdd.Enabled = false;
            this.btnFix.Enabled = false;
            this.btnDelete.Enabled = false;

            // Point to textfield txtbook_ID
            this.txtbook_ID.Focus();
        }
        private void btnFix_Click(object sender, EventArgs e)
        {
            // Activate Fix variable
            Them = false;
            // Allow manipulation in panel
            this.panel.Enabled = true;
            this.txtLengthStay.Enabled = false;
            this.txtPrice.Enabled = false;
            dgvROOMDETAIL_CellClick(null, null);
            // Allow manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel.Enabled = true;
            // Ban manipulation on buttons Add / Fix / Delete / Back
            this.btnAdd.Enabled = false;
            this.btnFix.Enabled = false;
            this.btnDelete.Enabled = false;

            // Point to textfield txtbook_ID
            this.txtbook_ID.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Execute command
                // Get the order of current record
                int r = dgvROOMDETAIL.CurrentCell.RowIndex;
                string strRD1 = dgvROOMDETAIL.Rows[r].Cells[0].Value.ToString();
                string strRD2 = dgvROOMDETAIL.Rows[r].Cells[1].Value.ToString();
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
                    dbRD.DeleteRoomDetail(ref err, Convert.ToInt32(strRD1), Convert.ToInt32(strRD2));
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
            this.txtbook_ID.ResetText();
            this.txtroom_ID.ResetText();
            this.txtLengthStay.ResetText();
            this.txtPrice.ResetText();
            // Allow manipulation on buttons Add / Update / Delete / Back
            this.btnAdd.Enabled = true;
            this.btnFix.Enabled = true;
            this.btnDelete.Enabled = true;
            // Ban manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;
            this.panel.Enabled = false;
            dgvROOMDETAIL_CellClick(null, null);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Open connection
            // Add data
            if (Them)
            {
                BLRoomDetail blRd = new BLRoomDetail();
                if (blRd.AddRoomDetail( Convert.ToInt32(this.txtbook_ID.Text), 
                                        Convert.ToInt32(this.txtroom_ID.Text), ref err))
                    MessageBox.Show("Add successfully!");
                LoadData();
            }
            else
            {
                // Execute command
                BLRoomDetail blRd = new BLRoomDetail();
                blRd.UpdateRoomDetail( Convert.ToInt32(this.txtbook_ID.Text), 
                                       Convert.ToInt32(this.txtroom_ID.Text), ref err);
                // Reload data to DataGridView
                LoadData();
                // Announce
                MessageBox.Show("Update successfully!");
            }
            // Close connection
        }
    }
}

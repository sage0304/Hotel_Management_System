using System;
using HotelManagement_ADO.BS_Layer;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement_ADO.AdminForms
{
    public partial class Booking : Form
    {
        bool Them;
        string err;
        BLBooking dbBooking = new BLBooking();
        public Booking()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {
                // Transfer data to DataGridView
                DataSet dataSet = dbBooking.TakeBooking();
                DataTable dataTable = dataSet.Tables[0];
                // Set the DataSource of the DataGridView
                dgvBOOKING.DataSource = dataTable;
                // Delete all contents of each box in panel
                this.txtbook_ID.ResetText();
                this.txtStaff_ID.ResetText();
                this.txtCustomer_ID.ResetText();
                this.txtCustomer_Amount.ResetText();
                this.txtTotal_Price.ResetText();
                // Ban manipulation on buttons Save / Cancel
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;
                this.panel.Enabled = false;
                // Allow manipulation on buttons Add / Update / Delete / Back
                this.btnAdd.Enabled = true;
                this.btnFix.Enabled = true;
                this.btnDelete.Enabled = true;

                //
                dgvBOOKING_CellClick(null, null);
            }
            catch
            {
                MessageBox.Show("Cannot access to table Booking. An error occurred!!!");
            }
        }
        private void dgvBOOKING_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Order of current record
            int r = dgvBOOKING.CurrentCell.RowIndex;
            // Transfer data to panel
            this.txtbook_ID.Text = dgvBOOKING.Rows[r].Cells[0].Value.ToString();
            this.txtStaff_ID.Text = dgvBOOKING.Rows[r].Cells[1].Value.ToString();
            this.txtCustomer_ID.Text = dgvBOOKING.Rows[r].Cells[2].Value.ToString();
            this.txtCustomer_Amount.Text = dgvBOOKING.Rows[r].Cells[3].Value.ToString();
            this.dtpCheckIn.Text = dgvBOOKING.Rows[r].Cells[4].Value.ToString();
            this.dtpCheckOut.Text = dgvBOOKING.Rows[r].Cells[5].Value.ToString();
            this.txtTotal_Price.Text = dgvBOOKING.Rows[r].Cells[6].Value.ToString();
        }
        private void Booking_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.txtbook_ID.Enabled = false;
            // Activate Them variable
            Them = true;
            // Delete all contents of each box in panel
            int newBookingID = Convert.ToInt32(dgvBOOKING.Rows[dgvBOOKING.Rows.Count - 2].Cells[0].Value) + 1;

            this.txtbook_ID.Text = newBookingID.ToString();
            this.txtStaff_ID.ResetText();
            this.txtCustomer_ID.ResetText();
            this.txtCustomer_Amount.ResetText();
            this.txtTotal_Price.ResetText();
            this.txtTotal_Price.Enabled = false;
            // Allow manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel.Enabled = true;
            // Ban manipulation on buttons Add / Update / Delete / Back
            this.btnAdd.Enabled = false;
            this.btnFix.Enabled = false;
            this.btnDelete.Enabled = false;

            // Point to textfield txtbook_ID
            this.txtStaff_ID.Focus();
        }
        private void btnFix_Click(object sender, EventArgs e)
        {
            // Activate Fix variable
            Them = false;
            // Allow manipulation in panel
            this.panel.Enabled = true;
            this.txtTotal_Price.Enabled = false;
            dgvBOOKING_CellClick(null, null);
            // Allow manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel.Enabled = true;
            // Ban manipulation on buttons Add / Fix / Delete / Back
            this.btnAdd.Enabled = false;
            this.btnFix.Enabled = false;
            this.btnDelete.Enabled = false;

            // Point to textfield txtbook_ID
            this.txtbook_ID.Enabled = false;
            this.txtStaff_ID.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Execute command
                // Get the order of current record
                int r = dgvBOOKING.CurrentCell.RowIndex;
                string strB = dgvBOOKING.Rows[r].Cells[0].Value.ToString();
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
                    dbBooking.DeleteBooking(ref err, Convert.ToInt32(strB));
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
            this.txtStaff_ID.ResetText();
            this.txtCustomer_ID.ResetText();
            this.txtCustomer_Amount.ResetText();
            this.txtTotal_Price.ResetText();
            // Allow manipulation on buttons Add / Update / Delete / Back
            this.btnAdd.Enabled = true;
            this.btnFix.Enabled = true;
            this.btnDelete.Enabled = true;
            // Ban manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;
            this.panel.Enabled = false;
            dgvBOOKING_CellClick(null, null);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Open connection
            // Add data
            if (Them)
            {
                BLBooking blbk = new BLBooking();
                if (blbk.AddBooking( Convert.ToInt32(this.txtStaff_ID.Text), 
                                     Convert.ToInt32(this.txtCustomer_ID.Text), 
                                     Convert.ToInt32(this.txtCustomer_Amount.Text), 
                                     dtpCheckIn.Value, 
                                     dtpCheckOut.Value, ref err))
                    MessageBox.Show("Add successfully!");
                LoadData();
            }
            else
            {
                // Execute command
                BLBooking blbk = new BLBooking();
                blbk.UpdateBooking( Convert.ToInt32(this.txtbook_ID.Text), 
                                    Convert.ToInt32(this.txtStaff_ID.Text), 
                                    Convert.ToInt32(this.txtCustomer_ID.Text), 
                                    Convert.ToInt32(this.txtCustomer_Amount.Text), 
                                    dtpCheckIn.Value, 
                                    dtpCheckOut.Value, ref err);
                // Reload data to DataGridView
                LoadData();
                // Announce
                MessageBox.Show("Update successfully!");
            }
            // Close connection
        }
    }
}

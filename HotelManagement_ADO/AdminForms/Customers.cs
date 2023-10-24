using HotelManagement_ADO.BS_Layer;
using System;
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
    public partial class Customers : Form
    {
        bool Them;
        string err;
        BLCustomer dbCus = new BLCustomer();
        public Customers()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {
                // Transfer data to DataGridView
                DataSet dataSet = dbCus.TakeCustomers();
                DataTable dataTable = dataSet.Tables[0];
                // Set the DataSource of the DataGridView
                dgvCUSTOMERS.DataSource = dataTable;
                // Delete all contents of each box in panel
                this.txtCID.ResetText();
                this.txtFullName.ResetText();
                this.dtpBirthday.ResetText();
                this.cbGender.ResetText();
                this.txtPhoneNo.ResetText();
                this.txtAdd.ResetText();
                this.txtIdentifyNumber.ResetText();
                // Ban manipulation on buttons Save / Cancel
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;
                this.panel.Enabled = false;
                // Allow manipulation on buttons Add / Update / Delete / Back
                this.btnAdd.Enabled = true;
                this.btnUpdate.Enabled = true;
                this.btnDelete.Enabled = true;

                //
                dgvCUSTOMERS_CellClick(null, null);
            }
            catch
            {
                MessageBox.Show("Cannot access to table Customers. An error occurred!!!");
            }
        }
        private void dgvCUSTOMERS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Order of current record
            int r = dgvCUSTOMERS.CurrentCell.RowIndex;
            // Transfer data to panel
            this.txtCID.Text = dgvCUSTOMERS.Rows[r].Cells[0].Value.ToString();
            this.txtFullName.Text = dgvCUSTOMERS.Rows[r].Cells[1].Value.ToString();
            this.dtpBirthday.Text = dgvCUSTOMERS.Rows[r].Cells[2].Value.ToString();
            this.cbGender.Text = dgvCUSTOMERS.Rows[r].Cells[3].Value.ToString();
            this.txtPhoneNo.Text = dgvCUSTOMERS.Rows[r].Cells[4].Value.ToString();
            this.txtAdd.Text = dgvCUSTOMERS.Rows[r].Cells[5].Value.ToString();
            this.txtIdentifyNumber.Text = dgvCUSTOMERS.Rows[r].Cells[6].Value.ToString();
        }
        private void FormCustomers_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.txtCID.Enabled = false;
            // Activate Them variable
            Them = true;
            // Delete all contents of each box in panel
            int newcID = Convert.ToInt32(dgvCUSTOMERS.Rows[dgvCUSTOMERS.Rows.Count - 2].Cells[0].Value) + 1;

            this.txtCID.Text = newcID.ToString(); ;
            this.txtFullName.ResetText();
            this.dtpBirthday.ResetText();
            this.cbGender.ResetText();
            this.txtPhoneNo.ResetText();
            this.txtAdd.ResetText();
            this.txtIdentifyNumber.ResetText();
            // Allow manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel.Enabled = true;
            // Ban manipulation on buttons Add / Update / Delete / Back
            this.btnAdd.Enabled = false;
            this.btnUpdate.Enabled = false;
            this.btnDelete.Enabled = false;

            // Point to textfield txtCID
            this.txtCID.Focus();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Activate Fix variable
            Them = false;
            // Allow manipulation in panel
            this.panel.Enabled = true;
            dgvCUSTOMERS_CellClick(null, null);
            // Allow manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel.Enabled = true;
            // Ban manipulation on buttons Add / Fix / Delete / Back
            this.btnAdd.Enabled = false;
            this.btnUpdate.Enabled = false;
            this.btnDelete.Enabled = false;

            // Point to textfield txtCID
            this.txtCID.Enabled = false;
            this.txtIdentifyNumber.Focus();    
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Execute command
                // Get the order of current record
                int r = dgvCUSTOMERS.CurrentCell.RowIndex;
                string strC = dgvCUSTOMERS.Rows[r].Cells[0].Value.ToString();
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
                    dbCus.DeleteCustomers(ref err, Convert.ToInt32(strC));
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
            this.txtCID.ResetText();
            this.txtFullName.ResetText();
            this.dtpBirthday.ResetText();
            this.cbGender.ResetText();
            this.txtPhoneNo.ResetText();
            this.txtAdd.ResetText();
            this.txtIdentifyNumber.ResetText();
            // Allow manipulation on buttons Add / Update / Delete / Back
            this.btnAdd.Enabled = true;
            this.btnUpdate.Enabled = true;
            this.btnDelete.Enabled = true;
            // Ban manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;
            this.panel.Enabled = false;
            dgvCUSTOMERS_CellClick(null, null);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Open connection
            // Add data
            if (Them)
            {
                BLCustomer bcus = new BLCustomer();
                bool gen = false;
                if (cbGender.Text == "Female") gen = true;
                if (bcus.AddCustomers( this.txtFullName.Text,
                                       this.dtpBirthday.Value,
                                       gen,
                                       this.txtPhoneNo.Text,
                                       this.txtAdd.Text,
                                       this.txtIdentifyNumber.Text, ref err))
                    MessageBox.Show("Add successfully!");
                LoadData();
            }
            else
            {
                // Execute command
                BLCustomer bcus = new BLCustomer();
                bool gen = false;
                if (cbGender.Text == "Female") gen = true;
                bcus.UpdateCustomers( Convert.ToInt32(this.txtCID.Text),
                                      this.txtFullName.Text,
                                      DateTime.Parse(this.dtpBirthday.Text),
                                      gen,
                                      this.txtPhoneNo.Text,
                                      this.txtAdd.Text,
                                      this.txtIdentifyNumber.Text, ref err);
                // Reload data to DataGridView
                LoadData();
                // Announce
                MessageBox.Show("Update successfully!");
            }
            // Close connection
        }
    }
}

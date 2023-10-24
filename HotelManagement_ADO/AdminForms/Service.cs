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
    public partial class Service : Form
    {
        bool Them;
        string err;
        BLService dbSV = new BLService();
        public Service()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {
                // Transfer data to DataGridView
                DataSet dataSet = dbSV.TakeService();
                DataTable dataTable = dataSet.Tables[0];
                // Set the DataSource of the DataGridView
                dgvService.DataSource = dataTable;
                // Resize column
                dgvService.AutoResizeColumns();
                // Delete all contents of each box in panel
                this.txtSerID.ResetText();
                this.txtTitle.ResetText();
                this.txtUnitNote.ResetText();
                this.txtPrice.ResetText();
                this.txtAmount.ResetText();
                // Ban manipulation on buttons Save / Cancel
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;
                this.pnService.Enabled = false;
                // Allow manipulation on buttons Add / Update / Delete / Back
                this.btnAdd.Enabled = true;
                this.btnUpdate.Enabled = true;
                this.btnDelete.Enabled = true;

                //
                dgvService_CellClick(null, null);
            }
            catch
            {
                MessageBox.Show("Cannot access to table Service. An error occurred!!!");
            }
        }
        private void dgvService_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Order of current record
            int r = dgvService.CurrentCell.RowIndex;
            // Transfer data to panel
            this.txtSerID.Text = dgvService.Rows[r].Cells[0].Value.ToString();
            this.txtTitle.Text = dgvService.Rows[r].Cells[1].Value.ToString();
            this.txtPrice.Text = dgvService.Rows[r].Cells[2].Value.ToString();
            this.txtAmount.Text = dgvService.Rows[r].Cells[3].Value.ToString();
            this.txtUnitNote.Text = dgvService.Rows[r].Cells[4].Value.ToString();
        }
        private void FormService_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.txtSerID.Enabled = true;
            // Activate Them variable
            Them = true;
            // Delete all contents of each box in panel
            int newSerID = Convert.ToInt32(dgvService.Rows[dgvService.Rows.Count - 2].Cells[0].Value) + 1;

            this.txtSerID.Text = newSerID.ToString();
            this.txtTitle.ResetText();
            this.txtUnitNote.ResetText();
            this.txtPrice.ResetText();
            this.txtAmount.ResetText();
            // Allow manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.pnService.Enabled = true;
            // Ban manipulation on buttons Add / Update / Delete / Back
            this.btnAdd.Enabled = false;
            this.btnUpdate.Enabled = false;
            this.btnDelete.Enabled = false;

            // Point to textfield txtserID
            this.txtTitle.Focus();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Activate Fix variable
            Them = false;
            // Allow manipulation in panel
            this.pnService.Enabled = true;
            dgvService_CellClick(null, null);
            // Allow manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.pnService.Enabled = true;
            // Ban manipulation on buttons Add / Fix / Delete / Back
            this.btnAdd.Enabled = false;
            this.btnUpdate.Enabled = false;
            this.btnDelete.Enabled = false;

            // Point to textfield txtSerID
            this.txtSerID.Enabled = false;
            this.txtTitle.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Execute command
                // Get the order of current record
                int r = dgvService.CurrentCell.RowIndex;
                string strSV = dgvService.Rows[r].Cells[0].Value.ToString();
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
                    dbSV.DeleteService(ref err, Convert.ToInt32(strSV));
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
            this.txtSerID.ResetText();
            this.txtTitle.ResetText();
            this.txtUnitNote.ResetText();
            this.txtPrice.ResetText();
            this.txtAmount.ResetText();
            // Allow manipulation on buttons Add / Update / Delete / Back
            this.btnAdd.Enabled = true;
            this.btnUpdate.Enabled = true;
            this.btnDelete.Enabled = true;
            // Ban manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;
            this.pnService.Enabled = false;
            dgvService_CellClick(null, null);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Open connection
            // Add data
            if (Them)
            {
                BLService dbSV = new BLService();
                if (dbSV.AddService( txtTitle.Text,
                                     Convert.ToDouble(this.txtPrice.Text), 
                                     Convert.ToInt32(this.txtAmount.Text), 
                                     this.txtUnitNote.Text, ref err))
                    MessageBox.Show("Add successfully!");
                LoadData();

            }
            else
            {
                // Execute command
                BLService dbSV = new BLService();
                dbSV.UpdateService( Convert.ToInt32(this.txtSerID.Text), 
                                    txtTitle.Text, 
                                    Convert.ToDouble(this.txtPrice.Text), 
                                    Convert.ToInt32(this.txtAmount.Text), 
                                    this.txtUnitNote.Text, ref err);
                // Reload data to DataGridView
                LoadData();
                // Announce
                MessageBox.Show("Update successfully!");
            }
            // Close connection
        }
    }
}

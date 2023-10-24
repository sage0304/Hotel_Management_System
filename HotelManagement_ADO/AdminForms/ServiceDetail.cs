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
    public partial class ServiceDetail : Form
    {
        bool Them;
        string err;
        BLServiceDetail dbSE = new BLServiceDetail();
        public ServiceDetail()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {
                // Transfer data to DataGridView
                DataSet dataSet = dbSE.TakeServiceDetail();
                DataTable dataTable = dataSet.Tables[0];
                // Set the DataSource of the DataGridView
                dgvServiceDetail.DataSource = dataTable;
                // Resize column
                dgvServiceDetail.AutoResizeColumns();
                // Delete all contents of each box in panel
                this.txtSerID.ResetText();
                this.txtBookID.ResetText();
                this.txtNumUser.ResetText();
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
                dgvServiceDetail_CellClick(null, null);
            }
            catch
            {
                MessageBox.Show("Cannot access to table ServiceDetail. An error occurred!!!");
            }
        }
        private void dgvServiceDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Order of current record
            int r = dgvServiceDetail.CurrentCell.RowIndex;
            // Transfer data to panel
            this.txtSerID.Text = dgvServiceDetail.Rows[r].Cells[0].Value.ToString();
            this.txtBookID.Text = dgvServiceDetail.Rows[r].Cells[1].Value.ToString();
            this.txtNumUser.Text = dgvServiceDetail.Rows[r].Cells[2].Value.ToString();
            this.txtPrice.Text = dgvServiceDetail.Rows[r].Cells[3].Value.ToString();
            this.txtAmount.Text = dgvServiceDetail.Rows[r].Cells[4].Value.ToString();
            this.dtpPaydate.Text = dgvServiceDetail.Rows[r].Cells[5].Value.ToString();
        }
        private void FormServiceDetail_Load(object sender, EventArgs e)
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
            this.txtBookID.Enabled = true;
            // Activate Them variable
            Them = true;
            // Delete all contents of each box in panel
            this.txtSerID.ResetText();
            this.txtBookID.ResetText();
            this.txtNumUser.ResetText();
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

            // Point to textfield txtroomID
            this.txtSerID.Focus();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Activate Fix variable
            Them = false;
            // Allow manipulation in panel
            this.pnService.Enabled = true;
            dgvServiceDetail_CellClick(null, null);
            // Allow manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.pnService.Enabled = true;
            // Ban manipulation on buttons Add / Update / Delete / Back
            this.btnAdd.Enabled = false;
            this.btnUpdate.Enabled = false;
            this.btnDelete.Enabled = false;

            // Point to textfield txtSerID
            this.txtBookID.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Execute command
                // Get the order of current record
                int r = dgvServiceDetail.CurrentCell.RowIndex;
                string strSD1 = dgvServiceDetail.Rows[r].Cells[0].Value.ToString();
                string strSD2 = dgvServiceDetail.Rows[r].Cells[1].Value.ToString();
                string strSD3 = dgvServiceDetail.Rows[r].Cells[5].Value.ToString();
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
                    dbSE.DeleteServiceDetail(ref err, Convert.ToInt32(strSD1), Convert.ToInt32(strSD2), DateTime.Parse(strSD3));
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
            this.txtBookID.ResetText();
            this.txtNumUser.ResetText();
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
            dgvServiceDetail_CellClick(null, null);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Open connection
            // Add data
            if (Them)
            {
                BLServiceDetail dbSE = new BLServiceDetail();
                if (dbSE.AddServiceDetail( Convert.ToInt32(this.txtSerID.Text), 
                                           Convert.ToInt32(this.txtBookID.Text), 
                                           Convert.ToInt32(this.txtNumUser.Text), 
                                           Convert.ToInt32(this.txtAmount.Text), 
                                           dtpPaydate.Value, ref err))
                    MessageBox.Show("Add successfully!"); ;
                LoadData();
            }
            else
            {
                // Execute command
                BLServiceDetail dbSE = new BLServiceDetail(); ;
                dbSE.UpdateServiceDetail( Convert.ToInt32(this.txtSerID.Text), 
                                          Convert.ToInt32(this.txtBookID.Text), 
                                          Convert.ToInt32(this.txtNumUser.Text), 
                                          Convert.ToInt32(this.txtAmount.Text), 
                                          dtpPaydate.Value, ref err);
                // Reload data to DataGridView
                LoadData();
                // Announce
                MessageBox.Show("Update successfully!");
            }
            // Close connection
        }
    }
}

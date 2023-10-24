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
    public partial class IncludedItem : Form
    {
        bool Them;
        string err;
        BLIncludedItem dbIncludedItem = new BLIncludedItem();
        public IncludedItem()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {
                // Transfer data to DataGridView
                DataSet dataSet = dbIncludedItem.TakeIncludedItem();
                DataTable dataTable = dataSet.Tables[0];
                // Set the DataSource of the DataGridView
                dgvINCLUDEDITEM.DataSource = dataTable;
                // Delete all contents of each box in panel
                this.txtitemID.ResetText();
                this.txtitemName.ResetText();
                this.txtroomType.ResetText();
                this.txtiiPrice.ResetText();
                this.txtiiAmount.ResetText();
                // Ban manipulation on buttons Save / Cancel
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;
                this.panel.Enabled = false;
                // Allow manipulation on buttons Add / Update / Delete / Back
                this.btnAdd.Enabled = true;
                this.btnFix.Enabled = true;
                this.btnDelete.Enabled = true;

                //
                dgvINCLUDEDITEM_CellClick(null, null);
            }
            catch
            {
                MessageBox.Show("Cannot access to table RoomDetail. An error occurred!!!");
            }
        }
        private void dgvINCLUDEDITEM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Order of current record
            int r = dgvINCLUDEDITEM.CurrentCell.RowIndex;
            // Transfer data to panel
            this.txtitemID.Text = dgvINCLUDEDITEM.Rows[r].Cells[0].Value.ToString();
            this.txtitemName.Text = dgvINCLUDEDITEM.Rows[r].Cells[1].Value.ToString();
            this.txtroomType.Text = dgvINCLUDEDITEM.Rows[r].Cells[2].Value.ToString();
            this.txtiiPrice.Text = dgvINCLUDEDITEM.Rows[r].Cells[3].Value.ToString();
            this.txtiiAmount.Text = dgvINCLUDEDITEM.Rows[r].Cells[4].Value.ToString();
        }
        private void IncludedItem_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.txtitemID.Enabled = true;
            // Activate Them variable
            Them = true;
            // Delete all contents of each box in panel
            int newIncludedItem = Convert.ToInt32(dgvINCLUDEDITEM.Rows[dgvINCLUDEDITEM.Rows.Count - 2].Cells[0].Value) + 1;

            this.txtitemID.Text = newIncludedItem.ToString();
            this.txtitemName.ResetText();
            this.txtroomType.ResetText();
            this.txtiiPrice.ResetText();
            this.txtiiAmount.ResetText();
            // Allow manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel.Enabled = true;
            // Ban manipulation on buttons Add / Update / Delete / Back
            this.btnAdd.Enabled = false;
            this.btnFix.Enabled = false;
            this.btnDelete.Enabled = false;

            // Point to textfield txtitemID
            this.txtitemName.Focus();
        }
        private void btnFix_Click(object sender, EventArgs e)
        {
            // Activate Fix variable
            Them = false;
            // Allow manipulation in panel
            this.panel.Enabled = true;
            dgvINCLUDEDITEM_CellClick(null, null);
            // Allow manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel.Enabled = true;
            // Ban manipulation on buttons Add / Fix / Delete / Back
            this.btnAdd.Enabled = false;
            this.btnFix.Enabled = false;
            this.btnDelete.Enabled = false;

            // Point to textfield txtitemID
            this.txtitemID.Enabled = false;
            this.txtitemName.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Execute command
                // Get the order of current record
                int r = dgvINCLUDEDITEM.CurrentCell.RowIndex;
                string strII = dgvINCLUDEDITEM.Rows[r].Cells[0].Value.ToString();
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
                    dbIncludedItem.DeleteIncludedItem(ref err, Convert.ToInt32(strII));
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
            this.txtroomType.ResetText();
            this.txtiiPrice.ResetText();
            this.txtiiAmount.ResetText();
            // Allow manipulation on buttons Add / Fix / Delete / Back
            this.btnAdd.Enabled = true;
            this.btnFix.Enabled = true;
            this.btnDelete.Enabled = true;
            // Ban manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;
            this.panel.Enabled = false;
            dgvINCLUDEDITEM_CellClick(null, null);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Open connection
            // Add data
            if (Them)
            {
                BLIncludedItem ii = new BLIncludedItem();
                if (ii.AddIncludedItem( this.txtitemName.Text, 
                                        this.txtroomType.Text, 
                                        Convert.ToDouble(this.txtiiPrice.Text),
                                        Convert.ToInt32(this.txtiiAmount.Text), ref err))
                    MessageBox.Show("Add successfully");
                LoadData();
            }
            else
            {
                // Execute command
                BLIncludedItem ii = new BLIncludedItem();
                ii.UpdateIncludedItem( Convert.ToInt32(this.txtitemID.Text), 
                                       this.txtitemName.Text,
                                       this.txtroomType.Text,
                                       Convert.ToDouble(this.txtiiPrice.Text), 
                                       Convert.ToInt32(this.txtiiAmount.Text), ref err);
                // Reload data to DataGridView
                LoadData();
                // Announce
                MessageBox.Show("Update successfully!");
            }
            // Close connection
        }
    }
}

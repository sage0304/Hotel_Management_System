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
    public partial class Users : Form
    {
        bool Them;
        string err;
        BLUsers dbU = new BLUsers();
        bool bsearch = false;
        public Users()
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
                    DataSet dataSet = dbU.FindUser(Convert.ToInt32(textID.Text), textName.Text);
                    DataTable dataTable = dataSet.Tables[0];
                    // Set the DataSource of the DataGridView
                    dgvUSER.DataSource = dataTable;
                    bsearch = false;
                }
                else
                {
                    DataSet dataSet = dbU.TakeUser();
                    DataTable dataTable = dataSet.Tables[0];
                    // Set the DataSource of the DataGridView
                    dgvUSER.DataSource = dataTable;
                    // Resize column
                    dgvUSER.AutoResizeColumns();
                }
                // Delete all contents of each box in panel
                this.txtuserID.ResetText();
                this.txtFullname.ResetText();
                this.txtPassword.ResetText();
                this.dtpBirthday.ResetText();
                this.cbGender.ResetText();
                this.txtEmail.ResetText();
                this.txtPhone_Number.ResetText();
                this.txtAddress.ResetText();
                this.txtrole_id.ResetText();
                // Ban manipulation on buttons Save / Cancel
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;
                this.panel.Enabled = false;
                // Allow manipulation on buttons Add / Update / Delete / Back
                this.btnAdd.Enabled = true;
                this.btnFix.Enabled = true;
                this.btnDelete.Enabled = true;

                //
                dgvUSER_CellClick(null, null);
            }
            catch
            {
                MessageBox.Show("Cannot access to table Users. An error occurred!!!");
            }
        }
        private void dgvUSER_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Order of current record
            int r = dgvUSER.CurrentCell.RowIndex;
            // Transfer data to panel
            this.txtuserID.Text = dgvUSER.Rows[r].Cells[0].Value.ToString();
            this.txtFullname.Text = dgvUSER.Rows[r].Cells[1].Value.ToString();
            this.dtpBirthday.Text = dgvUSER.Rows[r].Cells[2].Value.ToString();
            this.cbGender.Text = dgvUSER.Rows[r].Cells[3].Value.ToString();
            this.txtEmail.Text = dgvUSER.Rows[r].Cells[4].Value.ToString();
            this.txtPhone_Number.Text = dgvUSER.Rows[r].Cells[5].Value.ToString();
            this.txtAddress.Text = dgvUSER.Rows[r].Cells[6].Value.ToString();
            this.txtrole_id.Text = dgvUSER.Rows[r].Cells[7].Value.ToString();
            this.txtPassword.Text = dgvUSER.Rows[r].Cells[8].Value.ToString();
        }
        private void FromUser_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnReLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.txtuserID.Enabled = true;
            // Activate Them variable
            Them = true;
            // Delete all contents of each box in panel
            int newUserId = Convert.ToInt32(dgvUSER.Rows[dgvUSER.Rows.Count - 2].Cells[0].Value) + 1;

            this.txtuserID.Text = newUserId.ToString();
            this.txtFullname.ResetText();
            this.txtPassword.ResetText();
            this.dtpBirthday.ResetText();
            this.cbGender.ResetText();
            this.txtEmail.ResetText();
            this.txtPhone_Number.ResetText();
            this.txtAddress.ResetText();
            this.txtrole_id.ResetText();
            // Allow manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel.Enabled = true;
            // Ban manipulation on buttons Add / Update / Delete / Back
            this.btnAdd.Enabled = false;
            this.btnFix.Enabled = false;
            this.btnDelete.Enabled = false;

            // Point to textfield txtuserID
            this.txtFullname.Focus();
        }
        private void btnFix_Click(object sender, EventArgs e)
        {
            // Activate Fix variable
            Them = false;
            // Allow manipulation in panel
            this.panel.Enabled = true;
            dgvUSER_CellClick(null, null);
            // Allow manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.panel.Enabled = true;
            // Ban manipulation on buttons Add / Fix / Delete / Back
            this.btnAdd.Enabled = false;
            this.btnFix.Enabled = false;
            this.btnDelete.Enabled = false;

            // Point to textfield txtFullname
            this.txtuserID.Enabled = false;
            this.txtFullname.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Execute command
                // Get the order of current record
                int r = dgvUSER.CurrentCell.RowIndex;
                string strU = dgvUSER.Rows[r].Cells[0].Value.ToString();
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
                    dbU.DeleteUser(ref err, Convert.ToInt32(strU));
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
            this.txtuserID.ResetText();
            this.txtFullname.ResetText();
            this.txtPassword.ResetText();
            this.dtpBirthday.ResetText();
            this.cbGender.ResetText();
            this.txtEmail.ResetText();
            this.txtPhone_Number.ResetText();
            this.txtAddress.ResetText();
            this.txtrole_id.ResetText();
            // Allow manipulation on buttons Add / Update / Delete / Back
            this.btnAdd.Enabled = true;
            this.btnFix.Enabled = true;
            this.btnDelete.Enabled = true;
            // Ban manipulation on buttons Save / Cancel / Panel
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;
            this.panel.Enabled = false;
            dgvUSER_CellClick(null, null);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Open connection
            // Add data
            if (Them)
            {
                BLUsers blU = new BLUsers();
                bool gen = false;
                if (cbGender.Text == "Female") gen = true;
                if (blU.AddUser( this.txtFullname.Text, 
                                 this.txtPassword.Text, 
                                 this.dtpBirthday.Value, 
                                 gen, 
                                 this.txtEmail.Text, 
                                 this.txtPhone_Number.Text, 
                                 this.txtAddress.Text, 
                                 Convert.ToInt32(this.txtrole_id.Text), ref err))
                    MessageBox.Show("Add successfully!");
                LoadData();
            }
            else
            {
                // Execute command
                BLUsers blU = new BLUsers();
                bool gen = false;
                if (cbGender.Text == "Female") gen = true;
                blU.UpdateUser( Convert.ToInt32(this.txtuserID.Text), 
                                this.txtFullname.Text, 
                                this.txtPassword.Text, 
                                this.dtpBirthday.Value, 
                                gen, this.txtEmail.Text, 
                                this.txtPhone_Number.Text, 
                                this.txtAddress.Text, 
                                Convert.ToInt32(this.txtrole_id.Text), ref err);
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

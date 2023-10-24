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
using HotelManagement_ADO.DB_Layer;

namespace HotelManagement_ADO.EmployeeForms
{
    public partial class EmployeeService : Form
    {
        string err;
        int rAvai = 0;
        int rBooked = 0;
        DBMain database = null;
        public EmployeeService()
        {
            InitializeComponent();
            database = new DBMain();
            this.btnAddService.Enabled = false;
        }
        void LoadDataAvai()
        {
            var view = database.ExecuteQueryDataSet("Select * from View_Service", CommandType.Text);
            DataTable dataTable = view.Tables[0];
            dgvAvaiServices.DataSource = dataTable;
            dgvAvaiServices.AutoGenerateColumns = true;
            dgvAvaiServices.ColumnHeadersHeight = 30;
            dgvAvaiServices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAvaiServices.Columns[1].HeaderText = "Service Name";
            dgvAvaiServices.Columns[4].HeaderText = "Unit for price";
            dgvAvaiServices_CellClick(null, null);
        }

        void LoadDataBooked()
        {
            var proc = database.ExecuteQueryDataSet($"EXEC SP_FindBookedServiceByName N'{txtName.Text}'", CommandType.Text);
            if (proc != null)
            {
                DataTable dataTable = proc.Tables[0];
                dgvBookedServices.DataSource = dataTable;
                dgvBookedServices.AutoGenerateColumns = true;
                dgvBookedServices.ColumnHeadersHeight = 30;
                dgvBookedServices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvBookedServices.Columns[0].HeaderText = "Book ID";
                dgvBookedServices.Columns[1].HeaderText = "Service ID";
                dgvBookedServices.Columns[2].HeaderText = "Name";
                dgvBookedServices_CellClick(null, null);
            }
            else dgvBookedServices.DataSource = null;
        }
        private void btnAddService_Click(object sender, EventArgs e)
        {
            try
            {
                BLServiceDetail dbSE = new BLServiceDetail();
                dbSE.AddServiceDetail( Convert.ToInt32(dgvAvaiServices.Rows[rAvai].Cells[0].Value),
                                       Convert.ToInt32(txtBookID.Text),
                                       Convert.ToInt32(dgvBookedServices.Rows[rBooked].Cells[0].Value),
                                       Convert.ToInt32(txtAmount.Text), 
                                       DateTime.Now,ref err);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
            }
            LoadDataBooked();
        }

        private void dgvAvaiServices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rAvai = dgvAvaiServices.CurrentCell.RowIndex;
            this.txtNameService.Text = dgvAvaiServices.Rows[rAvai].Cells[1].Value.ToString();
        }

        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            try
            {
                BLServiceDetail dbSE = new BLServiceDetail();
               dbSE.DeleteServiceDetail(ref err,
                   Convert.ToInt32(dgvBookedServices.Rows[rBooked].Cells[1].Value.ToString()),
                   Convert.ToInt32(dgvBookedServices.Rows[rBooked].Cells[0].Value.ToString()),
                   Convert.ToDateTime(dgvBookedServices.Rows[rBooked].Cells[6].Value.ToString()));
                LoadDataBooked();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.InnerException.Message);
            }
        }

        private void dgvBookedServices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBookedServices.CurrentCell != null)
            {
                rBooked = dgvBookedServices.CurrentCell.RowIndex;
                this.txtBookID.Text = dgvBookedServices.Rows[rBooked].Cells[0].Value.ToString();
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            txtBookID.ResetText();
            LoadDataBooked();
            if (txtName.Text != null && txtNameService != null && txtAmount != null && txtBookID != null)
            {
                this.btnAddService.Enabled = true;
            }
            else this.btnAddService.Enabled = false;
        }

        private void EmployeeService_Load(object sender, EventArgs e)
        {
            LoadDataAvai();
        }

        private void txtNameService_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text != null && txtNameService != null && txtAmount != null && txtBookID != null)
            {
                this.btnAddService.Enabled = true;
            }
            else this.btnAddService.Enabled = false;
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text != null && txtNameService != null && txtAmount != null && txtBookID != null)
            {
                this.btnAddService.Enabled = true;
            }
            else this.btnAddService.Enabled = false;
        }

        private void txtBookID_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text != null && txtNameService != null && txtAmount != null && txtBookID != null)
            {
                this.btnAddService.Enabled = true;
            }
            else this.btnAddService.Enabled = false;
        }
    }
}
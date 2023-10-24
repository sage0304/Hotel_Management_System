using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using HotelManagement_ADO.DB_Layer;

namespace HotelManagement_ADO.EmployeeForms
{
    public partial class CheckOut : Form
    {
        int rAvai;
        int currentCustomerID;
        int currentBookingID;

        DBMain database = null;
        Receipt formReceipt = null;
        public CheckOut()
        {
            InitializeComponent();
            database = new DBMain();
            LoadCustomer();
        }

        void LoadCustomer()
        {
        
            DataTable dataTable = null;
            if (string.IsNullOrEmpty(this.txtFindName.Text))
            {
                var view = database.ExecuteQueryDataSet("SELECT * FROM View_CustomerCheckOut", CommandType.Text);
                dataTable = view.Tables[0];
            }
            else
            {
                var query = $"SELECT * FROM FN_GetCustomerByFullName(N'{this.txtFindName.Text}')";  
                var view = database.ExecuteQueryDataSet(query, CommandType.Text);
                dataTable = view.Tables[0];
            }
            dgvCustomer.DataSource = dataTable;
            dgvCustomer.AutoGenerateColumns = true;
            dgvCustomer.ColumnHeadersHeight = 30;
            dgvCustomer.Columns[3].HeaderText = "Customer Name";
            dgvCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void checkOutBtn_Click(object sender, EventArgs e)
        {
           if(currentCustomerID != 0)
           {
                var checkOutDataSet = database.ExecuteQueryDataSet($"SELECT * FROM FN_GetCustomerReceipt('{currentBookingID}', '{currentCustomerID}')", CommandType.Text);
                formReceipt = new Receipt();
                formReceipt.currentCustomerID = currentCustomerID;
                formReceipt.dataSet = checkOutDataSet;
                formReceipt.LoadReceipt();

                formReceipt.TopLevel = false;
                formReceipt.FormBorderStyle = FormBorderStyle.None;
                formReceipt.Dock = DockStyle.Fill;
                receptPanel.Controls.Add(formReceipt);
                receptPanel.Tag = formReceipt;
                receptPanel.BringToFront();
                formReceipt.BringToFront();
                closeBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
                closeBtn.BringToFront();
                formReceipt.Show();
           }  
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            formReceipt.Close();
            closeBtn.SendToBack();
            closeBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
            receptPanel.SendToBack();
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rAvai = dgvCustomer.CurrentCell.RowIndex;
            this.txtCName.Text = dgvCustomer.Rows[rAvai].Cells[3].Value.ToString();
            this.txtRoom.Text = dgvCustomer.Rows[rAvai].Cells[1].Value.ToString();
            currentBookingID = Convert.ToInt32(dgvCustomer.Rows[rAvai].Cells[0].Value);
            currentCustomerID = Convert.ToInt32(dgvCustomer.Rows[rAvai].Cells[2].Value);
            DateTime checkIn = DateTime.Parse(dgvCustomer.Rows[rAvai].Cells[6].Value.ToString());
            DateTime checkOut = DateTime.Parse(dgvCustomer.Rows[rAvai].Cells[7].Value.ToString());
            TimeSpan duration = checkOut - checkIn;
            this.txtStayingDays.Text = duration.Days.ToString() + " Days";
        }

        private void findBtn_Click(object sender, EventArgs e)
        {
            LoadCustomer();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using HotelManagement_ADO.AdminForms;
using HotelManagement_ADO.EmployeeForms;

namespace HotelManagement_ADO.Interface
{
    public partial class InteractionInterface : Form
    {
        public string StoredUsername { get; set; }
        public int Role { get; set; }
        public string FullName { get; set; }
        public int currentUserID { get; set; }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private Form currentChildForm;

        public InteractionInterface()
        {
            InitializeComponent();
            AllocConsole();          
        }

        public InteractionInterface(int userID)
        {
            InitializeComponent();
            AllocConsole();
            currentUserID = userID;
        }

        private void openChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(childForm);
            mainPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void userBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new Users());
        }

        private void roomDetailBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new RoomDetail());
        }

        private void bookingBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new Booking());
        }

        private void roomBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new Room());
        }

        private void serviceBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new Service());
        }

        private void serviceDetailBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new ServiceDetail());
        }

        private void includedItemBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new IncludedItem());
        }

        private void damagedItemBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new DamagedItem());
        }

        private void customersBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new Customers());
        }

        private void roomBookingBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new EmployeeBooking(currentUserID));
        }

        private void servicesBookingBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new EmployeeService());
        }

        private void checkOutBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new CheckOut());
        }

        public void SetUserDetails(string storedUsername, int role, string fullName, int UserID)
        {
            lbUserID.Text = storedUsername;
            if(role == 1)
            {
                lbRole.Text = "Admin";
            }
            else if(role == 2)
            {
                lbRole.Text = "Employee";
            }
            lbName.Text = fullName;
        }

        private void bntMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bntMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

using SoftDev.Forms.AdminForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftDev.Forms
{
    public partial class Main : Form
    {
        public static string idUser, login;

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Autorization().Show();
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            new Employees().Show();
            this.Close();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            new Users().Show();
            this.Close();
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {

        }

        public Main()
        {
            InitializeComponent();
        }
    }
}

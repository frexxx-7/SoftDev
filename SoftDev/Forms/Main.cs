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

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Autorization().Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        public Main()
        {
            InitializeComponent();
        }
    }
}

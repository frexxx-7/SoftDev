using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftDev.Forms.AdminForms
{
    public partial class References : Form
    {
        public References()
        {
            InitializeComponent();
        }

        private void RequestsButton_Click(object sender, EventArgs e)
        {
            new Address().Show();
            this.Hide();
        }
    }
}

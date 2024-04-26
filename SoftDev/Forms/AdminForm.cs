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
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Requests().Show();
        }

        private void ClientsButton_Click(object sender, EventArgs e)
        {
            new Clients().Show();
        }

        private void ReferenceButton_Click(object sender, EventArgs e)
        {
            new ReferencesForm().Show();
            this.Hide();
        }

        private void DevelopersButton_Click(object sender, EventArgs e)
        {
            new Developers().Show();
            this.Hide();
        }

        private void ProjectsButton_Click(object sender, EventArgs e)
        {
            new Projects().Show();
            this.Hide();
        }

        private void TaskButton_Click(object sender, EventArgs e)
        {
            new Tasks().Show();
            this.Hide();
        }
    }
}

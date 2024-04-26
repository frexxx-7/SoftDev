using SoftDev.Forms.AdminForms.References;
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
    public partial class ReferencesForm : Form
    {
        public ReferencesForm()
        {
            InitializeComponent();
        }

        private void RequestsButton_Click(object sender, EventArgs e)
        {
            new Address().Show();
            this.Hide();
        }

        private void TechnologiesButton_Click(object sender, EventArgs e)
        {
            new Technologies().Show();
            this.Hide();
        }
    }
}

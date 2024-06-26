using SoftDev.Forms.AdminForms;
using SoftDev.Forms.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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


        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Employees().Show();
            this.Close();
        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Users().Show();
            this.Close();
        }

        private void отделыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Locality().Show();
            this.Close();
        }

        private void организацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Organizations().Show();
            this.Close();
        }

        private void программноеОбеспечениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SoftWare().Show();
            this.Close();
        }

        private void создатьЗаявкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Request().Show();
            this.Close();
        }

        private void заявкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Request().Show();
            this.Close();
        }

        private void информацияОРазработкеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Development().Show();
            this.Close();
        }

        private void актыПриемапередачиПОToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TransferAcceptanceCertificate().Show();
            this.Close();
        }

        private void договораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Agreement().Show();
            this.Close();
        }

        private void переченьПОToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ScrollSoftWare().Show();
            this.Close();
        }

        private void разработчикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Developers().Show();
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sourceFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Poyasnitelnaya_zapiska.docx");

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "Руководство пользователя.docx",
                Filter = "Word Document (*.docx)|*.docx",
                Title = "Сохранить как"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string destinationFilePath = saveFileDialog.FileName;

                try
                {
                    File.Copy(sourceFilePath, destinationFilePath, true);
                    MessageBox.Show("Файл успешно сохранен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Process.Start(new ProcessStartInfo(destinationFilePath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при копировании файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public Main()
        {
            InitializeComponent();
        }
    }
}

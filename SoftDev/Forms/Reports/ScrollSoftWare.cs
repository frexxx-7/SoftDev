using MySql.Data.MySqlClient;
using SoftDev.Classes;
using SoftDev.Forms.AdminForms;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace SoftDev.Forms.Reports
{
    public partial class ScrollSoftWare : Form
    {
        public ScrollSoftWare()
        {
            InitializeComponent();
        }
        private void loadInfoSoftWare()
        {
            DB db = new DB();

            SoftWareDataGrid.Rows.Clear();

            string query = $"select * from software ";

            db.openConnection();
            using (MySqlCommand mySqlCommand = new MySqlCommand(query, db.getConnection()))
            {
                MySqlDataReader reader = mySqlCommand.ExecuteReader();

                List<string[]> dataDB = new List<string[]>();
                while (reader.Read())
                {

                    dataDB.Add(new string[reader.FieldCount]);

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dataDB[dataDB.Count - 1][i] = reader[i].ToString();
                    }
                }
                reader.Close();
                foreach (string[] s in dataDB)
                    SoftWareDataGrid.Rows.Add(s);
            }
            db.closeConnection();
        }
        private void ScrollSoftWare_Load(object sender, EventArgs e)
        {
            loadInfoSoftWare();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            for (int j = 0; j < SoftWareDataGrid.Columns.Count; j++)
            {
                if (SoftWareDataGrid.Columns[j].Visible)
                {
                    worksheet.Cells[1, j] = SoftWareDataGrid.Columns[j].HeaderText;
                }
            }
            for (int i = 0; i < SoftWareDataGrid.Rows.Count; i++)
            {
                for (int j = 0; j < SoftWareDataGrid.Columns.Count; j++)
                {
                    if (SoftWareDataGrid.Columns[j].Visible)
                    {
                        worksheet.Cells[i + 2, j] = SoftWareDataGrid.Rows[i].Cells[j].Value;
                    }
                }
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel File|*.xlsx";
            saveFileDialog1.Title = "Сохранить Excel файл";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                workbook.SaveAs(saveFileDialog1.FileName);
            }
            workbook.Close();
            excelApp.Quit();
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

        private void организацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Organizations().Show();
            this.Close();
        }

        private void отделыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Locality().Show();
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

        private void информацияОРазработкеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Development().Show();
            this.Close();
        }

        private void заявкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Request().Show();
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
    }
}

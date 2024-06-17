using Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;
using SoftDev.Classes;
using SoftDev.Forms.AdminForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftDev.Forms.Reports
{
    public partial class Agreement : Form
    {
        private int countMonth;
        private string nameOrg;
        private string nameSoftWare;
        private string deadline;
        private string FIODirector;

        public Agreement()
        {
            InitializeComponent();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
           System.Windows.Forms.Application.Exit();
        }
        private void loadInfoRequestsComboBox()
        {
            DevelopmentComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT development.id, requests.name FROM development " +
                $"inner join requests on development.idRequest = requests.id";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                DevelopmentComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }
        private void ReplaceBookmarkText(Document doc, string bookmarkName, string text)
        {
            Bookmark bookmark = doc.Bookmarks[bookmarkName];
            if (bookmark != null)
            {
                Range range = bookmark.Range;
                range.Text = text;
            }
        }
        private void loadInfoVariable()
        {
            DB db = new DB();
            string queryInfo = $"select development.dateStart, development.dateEnd, organizations.name, software.name, organizations.fiodirector from development " +
                $"inner join requests on development.idRequest = requests.id " +
                $"inner join organizations on requests.idOrganizations = organizations.id " +
                $"inner join software on requests.idSoftware = software.id " +
            $"where development.id = {(DevelopmentComboBox.SelectedItem as ComboBoxItem).Value}";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                DateTime dateStart = Convert.ToDateTime(reader[0].ToString());
                DateTime dateEnd = Convert.ToDateTime(reader[1].ToString());
                countMonth = ((dateEnd.Year - dateStart.Year) * 12) + dateEnd.Month - dateStart.Month;

                if (dateEnd.Day < dateStart.Day)
                {
                    countMonth--;
                }
                nameOrg = reader[2].ToString();
                nameSoftWare = reader[3].ToString();
                deadline = countMonth + " месяцев";
                FIODirector = reader[4].ToString();
            }
            reader.Close();

            db.closeConnection();
        }
        private void guna2Button13_Click(object sender, EventArgs e)
        {
            loadInfoVariable();
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();

            Document sourceDoc = wordApp.Documents.Open(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Шаблон договора.docx"));
            sourceDoc.Content.Copy();

            Document targetDoc = wordApp.Documents.Add();
            targetDoc.Content.Paste();

            ReplaceBookmarkText(targetDoc, "НомерДоговора", NumberAgreementTextBox.Text);
            ReplaceBookmarkText(targetDoc, "ДатаДоговора", DateAgreementDateTimePicker.Value.ToString("dd.MM.yyyy"));
            ReplaceBookmarkText(targetDoc, "КоличествоМесяцев", countMonth.ToString());
            ReplaceBookmarkText(targetDoc, "НазваниеОрганизации", nameOrg);
            ReplaceBookmarkText(targetDoc, "НазваниеПО", nameSoftWare);
            ReplaceBookmarkText(targetDoc, "СрокИсполнения", deadline);
            ReplaceBookmarkText(targetDoc, "ТипыУстройств", DeviceTypesTextBox.Text);
            ReplaceBookmarkText(targetDoc, "ФИОДиректора", FIODirector);

            sourceDoc.Close();

            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "Документ Word (*.docx)|*.docx",
                Title = "Сохранить скопированный документ в"
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string targetPath = saveFileDialog1.FileName;
                targetDoc.SaveAs2(targetPath);
                targetDoc.Close();

                Document wordDocument = wordApp.Documents.Open(targetPath);
                wordApp.Visible = true;
            }
            else
            {
                targetDoc.Close(false);
                wordApp.Quit();
            }
        }

        private void Agreement_Load(object sender, EventArgs e)
        {
            loadInfoRequestsComboBox();
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
            new Employees().Show();
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

        private void переченьПОToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ScrollSoftWare().Show();
            this.Close();
        }
    }
}

using MySql.Data.MySqlClient;
using SoftDev.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.IO;

namespace SoftDev.Forms.Reports
{
    public partial class TransferAcceptanceCertificate : Form
    {
        public TransferAcceptanceCertificate()
        {
            InitializeComponent();
        }
        private void loadInfoChairmanComboBox()
        {
            ChairmanComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT positions.name, employees.name FROM employees " +
                $"inner join positions on employees.idPosition = positions.id";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                ChairmanComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoDeveloperComboBox()
        {
            DeveloperComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT id, name FROM employees";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                DeveloperComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoDirectorComboBox()
        {
            DirectorComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT id, name FROM employees";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                DirectorComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoSoftWareComboBox()
        {
            SoftWareComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT version, name FROM software";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                SoftWareComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoOrganizationComboBox()
        {
            OrganizationComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT id, name FROM organizations";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                OrganizationComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }
        private void TransferAcceptanceCertificate_Load(object sender, EventArgs e)
        {
            loadInfoChairmanComboBox();
            loadInfoDeveloperComboBox();
            loadInfoDirectorComboBox();
            loadInfoSoftWareComboBox();
            loadInfoOrganizationComboBox();
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
        private void guna2Button13_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();

            Document sourceDoc = wordApp.Documents.Open(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Шаблон акт приема-передачи.docx"));
            sourceDoc.Content.Copy();

            Document targetDoc = wordApp.Documents.Add();
            targetDoc.Content.Paste();

            // Replace bookmarks with the desired text
            ReplaceBookmarkText(targetDoc, "НомерАкта", ActNumberTextBox.Text);
            ReplaceBookmarkText(targetDoc, "ДатаАкта", ActDateDateTimePicker.Value.ToString("dd.MM.yyyy"));
            ReplaceBookmarkText(targetDoc, "НомерНаправления", NumberDirectionTextBox.Text);
            ReplaceBookmarkText(targetDoc, "ДатаНаправления", DateDirectionDateTimePicker.Value.ToString("dd.MM.yyyy"));
            ReplaceBookmarkText(targetDoc, "ПредседательДолжность", (ChairmanComboBox.SelectedItem as ComboBoxItem).Value.ToString());
            ReplaceBookmarkText(targetDoc, "ПредседательФИО", (ChairmanComboBox.SelectedItem as ComboBoxItem).Text);
            ReplaceBookmarkText(targetDoc, "РазработчикФИО", (DeveloperComboBox.SelectedItem as ComboBoxItem).Text);
            ReplaceBookmarkText(targetDoc, "ДиректорФИО", (DirectorComboBox.SelectedItem as ComboBoxItem).Text);
            ReplaceBookmarkText(targetDoc, "НазваниеПО", (SoftWareComboBox.SelectedItem as ComboBoxItem).Value.ToString());
            ReplaceBookmarkText(targetDoc, "ВерсияПО", (SoftWareComboBox.SelectedItem as ComboBoxItem).Text);
            ReplaceBookmarkText(targetDoc, "НазваниеОрганизации", (OrganizationComboBox.SelectedItem as ComboBoxItem).Text);

            // Close the source document
            sourceDoc.Close();

            // Show the save file dialog
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "Документ Word (*.docx)|*.docx",
                Title = "Сохранить скопированный документ в"
            };

            // Save the document if the user clicks OK
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string targetPath = saveFileDialog1.FileName;
                targetDoc.SaveAs2(targetPath);
                targetDoc.Close();

                // Open the saved document in the same Word application instance and make it visible
                Document wordDocument = wordApp.Documents.Open(targetPath);
                wordApp.Visible = true;
            }
            else
            {
                // Close the target document without saving if the user cancels the save dialog
                targetDoc.Close(false);
                wordApp.Quit();
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}

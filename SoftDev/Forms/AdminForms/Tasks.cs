using MySql.Data.MySqlClient;
using SoftDev.Classes;
using SoftDev.Forms.AddForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace SoftDev.Forms.AdminForms
{
    public partial class Tasks : Form
    {
        public delegate void LoadInfoTasks();
        private LoadInfoTasks lit;
        public Tasks()
        {
            InitializeComponent();
            lit = loadInfoTasks;
        }
        private void loadInfoTasks()
        {
            DB db = new DB();

            TaskDataGridView.Rows.Clear();

            string query = $"select tasks.id, project.name, tasks.description, tasks.deadline, tasks.state from tasks " +
                $"join project on project.id = tasks.idProject ";

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
                    TaskDataGridView.Rows.Add(s);
            }
            db.closeConnection();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            new AddTask(null, lit).Show();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            new AddTask(TaskDataGridView[0, TaskDataGridView.SelectedCells[0].RowIndex].Value.ToString(), lit).Show();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"delete from tasks where id = {TaskDataGridView[0, TaskDataGridView.SelectedCells[0].RowIndex].Value}", db.getConnection());
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Задача удалена");

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
            loadInfoTasks();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            TaskDataGridView.Rows.Clear();

            string searchString = $"select tasks.id, project.name, tasks.description, tasks.deadline, tasks.state from tasks  " +
                $"join project on project.id = tasks.idProject " +
                $"where concat (tasks.id, project.name, tasks.description, tasks.deadline, tasks.state) like '%" + SearchTextBox.Text + "%'";

            db.openConnection();
            using (MySqlCommand mySqlCommand = new MySqlCommand(searchString, db.getConnection()))
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
                    TaskDataGridView.Rows.Add(s);
            }
            db.closeConnection();
        }

        private void Tasks_Load(object sender, EventArgs e)
        {
            loadInfoTasks();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void OutputButton_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            for (int j = 0; j < TaskDataGridView.Columns.Count; j++)
            {
                if (TaskDataGridView.Columns[j].Visible)
                {
                    worksheet.Cells[1, j] = TaskDataGridView.Columns[j].HeaderText;
                }
            }
            for (int i = 0; i < TaskDataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < TaskDataGridView.Columns.Count; j++)
                {
                    if (TaskDataGridView.Columns[j].Visible)
                    {
                        worksheet.Cells[i + 2, j] = TaskDataGridView.Rows[i].Cells[j].Value;
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
    }
}

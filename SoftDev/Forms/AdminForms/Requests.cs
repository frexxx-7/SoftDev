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

namespace SoftDev.Forms
{
    public partial class Requests : Form
    {
        public delegate void LoadInfoRequests();
        private LoadInfoRequests lir;
        public Requests()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            new AddRequest(null, lir).Show();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            new AddRequest(RequestDataGridView[0, RequestDataGridView.SelectedCells[0].RowIndex].Value.ToString(), lir).Show();
        }

        private void loadInfoRequestsFromDB()
        {
            DB db = new DB();

            RequestDataGridView.Rows.Clear();

            string query = $"select requests.id, concat(client.surname, ' ', client.name, ' ', client.patronymic) as FIOClient, project.name, requests.dateCreate, requests.state from requests " +
                $"join client on client.id = requests.idClient "+
                $"join project on project.id = requests.idProject ";

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
                    RequestDataGridView.Rows.Add(s);
            }
            db.closeConnection();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"delete from requests where id = {RequestDataGridView[0, RequestDataGridView.SelectedCells[0].RowIndex].Value}", db.getConnection());
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Заявка удалена");

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
            loadInfoRequestsFromDB();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            RequestDataGridView.Rows.Clear();

            string searchString = $"select requests.id, concat(client.surname, ' ', client.name, ' ', client.patronymic) as FIOClient, project.name, requests.dateCreate, requests.state from requests " +
                $"join client on client.id = requests.idClient " +
                $"join project on project.id = requests.idProject " +
                $"where concat (requests.id, concat(client.surname, ' ', client.name, ' ', client.patronymic), project.name, requests.dateCreate, requests.state) like '%" + SearchTextBox.Text + "%'";

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
                    RequestDataGridView.Rows.Add(s);
            }
            db.closeConnection();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Requests_Load(object sender, EventArgs e)
        {
            lir = loadInfoRequestsFromDB;
            loadInfoRequestsFromDB();
        }
    }
}

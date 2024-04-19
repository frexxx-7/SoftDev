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

namespace SoftDev.Forms.AdminForms
{
    public partial class Clients : Form
    {
        public delegate void LoadInfoClient();
        private LoadInfoClient lic;
        public Clients()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            new AddClient(null, lic).Show();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            new AddClient(ClientDataGridView[0, ClientDataGridView.SelectedCells[0].RowIndex].Value.ToString(), lic).Show();
        }

        private void loadInfoClientsFromDB()
        {
            DB db = new DB();

            ClientDataGridView.Rows.Clear();

            string query = $"select client.id, client.surname, client.name, client.patronymic, client.numberPhone, client.passport, concat(address.country, ' ', address.city, ' ', address.street, ' ', address.house) as AddressInfo from client " +
                $"join address on address.id = client.idAddress ";

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
                    ClientDataGridView.Rows.Add(s);
            }
            db.closeConnection();
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"delete from client where id = {ClientDataGridView[0, ClientDataGridView.SelectedCells[0].RowIndex].Value}", db.getConnection());
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Клиент удален");

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
            loadInfoClientsFromDB();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            ClientDataGridView.Rows.Clear();

            string searchString = $"select client.id, client.surname, client.name, client.patronymic, client.numberPhone, client.passport, concat(address.country, ' ', address.city, ' ', address.street, ' ', address.house) as AddressInfo from client " +
                $"join address on address.id = client.idAddress " +
                $"where concat (client.surname, client.name, client.patronymic, client.numberPhone, client.passport, concat(address.country, ' ', address.city, ' ', address.street, ' ', address.house)) like '%" + SearchTextBox.Text + "%'";

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
                    ClientDataGridView.Rows.Add(s);
            }
            db.closeConnection();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            loadInfoClientsFromDB();
            lic = loadInfoClientsFromDB;
        }
    }
}

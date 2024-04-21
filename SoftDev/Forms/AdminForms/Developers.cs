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
    public partial class Developers : Form
    {
        public delegate void LoadInfoDevelopers();
        private LoadInfoDevelopers lid;
        public Developers()
        {
            InitializeComponent();
        }

        private void Developers_Load(object sender, EventArgs e)
        {
            lid = loadInfoDevelopers;
            loadInfoDevelopers();
        }

        private void loadInfoDevelopers()
        {
            DB db = new DB();

            DevelopersDataGridView.Rows.Clear();

            string query = $"select developers.id, developers.surname, developers.name, developers.patronymic,  concat(address.country, ' ', address.city, ' ', address.street, ' ', address.house) as AddressInfo, developers.skills from developers " +
                $"join address on address.id = developers.idAddress ";

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
                    DevelopersDataGridView.Rows.Add(s);
            }
            db.closeConnection();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            new AddDeveloper(null, lid).Show();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            new AddDeveloper(DevelopersDataGridView[0, DevelopersDataGridView.SelectedCells[0].RowIndex].Value.ToString(), lid).Show();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"delete from developers where id = {DevelopersDataGridView[0, DevelopersDataGridView.SelectedCells[0].RowIndex].Value}", db.getConnection());
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Разработчик удален");

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
            loadInfoDevelopers();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            DevelopersDataGridView.Rows.Clear();

            string searchString = $"select developers.id, developers.surname, developers.name, developers.patronymic,  concat(address.country, ' ', address.city, ' ', address.street, ' ', address.house) as AddressInfo, developers.skills from developers " +
                $"join address on address.id = developers.idAddress " +
                $"where concat (developers.surname, developers.name, developers.patronymic, concat(address.country, ' ', address.city, ' ', address.street, ' ', address.house), developers.skills) like '%" + SearchTextBox.Text + "%'";

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
                    DevelopersDataGridView.Rows.Add(s);
            }
            db.closeConnection();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

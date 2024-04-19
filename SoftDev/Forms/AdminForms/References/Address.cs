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
    public partial class Address : Form
    {
        public delegate void LoadInfoAddress();
        private LoadInfoAddress lia;
        public Address()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void loadInfoAddress()
        {
            DB db = new DB();

            AddressDataGridView.Rows.Clear();

            string query = $"select * from address";

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
                    AddressDataGridView.Rows.Add(s);
            }
            db.closeConnection();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"delete from address where id = {AddressDataGridView[0, AddressDataGridView.SelectedCells[0].RowIndex].Value}", db.getConnection());
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Адрес удален");

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
            loadInfoAddress();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            new AddAddress(null, lia).Show();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            new AddAddress(AddressDataGridView[0, AddressDataGridView.SelectedCells[0].RowIndex].Value.ToString(), lia).Show();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            AddressDataGridView.Rows.Clear();

            string searchString = $"select * from address " +
                $"where concat (country, city, street, house) like '%" + SearchTextBox.Text + "%'";

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
                    AddressDataGridView.Rows.Add(s);
            }
            db.closeConnection();
        }

        private void Address_Load(object sender, EventArgs e)
        {
            loadInfoAddress();
            lia = loadInfoAddress;
        }
    }
}

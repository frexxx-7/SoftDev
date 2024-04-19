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

namespace SoftDev.Forms.AddForms
{
    public partial class AddAddress : Form
    {
        private string idAddress;
        private Address.LoadInfoAddress lia;
        public AddAddress(string idAddress, Address.LoadInfoAddress lia)
        {
            InitializeComponent();
            this.idAddress = idAddress;

            if (idAddress != null)
            {
                label1.Text = "Изменить адрес";
                AddButton.Text = "Изменить";
                loadInfoClient();
            }

            this.lia = lia;
        }

        private void loadInfoClient()
        {
            DB db = new DB();
            string queryInfo = $"SELECT * FROM address WHERE id = '{idAddress}'";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                CountryTextBox.Text = reader["country"].ToString();
                CityTextBox.Text = reader["city"].ToString();
                StreetTextBox.Text = reader["street"].ToString();
                HouseTextBox.Text = reader["house"].ToString();
            }
            reader.Close();

            db.closeConnection();
        }

        private void CanceledButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            if (idAddress == null)
            {
                MySqlCommand command = new MySqlCommand($"INSERT into address (country, city, street, house) " +
                    $"values(@country, @city, @street, @house)", db.getConnection());
                command.Parameters.AddWithValue("@country", CountryTextBox.Text);
                command.Parameters.AddWithValue("@city", CityTextBox.Text);
                command.Parameters.AddWithValue("@street", StreetTextBox.Text);
                command.Parameters.AddWithValue("@house", HouseTextBox.Text);
                db.openConnection();

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Адрес добавлен");
                    lia();
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                db.closeConnection();
            }
            else
            {
                MySqlCommand command = new MySqlCommand($"Update address set country = @country, city = @city, street = @street, house = @house where id = {idAddress}", db.getConnection());
                command.Parameters.AddWithValue("@country", CountryTextBox.Text);
                command.Parameters.AddWithValue("@city", CityTextBox.Text);
                command.Parameters.AddWithValue("@street", StreetTextBox.Text);
                command.Parameters.AddWithValue("@house", HouseTextBox.Text);
                db.openConnection();

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Адрес изменен");
                    lia();
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                db.closeConnection();
            }
        }
    }
}

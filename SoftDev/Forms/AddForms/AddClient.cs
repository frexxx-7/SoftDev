using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using SoftDev.Classes;
using SoftDev.Forms.AdminForms;
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
    public partial class AddClient : Form
    {
        private string idClient;
        private Clients.LoadInfoClient lic;
        public AddClient(string idClient, Clients.LoadInfoClient lic)
        {
            InitializeComponent();
            this.idClient = idClient;
            this.lic = lic;

            loadInfoAddress();

            if (idClient != null)
            {
                label1.Text = "Изменить клиента";
                AddButton.Text = "Изменить";
                loadInfoClient();
            }
        }
        private void loadInfoClient()
        {
            DB db = new DB();
            string queryInfo = $"SELECT * FROM client WHERE id = '{idClient}'";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                SurnameTextBox.Text = reader["surname"].ToString();
                NameTextBox.Text = reader["name"].ToString();
                PatronymicTextBox.Text = reader["patronymic"].ToString();
                NumberPhoneTextBox.Text = reader["numberPhone"].ToString();
                PassportTextBox.Text = reader["passport"].ToString();

                for (int i = 0; i < AddressComboBox.Items.Count; i++)
                {
                    if (reader["idAddress"].ToString() != "")
                    {
                        if (Convert.ToInt32((AddressComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader["idAddress"]))
                        {
                            AddressComboBox.SelectedIndex = i;
                        }
                    }
                }
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoAddress()
        {
            DB db = new DB();
            string queryInfo = $"SELECT address.id, concat(address.country, ' ', address.city, ' ', address.street, ' ', address.house) as AddressInfo FROM address ";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $"{reader[1]}";
                item.Value = reader[0];
                AddressComboBox.Items.Add(item);
            }
            reader.Close();
            reader.Close();

            db.closeConnection();
        }

        private void CanceledButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            if (idClient == null)
            {
                MySqlCommand command = new MySqlCommand($"INSERT into client (surname, name, patronymic, numberPhone, passport, idAddress) " +
                    $"values(@surname, @name, @patronymic, @numberPhone, @passport, @idAddress)", db.getConnection());
                command.Parameters.AddWithValue("@surname", SurnameTextBox.Text);
                command.Parameters.AddWithValue("@name", NameTextBox.Text);
                command.Parameters.AddWithValue("@patronymic", PatronymicTextBox.Text);
                command.Parameters.AddWithValue("@numberPhone", NumberPhoneTextBox.Text);
                command.Parameters.AddWithValue("@passport", PassportTextBox.Text);
                command.Parameters.AddWithValue("@idAddress", (AddressComboBox.SelectedItem as ComboBoxItem).Value);
                db.openConnection();

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Клиент добавлен");
                    lic();
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                db.closeConnection();
            }
            else
            {
                MySqlCommand command = new MySqlCommand($"Update client set surname = @surname, name = @name, patronymic = @patronymic, numberPhone = @numberPhone, passport = @passport, idAddress = @idAddress where id = {idClient}", db.getConnection());
                command.Parameters.AddWithValue("@surname", SurnameTextBox.Text);
                command.Parameters.AddWithValue("@name", NameTextBox.Text);
                command.Parameters.AddWithValue("@patronymic", PatronymicTextBox.Text);
                command.Parameters.AddWithValue("@numberPhone", NumberPhoneTextBox.Text);
                command.Parameters.AddWithValue("@passport", PassportTextBox.Text);
                command.Parameters.AddWithValue("@idAddress", (AddressComboBox.SelectedItem as ComboBoxItem).Value);
                lic();
                db.openConnection();

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Клиент изменен");
                    lic();
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                db.closeConnection();
            }
        }
    }
}

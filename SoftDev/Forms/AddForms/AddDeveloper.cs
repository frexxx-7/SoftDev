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
    public partial class AddDeveloper : Form
    {
        private string idDeveloper;
        private Developers.LoadInfoDevelopers lid;
        public AddDeveloper(string idDeveloper, Developers.LoadInfoDevelopers lid)
        {
            InitializeComponent();
            this.idDeveloper = idDeveloper;
            this.lid = lid;

            loadInfoAddress();

            if (idDeveloper != null)
            {
                label1.Text = "Изменить разработчика";
                AddButton.Text = "Изменить";
                loadInfoDeveloper();
            }
        }
        private void loadInfoDeveloper()
        {
            DB db = new DB();
            string queryInfo = $"SELECT * FROM developers WHERE id = '{idDeveloper}'";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                SurnameTextBox.Text = reader["surname"].ToString();
                NameTextBox.Text = reader["name"].ToString();
                PatronymicTextBox.Text = reader["patronymic"].ToString();

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
                SkillsTextBox.Text = reader["skills"].ToString();
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
            if (idDeveloper == null)
            {
                MySqlCommand command = new MySqlCommand($"INSERT into developers (surname, name, patronymic, idAddress, skills) " +
                    $"values(@surname, @name, @patronymic, @idAddress, @skills)", db.getConnection());
                command.Parameters.AddWithValue("@surname", SurnameTextBox.Text);
                command.Parameters.AddWithValue("@name", NameTextBox.Text);
                command.Parameters.AddWithValue("@patronymic", PatronymicTextBox.Text);
                command.Parameters.AddWithValue("@idAddress", (AddressComboBox.SelectedItem as ComboBoxItem).Value);
                command.Parameters.AddWithValue("@skills", SkillsTextBox.Text);
                db.openConnection();

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Разработчик добавлен");
                    lid();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                db.closeConnection();
            }
            else
            {
                MySqlCommand command = new MySqlCommand($"Update developers set surname = @surname, name = @name, patronymic = @patronymic, idAddress = @idAddress, skills = @skills where id = {idDeveloper}", db.getConnection());
                command.Parameters.AddWithValue("@surname", SurnameTextBox.Text);
                command.Parameters.AddWithValue("@name", NameTextBox.Text);
                command.Parameters.AddWithValue("@patronymic", PatronymicTextBox.Text);
                command.Parameters.AddWithValue("@idAddress", (AddressComboBox.SelectedItem as ComboBoxItem).Value);
                command.Parameters.AddWithValue("@skills", SkillsTextBox.Text);
                lid();
                db.openConnection();

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Разработчик изменен");
                    lid();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                db.closeConnection();
            }
        }
    }
}

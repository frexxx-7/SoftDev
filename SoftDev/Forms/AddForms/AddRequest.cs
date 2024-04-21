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
    public partial class AddRequest : Form
    {
        private string idRequest;
        private Requests.LoadInfoRequests lir;
        public AddRequest(string idRequest, Requests.LoadInfoRequests lir)
        {
            InitializeComponent();
            this.idRequest = idRequest;

            loadInfoClient();
            loadInfoProject();

            if (idRequest != null)
            {
                label1.Text = "Изменить заявку";
                AddButton.Text = "Изменить";
                loadInfoRequest();
            }

            this.lir = lir;
        }
        private void loadInfoClient()
        {
            DB db = new DB();
            string queryInfo = $"SELECT * FROM client ";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $"{reader[1]} {reader[2]} {reader[3]}";
                item.Value = reader[0];
                ClientComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }

        private void loadInfoProject()
        {
            DB db = new DB();
            string queryInfo = $"SELECT * FROM project ";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $"{reader[1]}";
                item.Value = reader[0];
                ProjectComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }

        private void loadInfoRequest()
        {
            DB db = new DB();
            string queryInfo = $"SELECT * FROM requests WHERE id = '{idRequest}'";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                StateTextBox.Text = reader["state"].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(reader["dateCreate"].ToString());

                for (int i = 0; i < ClientComboBox.Items.Count; i++)
                {
                    if (reader["idClient"].ToString() != "")
                    {
                        if (Convert.ToInt32((ClientComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader["idClient"]))
                        {
                            ClientComboBox.SelectedIndex = i;
                        }
                    }
                }
                for (int i = 0; i < ProjectComboBox.Items.Count; i++)
                {
                    if (reader["idProject"].ToString() != "")
                    {
                        if (Convert.ToInt32((ProjectComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader["idProject"]))
                        {
                            ProjectComboBox.SelectedIndex = i;
                        }
                    }
                }
            }
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
            if (idRequest == null)
            {
                MySqlCommand command = new MySqlCommand($"INSERT into requests (idClient, idProject, dateCreate, state) " +
                    $"values(@idClient, @idProject, @dateCreate, @state)", db.getConnection());
                command.Parameters.AddWithValue("@idClient", (ClientComboBox.SelectedItem as ComboBoxItem).Value);
                command.Parameters.AddWithValue("@idProject", (ProjectComboBox.SelectedItem as ComboBoxItem).Value);
                command.Parameters.AddWithValue("@dateCreate", dateTimePicker1.Value.ToString("dd.MM.yyyy"));
                command.Parameters.AddWithValue("@state", StateTextBox.Text);
                db.openConnection();

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Заявка добавлена");
                    lir();
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
                MySqlCommand command = new MySqlCommand($"Update requests set idClient = @idClient, idProject = @idProject, dateCreate = @dateCreate, state = @state where id = {idRequest}", db.getConnection());
                command.Parameters.AddWithValue("@idClient", (ClientComboBox.SelectedItem as ComboBoxItem).Value);
                command.Parameters.AddWithValue("@idProject", (ProjectComboBox.SelectedItem as ComboBoxItem).Value);
                command.Parameters.AddWithValue("@dateCreate", dateTimePicker1.Value.ToString("dd.MM.yyyy"));
                command.Parameters.AddWithValue("@state", StateTextBox.Text);
                db.openConnection();

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Заявка изменена");
                    lir();
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

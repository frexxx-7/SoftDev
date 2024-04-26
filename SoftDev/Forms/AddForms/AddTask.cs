using MySql.Data.MySqlClient;
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
    public partial class AddTask : Form
    {
        private string idTask;
        private Tasks.LoadInfoTasks lit;
        public AddTask(string idTask, Tasks.LoadInfoTasks lit)
        {
            InitializeComponent();
            this.idTask = idTask;
            this.lit = lit;

            loadInfoProject();

            if (idTask != null)
            {
                label1.Text = "Изменить заявку";
                AddButton.Text = "Изменить";
                loadInfoTask();
            }
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

        private void loadInfoTask()
        {
            DB db = new DB();
            string queryInfo = $"SELECT * FROM tasks WHERE id = '{idTask}'";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                StateTextBox.Text = reader["state"].ToString();
                DescriptionTextBox.Text = reader["description"].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(reader["deadLine"].ToString());

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
            if (idTask == null)
            {
                MySqlCommand command = new MySqlCommand($"INSERT into tasks (idProject, description, deadline, state) " +
                    $"values(@idProject, @description, @deadline, @state)", db.getConnection());
                command.Parameters.AddWithValue("@description", DescriptionTextBox.Text);
                command.Parameters.AddWithValue("@idProject", (ProjectComboBox.SelectedItem as ComboBoxItem).Value);
                command.Parameters.AddWithValue("@deadline", dateTimePicker1.Value.ToString("dd.MM.yyyy"));
                command.Parameters.AddWithValue("@state", StateTextBox.Text);
                db.openConnection();

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Задача добавлена");
                    lit();
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
                MySqlCommand command = new MySqlCommand($"Update tasks set idProject = @idProject, description = @description, deadline = @deadline, state = @state where id = {idTask}", db.getConnection());
                command.Parameters.AddWithValue("@description", DescriptionTextBox.Text);
                command.Parameters.AddWithValue("@idProject", (ProjectComboBox.SelectedItem as ComboBoxItem).Value);
                command.Parameters.AddWithValue("@deadline", dateTimePicker1.Value.ToString("dd.MM.yyyy"));
                command.Parameters.AddWithValue("@state", StateTextBox.Text);
                db.openConnection();

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Задача изменена");
                    lit();
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

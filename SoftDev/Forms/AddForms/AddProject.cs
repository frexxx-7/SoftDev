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
    public partial class AddProject : Form
    {
        private string idProject;
        private Projects.LoadInfoProject lip;
        public AddProject(string idProject, Projects.LoadInfoProject lip)
        {
            InitializeComponent();
            this.idProject = idProject;
            this.lip = lip;
            
            if (idProject!= null)
            {
                label1.Text = "Изменить клиента";
                AddButton.Text = "Изменить";
                loadInfoProject();
            }
        }
        private void loadInfoProject()
        {
            DB db = new DB();
            string queryInfo = $"SELECT * FROM project WHERE id = '{idProject}'";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                NameTextBox.Text = reader["name"].ToString();
                DescriptionRichTextBox.Text = reader["description"].ToString();
                StateTextBox.Text = reader["state"].ToString();

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
            if (idProject == null)
            {
                MySqlCommand command = new MySqlCommand($"INSERT into project (name, description, state) " +
                    $"values(@name, @description, @state)", db.getConnection());
                command.Parameters.AddWithValue("@name", NameTextBox.Text);
                command.Parameters.AddWithValue("@description", DescriptionRichTextBox.Text);
                command.Parameters.AddWithValue("@state", StateTextBox.Text);
             
                db.openConnection();

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Проект добавлен");
                    lip();
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
                MySqlCommand command = new MySqlCommand($"Update project set name = @name, description = @description, state = @state where id = {idProject}", db.getConnection());
                command.Parameters.AddWithValue("@name", NameTextBox.Text);
                command.Parameters.AddWithValue("@description", DescriptionRichTextBox.Text);
                command.Parameters.AddWithValue("@state", StateTextBox.Text);
                
                db.openConnection();

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Проект изменен");
                    lip();
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

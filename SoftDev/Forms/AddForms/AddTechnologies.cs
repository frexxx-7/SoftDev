using MySql.Data.MySqlClient;
using SoftDev.Classes;
using SoftDev.Forms.AdminForms.References;
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
    public partial class AddTechnologies : Form
    {
        private string idTechnologies;
        private Technologies.LoadInfoTechnologies lit;
        public AddTechnologies(string idTechnologies, Technologies.LoadInfoTechnologies lit)
        {
            InitializeComponent();
            this.idTechnologies = idTechnologies;
            this.lit = lit;

            if (idTechnologies != null)
            {
                label1.Text = "Изменить технологию";
                AddButton.Text = "Изменить";
                loadInfoTechnologies();
            }
        }

        private void loadInfoTechnologies()
        {
            DB db = new DB();
            string queryInfo = $"SELECT * FROM technologies WHERE id = '{idTechnologies}'";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                NameTextBox.Text = reader["name"].ToString();
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
            if (idTechnologies == null)
            {
                MySqlCommand command = new MySqlCommand($"INSERT into technologies (name) " +
                    $"values(@name)", db.getConnection());
                command.Parameters.AddWithValue("@name", NameTextBox.Text);
                db.openConnection();

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Технология добавлена");
                    lit();
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
                MySqlCommand command = new MySqlCommand($"Update technologies set name = @name where id = {idTechnologies}", db.getConnection());
                command.Parameters.AddWithValue("@name", NameTextBox.Text);
                db.openConnection();

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Технология изменена");
                    lit();
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

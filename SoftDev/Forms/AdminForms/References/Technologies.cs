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

namespace SoftDev.Forms.AdminForms.References
{
    public partial class Technologies : Form
    {
        public delegate void LoadInfoTechnologies();
        private LoadInfoTechnologies lit;
        public Technologies()
        {
            InitializeComponent();
        }

        private void Technologies_Load(object sender, EventArgs e)
        {
            lit = loadInfoTechnogies;
            loadInfoTechnogies();
        }
        private void loadInfoTechnogies()
        {
            DB db = new DB();

            TechnologiesDataGridView.Rows.Clear();

            string query = $"select * from technologies";

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
                    TechnologiesDataGridView.Rows.Add(s);
            }
            db.closeConnection();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            new AddTechnologies(null, lit).Show();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            new AddTechnologies(TechnologiesDataGridView[0, TechnologiesDataGridView.SelectedCells[0].RowIndex].Value.ToString(), lit).Show();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"delete from technologies where id = {TechnologiesDataGridView[0, TechnologiesDataGridView.SelectedCells[0].RowIndex].Value}", db.getConnection());
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Технология удалена");

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
            loadInfoTechnogies();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            TechnologiesDataGridView.Rows.Clear();

            string searchString = $"select * from technologies " +
                $"where concat (technologies.name) like '%" + SearchTextBox.Text + "%'";

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
                    TechnologiesDataGridView.Rows.Add(s);
            }
            db.closeConnection();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

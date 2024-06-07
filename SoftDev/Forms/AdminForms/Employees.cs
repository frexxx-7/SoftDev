using Guna.UI2.WinForms;
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

namespace SoftDev.Forms.AdminForms
{
    public partial class Employees : Form
    {
        private Guna2Panel addPanel;
        public Employees()
        {
            InitializeComponent();
        }
        private void loadInfoPosition()
        {
            DB db = new DB();

            PositionsDataGrid.Rows.Clear();

            string query = $"select * from positions ";

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
                    PositionsDataGrid.Rows.Add(s);
            }
            db.closeConnection();
        }
        private void loadInfoDepartament()
        {
            DB db = new DB();

            DepartamentsDataGrid.Rows.Clear();

            string query = $"select * from departaments ";

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
                    DepartamentsDataGrid.Rows.Add(s);
            }
            db.closeConnection();
        }
        private void loadInfoEmployees()
        {
            DB db = new DB();

            PositionsDataGrid.Rows.Clear();

            string query = $"select * from positions ";

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
                    PositionsDataGrid.Rows.Add(s);
            }
            db.closeConnection();
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Autorization().Show();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            addPanel.Visible = true;
            AddButtonPos.Text = "Добавить";
            EmpAddButton.Text = "Добавить";
            AddDepButton.Text = "Добавить";
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            addPanel.Visible = true;
            AddButtonPos.Text = "Изменить";
            EmpAddButton.Text = "Изменить";
            AddDepButton.Text = "Изменить";

            switch (EmployeesTab.SelectedIndex)
            {
                case 0:
                    addPanel = guna2Panel3;
                    break;
                case 1:
                    loadInfoOneDepartament(DepartamentsDataGrid[0, DepartamentsDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    break;
                case 2:
                    loadInfoOnePostion(PositionsDataGrid[0, PositionsDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    break;
                default:
                    break;
            }
        }

        private void guna2Button15_Click(object sender, EventArgs e)
        {
            addPanel.Visible = false;
        }

        private void EmployeesTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (EmployeesTab.SelectedIndex)
            {
                case 0:
                    addPanel = guna2Panel3;
                    break;
                case 1:
                    addPanel = guna2Panel5;
                break;
                case 2:
                    addPanel = guna2Panel6;
                break;
                default:
                    break;
            }
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            addPanel = guna2Panel3;
            loadInfoPosition();
            loadInfoDepartament();
        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            addPanel.Visible = false;
        }

        private void guna2Button18_Click(object sender, EventArgs e)
        {
            addPanel.Visible = false;
        }

        private void addPositionInDB()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"INSERT into positions (name) values(@name)", db.getConnection());
            command.Parameters.AddWithValue("@name", PositionsTextBox.Text);
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Должность добавлена");
                loadInfoPosition();
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            PositionsTextBox.Text = "";
            db.closeConnection();
        }
        private void addDepartamentInDB()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"INSERT into departaments (name) values(@name)", db.getConnection());
            command.Parameters.AddWithValue("@name", DepartamentsNameTextBox.Text);
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Отдел добавлен");
                loadInfoPosition();
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            PositionsTextBox.Text = "";
            db.closeConnection();
        }
        private void loadInfoOnePostion(string idRegion)
        {
            DB db = new DB();
            string queryInfo = $"select * from positions " +
            $"where id = {idRegion}";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                PositionsTextBox.Text = reader["name"].ToString();
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoOneDepartament(string idRegion)
        {
            DB db = new DB();
            string queryInfo = $"select * from departaments " +
            $"where id = {idRegion}";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                DepartamentsNameTextBox.Text = reader["name"].ToString();
            }
            reader.Close();

            db.closeConnection();
        }
        private void updatePositionInDB(string idRegion)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"update positions set name=@name where id = {idRegion}", db.getConnection());
            command.Parameters.AddWithValue("@name", PositionsTextBox.Text);

            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Должность измененв");
                loadInfoPosition();

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
        }
        private void updateDepartamentInDB(string idRegion)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"update departaments set name=@name where id = {idRegion}", db.getConnection());
            command.Parameters.AddWithValue("@name", DepartamentsNameTextBox.Text);

            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Отдел изменен");
                loadInfoPosition();

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
        }
        private void AddButtonPos_Click(object sender, EventArgs e)
        {
            if (AddButtonPos.Text == "Добавить")
                addPositionInDB();
            else
                updatePositionInDB(PositionsDataGrid[0, PositionsDataGrid.SelectedCells[0].RowIndex].Value.ToString());

            loadInfoPosition();
        }
        private void deleteRecordInBd(string tableName, string id)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"delete from {tableName} where id = {id}", db.getConnection());
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Запись удалена");

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
        }
        private void guna2Button12_Click(object sender, EventArgs e)
        {
            switch (EmployeesTab.SelectedIndex)
            {
                case 0:
                    addPanel = guna2Panel3;
                    break;
                case 1:
                    deleteRecordInBd("departaments", DepartamentsDataGrid[0, DepartamentsDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    loadInfoDepartament();
                    break;
                case 2:
                    deleteRecordInBd("positions", PositionsDataGrid[0, PositionsDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    loadInfoPosition();
                    break;
                default:
                    break;
            }
        }

        private void AddDepButton_Click(object sender, EventArgs e)
        {
            if (AddButtonPos.Text == "Добавить")
                addDepartamentInDB();
            else
                updateDepartamentInDB(DepartamentsDataGrid[0, DepartamentsDataGrid.SelectedCells[0].RowIndex].Value.ToString());

            loadInfoDepartament();
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            new Main().Show();
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            new Users().Show();
            this.Close();
        }
    }
}

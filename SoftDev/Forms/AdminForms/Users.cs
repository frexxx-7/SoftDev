using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using SoftDev.Classes;
using SoftDev.Forms.Reports;
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
    public partial class Users : Form
    {
        private Guna2Panel addPanel;

        public Users()
        {
            InitializeComponent();
        }

        private void EmployeesTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadInfoAccessRightComboBox();
            switch (UsersTab.SelectedIndex)
            {
                case 0:
                    addPanel = guna2Panel5;
                    break;
                case 1:
                    addPanel = guna2Panel6;
                    break;
                default:
                    break;
            }
        }

        private void Users_Load(object sender, EventArgs e)
        {
            addPanel = guna2Panel5;
            loadInfoUsers();
            loadInfoAccessRightComboBox();
            loadInfoAccessRight();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loadInfoUsers()
        {
            DB db = new DB();

            UsersDataGrid.Rows.Clear();

            string query = $"select users.id, login, password, CONCAT(accessright.name, ' ', accessright.role) as accessrightInfo from users " +
                $"left join accessright on users.idAccessRight = accessright.id ";

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
                    UsersDataGrid.Rows.Add(s);
            }
            db.closeConnection();
        }
        private void loadInfoAccessRight()
        {
            DB db = new DB();

            AccessRightDataGrid.Rows.Clear();

            string query = $"select * from accessright ";

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
                    AccessRightDataGrid.Rows.Add(s);
            }
            db.closeConnection();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            addPanel.Visible = true;
            AddAccessRightButton.Text = "Добавить";
            AddUserButton.Text = "Добавить";
        }
        private void loadInfoAccessRightComboBox()
        {
            AccessRightComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT id, name, role FROM accessright";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                AccessRightComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoOneUser(string idRegion)
        {
            DB db = new DB();
            string queryInfo = $"select * from users " +
            $"where id = {idRegion}";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                LoginTextBox.Text = reader["login"].ToString();
                PasswordTextBox.Text = reader["password"].ToString();

                for (int i = 0; i < AccessRightComboBox.Items.Count; i++)
                {
                    if (reader["idAccessRight"].ToString() != "")
                    {
                        if (Convert.ToInt32((AccessRightComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader["idAccessRight"]))
                        {
                            AccessRightComboBox.SelectedIndex = i;
                        }
                    }
                }
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoOneAccessRight(string idRegion)
        {
            DB db = new DB();
            string queryInfo = $"select * from accessright " +
            $"where id = {idRegion}";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                AccessRightNameTextBox.Text = reader["name"].ToString();
                RoleTextBox.Text = reader["role"].ToString();
            }
            reader.Close();

            db.closeConnection();
        }
        private void guna2Button11_Click(object sender, EventArgs e)
        {
            addPanel.Visible = true;
            AddAccessRightButton.Text = "Изменить";
            AddUserButton.Text = "Изменить";

            switch (UsersTab.SelectedIndex)
            {
                case 0:
                    loadInfoOneUser(UsersDataGrid[0, UsersDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    break;
                case 1:
                    loadInfoOneAccessRight(AccessRightDataGrid[0, AccessRightDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    break;
                default:
                    break;
            }
        }

        private void guna2Button18_Click(object sender, EventArgs e)
        {
            addPanel.Visible = false;
        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            addPanel.Visible = false;
        }
        private void addAccessRightInDB()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"INSERT into accessright (name, role) values(@name, @role)", db.getConnection());
            command.Parameters.AddWithValue("@name", AccessRightNameTextBox.Text);
            command.Parameters.AddWithValue("@role", RoleTextBox.Text);
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Право доступа добавлено");
                loadInfoAccessRight();
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            AccessRightNameTextBox.Text = "";
            RoleTextBox.Text = "";
            db.closeConnection();
        }
        private void addUserInDB()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"INSERT into users (login, password, idAccessRight) values(@login, @password, @idAccessRight)", db.getConnection());
            command.Parameters.AddWithValue("@login", LoginTextBox.Text);
            command.Parameters.AddWithValue("@password", PasswordTextBox.Text);
            command.Parameters.AddWithValue("@idAccessRight", (AccessRightComboBox.SelectedItem as ComboBoxItem).Value);
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Пользователь добавлен");
                loadInfoAccessRight();
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            PasswordTextBox.Text = "";
            LoginTextBox.Text = "";
            AccessRightComboBox.SelectedIndex = -1;
            db.closeConnection();
        }
        private void updateAccessRightInDB(string idRegion)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"update accessright set name=@name, role = @role where id = {idRegion}", db.getConnection());
            command.Parameters.AddWithValue("@name", AccessRightNameTextBox.Text);
            command.Parameters.AddWithValue("@role", RoleTextBox.Text);

            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Право доступа изменено");
                loadInfoAccessRight();

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
        }

        private void updateUserInDB(string idRegion)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"update users set login=@login, password = @password, idAccessRight = @idAccessRight where id = {idRegion}", db.getConnection());
            command.Parameters.AddWithValue("@login", LoginTextBox.Text);
            command.Parameters.AddWithValue("@password", PasswordTextBox.Text);
            command.Parameters.AddWithValue("@idAccessRight", (AccessRightComboBox.SelectedItem as ComboBoxItem).Value);

            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Пользователь изменен");
                loadInfoAccessRight();

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
        }

        private void AddUserButton_Click(object sender, EventArgs e)
        {
            if (AddUserButton.Text == "Добавить")
                addUserInDB();
            else
                updateUserInDB(UsersDataGrid[0, UsersDataGrid.SelectedCells[0].RowIndex].Value.ToString());

            loadInfoUsers();
        }

        private void AddAccessRightButton_Click(object sender, EventArgs e)
        {
            if (AddUserButton.Text == "Добавить")
                addAccessRightInDB();
            else
                updateAccessRightInDB(AccessRightDataGrid[0, AccessRightDataGrid.SelectedCells[0].RowIndex].Value.ToString());

            loadInfoAccessRight();
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
            switch (UsersTab.SelectedIndex)
            {
                case 0:
                    deleteRecordInBd("users", UsersDataGrid[0, UsersDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    loadInfoUsers();
                    break;
                case 1:
                    deleteRecordInBd("accessright", AccessRightDataGrid[0, AccessRightDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    loadInfoAccessRight();
                    break;
                default:
                    break;
            }
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            new Locality().Show();
            this.Close();
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Employees().Show();
            this.Close();
        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Users().Show();
            this.Close();
        }

        private void организацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Organizations().Show();
            this.Close();
        }

        private void отделыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Locality().Show();
            this.Close();
        }

        private void программноеОбеспечениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SoftWare().Show();
            this.Close();
        }

        private void создатьЗаявкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Request().Show();
            this.Close();
        }

        private void заявкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Request().Show();
            this.Close();
        }

        private void информацияОРазработкеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Development().Show();
            this.Close();
        }

        private void актыПриемапередачиПОToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TransferAcceptanceCertificate().Show();
            this.Close();
        }

        private void договораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Agreement().Show();
            this.Close();
        }

        private void переченьПОToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ScrollSoftWare().Show();
            this.Close();
        }

        private void разработчикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Developers().Show();
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

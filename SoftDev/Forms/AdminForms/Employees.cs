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

            EmployeesDatagrid.Rows.Clear();

            string query = $"select employees.id, employees.surname, employees.name, employees.patronymic, employees.dateBirthday, departaments.name, " +
                $"positions.name, users.login, locality.name, numberPhone, street, house, frame, apartment, email, supervisor from employees " +
                $"inner join departaments on employees.idDepartament = departaments.id " +
                $"inner join positions on employees.idPosition = positions.id " +
                $"inner join users on employees.idUser = users.id " +
                $"inner join locality on employees.idLocality = locality.id ";

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
                    EmployeesDatagrid.Rows.Add(s);
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

        private void loadInfoOneEmployee(string idRegion)
        {
            DB db = new DB();
            string queryInfo = $"select * from employees " +
            $"where id = {idRegion}";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                SurnameTextBox.Text = reader[1].ToString();
                NameTextBox.Text = reader[2].ToString();
                PatronymicTextBox.Text = reader[3].ToString();
                DateBirthdayDateTimePicker.Value = Convert.ToDateTime(reader[4].ToString());

                for (int i = 0; i < DepartamentComboBox.Items.Count; i++)
                {
                    if (reader[5].ToString() != "")
                    {
                        if (Convert.ToInt32((DepartamentComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader[5]))
                        {
                            DepartamentComboBox.SelectedIndex = i;
                        }
                    }
                }
                for (int i = 0; i < PositionComboBox.Items.Count; i++)
                {
                    if (reader[6].ToString() != "")
                    {
                        if (Convert.ToInt32((PositionComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader[6]))
                        {
                            PositionComboBox.SelectedIndex = i;
                        }
                    }
                }
                for (int i = 0; i < UserComboBox.Items.Count; i++)
                {
                    if (reader[7].ToString() != "")
                    {
                        if (Convert.ToInt32((UserComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader[7]))
                        {
                            UserComboBox.SelectedIndex = i;
                        }
                    }
                }
                for (int i = 0; i < LocalityComboBox.Items.Count; i++)
                {
                    if (reader[8].ToString() != "")
                    {
                        if (Convert.ToInt32((LocalityComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader[8]))
                        {
                            LocalityComboBox.SelectedIndex = i;
                        }
                    }
                }

                PhoneTextBox.Text = reader[9].ToString();
                StreetTextBox.Text = reader[10].ToString();
                HouseTextBox.Text = reader[11].ToString();
                FrameTextBox.Text = reader[12].ToString();
                ApartmentTextBox.Text = reader[13].ToString();
                EmailTextBox.Text = reader[14].ToString();
                SupervisorCheckbox.Checked = Convert.ToBoolean(reader[15]);
            }
            reader.Close();

            db.closeConnection();
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
                    loadInfoOneEmployee(EmployeesDatagrid[0, EmployeesDatagrid.SelectedCells[0].RowIndex].Value.ToString());
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
            loadInfoDepartamentComboBox();
            loadInfoPositionComboBox();
            loadInfoUsersComboBox();
            loadInfoLocalityComboBox();
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
        private void loadInfoDepartamentComboBox()
        {
            DepartamentComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT id, name FROM departaments";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                DepartamentComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoPositionComboBox()
        {
            PositionComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT id, name FROM positions";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                PositionComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoUsersComboBox()
        {
            UserComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT id, login FROM users";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                UserComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoLocalityComboBox()
        {
            LocalityComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT id, name FROM locality";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                LocalityComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }
        private void Employees_Load(object sender, EventArgs e)
        {
            addPanel = guna2Panel3;
            loadInfoPosition();
            loadInfoDepartament();
            loadInfoEmployees();
            loadInfoDepartamentComboBox();
            loadInfoPositionComboBox();
            loadInfoUsersComboBox();
            loadInfoLocalityComboBox();
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
        private void addEmployeetInDB()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"INSERT into employees (surname, name, patronymic, dateBirthday, idDepartament, idPosition, idUser, idLocality, numberPhone, street, house, frame, apartment, email, supervisor) values(" +
                                                    $"@surname, @name, @patronymic, @dateBirthday, @idDepartament, @idPosition, @idUser, @idLocality, @numberPhone, @street, @house, @frame, @apartment, @email, @supervisor)", db.getConnection());
            command.Parameters.AddWithValue("@surname", SurnameTextBox.Text);
            command.Parameters.AddWithValue("@name", NameTextBox.Text);
            command.Parameters.AddWithValue("@patronymic", PatronymicTextBox.Text);
            command.Parameters.AddWithValue("@dateBirthday", DateBirthdayDateTimePicker.Value.ToString("yyyy.MM.dd"));
            command.Parameters.AddWithValue("@idDepartament", (DepartamentComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@idPosition", (PositionComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@idUser", (UserComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@idLocality", (LocalityComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@numberPhone", PhoneTextBox.Text);
            command.Parameters.AddWithValue("@street", StreetTextBox.Text);
            command.Parameters.AddWithValue("@house", HouseTextBox.Text);
            command.Parameters.AddWithValue("@frame", FrameTextBox.Text);
            command.Parameters.AddWithValue("@apartment", ApartmentTextBox.Text);
            command.Parameters.AddWithValue("@email", EmailTextBox.Text);
            command.Parameters.AddWithValue("@supervisor", SupervisorCheckbox.Checked);
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Сотрудник добавлен");
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
        private void updateEmployeeInDB(string idRegion)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"update employees set surname=@surname, name=@name, patronymic=@patronymic, dateBirthday=@dateBirthday, idDepartament=@idDepartament, idPosition=@idPosition, idUser=@idUser, idLocality=@idLocality, numberPhone=@numberPhone, street=@street, house=@house, frame=@frame, apartment=@apartment, email=@email, supervisor=@supervisor where id = {idRegion}", db.getConnection());
            command.Parameters.AddWithValue("@surname", SurnameTextBox.Text);
            command.Parameters.AddWithValue("@name", NameTextBox.Text);
            command.Parameters.AddWithValue("@patronymic", PatronymicTextBox.Text);
            command.Parameters.AddWithValue("@dateBirthday", DateBirthdayDateTimePicker.Value.ToString("yyyy.MM.dd"));
            command.Parameters.AddWithValue("@idDepartament", (DepartamentComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@idPosition", (PositionComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@idUser", (UserComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@idLocality", (LocalityComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@numberPhone", PhoneTextBox.Text);
            command.Parameters.AddWithValue("@street", StreetTextBox.Text);
            command.Parameters.AddWithValue("@house", HouseTextBox.Text);
            command.Parameters.AddWithValue("@frame", FrameTextBox.Text);
            command.Parameters.AddWithValue("@apartment", ApartmentTextBox.Text);
            command.Parameters.AddWithValue("@email", EmailTextBox.Text);
            command.Parameters.AddWithValue("@supervisor", SupervisorCheckbox.Checked);

            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Сотрудник изменен");
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
                    deleteRecordInBd("employees", EmployeesDatagrid[0, EmployeesDatagrid.SelectedCells[0].RowIndex].Value.ToString());
                    loadInfoEmployees();
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

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            new Locality().Show();
            this.Close();
        }

        private void EmpAddButton_Click(object sender, EventArgs e)
        {
            if (EmpAddButton.Text == "Добавить")
                addEmployeetInDB();
            else
                updateEmployeeInDB(EmployeesDatagrid[0, EmployeesDatagrid.SelectedCells[0].RowIndex].Value.ToString());
            loadInfoEmployees();
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
    }
}

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
    public partial class Development : Form
    {
        private Guna2Panel addPanel;
        public Development()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            new Autorization().Show();
            this.Close();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Development_Load(object sender, EventArgs e)
        {
            addPanel = guna2Panel5;
            loadInfoDevelopment();
            loadInfoRequestsComboBox();
            loadInfoEmployeeComboBox();
        }

        private void DevelopmentTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadInfoRequestsComboBox();
            loadInfoEmployeeComboBox();
            switch (DevelopmentTab.SelectedIndex)
            {
                case 0:
                    addPanel = guna2Panel5;
                    break;
                default:
                    break;
            }
        }
        private void loadInfoDevelopment()
        {
            DB db = new DB();

            DevelopmentDataGrid.Rows.Clear();

            string query = $"select development.id, development.dateStart, development.dateEnd, concat(employees.surname, ' ', employees.name, ' ', employees.patronymic), requests.name from development " +
                $"inner join employees on development.idEmployees = employees.id " +
                $"inner join requests on development.idRequest = requests.id";

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
                    DevelopmentDataGrid.Rows.Add(s);
            }
            db.closeConnection();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            addPanel.Visible = true;
            AddDevelopmentButton.Text = "Добавить";
        }
        private void loadInfoEmployeeComboBox()
        {
            DeveloperComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT id, concat(employees.surname, ' ', employees.name, ' ', employees.patronymic) FROM employees";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                DeveloperComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoRequestsComboBox()
        {
            RequestsComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT id, name FROM requests";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                RequestsComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoOneDevelopment(string idRegion)
        {
            DB db = new DB();
            string queryInfo = $"select * from development " +
            $"where id = {idRegion}";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                DateStartDateTimePicker.Value = Convert.ToDateTime(reader[1].ToString());
                DateEndTimePicker.Value = Convert.ToDateTime(reader[2].ToString());

                for (int i = 0; i < DeveloperComboBox.Items.Count; i++)
                {
                    if (reader["idEmployees"].ToString() != "")
                    {
                        if (Convert.ToInt32((DeveloperComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader["idEmployees"]))
                        {
                            DeveloperComboBox.SelectedIndex = i;
                        }
                    }
                }
                for (int i = 0; i < RequestsComboBox.Items.Count; i++)
                {
                    if (reader["idRequest"].ToString() != "")
                    {
                        if (Convert.ToInt32((RequestsComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader["idRequest"]))
                        {
                            RequestsComboBox.SelectedIndex = i;
                        }
                    }
                }
            }
            reader.Close();

            db.closeConnection();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            addPanel.Visible = true;
            AddDevelopmentButton.Text = "Изменить";

            switch (DevelopmentTab.SelectedIndex)
            {
                case 0:
                    loadInfoOneDevelopment(DevelopmentDataGrid[0, DevelopmentDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    break;
                default:
                    break;
            }
        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            addPanel.Visible = false;
        }
        private void addDevelopmentInDB()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"INSERT into development (dateStart, dateEnd, idEmployees, idRequest) values(@dateStart, @dateEnd, @idEmployees, @idRequest)", db.getConnection());
            command.Parameters.AddWithValue("@dateStart", DateStartDateTimePicker.Value.ToString("yyyy.MM.dd"));
            command.Parameters.AddWithValue("@dateEnd", DateEndTimePicker.Value.ToString("yyyy.MM.dd"));
            command.Parameters.AddWithValue("@idEmployees", (DeveloperComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@idRequest", (RequestsComboBox.SelectedItem as ComboBoxItem).Value);
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Разработка добавлена");
                loadInfoDevelopment();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DeveloperComboBox.SelectedIndex = -1;
            RequestsComboBox.SelectedIndex = -1;
            db.closeConnection();
        }
        private void updateDevelopmentInDB(string idRegion)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"update development set dateStart=@dateStart, dateEnd=@dateEnd, idEmployees=@idEmployees, idRequest=@idRequest where id = {idRegion}", db.getConnection());
            command.Parameters.AddWithValue("@dateStart", DateStartDateTimePicker.Value.ToString("yyyy.MM.dd"));
            command.Parameters.AddWithValue("@dateEnd", DateEndTimePicker.Value.ToString("yyyy.MM.dd"));
            command.Parameters.AddWithValue("@idEmployees", (DeveloperComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@idRequest", (RequestsComboBox.SelectedItem as ComboBoxItem).Value);

            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Разработка изменена");
                loadInfoDevelopment();

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
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
            switch (DevelopmentTab.SelectedIndex)
            {
                case 0:
                    deleteRecordInBd("development", DevelopmentDataGrid[0, DevelopmentDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    loadInfoDevelopment();
                    break;
                default:
                    break;
            }
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

        private void AddDevelopmentButton_Click(object sender, EventArgs e)
        {
            if (AddDevelopmentButton.Text == "Добавить")
                addDevelopmentInDB();
            else
                updateDevelopmentInDB(DevelopmentDataGrid[0, DevelopmentDataGrid.SelectedCells[0].RowIndex].Value.ToString());

            loadInfoDevelopment();
        }

        private void информацияОРазработкеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Development().Show();
            this.Close();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void отчетностьToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
    }
}

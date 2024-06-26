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
    public partial class Request : Form
    {
        private Guna2Panel addPanel;
        public Request()
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

        private void Request_Load(object sender, EventArgs e)
        {
            addPanel = guna2Panel5;
            loadInfoSoftWareComboBox();
            loadInfoOrganizationComboBox();
            loadInfoRequests();
        }

        private void RequestTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadInfoSoftWareComboBox();
            loadInfoOrganizationComboBox();
            switch (RequestTab.SelectedIndex)
            {
                case 0:
                    addPanel = guna2Panel5;
                    break;
                default:
                    break;
            }
        }
        private void loadInfoRequests()
        {
            DB db = new DB();

            RequestDataGrid.Rows.Clear();

            string query = $"select requests.id, requests.dateRequest, requests.name, requests.formularProblem, software.name, organizations.name, requests.state from requests " +
                $"inner join software on requests.idSoftware = software.id " +
                $"inner join organizations on requests.idOrganizations = organizations.id";

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
                    RequestDataGrid.Rows.Add(s);
            }
            db.closeConnection();
        }

        private void AddSoftWareButton_Click(object sender, EventArgs e)
        {
            if (AddRequestButton.Text == "Добавить")
                addRequestInDB();
            else
                updateRequestInDB(RequestDataGrid[0, RequestDataGrid.SelectedCells[0].RowIndex].Value.ToString());

            loadInfoRequests();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            addPanel.Visible = true;
            AddRequestButton.Text = "Добавить";
        }
        private void loadInfoSoftWareComboBox()
        {
            SoftWareComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT id, name FROM software";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                SoftWareComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }

        private void loadInfoOrganizationComboBox()
        {
            OrganizationComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT id, name FROM organizations";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                OrganizationComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoOneRequest(string idRegion)
        {
            DB db = new DB();
            string queryInfo = $"select * from requests " +
            $"where id = {idRegion}";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                DateRequestDateTimePicker.Value = Convert.ToDateTime(reader[1].ToString());
                NameTextBox.Text = reader[2].ToString();
                FormularProblemTextBox.Text = reader[3].ToString();

                for (int i = 0; i < SoftWareComboBox.Items.Count; i++)
                {
                    if (reader["idSoftware"].ToString() != "")
                    {
                        if (Convert.ToInt32((SoftWareComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader["idSoftware"]))
                        {
                            SoftWareComboBox.SelectedIndex = i;
                        }
                    }
                }
                for (int i = 0; i < OrganizationComboBox.Items.Count; i++)
                {
                    if (reader["idOrganizations"].ToString() != "")
                    {
                        if (Convert.ToInt32((OrganizationComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader["idOrganizations"]))
                        {
                            OrganizationComboBox.SelectedIndex = i;
                        }
                    }
                }
                for (int i = 0; i < StateComboBox.Items.Count; i++)
                {
                    if (reader["state"].ToString() != "")
                    {
                        if (StateComboBox.Items[i].ToString() == reader["state"].ToString())
                        {
                            StateComboBox.SelectedIndex = i;
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
            AddRequestButton.Text = "Изменить";

            switch (RequestTab.SelectedIndex)
            {
                case 0:
                    loadInfoOneRequest(RequestDataGrid[0, RequestDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    break;
                default:
                    break;
            }
        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            addPanel.Visible = false;
        }
        private void addRequestInDB()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"INSERT into requests (dateRequest, name, formularProblem, idSoftware, idOrganizations, state) values(@dateRequest, @name, @formularProblem, @idSoftware, @idOrganizations, @state)", db.getConnection());
            command.Parameters.AddWithValue("@dateRequest", DateRequestDateTimePicker.Value.ToString("yyyy.MM.dd"));
            command.Parameters.AddWithValue("@name", NameTextBox.Text);
            command.Parameters.AddWithValue("@formularProblem", FormularProblemTextBox.Text);
            command.Parameters.AddWithValue("@idSoftware", (SoftWareComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@idOrganizations", (OrganizationComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@state", StateComboBox.Text);
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Заявка добавлено");
                loadInfoRequests();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            NameTextBox.Text = "";
            FormularProblemTextBox.Text = "";
            SoftWareComboBox.SelectedIndex = -1;
            OrganizationComboBox.SelectedIndex = -1;
            db.closeConnection();
        }
        private void updateRequestInDB(string idRegion)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"update requests set dateRequest=@dateRequest, name=@name, formularProblem=@formularProblem, idSoftware=@idSoftware, idOrganizations=@idOrganizations, state=@state where id = {idRegion}", db.getConnection());
            command.Parameters.AddWithValue("@dateRequest", DateRequestDateTimePicker.Value.ToString("yyyy.MM.dd"));
            command.Parameters.AddWithValue("@name", NameTextBox.Text);
            command.Parameters.AddWithValue("@formularProblem", FormularProblemTextBox.Text);
            command.Parameters.AddWithValue("@idSoftware", (SoftWareComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@idOrganizations", (OrganizationComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@state", StateComboBox.Text);

            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Заявка изменена");
                loadInfoRequests();

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
            switch (RequestTab.SelectedIndex)
            {
                case 0:
                    deleteRecordInBd("requests", RequestDataGrid[0, RequestDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    loadInfoRequests();
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

        private void информацияОРазработкеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Development().Show();
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

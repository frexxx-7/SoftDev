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
    public partial class SoftWare : Form
    {
        private Guna2Panel addPanel;
        public SoftWare()
        {
            InitializeComponent();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SoftwWareTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadInfoDevToolsComboBox();
            loadInfoSoftWareComboBox();
            switch (SoftwWareTab.SelectedIndex)
            {
                case 0:
                    addPanel = guna2Panel5;
                    break;
                case 1:
                    addPanel = guna2Panel6;
                    break;
                case 2:
                    addPanel = guna2Panel3;
                    break;
                default:
                    break;
            }
        }

        private void SoftWare_Load(object sender, EventArgs e)
        {
            addPanel = guna2Panel5;
            loadInfoSoftWare();
            loadInfoDevTools();
            loadInfoListDevTools();
            loadInfoDevToolsComboBox();
            loadInfoSoftWareComboBox();
        }
        private void loadInfoSoftWare()
        {
            DB db = new DB();

            SoftWareDataGrid.Rows.Clear();

            string query = $"select * from software ";

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
                    SoftWareDataGrid.Rows.Add(s);
            }
            db.closeConnection();
        }
        private void loadInfoDevTools()
        {
            DB db = new DB();

            DevToolsDataGrid.Rows.Clear();

            string query = $"select * from devtools ";

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
                    DevToolsDataGrid.Rows.Add(s);
            }
            db.closeConnection();
        }
        private void loadInfoListDevTools()
        {
            DB db = new DB();

            ListDevToolsDataGridView.Rows.Clear();

            string query = $"select listdevtools.id, devtools.name, software.name from listdevtools " +
                $"inner join devtools on listdevtools.idDevTools = devtools.id " +
                $"inner join software on listdevtools.idSoftWare = software.id ";

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
                    ListDevToolsDataGridView.Rows.Add(s);
            }
            db.closeConnection();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            addPanel.Visible = true;
            AddDevToolsButton.Text = "Добавить";
            AddSoftWareButton.Text = "Добавить";
            AddListDevTools.Text = "Добавить";
        }
        private void loadInfoDevToolsComboBox()
        {
            DevToolsComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT id, name FROM devtools";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                DevToolsComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
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
        private void loadInfoOneListDevTools(string idRegion)
        {
            DB db = new DB();
            string queryInfo = $"select * from listdevtools " +
            $"where id = {idRegion}";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < DevToolsComboBox.Items.Count; i++)
                {
                    if (reader["idDevTools"].ToString() != "")
                    {
                        if (Convert.ToInt32((DevToolsComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader["idDevTools"]))
                        {
                            DevToolsComboBox.SelectedIndex = i;
                        }
                    }
                }
                for (int i = 0; i < SoftWareComboBox.Items.Count; i++)
                {
                    if (reader["idSoftWare"].ToString() != "")
                    {
                        if (Convert.ToInt32((SoftWareComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader["idSoftWare"]))
                        {
                            SoftWareComboBox.SelectedIndex = i;
                        }
                    }
                }
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoOneSoftware(string idRegion)
        {
            DB db = new DB();
            string queryInfo = $"select * from software " +
            $"where id = {idRegion}";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                SoftWareNameTextBox.Text = reader[1].ToString();
                SoftWareVersionTextBox.Text = reader[2].ToString();
                DescriptionTextBox.Text = reader[3].ToString();
                TechnicalTaskTextBox.Text = reader[4].ToString();
                FunctionalTextBox.Text = reader[5].ToString();
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoOneDevTools(string idRegion)
        {
            DB db = new DB();
            string queryInfo = $"select * from devtools " +
            $"where id = {idRegion}";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                DevtoolsNameTextBox.Text = reader[1].ToString();
                DevToolsVersionTextBox.Text = reader[2].ToString();
                TypeTextBox.Text = reader[3].ToString();
            }
            reader.Close();

            db.closeConnection();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            addPanel.Visible = true;
            AddDevToolsButton.Text = "Изменить";
            AddSoftWareButton.Text = "Изменить";
            AddListDevTools.Text = "Изменить";

            switch (SoftwWareTab.SelectedIndex)
            {
                case 0:
                    loadInfoOneSoftware(SoftWareDataGrid[0, SoftWareDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    break;
                case 1:
                    loadInfoOneDevTools(DevToolsDataGrid[0, DevToolsDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    break;
                case 2:
                    loadInfoOneListDevTools(ListDevToolsDataGridView[0, ListDevToolsDataGridView.SelectedCells[0].RowIndex].Value.ToString());
                    break;
                default:
                    break;
            }
        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            addPanel.Visible = false;
        }

        private void guna2Button18_Click(object sender, EventArgs e)
        {
            addPanel.Visible = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            addPanel.Visible = false;
        }
        private void addListDevtoolsInDB()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"INSERT into listdevtools (idDevtools, idSoftWare) values(@idDevtools, @idSoftWare)", db.getConnection());
            command.Parameters.AddWithValue("@idDevtools", (DevToolsComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@idSoftWare", (SoftWareComboBox.SelectedItem as ComboBoxItem).Value);
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Список инструментов разработки добавлен");
                loadInfoListDevTools();
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DevToolsComboBox.SelectedIndex = -1;
            SoftWareComboBox.SelectedIndex = -1;
            db.closeConnection();
        }
        private void addSoftwareInDB()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"INSERT into software (name, version, description, technicalTask, functional) values(@name, @version, @description, @technicalTask, @functional)", db.getConnection());
            command.Parameters.AddWithValue("@name", SoftWareNameTextBox.Text);
            command.Parameters.AddWithValue("@version", SoftWareVersionTextBox.Text);
            command.Parameters.AddWithValue("@description", DescriptionTextBox.Text);
            command.Parameters.AddWithValue("@technicalTask", TechnicalTaskTextBox.Text);
            command.Parameters.AddWithValue("@functional", FunctionalTextBox.Text);
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("ПО добавлено");
                loadInfoSoftWare();
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SoftWareNameTextBox.Text = "";
            SoftWareVersionTextBox.Text = "";
            DescriptionTextBox.Text = "";
            TechnicalTaskTextBox.Text = "";
            FunctionalTextBox.Text = "";
            db.closeConnection();
        }
        private void addDevtoolsInDB()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"INSERT into devtools (name, version, typeSoftware) values(@name, @version, @typeSoftware)", db.getConnection());
            command.Parameters.AddWithValue("@name", DevtoolsNameTextBox.Text);
            command.Parameters.AddWithValue("@version", DevToolsVersionTextBox.Text);
            command.Parameters.AddWithValue("@typeSoftware", TypeTextBox.Text);
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Инструмент разработки добавлен");
                loadInfoDevTools();
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DevtoolsNameTextBox.Text = "";
            DevToolsVersionTextBox.Text = "";
            TypeTextBox.Text = "";
            db.closeConnection();
        }
        private void updateListDevtoolsInDB(string idRegion)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"update listdevtools set idDevtools=@idDevtools, idSoftWare = @idSoftWare where id = {idRegion}", db.getConnection());
            command.Parameters.AddWithValue("@idDevtools", (DevToolsComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@idSoftWare", (SoftWareComboBox.SelectedItem as ComboBoxItem).Value);

            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Список инструментов разработки изменен");
                loadInfoListDevTools();

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
        }
        private void updateSoftwareInDB(string idRegion)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"update software set name=@name, version=@version, description=@description, technicalTask=@technicalTask, functional=@functional where id = {idRegion}", db.getConnection());
            command.Parameters.AddWithValue("@name", SoftWareNameTextBox.Text);
            command.Parameters.AddWithValue("@version", SoftWareVersionTextBox.Text);
            command.Parameters.AddWithValue("@description", DescriptionTextBox.Text);
            command.Parameters.AddWithValue("@technicalTask", TechnicalTaskTextBox.Text);
            command.Parameters.AddWithValue("@functional", FunctionalTextBox.Text);

            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("ПО изменено");
                loadInfoSoftWare();

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
        }
        private void updateDevToolsInDB(string idRegion)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"update devtools set name=@name, version=@version, typeSoftware=@typeSoftware where id = {idRegion}", db.getConnection());
            command.Parameters.AddWithValue("@name", DevtoolsNameTextBox.Text);
            command.Parameters.AddWithValue("@version", DevToolsVersionTextBox.Text);
            command.Parameters.AddWithValue("@typeSoftware", TypeTextBox.Text);

            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Инструмент разработки изменен");
                loadInfoDevTools();

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
        }

        private void AddSoftWareButton_Click(object sender, EventArgs e)
        {
            if (AddSoftWareButton.Text == "Добавить")
                addSoftwareInDB();
            else
                updateSoftwareInDB(SoftWareDataGrid[0, SoftWareDataGrid.SelectedCells[0].RowIndex].Value.ToString());

            loadInfoSoftWare();
        }

        private void AddDevToolsButton_Click(object sender, EventArgs e)
        {
            if (AddDevToolsButton.Text == "Добавить")
                addDevtoolsInDB();
            else
                updateDevToolsInDB(DevToolsDataGrid[0, DevToolsDataGrid.SelectedCells[0].RowIndex].Value.ToString());

            loadInfoDevTools();
        }

        private void AddListDevTools_Click(object sender, EventArgs e)
        {
            if (AddListDevTools.Text == "Добавить")
                addListDevtoolsInDB();
            else
                updateListDevtoolsInDB(ListDevToolsDataGridView[0, ListDevToolsDataGridView.SelectedCells[0].RowIndex].Value.ToString());

            loadInfoDevTools();
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
            switch (SoftwWareTab.SelectedIndex)
            {
                case 0:
                    deleteRecordInBd("software", SoftWareDataGrid[0, SoftWareDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    loadInfoSoftWare();
                    break;
                case 1:
                    deleteRecordInBd("devtools", DevToolsDataGrid[0, DevToolsDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    loadInfoDevTools();
                    break;
                case 2:
                    deleteRecordInBd("listdevtools", ListDevToolsDataGridView[0, ListDevToolsDataGridView.SelectedCells[0].RowIndex].Value.ToString());
                    loadInfoListDevTools ();
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
            new Users().Show(); this.Close();
        }

        private void организацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Organizations().Show(); this.Close();
        }

        private void отделыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Locality().Show(); this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            new Autorization().Show(); this.Close();
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
    }
}

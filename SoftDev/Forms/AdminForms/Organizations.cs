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
    public partial class Organizations : Form
    {
        private Guna2Panel addPanel;
        public Organizations()
        {
            InitializeComponent();
        }
        private void loadInfoOrganizatioins()
        {
            DB db = new DB();

            OrganizationsDataGrid.Rows.Clear();

            string query = $"select organizations.id, organizations.name, locality.name, organizations.numberPhone, organizations.street, organizations.house, organizations.frame, organizations.office, organizations.email, organizations.fiodirector from organizations " +
                $"inner join locality on organizations.idLocality = locality.id";

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
                    OrganizationsDataGrid.Rows.Add(s);
            }
            db.closeConnection();
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
            AddOrgButton.Text = "Добавить";
        }
        private void loadInfoOneOrganization(string idRegion)
        {
            DB db = new DB();
            string queryInfo = $"select * from organizations " +
            $"where id = {idRegion}";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                NameTextBox.Text = reader[1].ToString();

                for (int i = 0; i < LocalityComboBox.Items.Count; i++)
                {
                    if (reader[2].ToString() != "")
                    {
                        if (Convert.ToInt32((LocalityComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader[2]))
                        {
                            LocalityComboBox.SelectedIndex = i;
                        }
                    }
                }

                NumberPhoneTextBox.Text = reader[3].ToString();
                StreetTextBox.Text = reader[4].ToString();
                HouseTextBox.Text = reader[5].ToString();
                FrameTextBox.Text = reader[6].ToString();
                OfficTextBox.Text = reader[7].ToString();
                emailTextBox.Text = reader[8].ToString();
                FIODirectorTextBox.Text = reader[9].ToString();
            }
            reader.Close();

            db.closeConnection();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            addPanel.Visible = true;
            AddOrgButton.Text = "Изменить";

            switch (OrganizationsTab.SelectedIndex)
            {
                case 0:
                    loadInfoOneOrganization(OrganizationsDataGrid[0, OrganizationsDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    break;
                default:
                    break;
            }
        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            addPanel.Visible = false;
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
        private void OrganizationsTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadInfoOrganizatioins();

            switch (OrganizationsTab.SelectedIndex)
            {
                case 0:
                    addPanel = guna2Panel5;
                    break;
                default:
                    break;
            }
        }

        private void Organizations_Load(object sender, EventArgs e)
        {
            addPanel = guna2Panel5;
            loadInfoOrganizatioins();
            loadInfoLocalityComboBox();
        }

        private void addOrganizationstInDB()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"INSERT into organizations (name, idLocality, numberPhone, street, house, frame, office, email, fiodirector) values(" +
                                                    $"@name, @idLocality, @numberPhone, @street, @house, @frame, @office, @email, @fiodirector)", db.getConnection());
            command.Parameters.AddWithValue("@name", NameTextBox.Text);
            command.Parameters.AddWithValue("@idLocality", (LocalityComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@numberPhone", NumberPhoneTextBox.Text);
            command.Parameters.AddWithValue("@street", StreetTextBox.Text);
            command.Parameters.AddWithValue("@house", HouseTextBox.Text);
            command.Parameters.AddWithValue("@frame", FrameTextBox.Text);
            command.Parameters.AddWithValue("@office", OfficTextBox.Text);
            command.Parameters.AddWithValue("@email", emailTextBox.Text);
            command.Parameters.AddWithValue("@fiodirector", FIODirectorTextBox.Text);
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Организация добавлена");
                loadInfoOrganizatioins();
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            NameTextBox.Text = "";
            LocalityComboBox.SelectedIndex = -1;
            StreetTextBox.Text = "";
            HouseTextBox.Text = "";
            FrameTextBox.Text = "";
            OfficTextBox.Text = "";
            emailTextBox.Text = "";
            db.closeConnection();
        }
        private void updateOrganizationsInDB(string idRegion)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"update organizations set name=@name, idLocality=@idLocality, numberPhone=@numberPhone, street=@street, house=@house, frame=@frame, office=@office, email=@email, fiodirector=@fiodirector where id = {idRegion}", db.getConnection());
            command.Parameters.AddWithValue("@name", NameTextBox.Text);
            command.Parameters.AddWithValue("@idLocality", (LocalityComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@numberPhone", NumberPhoneTextBox.Text);
            command.Parameters.AddWithValue("@street", StreetTextBox.Text);
            command.Parameters.AddWithValue("@house", HouseTextBox.Text);
            command.Parameters.AddWithValue("@frame", FrameTextBox.Text);
            command.Parameters.AddWithValue("@office", OfficTextBox.Text);
            command.Parameters.AddWithValue("@email", emailTextBox.Text);
            command.Parameters.AddWithValue("@fiodirector", FIODirectorTextBox.Text);

            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Организация изменена");
                loadInfoOrganizatioins();

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
        }

        private void AddOrgButton_Click(object sender, EventArgs e)
        {
            if (AddOrgButton.Text == "Добавить")
                addOrganizationstInDB();
            else
                updateOrganizationsInDB(OrganizationsDataGrid[0, OrganizationsDataGrid.SelectedCells[0].RowIndex].Value.ToString());

            loadInfoOrganizatioins();
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
            switch (OrganizationsTab.SelectedIndex)
            {
                case 0:
                    deleteRecordInBd("organizations", OrganizationsDataGrid[0, OrganizationsDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    loadInfoOrganizatioins();
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
    }
}

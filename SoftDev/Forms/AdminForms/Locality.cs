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
    public partial class Locality : Form
    {
        private Guna2Panel addPanel;
        public Locality()
        {
            InitializeComponent();
        }
        private void loadInfoAreaComboBox()
        {
            AreaComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT id, name FROM area";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                AreaComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoTypeLocalityComboBox()
        {
            TypeLocalityComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT id, name FROM typelocality";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                TypeLocalityComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoRegionComboBox()
        {
            RegionComboBox.Items.Clear();

            DB db = new DB();
            string queryInfo = $"SELECT id, name FROM region";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = $" {reader[1]}";
                item.Value = reader[0];
                RegionComboBox.Items.Add(item);
            }
            reader.Close();

            db.closeConnection();
        }
        private void LocalityTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadInfoAreaComboBox();
            loadInfoTypeLocalityComboBox();
            loadInfoRegionComboBox();
            switch (LocalityTab.SelectedIndex)
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
                case 3:
                    addPanel = guna2Panel4;
                    break;
                default:
                    break;
            }
        }
        private void loadInfoTypeLocality()
        {
            DB db = new DB();

            TypeLocalityGridView.Rows.Clear();

            string query = $"select * from typelocality ";

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
                    TypeLocalityGridView.Rows.Add(s);
            }
            db.closeConnection();
        }
        private void loadInfoArea()
        {
            DB db = new DB();

            AreaDataGrid.Rows.Clear();

            string query = $"select * from area ";

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
                    AreaDataGrid.Rows.Add(s);
            }
            db.closeConnection();
        }
        private void loadInfoRegion()
        {
            DB db = new DB();

            RegionDataGrid.Rows.Clear();

            string query = $"select region.id, region.name, area.name from region " +
                $"inner join area on region.idArea = area.id ";

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
                    RegionDataGrid.Rows.Add(s);
            }
            db.closeConnection();
        }
        private void loadInfoLocality()
        {
            DB db = new DB();

            LocalityDataGrid.Rows.Clear();

            string query = $"select locality.id, locality.name, typeLocality.name, region.name from locality " +
                $"inner join region on locality.idRegion = region.id " +
                $"inner join typelocality on locality.idTypeLocality = typelocality.id ";

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
                    LocalityDataGrid.Rows.Add(s);
            }
            db.closeConnection();
        }
        private void Locality_Load(object sender, EventArgs e)
        {
            addPanel = guna2Panel5;
            loadInfoTypeLocality();
            loadInfoArea();
            loadInfoRegion();
            loadInfoLocality();
            loadInfoAreaComboBox();
            loadInfoTypeLocalityComboBox();
            loadInfoRegionComboBox();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            addPanel.Visible = true;
            AddLocalityButton.Text = "Добавить";
            AddRegionButton.Text = "Добавить";
            AddAreaButton.Text = "Добавить";
            AddTypeLocalityButton.Text = "Добавить";
        }
        private void loadInfoOneLocality(string idRegion)
        {
            DB db = new DB();
            string queryInfo = $"select * from locality " +
            $"where id = {idRegion}";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                LocalityNameTextBox.Text = reader["name"].ToString();

                for (int i = 0; i < TypeLocalityComboBox.Items.Count; i++)
                {
                    if (reader["idTypeLocality"].ToString() != "")
                    {
                        if (Convert.ToInt32((TypeLocalityComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader["idTypeLocality"]))
                        {
                            TypeLocalityComboBox.SelectedIndex = i;
                        }
                    }
                }
                for (int i = 0; i < RegionComboBox.Items.Count; i++)
                {
                    if (reader["idRegion"].ToString() != "")
                    {
                        if (Convert.ToInt32((RegionComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader["idRegion"]))
                        {
                            RegionComboBox.SelectedIndex = i;
                        }
                    }
                }
            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoOneRegion(string idRegion)
        {
            DB db = new DB();
            string queryInfo = $"select * from region " +
            $"where id = {idRegion}";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                RegionNameTextBox.Text = reader["name"].ToString();

                for (int i = 0; i < AreaComboBox.Items.Count; i++)
                {
                    if (reader["idArea"].ToString() != "")
                    {
                        if (Convert.ToInt32((AreaComboBox.Items[i] as ComboBoxItem).Value) == Convert.ToInt32(reader["idArea"]))
                        {
                            AreaComboBox.SelectedIndex = i;
                        }
                    }
                }
                
            }
            reader.Close();

            db.closeConnection();
        }

        private void loadInfoOneArea(string idRegion)
        {
            DB db = new DB();
            string queryInfo = $"select * from area " +
            $"where id = {idRegion}";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                AreaNameTextBox.Text = reader["name"].ToString();


            }
            reader.Close();

            db.closeConnection();
        }
        private void loadInfoOneTypeLocality(string idRegion)
        {
            DB db = new DB();
            string queryInfo = $"select * from typelocality " +
            $"where id = {idRegion}";
            MySqlCommand mySqlCommand = new MySqlCommand(queryInfo, db.getConnection());

            db.openConnection();

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                TypeLocalityTextBox.Text = reader["name"].ToString();
            }
            reader.Close();

            db.closeConnection();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            addPanel.Visible = true;
            AddLocalityButton.Text = "Изменить";
            AddRegionButton.Text = "Изменить";
            AddAreaButton.Text = "Изменить";
            AddTypeLocalityButton.Text = "Изменить";

            switch (LocalityTab.SelectedIndex)
            {
                case 0:
                    loadInfoOneLocality(LocalityDataGrid[0, LocalityDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    break;
                case 1:
                    loadInfoOneRegion(RegionDataGrid[0, RegionDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    break;
                case 2:
                    loadInfoOneArea(AreaDataGrid[0, AreaDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    break;
                case 3:
                    loadInfoOneTypeLocality(TypeLocalityGridView[0, TypeLocalityGridView.SelectedCells[0].RowIndex].Value.ToString());
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

        private void guna2Button15_Click(object sender, EventArgs e)
        {
            addPanel.Visible = false;
        }

        private void guna2Button19_Click(object sender, EventArgs e)
        {
            addPanel.Visible = false;
        }
        private void addLocalityInDB()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"INSERT into locality (name, idTypeLocality, idRegion) values(@name, @idTypeLocality, @idRegion)", db.getConnection());
            command.Parameters.AddWithValue("@name", LocalityNameTextBox.Text);
            command.Parameters.AddWithValue("@idTypeLocality", (TypeLocalityComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@idRegion", (RegionComboBox.SelectedItem as ComboBoxItem).Value);
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Населенный пункт добавлен");
                loadInfoLocality();
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LocalityNameTextBox.Text = "";
            TypeLocalityComboBox.SelectedIndex = -1;
            RegionComboBox.SelectedIndex = -1;
            db.closeConnection();
        }
        private void addRegionInDB()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"INSERT into region (name, idArea) values(@name, @idArea)", db.getConnection());
            command.Parameters.AddWithValue("@name", RegionNameTextBox.Text);
            command.Parameters.AddWithValue("@idArea", (AreaComboBox.SelectedItem as ComboBoxItem).Value);
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Район добавлен");
                loadInfoLocality();
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            RegionNameTextBox.Text = "";
            AreaComboBox.SelectedIndex = -1;
            db.closeConnection();
        }
        private void addAreaInDB()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"INSERT into area (name) values(@name)", db.getConnection());
            command.Parameters.AddWithValue("@name", AreaNameTextBox.Text);
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Область добавлена");
                loadInfoLocality();
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            AreaNameTextBox.Text = "";
            db.closeConnection();
        }
        private void addTypeLocalityInDB()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"INSERT into typelocality (name) values(@name)", db.getConnection());
            command.Parameters.AddWithValue("@name", TypeLocalityTextBox.Text);
            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Тип населенного пункта добавлена");
                loadInfoLocality();
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            TypeLocalityTextBox.Text = "";
            db.closeConnection();
        }
        private void updateLocalityInDB(string idRegion)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"update locality set name=@name, idTypeLocality = @idTypeLocality, idRegion = @idRegion where id = {idRegion}", db.getConnection());
            command.Parameters.AddWithValue("@name", LocalityNameTextBox.Text);
            command.Parameters.AddWithValue("@idTypeLocality", (TypeLocalityComboBox.SelectedItem as ComboBoxItem).Value);
            command.Parameters.AddWithValue("@idRegion", (RegionComboBox.SelectedItem as ComboBoxItem).Value);

            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Населенный пункт изменен");
                loadInfoLocality();

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
        }
        private void updateRegionInDB(string idRegion)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"update region set name=@name, idArea = @idArea where id = {idRegion}", db.getConnection());
            command.Parameters.AddWithValue("@name", RegionNameTextBox.Text);
            command.Parameters.AddWithValue("@idArea", (AreaComboBox.SelectedItem as ComboBoxItem).Value);

            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Район изменен");
                loadInfoLocality();

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
        }
        private void updateAreaInDB(string idRegion)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"update area set name=@name where id = {idRegion}", db.getConnection());
            command.Parameters.AddWithValue("@name", AreaNameTextBox.Text);

            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Область изменена");
                loadInfoLocality();

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
        }
        private void updateTypeLocalityInDB(string idRegion)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"update typelocality set name=@name where id = {idRegion}", db.getConnection());
            command.Parameters.AddWithValue("@name", TypeLocalityTextBox.Text);

            db.openConnection();

            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Тип населенного пункта изменен");
                loadInfoLocality();

            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            db.closeConnection();
        }

        private void AddLocalityButton_Click(object sender, EventArgs e)
        {
            if (AddLocalityButton.Text == "Добавить")
                addLocalityInDB();
            else
                updateLocalityInDB(LocalityDataGrid[0, LocalityDataGrid.SelectedCells[0].RowIndex].Value.ToString());

            loadInfoLocality();
        }

        private void AddRegionButton_Click(object sender, EventArgs e)
        {
            if (AddRegionButton.Text == "Добавить")
                addRegionInDB();
            else
                updateRegionInDB(RegionDataGrid[0, RegionDataGrid.SelectedCells[0].RowIndex].Value.ToString());

            loadInfoRegion();
        }

        private void AddAreaButton_Click(object sender, EventArgs e)
        {
            if (AddAreaButton.Text == "Добавить")
                addAreaInDB();
            else
                updateAreaInDB(AreaDataGrid[0, AreaDataGrid.SelectedCells[0].RowIndex].Value.ToString());

            loadInfoArea();
        }

        private void AddTypeLocalityButton_Click(object sender, EventArgs e)
        {
            if (AddTypeLocalityButton.Text == "Добавить")
                addTypeLocalityInDB();
            else
                updateTypeLocalityInDB(TypeLocalityGridView[0, TypeLocalityGridView.SelectedCells[0].RowIndex].Value.ToString());

            loadInfoTypeLocality();
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
            switch (LocalityTab.SelectedIndex)
            {
                case 0:
                    deleteRecordInBd("locality", LocalityDataGrid[0, LocalityDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    loadInfoLocality();
                    break;
                case 1:
                    deleteRecordInBd("region", RegionDataGrid[0, RegionDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    loadInfoRegion();
                    break;
                case 2:
                    deleteRecordInBd("area", AreaDataGrid[0, AreaDataGrid.SelectedCells[0].RowIndex].Value.ToString());
                    loadInfoArea();
                    break;
                case 3:
                    deleteRecordInBd("typelocality", TypeLocalityGridView[0, TypeLocalityGridView.SelectedCells[0].RowIndex].Value.ToString());
                    loadInfoTypeLocality();
                    break;
                default:
                    break;
            }
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            new Main().Show();
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            new Employees().Show();
            this.Close();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            new Users().Show();
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            new Autorization().Show();
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

        private void отделыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Locality().Show();
            this.Close();
        }

        private void организацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Organizations().Show();
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
    }
}

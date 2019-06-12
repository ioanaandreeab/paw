using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace Profesor
{
    public partial class Form1 : Form
    {
        Profesor[] profesori = new Profesor[] { };
        const string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= C:\Users\ioana\Desktop\AN 2\Sem 2\PAW\subiecte\rezolvari_eu\Profesor\Profesor\bin\Debug\Profesori.mdb";
        const string providerName = @"System.Data.OleDb";
        bool check = false;
        public Form1()
        {
            InitializeComponent();
            DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connString;
                connection.Open();

                DbCommand selectCheck = connection.CreateCommand();
                selectCheck.CommandText = "SELECT COUNT(Marca) FROM tblProfesor";
                int result = (int)selectCheck.ExecuteScalar();
                if (result == 0)
                    check = true;
            }
            if (check == true) {
                InitializareProfesori();
                saveDataInDB(profesori);

            }
            else
                AfisareDateDinBd();
            AfisareDate(profesori);
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            dataGridView1.KeyDown += DataGridView1_KeyDown;
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.E)
            {
                Application.Exit();
            }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow randCurent = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
            Profesor curent = (Profesor)randCurent.Tag;
            foreach (Profesor profesor in profesori)
            {
                if (profesor.Marca == (int)randCurent.Cells[0].Value)
                {
                    profesor.Nume = randCurent.Cells[1].Value.ToString();
                    string salariu = randCurent.Cells[2].Value.ToString();
                    profesor.Salariu = double.Parse(salariu);
                    ModificareDate(profesori);
                    MessageBox.Show("Date modificate");
                }
            }
        }

        private void InitializareProfesori()
        {
            Profesor[] profesoriVect = new Profesor[]
            {
                new Profesor(1,"Popescu",1000),
                new Profesor(2,"Toma",1500),
                new Profesor(3,"Popa",700),
                new Profesor(4,"Marinescu",2500),
                new Profesor(5,"Covei",3500)
            };
            profesori = profesoriVect;
        }

        private void AfisareDate(Profesor[] profesori)
        {
            dataGridView1.Rows.Clear();
            foreach(Profesor profesor in profesori)
            {
                int row = dataGridView1.Rows.Add();
                dataGridView1.Rows[row].Cells[0].Value = profesor.Marca;
                dataGridView1.Rows[row].Cells[1].Value = profesor.Nume;
                dataGridView1.Rows[row].Cells[2].Value = profesor.Salariu;
                dataGridView1.Rows[row].Tag = profesor;
            }
        }
        private void saveDataInDB(Profesor[] profesori)
        {
            foreach (Profesor profesor in profesori) {
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
                using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connString;
                    connection.Open();

                    DbCommand insert = connection.CreateCommand();
                    insert.CommandText = "INSERT INTO tblProfesor VALUES(@marca,@nume,@salariu)";

                    DbParameter marca = insert.CreateParameter();
                    marca.DbType = System.Data.DbType.Int32;
                    insert.Parameters.Add(marca);

                    DbParameter nume = insert.CreateParameter();
                    nume.DbType = System.Data.DbType.String;
                    insert.Parameters.Add(nume);

                    DbParameter salariu = insert.CreateParameter();
                    salariu.DbType = System.Data.DbType.Double;
                    insert.Parameters.Add(salariu);

                    marca.Value = profesor.Marca;
                    nume.Value = profesor.Nume;
                    salariu.Value = profesor.Salariu;

                    insert.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Datele au fost introduse cu succes in baza de date");
        }

        private void ModificareDate(Profesor[] profesori)
        {
            foreach(Profesor profesor in profesori)
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
                using(DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connString;
                    connection.Open();

                    DbCommand update = connection.CreateCommand();
                    update.CommandText = "UPDATE tblProfesor SET Nume='" + profesor.Nume + "', Salariu='" + profesor.Salariu +
                        "' WHERE Marca=" + profesor.Marca;

                    update.ExecuteNonQuery();
                }
            }
        }

        private void AfisareDateDinBd()
        {
            List<Profesor> listaProfesori = new List<Profesor>();
            Profesor[] profesoriVect = new Profesor[] { };
            DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connString;
                connection.Open();

                DbCommand select = connection.CreateCommand();
                select.CommandText = "SELECT * FROM tblProfesor";
                using (DbDataReader reader = select.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int marca = reader.GetInt32(0);
                        string nume = reader.GetString(1);
                        double salariu = reader.GetDouble(2);
                        Profesor profesor = new Profesor(marca, nume, salariu);
                        listaProfesori.Add(profesor);
                    }
                }

                foreach (Profesor profesor in listaProfesori)
                {
                    if (profesoriVect.Length == 0)
                    {
                        Profesor[] nou = new Profesor[1] { profesor };
                        profesoriVect = nou;
                    }
                    else
                    {
                        Profesor[] nou = new Profesor[profesoriVect.Length + 1];
                        for (int i = 0; i < profesoriVect.Length; i++)
                        {
                            nou[i] = profesoriVect[i];
                        }
                        nou[profesoriVect.Length] = profesor;
                        profesoriVect = nou;
                    }
                }                
            }
            profesori = profesoriVect;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Title = "Salvati fisier XML?";
            sv.Filter = "XML-File | *.xml";
            if(sv.ShowDialog() == DialogResult.OK)
            {
                MemoryStream ms = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(ms, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("Profesori");
                foreach(Profesor profesor in profesori)
                {
                    writer.WriteStartElement(profesor.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();

                string xml = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();

                StreamWriter sw = new StreamWriter(sv.FileName);
                sw.WriteLine(xml);
                sw.Close();
                MessageBox.Show("Fisier XML salvat");
            }
        }

        private void imprimaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

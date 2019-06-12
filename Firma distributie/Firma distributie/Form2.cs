using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;

namespace Firma_distributie
{
    public partial class Form2 : Form
    {
        const string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\ioana\Desktop\AN 2\Sem 2\PAW\subiecte\rezolvari_eu\Firma distributie\Firma distributie\bin\Debug\Produse.mdb";
        const string providerName = @"System.Data.OleDb";
        private bool updating = false;
        private bool inserting = false;
        List<Furnizor> furnizori = new List<Furnizor>();
        Produs produs;

        public bool Updating { get => updating; set => updating = value; }
        public bool Inserting { get => inserting; set => inserting = value; }

        public Form2(Produs produs, bool updating, bool inserting)
        {
            this.updating = updating;
            this.inserting = inserting;
            InitializeComponent();
            PreluareFurnizori();
            if (updating)
            {
                this.produs = produs;
                textBox1.Text = produs.Id.ToString();
                textBox2.Text = produs.Nume;
                textBox3.Text = produs.Unitati.ToString();
                textBox4.Text = produs.Pret.ToString();
                comboBox1.Text = produs.FurnizorId.ToString();
            }

            foreach (Furnizor furnizor in furnizori)
            {
                comboBox1.Items.Add(furnizor.Id.ToString());
            }
        }

        private void PreluareFurnizori()
        {
            StreamReader sr = new StreamReader("Furnizori.txt");
            string line = null;
            while ((line = sr.ReadLine()) != null)
            {
                string id = line.Split(',')[0];
                string denumire = line.Split(',')[1];
                Furnizor furnizor = new Furnizor(int.Parse(id), denumire);
                furnizori.Add(furnizor);
            }
            sr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text)||
                string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Trebuie sa completati toate campurile");
                MessageBox.Show("Trebuie sa completati toate campurile");
            }
            else if (!(textBox4.Text.All(char.IsDigit)))
            {
                errorProvider1.SetError(textBox4, "Puteti introduce doar cifre");
                MessageBox.Show("Pretul poate fi format doar din cifre");
            }
            else {
                if (inserting == true)
                {
                    DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
                    using (DbConnection connection = factory.CreateConnection())
                    {
                        connection.ConnectionString = connString;
                        connection.Open();

                        DbCommand insert = connection.CreateCommand();
                        insert.CommandText = "INSERT INTO Produse VALUES(@Id,@Nume,@Unitati,@Pret,@FurnizorId)";

                        DbParameter paramId = insert.CreateParameter();
                        paramId.DbType = System.Data.DbType.Int32;
                        insert.Parameters.Add(paramId);

                        DbParameter paramNume = insert.CreateParameter();
                        paramNume.DbType = System.Data.DbType.String;
                        insert.Parameters.Add(paramNume);

                        DbParameter paramUnitati = insert.CreateParameter();
                        paramUnitati.DbType = System.Data.DbType.Int32;
                        insert.Parameters.Add(paramUnitati);

                        DbParameter paramPret = insert.CreateParameter();
                        paramPret.DbType = System.Data.DbType.Decimal;
                        insert.Parameters.Add(paramPret);

                        DbParameter paramFurnizor = insert.CreateParameter();
                        paramFurnizor.DbType = System.Data.DbType.String;
                        insert.Parameters.Add(paramFurnizor);

                        paramId.Value = int.Parse(textBox1.Text);
                        paramNume.Value = textBox2.Text;
                        paramUnitati.Value = int.Parse(textBox3.Text);
                        paramPret.Value = decimal.Parse(textBox4.Text);
                        paramFurnizor.Value = int.Parse(comboBox1.Text);

                        insert.ExecuteNonQuery();
                        MessageBox.Show("Ati introdus un produs in baza de date");
                        this.Close();
                    }
                }

                if (updating == true)
                {
                    DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
                    using (DbConnection connection = factory.CreateConnection())
                    {
                        connection.ConnectionString = connString;
                        connection.Open();

                        DbCommand update = connection.CreateCommand();
                        update.CommandText = "UPDATE Produse SET Nume='" + textBox2.Text + "', Unitati='" + int.Parse(textBox3.Text)
                            + "', Pret='" + decimal.Parse(textBox4.Text) + "', FurnizorId='" + int.Parse(comboBox1.Text) +
                            "' WHERE Id=" + int.Parse(textBox1.Text);
                        update.ExecuteNonQuery();
                    }
                    MessageBox.Show("Ati actualizat cu succes produsul!");
                    this.Close();
                }
            }
            
        }
    }
}

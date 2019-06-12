using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.Common;


namespace Firma_distributie
{
    public partial class Form1 : Form
    {
        const string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\ioana\Desktop\AN 2\Sem 2\PAW\subiecte\rezolvari_eu\Firma distributie\Firma distributie\bin\Debug\Produse.mdb";
        const string providerName = @"System.Data.OleDb";
        int nrFurnizor1;
        int nrFurnizor2;
        int nrFurnizor3;
        int nrFurnizor5;
        int nrFurnizor6;
        int nrFurnizor7;
        public Form1()
        {
            InitializeComponent();
            AfisareProduse();
            listView1.View = View.Details;
            listView1.Columns.Add("Id", 40,HorizontalAlignment.Center);
            listView1.Columns.Add("Nume", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Unitati", 70, HorizontalAlignment.Center);
            listView1.Columns.Add("Pret", 70, HorizontalAlignment.Center);
            listView1.Columns.Add("Id furnizor", 70, HorizontalAlignment.Center);
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;
        }

        private void AfisareProduse()
        {
            List<Produs> produse = new List<Produs>();
            DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
            using(DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connString;
                connection.Open();

                DbCommand cmdSelect = connection.CreateCommand();
                cmdSelect.CommandText = "SELECT * FROM Produse";
                using(DbDataReader reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Produs produs = new Produs(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetInt32(2),
                            reader.GetDecimal(3),
                            reader.GetInt32(4));
                        produse.Add(produs);
                    }
                }
            }

            produse.Sort();
            foreach (Produs produs in produse)
            {
                ListViewItem rand = new ListViewItem();
                rand.Text = produs.Id.ToString();
                rand.SubItems.Add(produs.Nume);
                rand.SubItems.Add(produs.Unitati.ToString());
                rand.SubItems.Add(produs.Pret.ToString());
                rand.SubItems.Add(produs.FurnizorId.ToString());
                rand.Tag = produs;
                listView1.Items.Add(rand);
            }

            //filtrare
            foreach (Produs produs in produse.Where(x => x.FurnizorId == 1))
            {
                nrFurnizor1++;
            }
            foreach (Produs produs in produse.Where(x => x.FurnizorId == 2))
            {
                nrFurnizor2++;
            }
            foreach (Produs produs in produse.Where(x => x.FurnizorId == 3))
            {
                nrFurnizor3++;
            }
            foreach (Produs produs in produse.Where(x => x.FurnizorId == 5))
            {
                nrFurnizor5++;
            }
            foreach (Produs produs in produse.Where(x => x.FurnizorId == 6))
            {
                nrFurnizor6++;
            }
            foreach (Produs produs in produse.Where(x => x.FurnizorId == 7))
            {
                nrFurnizor7++;
            }
        }

        private void adaugaProdusToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 formular = new Form2(new Produs(),false,true);
            formular.Inserting = true;
            formular.ShowDialog();
            listView1.Items.Clear();
            AfisareProduse();
        }

        private void stergereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0)
            {
                ListViewItem rand = listView1.SelectedItems[0];
                Produs produs = (Produs)rand.Tag;

                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
                using(DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connString;
                    connection.Open();
                    DbCommand delete = connection.CreateCommand();
                    delete.CommandText = "DELETE FROM Produse WHERE Id=" + produs.Id;
                    delete.ExecuteNonQuery();
                }
                listView1.Items.RemoveAt(rand.Index);
            }
        }

        private void modificareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0)
            {
                ListViewItem rand = listView1.SelectedItems[0];
                Produs produs = (Produs)rand.Tag;
                Form2 formular = new Form2(produs,true,false);
                formular.ShowDialog();
                listView1.Items.Clear();
                AfisareProduse();
            }
        }

        private void aflaValoareTotalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem rand = listView1.SelectedItems[0];
                Produs produs = (Produs)rand.Tag;
                MessageBox.Show(string.Format("Valoarea totala a produsului selectat este de: {0} ron ", (decimal)produs));
            }
        }

        private void vizualizatiGraficToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] Data = new int[] { nrFurnizor1, nrFurnizor2, nrFurnizor3, nrFurnizor5, nrFurnizor6, nrFurnizor7 };
            Form3 formular = new Form3(Data);
            formular.ShowDialog();
        }
    }
}

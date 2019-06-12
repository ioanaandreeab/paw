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
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Printing;

namespace Smartphones_producatori
{
    public partial class Form1 : Form
    {
        List<Smartphone> smartphones = new List<Smartphone>();
        List<Producator> producatori = new List<Producator>();
        public Form1(List<Producator> producatori)
        {
            this.producatori = producatori;
            InitializeComponent();
            InitializareProducatori();
            AfisareProducatori(producatori);
            PreluareDate();
            listView1.View = View.Details;
        }

        private void PreluareDate()
        {
            StreamReader sr = new StreamReader("Smartphones.txt");
            string line = null;
            while((line=sr.ReadLine())!=null)
            {
                try
                {
                    int id = int.Parse(line.Split(',')[0]);
                    string model = line.Split(',')[1];
                    int stocDisp = int.Parse(line.Split(',')[2]);
                    float pret = float.Parse(line.Split(',')[3]);
                    string dataAparitie = line.Split(',')[4];
                    int idProducator = int.Parse(line.Split(',')[5]);
                    Smartphone s = new Smartphone(id, model, stocDisp, pret, dataAparitie, idProducator);
                    smartphones.Add(s);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            MessageBox.Show("Date despre smartphones incarcate cu succes");
            sr.Close();

            listView1.Columns.Add("Id", 25);
            listView1.Columns.Add("Model", 80);
            listView1.Columns.Add("Stoc", 50);
            listView1.Columns.Add("Pret", 50);
            listView1.Columns.Add("Data aparitiei", 100);
            listView1.Columns.Add("IdProd", 50);

            foreach (Smartphone s in smartphones)
            {
                ListViewItem rand = new ListViewItem();
                rand.Text = s.Id.ToString();
                rand.SubItems.Add(s.Model);
                rand.SubItems.Add(s.StocDisp.ToString());
                rand.SubItems.Add(s.Pret.ToString());
                rand.SubItems.Add(s.DataAparitie);
                rand.SubItems.Add(s.IdProducator.ToString());
                listView1.Items.Add(rand);
            }
        }

        private void InitializareProducatori()
        {
            Producator p1 = new Producator(387, "LG");
            Producator p2 = new Producator(250, "Apple");
            Producator p3 = new Producator(125, "Huawei");
            Producator p4 = new Producator(58, "Google");
            Producator p5 = new Producator(410, "Oneplus");
            producatori.Add(p1);
            producatori.Add(p2);
            producatori.Add(p3);
            producatori.Add(p4);
            producatori.Add(p5);
        }

        private void AfisareProducatori(List<Producator> producatori)
        {
            dataGridView1.Rows.Clear();
            foreach (Producator producator in producatori)
            {
                int row = dataGridView1.Rows.Add();
                dataGridView1.Rows[row].Cells["Id"].Value = producator.Id;
                dataGridView1.Rows[row].Cells["Denumire"].Value = producator.Denumire;
                dataGridView1.Rows[row].Tag = producator;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 formular = new Form2(new Producator());
            formular.ShowDialog();
            producatori.Add(formular.Rezultat);
            AfisareProducatori(producatori);
        }

        private void modificaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                Producator prod = (Producator)selectedRow.Tag;
                Form2 formular = new Form2(prod);
                formular.ShowDialog();
                AfisareProducatori(producatori);
            }
        }

        private void stergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            smartphones.Sort();
            StreamWriter sw = new StreamWriter("smartphones_fisier.txt");
            foreach(Smartphone s in smartphones)
            {
                sw.WriteLine(s.ToString());
            }
            sw.Close();
            MessageBox.Show("Lista cu smartphones a fost salvata intr-un fisier text!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileStream fs1 = new FileStream("smartphones_serialized.dat", FileMode.Create,FileAccess.Write);
            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(fs1, smartphones);

            FileStream fs2 = new FileStream("producers_serialized.dat", FileMode.Create, FileAccess.Write);
            BinaryFormatter bf2 = new BinaryFormatter();
            bf2.Serialize(fs2, producatori);
            fs1.Close();
            fs2.Close();
            MessageBox.Show("Cele doua liste au fost serializate!");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int total = 0;
            foreach(Smartphone s in smartphones)
            {
                total += (int)s;
            }
            MessageBox.Show(string.Format("Stocul total este de {0}",total));
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PageSettings pgs = new PageSettings();
            pageSetupDialog1.PageSettings = pgs;
            if(pageSetupDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.DefaultPageSettings = pageSetupDialog1.PageSettings;
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(dataGridView1.Width, dataGridView1.Height, dataGridView1.CreateGraphics());
            dataGridView1.DrawToBitmap(bmp, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
            RectangleF bounds = e.PageSettings.PrintableArea;
            float factor = ((float)bmp.Height / (float)bmp.Width);
            e.Graphics.DrawImage(bmp, bounds.Left, bounds.Top, bounds.Width, bounds.Width*factor);
        }
    }
}

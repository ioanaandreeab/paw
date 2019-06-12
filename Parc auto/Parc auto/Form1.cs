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

namespace Parc_auto
{
    public partial class Form1 : Form
    {
        List<Vehicul> lstParc;
        public Form1()
        {
            InitializeComponent();
            InitializareDate();

            listView1.View = View.Details;
            listView1.MultiSelect = false;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Cod", 40, HorizontalAlignment.Center);
            listView1.Columns.Add("Denumire", 100,HorizontalAlignment.Center);
            listView1.Columns.Add("Tonaj", 40, HorizontalAlignment.Center);
            Afisare();
            listView1.DoubleClick += ListView1_DoubleClick;

        }

        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem rand = listView1.SelectedItems[0];
            Vehicul curent = (Vehicul)rand.Tag;
            Form2 formular = new Form2(curent,lstParc);
            formular.ShowDialog();
            for (int i = 0; i < lstParc.Count; i++)
            {
                lstParc[i].Cod = formular.vehicule[i].Cod;
                lstParc[i].Denumire = formular.vehicule[i].Denumire;
                lstParc[i].Tonaj = formular.vehicule[i].Tonaj;

            }
            Afisare();
        }

        private void InitializareDate()
        {
            List<Vehicul> vehicule = new List<Vehicul>();
            Vehicul v1 = new Vehicul(1, "Panamera", 4);
            Vehicul v2 = new Vehicul(2, "BMW", 3);
            Vehicul v3 = new Vehicul(3, "Audi", 1);
            Vehicul v4 = new Vehicul(4, "Subaru", 5);
            vehicule.Add(v1);
            vehicule.Add(v2);
            vehicule.Add(v3);
            vehicule.Add(v4);
            lstParc = vehicule;
        }

        private void Afisare()
        {
            listView1.Items.Clear();
            foreach (Vehicul vehicul in lstParc)
            {
                ListViewItem rand = new ListViewItem();
                rand.Text = vehicul.Cod.ToString();
                rand.SubItems.Add(vehicul.Denumire);
                rand.SubItems.Add(vehicul.Tonaj.ToString());
                rand.Tag = vehicul;
                listView1.Items.Add(rand);
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("vehicule.dat", FileMode.Create, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, lstParc);
            fs.Close();
            MessageBox.Show("Serializare efectuata cu succes");
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("vehicule.dat", FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            List<Vehicul> vehicule = (List<Vehicul>)bf.Deserialize(fs);
            fs.Close();
            //verificare deserializare
            for (int i = 0; i < lstParc.Count; i++)
            {
                lstParc[i].Cod = vehicule[i].Cod;
                lstParc[i].Denumire = vehicule[i].Denumire;
                lstParc[i].Tonaj = vehicule[i].Tonaj;

            }
            Afisare();
            MessageBox.Show("Deserializare efectuata cu succes");
        }

        private void tonajMinimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstParc.Sort();
            MessageBox.Show(lstParc[0].ToString());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            List<float> tonaje = new List<float>();
            foreach (Vehicul vehicul in lstParc)
            {
                tonaje.Add(vehicul.Tonaj);
            }

            //luam contextul de desenare
            Graphics g = e.Graphics;
            //luam aria de desenare
            Rectangle rectangle = e.ClipRectangle;
            //aflam latimea totala
            var Width = rectangle.Width;
            //latimea unui bar
            var barWidth = rectangle.Width / tonaje.Count;
            //aflam inaltimea maxima
            var maxHeight = rectangle.Height * 0.9;
            //factorul de scalare
            var scalingFactor = maxHeight / tonaje.Max();

            Brush[] brushes = new Brush[] { new SolidBrush(Color.MediumVioletRed), new SolidBrush(Color.CornflowerBlue),
            new SolidBrush(Color.Navy), new SolidBrush(Color.Olive)};

            for(int i = 0; i < tonaje.Count; i++)
            {
                var barHeight = tonaje[i] * scalingFactor;
                g.FillRectangle(
                    brushes[i],
                    i*barWidth,
                    (float)(rectangle.Height-barHeight),
                    (float)0.8*barWidth,
                    (float)barHeight);
            }

        }
    }
}

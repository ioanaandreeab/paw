using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unitate_turistica
{
    public partial class Form1 : Form
    {
        List<Camera> camere;
        int nrOcupate;
        int nrLibere;
        public Form1(List<Camera> camere)
        {
            this.camere = camere;
            InitializeComponent();
            listView1.View = View.Details;
            listView1.MultiSelect = false;
            listView1.FullRowSelect = true;
            listView1.DoubleClick += ListView1_DoubleClick;
            Application.ApplicationExit += Application_ApplicationExit;
            getNr();

            Afisare(camere);
        }

        private void getNr()
        {
            foreach(Camera camera in camere)
            {
                if(camera.EsteOcupata==true)
                {
                    nrOcupate++;
                }
                else
                {
                    nrLibere++;
                }
            }
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("camere.dat", FileMode.Create, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, camere);
            fs.Close();
        }

        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem rand = listView1.SelectedItems[0];
                Camera camera = (Camera)rand.Tag;
                Form2 formular = new Form2(camera);
                formular.ShowDialog();
                Afisare(camere);
            }
        }

        private void Afisare(List<Camera> camere)
        {
            listView1.Items.Clear();
            camere.Sort();
            foreach (Camera camera in camere.Where(x => x.EsteOcupata == false))
            {
                ListViewItem rand = new ListViewItem();
                rand.Text = camera.NrCamera.ToString();
                rand.SubItems.Add(camera.TipCamera.ToString());
                rand.SubItems.Add(camera.EsteOcupata.ToString());
                rand.SubItems.Add(camera.DataCazare.ToString());
                rand.Tag = camera;
                listView1.Items.Add(rand);
            }
        }

        private void culoareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                listView1.SelectedItems[0].BackColor = dlg.Color;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("camere.dat", FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            List<Camera> camere = (List<Camera>)bf.Deserialize(fs);
            Afisare(camere);
            fs.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            float[] Data = new float[] { nrOcupate, nrLibere};
            //luam contextul de desenare
            Graphics graphics = e.Graphics;
            //luam aria de desenare -> care e un patrat
            Rectangle clipRectangle = e.ClipRectangle;

            //determinam latimea unui bar din grafic
            var barWidth = clipRectangle.Width / Data.Length; //latimea patratului care e contextul de desenare impartita la nr de elem din vectorul valori
            //calculam valoarea maxima a inaltimii unui bar, care e *0.9 pentru ca mai trb sa lasam putin spatiu sus
            var maxBarHeight = clipRectangle.Height * 0.9;
            //calculam factorul de scalare in functie de maximul ce poate fi reprezentat
            var scalingFactor = maxBarHeight / Data.Max(); //inaltimea maxima supra valoarea maxima din vectorul de valori

            Brush[] brushes = new Brush[] { new SolidBrush(Color.DarkRed), new SolidBrush(Color.DarkGreen) }; //luam un brush pentru desenare

            for (int i = 0; i < Data.Length; i++) //desenam toate valorile din vector
            {
                var barHeight = Data[i] * scalingFactor; //inaltimea este valoarea * factorul de scalare

                graphics.FillRectangle(
                    brushes[i], //folosim red brush pentru asta, aici puteam sa avem si un vector de brushes
                    i * barWidth, //x - incepe dupa fiecare bar de pana acum - de unde si indice * width-ul unui bar
                    (float)(clipRectangle.Height - barHeight), //y - inaltimea patratului - inaltimea barului curent, adica seteaza punctul la care se opreste barul curent in inaltime
                    (float)(0.8 * barWidth), //width - e *0.8 ca sa lase spatiu intre ele
                    (float)barHeight); //height - inaltimea unui bar e lasata la fel si se duce pana la y
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parc_auto
{
    public partial class Form2 : Form
    {
        Vehicul primit;
        public List<Vehicul> vehicule = new List<Vehicul>();
        int indexCurent;

        public Form2(Vehicul primit, List<Vehicul> vehicule)
        {
            InitializeComponent();
            this.primit = primit;
            this.vehicule = vehicule;
            textBox1.Text = primit.Cod.ToString();
            textBox2.Text = primit.Denumire;
            textBox3.Text = primit.Tonaj.ToString();
            textBox1.KeyDown += TextBox1_KeyDown;
            //aflu index curent
            for (int i = 0; i < vehicule.Count; i++)
            {
                if (vehicule[i].Equals(primit))
                    indexCurent = i;
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (indexCurent + 1 < vehicule.Count)
                {
                    //salvez modificari
                    vehicule[indexCurent].Cod = int.Parse(textBox1.Text);
                    vehicule[indexCurent].Denumire = textBox2.Text;
                    vehicule[indexCurent].Tonaj = int.Parse(textBox3.Text);
                    //afisez urm vehicul
                    textBox1.Text = vehicule[indexCurent + 1].Cod.ToString();
                    textBox2.Text = vehicule[indexCurent + 1].Denumire;
                    textBox3.Text = vehicule[indexCurent + 1].Tonaj.ToString();
                    //modific index curent
                    indexCurent = indexCurent + 1;
                }
            }
            if(e.KeyCode == Keys.Up)
            {
                if (indexCurent > 0)
                {
                    //salvez modificari
                    vehicule[indexCurent].Cod = int.Parse(textBox1.Text);
                    vehicule[indexCurent].Denumire = textBox2.Text;
                    vehicule[indexCurent].Tonaj = int.Parse(textBox3.Text);
                    //afisez vehicul prec
                    textBox1.Text = vehicule[indexCurent - 1].Cod.ToString();
                    textBox2.Text = vehicule[indexCurent - 1].Denumire;
                    textBox3.Text = vehicule[indexCurent - 1].Tonaj.ToString();
                    //modific index curent
                    indexCurent = indexCurent - 1;
                }
            }

        }
    }
}

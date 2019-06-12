using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smartphones_producatori
{
    public partial class Form2 : Form
    {
        Producator prod;
        public Form2(Producator prod)
        {
            this.prod = prod;
            InitializeComponent();
            initializareForm();
        }

        private Producator rezultat;

        public Producator Rezultat { get => rezultat; set => rezultat = value; }
        
        private void initializareForm()
        {
            textBox1.Text = prod.Id.ToString();
            textBox2.Text = prod.Denumire;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Introduceti o valoare");
            }
            else if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Introduceti o valoare");
            }
            else
            {
                prod.Id = int.Parse(textBox1.Text);
                prod.Denumire = textBox2.Text;
                Producator nou = new Producator(int.Parse(textBox1.Text), textBox2.Text);
                this.rezultat = nou;
                this.Close();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unitate_turistica
{
    public partial class Form2 : Form
    {
        Camera camera;
        public Form2(Camera camera)
        {
            InitializeComponent();
            this.camera = camera;
            textBox1.Text = camera.NrCamera.ToString();
            textBox2.Text = camera.TipCamera.ToString();
            textBox3.Text = camera.EsteOcupata.ToString();
            textBox4.Text = camera.DataCazare.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Introduceti o valoare");
            }
            else if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Introduceti o valoare");
            }
            else if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Introduceti o valoare");
            }
            else if (string.IsNullOrWhiteSpace(textBox4.Text)) {
                errorProvider1.SetError(textBox4, "Introduceti o valoare");
            }
            else
            {
                camera.NrCamera = int.Parse(textBox1.Text);
                camera.TipCamera = char.Parse(textBox2.Text);
                camera.EsteOcupata = bool.Parse(textBox3.Text);
                camera.DataCazare = textBox4.Text;
                this.Close();
            }
        }
    }
}

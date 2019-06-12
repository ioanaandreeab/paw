using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smartphones
{
    public partial class Form2 : Form
    {
        const string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= C:\Users\ioana\Desktop\AN 2\Sem 2\PAW\subiecte\rezolvari_eu\Smartphones\Smartphones\bin\Debug\Devices.mdb";
        const string providerName = @"System.Data.OleDb";
        Device rezultat;
        int codMax = 0;

        public Device Rezultat { get => rezultat; set => rezultat = value; }

        public Form2()
        {
            InitializeComponent();
            DeterminareMax();
            comboBox1.Items.Add("Smartphone");
            comboBox1.Items.Add("Tablet");
        }

        private void DeterminareMax()
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
            using(DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connString;
                connection.Open();

                DbCommand selectMax = connection.CreateCommand();
                selectMax.CommandText = "SELECT MAX(cod) FROM Devices";

                codMax = (int)selectMax.ExecuteScalar();
            }
            textBox1.Text = (codMax + 1).ToString();
            textBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("Smartphone"))
            {
                Smartphone s = new Smartphone(int.Parse(textBox1.Text), textBox2.Text,
                    textBox3.Text, int.Parse(textBox4.Text), int.Parse(textBox5.Text),
                    textBox6.Text, float.Parse(textBox7.Text), bool.Parse(textBox9.Text), int.Parse(textBox8.Text));
                Rezultat = s;
            }
            else
            {
               Tablet t = new Tablet(int.Parse(textBox1.Text), textBox2.Text,
                    textBox3.Text, int.Parse(textBox4.Text), int.Parse(textBox5.Text),
                    textBox6.Text, float.Parse(textBox7.Text), bool.Parse(textBox9.Text), int.Parse(textBox8.Text));
                Rezultat = t;
            }

            MessageBox.Show("Ati adaugat cu succes un device!");
        }
    }
}

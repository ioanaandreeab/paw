using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cabinet_medical
{
    public partial class Form2 : Form
    {
        const string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= C:\Users\ioana\Desktop\AN 2\Sem 2\PAW\subiecte\rezolvari_eu\Cabinet medical\Cabinet medical\bin\Debug\Pacienti.mdb";
        const string ProviderName = @"System.Data.OleDb";
        Pacient pacient;
        public Form2(Pacient pacient)
        {
            this.pacient = pacient;
            InitializeComponent();
            AfisareDate();
        }
        private void AfisareDate()
        {
            textBox1.Text = pacient.IdPacient.ToString();
            textBox2.Text = pacient.NumePacient;
            textBox3.Text = pacient.IdMedic.ToString();
            textBox4.Text = pacient.DataProgramare;
            textBox5.Text = pacient.OraProgramare;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                errorProvider1.SetError(textBox1, "Introduceti o valoare");
            else if(string.IsNullOrWhiteSpace(textBox2.Text))
                errorProvider1.SetError(textBox2, "Introduceti o valoare");
            else if (string.IsNullOrWhiteSpace(textBox3.Text))
                errorProvider1.SetError(textBox3, "Introduceti o valoare");
            else if (string.IsNullOrWhiteSpace(textBox4.Text))
                errorProvider1.SetError(textBox4, "Introduceti o valoare");
            else if (string.IsNullOrWhiteSpace(textBox5.Text))
                errorProvider1.SetError(textBox5, "Introduceti o valoare");
            else
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);
                using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();
                    DbCommand update = connection.CreateCommand();
                    update.CommandText = "UPDATE Pacienti set nume='" + textBox2.Text + "', idMedic='"
                    + int.Parse(textBox3.Text) + "', dataProgramare='" + textBox4.Text +
                    "', oraProgramare='" + textBox5.Text + "' where idPacient=" + int.Parse(textBox1.Text);
                    update.ExecuteNonQuery();
                }
                MessageBox.Show("Ati actualizat angajatul");
                this.Close();
            }
        }
    }
}

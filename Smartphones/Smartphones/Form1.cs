using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using System.IO;
using System.Xml;

namespace Smartphones
{
    public partial class Form1 : Form
    {
        const string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= C:\Users\ioana\Desktop\AN 2\Sem 2\PAW\subiecte\rezolvari_eu\Smartphones\Smartphones\bin\Debug\Devices.mdb";
        const string providerName = @"System.Data.OleDb";
        ArrayList devices = new ArrayList();
        ArrayList rezultate = new ArrayList();
        
        public Form1()
        {
            InitializeComponent();
            AdaugareDate();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;
            listView1.Columns.Add("Cod produs", 60);
            listView1.Columns.Add("Denumire", 100);
            listView1.Columns.Add("Dimensiuni", 80);
            listView1.Columns.Add("Procesor", 70);
            listView1.Columns.Add("Nuclee", 50);
            listView1.Columns.Add("SO", 50);
            listView1.Columns.Add("Pret", 50);
            listView1.Columns.Add("Activ", 40);
            listView1.Columns.Add("SIM/USB", 80);
            Afisare();
            Application.ApplicationExit += Application_ApplicationExit;
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (rezultate.Count != 0)
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
                using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connString;
                    connection.Open();

                    DbCommand insert = connection.CreateCommand();
                    insert.CommandText = "INSERT INTO Devices VALUES(@cod,@denumire,@dimensiuni,@frecvproc,@nrcore,@so,@pret,@activ,@sim_usb)";

                    DbParameter cod = insert.CreateParameter();
                    cod.DbType = System.Data.DbType.Int32;
                    insert.Parameters.Add(cod);

                    DbParameter denumire = insert.CreateParameter();
                    denumire.DbType = System.Data.DbType.String;
                    insert.Parameters.Add(denumire);

                    DbParameter dimensiuni = insert.CreateParameter();
                    dimensiuni.DbType = System.Data.DbType.String;
                    insert.Parameters.Add(dimensiuni);

                    DbParameter frecvproc = insert.CreateParameter();
                    frecvproc.DbType = System.Data.DbType.Int32;
                    insert.Parameters.Add(frecvproc);

                    DbParameter nrcore = insert.CreateParameter();
                    nrcore.DbType = System.Data.DbType.Int32;
                    insert.Parameters.Add(nrcore);

                    DbParameter so = insert.CreateParameter();
                    so.DbType = System.Data.DbType.String;
                    insert.Parameters.Add(so);

                    DbParameter pret = insert.CreateParameter();
                    pret.DbType = System.Data.DbType.Int32;
                    insert.Parameters.Add(pret);

                    DbParameter activ = insert.CreateParameter();
                    activ.DbType = System.Data.DbType.Boolean;
                    insert.Parameters.Add(activ);

                    DbParameter sim_usb = insert.CreateParameter();
                    sim_usb.DbType = System.Data.DbType.Int32;
                    insert.Parameters.Add(sim_usb);

                    foreach (Device device in rezultate)
                    {
                        if (device is Smartphone)
                        {
                            Smartphone s = (Smartphone)device;
                            cod.Value = s.Cod;
                            denumire.Value = s.Denumire;
                            dimensiuni.Value = s.Dimensiuni;
                            frecvproc.Value = s.Frecvproc;
                            nrcore.Value = s.Nrcore;
                            so.Value = s.Sistemop;
                            pret.Value = s.Pret;
                            activ.Value = s.Activ;
                            sim_usb.Value = s.Nrsim;
                            insert.ExecuteNonQuery();
                        }
                        else
                        {
                            Tablet t = (Tablet)device;
                            cod.Value = t.Cod;
                            denumire.Value = t.Denumire;
                            dimensiuni.Value = t.Dimensiuni;
                            frecvproc.Value = t.Frecvproc;
                            nrcore.Value = t.Nrcore;
                            so.Value = t.Sistemop;
                            pret.Value = t.Pret;
                            activ.Value = t.Activ;
                            sim_usb.Value = t.Nrusb;
                            insert.ExecuteNonQuery();
                        }
                    }
                }
                MessageBox.Show("Datele au fost salvate cu succes!");
            }
        }

        private void AdaugareDate()
        {
            Smartphone s1 = new Smartphone(1, "Samsung S10", "150g", 2, 4, "Android", 4000, true, 2457);
            Smartphone s2 = new Smartphone(2, "Huawei P20", "170g", 3, 5, "EMUI", 3000, true, 7827);
            Tablet t1 = new Tablet(3, "IPad pro", "250 g", 3, 5, "iOS", 2500, false, 3);
            Tablet t2 = new Tablet(4, "Samsung pad", "220 g", 4, 6, "Android", 2000, true, 3);
            devices.Add(s1);
            devices.Add(s2);
            devices.Add(t1);
            devices.Add(t2);
        }

        private void Afisare()
        {
            listView1.Items.Clear();

            foreach (Device device in devices)
            {
                listView1.Items.Add(device.Afisare());
            }
            foreach (Device device in rezultate)
            {
                listView1.Items.Add(device.Afisare());
            }
        }

        private void adaugaDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 formular = new Form2();
            formular.ShowDialog();
            if(formular.Rezultat!=null)
                rezultate.Add(formular.Rezultat);
            Afisare();
        }

        private void stergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Stergeti device-ul?");
            if(res == DialogResult.OK)
            {
                ListViewItem rand = listView1.SelectedItems[0];
                listView1.Items.Remove(rand);
                Device dev = (Device)rand.Tag;
                devices.Remove(dev);
            }
            
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                stergeToolStripMenuItem.Visible = true;
            }
            else stergeToolStripMenuItem.Visible = false;
        }

        private void exportaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(ms, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("Devices");
            foreach (Device device in devices)
            {
                writer.WriteStartElement(device.ToString());
                writer.WriteEndElement();
            }
            foreach (Device device in rezultate)
            {
                writer.WriteStartElement(device.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();

            string xml = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();

            StreamWriter sw = new StreamWriter("fisier.xml");
            sw.WriteLine(xml);
            sw.Close();
            MessageBox.Show("Fisier xml scris");
        }
    }
}

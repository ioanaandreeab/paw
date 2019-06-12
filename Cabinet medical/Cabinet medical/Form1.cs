using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Cabinet_medical
{
    public partial class Form1 : Form
    {
        const string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= C:\Users\ioana\Desktop\AN 2\Sem 2\PAW\subiecte\rezolvari_eu\Cabinet medical\Cabinet medical\bin\Debug\Pacienti.mdb";
        const string ProviderName = @"System.Data.OleDb";
        List<Medic> medici;
        List<Pacient> pacienti = new List<Pacient>();
        int nrMedic1;
        int nrMedic2;
        int nrMedic3;
        int nrMedic4;
        public Form1(List<Medic> medici)
        {
            this.medici = medici;
            InitializeComponent();
            PreluareDate();
            treeView1.DoubleClick += TreeView1_DoubleClick;
            Application.ApplicationExit += Application_ApplicationExit;
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(memoryStream, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;

            writer.WriteStartDocument();
            writer.WriteStartElement("Medici");
            foreach (TreeNode parinte in treeView1.Nodes)
            {
                writer.WriteStartElement(parinte.Text);
                foreach(TreeNode copil in parinte.Nodes)
                {
                    writer.WriteStartElement(copil.Text);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();

            string xml = Encoding.UTF8.GetString(memoryStream.ToArray());
            memoryStream.Close();

            StreamWriter sw = new StreamWriter("fisier.xml");
            sw.WriteLine(xml);
            sw.Close();
            MessageBox.Show("Fisier xml scris");
        }

        private void TreeView1_DoubleClick(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            string idMedic = node.Parent.Text.Split('-')[1];
            string idPacient = node.Text.Split(',')[0];
            string numePacient = node.Text.Split(',')[1];
            string dataProgramare = node.Text.Split(',')[2];
            string oraProgramare = node.Text.Split(',')[3];
            Pacient pacient = new Pacient(int.Parse(idPacient), numePacient, int.Parse(idMedic), dataProgramare, oraProgramare);

            Form2 formular = new Form2(pacient);
            formular.ShowDialog();
            
        }

        private void PreluareDate()
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);
            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                DbCommand cmdSelect = connection.CreateCommand();
                cmdSelect.CommandText = "SELECT * FROM Pacienti";
                using (DbDataReader reader = cmdSelect.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Pacient pacient = new Pacient(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetInt32(2),
                            reader.GetString(3),
                            reader.GetString(4));
                        pacienti.Add(pacient);
                    }
                }
            }
            foreach (Medic medic in medici)
            {
                TreeNode parinte = new TreeNode("Medic - "+medic.IdMedic.ToString());
                treeView1.Nodes.Add(parinte);
            }
            foreach (Pacient pacient in pacienti.Where(x => x.IdMedic == 1))
            {
                treeView1.Nodes[0].Nodes.Add(pacient.ToString());
                nrMedic1++;
            }
            foreach (Pacient pacient in pacienti.Where(x => x.IdMedic == 2))
            {
                treeView1.Nodes[1].Nodes.Add(pacient.ToString());
                nrMedic2++;
            }
            foreach (Pacient pacient in pacienti.Where(x => x.IdMedic == 3))
            {
                treeView1.Nodes[2].Nodes.Add(pacient.ToString());
                nrMedic3++;
            }
            foreach (Pacient pacient in pacienti.Where(x => x.IdMedic == 4))
            {
                treeView1.Nodes[3].Nodes.Add(pacient.ToString());
                nrMedic4++;
            }

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode nodSelectat = treeView1.SelectedNode;
            string id = nodSelectat.Text.Split(',')[0];
            foreach (Pacient pacient in pacienti)
            {
                if (pacient.IdPacient.ToString().Equals(id))
                    textBox1.Text = id + Environment.NewLine +
                        nodSelectat.Text.Split(',')[1] + Environment.NewLine + 
                        nodSelectat.Text.Split(',')[2] + Environment.NewLine +
                        nodSelectat.Text.Split(',')[3];
            }
        }

        private void stergePacientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string idPacient = treeView1.SelectedNode.Text.Split(',')[0];
            DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);
            using(DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                DbCommand delete = connection.CreateCommand();
                delete.CommandText = "DELETE FROM Pacienti WHERE idPacient=" + int.Parse(idPacient);
                delete.ExecuteNonQuery();
            }
            treeView1.SelectedNode.Remove();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int[] Data = new int[] { nrMedic1, nrMedic2, nrMedic3, nrMedic4 };
            //luam contextul de desenare
            Graphics graphics = e.Graphics;
            //luam aria de desenare
            Rectangle clipRectangle = e.ClipRectangle;

            //det latimea unui bar
            var barWidth = clipRectangle.Width / Data.Length;
            //val maxima a inaltimii
            var maxBarHeight = clipRectangle.Height * 0.9;
            //factorul de scalare
            var scalingFactor = maxBarHeight / Data.Max();

            Brush[] brushes = new Brush[] { new SolidBrush(Color.LightPink), new SolidBrush(Color.LightSkyBlue), new SolidBrush(Color.LimeGreen), new SolidBrush(Color.MediumAquamarine) };
            
            for(int i=0;i<Data.Length;i++)
            {
                var barHeight = Data[i] * scalingFactor;
                graphics.FillRectangle(
                    brushes[i],
                    i*barWidth,
                    (float)(clipRectangle.Height-barHeight),
                    (float)(0.8*barWidth),
                    (float)barHeight);
            }
        }

        void pd_print(object sender, PrintPageEventArgs e)
        {
            int[] Data = new int[] { nrMedic1, nrMedic2, nrMedic3, nrMedic4 };
            //luam contextul de desenare
            Graphics graphics = e.Graphics;
            //luam aria de desenare
            Rectangle clipRectangle = new Rectangle(e.PageBounds.X,
                e.PageBounds.Y, e.PageBounds.Width, e.PageBounds.Height);

            //det latimea unui bar
            var barWidth = clipRectangle.Width / Data.Length;
            //val maxima a inaltimii
            var maxBarHeight = clipRectangle.Height * 0.9;
            //factorul de scalare
            var scalingFactor = maxBarHeight / Data.Max();

            Brush[] brushes = new Brush[] { new SolidBrush(Color.LightPink), new SolidBrush(Color.LightSkyBlue), new SolidBrush(Color.LimeGreen), new SolidBrush(Color.MediumAquamarine) };

            for (int i = 0; i < Data.Length; i++)
            {
                var barHeight = Data[i] * scalingFactor;
                graphics.FillRectangle(
                    brushes[i],
                    i * barWidth,
                    (float)(clipRectangle.Height - barHeight),
                    (float)(0.8 * barWidth),
                    (float)barHeight);
            }
            Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.pd_print);
            PrintPreviewDialog pdlg = new PrintPreviewDialog();
            pdlg.Document = pd;
            pdlg.ShowDialog();
        }
    }
}
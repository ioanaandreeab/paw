using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firma_distributie
{
    public partial class Form3 : Form
    {
        int[] Data;
        public Form3(int[] Data)
        {
            this.Data = Data;
            InitializeComponent();
            this.Resize += Form3_Resize;
        }

        private void Form3_Resize(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //luam contextul grafic
            Graphics g = e.Graphics;
            //luam aria de desenare
            Rectangle clipRectangle = e.ClipRectangle;

            //det latimea unui bar
            var barWidth = clipRectangle.Width / Data.Length;
            //det val max a inaltimii
            var maxBarHeight = clipRectangle.Height * 0.9;
            //det factorul de scalare
            var scalingFactor = maxBarHeight / Data.Max();

            Brush[] brushes = new Brush[] { new SolidBrush(Color.Aquamarine), new SolidBrush(Color.DarkMagenta), new SolidBrush(Color.DarkOliveGreen),
            new SolidBrush(Color.DarkSalmon), new SolidBrush(Color.DarkViolet), new SolidBrush(Color.DeepPink)};

            for (int i = 0; i < Data.Length; i++)
            {
                var barHeight = Data[i] * scalingFactor;
                g.FillRectangle(
                    brushes[i],
                    i * barWidth,
                    (float)(clipRectangle.Height - barHeight),
                    (float)(0.8 * barWidth),
                    (float)barHeight);
            }
        }
    }
}

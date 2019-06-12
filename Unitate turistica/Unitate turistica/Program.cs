using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unitate_turistica
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Camera c1 = new Camera(1, 'd', true, "25.05.2019");
            Camera c2 = new Camera(2, 's', false, "25.05.2019");
            Camera c3 = new Camera(3, 't', true, "25.05.2019");
            Camera c4 = new Camera(4, 'd', false, "25.05.2019");
            Camera c5 = new Camera(5, 'd', true, "25.05.2019");

            List<Camera> camere = new List<Camera>();
            camere.Add(c1);
            camere.Add(c2);
            camere.Add(c3);
            camere.Add(c4);
            camere.Add(c5);

            Camera c6 = new Camera(6, 'd', true, "25.05.2019");
            Unitate_turistica unitate = new Unitate_turistica(camere);
            unitate += c6;
            Console.WriteLine("*******Unitate turistica dupa adaugare camera******");
            Console.WriteLine(unitate.ToString());
            Console.WriteLine("*******Verificare operator < ********");

            List<Camera> camere2 = new List<Camera>();
            camere2.Add(c1);
            camere2.Add(c2);
            Unitate_turistica unitate2 = new Unitate_turistica(camere2);
            Console.WriteLine("Unitatea1 are mai multe camere decat unitatea2? {0}", unitate > unitate2);

            /* Unitate_turistica unitate3 = (Unitate_turistica)unitate2.Clone();
             unitate3.Camere[1].EsteOcupata = true;
             Console.WriteLine(unitate2);*/

            Unitate_turistica unitate3 = new Unitate_turistica("camere.txt");
            Console.WriteLine(unitate3.ToString());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(unitate3.Camere));

        }
    }
}

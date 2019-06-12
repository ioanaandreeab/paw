using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unitate_turistica
{
    [Serializable]
    public class Camera : IComparable<Camera>
    {
        private int nrCamera;
        private char tipCamera;
        private bool esteOcupata;
        private string dataCazare;

        public Camera(int nrCamera, char tipCamera, bool esteOcupata, string dataCazare)
        {
            this.nrCamera = nrCamera;
            this.tipCamera = tipCamera;
            this.esteOcupata = esteOcupata;
            this.dataCazare = dataCazare;
        }

        public int NrCamera { get => nrCamera; set => nrCamera = value; }
        public char TipCamera { get => tipCamera; set => tipCamera = value; }
        public bool EsteOcupata { get => esteOcupata; set => esteOcupata = value; }
        public string DataCazare { get => dataCazare; set => dataCazare = value; }

        public override string ToString()
        {
            return string.Format("Camera nr. {0}, tip-{1}, status ocupare:{2} in data de {3}",
                this.nrCamera, this.tipCamera, this.esteOcupata, this.dataCazare);
        }

        int IComparable<Camera>.CompareTo(Camera other)
        {
            if (this.NrCamera > other.NrCamera) return 1;
            else if (this.NrCamera < other.nrCamera) return -1;
            else return 0;
        }
    }
}

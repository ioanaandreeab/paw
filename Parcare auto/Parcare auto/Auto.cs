using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcare_auto
{
    [Serializable]
    public class Auto : IMiscare
    {
        private string nrInmatriculare;
        private bool inCursa;

        public Auto(string nrInmatriculare, bool inCursa)
        {
            this.nrInmatriculare = nrInmatriculare;
            this.inCursa = inCursa;
        }

        public string NrInmatriculare { get => nrInmatriculare; set => nrInmatriculare = value; }
        public bool InCursa { get => inCursa; set => inCursa = value; }

        public void Start()
        {
            this.inCursa = true;
        }

        public void Stop()
        {
            this.inCursa = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet_medical
{
    public class Pacient
    {
        private int idPacient;
        private string numePacient;
        private int idMedic;
        private string dataProgramare;
        private string oraProgramare;

        public Pacient(int idPacient, string numePacient, int idMedic, string dataProgramare, string oraProgramare)
        {
            this.idPacient = idPacient;
            this.numePacient = numePacient;
            this.idMedic = idMedic;
            this.dataProgramare = dataProgramare;
            this.oraProgramare = oraProgramare;
        }

        public int IdPacient { get => idPacient; set => idPacient = value; }
        public string NumePacient { get => numePacient; set => numePacient = value; }
        public int IdMedic { get => idMedic; set => idMedic = value; }
        public string DataProgramare { get => dataProgramare; set => dataProgramare = value; }
        public string OraProgramare { get => oraProgramare; set => oraProgramare = value; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}", this.idPacient, this.numePacient, this.dataProgramare, this.oraProgramare);
        }
    }
}

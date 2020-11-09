using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Copias
{
    class Componente
    {
        private int nCod;
        private string nComp;
        private string nSerie;

        public int Codigo
        {
            get { return nCod; }
            set { nCod = value; }
        }

        public string Disciplina
        {
            get { return nComp; }
            set { nComp = value; }
        }

        public string Serie
        {
            get { return nSerie; }
            set { nSerie = value; }
        }
    }
}

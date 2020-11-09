using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Copias
{
    class Curso
    {
        private int nCod;
        private string nNome;
        private string nMod;
        private string nSit;
        private string nTp;
        private string nCoord;
        private string nCoord2;
        
        public int Codigo
        {
            get { return nCod; }
            set { nCod = value; }
        }

        public string Nome
        {
            get { return nNome; }
            set { nNome = value; }
        }

        public string Modulo
        {
            get { return nMod; }
            set { nMod = value; }
        }

        public string Situacao
        {
            get { return nSit; }
            set { nSit = value; }
        }

        public string Coordenador
        {
            get { return nCoord; }
            set { nCoord = value; }
        }

        public string Coordenador2
        {
            get { return nCoord2; }
            set { nCoord2 = value; }
        }

        public string Tipo
        {
            get { return nTp; }
            set { nTp = value; }
        }
    }
}

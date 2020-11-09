using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Copias
{
    class Agenda
    {
        private int nCod;
        private string nDt;
        private string nCur;
        private string nPer;
        private string nCoi;
        private string nMpe;
        private string nResp;
        private string nAgen;
        private int nAul;
        private string nOut;

        public int Codigo
        {
            get { return nCod; }
            set { nCod = value; }
        }

        public string Data
        {
            get { return nDt; }
            set { nDt = value; }
        }

        public string Curso
        {
            get { return nCur; }
            set { nCur = value; }
        }

        public string Periodo
        {
            get { return nPer; }
            set { nPer = value; }
        }

        public string Coincide
        {
            get { return nCoi; }
            set { nCoi = value; }
        }

        public string MesmoPeriodo
        {
            get { return nMpe; }
            set { nMpe = value; }
        }

        public string Responsavel
        {
            get { return nResp; }
            set { nResp = value; }
        }
        public string Local
        {
            get { return nAgen; }
            set { nAgen = value; }
        }

        public int Aula
        {
            get { return nAul; }
            set { nAul = value; }
        }

        public string Outros
        {
            get { return nOut; }
            set { nOut = value; }
        }
    }
}

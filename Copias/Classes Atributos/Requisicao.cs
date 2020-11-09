using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Copias
{
    class Requisicao
    {
        private int nCod;
        private string nNome;
        private DateTime nData;
        private string nHora;
        private string nProf;
        private int nQtCop;
        private int nPag;
        private string nCurso;
        private string nFv;
        private string nAut;
        private string nCoord;
        private DateTime nDtImp;
        private DateTime nDtEnt;
        private string nOc;
        private string nResp;
        private string nDest;
        private string nAt;
        private string nCam;
        private string nArq;
        private string nLib;

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

        public DateTime Data
        {
            get { return nData; }
            set { nData = value; }
        }

        public string Hora
        {
            get { return nHora; }
            set { nHora = value; }
        }

        public string Professor
        {
            get { return nProf; }
            set { nProf = value; }
        }

        public int QtCopias
        {
            get { return nQtCop; }
            set { nQtCop = value; }
        }

        public int Paginas
        {
            get { return nPag; }
            set { nPag = value; }
        }

        public string Curso
        {
            get { return nCurso; }
            set { nCurso = value; }
        }

        public string FrenteVerso
        {
            get { return nFv; }
            set { nFv = value; }
        }

        public string Autoriza
        {
            get { return nAut; }
            set { nAut = value; }
        }

        public string Coordenador
        {
            get { return nCoord; }
            set { nCoord = value; }
        }

        public DateTime DataImpressao
        {
            get { return nDtImp; }
            set { nDtImp = value; }
        }

        public DateTime Entrega
        {
            get { return nDtEnt; }
            set { nDtEnt = value; }
        }

        public string Ocultar
        {
            get { return nOc; }
            set { nOc = value; }
        }

        public string RespImpressao
        {
            get { return nResp; }
            set { nResp = value; }
        }

        public string Destino
        {
            get { return nDest; }
            set { nDest = value; }
        }

        public string NaoAutoriza
        {
            get { return nAt; }
            set { nAt = value; }
        }

        public string Caminho
        {
            get { return nCam; }
            set { nCam = value; }
        }

        public string Arquivo
        {
            get { return nArq; }
            set { nArq = value; }
        }

        public string LibData
        {
            get { return nLib; }
            set { nLib = value; }
        }
    }
}

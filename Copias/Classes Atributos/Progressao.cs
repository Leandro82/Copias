using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Copias
{
    class Progressao
    {
        private int nCod;
        private string nPort;
        private string nNome;
        private string nSexoAl;
        private string nComp;
        private string nProf;
        private string nSexProf;
        private string nTurma;
        private string nModAtual;
        private string nSem;
        private string nEnt;
        private string nDev;


        public int Codigo
        {
            get { return nCod; }
            set { nCod = value; }
        }

        public string Portaria
        {
            get { return nPort; }
            set { nPort = value; }
        }

        public string Nome
        {
            get { return nNome; }
            set { nNome = value; }
        }

        public string SexoAl
        {
            get { return nSexoAl; }
            set { nSexoAl = value; }
        }

        public string Componente
        {
            get { return nComp; }
            set { nComp = value; }
        }

        public string Professor
        {
            get { return nProf; }
            set { nProf = value; }
        }

        public string SexoProf
        {
            get { return nSexProf; }
            set { nSexProf = value; }
        }

        public string Turma
        {
            get { return nTurma; }
            set { nTurma = value; }
        }

        public string Atual
        {
            get { return nModAtual; }
            set { nModAtual = value; }
        }

        public string Semetre
        {
            get { return nSem; }
            set { nSem = value; }
        }

        public string Entrega
        {
            get { return nEnt; }
            set { nEnt = value; }
        }

        public string Devolucao
        {
            get { return nDev; }
            set { nDev = value; }
        }
    }
}

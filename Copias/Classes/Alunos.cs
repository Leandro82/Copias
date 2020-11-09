using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Copias
{
    class Alunos
    {
        private int nCod;
        private string nNome;
        private string nTurma;
        private string nSit;

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

        public string Turma
        {
            get { return nTurma; }
            set { nTurma = value; }
        }

        public string Situacao
        {
            get { return nSit; }
            set { nSit = value; }
        }
    }
}

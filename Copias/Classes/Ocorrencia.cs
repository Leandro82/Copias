using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Copias
{
    class Ocorrencia
    {
        private int nCod;
        private string nAluno;
        private string nSexo;
        private string nSerie;
        private string nSigla;
        private DateTime nData;
        private string nProf;
        private string nMotivo;
        private string nGerado;

        public int Codigo
        {
            get { return nCod; }
            set { nCod = value; }
        }

        public string Aluno
        {
            get { return nAluno; }
            set { nAluno = value; }
        }

        public string Sexo
        {
            get { return nSexo; }
            set { nSexo = value; }
        }

        public string Serie
        {
            get { return nSerie; }
            set { nSerie = value; }
        }

        public string Sigla
        {
            get { return nSigla; }
            set { nSigla = value; }
        }

        public DateTime Data
        {
            get { return nData; }
            set { nData = value; }
        }

        public string Professor
        {
            get { return nProf; }
            set { nProf = value; }
        }

        public string Motivo
        {
            get { return nMotivo; }
            set { nMotivo = value; }
        }

        public string Gerado
        {
            get { return nGerado; }
            set { nGerado = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Copias
{
    class Acoes
    {
        private int nCod;
        private int nReq;
        private string nNome;
        private string nAcao;
        private string nHora;
        private DateTime nData;
        private string nPesq;

        public int Codigo
        {
            get { return nCod; }
            set { nCod = value; }
        }

        public int Requisição
        {
            get { return nReq; }
            set { nReq = value; }
        }

        public string Nome
        {
            get { return nNome; }
            set { nNome = value; }
        }

        public string Acao
        {
            get { return nAcao; }
            set { nAcao = value; }
        }

        public string Hora
        {
            get { return nHora; }
            set { nHora = value; }
        }

        public DateTime Data
        {
            get { return nData; }
            set { nData = value; }
        }

        public string Pesquisa
        {
            get { return nPesq; }
            set { nPesq = value; }
        }
    }
}

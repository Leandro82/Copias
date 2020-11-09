using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Copias
{
    class Itens
    {
        private int nCod;
        private int nCodCat;
        private string nCat;
        private string nIt;

        public int Codigo
        {
            get { return nCod; }
            set { nCod = value; }
        }

        public int CodCategoria
        {
            get { return nCodCat; }
            set { nCodCat = value; }
        }

        public string Categoria
        {
            get { return nCat; }
            set { nCat = value; }
        }

        public string ItensCategoria
        {
            get { return nIt; }
            set { nIt = value; }
        }
    }
}

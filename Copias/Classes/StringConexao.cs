﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Copias
{
    class StringConexao
    {
        string caminho;

        public string Endereco()
        {
           caminho = "Persist Security Info=false;SERVER=10.66.122.42;DATABASE=copias;UID=secac;pwd=secac;Allow User Variables=True;Convert Zero Datetime=True;default command timeout=0";
           return caminho;
        }
    }
}

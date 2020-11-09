using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Copias
{
    class Usuario
    {
        private int nCod;
        private string nNome;
        private string nEmail;
        private string nLogin;
        private string nSenha;
        private string nProf1;
        private string nProf2;
        private string nAcesso;

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

        public string Email
        {
            get { return nEmail; }
            set { nEmail = value; }
        }

        public string Login
        {
            get { return nLogin; }
            set { nLogin = value; }
        }

        public string Senha
        {
            get { return nSenha; }
            set { nSenha = value; }
        }

        public string Profissao1
        {
            get { return nProf1; }
            set { nProf1 = value; }
        }

        public string Profissao2
        {
            get { return nProf2; }
            set { nProf2 = value; }
        }

        public string Acesso
        {
            get { return nAcesso; }
            set { nAcesso = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Copias
{
    class Horario
    {
        private int nCod;
        private string nPriIn;
        private string nPriFi;
        private string nSegIn;
        private string nSegFi;
        private string nTerIn;
        private string nTerFi;
        private string nQuaIn;
        private string nQuaFi;
        private string nQuiIn;
        private string nQuiFi;
        private string nSexIn;
        private string nSexFi;
        private string nSetIn;
        private string nSetFi;
        private string nOitIn;
        private string nOitFi;
        private string nPer;
        private string nMod;
        private string nCur;

        public int Codigo
        {
            get { return nCod; }
            set { nCod = value; }
        }

        public string PrimeiraAulaInicio
        {
            get { return nPriIn; }
            set { nPriIn = value; }
        }

        public string PrimeiraAulaFim
        {
            get { return nPriFi; }
            set { nPriFi = value; }
        }

        public string SegundaAulaInicio
        {
            get { return nSegIn; }
            set { nSegIn = value; }
        }

        public string SegundaAulaFim
        {
            get { return nSegFi; }
            set { nSegFi = value; }
        }

        public string TerceiraAulaInicio
        {
            get { return nTerIn; }
            set { nTerIn = value; }
        }

        public string TerceiraAulaFim
        {
            get { return nTerFi; }
            set { nTerFi = value; }
        }

        public string QuartaAulaInicio
        {
            get { return nQuaIn; }
            set { nQuaIn = value; }
        }

        public string QuartaAulaFim
        {
            get { return nQuaFi; }
            set { nQuaFi = value; }
        }

        public string QuintaAulaInicio
        {
            get { return nQuiIn; }
            set { nQuiIn = value; }
        }

        public string QuintaAulaFim
        {
            get { return nQuiFi; }
            set { nQuiFi = value; }
        }

        public string SextaAulaInicio
        {
            get { return nSexIn; }
            set { nSexIn = value; }
        }

        public string SextaAulaFim
        {
            get { return nSexFi; }
            set { nSexFi = value; }
        }

        public string SetimaAulaInicio
        {
            get { return nSetIn; }
            set { nSetIn = value; }
        }

        public string SetimaAulaFim
        {
            get { return nSetFi; }
            set { nSetFi = value; }
        }

        public string OitavaAulaInicio
        {
            get { return nOitIn; }
            set { nOitIn = value; }
        }

        public string OitavaAulaFim
        {
            get { return nOitFi; }
            set { nOitFi = value; }
        }

        public string Periodo
        {
            get { return nPer; }
            set { nPer = value; }
        }

        public string Modulo
        {
            get { return nMod; }
            set { nMod = value; }
        }

        public string Curso
        {
            get { return nCur; }
            set { nCur = value; }
        }
    }
}

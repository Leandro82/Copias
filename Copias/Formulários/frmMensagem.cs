using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Copias
{
    public partial class frmMensagem : Form
    {
        string msg;
        ConectaReq co = new ConectaReq();
        public frmMensagem(string mg)
        {
            InitializeComponent();
            msg = mg;
        }

        private void frmMensagem_Load(object sender, EventArgs e)
        {
            Fechar();
            textBox1.Text = msg;
            textBox2.Select();
        }

        public void Fechar()
        {
            timer1.Interval = 5000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendKeys.Send("{ESC}");
            timer1.Stop();
            this.Close();
        }

        private void frmMensagem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 27)
            {
                this.Close();
            }
        }
    }
}

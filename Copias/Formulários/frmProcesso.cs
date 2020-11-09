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
    public partial class frmProcesso : Form
    {
        public frmProcesso()
        {
            InitializeComponent();
        }

        public void Piscar()
        {
            timer1.Stop();
            timer1.Interval = 300;
            timer1.Start();
        }

        public void Atualizar()
        {
            label1.Text = AuxClas.processo;
        }

        private void frmProcesso_Load(object sender, EventArgs e)
        {
            label1.Visible = true;
            Piscar();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label1.Visible == true)
                label1.Visible = false;
            else
                label1.Visible = true;
        }
    }
}

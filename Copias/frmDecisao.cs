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
    public partial class frmDecisao : Form
    {
        ConectaAgenda ca = new ConectaAgenda();
        ConectaItens ci = new ConectaItens();
        Itens it = new Itens();
        Agenda ag = new Agenda();
        int codigo, botao;
        string mensagem, responsavel;
        public frmDecisao(int cod, string msg, string res, int bt)
        {
            InitializeComponent();
            codigo = cod;
            mensagem = msg;
            responsavel = res;
            botao = bt;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (botao == 0)
            {
                it.Codigo = codigo;
                it.Categoria = responsavel;
                ci.AtualizaCategoria(it);
                ((frmCategItensAgenda)this.Owner).Atualizar();
                string msg = "Categoria Atualizada!!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                this.Close();
            }
            else
            {
                ag.Codigo = codigo;
                AuxClas.codigo = botao;
                ((frmAgenda)this.Owner).Atualizar();
                ca.excluir(ag);
                string msg = "Agendamento cancelado";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                this.Close();
            }
        }

        private void frmDecisao_Load(object sender, EventArgs e)
        {
            textBox1.Text = mensagem;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

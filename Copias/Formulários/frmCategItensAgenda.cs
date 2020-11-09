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
    public partial class frmCategItensAgenda : Form
    {
        Itens it = new Itens();
        ConectaItens ci = new ConectaItens();
        int codCat; 
        string mensagem, categoria;
        public frmCategItensAgenda()
        {
            InitializeComponent();
        }

        public void Atualizar()
        {
            Categorias();
            textBox1.Clear();
            textBox1.BackColor = Color.White;
            button2.Enabled = true;
            button3.Text = "Atualizar";
            label1.Text = "Categorias Cadastradas";
        }

        public void Categorias()
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow cat in ci.SelecionaCategoria().Rows)
            {
                int n = dataGridView1.Rows.Add();               
                dataGridView1.Rows[n].Cells[0].Value = cat["codCat"].GetHashCode();
                dataGridView1.Rows[n].Cells[1].Value = cat["categoria"].ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Atualizar")
            {
                label1.Text = "Clique 2X para atualizar!!";
                textBox1.BackColor = Color.Yellow;
                button3.Text = "Salvar";
                button2.Enabled = false;
            }
            else if (button3.Text == "Salvar")
            {
                it.Codigo = codCat;
                it.Categoria = textBox1.Text;
                ci.AtualizaCategoria(it);
                Categorias();
                string msg = "Categoria Atualizada!!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                string msg = "Informe uma categoria!!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox1.Focus();
            }
            else
            {
                it.Categoria = textBox1.Text;
                ci.CadCategoria(it);
                Categorias();
                string msg = "Categoria Cadastrada!!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox1.Clear();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (textBox1.BackColor == Color.Yellow)
            {
                codCat = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.GetHashCode();
                textBox1.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            }
        }

        private void frmCategItensAgenda_Load(object sender, EventArgs e)
        {
            Categorias();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.BackColor = Color.White;
            button2.Enabled = true;
            button3.Text = "Atualizar";
            label1.Text = "Categorias Cadastradas";
        }
    }
}

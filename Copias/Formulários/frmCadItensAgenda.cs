using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Copias.Formulários
{
    public partial class frmCadItensAgenda : Form
    {
        ConectaItens ci = new ConectaItens();
        Itens it = new Itens();
        int codItem, codCat;
        public frmCadItensAgenda()
        {
            InitializeComponent();
        }

        public void Itens()
        {    
            it.Categoria = comboBox1.Text;
            dataGridView1.Rows.Clear();
            foreach (DataRow cat in ci.SelecionaItensCategoria(it).Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = cat["codItem"].GetHashCode();
                dataGridView1.Rows[n].Cells[1].Value = cat["categoria"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = cat["item"].ToString();
            }           
        }

        private void frmCadItensAgenda_Load(object sender, EventArgs e)
        {
            label3.Visible = false;
            dataGridView1.Visible = false;
            this.Size = new Size(374, 278);

            int aux = ci.SelecionaCategoria().Rows.Count;
            for (int j = 0; j < aux; j++)
            {
                comboBox1.Items.Add(ci.SelecionaCategoria().Rows[j]["categoria"].ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Atualizar")
            {
                if (comboBox1.Text == "")
                {
                    string msg = "Selecione uma categoria!!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    this.Size = new Size(622, 278);
                    label3.Visible = true;
                    dataGridView1.Visible = true;
                    textBox1.BackColor = Color.Yellow;
                    button2.Enabled = false;
                    button5.Enabled = false;
                    button3.Text = "Salvar";
                    Itens();
                }
            }
            else if (button3.Text == "Salvar")
            {               
                it.Codigo = codItem;
                it.Categoria = comboBox1.Text;
                it.ItensCategoria = textBox1.Text;
                ci.AtualizaItensCategoria(it);
                string msg = textBox1.Text + " atualizado(a)!!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                label3.Visible = false;
                dataGridView1.Visible = false;
                this.Size = new Size(382, 278);
                button2.Enabled = true;
                comboBox1.Text = "";
                textBox1.BackColor = Color.White;
                textBox1.Text = "";
                button3.Text = "Atualizar";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            dataGridView1.Visible = false;
            this.Size = new Size(382, 278);
            button2.Enabled = true;
            button3.Enabled = true;
            button5.Enabled = true;
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox1.BackColor = Color.White;
            button3.Text = "Atualizar";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var peq = new frmCategItensAgenda();
            if (Application.OpenForms.OfType<frmCategItensAgenda>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                string msg = "Selecione uma categoria!!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox1.Focus();
            }
            else if (textBox1.Text == "")
            {
                string msg = "Informe o que será cadastrado!!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox1.Focus();
            }
            else
            {
                it.CodCategoria = codCat;
                it.ItensCategoria = textBox1.Text;
                ci.CadItensCategoria(it);
                string msg = textBox1.Text + " cadastrada(o)!!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                comboBox1.Text = "";
                textBox1.Clear();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (textBox1.BackColor == Color.Yellow || textBox1.BackColor == Color.YellowGreen)
            {
                codItem = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.GetHashCode();
                comboBox1.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                textBox1.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.BackColor == Color.White)
            {
                if (comboBox1.Text == "")
                {
                    string msg = "Selecione uma categoria!!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    textBox1.BackColor = Color.YellowGreen;
                    this.Size = new Size(622, 278);
                    label3.Visible = true;
                    dataGridView1.Visible = true;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    Itens();
                }
            }
            else if (textBox1.BackColor == Color.YellowGreen)
            {
                it.Codigo = codItem;
                ci.excluirItem(it);
                Itens();
                string msg = textBox1.Text + " excluído!!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                label3.Visible = false;
                dataGridView1.Visible = false;
                this.Size = new Size(382, 278);
                button2.Enabled = true;
                button3.Enabled = true;
                comboBox1.Text = "";
                textBox1.Clear();
                textBox1.BackColor = Color.White;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            it.Categoria = comboBox1.Text;
            codCat = ci.CodCategoria(it).Rows[0][0].GetHashCode();
        }
    }
}

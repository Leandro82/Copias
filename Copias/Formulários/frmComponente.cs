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
    public partial class frmComponente : Form
    {
        Componente cm = new Componente();
        ConectaComp co = new ConectaComp();
        ConectaCurso cs = new ConectaCurso();
        int cod, disc=0;
        string[] comp;
        string aux;
        public frmComponente()
        {
            InitializeComponent();
        }

        public void Componentes()
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow item in co.Componentes().Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["cod"].GetHashCode();
                dataGridView1.Rows[n].Cells[1].Value = item["comp"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["serie"].ToString();
            }
        }

        private void frmComponente_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;

            comp = new string[13];
            int cont = cs.Curso().Rows.Count;

            foreach (DataRow item in co.Componentes().Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["cod"].GetHashCode();
                dataGridView1.Rows[n].Cells[1].Value = item["comp"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["serie"].ToString();
            }

            for (int i = 0; i < cont; i++)
            {
                string curso = cs.Curso().Rows[i]["modulo"].ToString() + " " + cs.Curso().Rows[i]["nome"].ToString();
                if (curso.Contains("Médio") && !curso.Contains("integrado"))
                {
                    comp[i] = curso.Replace("A ", "").Replace("B ", "").Replace("C ", "").Replace("- ", "");
                    disc++;
                }
                if (curso.Contains("integrado"))
                {
                    comp[i] = curso;
                    disc++;
                }
                if (curso.Contains("NovoTec"))
                {
                    comp[i] = curso;
                    disc++;
                }
            }

            comboBox1.Items.Clear();
            for (int j = 0; j < disc; j++)
            {
                for (int m = 0; m < j; m++)
                {
                    if(comp[j] == comp[m])
                    {
                        aux = "ok";
                    }
                }
                if (aux != "ok")
                {
                    comboBox1.Items.Add(comp[j].ToUpper());
                }
                aux = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                string msg = "INFORMAR A SÉRIE DO COMPONENTE!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (textBox1.Text == "")
            {
                string msg = "INFORMAR O NOME DO COMPONENTE!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                cm.Serie = comboBox1.Text;
                cm.Disciplina = textBox1.Text;
                co.cadastro(cm);
                Componentes();

                string msg = "COMPONENTE CADASTRADO!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();

                textBox1.Text = "";
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            cod = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.GetHashCode();
            comboBox1.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            textBox1.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                string msg = "INFORMAR A SÉRIE DO COMPONENTE!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (textBox1.Text == "")
            {
                string msg = "INFORMAR O NOME DO COMPONENTE!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                cm.Codigo = cod;
                cm.Serie = comboBox1.Text;
                cm.Disciplina = textBox1.Text;
                co.atualizar(cm);
                Componentes();
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;

                string msg = "COMPONENTE ALTERADO!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();

                comboBox1.Text = "";
                textBox1.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cm.Codigo = cod;
            co.excluir(cm);
            Componentes();
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;

            string msg = "COMPONENTE EXCLUÍDO!!";
            frmMensagem mg = new frmMensagem(msg);
            mg.ShowDialog();

            comboBox1.Text = "";
            textBox1.Text = "";
        }
    }
}

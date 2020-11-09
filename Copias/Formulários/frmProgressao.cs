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
    public partial class frmProgressao : Form
    {
        ConectaProg co = new ConectaProg();
        Progressao pr = new Progressao();
        ConectaUs cn = new ConectaUs();

        public frmProgressao()
        {
            InitializeComponent();
        }

        private void Progressao()
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow item in co.Progressao().Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["portaria"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["nome"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["componente"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["professor"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["turma"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["modSerieAtual"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["semestreAno"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                string msg = "INFORMAR O NÚMERO DA PORTARIA!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (textBox2.Text == "")
            {
                string msg = "INFORMAR O NOME DO ALUNO!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (textBox3.Text == "")
            {
                string msg = "INFORMAR O NOME DO COMPONENTE!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (comboBox1.Text == "")
            {
                string msg = "INFORMAR O NOME DO PROFESSOR!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (textBox5.Text == "")
            {
                string msg = "INFORMAR A TURMA DA PP!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (textBox7.Text == "")
            {
                string msg = "INFORMAR O MÓDULO/SÉRIE ATUAL!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (textBox6.Text == "")
            {
                string msg = "INFORMAR INFORMAR O SEMESTRE/ANO OU ANO!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                pr.Portaria = textBox1.Text;
                pr.Nome = textBox2.Text;
                pr.Componente = textBox3.Text;
                pr.Professor = comboBox1.Text;
                pr.Turma = textBox5.Text;
                pr.Atual = textBox7.Text;
                pr.Semetre = textBox6.Text;
                if (checkBox2.Checked == true)
                {
                    pr.Entrega = "Sim";
                }
                else
                {
                    pr.Entrega = "Não";
                }

                this.dataGridView1.Rows.Insert(0, textBox1.Text, textBox2.Text, textBox3.Text, comboBox1.Text, textBox5.Text, textBox7.Text, textBox6.Text);
                co.cadastro(pr);
                string msg = "PROGRESSÃO CADASTRADA!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();

                checkBox2.Checked = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.SelectedIndex = -1;
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Salvar")
            {
                if (textBox1.Text == "")
                {
                    string msg = "INFORMAR O NÚMERO DA PORTARIA!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (textBox2.Text == "")
                {
                    string msg = "INFORMAR O NOME DO ALUNO!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (textBox3.Text == "")
                {
                    string msg = "INFORMAR O NOME DO COMPONENTE!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (comboBox1.Text == "")
                {
                    string msg = "INFORMAR O NOME DO PROFESSOR!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (textBox5.Text == "")
                {
                    string msg = "INFORMAR A TURMA DA PP!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (textBox7.Text == "")
                {
                    string msg = "INFORMAR O MÓDULO/SÉRIE ATUAL!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (textBox6.Text == "")
                {
                    string msg = "INFORMAR INFORMAR O SEMESTRE/ANO OU ANO!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    pr.Portaria = textBox1.Text;
                    pr.Nome = textBox2.Text;
                    pr.Componente = textBox3.Text;
                    pr.Professor = comboBox1.Text;
                    pr.Turma = textBox5.Text;
                    pr.Atual = textBox7.Text;
                    pr.Semetre = textBox6.Text;
                    if (checkBox2.Checked == true)
                    {
                        pr.Entrega = "Sim";
                    }
                    else
                    {
                        pr.Entrega = "Nao";
                    }
                    if (checkBox1.Checked == true)
                    {
                        pr.Devolucao = "Sim";
                    }
                    else
                    {
                        pr.Devolucao = "Nao";
                    }

                    co.atualizar(pr);

                    button3.Text = "Editar";
                    string msg = "PROGRESSÃO ALTERADA!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                    Progressao();
                    button1.Enabled = true;
                    checkBox1.Checked = false;
                    checkBox1.Visible = false;
                    checkBox2.Checked = false;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    comboBox1.SelectedIndex = -1;
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                }
            }
            else
            {
                textBox1.BackColor = Color.Yellow;
                textBox1.Focus();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.BackColor == Color.Yellow)
                {
                    button5.Enabled = true;
                    button1.Enabled = false;
                    pr.Portaria = textBox1.Text;
                    int cont = co.BuscaProgressao(pr).Rows.Count;

                    if (cont > 0)
                    {
                        checkBox1.Visible = true;
                        checkBox2.Visible = true;
                        textBox1.BackColor = Color.White;
                        button3.Text = "Salvar";
                        textBox2.Text = co.BuscaProgressao(pr).Rows[0]["nome"].ToString();
                        textBox3.Text = co.BuscaProgressao(pr).Rows[0]["componente"].ToString();
                        comboBox1.Text = co.BuscaProgressao(pr).Rows[0]["professor"].ToString();
                        textBox5.Text = co.BuscaProgressao(pr).Rows[0]["turma"].ToString();
                        textBox7.Text = co.BuscaProgressao(pr).Rows[0]["modSerieAtual"].ToString();
                        textBox6.Text = co.BuscaProgressao(pr).Rows[0]["semestreAno"].ToString();
                        if (co.BuscaProgressao(pr).Rows[0]["entrega"].ToString() == "Sim")
                        {
                            checkBox2.Checked = true;
                        }
                        if (co.BuscaProgressao(pr).Rows[0]["devolucao"].ToString() == "Sim")
                        {
                            checkBox1.Checked = true;
                        }
                    }
                    else
                    {
                        string msg = "NÃO EXISTE DADOS PARA ESSA PORTARIA!!";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                    }
                }
            }
        }

        private void frmProgressao_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            checkBox1.Visible = false;
            button5.Enabled = false;

            int prf = cn.Professor().Rows.Count;
            for (int j = 0; j < prf; j++)
            {
                comboBox1.Items.Add(cn.Professor().Rows[j]["nome"].ToString());
            }

            foreach (DataRow item in co.Progressao().Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["portaria"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["nome"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["componente"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["professor"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["turma"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["modSerieAtual"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["semestreAno"].ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                string msg = "NÃO EXISTE DADOS PARA EXCLUIR!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                string message = "Deseja realmente excluir essa Progressão?";
                string caption = "ATENÇÃO!!!";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    pr.Portaria = textBox1.Text;
                    co.excluir(pr);
                    Progressao();
                    string msg = "PORTARIA EXCLUÍDA!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                button3.Text = "Editar";
                button1.Enabled = true;
                checkBox1.Checked = false;
                checkBox1.Visible = false;
                checkBox2.Checked = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.SelectedIndex = -1;
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Text = "Editar";
            button1.Enabled = true;
            checkBox1.Checked = false;
            checkBox1.Visible = false;
            checkBox2.Checked = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedIndex = -1;
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
        }
    }
}

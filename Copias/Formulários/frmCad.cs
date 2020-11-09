using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Copias
{
    public partial class frmCad : Form
    {
        ConectaReq cr = new ConectaReq();
        ConectaUs co = new ConectaUs();
        Usuario us = new Usuario();
        Acoes ac = new Acoes();
        ConectaAc ca = new ConectaAc();
        public frmCad()
        {
            InitializeComponent();
        }

        private void dataGrid()
        {
            ConectaUs co = new ConectaUs();
            dataGridView1.Rows.Clear();
            foreach (DataRow item in co.Grid().Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["nome"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["email"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["login"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["func1"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["func2"].ToString();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConectaUs co = new ConectaUs();
            Usuario us = new Usuario();
            us.Nome = textBox1.Text;
            int aux = co.GridView(us).Rows.Count;

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                string msg = "Preencher todos os dados";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false && checkBox6.Checked == false)
            {
                string msg = "Escolha uma das funções";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                if (aux > 0)
                {
                    string msg = "Usuário já está cadastrado";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    us.Nome = textBox1.Text;
                    us.Email = textBox2.Text;
                    us.Login = textBox3.Text;
                    us.Senha = textBox4.Text;
                    if (checkBox1.Checked == true && checkBox2.Checked == true)
                    {
                        us.Profissao1 = "Professor";
                        us.Profissao2 = "Coordenador";
                    }
                    else if (checkBox1.Checked == true)
                    {
                        us.Profissao1 = "Professor";
                    }
                    else if (checkBox3.Checked == true)
                    {
                        us.Profissao1 = "Secretaria";
                    }
                    else if (checkBox4.Checked == true)
                    {
                        us.Profissao1 = "Diretoria de Serviços";
                    }
                    else if (checkBox6.Checked == true)
                    {
                        us.Profissao1 = "Direção";
                    }
                    else if (checkBox5.Checked == true)
                    {
                        us.Profissao1 = "Orientador Educacional";
                    }
                    else if (checkBox7.Checked == true)
                    {
                        us.Profissao1 = "Coordenador Pedagógico";
                    }

                    co.cadastro(us);
                    us.Profissao1 = "";
                    us.Profissao2 = "";
                    string msg = "Usuário cadastrado";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            label5.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox6.Checked = false;
            label5.Visible = true;
            dataGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                string msg = "Não existe dados para editar";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (textBox4.Text != "")
            {
                us.Nome = textBox1.Text;
                us.Email = textBox2.Text;
                us.Login = textBox3.Text;
                us.Senha = textBox4.Text;
                if (checkBox1.Checked == true && checkBox2.Checked == true)
                {
                    us.Profissao1 = "Professor";
                    us.Profissao2 = "Coordenador";
                }
                else if (checkBox1.Checked == true)
                {
                    us.Profissao1 = "Professor";
                }
                else if (checkBox3.Checked == true)
                {
                    us.Profissao1 = "Secretaria";
                }
                else if (checkBox4.Checked == true)
                {
                    us.Profissao1 = "Diretoria de Serviços";
                }
                else if (checkBox6.Checked == true)
                {
                    us.Profissao1 = "Direção";
                }
                else if (checkBox5.Checked == true)
                {
                    us.Profissao1 = "Orientador Educacional";
                }
                else if (checkBox7.Checked == true)
                {
                    us.Profissao1 = "Coordenador Pedagógico";
                }
                co.atualizarTudo(us);

                ac.Nome = AuxClas.coordenador;
                ac.Data = Convert.ToDateTime(cr.BuscaDataServidor());
                ac.Hora = cr.BuscaHoraServidor();
                ac.Acao = "Atualizou dados de " + textBox1.Text; 
                ca.cadastro(ac);

                us.Profissao1 = "";
                us.Profissao2 = "";
                string msg = "Cadastro atualizado";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                label5.Text = "";
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox6.Checked = false;
                dataGrid();
            }
            else
            {
                us.Nome = textBox1.Text;
                us.Email = textBox2.Text;
                if (checkBox1.Checked == true && checkBox2.Checked == true)
                {
                    us.Profissao1 = "Professor";
                    us.Profissao2 = "Coordenador";
                }
                else if (checkBox1.Checked == true)
                {
                    us.Profissao1 = "Professor";
                    us.Profissao2 = "";
                }
                else if (checkBox3.Checked == true)
                {
                    us.Profissao1 = "Secretaria";
                }
                else if (checkBox4.Checked == true)
                {
                    us.Profissao1 = "Diretoria de Serviços";
                }
                else if (checkBox6.Checked == true)
                {
                    us.Profissao1 = "Direção";
                }
                else if (checkBox5.Checked == true)
                {
                    us.Profissao1 = "Orientador Educacional";
                }
                else if (checkBox7.Checked == true)
                {
                    us.Profissao1 = "Coordenador Pedagógico";
                }
                co.atualizar(us);
                us.Profissao1 = "";
                us.Profissao2 = "";
                string msg = "Cadastro atualizado";

                ac.Nome = AuxClas.coordenador;
                ac.Data = Convert.ToDateTime(cr.BuscaDataServidor());
                ac.Hora = cr.BuscaHoraServidor();
                ac.Acao = "Atualizou dados de " + textBox1.Text;
                ca.cadastro(ac);

                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                label5.Text = "";
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox6.Checked = false;
                checkBox5.Checked = false;
                checkBox7.Checked = false;
                dataGrid();
            }
            button1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string message = "Deseja realmente excluir este usuário?";
                string caption = "Confirmar exclusão";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);

                if (result == System.Windows.Forms.DialogResult.No)
                {
                    this.Close();
                }
                else
                {
                    if (textBox1.Text == "")
                    {
                        string msg = "Não existe dados para excluir";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                    }
                    else
                    {
                        us.Nome = textBox1.Text;
                        co.excluir(us);

                        ac.Nome = AuxClas.coordenador;
                        ac.Data = Convert.ToDateTime(cr.BuscaDataServidor());
                        ac.Hora = cr.BuscaHoraServidor();
                        ac.Acao = "Excluiu " + textBox1.Text + " do sistema";
                        ca.cadastro(ac);

                        string msg = "Cadastro excluído";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        label5.Text = "";
                        checkBox1.Checked = false;
                        checkBox2.Checked = false;
                        checkBox3.Checked = false;
                        checkBox4.Checked = false;
                        checkBox6.Checked = false;
                        dataGrid();
                    }
                }
               button1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCad_Load(object sender, EventArgs e)
        {
            dataGrid();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            string nome = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            string email = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            string login = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            string cargo = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            string coord = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            textBox1.Text = nome;
            textBox2.Text = email;
            textBox3.Text = login;
            if (cargo == "Professor")
            {
                checkBox1.Checked = true;
            }
            else if (cargo == "Secretaria")
            {
                checkBox3.Checked = true;
            }
            else if (cargo == "Diretoria de Serviços")
            {
                checkBox4.Checked = true;
            }
            else if (cargo == "Direção")
            {
                checkBox6.Checked = true;
            }
            else if (cargo == "Orientador Educacional")
            {
                checkBox5.Checked = true;
            }
            else if (cargo == "Coordenador Pedagógico")
            {
                checkBox7.Checked = true;
            }

            if (coord == "Coordenador")
            {
                checkBox2.Checked = true;
            }

            button1.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox6.Checked = false;
                checkBox5.Checked = false;
                checkBox7.Checked = false;
            }
        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = true;
                checkBox4.Checked = false;
                checkBox6.Checked = false;
                checkBox5.Checked = false;
                checkBox7.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox4.Checked = false;
            checkBox6.Checked = false;
            checkBox5.Checked = false;
            checkBox7.Checked = false;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox6.Checked = false;
            checkBox5.Checked = false;
            checkBox7.Checked = false;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox7.Checked = false;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox6.Checked = false;
            checkBox5.Checked = false;
        }

        public void Fechar()
        {
            timer1.Interval = 2000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendKeys.Send("{ESC}");
            timer1.Stop();
        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {
            label5.Visible = true;
            if (textBox4.Text != textBox5.Text)
            {
                label5.MaximumSize = new Size(100, 0);
                label5.AutoSize = true;
                label5.Font = new Font(label5.Font.Name, 10);
                label5.Text = "Senhas não batem";
                label5.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                label5.Font = new Font(label5.Font.Name, 12);
                label5.Text = "OK";
                label5.ForeColor = System.Drawing.Color.Green;
            }
        }     
    }
}

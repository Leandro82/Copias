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
    public partial class frmCursos : Form
    {
        Curso cs = new Curso();
        Usuario us = new Usuario();
        ConectaCurso cc = new ConectaCurso();
        int codCurso = 0;
        string nomeCurso;
        public frmCursos()
        {
            InitializeComponent();
        }

        private void dataGrid()
        {
            cs.Situacao = comboBox2.Text;
            dataGridView1.Rows.Clear();
            foreach (DataRow item in cc.CarregaGridCurso(cs).Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["modulo"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["nome"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["situacao"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["coord"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["coord2"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["cod"].GetHashCode();
                dataGridView1.Rows[n].Cells[6].Value = item["tipo"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item["periodo"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConectaCurso co = new ConectaCurso();   
            cs.Nome = textBox1.Text;
            cs.Modulo = comboBox1.Text;
            cs.Coordenador = comboBox3.Text;
            cs.Periodo = comboBox5.Text;
            nomeCurso = textBox1.Text;
            if (nomeCurso.Contains("Médio") && !nomeCurso.Contains("integrado"))
            {
                cs.NumAulas = 5;
            }
            else if (nomeCurso.Contains("integrado"))
            {
                cs.NumAulas = 8;
            }
            else if (nomeCurso.Contains("NovoTec"))
            {
                cs.NumAulas = 6;
            }
            else
            {
                cs.NumAulas = 4;
            }

            if (comboBox4.Text != "")
                cs.Coordenador2 = comboBox4.Text;

            int aux = co.VerificaCadastro(cs).Rows.Count;

            if (textBox1.Text == "")
            {
                string msg = "Por favor, informar o nome do curso";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (comboBox5.Text == "")
            {
                string msg = "Por favor, informe o período do curso";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                string msg = "Por favor, informar a situação do curso";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (radioButton3.Checked == false && radioButton4.Checked == false)
            {
                string msg = "Informe o tipo do do Curso - M = Médio, NovoTec e Integral - T = Técnico";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                if (aux == 0)
                {
                    if (radioButton1.Checked == true)
                    {
                        cs.Situacao = "Ativo";
                    }
                    else if (radioButton2.Checked == true)
                    {
                        cs.Situacao = "Inativo";
                    }

                    if (radioButton3.Checked == true)
                        cs.Tipo = "M";
                    else if (radioButton4.Checked == true)
                        cs.Tipo = "T";

                    cc.cadastro(cs);
                    string msg = "Curso cadastrado";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (aux > 0)
                {
                    string msg = "Curso já cadastrado";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
            }
            textBox1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            comboBox1.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            dataGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sit, tipo;

            if (radioButton1.Checked == true)
            {
                sit = "Ativo";
            }
            else
            {
                sit = "Inativo";
            }

            if (radioButton3.Checked == true)
                tipo = "M";
            else
                tipo = "T";

            cs.Nome = textBox1.Text;
            cs.Modulo = comboBox1.Text;
            cs.Coordenador = comboBox3.Text;
            cs.Periodo = comboBox5.Text;
            if (comboBox4.Text != "")
                cs.Coordenador2 = comboBox4.Text;
            cs.Situacao = sit;
            
            
                if (textBox1.Text == "" && radioButton1.Checked == false && radioButton2.Checked == false)
                {
                    string msg = "Não existe dados para editar";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (radioButton3.Checked == false && radioButton4.Checked == false)
                {
                    string msg = "Informe o tipo do do Curso - M = Médio, NovoTec e Integral - T = Técnico";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    nomeCurso = textBox1.Text;
                    if (nomeCurso.Contains("Médio") && !nomeCurso.Contains("integrado"))
                    {
                        cs.NumAulas = 5;
                    }
                    else if (nomeCurso.Contains("integrado"))
                    {
                        cs.NumAulas = 8;
                    }
                    else if (nomeCurso.Contains("NovoTec"))
                    {
                        cs.NumAulas = 6;
                    }
                    else
                    {
                        cs.NumAulas = 4;
                    }

                    cs.Nome = textBox1.Text;
                    cs.Modulo = comboBox1.Text;
                    cs.Coordenador = comboBox3.Text;
                    if (comboBox4.Text != "")
                        cs.Coordenador2 = comboBox4.Text;

                    if (radioButton1.Checked == true)
                    {
                        cs.Situacao = "Ativo";
                    }
                    else if (radioButton2.Checked == true)
                    {
                        cs.Situacao = "Inativo";
                    }

                    if (radioButton3.Checked == true)
                        cs.Tipo = "M";
                    else if(radioButton4.Checked == true)
                        cs.Tipo = "T";
                    cc.atualizar(cs);
                    string msg = "Cadastro alterado com sucesso";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
            }
            textBox1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            comboBox1.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            button1.Enabled = true;
            comboBox1.Enabled = true;
            textBox1.Enabled = true;
            dataGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string message = "Deseja realmente excluir esse cadastro?";
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
                        cs.Codigo = codCurso;
                    }
                    cc.excluir(cs);
                    string msg1 = "Cadastro excluído com sucesso";
                    frmMensagem mg1 = new frmMensagem(msg1);
                    mg1.ShowDialog();
                    textBox1.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    comboBox1.Text = "";
                    comboBox3.Text = "";
                    comboBox4.Text = "";
                    button1.Enabled = true;
                    comboBox1.Enabled = true;
                    textBox1.Enabled = true;
                    dataGrid();
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCursos_Load(object sender, EventArgs e)
        {
            comboBox2.Text = "Ativo";
            int prf = cc.Coordenador().Rows.Count;
            for (int j = 0; j < prf; j++)
            {
                comboBox3.Items.Add(cc.Coordenador().Rows[j]["nome"].ToString());
                comboBox4.Items.Add(cc.Coordenador().Rows[j]["nome"].ToString());
            }
            dataGrid();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            string mod = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            string nome = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            string sit = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            string coord = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            string coord2 = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            codCurso = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.GetHashCode();
            string tipo = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            string periodo = dataGridView1[7, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            textBox1.Text = nome;
            comboBox1.Text = mod;
            comboBox3.Text = coord;
            comboBox4.Text = coord2;
            comboBox5.Text = periodo;
            if (tipo == "M")
            {
                radioButton3.Checked = true;
            }
            else if (tipo == "T")
            {
                radioButton4.Checked = true;
            }

            if (sit == "Ativo")
            {
                radioButton1.Checked = true;
            }
            else if (sit == "Inativo")
            {
                radioButton2.Checked = true;
            }
            button1.Enabled = false;
            comboBox1.Enabled = false;
            textBox1.Enabled = false;
            dataGrid();
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            if (textBox1.Text == "Açúcar e Álcool" || textBox1.Text == "Enfermagem" || textBox1.Text == "Química")
            {
                comboBox1.Items.Add("1º");
                comboBox1.Items.Add("2º");
                comboBox1.Items.Add("3º");
                comboBox1.Items.Add("4º");
            }
            else 
            {
                comboBox1.Items.Add("1º");
                comboBox1.Items.Add("2º");
                comboBox1.Items.Add("3º");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cs.Situacao = comboBox2.Text;
            if (comboBox2.Text == "Ativo" || comboBox2.Text == "Inativo")
            {
                dataGridView1.Rows.Clear();
                foreach (DataRow item in cc.CarregaGridCurso(cs).Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item["modulo"].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = item["nome"].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item["situacao"].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = item["coord"].ToString();
                    dataGridView1.Rows[n].Cells[4].Value = item["coord2"].ToString();
                    dataGridView1.Rows[n].Cells[5].Value = item["cod"].GetHashCode();
                    dataGridView1.Rows[n].Cells[6].Value = item["tipo"].ToString();
                    dataGridView1.Rows[n].Cells[7].Value = item["periodo"].ToString();
                }
            }
            else
            {
                dataGridView1.Rows.Clear();
                foreach (DataRow item in cc.CarregaGridCurso(cs).Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item["modulo"].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = item["nome"].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item["situacao"].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = item["coord"].ToString();
                    dataGridView1.Rows[n].Cells[4].Value = item["coord2"].ToString();
                    dataGridView1.Rows[n].Cells[5].Value = item["cod"].GetHashCode();
                    dataGridView1.Rows[n].Cells[6].Value = item["tipo"].ToString();
                    dataGridView1.Rows[n].Cells[7].Value = item["periodo"].ToString();
                }
            }
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
    }
}

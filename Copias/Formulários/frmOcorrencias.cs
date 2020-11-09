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
    public partial class frmOcorrencias : Form
    {
        ConectaAluno cl = new ConectaAluno();
        Alunos al = new Alunos();
        Usuario us = new Usuario();
        ConectaUs cn = new ConectaUs();
        ConectaCurso cs = new ConectaCurso();
        Curso cc = new Curso();
        Ocorrencia oc = new Ocorrencia();
        ConectaOc co = new ConectaOc();
        ConectaComp cp = new ConectaComp();
        Componente cm = new Componente();
        string atualiza = "",exclui = "", nome, curso;
        int cod;
        public frmOcorrencias(string nm)
        {
            InitializeComponent();
            nome = nm;
        }

        public void Atualizar()
        {
            cod = AuxClas.codigo;
            comboBox1.Items.Add(AuxClas.NmArquivo);
            comboBox3.Text = AuxClas.sigla;
            dateTimePicker1.Text = AuxClas.dtEntrega;
            textBox2.Text = AuxClas.motivo;
            atualiza = AuxClas.atualiza;
            exclui = AuxClas.exclui;
        }

        private void frmOcorrencias_Load(object sender, EventArgs e)
        {
            textBox2.MaxLength = 330;
            label7.Text = "Restam: 330 caracteres.";
            int cont = cs.Curso().Rows.Count;
            for (int i = 0; i < cont; i++)
            {
                comboBox2.Items.Add(cs.Curso().Rows[i]["modulo"].ToString() + " " + cs.Curso().Rows[i]["nome"].ToString());
            }

            if (atualiza == "atualiza")
            {
                button2.Enabled = false;
                button4.Enabled = false;
                comboBox2.SelectedItem = AuxClas.curso.TrimEnd();
                comboBox1.SelectedItem = AuxClas.NmArquivo;
                comboBox3.SelectedItem = AuxClas.sigla;
            }
            else if (exclui == "exclui")
            {
                button2.Enabled = false;
                button3.Enabled = false;
                comboBox2.SelectedItem = AuxClas.curso.TrimEnd();
                comboBox1.SelectedItem = AuxClas.NmArquivo;
                comboBox3.SelectedItem = AuxClas.sigla;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                string msg = "Informe o nome do(a) aluno(a)";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                comboBox1.Focus();
            }  
            else if (comboBox2.Text == "")
            {
                string msg = "Informe a série do(a) aluno(a)";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();

            }
            else if(comboBox3.Text == "")
            {
                string msg = "Informe Componente";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (textBox2.Text == "")
            {
                string msg = "Informe o motivo da ocorrência";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox2.Focus();
            }
            else
            {
                oc.Aluno = comboBox1.Text;
                oc.Serie = comboBox2.Text.TrimEnd();
                oc.Sigla = comboBox3.Text;
                oc.Data = Convert.ToDateTime(dateTimePicker1.Text);
                oc.Professor = nome;
                oc.Motivo = textBox2.Text;
                co.cadastro(oc);
                string msg = "OCORRÊNCIA CADASTRADA";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                textBox2.Text = "";
                comboBox3.SelectedIndex = -1;
                label7.Text = "Restam: 330 caracteres.";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int cont = co.carregaGridOc(oc).Rows.Count;
            
                if (atualiza == "atualiza")
                {
                    if (comboBox2.Text == "")
                    {
                        string msg = "SELECIONAR A CLASSE!!";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                    }
                    else
                    {
                        oc.Codigo = AuxClas.codigo;
                        oc.Aluno = comboBox1.Text;
                        oc.Serie = comboBox2.Text.TrimEnd();
                        oc.Sigla = comboBox3.Text;
                        oc.Data = Convert.ToDateTime(dateTimePicker1.Text);
                        oc.Motivo = textBox2.Text;
                        co.atualizar(oc);
                        string msg = "OCORRÊNCIA ATUALIZADA";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                        this.Close();
                    }
                }
                else if (cont == 0)
                {
                    string msg = "NÃO EXISTE DADOS PARA ATUALIZAR.";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    string at = "atualiza";
                    frmAtOcorrencia frm = new frmAtOcorrencia(at);
                    frm.Show();
                    this.Close();
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int cont = co.carregaGridOc(oc).Rows.Count;

            if (atualiza == "exclui")
            {
                oc.Codigo = AuxClas.codigo;
                co.excluir(oc);
                string msg = "OCORRÊNCIA EXCLUÍDA";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                this.Close();
            }
            else if (cont == 0)
            {
                string msg = "NÃO EXISTE DADOS PARA EXCLUIR.";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                string at = "exclui";
                frmAtOcorrencia frm = new frmAtOcorrencia(at);
                frm.Show();
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (atualiza != "atualiza" && exclui != "exclui")
            {
                comboBox1.Items.Clear();
            }
            al.Turma = comboBox2.Text;
            int alu = cl.BuscaAlunos(al).Rows.Count;

            for (int i = 0; i < alu; i++)
            {
                comboBox1.Items.Add(cl.BuscaAlunos(al).Rows[i]["nome"].ToString());
            }

            
            string materia = comboBox2.Text;
            if (materia.Contains("Médio") && !materia.Contains("integrado"))
            {
                curso = materia.Replace("A ", "").Replace("B ", "").Replace("C ", "").Replace("- ", "");
            }
            if (materia.Contains("integrado"))
            {
                curso = materia;
            }
            if (materia.Contains("NovoTec"))
            {
                curso = materia;
            }

            comboBox3.Items.Clear();
            cm.Serie = curso;
            int comp = cp.ComboComponentes(cm).Rows.Count;
            for (int i = 0; i < comp; i++)
            {
                comboBox3.Items.Add(cp.ComboComponentes(cm).Rows[i]["comp"].ToString());
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string characters = textBox2.Text;
            label7.Text = "Restam: " + Convert.ToString(330 - characters.Length) + " caracteres.";
        }
    }
}

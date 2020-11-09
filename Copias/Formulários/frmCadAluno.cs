using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;


namespace Copias
{
    public partial class frmCadAluno : Form
    {
        ConectaCurso cs = new ConectaCurso();
        ConectaAluno co = new ConectaAluno();
        Alunos al = new Alunos();
        OpenFileDialog arquivo = new OpenFileDialog();

        string caminho;
        int lin, cont;

        public frmCadAluno()
        {
            InitializeComponent();
        }

        private void frmCadAluno_Load(object sender, EventArgs e)
        {
            button1.Visible = false;
            dataGridView1.Enabled = false;
            int cont = cs.Curso().Rows.Count;
            for (int i = 0; i < cont; i++)
            {
                comboBox1.Items.Add(cs.Curso().Rows[i]["modulo"].ToString() + " " + cs.Curso().Rows[i]["nome"].ToString());
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Visible = true;
            button1.Text = "Cadastrar";
            dataGridView1.Enabled = false;
            textBox1.Enabled = true;
            dataGridView1.Rows.Clear();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Teste");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Visible = true;
            button1.Text = "Buscar";
            dataGridView1.Enabled = true;
            textBox1.Text = "";
            textBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                string msg = "Escolha uma opção";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (radioButton1.Checked == true)
            {
                button1.Text = "Cadastrar";
                new System.Threading.Thread(delegate()
                {
                    Carrega();
                }).Start();
            }
            else if (radioButton2.Checked == true)
            {
                new System.Threading.Thread(delegate()
                {
                    Carrega();
                }).Start();
            }
        }

        public void Carrega()
        {
            System.Threading.Thread arquivo1 = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
             {
                 if (radioButton1.Checked == false && radioButton2.Checked == false)
                 {
                     string msg = "Escolha uma opção";
                     frmMensagem mg = new frmMensagem(msg);
                     mg.ShowDialog();
                 }
                 else if (radioButton1.Checked == true)
                 {
                     if (textBox1.Text == "")
                     {
                         string msg = "Informe o nome completo do(a) Aluno(a)!!!";
                         frmMensagem mg = new frmMensagem(msg);
                         mg.ShowDialog();
                     }
                     else if (comboBox1.Text == "")
                     {
                         string msg = "Informe a turma do(a) Aluno(a)!!!";
                         frmMensagem mg = new frmMensagem(msg);
                         mg.ShowDialog();
                     }
                     else
                     {
                         al.Nome = textBox1.Text;
                         al.Turma = comboBox1.Text;
                         co.cadastro(al);
                         string msg = "Aluno(a) Cadastrado(a)!!!";
                         frmMensagem mg = new frmMensagem(msg);
                         mg.ShowDialog();

                         textBox1.Text = "";
                         comboBox1.SelectedIndex = -1;
                     }
                 }
                 else if (radioButton2.Checked == true && button1.Text == "Buscar")
                 {
                     if (arquivo.ShowDialog() == DialogResult.OK)
                     {
                         this.Invoke((MethodInvoker)delegate()
                          {
                              caminho = arquivo.FileName;

                              OleDbConnection conexao = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + caminho + "';Extended Properties='Excel 12.0 Xml;HDR=YES';");

                              OleDbDataAdapter adapter = new OleDbDataAdapter(@"select * from [Planilha1$]", conexao);
                              DataTable ds = new DataTable();
                              adapter.Fill(ds);
                              int cont = ds.Rows.Count;
                              try
                              {
                                  conexao.Open();

                                  lin = 3;
                                  while (lin < cont)
                                  {
                                      int n = dataGridView1.Rows.Add();
                                      dataGridView1.Rows[n].Cells[0].Value = ds.Rows[lin][1].ToString();
                                      lin = lin + 1;
                                  }
                              }
                              catch (Exception ex)
                              {
                                  Console.WriteLine("Erro ao acessar os dados: " + ex.Message);
                              }
                              finally
                              {
                                  conexao.Close();
                              }
                              button1.Text = "Cadastrar";
                          });
                     }
                 }
                 else if (button1.Text == "Cadastrar" && radioButton2.Checked == true)
                 {
                     if (comboBox1.Text == "")
                     {
                         string msg = "Informe a Turma!!!";
                         frmMensagem mg = new frmMensagem(msg);
                         mg.ShowDialog();
                     }
                     else
                     {
                         al.Turma = comboBox1.Text;
                         int datatable = co.BuscaAlunos(al).Rows.Count;

                         if (datatable == 0)
                         {
                             cont = dataGridView1.Rows.Count;
                             for (int i = 0; i < cont; i++)
                             {
                                 al.Nome = dataGridView1.Rows[i].Cells[0].Value.ToString();
                                 al.Turma = comboBox1.Text;
                                 co.cadastro(al);
                             }

                             string msg = "Turma Cadastrada!!!";
                             frmMensagem mg = new frmMensagem(msg);
                             mg.ShowDialog();

                             comboBox1.SelectedIndex = -1;
                             dataGridView1.Rows.Clear();
                             button1.Text = "Buscar";
                         }
                         else if (co.BuscaAlunos(al).Rows[0][2].ToString() == comboBox1.Text)
                         {
                             string msg = "Turma já cadastrada no sistema!!!";
                             frmMensagem mg = new frmMensagem(msg);
                             mg.ShowDialog();
                             button1.Text = "Buscar";
                             dataGridView1.Rows.Clear();
                         }
                     }
                     
                 }
             }));
            arquivo1.SetApartmentState(System.Threading.ApartmentState.STA);
            arquivo1.IsBackground = false;
            arquivo1.Start();
        }
    }
}

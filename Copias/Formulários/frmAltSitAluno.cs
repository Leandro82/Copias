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
    public partial class frmAltSitAluno : Form
    {
        ConectaCurso cs = new ConectaCurso();
        Alunos al = new Alunos();
        ConectaAluno co = new ConectaAluno();

        public frmAltSitAluno()
        {
            InitializeComponent();
        }

        private void frmAltSitAluno_Load(object sender, EventArgs e)
        {
            int cont = cs.Curso().Rows.Count;
            for (int i = 0; i < cont; i++)
            {
                comboBox1.Items.Add(cs.Curso().Rows[i]["modulo"].ToString() + " " + cs.Curso().Rows[i]["nome"].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                string msg = "Informe a Turma!!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                dataGridView1.Rows.Clear();
                al.Turma = comboBox1.Text;
                int cont = co.Alunos(al).Rows.Count;
                if (cont == 0)
                {
                    string msg = "Essa turma não possui alunos cadastrados!!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                    dataGridView1.Rows.Clear();
                }
                else
                {
                    foreach(DataRow item in co.Alunos(al).Rows)
                    {
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells[1].Value = item["cod"].GetHashCode();
                        dataGridView1.Rows[n].Cells[2].Value = item["nome"].ToString();
                        if (item["situacao"].ToString() == "Ok")
                        {
                            dataGridView1.Rows[n].Cells[0].Value = true;
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns[0].Index)
            {
                dataGridView1.EndEdit();
                int cod = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                if ((bool)dataGridView1.Rows[e.RowIndex].Cells[0].Value)
                {
                    al.Codigo = cod;
                    al.Situacao = "Ok";
                    co.BaixarAlunos(al);

                    string msg = "Aluno baixado no sistema!!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    al.Codigo = cod;
                    al.Situacao = "";
                    co.BaixarAlunos(al);

                    string msg = "Aluno incluso na sala novamente!!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }
    }
}

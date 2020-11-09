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
    public partial class frmAtualiza : Form
    {
        string name, pag, atualiza, exclui;

        public frmAtualiza(string nm, string pg, string at, string exc)
        {
            InitializeComponent();
            name = nm;
            pag = pg;
            atualiza = at;
            exclui = exc;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (pag == "abre")
            {
                frmCopiaProf co = new frmCopiaProf("");
                AuxClas.codigo = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.GetHashCode();
                AuxClas.professor = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                AuxClas.NmArquivo = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                AuxClas.dtEntrega = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                AuxClas.quantidade = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                AuxClas.total = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                AuxClas.curso = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                AuxClas.frente = dataGridView1[7, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                AuxClas.atualiza = atualiza;
                AuxClas.exclui = exclui;
                //AuxClas.aux = 2;
                co.Atualizar();
                co.Show();
                this.Close();
            }
            else
            {
                frmCopias co = new frmCopias(name);
                AuxClas.codigo = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.GetHashCode();
                AuxClas.professor = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                AuxClas.NmArquivo = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                AuxClas.dtEntrega = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                AuxClas.quantidade = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                AuxClas.total = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                AuxClas.curso = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                AuxClas.frente = dataGridView1[7, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                AuxClas.atualiza = atualiza;
                AuxClas.exclui = exclui;
                co.Atualizar();
                co.Show();
                this.Close();
            }
        }

        private void frmAtualiza_Load(object sender, EventArgs e)
        {
            ConectaReq co = new ConectaReq();
            Requisicao re = new Requisicao();

            re.Nome = name;
            if (pag == "abre")
            {
                    re.Professor = name;
                    dataGridView1.Rows.Clear();
                    foreach (DataRow item in co.porProfessor(re).Rows)
                    {
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells[0].Value = item["cod"].GetHashCode();
                        dataGridView1.Rows[n].Cells[1].Value = item["prof"].ToString();
                        dataGridView1.Rows[n].Cells[2].Value = item["nmArquivo"].ToString();
                        dataGridView1.Rows[n].Cells[3].Value = Convert.ToDateTime(item["dtEntrega"].ToString()).ToString("dd/MM/yyyy");
                        dataGridView1.Rows[n].Cells[4].Value = item["quant"].ToString();
                        dataGridView1.Rows[n].Cells[5].Value = item["total"].ToString();
                        dataGridView1.Rows[n].Cells[6].Value = item["curso"].ToString();
                        dataGridView1.Rows[n].Cells[7].Value = item["frente"].ToString();
                    }
            }
            else
            {
                    dataGridView1.Rows.Clear();
                    foreach (DataRow item in co.Geral(re).Rows)
                    {
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells[0].Value = item["cod"].GetHashCode();
                        dataGridView1.Rows[n].Cells[1].Value = item["prof"].ToString();
                        dataGridView1.Rows[n].Cells[2].Value = item["nmArquivo"].ToString();
                        dataGridView1.Rows[n].Cells[3].Value = Convert.ToDateTime(item["dtEntrega"].ToString()).ToString("dd/MM/yyyy");
                        dataGridView1.Rows[n].Cells[4].Value = item["quant"].ToString();
                        dataGridView1.Rows[n].Cells[5].Value = item["total"].ToString();
                        dataGridView1.Rows[n].Cells[6].Value = item["curso"].ToString();
                        dataGridView1.Rows[n].Cells[7].Value = item["frente"].ToString();
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

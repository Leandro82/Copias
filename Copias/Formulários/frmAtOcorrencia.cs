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
    public partial class frmAtOcorrencia : Form
    {
        string atualiza, exclui;
        Ocorrencia oc = new Ocorrencia();
        ConectaOc co = new ConectaOc();
        public frmAtOcorrencia(string at)
        {
            InitializeComponent();
            atualiza = at;
            exclui = at;
        }

        private void frmAtOcorrencia_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow item in co.carregaGridOcNovas().Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["cod"].GetHashCode();
                dataGridView1.Rows[n].Cells[1].Value = item["aluno"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["serie"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["sigla"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = Convert.ToDateTime(item["data"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells[5].Value = item["prof"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["motivo"].ToString();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            frmOcorrencias frm = new frmOcorrencias("");
            AuxClas.codigo = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.GetHashCode();
            AuxClas.NmArquivo = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            AuxClas.curso = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            AuxClas.sigla = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            AuxClas.dtEntrega = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            AuxClas.motivo = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            AuxClas.atualiza = atualiza;
            AuxClas.exclui = exclui;
            frm.Atualizar();
            frm.Show();
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace Copias
{
    public partial class frmImp : Form
    {
        string nome, prof;
        ConectaReq co = new ConectaReq();
        Requisicao re = new Requisicao();
        public frmImp(string nm)
        {
            InitializeComponent();
            nome = nm;
        }

        public void carregaGrid()
        {
            int cont = co.Impressao().Rows.Count;
            dataGridView1.Rows.Clear();
            foreach (DataRow item in co.Impressao().Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["cod"].GetHashCode();
                dataGridView1.Rows[n].Cells[1].Value = item["prof"].ToString();
                prof = item["prof"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["nmArquivo"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = Convert.ToDateTime(item["dtEntrega"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells[4].Value = item["quant"].GetHashCode();
                dataGridView1.Rows[n].Cells[5].Value = item["total"].GetHashCode();
                dataGridView1.Rows[n].Cells[6].Value = item["curso"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item["frente"].ToString();
                dataGridView1.Rows[n].Cells[8].Value = item["coord"].ToString();
                dataGridView1.Rows[n].Cells[9].Value = "Baixar";
            }
        }

        private void frmImp_Load(object sender, EventArgs e)
        {
            int cont = co.Impressao().Rows.Count;
            dataGridView1.Rows.Clear();
            foreach (DataRow item in co.Impressao().Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["cod"].GetHashCode();
                dataGridView1.Rows[n].Cells[1].Value = item["prof"].ToString();
                prof = item["prof"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["nmArquivo"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = Convert.ToDateTime(item["dtEntrega"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells[4].Value = item["quant"].GetHashCode();
                dataGridView1.Rows[n].Cells[5].Value = item["total"].GetHashCode();
                dataGridView1.Rows[n].Cells[6].Value = item["curso"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item["frente"].ToString();
                dataGridView1.Rows[n].Cells[8].Value = item["coord"].ToString();
                dataGridView1.Rows[n].Cells[9].Value = "Baixar";
            }
            Fechar();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Requisicao re = new Requisicao();
            ConectaReq co = new ConectaReq();
            int cod = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.GetHashCode();
            string arquivo = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            string prof = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            string resp = nome;
            string oc = "Sim";
            frmDestino frm = new frmDestino(cod, resp, oc, prof, arquivo);
            frm.Show();           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
            {
                re.Codigo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                co.SalvaArquivo(re);
            }
            carregaGrid();
        }
        
        public void Fechar()
        {
            timer1.Interval = 2000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            carregaGrid();
        }
    }
}

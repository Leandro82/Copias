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
    public partial class frmVerifica : Form
    {
        string nome;
        Requisicao re = new Requisicao();
        ConectaReq co = new ConectaReq();
        public frmVerifica(string nm)
        {
            InitializeComponent();
            nome = nm;
        }

        private void frmVerifica_Load(object sender, EventArgs e)
        {
            re.Professor = nome;
            
                dataGridView1.Rows.Clear();
                foreach (DataRow item in co.CarregaGridDestino(re).Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    if (item["autoriza"].ToString() == "Sim")
                    {
                        dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.Red;
                    }
                    dataGridView1.Rows[n].Cells[0].Value = item["cod"].GetHashCode();
                    dataGridView1.Rows[n].Cells[1].Value = item["nmArquivo"].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item["curso"].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = Convert.ToDateTime(item["data"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows[n].Cells[4].Value = item["respImp"].ToString();
                    dataGridView1.Rows[n].Cells[5].Value = item["destino"].ToString();
                    dataGridView1.Rows[n].Cells[6].Value = item["coord"].ToString();
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

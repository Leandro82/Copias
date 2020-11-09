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
using System.Net.Mail;

namespace Copias
{
    public partial class frmReq : Form
    {
        string prof, email;
        string coord;
        Requisicao re = new Requisicao();
        ConectaReq co = new ConectaReq();
        Usuario us = new Usuario();
        ConectaUs cn = new ConectaUs();
        Acoes ac = new Acoes();
        ConectaAc ca = new ConectaAc();
        string bt1;
        public frmReq(string nm)
        {
            InitializeComponent();
            coord = nm;
        }

        public void carregaGrid()
        {
            string data = co.BuscaDataServidor();
            int cont = co.CarregaGridReq().Rows.Count;
            dataGridView1.Rows.Clear();
            foreach (DataRow item in co.CarregaGridReq().Rows)
            {
                int n = dataGridView1.Rows.Add();
                if (item["autCoord"].ToString() == "Sim")
                {
                    dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.LawnGreen;
                }
                if (item["autoriza"].ToString() == "Sim")
                {
                    dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.Red;
                }
                dataGridView1.Rows[n].Cells[0].Value = item["cod"].GetHashCode();
                dataGridView1.Rows[n].Cells[1].Value = item["nmArquivo"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["prof"].ToString();
                prof = item["prof"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = Convert.ToDateTime(item["data"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells[4].Value = Convert.ToDateTime(item["dtEntrega"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells[5].Value = item["quant"].GetHashCode();
                dataGridView1.Rows[n].Cells[6].Value = item["curso"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item["frente"].ToString();
                dataGridView1.Rows[n].Cells[8].Value = "Baixar";
                dataGridView1.Rows[n].Cells[9].Value = "Autorizar";
                dataGridView1.Rows[n].Cells[10].Value = "X";
                TimeSpan date = Convert.ToDateTime(Convert.ToDateTime(dataGridView1.Rows[n].Cells[4].Value).AddDays(1).ToString("dd/MM/yyyy")) - Convert.ToDateTime(Convert.ToDateTime(data).ToString("dd/MM/yyyy"));
                int totalDias = date.Days;
                if (totalDias <= 0)
                {
                    ac.Requisição = Convert.ToInt32(dataGridView1.Rows[n].Cells[0].Value);
                    ac.Nome = "Sistema";
                    ac.Data = Convert.ToDateTime(co.BuscaDataServidor());
                    ac.Hora = co.BuscaHoraServidor();
                    ac.Acao = "Excluiu a Requisição " + dataGridView1.Rows[n].Cells[1].Value + " do(a) Prof(a) " + dataGridView1.Rows[n].Cells[2].Value;
                    ca.cadastro(ac);
                    re.Codigo = Convert.ToInt32(dataGridView1.Rows[n].Cells[0].Value);
                    co.excluir(re);
                    dataGridView1.Rows.RemoveAt(n);
                }
            }
        }

        private void frmReq_Load(object sender, EventArgs e)
        {
            string data = co.BuscaDataServidor();
                dataGridView1.Rows.Clear();
                foreach (DataRow item in co.CarregaGridReq().Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    if (item["autCoord"].ToString() == "Sim")
                    {
                        dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.LawnGreen;
                    }
                    if (item["autoriza"].ToString() == "Sim")
                    {
                        dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.Red;
                    }
                    dataGridView1.Rows[n].Cells[0].Value = item["cod"].GetHashCode();
                    dataGridView1.Rows[n].Cells[1].Value = item["nmArquivo"].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item["prof"].ToString();
                    prof = item["prof"].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = Convert.ToDateTime(item["data"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows[n].Cells[4].Value = Convert.ToDateTime(item["dtEntrega"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows[n].Cells[5].Value = item["quant"].GetHashCode();
                    dataGridView1.Rows[n].Cells[6].Value = item["curso"].ToString();
                    dataGridView1.Rows[n].Cells[7].Value = item["frente"].ToString();
                    dataGridView1.Rows[n].Cells[8].Value = "Baixar";
                    dataGridView1.Rows[n].Cells[9].Value = "Autorizar";
                    dataGridView1.Rows[n].Cells[10].Value = "X";
                    if (dataGridView1.Rows[n].DefaultCellStyle.BackColor != Color.LawnGreen)
                    {
                        TimeSpan date = Convert.ToDateTime(Convert.ToDateTime(dataGridView1.Rows[n].Cells[4].Value).AddDays(1).ToString("dd/MM/yyyy")) - Convert.ToDateTime(Convert.ToDateTime(data).ToString("dd/MM/yyyy"));
                        int totalDias = date.Days;
                        if (totalDias <= 0)
                        {
                            ac.Requisição = Convert.ToInt32(dataGridView1.Rows[n].Cells[0].Value);
                            ac.Nome = "Sistema";
                            ac.Data = Convert.ToDateTime(co.BuscaDataServidor());
                            ac.Hora = co.BuscaHoraServidor();
                            ac.Acao = "Excluiu a Requisição " + dataGridView1.Rows[n].Cells[1].Value +" do(a) Prof(a) "+dataGridView1.Rows[n].Cells[2].Value;
                            ca.cadastro(ac);
                            re.Codigo = Convert.ToInt32(dataGridView1.Rows[n].Cells[0].Value);
                            co.excluir(re);
                            dataGridView1.Rows.RemoveAt(n);
                        }
                    }
                }
            }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            var senderGrid2 = (DataGridView)sender;
            us.Nome = prof;
   
            foreach (DataRow item in cn.Email(us).Rows)
            {
                email = item["email"].ToString();
            }

            if (dataGridView1.Rows[e.RowIndex].Cells[8].Selected)
            {
                bt1 = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            }
            else if (dataGridView1.Rows[e.RowIndex].Cells[9].Selected)
            {
                bt1 = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            }
            else if (dataGridView1.Rows[e.RowIndex].Cells[10].Selected)
            {
                bt1 = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            }

            if (bt1 == "X")
            {
                if (senderGrid2.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    
                    string message = "Deseja recusar essa Requisição?";
                    string caption = "Confirmar";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;

                    result = MessageBox.Show(message, caption, buttons);

                    if (result == System.Windows.Forms.DialogResult.No)
                    {

                    }
                    else
                    {
                        int cod = dataGridView1.Rows[e.RowIndex].Cells[0].Value.GetHashCode();
                        DateTime data = Convert.ToDateTime(co.BuscaDataServidor());
                        string hora = co.BuscaHoraServidor();


                        re.Codigo = dataGridView1.Rows[e.RowIndex].Cells[0].Value.GetHashCode();
                        re.NaoAutoriza = "Sim";
                        re.Destino = "Não autorizado";
                        re.Autoriza = "Não";
                        re.Coordenador = coord;
                        co.naoAutorizar(re);
                       
                        if (email == "" || prof == "")
                        {
                        }
                        else
                        {
                            var peq = new frmMotivo(prof, email, coord, cod, data, hora);
                            if (Application.OpenForms.OfType<frmMotivo>().Count() > 0)
                            {
                                Application.OpenForms[peq.Name].Focus();
                            }
                            else
                            {
                                peq.Show();
                            } 
                        }
                        carregaGrid();
                    }

                }
            }
            else if (bt1 == "Baixar")
            {
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    re.Codigo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    co.SalvaArquivo(re);
                }
                carregaGrid();
            }
            else if (bt1 == "Autorizar")
            {
                ac.Requisição = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                ac.Nome = coord;
                ac.Data = Convert.ToDateTime(co.BuscaDataServidor());
                ac.Hora = co.BuscaHoraServidor();
                string prof1 = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                ac.Acao = "Autorizou a requisição do(a) Prof(a) " + prof1;
                ca.cadastro(ac);

                re.Codigo = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.GetHashCode();
                re.Coordenador = coord;
                re.Autoriza = "Sim";
                re.DataImpressao = Convert.ToDateTime(co.BuscaDataServidor());
                re.NaoAutoriza = "Não";
                re.Destino = "Aguardando impressão";
                co.liberaReq(re);
                string msg = "Requisição liberada para impressão";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                carregaGrid();
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

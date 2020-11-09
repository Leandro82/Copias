using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace Copias
{
    public partial class frmRelatorio : Form
    {
        string nm;
        ConectaUs us = new ConectaUs();
        ConectaCurso cc = new ConectaCurso();
        ConectaReq cq = new ConectaReq();
        Requisicao re = new Requisicao();
        DataTable data = new DataTable();
        SaveFileDialog salvarArquivo = new SaveFileDialog(); // novo
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;
        public frmRelatorio(string name)
        {
            InitializeComponent();
            nm = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false && radioButton3.Checked == false)
            {
                string msg = "Por favor, escolha uma opção.";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                if (radioButton3.Checked == true)
                {
                    re.Arquivo = "Geral";
                    int cont = cq.CarregaRelatorio(re).Rows.Count;
                    dataGridView1.Rows.Clear();
                    if (cont == 0)
                    {
                        string msg = "Não existe nenhuma requisição cadastrada.";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                    }
                }
                else if (radioButton1.Checked == true)
                {
                    if (comboBox1.Text == "")
                    {
                        string msg = "Selecione o professor.";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                        comboBox1.Focus();
                    }
                    else
                    {
                        re.Arquivo = "Prof";
                        re.Professor = comboBox1.Text;
                        int cont = cq.CarregaRelatorio(re).Rows.Count;
                        dataGridView1.Rows.Clear();
                        if (cont == 0)
                        {
                            string msg = "Não existe nenhuma requisição cadastrada para esse professor.";
                            frmMensagem mg = new frmMensagem(msg);
                            mg.ShowDialog();
                        }
                    }
                }
                int total = 0, linha = 0;
                dataGridView1.Rows.Clear();
                foreach (DataRow item in cq.CarregaRelatorio(re).Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item["cod"].GetHashCode();
                    dataGridView1.Rows[n].Cells[1].Value = item["prof"].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item["nmArquivo"].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = item["total"].GetHashCode();
                    dataGridView1.Rows[n].Cells[4].Value = item["coord"].ToString();
                    dataGridView1.Rows[n].Cells[5].Value = Convert.ToDateTime(item["dataImp"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows[n].Cells[6].Value = "Baixar";
                    total += item["total"].GetHashCode();
                    linha = linha + 1;
                }
                linha = dataGridView1.Rows.Add();
                dataGridView1.Rows[linha].Cells[0].Value = "";
                dataGridView1.Rows[linha].Cells[1].Value = "Total";
                dataGridView1.Rows[linha].Cells[2].Value = "";
                dataGridView1.Rows[linha].Cells[3].Value = total;
                dataGridView1.Rows[linha].Cells[4].Value = "";
                dataGridView1.Rows[linha].Cells[5].Value = "";
                dataGridView1.Rows[linha].Cells[6].Value = "";
            }
        }

        private void frmRelatorio_Load(object sender, EventArgs e)
        {
            int prf = us.Professor().Rows.Count;
            for (int j = 0; j < prf; j++)
            {
                comboBox1.Items.Add(us.Professor().Rows[j]["nome"].ToString());
            }

        }
 

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
            {
                re.Codigo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                cq.SalvaArquivo(re);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int cont = dataGridView1.Rows.Count;
            if (cont == 0)
            {
                string msg = "Não existe dados para gerar o excel";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                int l = 3;
                salvarArquivo.FileName = "Lista de Pedidos de Requisição";
                salvarArquivo.DefaultExt = "*.xls";
                salvarArquivo.Filter = "Todos os Aquivos do Excel (*.xls)|*.xls| Todos os arquivos (*.*)|*.*";

                try
                {
                    xlApp = new Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Add(misValue);

                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 6]].Merge();
                    xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 6]].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[1, 1] = "Lista de Pedido de Requisição";
                    xlWorkSheet.Cells[1, 1].ColumnWidth = 10;
                    xlWorkSheet.Cells[1, 2].ColumnWidth = 35;
                    xlWorkSheet.Cells[1, 3].ColumnWidth = 35;
                    xlWorkSheet.Cells[1, 4].ColumnWidth = 10;
                    xlWorkSheet.Cells[1, 5].ColumnWidth = 35;
                    xlWorkSheet.Cells[1, 6].ColumnWidth = 20;
                    xlWorkSheet.Cells[1, 1].Font.Size = 16;
                    xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 6]].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 1] = "Req. nº";
                    xlWorkSheet.Cells[2, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 2] = "Professor";
                    xlWorkSheet.Cells[2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 3] = "Arquivo";
                    xlWorkSheet.Cells[2, 3].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 4] = "Total";
                    xlWorkSheet.Cells[2, 4].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 5] = "Coordenador";
                    xlWorkSheet.Cells[2, 5].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 6] = "Data da Impressão";
                    xlWorkSheet.Cells[2, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 6].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Range[xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, 6]].Font.Size = 12;
                    int quant = cq.CarregaRelatorio(re).Rows.Count;
                    progressBar1.Visible = true;
                    progressBar1.Maximum = quant;

                    int total = 0;
                    for (int i = 0; i < quant; i++)
                    {
                        xlWorkSheet.Cells[l, 1] = dataGridView1.Rows[i].Cells[0].Value.GetHashCode();
                        xlWorkSheet.Cells[l, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                        xlWorkSheet.Cells[l, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 2] = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        xlWorkSheet.Cells[l, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 3] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                        xlWorkSheet.Cells[l, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 4] = dataGridView1.Rows[i].Cells[3].Value.GetHashCode();
                        xlWorkSheet.Cells[l, 4].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                        xlWorkSheet.Cells[l, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 5] = dataGridView1.Rows[i].Cells[4].Value.ToString();
                        xlWorkSheet.Cells[l, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 6] = dataGridView1.Rows[i].Cells[5].Value.ToString();
                        xlWorkSheet.Cells[l, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                        xlWorkSheet.Cells[l, 6].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        l = l + 1;
                        total += Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value.GetHashCode());
                        progressBar1.Value++;
                    }
                    xlWorkSheet.Cells[l, 2] = "Total";
                    xlWorkSheet.Cells[l, 4] = total;
                    xlWorkSheet.Cells[l, 4].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    progressBar1.Value = 0;
                    progressBar1.Visible = false;


                    new System.Threading.Thread(delegate()
                    {
                        Export();
                    }).Start();
                }
                catch (Exception ex)
                {
                    string msg = "Erro : " + ex.Message;
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
            }
        }

        private void Export()
        {
            System.Threading.Thread arquivo = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
             {
                 if (salvarArquivo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                 {
                     xlWorkBook.SaveAs(salvarArquivo.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                     Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                     xlWorkBook.Close(true, misValue, misValue);
                     xlApp.Quit();

                     liberarObjetos(xlWorkSheet);
                     liberarObjetos(xlWorkBook);
                     liberarObjetos(xlApp);
                 }
             }));
            arquivo.SetApartmentState(System.Threading.ApartmentState.STA);
            arquivo.IsBackground = false;
            arquivo.Start();
        }
            

        private void liberarObjetos(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                string msg = "Ocorreu um erro durante a liberação do objeto " + ex.ToString();
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            finally
            {
                GC.Collect();
            }
        }

        public void Fechar()
        {
            timer1.Interval = 2000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
        }

    }
}

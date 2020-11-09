using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace Copias
{
    public partial class frmRelCopiasProf : Form
    {
        ConectaReq co = new ConectaReq();
        Requisicao re = new Requisicao();
        SaveFileDialog salvarArquivo = new SaveFileDialog(); // novo
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;
        frmCarregando cr = new frmCarregando();

        public frmRelCopiasProf()
        {
            InitializeComponent();
        }

        private void CarregaFormAguarde()
        {
            cr.ShowDialog();
        }

        private void frmRelCopiasProf_Load(object sender, EventArgs e)
        {
            System.Threading.Thread tFormAguarde = new System.Threading.Thread(new System.Threading.ThreadStart(CarregaFormAguarde));
            tFormAguarde.Start();
            dataGridView1.Rows.Clear();
            foreach (DataRow item in co.CarregaGridCopiasProf().Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["cod"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["nome"].ToString();
                re.Professor = item["nome"].ToString();
                co.ContaCopias(re);
                if (co.ContaCopias(re).ToString() != "")
                {
                    dataGridView1.Rows[n].Cells[2].Value = co.ContaCopias(re).ToString();
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "0";
                }
            }
            tFormAguarde.Abort();
        }

        private void button1_Click(object sender, EventArgs e)
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
                salvarArquivo.FileName = "Cópias por Professor";
                salvarArquivo.DefaultExt = "*.xls";
                salvarArquivo.Filter = "Todos os Aquivos do Excel (*.xls)|*.xls| Todos os arquivos (*.*)|*.*";

                try
                {
                    xlApp = new Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Add(misValue);

                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 2]].Merge();
                    xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 2]].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[1, 1] = "Relatório de Cópias";
                    xlWorkSheet.Cells[1, 1].Font.Bold = true;
                    xlWorkSheet.Cells[1, 1].ColumnWidth = 35;
                    xlWorkSheet.Cells[1, 2].ColumnWidth = 10;
                    xlWorkSheet.Cells[1, 1].Font.Size = 16;
                    xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 2]].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 1] = "Professor";
                    xlWorkSheet.Cells[2, 1].Font.Size = 14;
                    xlWorkSheet.Cells[2, 1].Font.Bold = true;
                    xlWorkSheet.Cells[2, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 2] = "Total";
                    xlWorkSheet.Cells[2, 2].Font.Size = 14;
                    xlWorkSheet.Cells[2, 2].Font.Bold = true;
                    xlWorkSheet.Cells[2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Range[xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, 2]].Font.Size = 12;
                    int quant = dataGridView1.Rows.Count;

                    for (int i = 0; i < quant; i++)
                    {
                        xlWorkSheet.Cells[l, 1] = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        xlWorkSheet.Cells[l, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 2] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                        xlWorkSheet.Cells[l, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                        l = l + 1;
                    }

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
    }
}

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
    public partial class frmRelOc : Form
    {
        ConectaOc co = new ConectaOc();
        Ocorrencia oc = new Ocorrencia();
        ConectaCurso cs = new ConectaCurso();
        ConectaUs cn = new ConectaUs();
        SaveFileDialog salvarArquivo = new SaveFileDialog(); // novo
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;
        public frmRelOc()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            dateTimePicker1.Enabled = false;
            comboBox1.Text = "";
            comboBox2.Text = "";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            comboBox1.Enabled = true;
            comboBox2.Enabled = false;
            dateTimePicker1.Enabled = false;
            textBox1.Text = "";
            comboBox2.Text = "";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            dateTimePicker1.Enabled = true;
            textBox1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = true;
            dateTimePicker1.Enabled = false;
            textBox1.Text = "";
            comboBox1.Text = "";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            dateTimePicker1.Enabled = false;
            textBox1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            dateTimePicker1.Enabled = false;
            textBox1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false && radioButton4.Checked == false && radioButton5.Checked == false && radioButton6.Checked == false)
            {
                string msg = "SELECIONAR UMA OPÇÃO!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                if (radioButton1.Checked == true)
                {
                    oc.Aluno = textBox1.Text;
                    int cont = co.carregaGridOcNome(oc).Rows.Count;
                    dataGridView1.Rows.Clear();
                    if (cont == 0)
                    {
                        string msg = "NÃO EXISTE DADOS PARA ESSA PESQUISA";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                    }
                    else
                    {
                        if (textBox1.Text == "")
                        {
                            string msg = "INFORMAR O NOME DO ALUNO";
                            frmMensagem mg = new frmMensagem(msg);
                            mg.ShowDialog();
                            textBox1.Focus();
                        }
                        else
                        {
                            foreach (DataRow item in co.carregaGridOcNome(oc).Rows)
                            {
                                int n = dataGridView1.Rows.Add();
                                dataGridView1.Rows[n].Cells[1].Value = item["cod"].GetHashCode();
                                dataGridView1.Rows[n].Cells[2].Value = item["aluno"].ToString();
                                dataGridView1.Rows[n].Cells[3].Value = item["serie"].ToString();
                                dataGridView1.Rows[n].Cells[4].Value = item["sigla"].ToString();
                                dataGridView1.Rows[n].Cells[5].Value = Convert.ToDateTime(item["data"].ToString()).ToString("dd/MM/yyyy");
                                dataGridView1.Rows[n].Cells[6].Value = item["prof"].ToString();
                                dataGridView1.Rows[n].Cells[7].Value = item["motivo"].ToString();
                            }
                        }
                    }
                }
                else if (radioButton2.Checked == true)
                {
                    oc.Serie = comboBox1.Text;
                    int cont = co.carregaGridOcSerie(oc).Rows.Count;
                    dataGridView1.Rows.Clear();
                    if (cont == 0)
                    {
                        string msg = "NÃO EXISTE DADOS PARA ESSA PESQUISA";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                    }
                    else
                    {
                        if (comboBox1.Text == "")
                        {
                            string msg = "INFORMAR A CLASSE";
                            frmMensagem mg = new frmMensagem(msg);
                            mg.ShowDialog();
                            comboBox1.Focus();
                        }
                        else
                        {
                            foreach (DataRow item in co.carregaGridOcSerie(oc).Rows)
                            {
                                int n = dataGridView1.Rows.Add();
                                dataGridView1.Rows[n].Cells[1].Value = item["cod"].GetHashCode();
                                dataGridView1.Rows[n].Cells[2].Value = item["aluno"].ToString();
                                dataGridView1.Rows[n].Cells[3].Value = item["serie"].ToString();
                                dataGridView1.Rows[n].Cells[4].Value = item["sigla"].ToString();
                                dataGridView1.Rows[n].Cells[5].Value = Convert.ToDateTime(item["data"].ToString()).ToString("dd/MM/yyyy");
                                dataGridView1.Rows[n].Cells[6].Value = item["prof"].ToString();
                                dataGridView1.Rows[n].Cells[7].Value = item["motivo"].ToString();
                            }
                        }
                    }
                }
                else if (radioButton3.Checked == true)
                {
                    oc.Data = Convert.ToDateTime(dateTimePicker1.Text);
                    int cont = co.carregaGridOcData(oc).Rows.Count;
                    dataGridView1.Rows.Clear();
                    if (cont == 0)
                    {
                        string msg = "NÃO EXISTE DADOS PARA ESSA PESQUISA";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                    }
                    else
                    {
                        foreach (DataRow item in co.carregaGridOcData(oc).Rows)
                        {
                            int n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells[1].Value = item["cod"].GetHashCode();
                            dataGridView1.Rows[n].Cells[2].Value = item["aluno"].ToString();
                            dataGridView1.Rows[n].Cells[3].Value = item["serie"].ToString();
                            dataGridView1.Rows[n].Cells[4].Value = item["sigla"].ToString();
                            dataGridView1.Rows[n].Cells[5].Value = Convert.ToDateTime(item["data"].ToString()).ToString("dd/MM/yyyy");
                            dataGridView1.Rows[n].Cells[6].Value = item["prof"].ToString();
                            dataGridView1.Rows[n].Cells[7].Value = item["motivo"].ToString();
                        }
                    }
                }
                else if (radioButton4.Checked == true)
                {
                    oc.Professor = comboBox2.Text;
                    int cont = co.carregaGridOcProf(oc).Rows.Count;
                    dataGridView1.Rows.Clear();
                    if (cont == 0)
                    {
                        string msg = "NÃO EXISTE DADOS PARA ESSA PESQUISA";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                    }
                    else
                    {
                        if (comboBox2.Text == "")
                        {
                            string msg = "INFORMAR O PROFESSOR";
                            frmMensagem mg = new frmMensagem(msg);
                            mg.ShowDialog();
                            comboBox2.Focus();
                        }
                        else
                        {
                            foreach (DataRow item in co.carregaGridOcProf(oc).Rows)
                            {
                                int n = dataGridView1.Rows.Add();
                                dataGridView1.Rows[n].Cells[1].Value = item["cod"].GetHashCode();
                                dataGridView1.Rows[n].Cells[2].Value = item["aluno"].ToString();
                                dataGridView1.Rows[n].Cells[3].Value = item["serie"].ToString();
                                dataGridView1.Rows[n].Cells[4].Value = item["sigla"].ToString();
                                dataGridView1.Rows[n].Cells[5].Value = Convert.ToDateTime(item["data"].ToString()).ToString("dd/MM/yyyy");
                                dataGridView1.Rows[n].Cells[6].Value = item["prof"].ToString();
                                dataGridView1.Rows[n].Cells[7].Value = item["motivo"].ToString();
                            }
                        }
                    }
                }
                else if (radioButton5.Checked == true)
                {
                    int cont = co.carregaGridOcTd(oc).Rows.Count;
                    dataGridView1.Rows.Clear();
                    if (cont == 0)
                    {
                        string msg = "NÃO EXISTE DADOS PARA ESSA PESQUISA";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                    }
                    else
                    {
                        foreach (DataRow item in co.carregaGridOcTd(oc).Rows)
                        {
                            int n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells[1].Value = item["cod"].GetHashCode();
                            dataGridView1.Rows[n].Cells[2].Value = item["aluno"].ToString();
                            dataGridView1.Rows[n].Cells[3].Value = item["serie"].ToString();
                            dataGridView1.Rows[n].Cells[4].Value = item["sigla"].ToString();
                            dataGridView1.Rows[n].Cells[5].Value = Convert.ToDateTime(item["data"].ToString()).ToString("dd/MM/yyyy");
                            dataGridView1.Rows[n].Cells[6].Value = item["prof"].ToString();
                            dataGridView1.Rows[n].Cells[7].Value = item["motivo"].ToString();
                            if (item["gerado"].ToString() == "Sim")
                            {
                                dataGridView1.Rows[n].Cells[0].Value = true;
                            }
                        }
                    }
                }
                else if (radioButton6.Checked == true)
                {
                    int cont = co.carregaGridOcTd(oc).Rows.Count;
                    dataGridView1.Rows.Clear();
                    if (cont == 0)
                    {
                        string msg = "NÃO EXISTE DADOS PARA ESSA PESQUISA";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                    }
                    else
                    {
                        foreach (DataRow item in co.carregaGridOcNovas().Rows)
                        {
                            int n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells[1].Value = item["cod"].GetHashCode();
                            dataGridView1.Rows[n].Cells[2].Value = item["aluno"].ToString();
                            dataGridView1.Rows[n].Cells[3].Value = item["serie"].ToString();
                            dataGridView1.Rows[n].Cells[4].Value = item["sigla"].ToString();
                            dataGridView1.Rows[n].Cells[5].Value = Convert.ToDateTime(item["data"].ToString()).ToString("dd/MM/yyyy");
                            dataGridView1.Rows[n].Cells[6].Value = item["prof"].ToString();
                            dataGridView1.Rows[n].Cells[7].Value = item["motivo"].ToString();
                            if (item["gerado"].ToString() == "Sim")
                            {
                                dataGridView1.Rows[n].Cells[0].Value = true;
                            }
                        }
                    }
                }
            }
        }

        private void frmRelOc_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
            comboBox2.Enabled = false;

            int cont = cs.Curso().Rows.Count;
            for (int i = 0; i < cont; i++)
            {
                comboBox1.Items.Add(cs.Curso().Rows[i]["modulo"].ToString() + " " + cs.Curso().Rows[i]["nome"].ToString());
            }

            int prf = cn.Professor().Rows.Count;
            for (int j = 0; j < prf; j++)
            {
                comboBox2.Items.Add(cn.Professor().Rows[j]["nome"].ToString());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns[0].Index)
            {
                dataGridView1.EndEdit();  //Stop editing of cell.
                int cod = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                if ((bool)dataGridView1.Rows[e.RowIndex].Cells[0].Value)
                {
                    oc.Codigo = cod;
                    oc.Gerado = "Sim";
                    co.atualizarGerados(oc);
                }
                else
                {
                    oc.Codigo = cod;
                    oc.Gerado = "Não";
                    co.atualizarGerados(oc);
                }
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
                salvarArquivo.FileName = "Lista de Ocorrências";
                salvarArquivo.DefaultExt = "*.xls";
                salvarArquivo.Filter = "Todos os Aquivos do Excel (*.xls)|*.xls| Todos os arquivos (*.*)|*.*";

                try
                {
                    xlApp = new Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Add(misValue);

                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 6]].Merge();
                    xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 6]].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[1, 1] = "Lista de Ocorrências";
                    xlWorkSheet.Cells[1, 1].ColumnWidth = 35;
                    xlWorkSheet.Cells[1, 2].ColumnWidth = 35;
                    xlWorkSheet.Cells[1, 3].ColumnWidth = 35;
                    xlWorkSheet.Cells[1, 4].ColumnWidth = 15;
                    xlWorkSheet.Cells[1, 5].ColumnWidth = 35;
                    xlWorkSheet.Cells[1, 6].ColumnWidth = 35;
                    xlWorkSheet.Cells[1, 1].Font.Size = 16;
                    xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 6]].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 1] = "Aluno";
                    xlWorkSheet.Cells[2, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 2] = "Série";
                    xlWorkSheet.Cells[2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 3] = "Componente";
                    xlWorkSheet.Cells[2, 3].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 4] = "Data";
                    xlWorkSheet.Cells[2, 4].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 5] = "Professor";
                    xlWorkSheet.Cells[2, 5].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 6] = "Motivo";
                    xlWorkSheet.Cells[2, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 6].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Range[xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, 6]].Font.Size = 12;
                    int quant = dataGridView1.Rows.Count;
                   
                    for (int i = 0; i < quant; i++)
                    {
                        xlWorkSheet.Cells[l, 1] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                        xlWorkSheet.Cells[l, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 2] = dataGridView1.Rows[i].Cells[3].Value.ToString();
                        xlWorkSheet.Cells[l, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 3] = dataGridView1.Rows[i].Cells[4].Value.ToString();
                        xlWorkSheet.Cells[l, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        Range rg = (Excel.Range)xlWorkSheet.Cells[l, 4];
                        rg.EntireColumn.NumberFormat ="dd/MM";
                        xlWorkSheet.Cells[l, 4] = Convert.ToDateTime(dataGridView1.Rows[i].Cells[5].Value.ToString());
                        xlWorkSheet.Cells[l, 4].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                        xlWorkSheet.Cells[l, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 5] = dataGridView1.Rows[i].Cells[6].Value.ToString();
                        xlWorkSheet.Cells[l, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 6] = dataGridView1.Rows[i].Cells[7].Value.ToString();
                        xlWorkSheet.Cells[l, 6].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (co.carregaGridOcNovas().Rows.Count == 0)
            {
                string msg = "NÃO HÁ BILHETES PARA SER GERADOS!!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                var peq = new frmRelatorioOc();
                if (System.Windows.Forms.Application.OpenForms.OfType<frmRelatorioOc>().Count() > 0)
                {
                    System.Windows.Forms.Application.OpenForms[peq.Name].Focus();
                }
                else
                {
                    peq.ShowDialog();
                }
            }
        }
    }
}

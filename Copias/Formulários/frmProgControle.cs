using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net.Mail;


namespace Copias
{
    public partial class frmProgControle : Form
    {
        frmProcesso f = new frmProcesso();
        Progressao pr = new Progressao();
        ConectaProg co = new ConectaProg();
        ConectaUs cs = new ConectaUs();
        ConectaUs cn = new ConectaUs();
        Usuario us = new Usuario();
        SaveFileDialog salvarArquivo = new SaveFileDialog(); // novo
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;
        string email, botao, prof, aluno, comp, turma, sem, cargo, cod, func1, func2, texto;
        int linha;

        public frmProgControle(string nm, string cd)
        {
            InitializeComponent();
            cargo = nm;
            cod = cd;
        }

        private void CarregaFormAguarde()
        {
            AuxClas.processo = "Enviando e-mail ao Professor...";
            f.Atualizar();
            f.ShowDialog();
        }

        private void frmProgControle_Load(object sender, EventArgs e)
        {
            us.Codigo = Convert.ToInt32(cod);

            textBox2.Text = "Prezado(a) Prof. (a) Nome do Professor, considerando a Progressão do(a) aluno(a) Nome do Aluno sob sua responsabilidade: Aluno(a): Nome do Aluno, componente: Nome do Componente, turma: Turma da PP de(o) Semestre da PP.";
            foreach (DataRow item in cn.Menu(us).Rows)
            {
                func1 = item["func1"].ToString();
                func2 = item["func2"].ToString();
            }

            progressBar1.Visible = false;
            dataGridView1.Rows.Clear();
            foreach (DataRow item in co.ListaProgressao().Rows)
            {
                foreach (DataRow grid in cs.Grid().Rows)
                {
                    if (item["professor"].ToString() == grid["nome"].ToString())
                    {
                        email = grid["email"].ToString();
                    }
                }

                int n = dataGridView1.Rows.Add();
                if (co.ListaProgressao().Rows[n]["entrega"].ToString() != "Sim")
                {
                    dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.GreenYellow;
                }
                dataGridView1.Rows[n].Cells[1].Value = item["portaria"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["nome"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["componente"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["professor"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = email;
                dataGridView1.Rows[n].Cells[6].Value = item["turma"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item["semestreAno"].ToString();
                dataGridView1.Rows[n].Cells[8].Value = item["modSerieAtual"].ToString();
                dataGridView1.Rows[n].Cells[0].Value = "Enviar";
                email = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int cont = dataGridView1.Rows.Count;
            if (cont == 0)
            {
                string msg = "NÃO EXISTE DADOS PARA GERAR O EXCEL";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                int l = 3;
                salvarArquivo.FileName = "Lista de Progressões";
                salvarArquivo.DefaultExt = "*.xls";
                salvarArquivo.Filter = "Todos os Aquivos do Excel (*.xls)|*.xls| Todos os arquivos (*.*)|*.*";

                try
                {
                    xlApp = new Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Add(misValue);

                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    xlWorkSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                    xlWorkSheet.PageSetup.TopMargin = 2;
                    xlWorkSheet.PageSetup.BottomMargin = 1;
                    xlWorkSheet.PageSetup.LeftMargin = 0;
                    xlWorkSheet.PageSetup.RightMargin = 0;
                    xlWorkSheet.PageSetup.PrintTitleRows = "$A$2:$G$2";
                    xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 8]].Merge();
                    xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 8]].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[1, 1] = "Lista de Progressão Parcial";
                    xlWorkSheet.Cells[1, 1].ColumnWidth = 13;
                    xlWorkSheet.Cells[1, 2].ColumnWidth = 39;
                    xlWorkSheet.Cells[1, 3].ColumnWidth = 39;
                    xlWorkSheet.Cells[1, 4].ColumnWidth = 39;
                    xlWorkSheet.Cells[1, 5].ColumnWidth = 39;
                    xlWorkSheet.Cells[1, 6].ColumnWidth = 39;
                    xlWorkSheet.Cells[1, 7].ColumnWidth = 15;
                    xlWorkSheet.Cells[1, 8].ColumnWidth = 7;
                    xlWorkSheet.Cells[1, 1].Font.Size = 16;
                    xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 8]].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 1] = "PORTARIA";
                    xlWorkSheet.Cells[2, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 2] = "NOME";
                    xlWorkSheet.Cells[2, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 3] = "COMPONENTE";
                    xlWorkSheet.Cells[2, 3].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 4] = "PROFESSOR";
                    xlWorkSheet.Cells[2, 4].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 5] = "E-Mail";
                    xlWorkSheet.Cells[2, 5].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 6] = "TURMA DA PP";
                    xlWorkSheet.Cells[2, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 6].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 7] = "SEMESTRE/ANO";
                    xlWorkSheet.Cells[2, 7].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 7].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlWorkSheet.Cells[2, 8] = "ATUAL";
                    xlWorkSheet.Cells[2, 8].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                    xlWorkSheet.Cells[2, 8].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                    xlWorkSheet.Range[xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, 8]].Font.Size = 12;
                    int quant = dataGridView1.Rows.Count;

                    progressBar1.Visible = true;
                    progressBar1.Maximum = quant;
                    for (int i = 0; i < quant; i++)
                    {
                        if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.GreenYellow)
                        {
                            xlWorkSheet.Range[xlWorkSheet.Cells[l, 1], xlWorkSheet.Cells[l, 8]].Interior.Color = ColorTranslator.ToWin32(Color.Yellow);
                        }
                        xlWorkSheet.Cells[l, 1] = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        xlWorkSheet.Cells[l, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                        xlWorkSheet.Cells[l, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 2] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                        xlWorkSheet.Cells[l, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 3] = dataGridView1.Rows[i].Cells[3].Value.ToString();
                        xlWorkSheet.Cells[l, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 4] = dataGridView1.Rows[i].Cells[4].Value.ToString();
                        xlWorkSheet.Cells[l, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 5] = dataGridView1.Rows[i].Cells[5].Value.ToString();
                        xlWorkSheet.Cells[l, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 6] = dataGridView1.Rows[i].Cells[6].Value.ToString();
                        xlWorkSheet.Cells[l, 6].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 7] = dataGridView1.Rows[i].Cells[7].Value.ToString();
                        xlWorkSheet.Cells[l, 7].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                        xlWorkSheet.Cells[l, 7].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        xlWorkSheet.Cells[l, 8] = dataGridView1.Rows[i].Cells[8].Value.ToString();
                        xlWorkSheet.Cells[l, 8].HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
                        xlWorkSheet.Cells[l, 8].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        l = l + 1;
                        progressBar1.Value++;
                    }
                    xlWorkSheet.Application.Columns[2].ShrinkToFit = true;
                    xlWorkSheet.Application.Columns[3].ShrinkToFit = true;
                    xlWorkSheet.Application.Columns[4].ShrinkToFit = true;
                    xlWorkSheet.Application.Columns[5].ShrinkToFit = true;
                    xlWorkSheet.Application.Columns[6].ShrinkToFit = true;
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

        public void EnviarEmail(string al, string cmp, string trm, string smt, string professor, string em, string txt)
        {
            
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("copias027@gmail.com", "ete027oc");
            MailMessage mail = new MailMessage();
            mail.Sender = new System.Net.Mail.MailAddress("copias027@gmail.com", "Etec Amim Jundi");
            mail.From = new MailAddress("copias027@gmail.com", "Etec Amim Jundi");
            mail.To.Add(new MailAddress(em, professor));
            mail.Subject = "Progressão Parcial Pendente";
            mail.Body = "Prezado(a) Prof. (a) " + professor + ", considerando a Progressão do(a) aluno(a) " + al + " sob sua responsabilidade: Aluno(a): " + al + ", componente: " + cmp + ", turma: " + trm + " de(o) " + smt + ".<br>" + txt + 
                "<br>" + "<br>" + "<i><b><font size=2><font color=red>Este e-mail foi encaminhado apenas como aviso, por favor não responder. Qualquer dúvida procure o(a) Orientador(a) Educacional, Coordenador(a) Pedagógico(a) ou o(a) Coordenador(a) de Curso.</font></i></b><br>" + 
                "<br>" + "<br>" + "<br>" + "Att.<br>" + "Escola Técnica Estadual Amim Jundi<br>" + "Rua Japão, 724 - Centro<br>" + "Telefones: (18) 3528-3982  -  (18) 3528-4760<br>";
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            try
            {
                try
                {
                    System.Threading.Thread tFormAguarde = new System.Threading.Thread(new System.Threading.ThreadStart(CarregaFormAguarde));
                    tFormAguarde.Start();
                    client.Send(mail);
                    tFormAguarde.Abort();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                //f.Close();
                string msg = "EMAIL ENVIADO!!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            catch (System.Exception erro)
            {
                MessageBox.Show("Erro: " + erro);
            }
        }

        public bool MeuProcessoFoiFinalizado { get; set; }

        private void FazMeusProcessos()
        {
            MeuProcessoFoiFinalizado = false;

            // Meus Processos

            MeuProcessoFoiFinalizado = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (dataGridView1.Rows[e.RowIndex].Cells[0].Selected)
            {
                botao = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                email = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                prof = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                sem = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                turma = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                aluno = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                comp = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                texto = textBox1.Text;
                linha = e.RowIndex;
            }
            else 
            {
                botao = "";
            }

            if (botao == "Enviar")
            {
                if (dataGridView1.Rows[linha].DefaultCellStyle.BackColor == Color.GreenYellow)
                {
                    string msg = "PROFESSOR(A) AINDA NÃO PREENCHEU A PP!!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    if (textBox1.Text == "")
                    {
                        string msg = "DIGITAR A MENSAGEM QUE SERÁ ENVIADA!!!";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                        textBox1.Focus();
                    }
                    else if (email == "")
                    {
                        string msg = "PROFESSOR(A) SEM E-MAIL, POR FAVOR, COMUNICAR A SECRETARIA!!!";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                    }
                    else
                    {
                        EnviarEmail(aluno, comp, turma, sem, prof, email, texto);
                    }
                }
            }
        } 
   }
}

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
using System.Drawing.Imaging;
using System.Net.Mail;

namespace Copias
{
    public partial class frmCopias : Form
    {
        string des, atualiza, exclui, cd, cd2, func, curso, prof, func1, libData, comp, dosc;
        int cod;
        string frente;
        string name, coord;
        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        MailMessage mail = new MailMessage();        
        frmProcesso f = new frmProcesso();

        Acoes ac = new Acoes();
        ConectaAc ca = new ConectaAc();
        Requisicao re = new Requisicao();
        ConectaReq co = new ConectaReq();
        ConectaCurso cs = new ConectaCurso();
        Curso cc = new Curso();
        Usuario us = new Usuario();
        ConectaUs cn = new ConectaUs();
        DialogResult result;
        OpenFileDialog arquivo = new OpenFileDialog();

        public frmCopias(string nm)
        {
            InitializeComponent();
            coord = nm;
        }

        private void CarregaFormAguarde()
        {
            AuxClas.processo = "Enviando e-mail ao Coordenador...";
            f.Atualizar();
            f.ShowDialog();
        }

        public void Atualizar()
        {
            cod = AuxClas.codigo;
            textBox1.Text = AuxClas.NmArquivo;
            dateTimePicker1.Text = AuxClas.dtEntrega;
            dosc = AuxClas.professor;
            textBox2.Text = AuxClas.quantidade;
            textBox3.Text = AuxClas.total;
            comp = AuxClas.curso;
            frente = AuxClas.frente;
            name = AuxClas.professor;
            atualiza = AuxClas.atualiza;
            exclui = AuxClas.exclui;
            if (frente == "Sim")
            {
                radioButton1.Checked = true;
            }
            else if (frente == "Não")
            {
                radioButton2.Checked = true;
            }
        }

        private void BloqueiaData()
        {
            libData = co.BuscaLibData();
            if (libData == "Não")
            {
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
                var horaf = Convert.ToDateTime("12:00 PM");
                string hora = co.BuscaHoraServidor();
                string horateste = String.Format(ci, "{0:hh:mm tt}", Convert.ToDateTime(hora));
                var horain = Convert.ToDateTime(horateste);
                string dia = co.BuscaDiaServidor();
                if (dia == "6" && horaf < horain)
                {
                    this.dateTimePicker1.MinDate = Convert.ToDateTime(co.BuscaDataServidor()).AddDays(4);
                }
                else if (dia == "6" && horaf > horain)
                {
                    this.dateTimePicker1.MinDate = Convert.ToDateTime(co.BuscaDataServidor()).AddDays(3);
                }
                else if (dia == "Saturday")
                {
                    this.dateTimePicker1.MinDate = Convert.ToDateTime(co.BuscaDataServidor()).AddDays(3);
                }
                else if (dia == "Sunday")
                {
                    this.dateTimePicker1.MinDate = Convert.ToDateTime(co.BuscaDataServidor()).AddDays(2);
                }
                else
                {
                    this.dateTimePicker1.MinDate = Convert.ToDateTime(co.BuscaDataServidor()).AddDays(1);
                }
            }
            else
            {
                dateTimePicker1.MinDate = DateTime.Today;
                dateTimePicker1.Value = DateTime.Today;
            }
        }

        private void frmCopias_Load(object sender, EventArgs e)
        {
            libData = co.BuscaLibData();
            int cont = cs.Curso().Rows.Count;

            for (int i = 0; i < cont; i++)
            {
                comboBox2.Items.Add(cs.Curso().Rows[i]["modulo"].ToString() + " " + cs.Curso().Rows[i]["nome"].ToString());
            }

            int prf = cn.Professor().Rows.Count;
            for (int j = 0; j < prf; j++)
            {
                comboBox1.Items.Add(cn.Professor().Rows[j]["nome"].ToString());
            }

            if (libData == "Não")
            {
                radioButton3.Checked = true;
                radioButton4.Checked = false;
            }
            else if (libData == "Sim")
            {
                radioButton4.Checked = true;
                radioButton3.Checked = false;
            }

            us.Codigo = AuxClas.LibData;
            foreach (DataRow item in cn.Menu(us).Rows)
            {
                func1 = item["func1"].ToString();
            }

            if (func1 != "Secretaria" && func1 != "Direção")
            {
                groupBox3.Visible = false;
            }
            else
            {
                groupBox3.Visible = true;
            }

            BloqueiaData();
            
            
            if (atualiza == "atualiza")
            {
                comboBox1.SelectedItem = dosc;
                comboBox2.SelectedItem = comp;
                button2.Enabled = false;
                button4.Enabled = false;
            }

            else if (exclui == "exclui")
            {
                comboBox1.SelectedItem = dosc;
                comboBox2.SelectedItem = comp;
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || radioButton1.Checked == false && radioButton2.Checked == false)
            {
                string msg = "Preencha todos os campos";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                curso = comboBox2.Text;
                prof = comboBox1.Text;
                cc.Nome = (comboBox2.Text).Substring(3);
                us.Nome = name;

                string message = "Deseja anexar um arquivo?";
                string caption = "Escolha uma opção";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                
                result = MessageBox.Show(message, caption, buttons);

                foreach (DataRow item in cn.Menu(us).Rows)
                {
                    func = item["func1"].ToString();
                }

                if (func == "Secretaria" || func == "Direção")
                {
                    foreach (DataRow item in cs.CoordEmail(cc).Rows)
                    {
                        cd = item["coord"].ToString();
                        cd2 = item["coord2"].ToString();
                    }

                    us.Nome = cd;
                    us.Profissao2 = cd2;
                    
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;
                    client.Credentials = new System.Net.NetworkCredential("copias027@gmail.com", "ete027oc");                   
                    mail.Sender = new System.Net.Mail.MailAddress("copias027@gmail.com", "Etec Amim Jundi");
                    mail.From = new MailAddress("copias027@gmail.com", "Etec Amim Jundi");
                    foreach (DataRow item in cs.EmailCopia(us).Rows)
                    {
                        mail.To.Add(new MailAddress(item["email"].ToString(), item["nome"].ToString()));
                    }
                    mail.Subject = "Lembrete";
                    mail.Body = "Caro(a) Coordenador(a), você tem uma requisição de cópias para liberar no sistema de Requisição, do(a) Prof.(a) " + prof + " que será usado no " + curso + " no dia " + Convert.ToDateTime(dateTimePicker1.Text).ToString("dd/MM/yyyy") + ". Por favor, verifique assim que possível, caso passe da data solicitada pelo professor, o sistema irá deletar o pedido automaticamente.<br>" + 
                        "<br>" + "<br>" + "<i><b><font size=2><font color=red>Este e-mail foi encaminhado apenas como aviso, por favor não responder.</font></i></b><br>" +
                        "<br>" + "<br>" + "<br>" + "Att.<br>" + "Escola Técnica Estadual Amim Jundi<br>" + "Rua Japão, 724 - Centro<br>" + "Telefones: (18) 3528-3982  -  (18) 3528-4760<br>";
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    
                }

                new System.Threading.Thread(delegate()
                {
                    Carrega();
                }).Start();
            }
        }

        public void Carrega()
        {
            System.Threading.Thread arquivo1 = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
             {
                 if (radioButton1.Checked == true)
                 {
                     des = "Sim";
                 }
                 else if (radioButton2.Checked == true)
                 {
                     des = "Não";
                 }

                 if (result == System.Windows.Forms.DialogResult.Yes)
                 {
                     if (arquivo.ShowDialog() == DialogResult.OK)
                     {
                         this.Invoke((MethodInvoker)delegate()
                         {
                             re.Nome = textBox1.Text;
                             re.Professor = comboBox1.Text;
                             re.QtCopias = Convert.ToInt32(textBox2.Text);
                             re.Paginas = Convert.ToInt32(textBox3.Text);
                             re.Curso = comboBox2.Text;
                             re.FrenteVerso = des;
                             re.Entrega = Convert.ToDateTime(dateTimePicker1.Text);
                             re.Caminho = arquivo.FileName;
                             re.Arquivo = System.IO.Path.GetFileName(arquivo.FileName);
                             re.Ocultar = "Não";
                             re.Autoriza = "Não";
                             re.Destino = "Aguardando autorização do coordenador";
                             co.cadastroArquivo(re);

                             ac.Requisição = Convert.ToInt32(co.BuscaUltCod());
                             ac.Nome = AuxClas.coordenador;
                             ac.Data = Convert.ToDateTime(co.BuscaDataServidor());
                             ac.Hora = co.BuscaHoraServidor();
                             ac.Acao = "Cadastrou uma requisição para o(a) Prof(a) "+ comboBox1.Text +" - Nome do arquivo: " + System.IO.Path.GetFileName(arquivo.FileName);
                             ca.cadastro(ac);
                         });
                     }
                 }
                 else
                 {
                     this.Invoke((MethodInvoker)delegate()
                     {
                         string hora = co.BuscaHoraServidor();
                         string data = Convert.ToString(Convert.ToDateTime(co.BuscaHoraServidor()).ToString("yyyy-MM-dd")).ToString();
                         string horain = String.Format("{0:t}", Convert.ToDateTime(hora));
                         re.Nome = textBox1.Text;
                         re.FrenteVerso = des;
                         re.Data = Convert.ToDateTime(data);
                         re.Hora = horain;
                         re.Professor = comboBox1.Text;
                         re.QtCopias = Convert.ToInt32(textBox2.Text);
                         re.Paginas = Convert.ToInt32(textBox3.Text);
                         re.Curso = comboBox2.Text;
                         re.Entrega = Convert.ToDateTime(dateTimePicker1.Text);
                         re.Ocultar = "Não";
                         re.Autoriza = "Não";
                         re.Destino = "Aguardando autorização do coordenador";
                         co.cadastro(re);

                         ac.Requisição = Convert.ToInt32(co.BuscaUltCod());
                         ac.Nome = AuxClas.coordenador;
                         ac.Data = Convert.ToDateTime(co.BuscaDataServidor());
                         ac.Hora = co.BuscaHoraServidor();
                         ac.Acao = "Cadastrou uma requisição para o(a) Prof(a) " + comboBox1.Text + " - Nome do arquivo: " + textBox1.Text;
                         ca.cadastro(ac);
                     });
                 }
                 
                 this.Invoke((MethodInvoker)delegate()
                 {
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
                    }
                    catch (System.Exception erro)
                    {
                        MessageBox.Show("Erro: " + erro);
                    }
                    
                    //f.Close();
                    string msg = "Requisição cadastrada com sucesso";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    comboBox1.SelectedIndex = -1;
                    comboBox2.SelectedIndex = -1;
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;                  
                     
                 });
             }));
            arquivo1.SetApartmentState(System.Threading.ApartmentState.STA);
            arquivo1.IsBackground = false;
            arquivo1.Start();  
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (atualiza == "atualiza")
            {
                //-------controle de ações---------//

                ac.Requisição = AuxClas.codigo;
                ac.Nome = AuxClas.coordenador;
                ac.Data = Convert.ToDateTime(co.BuscaDataServidor());
                ac.Hora = co.BuscaHoraServidor();
                if (textBox1.Text != AuxClas.NmArquivo)
                {
                    ac.Acao = "Alterou o arquivo do(a) Prof(a) " + AuxClas.professor + " " + AuxClas.NmArquivo + " para " + textBox1.Text;
                    ca.cadastro(ac);
                }
                if (Convert.ToDateTime(dateTimePicker1.Text).ToString("dd/MM/yyyy") != Convert.ToDateTime(AuxClas.dtEntrega).ToString("dd/MM/yyyy"))
                {
                    ac.Acao = "Alterou a data de " + Convert.ToDateTime(AuxClas.dtEntrega).ToString("dd/MM/yyyy") + " para " + Convert.ToDateTime(dateTimePicker1.Text).ToString("dd/MM/yyyy");
                    ca.cadastro(ac);
                }
                if (comboBox1.Text != AuxClas.professor)
                {
                    ac.Acao = "Alterou o(a) professor(a) de: " + AuxClas.professor + " para: " + comboBox1.Text;
                    ca.cadastro(ac);
                }
                if (textBox2.Text != AuxClas.quantidade)
                {
                    ac.Acao = "Alterou a quantidade de " + AuxClas.quantidade + " para " + textBox2.Text;
                    ca.cadastro(ac);
                }
                if (textBox3.Text != AuxClas.total)
                {
                    ac.Acao = "Alterou o total de " + AuxClas.total + " para " + textBox3.Text;
                    ca.cadastro(ac);
                }
                if (comboBox2.Text != AuxClas.curso)
                {
                    ac.Acao = "Alterou o curso de " + AuxClas.curso + " para " + comboBox2.Text;
                    ca.cadastro(ac);
                }
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || radioButton1.Checked == false && radioButton2.Checked == false)
                {
                    string msg = "Preencha todos os campos";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    string message = "Deseja alterar o arquivo?";
                    string caption = "Escolha uma opção";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                  
                    result = MessageBox.Show(message, caption, buttons);

                    new System.Threading.Thread(delegate()
                    {
                        Altera();
                    }).Start();
                }
            }
            else
            {
                int cont = co.Geral(re).Rows.Count;
                if (cont == 0)
                {
                    string msg = "Não existe pedido de cópias para atualizar";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    string at = "atualiza";
                    frmAtualiza frm = new frmAtualiza(name, "", at, "");
                    frm.Show();
                    this.Close();
                }
            }
            button2.Enabled = true;
            button4.Enabled = true;
        }

        public void Altera()
        {
            System.Threading.Thread arquivo1 = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                if (radioButton1.Checked == true)
                {
                    des = "Sim";
                }
                else if (radioButton2.Checked == true)
                {
                    des = "Não";
                }

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (arquivo.ShowDialog() == DialogResult.OK)
                    {
                        this.Invoke((MethodInvoker)delegate()
                        {
                            re.Codigo = cod;
                            re.Nome = textBox1.Text;
                            re.Professor = comboBox1.Text;
                            re.QtCopias = Convert.ToInt32(textBox2.Text);
                            re.Paginas = Convert.ToInt32(textBox3.Text);
                            re.Curso = comboBox2.Text;
                            re.FrenteVerso = des;
                            re.Entrega = Convert.ToDateTime(dateTimePicker1.Text);
                            re.Caminho = arquivo.FileName;
                            re.Arquivo = System.IO.Path.GetFileName(arquivo.FileName);
                            re.Ocultar = "Não";
                            re.Autoriza = "Não";
                            re.Destino = "Aguardando autorização do coordenador";
                            co.alteraArquivo(re);
                        });
                    }
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate()
                    {
                        re.Codigo = Convert.ToInt32(cod);
                        re.Nome = textBox1.Text;
                        re.FrenteVerso = des;
                        re.Data = DateTime.Today;
                        re.Professor = comboBox1.Text;
                        re.QtCopias = Convert.ToInt32(textBox2.Text);
                        re.Paginas = Convert.ToInt32(textBox3.Text);
                        re.Curso = comboBox2.Text;
                        re.Entrega = Convert.ToDateTime(dateTimePicker1.Text);
                        co.atualizar(re);
                    });
                }

                this.Invoke((MethodInvoker)delegate()
                {
                    string msg = "Requisição alterada com sucesso";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    button2.Enabled = false;
                    button4.Enabled = false;
                    this.Close();
                });
            }));
            arquivo1.SetApartmentState(System.Threading.ApartmentState.STA);
            arquivo1.IsBackground = false;
            arquivo1.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ConectaReq co = new ConectaReq();
            Requisicao re = new Requisicao();

            if (exclui == "exclui")
            {
                //------Controle de ações-------
                ac.Requisição = AuxClas.codigo;
                ac.Nome = AuxClas.coordenador;
                ac.Data = Convert.ToDateTime(co.BuscaDataServidor());
                ac.Hora = co.BuscaHoraServidor();
                ac.Acao = "Excluiu a requisição do(a) Prof(a) " + AuxClas.professor + ": " + AuxClas.NmArquivo;
                ca.cadastro(ac);
                //----------Termina controle de ações----------

                re.Codigo = AuxClas.codigo;
                co.excluir(re);
                string msg = "Requisição Excluída com sucesso";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                button2.Enabled = true;
                button3.Enabled = true;
                this.Close();
            }
            else
            {
                int cont = co.Geral(re).Rows.Count;
                if (cont == 0)
                {
                    string msg = "Não existe pedido de cópias para excluir";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    string exc = "exclui";
                    frmAtualiza frm = new frmAtualiza(name, "", "", exc);
                    frm.Show();
                    this.Close();
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
            timer1.Stop();
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            re.LibData = "Sim";
            co.libData(re);
            string msg = "Data de pedido de Requisição liberada";
            frmMensagem mg = new frmMensagem(msg);
            mg.ShowDialog();
            BloqueiaData();

            //ac.Requisição = Convert.ToInt32(null);
            ac.Nome = coord;
            ac.Data = Convert.ToDateTime(co.BuscaDataServidor());
            ac.Hora = co.BuscaHoraServidor();
            ac.Acao = "Liberou a data de pedido de cópias";
            ca.cadastro(ac);
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            re.LibData = "Não";
            co.libData(re);
            string msg = "Data de pedido de Requisição bloqueada";
            frmMensagem mg = new frmMensagem(msg);
            mg.ShowDialog();
            BloqueiaData();

            //ac.Requisição = Convert.ToInt32(null);
            ac.Nome = coord;
            ac.Data = Convert.ToDateTime(co.BuscaDataServidor());
            ac.Hora = co.BuscaHoraServidor();
            ac.Acao = "Bloqueou a data de pedido de cópias";
            ca.cadastro(ac);
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                if (Convert.ToInt32(textBox3.Text) < Convert.ToInt32(textBox2.Text))
                {
                    string msg = "INFORMAR A QUANT. TOTAL DE IMPRESSÕES (QUANT. DE CÓPIAS X QUANT. DE PÁGINAS)";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                    textBox3.Clear();
                    textBox3.Focus();
                }
            }
            else
            {
                string msg = "INFORMAR A QUANT. TOTAL DE IMPRESSÕES (QUANT. DE CÓPIAS X QUANT. DE PÁGINAS)";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox3.Focus();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                if (textBox3.Text != "")
                {
                    if (Convert.ToInt32(textBox3.Text) < Convert.ToInt32(textBox2.Text))
                    {
                        string msg = "INFORMAR A QUANT. TOTAL DE IMPRESSÕES (QUANT. DE CÓPIAS X QUANT. DE PÁGINAS)";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                        textBox3.Clear();
                        textBox3.Focus();
                    }
                }
            }
            else
            {
                string msg = "INFORMAR A QUANTIDADE DE CÓPIAS";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox2.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
   }
}

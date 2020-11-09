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
using System.IO;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Globalization;

namespace Copias
{
    public partial class frmCopiaProf : Form
    {
        string des, name, frente, prof, atualiza, exclui, cd, cd2, curso;
        int cod;
        Acoes ac = new Acoes();
        ConectaAc ca = new ConectaAc();
        Requisicao re = new Requisicao();
        ConectaReq co = new ConectaReq();
        ConectaCurso cs = new ConectaCurso();
        Curso cc = new Curso();
        Usuario us = new Usuario();
        DialogResult result;
        OpenFileDialog arquivo = new OpenFileDialog();
        frmProcesso f = new frmProcesso();
        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        MailMessage mail = new MailMessage();
        public frmCopiaProf(string nm)
        {
            InitializeComponent();
            name = nm;
        }

        private void Data()
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
            prof = AuxClas.professor;
            textBox2.Text = AuxClas.quantidade;
            textBox3.Text = AuxClas.total;
            comboBox2.SelectedItem = AuxClas.curso;
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox2.Text == "" || radioButton1.Checked == false && radioButton2.Checked == false)
            {
                string msg = "Preencha todos os campos";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                string message = "Deseja anexar um arquivo?";
                string caption = "Escolha uma opção";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                
                result = MessageBox.Show(message, caption, buttons);

                cc.Nome = (comboBox2.Text).Substring(3);
 
                foreach (DataRow item in cs.CoordEmail(cc).Rows)
                {
                    cd = item["coord"].ToString();
                    cd2 = item["coord2"].ToString();
                }

                us.Nome = cd;
                us.Profissao2 = cd2;
                
                new System.Threading.Thread(delegate()
                {
                    Carrega();
                }
                ).Start();

                curso = comboBox2.Text;           
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
                mail.Body = "Caro(a) Coordenador(a), você tem uma requisição de cópias para liberar no sistema de Requisição, do(a) Prof.(a) " + name + " que será usado no " + curso + " no dia " + Convert.ToDateTime(dateTimePicker1.Text).ToString("dd/MM/yyyy") + ". Por favor, verifique assim que possível, caso passe da data solicitada pelo professor, o sistema irá deletar o pedido automaticamente.<br>" + 
                    "<br>" + "<br>" + "<i><b><font size=2><font color=red>Este e-mail foi encaminhado apenas como aviso, por favor não responder.</font></i></b><br>" +
                    "<br>" + "<br>" + "<br>" + "Att.<br>" + "Escola Técnica Estadual Amim Jundi<br>" + "Rua Japão, 724 - Centro<br>" + "Telefones: (18) 3528-3982  -  (18) 3528-4760<br>";
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
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
                             cc.Nome = comboBox2.Text;
                             re.Nome = textBox1.Text;
                             re.Professor = name;
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

                             //----------controle de ações------------
                             ac.Requisição = Convert.ToInt32(co.BuscaUltCod());
                             ac.Nome = name;
                             ac.Data = Convert.ToDateTime(co.BuscaDataServidor());
                             ac.Hora = co.BuscaHoraServidor();
                             ac.Acao = "Cadastrou uma requisição: " + System.IO.Path.GetFileName(arquivo.FileName);
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
                         cc.Nome = comboBox2.Text;
                         re.Nome = textBox1.Text;
                         re.Data = Convert.ToDateTime(data);
                         re.Hora = horain;
                         re.Professor = name;
                         re.FrenteVerso = des;
                         re.QtCopias = Convert.ToInt32(textBox2.Text);
                         re.Paginas = Convert.ToInt32(textBox3.Text);
                         re.Curso = comboBox2.Text;
                         re.Entrega = Convert.ToDateTime(dateTimePicker1.Text);
                         re.Ocultar = "Não";
                         re.Autoriza = "Não";
                         re.Destino = "Aguardando autorização do coordenador";
                         co.cadastro(re);

                         //----------controle de ações------------
                         ac.Requisição = Convert.ToInt32(co.BuscaUltCod());
                         ac.Nome = name;
                         ac.Data = Convert.ToDateTime(co.BuscaDataServidor());
                         ac.Hora = co.BuscaHoraServidor();
                         ac.Acao = "Cadastrou uma requisição: " + textBox1.Text;
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
                     comboBox2.SelectedIndex = -1;
                     radioButton1.Checked = false;
                     radioButton2.Checked = false;
                 });
             }));
            arquivo1.SetApartmentState(System.Threading.ApartmentState.STA);
            arquivo1.IsBackground = false;
            arquivo1.Start();           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (atualiza == "atualiza")
            {
                //-------controle de ações---------//

                ac.Requisição = AuxClas.codigo;
                ac.Nome = name;
                ac.Data = Convert.ToDateTime(co.BuscaDataServidor());
                ac.Hora = co.BuscaHoraServidor();

                if (textBox1.Text != AuxClas.NmArquivo)
                {
                    ac.Acao = "Alterou o arquivo do(a) Prof(a) "+AuxClas.professor+" "+ AuxClas.NmArquivo+" para "+textBox1.Text;
                    ca.cadastro(ac);
                }
                if (Convert.ToDateTime(dateTimePicker1.Text).ToString("dd/MM/yyyy") != Convert.ToDateTime(AuxClas.dtEntrega).ToString("dd/MM/yyyy"))
                {
                    ac.Acao = "Alterou a data de " + Convert.ToDateTime(AuxClas.dtEntrega).ToString("dd/MM/yyyy") + " para " + Convert.ToDateTime(dateTimePicker1.Text).ToString("dd/MM/yyyy");
                    ca.cadastro(ac);
                }
                if (textBox2.Text != AuxClas.quantidade)
                {
                    ac.Acao = "Alterou a quantidade de "+AuxClas.quantidade+" para "+textBox2.Text;
                    ca.cadastro(ac);
                }
                if (textBox3.Text != AuxClas.total)
                {
                    ac.Acao = "Alterou o total de " + AuxClas.total + " para " + textBox3.Text;
                    ca.cadastro(ac);
                }
                if (comboBox2.Text != AuxClas.curso)
                {
                    ac.Acao = "Alterou o curso de "+AuxClas.curso+" para "+comboBox2.Text;
                    ca.cadastro(ac);
                }
                //----------------termina controle de ações-----------------

                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox2.Text == "" || radioButton1.Checked == false && radioButton2.Checked == false)
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
                re.Professor = name;
                int cont = co.porProfessor(re).Rows.Count;
                if (cont == 0)
                {
                    string msg = "Não existe pedido de cópias para atualizar";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    string pg = "abre";
                    string at = "atualiza";
                    frmAtualiza frm = new frmAtualiza(name, pg, at, "");
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
                            re.Professor = name;
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
                        re.Codigo = cod;
                        re.Nome = textBox1.Text;
                        re.FrenteVerso = des;
                        re.Data = Convert.ToDateTime(DateTime.Today);
                        re.Professor = prof;
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
                    this.Close();
                });
            }));
            arquivo1.SetApartmentState(System.Threading.ApartmentState.STA);
            arquivo1.IsBackground = false;
            arquivo1.Start();
        }

        private void frmCopiaProf_Load(object sender, EventArgs e)
        {


            if (atualiza == "atualiza")
            {
                button2.Enabled = false;
                button4.Enabled = false;
                Data();
            }
            else
            {
                Data();
            }

            if (exclui == "exclui")
            {
                button2.Enabled = false;
                button3.Enabled = false;
            }

            int cont = cs.Curso().Rows.Count;
            for (int i = 0; i < cont; i++)
            {
                comboBox2.Items.Add(cs.Curso().Rows[i]["modulo"].ToString() + " " + cs.Curso().Rows[i]["nome"].ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ConectaReq co = new ConectaReq();
            Requisicao re = new Requisicao();

            if (exclui == "exclui")
            {
                //------Controle de ações-------
                ac.Requisição = AuxClas.codigo;
                ac.Nome = name;
                ac.Data = Convert.ToDateTime(co.BuscaDataServidor());
                ac.Hora = co.BuscaHoraServidor();
                ac.Acao = "Excluiu a requisição do(a) Prof(a) " + AuxClas.professor + ": " + AuxClas.NmArquivo;
                ca.cadastro(ac);
                //----------Termina controle de ações----------
                re.Codigo = AuxClas.codigo;
                co.excluir(re);
                string msg = "Requisição Excluída";
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
                re.Professor = name;
                int cont = co.porProfessor(re).Rows.Count;
                if (cont == 0)
                {
                    string msg = "Não existe pedido de cópias para excluir";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    string pg = "abre";
                    string exc = "exclui";
                    frmAtualiza frm = new frmAtualiza(name, pg, "", exc);
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
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

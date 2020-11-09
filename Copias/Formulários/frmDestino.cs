using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using MySql.Data.MySqlClient;

namespace Copias
{
    public partial class frmDestino : Form
    {
        int cod;
        string resp, prof, oc, email, arquivo;
        frmProcesso f = new frmProcesso();
        frmImp fi = new frmImp("");
        Acoes ac = new Acoes();
        ConectaAc ca = new ConectaAc();

        public frmDestino(int req, string nome, string oculta, string prf, string arq)
        {
            InitializeComponent();
            cod = req;
            resp = nome;
            oc = oculta;
            prof = prf;
            arquivo = arq;
        }

        private void CarregaFormAguarde()
        {
            AuxClas.processo = "Enviando e-mail ao Professor...";
            f.Atualizar();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConectaUs cs = new ConectaUs();
            Usuario us = new Usuario();

            us.Nome = prof;
            
            foreach (DataRow item in cs.Email(us).Rows)
            {
                email = item["email"].ToString();
            }

            Requisicao re = new Requisicao();
            ConectaReq co = new ConectaReq();

            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                string msg = "Escolher uma opção de destino";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                re.Codigo = cod;
                re.RespImpressao = resp;
                re.Ocultar = "Sim";
                re.DataImpressao = Convert.ToDateTime(dateTimePicker1.Text);
                if (radioButton1.Checked == true)
                {
                    re.Destino = "Armário";

                    ac.Requisição = cod;
                    ac.Nome = resp;
                    ac.Data = Convert.ToDateTime(co.BuscaDataServidor());
                    ac.Hora = co.BuscaHoraServidor();
                    ac.Acao = "Imprimiu as cópias de: " + prof + " - Destino: Armário na sala dos Professores";
                    ca.cadastro(ac);

                    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;
                    client.Credentials = new System.Net.NetworkCredential("copias027@gmail.com", "ete027oc");
                    MailMessage mail = new MailMessage();
                    mail.Sender = new System.Net.Mail.MailAddress("copias027@gmail.com", "Etec Amim Jundi");
                    mail.From = new MailAddress("copias027@gmail.com", "Etec Amim Jundi");
                    mail.To.Add(new MailAddress(email, prof));
                    mail.Subject = "Aviso";
                    if (radioButton1.Checked == true)
                    {
                        mail.Body = "Caro(a) Professor(a), a impressão solicitada: " + arquivo + ", foi realizada e se encontra em seu armário na sala dos professores.<br>" + 
                            "<br>" + "<br>" + "<i><b><font size=2><font color=red>Este e-mail foi encaminhado apenas como aviso, por favor não responder.</font></i></b><br>" +
                            "<br>" + "<br>" + "<br>" + "Att.<br>" + "Escola Técnica Estadual Amim Jundi<br>" + "Rua Japão, 724 - Centro<br>" + "Telefones: (18) 3528-3982  -  (18) 3528-4760<br>";
                    }
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
                    }
                    catch (System.Exception erro)
                    {
                        MessageBox.Show("Erro: " + erro);
                    }
                    finally
                    {
                        mail = null;
                    }
                }
                else if (radioButton2.Checked == true)
                {
                    re.Destino = "em mãos";

                    ac.Requisição = cod;
                    ac.Nome = resp;
                    ac.Data = Convert.ToDateTime(co.BuscaDataServidor());
                    ac.Hora = co.BuscaHoraServidor();
                    ac.Acao = "Imprimiu as cópias de: " + prof + " - Destino: Entregue em mãos";
                    ca.cadastro(ac);
                }
                co.FinalizaReq(re);
                fi.Refresh();
                //f.Close();
                string msg = "Requisição finalizada";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                this.Close();
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

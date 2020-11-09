using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;

namespace Copias
{
    public partial class frmMotivo : Form
    {
        frmProcesso f = new frmProcesso();
        string prof, email, coord, hora;
        int cod;
        DateTime data;
        Acoes ac = new Acoes();
        ConectaAc ca = new ConectaAc();
        public frmMotivo(string pf, string em, string cd, int cg, DateTime dt, string hr)
        {
            InitializeComponent();
            prof = pf;
            email = em;
            coord = cd;
            cod = cg;
            data = dt;
            hora = hr;
        }

        private void CarregaFormAguarde()
        {
            AuxClas.processo = "Enviando e-mail ao Professor...";
            f.Atualizar();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                string msg = "Por favor, informar o motivo";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                ac.Requisição = cod;
                ac.Nome = coord;
                ac.Data = data;
                ac.Hora = hora;
                ac.Acao = "Recusou a requisição do(a) Prof(a) "+prof+". Motivo: "+textBox1.Text;
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
                mail.Body = "Caro(a) Professor(a), sua requisição não foi aprovada. MOTIVO: " + textBox1.Text + ". Você pode atualizar ou procurar o(a) Coord. " + coord + " para maiores esclarecimentos.<br>" + 
                    "<br>" + "<br>" + "<i><b><font size=2><font color=red>Este e-mail foi encaminhado apenas como aviso, por favor não responder.</font></i></b><br>" +
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
                }
                catch (System.Exception erro)
                {
                    MessageBox.Show("Erro: " + erro);
                }
                //f.Close();
                string msg = "Requisição não autorizada";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                this.Close();
            }
        }
    }
}

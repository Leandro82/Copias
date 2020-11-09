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
    public partial class frmEsqSenha : Form
    {
        string email, cd, senha;
        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        MailMessage mail = new MailMessage();
        Usuario us = new Usuario();
        ConectaUs cs = new ConectaUs();
        frmProcesso cr = new frmProcesso();
        public frmEsqSenha()
        {
            InitializeComponent();
        }

        private void CarregaFormAguarde()
        {
            AuxClas.processo = "Enviando a senha ao seu E-MAIL...";
            cr.Atualizar();
            cr.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread tFormAguarde = new System.Threading.Thread(new System.Threading.ThreadStart(CarregaFormAguarde));
            tFormAguarde.Start();
            if (textBox1.Text == "")
            {
                string msg = "INFORMAR SEU E-MAIL!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox1.Focus();
            }
            else
            {
                us.Email = textBox1.Text;

                if (cs.selecionaSenha(us).Rows.Count == 0)
                {
                    string msg = "E-MAIL OU USUÁRIO NÃO CADASTRADO!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                    textBox1.Clear();
                    textBox1.Focus();
                }
                else
                {
                    email = textBox1.Text;
                    cd = cs.selecionaSenha(us).Rows[0][0].ToString();
                    senha = cs.selecionaSenha(us).Rows[0][2].ToString();
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;
                    client.Credentials = new System.Net.NetworkCredential("copias027@gmail.com", "ete027oc");
                    mail.Sender = new System.Net.Mail.MailAddress("copias027@gmail.com", "Etec Amim Jundi");
                    mail.From = new MailAddress("copias027@gmail.com", "Etec Amim Jundi");
                    mail.To.Add(new MailAddress(email, cd));
                    mail.Subject = "Requisição - Reenvio de Senha ";
                    mail.Body = "Caro(a) usuário, sua senha de acesso ao sistema é:<br>" +
                        "<br>" + "<i><b><font size=2><font color=red>"+senha+"</font></i></b><br>" +
                        "<br>" + "Caso queira alterá-la, procure a Secretaria Acadêmica."+"<br>" + "<br>" + "<br>" + "Att.<br>" + "Escola Técnica Estadual Amim Jundi<br>" + "Rua Japão, 724 - Centro<br>" + "Telefones: (18) 3528-3982  -  (18) 3528-4760<br>";
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    client.Send(mail);
                    tFormAguarde.Abort();
                    string msg = "A SENHA FOI ENVIADA AO SEU E-MAIL!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                    this.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEsqSenha_Load(object sender, EventArgs e)
        {

        }
    }
}

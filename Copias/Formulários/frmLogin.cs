using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Deployment.Application;

namespace Copias
{
    public partial class frmLogin : Form
    {
        private bool Logado = false;
        private bool Acesso = false;
        int ano; 
        string hoje;
        IniciaAno ia = new IniciaAno();
        ConectaUs co = new ConectaUs();
        Usuario us = new Usuario();
        ConectaReq cq = new ConectaReq();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void InstallUpdateSyncWithInfo()
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show("A nova versão do aplicativo não pode ser baixada no momento. \n\nVerifique sua conexão de rede ou tente novamente mais tarde. Error: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("Não é possível procurar por uma nova versão do aplicativo. A implantação do ClickOnce está corrompida. Por favor, reimplemente o aplicativo e tente novamente. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show("Este aplicativo não pode ser atualizado. Provavelmente não é um aplicativo ClickOnce. Error: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    Boolean doUpdate = true;

                    if (!info.IsUpdateRequired)
                    {
                        /*DialogResult dr = MessageBox.Show("Uma atualização está disponível. Deseja atualizar o aplicativo agora? ", " Atualização Disponível", MessageBoxButtons.OKCancel);
                        if (!(DialogResult.OK == dr))
                        {
                            doUpdate = false;
                        }*/
                        
                        var peq = new frmVersao(doUpdate);
                        if (Application.OpenForms.OfType<frmVersao>().Count() > 0)
                        {
                            Application.OpenForms[peq.Name].Focus();
                        }
                        else
                        {
                            peq.Show();
                        }

                        frmLogin frm = new frmLogin();
                        frm.Visible = false;
                    }
                    else
                    {
                        //  Exibe uma mensagem que o aplicativo DEVE ser reinicializado. Exibe a versão mínima exigida.
                        MessageBox.Show("Este aplicativo detectou uma atualização obrigatória do seu atual " +
                            "versão para versão " + info.MinimumRequiredVersion.ToString() +
                            ". O aplicativo agora instalará a atualização e reiniciará.",
                            "Atualização disponível", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    if (doUpdate)
                    {
                        try
                        {
                            ad.Update();
                            MessageBox.Show("O aplicativo foi atualizado e agora será reiniciado.");
                            Application.Restart();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            MessageBox.Show("Não é possível instalar a versão mais recente do aplicativo. \n\nPor favor, verifique sua conexão de rede ou tente novamente mais tarde. Error: " + dde);
                            return;
                        }
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AuxClas.login = textBox1.Text;
            AuxClas.senha = textBox2.Text;
            us.Login = textBox1.Text;
            us.Senha = textBox2.Text;
            bool result = co.VerificaLogin(us);
            bool acesso = co.VerificaAcesso(us);
            Logado = result;
            Acesso = acesso;

            if (result)
            {
                    this.Close();
            }
            else
            {
                label3.Visible = true;
                label3.Text = "Usuário ou senha incorreto!!";
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Logado)
            {
                this.Close();
            }
            else
            {
                Application.Exit();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            label3.Visible = false;
            
            int con = ia.Ano().Rows.Count;

            timer1.Interval = 850;
            timer1.Start();
            
            hoje = Convert.ToString(DateTime.Today.ToString("yyyy"));
            for (int j = 0; j < con; j++)
            {
                ano = Convert.ToInt32(ia.Ano().Rows[j]["ano"].ToString());
            }

            if (ano < Convert.ToUInt32(hoje))
            {
                Ano a = new Ano();
                IniciaAno ina = new IniciaAno();

                a.An = Convert.ToString(DateTime.Today.ToString("yyyy"));
                ina.InicioAno(a);
                ina.LimpaOcorrencias();
                ina.LimpaAlunos();
                ina.LimpaAcoes();
                ina.LimpaProgressoes();
                ina.LimpaAgenda();
                string msg = "Ano Iniciado";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                ina.AlteraAno(a);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                AuxClas.login = textBox1.Text;
                AuxClas.senha = textBox2.Text;
                us.Login = textBox1.Text;
                us.Senha = textBox2.Text;
                bool result = co.VerificaLogin(us);
                bool acesso = co.VerificaAcesso(us);
                Logado = result;
                Acesso = acesso;

                if (result)
                {
                    this.Close();
                }
                else
                {
                    label3.Visible = true;
                    label3.Text = "Usuário ou senha incorreto!!";
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmEsqSenha form = new frmEsqSenha();
            form.ShowDialog();
        }
    }
}

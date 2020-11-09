using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Copias
{
    public partial class frmCadLogin : Form
    {
        string cod;
        public frmCadLogin(string nome, string cd)
        {
            InitializeComponent();
            textBox1.Text = nome;
            cod = cd;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label5.Text == "Senhas não batem")
            {
                string msg = "Senhas não conferem, favor verificar";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                label5.Text = "";
            }
            else
            {
                if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    string msg = "Preencher todos os campos";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    label5.Text = "";
                }
                else
                {
                    ConectaUs co = new ConectaUs();
                    Usuario us = new Usuario();

                    us.Nome = textBox1.Text;
                    us.Login = textBox2.Text;
                    us.Senha = textBox3.Text;
                    us.Acesso = "Sim";
                    co.alteraCad(us);
                    string msg = "Login e Senha registrados";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                    string nome = textBox1.Text;
                    frmPrincipal pr = new frmPrincipal(nome,cod);
                    pr.Show();
                    this.Hide();
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != textBox4.Text)
            {
                label5.Text = "Senhas não batem";
                label5.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                label5.Text = "OK";
                label5.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void frmCadLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (label5.Text == "Senhas não batem")
                {
                    string msg = "Senhas não conferem, favor verificar";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    label5.Text = "";
                }
                else
                {
                    if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                    {
                        string msg = "Preencher todos os campos";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        label5.Text = "";
                    }
                    else
                    {
                        ConectaUs co = new ConectaUs();
                        Usuario us = new Usuario();

                        us.Nome = textBox1.Text;
                        us.Login = textBox2.Text;
                        us.Senha = textBox3.Text;
                        us.Acesso = "Sim";
                        co.alteraCad(us);
                        string msg = "Login e Senha registrados";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                        string nome = textBox1.Text;
                        frmPrincipal pr = new frmPrincipal(nome, cod);
                        pr.Show();
                        this.Hide();
                    }
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
            SendKeys.Send("{ESC}");
            timer1.Stop();
        }
    }
}

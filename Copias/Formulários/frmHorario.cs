using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Copias
{
    public partial class frmHorario : Form
    {
        Horario hr = new Horario();
        ConectaHorario ch = new ConectaHorario();
        string resp;
        public frmHorario()
        {
            InitializeComponent();
        }

        private void LimparMasKedBox()
        {
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
            maskedTextBox3.Clear();
            maskedTextBox4.Clear();
            maskedTextBox5.Clear();
            maskedTextBox6.Clear();
            maskedTextBox7.Clear();
            maskedTextBox8.Clear();
            maskedTextBox9.Clear();
            maskedTextBox10.Clear();
            maskedTextBox11.Clear();
            maskedTextBox12.Clear();
            maskedTextBox13.Clear();
            maskedTextBox14.Clear();
            maskedTextBox15.Clear();
            maskedTextBox16.Clear();
            comboBox1.Text = "";
        }

        public void horarios()
        {
            hr.Periodo = comboBox1.Text;
            hr.PrimeiraAulaInicio = maskedTextBox1.Text;
            hr.PrimeiraAulaFim = maskedTextBox2.Text;
            hr.SegundaAulaInicio = maskedTextBox3.Text;
            hr.SegundaAulaFim = maskedTextBox4.Text;
            hr.TerceiraAulaInicio = maskedTextBox5.Text;
            hr.TerceiraAulaFim = maskedTextBox6.Text;
            hr.QuartaAulaInicio = maskedTextBox7.Text;
            hr.QuartaAulaFim = maskedTextBox8.Text;
            if (comboBox1.Text == "Manhã" || comboBox1.Text == "Integral")
            {
                hr.QuintaAulaInicio = maskedTextBox9.Text;
                hr.QuintaAulaFim = maskedTextBox10.Text;
                hr.SextaAulaInicio = maskedTextBox11.Text;
                hr.SextaAulaFim = maskedTextBox12.Text;
                if (comboBox1.Text == "Integral")
                {
                    hr.SetimaAulaInicio = maskedTextBox13.Text;
                    hr.SetimaAulaFim = maskedTextBox14.Text;
                    hr.OitavaAulaInicio = maskedTextBox15.Text;
                    hr.OitavaAulaFim = maskedTextBox16.Text;
                }
            }
        }

        public string VerificarVazio()
        {
            if (comboBox1.Text == "Integral")
            {
                if (maskedTextBox1.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox2.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox3.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox4.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox5.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox6.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox7.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox8.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox9.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox10.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox11.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox12.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox13.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox14.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox15.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox16.Text.Replace(":", "").Trim() == "")
                {
                    resp = "Ok";
                }
            }
            else if (comboBox1.Text == "Manhã")
            {
                if (maskedTextBox1.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox2.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox3.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox4.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox5.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox6.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox7.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox8.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox9.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox10.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox11.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox12.Text.Replace(":", "").Trim() == "")
                {
                    resp = "Ok";
                }
            }
            else if (comboBox1.Text == "Tarde" || comboBox1.Text == "Noite")
            {
                if (maskedTextBox1.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox2.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox3.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox4.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox5.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox6.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox7.Text.Replace(":", "").Trim() == "" ||
                maskedTextBox8.Text.Replace(":", "").Trim() == "")
                {
                    resp = "Ok";
                }
            }
            return resp;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Manhã")
            {
                panel21.Visible = true;
                panel22.Visible = true;
                panel23.Visible = false;
                panel24.Visible = false;
            }
            else if (comboBox1.Text == "Integral")
            {
                panel21.Visible = true;
                panel22.Visible = true;
                panel23.Visible = true;
                panel24.Visible = true;
            }
            else if (comboBox1.Text == "Tarde" || comboBox1.Text == "Noite")
            {
                panel21.Visible = false;
                panel22.Visible = false;
                panel23.Visible = false;
                panel24.Visible = false;
            }

            if (comboBox1.BackColor == Color.Yellow)
            {
                hr.Periodo = comboBox1.Text;

                if (ch.SelecionaHorarios(hr).Rows.Count == 0)
                {
                    string msg = "Não existe horários cadastrados para esse período";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                    LimparMasKedBox();
                }
                else
                {
                    maskedTextBox1.Text = ch.SelecionaHorarios(hr).Rows[0]["pri_inicio"].ToString();
                    maskedTextBox2.Text = ch.SelecionaHorarios(hr).Rows[0]["pri_final"].ToString();
                    maskedTextBox3.Text = ch.SelecionaHorarios(hr).Rows[0]["seg_inicio"].ToString();
                    maskedTextBox4.Text = ch.SelecionaHorarios(hr).Rows[0]["seg_final"].ToString();
                    maskedTextBox5.Text = ch.SelecionaHorarios(hr).Rows[0]["ter_inicio"].ToString();
                    maskedTextBox6.Text = ch.SelecionaHorarios(hr).Rows[0]["ter_final"].ToString();
                    maskedTextBox7.Text = ch.SelecionaHorarios(hr).Rows[0]["qua_inicio"].ToString();
                    maskedTextBox8.Text = ch.SelecionaHorarios(hr).Rows[0]["qua_final"].ToString();
                    if (comboBox1.Text == "Integral")
                    {
                        maskedTextBox9.Text = ch.SelecionaHorarios(hr).Rows[0]["qui_inicio"].ToString();
                        maskedTextBox10.Text = ch.SelecionaHorarios(hr).Rows[0]["qui_final"].ToString();
                        maskedTextBox11.Text = ch.SelecionaHorarios(hr).Rows[0]["sex_inicio"].ToString();
                        maskedTextBox12.Text = ch.SelecionaHorarios(hr).Rows[0]["sex_final"].ToString();
                        maskedTextBox13.Text = ch.SelecionaHorarios(hr).Rows[0]["set_inicio"].ToString();
                        maskedTextBox14.Text = ch.SelecionaHorarios(hr).Rows[0]["set_final"].ToString();
                        maskedTextBox15.Text = ch.SelecionaHorarios(hr).Rows[0]["oit_inicio"].ToString();
                        maskedTextBox16.Text = ch.SelecionaHorarios(hr).Rows[0]["oit_final"].ToString();
                    }
                    else if (comboBox1.Text == "Manhã")
                    {
                        maskedTextBox9.Text = ch.SelecionaHorarios(hr).Rows[0]["qui_inicio"].ToString();
                        maskedTextBox10.Text = ch.SelecionaHorarios(hr).Rows[0]["qui_final"].ToString();
                        maskedTextBox11.Text = ch.SelecionaHorarios(hr).Rows[0]["sex_inicio"].ToString();
                        maskedTextBox12.Text = ch.SelecionaHorarios(hr).Rows[0]["sex_final"].ToString();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                string msg = "Selecione um período!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                hr.Periodo = comboBox1.Text;

                if (ch.SelecionaHorarios(hr).Rows.Count > 0 && ch.SelecionaHorarios(hr).Rows[0]["periodo"].ToString() == comboBox1.Text)
                {
                    string msg = "Já existem horários cadastrados para este período, você pode alterá-lo caso precise";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    if (VerificarVazio() == "Ok")
                    {
                        string msg = "Preencher todos os horários";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                    }
                    else
                    {
                        horarios();
                        ch.cadastroHorarios(hr);
                        string msg = "Horário cadastrado!!";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                        LimparMasKedBox();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "ATUALIZAR")
            {
                comboBox1.Text = "";
                comboBox1.BackColor = Color.Yellow;
                button1.Enabled = false;
                button2.Text = "SALVAR";
            }
            else if (button2.Text == "SALVAR")
            {
                if (comboBox1.Text == "")
                {
                    string msg = "Selecione um período!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    horarios();
                    ch.AtualizaHorarios(hr);
                    string msg = "Horário Atualizado!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                    LimparMasKedBox();
                    button1.Enabled = true;
                    comboBox1.BackColor = Color.White;
                    button2.Text = "ATUALIZAR";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LimparMasKedBox();
            button1.Enabled = true;
            comboBox1.BackColor = Color.White;
            button2.Text = "ATUALIZAR";
            panel21.Visible = true;
            panel22.Visible = true;
            panel23.Visible = true;
            panel24.Visible = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Copias
{
    public partial class frmAgenda : Form
    {
        ConectaHorario ch = new ConectaHorario();
        ConectaAgenda ca = new ConectaAgenda();
        ConectaItens ci = new ConectaItens();
        Agenda ag = new Agenda();
        Horario hr = new Horario();
        Itens it = new Itens();
        Usuario us = new Usuario();
        int codAula1, codAula2, codAula3, codAula4, codAula5, codAula6, codAula7, codAula8, botao, codCat;
        string resp, mensagem;
        public frmAgenda(string nm)
        {
            InitializeComponent();
            resp = nm;
        }

        private void frmAgenda_Load(object sender, EventArgs e)
        {
            label19.Visible = false;
            tableLayoutPanel1.Visible = false;
            label17.Text = monthCalendar1.SelectionStart.ToString("dd/MM/yyyy");
            comboBox4.Enabled = false;
            Application.VisualStyleState = VisualStyleState.NonClientAreaEnabled;
            Size s = monthCalendar1.SingleMonthSize;
            Size d = new Size(500, 300);
            s = d;
            monthCalendar1.Size = s;

            LiberaAgenda();
            
            int cont = ch.Curso().Rows.Count;
            for (int i = 0; i < cont; i++)
            {
                comboBox2.Items.Add(ch.Curso().Rows[i]["modulo"].ToString() + " " + ch.Curso().Rows[i]["nome"].ToString());
            }

            int prf = ch.Professor().Rows.Count;
            for (int j = 0; j < prf; j++)
            {
                comboBox3.Items.Add(ch.Professor().Rows[j]["nome"].ToString());
            }

            int aux = ci.SelecionaCategoria().Rows.Count;
            for (int j = 0; j < aux; j++)
            {
                comboBox6.Items.Add(ci.SelecionaCategoria().Rows[j]["categoria"].ToString());
            }
        }

        public void SelecionaCurso(int nm)
        {
            int quant = nm;
            if (quant == 4)
            {
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = false;
                button8.Visible = false;
                button9.Visible = true;
                button10.Visible = false;
                button11.Visible = false;
                button12.Visible = true;
                button13.Visible = false;
                button14.Visible = false;
                button15.Visible = false;
                button16.Visible = false;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
            }
            else if (quant == 6)
            {
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button8.Visible = true;
                button9.Visible = true;
                button10.Visible = false;
                button11.Visible = false;
                button12.Visible = true;
                button13.Visible = true;
                button14.Visible = true;
                button15.Visible = false;
                button16.Visible = false;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = false;
                label11.Visible = false;
            }
            else if (quant == 5)
            {
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button8.Visible = false;
                button9.Visible = true;
                button10.Visible = false;
                button11.Visible = false;
                button12.Visible = true;
                button13.Visible = true;
                button14.Visible = false;
                button15.Visible = false;
                button16.Visible = false;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
            }
            else if (quant == 8)
            {
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button8.Visible = true;
                button9.Visible = true;
                button10.Visible = true;
                button11.Visible = true;
                button12.Visible = true;
                button13.Visible = true;
                button14.Visible = true;
                button15.Visible = true;
                button16.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
            }
        }

        public void LiberaAgenda()
        {
            button1.BackColor = Color.Green;
            button2.BackColor = Color.Green;
            button4.BackColor = Color.Green;
            button6.BackColor = Color.Green;
            button7.BackColor = Color.Green;
            button8.BackColor = Color.Green;
            button10.BackColor = Color.Green;
            button11.BackColor = Color.Green;
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";
        }

        public void Agendar(int aula, string agendado, int cod)
        {
            ag.Data = monthCalendar1.SelectionStart.ToString();
            ag.Local = comboBox1.Text;
            ag.Aula = aula;
            ag.Codigo = cod;
            if (tabPage1.Focus())
            {
                ag.MesmoPeriodo = "Ok";
                ag.Curso = comboBox2.Text;
                int num = comboBox2.Text.Length;
                hr.Curso = comboBox2.Text.Remove(0, 3);
                if (checkBox1.Checked != true)
                    ag.Periodo = ch.SelecionaHorarios(hr).Rows[0]["Periodo"].ToString();
                else
                    ag.Periodo = comboBox4.Text;
                if (ag.Periodo == "Manhã" && (aula == 1 || aula == 2 || aula == 3 || aula == 4 || aula == 5))
                {
                    ag.Coincide = "Integral";
                }
                else if (ag.Periodo == "Integral" && (aula == 1 || aula == 2 || aula == 3 || aula == 4 || aula == 5))
                {
                    ag.Coincide = "Manhã";
                }
                else if(ag.Periodo == "Integral" && (aula == 6 || aula == 7))
                {
                    ag.Coincide = "Tarde";
                }
                    else if(ag.Periodo == "Tarde" && (aula == 1 || aula == 2))
                {
                    ag.Coincide = "Integral";
                }
                else
                {
                    ag.Coincide = "";
                }
                ag.Responsavel = comboBox3.Text;                
                
                if (agendado == "ok")
                {
                    if (ca.QuemAgendou(ag).Rows[0]["curso"].ToString() != "")
                    {
                        string msg = "Agendado por " + ca.QuemAgendou(ag).Rows[0]["responsavel"].ToString() + " - " + ca.QuemAgendou(ag).Rows[0]["curso"].ToString();
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                    }
                    else
                    {
                        string msg = "Agendado por " + ca.QuemAgendou(ag).Rows[0]["responsavel"].ToString() + " - " + ca.QuemAgendou(ag).Rows[0]["outros"].ToString();
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                    }
                }
                else
                {
                    hr.Modulo = comboBox2.Text.Remove(2, num - 2);
                    ca.Agendar(ag);
                    if (checkBox1.Checked != true)
                    {
                        SelecionaAgenda(ch.HorariodeAulas(hr).Rows[0]["Periodo"].ToString());
                    }
                    else
                    {
                        hr.Modulo = comboBox2.Text.Remove(2, num - 2);
                        SelecionaOutraAgenda(comboBox4.Text);
                    }
                }
            }
            else if (tabPage2.Focus())
            {
                ag.MesmoPeriodo = "";
                if (agendado == "ok")
                {
                    if (ca.QuemAgendou(ag).Rows[0]["curso"].ToString() != "")
                    {
                        string msg = "Agendado por " + ca.QuemAgendou(ag).Rows[0]["responsavel"].ToString() + " - " + ca.QuemAgendou(ag).Rows[0]["curso"].ToString();
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                    }
                    else
                    {
                        string msg = "Agendado por " + ca.QuemAgendou(ag).Rows[0]["responsavel"].ToString() + " - " + ca.QuemAgendou(ag).Rows[0]["outros"].ToString();
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                    }
                }
                else
                {
                    ag.Responsavel = resp;
                    ag.Periodo = comboBox5.Text;
                    ag.Outros = textBox1.Text;
                    ca.Agendar(ag);
                    SelecionaOutraAgenda(comboBox5.Text);
                }
            }
        }

        public void Horarios(string cr, string pr)
        {
            int num = cr.Length;
            hr.Curso = cr.Remove(0, 3);
            hr.Modulo = cr.Remove(2, num - 2);

            if ((ch.HorariodeAulas(hr).Rows[0]["Periodo"].ToString() == "Manhã" && comboBox2.Text.Contains("NovoTec")) || pr == "Manhã")
            {
                SelecionaCurso(ch.NumAulas(hr).Rows[0]["num_aulas"].GetHashCode());
                hr.Periodo = "Manhã";
                label4.Text = ch.SelecionaHorarios(hr).Rows[0]["pri_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["pri_final"].ToString();
                label5.Text = ch.SelecionaHorarios(hr).Rows[0]["seg_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["seg_final"].ToString();
                label6.Text = ch.SelecionaHorarios(hr).Rows[0]["ter_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["ter_final"].ToString();
                label7.Text = ch.SelecionaHorarios(hr).Rows[0]["qua_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["qua_final"].ToString();
                label8.Text = ch.SelecionaHorarios(hr).Rows[0]["qui_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["qui_final"].ToString();
                label9.Text = ch.SelecionaHorarios(hr).Rows[0]["sex_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["sex_final"].ToString();
            }
            else if ((ch.HorariodeAulas(hr).Rows[0]["Periodo"].ToString() == "Integral") || pr == "Integral")
            {
                SelecionaCurso(ch.NumAulas(hr).Rows[0]["num_aulas"].GetHashCode());
                hr.Periodo = "Integral";
                label4.Text = ch.SelecionaHorarios(hr).Rows[0]["pri_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["pri_final"].ToString();
                label5.Text = ch.SelecionaHorarios(hr).Rows[0]["seg_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["seg_final"].ToString();
                label6.Text = ch.SelecionaHorarios(hr).Rows[0]["ter_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["ter_final"].ToString();
                label7.Text = ch.SelecionaHorarios(hr).Rows[0]["qua_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["qua_final"].ToString();
                label8.Text = ch.SelecionaHorarios(hr).Rows[0]["qui_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["qui_final"].ToString();
                label9.Text = ch.SelecionaHorarios(hr).Rows[0]["sex_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["sex_final"].ToString();
                label10.Text = ch.SelecionaHorarios(hr).Rows[0]["set_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["set_final"].ToString();
                label11.Text = ch.SelecionaHorarios(hr).Rows[0]["oit_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["oit_final"].ToString();
            }
            else if (ch.HorariodeAulas(hr).Rows[0]["Periodo"].ToString() == "Tarde")
            {
                SelecionaCurso(ch.NumAulas(hr).Rows[0]["num_aulas"].GetHashCode());
                hr.Periodo = "Tarde";
                label4.Text = ch.SelecionaHorarios(hr).Rows[0]["pri_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["pri_final"].ToString();
                label5.Text = ch.SelecionaHorarios(hr).Rows[0]["seg_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["seg_final"].ToString();
                label6.Text = ch.SelecionaHorarios(hr).Rows[0]["ter_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["ter_final"].ToString();
                label7.Text = ch.SelecionaHorarios(hr).Rows[0]["qua_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["qua_final"].ToString();
            }
            else if (ch.HorariodeAulas(hr).Rows[0]["Periodo"].ToString() == "Noite")
            {
                SelecionaCurso(ch.NumAulas(hr).Rows[0]["num_aulas"].GetHashCode());
                hr.Periodo = "Noite";
                label4.Text = ch.SelecionaHorarios(hr).Rows[0]["pri_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["pri_final"].ToString();
                label5.Text = ch.SelecionaHorarios(hr).Rows[0]["seg_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["seg_final"].ToString();
                label6.Text = ch.SelecionaHorarios(hr).Rows[0]["ter_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["ter_final"].ToString();
                label7.Text = ch.SelecionaHorarios(hr).Rows[0]["qua_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["qua_final"].ToString();
            }
            else if (ch.HorariodeAulas(hr).Rows[0]["Periodo"].ToString() == "Manhã" && comboBox2.Text.Contains("Médio"))
            {
                SelecionaCurso(ch.NumAulas(hr).Rows[0]["num_aulas"].GetHashCode());
                hr.Periodo = "Manhã";
                label4.Text = ch.SelecionaHorarios(hr).Rows[0]["pri_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["pri_final"].ToString();
                label5.Text = ch.SelecionaHorarios(hr).Rows[0]["seg_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["seg_final"].ToString();
                label6.Text = ch.SelecionaHorarios(hr).Rows[0]["ter_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["ter_final"].ToString();
                label7.Text = ch.SelecionaHorarios(hr).Rows[0]["qua_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["qua_final"].ToString();
                label8.Text = ch.SelecionaHorarios(hr).Rows[0]["qui_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["qui_final"].ToString();
            }
        }

        public void OutroHorario(string per)
        {
            if (per == "Manhã")
            {
                SelecionaCurso(6);
                hr.Periodo = "Manhã";
                label4.Text = ch.SelecionaOutroHorario(hr).Rows[0]["pri_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["pri_final"].ToString();
                label5.Text = ch.SelecionaOutroHorario(hr).Rows[0]["seg_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["seg_final"].ToString();
                label6.Text = ch.SelecionaOutroHorario(hr).Rows[0]["ter_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["ter_final"].ToString();
                label7.Text = ch.SelecionaOutroHorario(hr).Rows[0]["qua_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["qua_final"].ToString();
                label8.Text = ch.SelecionaOutroHorario(hr).Rows[0]["qui_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["qui_final"].ToString();
                label9.Text = ch.SelecionaOutroHorario(hr).Rows[0]["sex_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["sex_final"].ToString();
            }
            else if (per == "Integral")
            {
                SelecionaCurso(8);
                hr.Periodo = "Integral";
                label4.Text = ch.SelecionaOutroHorario(hr).Rows[0]["pri_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["pri_final"].ToString();
                label5.Text = ch.SelecionaOutroHorario(hr).Rows[0]["seg_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["seg_final"].ToString();
                label6.Text = ch.SelecionaOutroHorario(hr).Rows[0]["ter_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["ter_final"].ToString();
                label7.Text = ch.SelecionaOutroHorario(hr).Rows[0]["qua_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["qua_final"].ToString();
                label8.Text = ch.SelecionaOutroHorario(hr).Rows[0]["qui_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["qui_final"].ToString();
                label9.Text = ch.SelecionaOutroHorario(hr).Rows[0]["sex_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["sex_final"].ToString();
                label10.Text = ch.SelecionaOutroHorario(hr).Rows[0]["set_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["set_final"].ToString();
                label11.Text = ch.SelecionaOutroHorario(hr).Rows[0]["oit_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["oit_final"].ToString();
            }
            else if (per == "Tarde")
            {
                SelecionaCurso(4);
                hr.Periodo = "Tarde";
                label4.Text = ch.SelecionaOutroHorario(hr).Rows[0]["pri_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["pri_final"].ToString();
                label5.Text = ch.SelecionaOutroHorario(hr).Rows[0]["seg_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["seg_final"].ToString();
                label6.Text = ch.SelecionaOutroHorario(hr).Rows[0]["ter_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["ter_final"].ToString();
                label7.Text = ch.SelecionaOutroHorario(hr).Rows[0]["qua_inicio"].ToString() + " a " + ch.SelecionaOutroHorario(hr).Rows[0]["qua_final"].ToString();
            }
            else if (per == "Noite")
            {
                SelecionaCurso(4);
                hr.Periodo = "Noite";
                label4.Text = ch.SelecionaHorarios(hr).Rows[0]["pri_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["pri_final"].ToString();
                label5.Text = ch.SelecionaHorarios(hr).Rows[0]["seg_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["seg_final"].ToString();
                label6.Text = ch.SelecionaHorarios(hr).Rows[0]["ter_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["ter_final"].ToString();
                label7.Text = ch.SelecionaHorarios(hr).Rows[0]["qua_inicio"].ToString() + " a " + ch.SelecionaHorarios(hr).Rows[0]["qua_final"].ToString();
            } 
        }

        public void SelecionaAgenda(string periodo)
        {
            if (checkBox1.Checked != true)
                Horarios(comboBox2.Text, "");
            else
                OutroHorario(periodo);
            ag.Data = monthCalendar1.SelectionStart.ToString();
            ag.Local = comboBox1.Text;
            ag.Periodo = periodo;
                        
            foreach (DataRow datas in ca.SelecionaAgenda(ag).Rows)
            {
               if (datas["periodo"].ToString() == periodo || datas["coincide"].ToString() == periodo)
                {
                    if (datas["periodo"].ToString() == "Manhã" && datas["aula"].GetHashCode() == 1 && datas["periodo"].ToString() == periodo)
                    {
                        button1.BackColor = Color.Red;
                        codAula1 = datas["cod"].GetHashCode();
                    }
                    else if (datas["periodo"].ToString() == "Integral" && datas["aula"].GetHashCode() == 1 && datas["periodo"].ToString() == periodo)
                    {
                        button1.BackColor = Color.Red;
                        codAula1 = datas["cod"].GetHashCode();
                    }
                    else if (datas["periodo"].ToString() == "Tarde" && datas["aula"].GetHashCode() == 1 && datas["periodo"].ToString() == periodo)
                    {
                        button1.BackColor = Color.Red;
                        codAula1 = datas["cod"].GetHashCode();
                    }
                    else if (datas["periodo"].ToString() == "Noite" && datas["aula"].GetHashCode() == 1 && datas["periodo"].ToString() == periodo)
                    {
                        button1.BackColor = Color.Red;
                        codAula1 = datas["cod"].GetHashCode();
                    }
                    else if (datas["periodo"].ToString() == "Integral" && datas["coincide"].ToString() == "Manhã" && datas["aula"].GetHashCode() == 1)
                    {
                        button1.BackColor = Color.Red;
                        codAula1 = datas["cod"].GetHashCode();
                    }
                    else if (datas["periodo"].ToString() == "Manhã" && datas["coincide"].ToString() == "Integral" && datas["aula"].GetHashCode() == 1)
                    {
                        button1.BackColor = Color.Red;
                        codAula1 = datas["cod"].GetHashCode();
                    }
                    else if ((datas["coincide"].ToString() == "Tarde" && datas["aula"].GetHashCode() == 6) && datas["periodo"].ToString() != periodo)
                    {
                        button1.BackColor = Color.Red;
                        codAula1 = datas["cod"].GetHashCode();
                    }

                    if ((datas["periodo"].ToString() == "Manhã" || datas["periodo"].ToString() == "Integral" || datas["periodo"].ToString() == "Tarde" || datas["periodo"].ToString() == "Noite") && datas["aula"].GetHashCode() == 2)
                    {
                        button2.BackColor = Color.Red;
                        codAula2 = datas["cod"].GetHashCode();
                    }
                    else if ((datas["coincide"].ToString() == "Tarde" && datas["aula"].GetHashCode() == 7) && datas["periodo"].ToString() != periodo)
                    {
                        button2.BackColor = Color.Red;
                        codAula2 = datas["cod"].GetHashCode();
                    }
                    if ((datas["aula"].GetHashCode() == 3 && datas["periodo"].ToString() == periodo) || (datas["aula"].GetHashCode() == 3 && datas["coincide"].ToString() == periodo))
                    {
                        button4.BackColor = Color.Red;
                        codAula3 = datas["cod"].GetHashCode();
                    }
                    if ((datas["aula"].GetHashCode() == 4 && datas["periodo"].ToString() == periodo) || (datas["aula"].GetHashCode() == 4 && datas["coincide"].ToString() == periodo))
                    {
                        button6.BackColor = Color.Red;
                        codAula4 = datas["cod"].GetHashCode();
                    }
                    if ((datas["aula"].GetHashCode() == 5 && datas["periodo"].ToString() == periodo) || (datas["aula"].GetHashCode() == 5 && datas["coincide"].ToString() == periodo))
                    {
                        button7.BackColor = Color.Red;
                        codAula5 = datas["cod"].GetHashCode();
                    }
                    if (datas["periodo"].ToString() == "Integral" && datas["aula"].GetHashCode() == 6)
                    {
                        button8.BackColor = Color.Red;
                        codAula6 = datas["cod"].GetHashCode();
                    }
                    else if ((datas["coincide"].ToString() == "Integral" && datas["periodo"].ToString() == "Tarde" && datas["aula"].GetHashCode() == 1) && datas["periodo"].ToString() != periodo)
                    {
                        button8.BackColor = Color.Red;
                        codAula6 = datas["cod"].GetHashCode();
                    }
                    if (datas["periodo"].ToString() == "Integral" && datas["aula"].GetHashCode() == 7)
                    {
                        button10.BackColor = Color.Red;
                        codAula7 = datas["cod"].GetHashCode();
                    }
                    else if ((datas["coincide"].ToString() == "Integral" && datas["periodo"].ToString() == "Tarde" && datas["aula"].GetHashCode() == 2) && datas["periodo"].ToString() != periodo)
                    {
                        button10.BackColor = Color.Red;
                        codAula7 = datas["cod"].GetHashCode();
                    }
                    if ((datas["aula"].GetHashCode() == 8 && datas["periodo"].ToString() == periodo) || (datas["aula"].GetHashCode() == 8 && datas["coincide"].ToString() == periodo))
                    {
                        button11.BackColor = Color.Red;
                        codAula8 = datas["cod"].GetHashCode();
                    }        
                }
            }
        }

        public void SelecionaOutraAgenda(string periodo)
        {
            OutroHorario(periodo);
            ag.Data = monthCalendar1.SelectionStart.ToString();
            ag.Local = comboBox1.Text;
            ag.Periodo = periodo;
 
            foreach (DataRow datas in ca.SelecionaAgenda(ag).Rows)
            {
                if (datas["periodo"].ToString() == periodo || datas["coincide"].ToString() == periodo)
                {
                    if (datas["periodo"].ToString() == "Manhã" && datas["aula"].GetHashCode() == 1 && datas["periodo"].ToString() == periodo)
                    {
                        button1.BackColor = Color.Red;
                        codAula1 = datas["cod"].GetHashCode();
                    }
                    else if (datas["periodo"].ToString() == "Integral" && datas["aula"].GetHashCode() == 1 && datas["periodo"].ToString() == periodo)
                    {
                        button1.BackColor = Color.Red;
                        codAula1 = datas["cod"].GetHashCode();
                    }
                    else if (datas["periodo"].ToString() == "Tarde" && datas["aula"].GetHashCode() == 1 && datas["periodo"].ToString() == periodo)
                    {
                        button1.BackColor = Color.Red;
                        codAula1 = datas["cod"].GetHashCode();
                    }
                    else if (datas["periodo"].ToString() == "Noite" && datas["aula"].GetHashCode() == 1 && datas["periodo"].ToString() == periodo)
                    {
                        button1.BackColor = Color.Red;
                        codAula1 = datas["cod"].GetHashCode();
                    }
                    else if (datas["periodo"].ToString() == "Integral" && datas["coincide"].ToString() == "Manhã" && datas["aula"].GetHashCode() == 1)
                    {
                        button1.BackColor = Color.Red;
                        codAula1 = datas["cod"].GetHashCode();
                    }
                    else if (datas["periodo"].ToString() == "Manhã" && datas["coincide"].ToString() == "Integral" && datas["aula"].GetHashCode() == 1)
                    {
                        button1.BackColor = Color.Red;
                        codAula1 = datas["cod"].GetHashCode();
                    }
                    else if ((datas["coincide"].ToString() == "Tarde" && datas["aula"].GetHashCode() == 6) && datas["periodo"].ToString() != periodo)
                    {
                        button1.BackColor = Color.Red;
                        codAula1 = datas["cod"].GetHashCode();
                    }

                    if ((datas["periodo"].ToString() == "Manhã" || datas["periodo"].ToString() == "Integral" || datas["periodo"].ToString() == "Tarde" || datas["periodo"].ToString() == "Noite") && datas["aula"].GetHashCode() == 2)
                    {
                        button2.BackColor = Color.Red;
                        codAula2 = datas["cod"].GetHashCode();
                    }
                    else if ((datas["coincide"].ToString() == "Tarde" && datas["aula"].GetHashCode() == 7) && datas["periodo"].ToString() != periodo)
                    {
                        button2.BackColor = Color.Red;
                        codAula2 = datas["cod"].GetHashCode();
                    }
                    if ((datas["aula"].GetHashCode() == 3 && datas["periodo"].ToString() == periodo) || (datas["aula"].GetHashCode() == 3 && datas["coincide"].ToString() == periodo))
                    {
                        button4.BackColor = Color.Red;
                        codAula3 = datas["cod"].GetHashCode();
                    }
                    if ((datas["aula"].GetHashCode() == 4 && datas["periodo"].ToString() == periodo) || (datas["aula"].GetHashCode() == 4 && datas["coincide"].ToString() == periodo))
                    {
                        button6.BackColor = Color.Red;
                        codAula4 = datas["cod"].GetHashCode();
                    }
                    if ((datas["aula"].GetHashCode() == 5 && datas["periodo"].ToString() == periodo) || (datas["aula"].GetHashCode() == 5 && datas["coincide"].ToString() == periodo))
                    {
                        button7.BackColor = Color.Red;
                        codAula5 = datas["cod"].GetHashCode();
                    }
                    if (datas["periodo"].ToString() == "Integral" && datas["aula"].GetHashCode() == 6)
                    {
                        button8.BackColor = Color.Red;
                        codAula6 = datas["cod"].GetHashCode();
                    }
                    else if ((datas["coincide"].ToString() == "Integral" && datas["periodo"].ToString() == "Tarde" && datas["aula"].GetHashCode() == 1) && datas["periodo"].ToString() != periodo)
                    {
                        button8.BackColor = Color.Red;
                        codAula6 = datas["cod"].GetHashCode();
                    }
                    if (datas["periodo"].ToString() == "Integral" && datas["aula"].GetHashCode() == 7)
                    {
                        button10.BackColor = Color.Red;
                        codAula7 = datas["cod"].GetHashCode();
                    }
                    else if ((datas["coincide"].ToString() == "Integral" && datas["periodo"].ToString() == "Tarde" && datas["aula"].GetHashCode() == 2) && datas["periodo"].ToString() != periodo)
                    {
                        button10.BackColor = Color.Red;
                        codAula7 = datas["cod"].GetHashCode();
                    }
                    if ((datas["aula"].GetHashCode() == 8 && datas["periodo"].ToString() == periodo) || (datas["aula"].GetHashCode() == 8 && datas["coincide"].ToString() == periodo))
                    {
                        button11.BackColor = Color.Red;
                        codAula8 = datas["cod"].GetHashCode();
                    }
                }
            }
        }

        public void Atualizar()
        {
            if (AuxClas.codigo == 1)
                button1.BackColor = Color.Green;
            if (AuxClas.codigo == 2)
                button2.BackColor = Color.Green;
            if (AuxClas.codigo == 3)
                button4.BackColor = Color.Green;
            if (AuxClas.codigo == 4)
                button6.BackColor = Color.Green;
            if (AuxClas.codigo == 5)
                button7.BackColor = Color.Green;
            if (AuxClas.codigo == 6)
                button8.BackColor = Color.Green;
            if (AuxClas.codigo == 7)
                button10.BackColor = Color.Green;
            if (AuxClas.codigo == 8)
                button11.BackColor = Color.Green;
        }

        private void frmAgenda_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.VisualStyleState = VisualStyleState.ClientAreaEnabled;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = true;
            if (checkBox1.Checked == true)
            {
                LiberaAgenda();
                SelecionaAgenda(comboBox4.Text);
            }
            else
            {
                LiberaAgenda();
                int num = comboBox2.Text.Length;
                hr.Curso = comboBox2.Text.Remove(0, 3);
                hr.Modulo = comboBox2.Text.Remove(2, num - 2);
                SelecionaAgenda(ch.HorariodeAulas(hr).Rows[0]["Periodo"].ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                comboBox4.Enabled = true;
            }
            else if (checkBox1.Checked == false)
            {
                comboBox4.Enabled = false;
                comboBox4.Text = "";
            }
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            LiberaAgenda();
            SelecionaOutraAgenda(comboBox4.Text);
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = true;
            LiberaAgenda();
            SelecionaOutraAgenda(comboBox5.Text);
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            label17.Text = monthCalendar1.SelectionStart.ToString("dd/MM/yyyy");
            comboBox2.Text = "";
            checkBox1.Checked = false;
            comboBox4.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabPage1.Focus())
            {
                if (comboBox1.Text == "")
                {
                    string msg = "Selecione o que você deseja agendar";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (label17.Text == "")
                {
                    string msg = "Selecione uma data";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (comboBox2.Text == "")
                {
                    string msg = "Selecione o curso";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (comboBox3.Text == "" && button1.BackColor == Color.Green)
                {
                    string msg = "Selecione o professor";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (checkBox1.Checked == true && comboBox4.Text == "")
                {
                    string msg = "Selecione o período";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    if (button1.BackColor == Color.Red)
                        Agendar(1, "ok", codAula1);
                    else
                        Agendar(1, "", codAula1);
                }
            }
            else if (tabPage2.Focus())
            {
                if (comboBox5.Text == "")
                {
                    string msg = "Selecione o período";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (textBox1.Text == "" && button1.BackColor == Color.Green)
                {
                    string msg = "Informe o motivo do agendamento";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    if (button1.BackColor == Color.Red)
                        Agendar(1, "ok", codAula1);
                    else
                        Agendar(1, "", codAula1);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tabPage1.Focus())
            {
                if (comboBox1.Text == "")
                {
                    string msg = "Selecione o que você deseja agendar";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (label17.Text == "")
                {
                    string msg = "Selecione uma data";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (comboBox2.Text == "")
                {
                    string msg = "Selecione o curso";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (comboBox3.Text == "" && button2.BackColor == Color.Green)
                {
                    string msg = "Selecione o professor";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (checkBox1.Checked == true && comboBox4.Text == "")
                {
                    string msg = "Selecione o período";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    if (button2.BackColor == Color.Red)
                        Agendar(2, "ok", codAula2);
                    else
                        Agendar(2, "", codAula2);
                }
            }
            else if (tabPage2.Focus())
            {
                if (comboBox5.Text == "")
                {
                    string msg = "Selecione o período";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (textBox1.Text == "" && button2.BackColor == Color.Green)
                {
                    string msg = "Informe o motivo do agendamento";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    if (button2.BackColor == Color.Red)
                        Agendar(2, "ok", codAula2);
                    else
                        Agendar(2, "", codAula2);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (tabPage1.Focus())
            {
                if (comboBox1.Text == "")
                {
                    string msg = "Selecione o que você deseja agendar";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (label17.Text == "")
                {
                    string msg = "Selecione uma data";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (comboBox2.Text == "")
                {
                    string msg = "Selecione o curso";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (comboBox3.Text == "" && button4.BackColor == Color.Green)
                {
                    string msg = "Selecione o professor";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (checkBox1.Checked == true && comboBox4.Text == "")
                {
                    string msg = "Selecione o período";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    if (button4.BackColor == Color.Red)
                        Agendar(3, "ok", codAula3);
                    else
                        Agendar(3, "", codAula3);
                }
            }
            else if (tabPage2.Focus())
            {
                if (comboBox5.Text == "")
                {
                    string msg = "Selecione o período";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (textBox1.Text == "" && button4.BackColor == Color.Green)
                {
                    string msg = "Informe o motivo do agendamento";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    if (button4.BackColor == Color.Red)
                        Agendar(3, "ok", codAula3);
                    else
                        Agendar(3, "", codAula3);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (tabPage1.Focus())
            {
                if (comboBox1.Text == "")
                {
                    string msg = "Selecione o que você deseja agendar";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (label17.Text == "")
                {
                    string msg = "Selecione uma data";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (comboBox2.Text == "")
                {
                    string msg = "Selecione o curso";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (comboBox3.Text == "" && button6.BackColor == Color.Green)
                {
                    string msg = "Selecione o professor";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (checkBox1.Checked == true && comboBox4.Text == "")
                {
                    string msg = "Selecione o período";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    if (button6.BackColor == Color.Red)
                        Agendar(4, "ok", codAula4);
                    else
                        Agendar(4, "", codAula4);
                }
            }
            else if (tabPage2.Focus())
            {
                if (comboBox5.Text == "")
                {
                    string msg = "Selecione o período";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (textBox1.Text == "" && button6.BackColor == Color.Green)
                {
                    string msg = "Informe o motivo do agendamento";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    if (button6.BackColor == Color.Red)
                        Agendar(4, "ok", codAula4);
                    else
                        Agendar(4, "", codAula4);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (tabPage1.Focus())
            {
                if (comboBox1.Text == "")
                {
                    string msg = "Selecione o que você deseja agendar";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (label17.Text == "")
                {
                    string msg = "Selecione uma data";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (comboBox2.Text == "")
                {
                    string msg = "Selecione o curso";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (comboBox3.Text == "" && button7.BackColor == Color.Green)
                {
                    string msg = "Selecione o professor";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (checkBox1.Checked == true && comboBox4.Text == "")
                {
                    string msg = "Selecione o período";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    if (button7.BackColor == Color.Red)
                        Agendar(5, "ok", codAula5);
                    else
                        Agendar(5, "", codAula5);
                }
            }
            else if (tabPage2.Focus())
            {
                if (comboBox5.Text == "")
                {
                    string msg = "Selecione o período";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (textBox1.Text == "" && button7.BackColor == Color.Green)
                {
                    string msg = "Informe o motivo do agendamento";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    if (button7.BackColor == Color.Red)
                        Agendar(5, "ok", codAula5);
                    else
                        Agendar(5, "", codAula5);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (tabPage1.Focus())
            {
                if (comboBox1.Text == "")
                {
                    string msg = "Selecione o que você deseja agendar";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (label17.Text == "")
                {
                    string msg = "Selecione uma data";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (comboBox2.Text == "")
                {
                    string msg = "Selecione o curso";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (comboBox3.Text == "" && button8.BackColor == Color.Green)
                {
                    string msg = "Selecione o professor";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (checkBox1.Checked == true && comboBox4.Text == "")
                {
                    string msg = "Selecione o período";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    if (button8.BackColor == Color.Red)
                        Agendar(6, "ok", codAula6);
                    else
                        Agendar(6, "", codAula6);
                }
            }
            else if (tabPage2.Focus())
            {
                if (comboBox5.Text == "")
                {
                    string msg = "Selecione o período";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (textBox1.Text == "" && button8.BackColor == Color.Green)
                {
                    string msg = "Informe o motivo do agendamento";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    if (button8.BackColor == Color.Red)
                        Agendar(6, "ok", codAula6);
                    else
                        Agendar(6, "", codAula6);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (tabPage1.Focus())
            {
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("Selecione a sala ou laboratório");
                }
                else if (label17.Text == "")
                {
                    MessageBox.Show("Selecione uma data");
                }
                else if (comboBox2.Text == "")
                {
                    MessageBox.Show("Selecione um curso");
                }
                else if (comboBox3.Text == "" && button10.BackColor == Color.Green)
                {
                    MessageBox.Show("Selecione o Professor");
                }
                else if (checkBox1.Checked == true && comboBox4.Text == "")
                {
                    MessageBox.Show("Selecione um período");
                }
                else
                {if (comboBox1.Text == "")
                {
                    string msg = "Selecione o que você deseja agendar";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (label17.Text == "")
                {
                    string msg = "Selecione uma data";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (comboBox2.Text == "")
                {
                    string msg = "Selecione o curso";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (comboBox3.Text == "")
                {
                    string msg = "Selecione o professor";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (checkBox1.Checked == true && comboBox4.Text == "")
                {
                    string msg = "Selecione o período";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                    if (button10.BackColor == Color.Red)
                        Agendar(7, "ok", codAula7);
                    else
                        Agendar(7, "", codAula7);
                }
            }
            else if (tabPage2.Focus())
            {
                if (comboBox5.Text == "")
                {
                    string msg = "Selecione o período";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (textBox1.Text == "" && button10.BackColor == Color.Green)
                {
                    string msg = "Informe o motivo do agendamento";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    if (button10.BackColor == Color.Red)
                        Agendar(7, "ok", codAula7);
                    else
                        Agendar(7, "", codAula7);
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (tabPage1.Focus())
            {
                if (comboBox1.Text == "")
                {
                    string msg = "Selecione o que você deseja agendar";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (label17.Text == "")
                {
                    string msg = "Selecione uma data";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (comboBox2.Text == "")
                {
                    string msg = "Selecione o curso";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (comboBox3.Text == "" && button11.BackColor == Color.Green)
                {
                    string msg = "Selecione o professor";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (checkBox1.Checked == true && comboBox4.Text == "")
                {
                    string msg = "Selecione o período";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    if (button11.BackColor == Color.Red)
                        Agendar(8, "ok", codAula8);
                    else
                        Agendar(8, "", codAula8);
                }
            }
            else if (tabPage2.Focus())
            {
                if (comboBox5.Text == "")
                {
                    string msg = "Selecione o período";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (textBox1.Text == "" && button11.BackColor == Color.Green)
                {
                    string msg = "Informe o motivo do agendamento";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    if (button11.BackColor == Color.Red)
                        Agendar(8, "ok", codAula8);
                    else
                        Agendar(8, "", codAula8);
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                comboBox2.Text = "";
                comboBox3.Text = "";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                comboBox4.Enabled = true;
            }
            else if (checkBox1.Checked == false)
            {
                comboBox4.Enabled = false;
                comboBox4.Text = "";
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            it.Categoria = comboBox6.Text;
            it.CodCategoria = ci.CodCategoria(it).Rows[0][0].GetHashCode();       
            comboBox1.Items.Clear();
            int item = ci.SelecionaItensCategoria(it).Rows.Count;
            for (int j = 0; j < item; j++)
            {
                comboBox1.Items.Add(ci.SelecionaItensCategoria(it).Rows[j]["item"].ToString());
            }
            label19.Visible = true;
            label19.Text = comboBox6.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            us.Nome = resp;
            ag.Codigo = codAula1;
            botao = 1;
            if ((ca.Professor(us).Rows[0]["func1"].ToString() == "Secretaria" || ca.Professor(us).Rows[0]["func1"].ToString() == "Diretoria de Serviços" || ca.Professor(us).Rows[0]["func2"].ToString() == "Coordenador" || ca.Professor(us).Rows[0]["func1"].ToString() == "Direção" || ca.Professor(us).Rows[0]["func1"].ToString() == "Orientador Educacional") && button1.BackColor == Color.Red)
            {
                mensagem = "DESEJA REALMENTE CANCELAR ESSE AGENDAMENTO?";
                frmDecisao frm = new frmDecisao(codAula1, mensagem, resp, botao);
                frm.Text = "CANCELAR AGENDAMENTO";
                frm.Owner = this;
                frm.ShowDialog();
            }
            else if (button1.BackColor == Color.Green)
            {
                string msg = "Este horário não possui agendamento!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (ca.Professor(us).Rows[0]["func1"].ToString() == "Professor" && ca.Professor(us).Rows[0]["nome"].ToString() != ca.Responsavel(ag).Rows[0]["responsavel"].ToString())
            {
                string msg = "Você não pode cancelar esse agendamento, procure um coordenador";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            us.Nome = resp;
            ag.Codigo = codAula2;
            if (button2.BackColor == Color.Green)
            {
                string msg = "Este horário não possui agendamento!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (ca.Professor(us).Rows[0]["func1"].ToString() == "Professor" && ca.Professor(us).Rows[0]["nome"].ToString() != ca.Responsavel(ag).Rows[0]["responsavel"].ToString())
            {
                string msg = "Você não pode cancelar esse agendamento, procure um coordenador";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                botao = 2;
                mensagem = "DESEJA REALMENTE CANCELAR ESSE AGENDAMENTO?";
                frmDecisao frm = new frmDecisao(codAula2, mensagem, resp, botao);
                frm.Text = "CANCELAR AGENDAMENTO";
                frm.Owner = this;
                frm.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            us.Nome = resp;
            ag.Codigo = codAula3;
            if (button4.BackColor == Color.Green)
            {
                string msg = "Este horário não possui agendamento!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (ca.Professor(us).Rows[0]["func1"].ToString() == "Professor" && ca.Professor(us).Rows[0]["nome"].ToString() != ca.Responsavel(ag).Rows[0]["responsavel"].ToString())
            {
                string msg = "Você não pode cancelar esse agendamento, procure um coordenador";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                botao = 3;
                mensagem = "DESEJA REALMENTE CANCELAR ESSE AGENDAMENTO?";
                frmDecisao frm = new frmDecisao(codAula3, mensagem, resp, botao);
                frm.Text = "CANCELAR AGENDAMENTO";
                frm.Owner = this;
                frm.ShowDialog();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            us.Nome = resp;
            ag.Codigo = codAula4;
            if (button6.BackColor == Color.Green)
            {
                string msg = "Este horário não possui agendamento!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (ca.Professor(us).Rows[0]["func1"].ToString() == "Professor" && ca.Professor(us).Rows[0]["nome"].ToString() != ca.Responsavel(ag).Rows[0]["responsavel"].ToString())
            {
                string msg = "Você não pode cancelar esse agendamento, procure um coordenador";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                botao = 4;
                mensagem = "DESEJA REALMENTE CANCELAR ESSE AGENDAMENTO?";
                frmDecisao frm = new frmDecisao(codAula4, mensagem, resp, botao);
                frm.Text = "CANCELAR AGENDAMENTO";
                frm.Owner = this;
                frm.ShowDialog();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            us.Nome = resp;
            ag.Codigo = codAula5;
            if (button7.BackColor == Color.Green)
            {
                string msg = "Este horário não possui agendamento!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (ca.Professor(us).Rows[0]["func1"].ToString() == "Professor" && ca.Professor(us).Rows[0]["nome"].ToString() != ca.Responsavel(ag).Rows[0]["responsavel"].ToString())
            {
                string msg = "Você não pode cancelar esse agendamento, procure um coordenador";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                botao = 5;
                mensagem = "DESEJA REALMENTE CANCELAR ESSE AGENDAMENTO?";
                frmDecisao frm = new frmDecisao(codAula5, mensagem, resp, botao);
                frm.Text = "CANCELAR AGENDAMENTO";
                frm.Owner = this;
                frm.ShowDialog();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            us.Nome = resp;
            ag.Codigo = codAula6;
            if (button8.BackColor == Color.Green)
            {
                string msg = "Este horário não possui agendamento!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (ca.Professor(us).Rows[0]["func1"].ToString() == "Professor" && ca.Professor(us).Rows[0]["nome"].ToString() != ca.Responsavel(ag).Rows[0]["responsavel"].ToString())
            {
                string msg = "Você não pode cancelar esse agendamento, procure um coordenador";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                botao = 6;
                mensagem = "DESEJA REALMENTE CANCELAR ESSE AGENDAMENTO?";
                frmDecisao frm = new frmDecisao(codAula6, mensagem, resp, botao);
                frm.Text = "CANCELAR AGENDAMENTO";
                frm.Owner = this;
                frm.ShowDialog();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            us.Nome = resp;
            ag.Codigo = codAula7;
            if (button10.BackColor == Color.Green)
            {
                string msg = "Este horário não possui agendamento!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (ca.Professor(us).Rows[0]["func1"].ToString() == "Professor" && ca.Professor(us).Rows[0]["nome"].ToString() != ca.Responsavel(ag).Rows[0]["responsavel"].ToString())
            {
                string msg = "Você não pode cancelar esse agendamento, procure um coordenador";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                botao = 7;
                mensagem = "DESEJA REALMENTE CANCELAR ESSE AGENDAMENTO?";
                frmDecisao frm = new frmDecisao(codAula7, mensagem, resp, botao);
                frm.Text = "CANCELAR AGENDAMENTO";
                frm.Owner = this;
                frm.ShowDialog();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            us.Nome = resp;
            ag.Codigo = codAula8;
            if (button11.BackColor == Color.Green)
            {
                string msg = "Este horário não possui agendamento!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (ca.Professor(us).Rows[0]["func1"].ToString() == "Professor" && ca.Professor(us).Rows[0]["nome"].ToString() != ca.Responsavel(ag).Rows[0]["responsavel"].ToString())
            {
                string msg = "Você não pode cancelar esse agendamento, procure um coordenador";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                botao = 8;
                mensagem = "DESEJA REALMENTE CANCELAR ESSE AGENDAMENTO?";
                frmDecisao frm = new frmDecisao(codAula8, mensagem, resp, botao);
                frm.Text = "CANCELAR AGENDAMENTO";
                frm.Owner = this;
                frm.ShowDialog();
            }
        }
    }
}

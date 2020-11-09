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
    public partial class frmAcoes : Form
    {
        Acoes ac = new Acoes();
        ConectaAc ca = new ConectaAc();
        frmCarregando cr = new frmCarregando();

        public frmAcoes()
        {
            InitializeComponent();
        }
        
        private void CarregaFormAguarde()
        {
            cr.ShowDialog();
        }

        private void Acoes()
        {
            if (radioButton1.Checked == true)
            {
                ac.Pesquisa = "Data";
                ac.Data = Convert.ToDateTime(dateTimePicker1.Text);
                if (ca.BuscaAcoes(ac).Rows.Count == 0)
                {
                    string msg = "NÃO EXISTE NENHUMA AÇÃO CADASTRADA NESSA DATA";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
            }
            else if (radioButton2.Checked == true)
            {
                if (comboBox1.Text == "")
                {
                    string msg = "INFORMAR UM USUÁRIO";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                    comboBox1.Focus();
                }
                else
                {
                    ac.Pesquisa = "Usuario";
                    ac.Nome = comboBox1.Text;
                }
            }
            else if (radioButton3.Checked == true)
            {
                if (comboBox2.Text == "")
                {
                    string msg = "INFORMAR O NÚMERO DA REQUISIÇÃO";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                    comboBox2.Focus();
                }
                else
                {
                    ac.Pesquisa = "Requisicao";
                    ac.Requisição = Convert.ToInt32(comboBox2.Text);
                }
            }


            dataGridView1.Rows.Clear();
                foreach (DataRow item in ca.BuscaAcoes(ac).Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item["requisicao"].GetHashCode();
                    dataGridView1.Rows[n].Cells[1].Value = item["nome"].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item["acao"].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = Convert.ToDateTime(item["data"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows[n].Cells[4].Value = item["hora"].ToString();
                }
        }

        private void frmAcoes_Load(object sender, EventArgs e)
        {
            System.Threading.Thread tFormAguarde = new System.Threading.Thread(new System.Threading.ThreadStart(CarregaFormAguarde));
            tFormAguarde.Start();
            radioButton1.Checked = true;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            
            //button1.Enabled = false;
            //Acoes();

            int us = ca.Usuario().Rows.Count;
            for (int i = 0; i < us; i++)
            {
                comboBox1.Items.Add(ca.Usuario().Rows[i]["nome"].ToString());
            }

            int req = ca.Requisicao().Rows.Count;
            for (int i = 0; i < req; i++)
            {
                comboBox2.Items.Add(ca.Requisicao().Rows[i]["requisicao"].ToString());
            }
            tFormAguarde.Abort();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            dateTimePicker1.Enabled = true;
            comboBox1.Text = "";
            comboBox2.Text = "";
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            comboBox2.Enabled = false;
            dateTimePicker1.Enabled = false;
            comboBox2.Text = "";
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = true;
            dateTimePicker1.Enabled = false;
            comboBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Acoes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

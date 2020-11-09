using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Drawing.Printing;
using System.IO;


namespace Copias
{
    public partial class frmRelatorioOc : Form
    {
        ConectaOc co = new ConectaOc();
        Ocorrencia oc = new Ocorrencia();
        ConectaCurso cs = new ConectaCurso();
        ConectaComp cp = new ConectaComp();
        Componente cm = new Componente();
        Report report;
        int medio = 0, integrado = 0, desenv = 0, quimica = 0, recursos = 0, disc = 0;
        string[] comp;
        string aux;
        public frmRelatorioOc()
        {
            InitializeComponent();
        }


        private void frmRelatorioOc_Load(object sender, EventArgs e)
        {
            comp = new string[13];
            int cont = cs.Curso().Rows.Count;

            for (int i = 0; i < cont; i++)
            {
                string curso = cs.Curso().Rows[i]["nome"].ToString();
                if (curso.Contains("Médio") && !curso.Contains("integrado"))
                {
                    comp[i] = curso.Replace("A ", "").Replace("B ", "").Replace("C ", "").Replace("- ", "");
                    disc++;
                }
                if (curso.Contains("integrado"))
                {
                    comp[i] = curso;
                    disc++;
                }
                if (curso.Contains("NovoTec"))
                {
                    comp[i] = curso;
                    disc++;
                }
            }

            comboBox1.Items.Clear();
            for (int j = 0; j < disc; j++)
            {
                for (int m = 0; m < j; m++)
                {
                    if (comp[j] == comp[m])
                    {
                        aux = "ok";
                    }
                }
                if (aux != "ok")
                {
                    comboBox1.Items.Add(comp[j].ToUpper().TrimEnd());
                }
                aux = "";
            }

            int soma = co.carregaGridOcNovas().Rows.Count;
            for (int i = 0; i < soma; i++)
            {
                string curso = co.carregaGridOcNovas().Rows[i]["serie"].ToString();
                if (curso.Contains("Médio") && !curso.Contains("integrado"))
                {
                    medio++;
                }
                if (curso.Contains("integrado"))
                {
                    integrado++;
                }
                if (curso.Contains("NovoTec - Desenvolvimento"))
                {
                    desenv++;
                }
                if (curso.Contains("NovoTec - Química"))
                {
                    quimica++;
                }
                if (curso.Contains("NovoTec - Recursos"))
                {
                    recursos++;
                }
            }
            label2.Text = Convert.ToString(medio);
            label3.Text = Convert.ToString(integrado);
            label5.Text = Convert.ToString(desenv);
            label7.Text = Convert.ToString(quimica);
            label9.Text = Convert.ToString(recursos);
            this.reportViewer1.RefreshReport();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.Text == "TÉC. EM ADMIN. INTEGRADO AO ENSINO MÉDIO")
            {
                reportViewer1.Visible = false;
                reportViewer2.Visible = true;
                reportViewer3.Visible = false;
                reportViewer4.Visible = false;
                reportViewer5.Visible = false;
                foreach (DataRow item in co.carregaGridOcNovas().Rows)
                {
                    this.ocorrenciaTableAdapter.FillBy(this.copiasDataSet1.ocorrencia, "%" + comboBox1.Text);
                    reportViewer2.RefreshReport();
                }
            }
            else if (comboBox1.Text == "ENSINO MÉDIO")
            {
                reportViewer1.Visible = true;
                reportViewer2.Visible = false;
                reportViewer3.Visible = false;
                reportViewer4.Visible = false;
                reportViewer5.Visible = false;
                foreach (DataRow item in co.carregaGridOcNovas().Rows)
                {
                    this.ocorrenciaTableAdapter.FillBy1(this.copiasDataSet1.ocorrencia, "%ENSINO MÉDIO");
                    reportViewer1.RefreshReport();
                }
            }
            else if (comboBox1.Text == "NOVOTEC - DESENVOLVIMENTO DE SISTEMAS")
            {
                reportViewer1.Visible = false;
                reportViewer2.Visible = false;
                reportViewer3.Visible = true;
                reportViewer4.Visible = false;
                reportViewer5.Visible = false;
                foreach (DataRow item in co.carregaGridOcNovas().Rows)
                {
                    this.ocorrenciaTableAdapter.FillBy(this.copiasDataSet1.ocorrencia, "%" + comboBox1.Text);
                    reportViewer3.RefreshReport();
                }
            }
            else if (comboBox1.Text == "NOVOTEC - QUÍMICA")
            {
                reportViewer1.Visible = false;
                reportViewer2.Visible = false;
                reportViewer3.Visible = false;
                reportViewer4.Visible = true;
                reportViewer5.Visible = false;
                foreach (DataRow item in co.carregaGridOcNovas().Rows)
                {
                    this.ocorrenciaTableAdapter.FillBy(this.copiasDataSet1.ocorrencia, "%" + comboBox1.Text);
                    reportViewer4.RefreshReport();
                }
            }
            else if (comboBox1.Text == "NOVOTEC - RECURSOS HUMANOS")
            {
                reportViewer1.Visible = false;
                reportViewer2.Visible = false;
                reportViewer3.Visible = false;
                reportViewer4.Visible = false;
                reportViewer5.Visible = true;
                foreach (DataRow item in co.carregaGridOcNovas().Rows)
                {
                    this.ocorrenciaTableAdapter.FillBy(this.copiasDataSet1.ocorrencia, "%" + comboBox1.Text);
                    reportViewer5.RefreshReport();
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.Text == "ENSINO MÉDIO")
            {
                report = reportViewer1.LocalReport;
            }
            else if (comboBox1.Text == "TÉC. EM ADMIN. INTEGRADO AO ENSINO MÉDIO")
            {
                report = reportViewer2.LocalReport;
            }
            else if (comboBox1.Text == "NOVOTEC - DESENVOLVIMENTO DE SISTEMAS")
            {
                report = reportViewer3.LocalReport;
            }
            else if (comboBox1.Text == "NOVOTEC - QUÍMICA")
            {
                report = reportViewer4.LocalReport;
            }
            else if (comboBox1.Text == "NOVOTEC - RECURSOS HUMANOS")
            {
                report = reportViewer5.LocalReport;
            }

            try
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;

                byte[] bytes = this.report.Render(
                    "PDF", null, out mimeType, out encoding, out filenameExtension,
                    out streamids, out warnings);

                using (FileStream fs = new FileStream(comboBox1.Text + ".pdf", FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }

                System.Diagnostics.Process.Start(comboBox1.Text + ".pdf");
            }
            catch
            {
                string msg = "FECHE O ARQUIVO PDF!!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
        }
    }
}

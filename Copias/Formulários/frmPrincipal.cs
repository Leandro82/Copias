using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Deployment.Application;
using Copias.Formulários;

namespace Copias
{
    public partial class frmPrincipal : Form
    {
        string nome, cod, func1, func2;
        int contReq = 0, contImp = 0;
        ConectaUs co = new ConectaUs();
        Usuario us = new Usuario();
        Requisicao re = new Requisicao();
        ConectaReq cq = new ConectaReq();
        ConectaAc ca = new ConectaAc();
        Acoes ac = new Acoes();
        static System.Windows.Forms.Timer tp = new System.Windows.Forms.Timer();
        public frmPrincipal(string nm, string cd)
        {
            InitializeComponent();
            nome = nm;
            cod = cd;
            AuxClas.coordenador = nm;
        }

        private void usuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmCad();
            if (Application.OpenForms.OfType<frmCad>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void cursosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmCursos();
            if (Application.OpenForms.OfType<frmCursos>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void cópiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmCopias(nome);
            if (Application.OpenForms.OfType<frmCopias>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void aaas_Tick(object sender, EventArgs e)
        {
            contReq = cq.PrincipalReq().Rows.Count;
            contImp = cq.PrincipalImp().Rows.Count;
            if (contReq > 0)
            {
                requisiçãoToolStripMenuItem.ForeColor = Color.Red;
                requisiçãoToolStripMenuItem.Text = "Requisição " + "(" + contReq + ")";
            }
            else
            {
                requisiçãoToolStripMenuItem.ForeColor = Color.Black;
                requisiçãoToolStripMenuItem.Text = "Requisição";
            }

            if (contImp > 0)
            {
                imprimirToolStripMenuItem.ForeColor = Color.Red;
                imprimirToolStripMenuItem.Text = "Imprimir " + "(" + contImp + ")";
            }
            else
            {
                imprimirToolStripMenuItem.ForeColor = Color.Black;
                imprimirToolStripMenuItem.Text = "Impressão";
            }
        }

        public void Principal()
        {
            tp.Interval = 2000;
            tp.Tick += new EventHandler(aaas_Tick);
            tp.Start();
        }


        public void frmPrincipal_Load(object sender, EventArgs e)
        {
            Version appVersion;

            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                appVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            else
            {
                appVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            }

            if (cod == "")
            {
            }
            else
            {
                AuxClas.LibData = Convert.ToInt32(cod);
            }

            contReq = cq.PrincipalReq().Rows.Count;
            contImp = cq.PrincipalImp().Rows.Count;
            if (contReq > 0)
            {
                requisiçãoToolStripMenuItem.ForeColor = Color.Red;
                requisiçãoToolStripMenuItem.Text = "Requisição " + "(" + contReq + ")";
            }
            else
            {
                requisiçãoToolStripMenuItem.ForeColor = Color.Black;
                requisiçãoToolStripMenuItem.Text = "Requisição";
            }

            if (contImp > 0)
            {
                imprimirToolStripMenuItem.ForeColor = Color.Red;
                imprimirToolStripMenuItem.Text = "Imprimir " + "(" + contImp + ")";
            }
            else
            {
                imprimirToolStripMenuItem.ForeColor = Color.Black;
                imprimirToolStripMenuItem.Text = "Impressão";
            }

            Principal();
            //---------------------------------------------------
            
            if (cod == "")
            {
                Application.Exit();
            }
            else
            {
                us.Codigo = Convert.ToInt32(cod);
                toolStripLabel2.Text = nome;
                toolStripLabel4.Text = Convert.ToString(appVersion);

                foreach (DataRow item in co.Menu(us).Rows)
                {
                    func1 = item["func1"].ToString();
                    func2 = item["func2"].ToString();
                }

                if (func1 == "Secretaria")
                { 
                    cadastrosToolStripMenuItem.Visible = true;
                    usuárioToolStripMenuItem.Visible = true;
                    cursosToolStripMenuItem.Visible = true;
                    cópiasToolStripMenuItem.Visible = true;
                    imprimirToolStripMenuItem.Visible = true;
                    relatórioToolStripMenuItem.Visible = true;
                    gerarOcorrênciasToolStripMenuItem.Visible = true;
                    relatórioDeOcorrênciasToolStripMenuItem.Visible = true;
                    toolStripMenuItem1.Visible = false;
                    verificaPedidoToolStripMenuItem.Visible = false;
                    requisiçãoToolStripMenuItem.Visible = false;
                    açõesNoSistemaToolStripMenuItem.Visible = true;
                    progressõesToolStripMenuItem.Visible = true;
                    cadastrarToolStripMenuItem.Visible = true;
                    listaDeProgressõesToolStripMenuItem.Visible = true;
                    alunosToolStripMenuItem.Visible = true;
                    comonentesToolStripMenuItem.Visible = true;
                    cópiasPorProfessorToolStripMenuItem.Visible = true;
                    agendaToolStripMenuItem.Visible = true;
                    horáriosToolStripMenuItem.Visible = true;
                    locaisItensToolStripMenuItem.Visible = true;
                    agendaToolStripMenuItem1.Visible = true;
                }
                else if(func1 == "Professor" && func2 == "Coordenador")
                {
                    cadastrosToolStripMenuItem.Visible = true;
                    usuárioToolStripMenuItem.Visible = false;
                    cursosToolStripMenuItem.Visible = false;
                    cópiasToolStripMenuItem.Visible = true;
                    imprimirToolStripMenuItem.Visible = false;
                    relatórioToolStripMenuItem.Visible = false;
                    toolStripMenuItem1.Visible = false;
                    verificaPedidoToolStripMenuItem.Visible = true;
                    requisiçãoToolStripMenuItem.Visible = true;
                    gerarOcorrênciasToolStripMenuItem.Visible = true;
                    relatórioDeOcorrênciasToolStripMenuItem.Visible = true;
                    açõesNoSistemaToolStripMenuItem.Visible = false;
                    progressõesToolStripMenuItem.Visible = false;
                    cadastrarToolStripMenuItem.Visible = false;
                    listaDeProgressõesToolStripMenuItem.Visible = false;
                    alunosToolStripMenuItem.Visible = false;
                    comonentesToolStripMenuItem.Visible = false;
                    cópiasPorProfessorToolStripMenuItem.Visible = true;
                    horáriosToolStripMenuItem.Visible = false;
                    locaisItensToolStripMenuItem.Visible = false;
                }
                else if(func1 == "Direção")
                {
                    cadastrosToolStripMenuItem.Visible = true;
                    usuárioToolStripMenuItem.Visible = true;
                    cursosToolStripMenuItem.Visible = true;
                    cópiasToolStripMenuItem.Visible = true;
                    imprimirToolStripMenuItem.Visible = true;
                    relatórioToolStripMenuItem.Visible = true;
                    toolStripMenuItem1.Visible = false;
                    verificaPedidoToolStripMenuItem.Visible = false;
                    requisiçãoToolStripMenuItem.Visible = true;
                    gerarOcorrênciasToolStripMenuItem.Visible = true;
                    relatórioDeOcorrênciasToolStripMenuItem.Visible = true;
                    açõesNoSistemaToolStripMenuItem.Visible = true;
                    progressõesToolStripMenuItem.Visible = true;
                    cadastrarToolStripMenuItem.Visible = false;
                    listaDeProgressõesToolStripMenuItem.Visible = true;
                    alunosToolStripMenuItem.Visible = true;
                    comonentesToolStripMenuItem.Visible = true;
                    cópiasPorProfessorToolStripMenuItem.Visible = true;
                    agendaToolStripMenuItem.Visible = true;
                    horáriosToolStripMenuItem.Visible = true;
                    locaisItensToolStripMenuItem.Visible = true;
                    agendaToolStripMenuItem1.Visible = true;
                }
                else if (func1 == "Professor" && func2 == "")
                {
                    cadastrosToolStripMenuItem.Visible = true;
                    usuárioToolStripMenuItem.Visible = false;
                    cursosToolStripMenuItem.Visible = false;
                    cópiasToolStripMenuItem.Visible = false;
                    imprimirToolStripMenuItem.Visible = false;
                    relatórioToolStripMenuItem.Visible = false;
                    toolStripMenuItem1.Visible = true;
                    verificaPedidoToolStripMenuItem.Visible = true;
                    requisiçãoToolStripMenuItem.Visible = false;
                    gerarOcorrênciasToolStripMenuItem.Visible = true;
                    relatórioDeOcorrênciasToolStripMenuItem.Visible = false;
                    açõesNoSistemaToolStripMenuItem.Visible = false;
                    progressõesToolStripMenuItem.Visible = false;
                    progressõesToolStripMenuItem.Visible = false;
                    cadastrarToolStripMenuItem.Visible = false;
                    listaDeProgressõesToolStripMenuItem.Visible = false;
                    alunosToolStripMenuItem.Visible = false;
                    comonentesToolStripMenuItem.Visible = false;
                    cópiasPorProfessorToolStripMenuItem.Visible = false;
                    horáriosToolStripMenuItem.Visible = false;
                    locaisItensToolStripMenuItem.Visible = false;
                }
                else if (func1 == "Orientador Educacional")
                {
                    cadastrosToolStripMenuItem.Visible = true;
                    usuárioToolStripMenuItem.Visible = false;
                    cursosToolStripMenuItem.Visible = false;
                    cópiasToolStripMenuItem.Visible = true;
                    imprimirToolStripMenuItem.Visible = false;
                    relatórioToolStripMenuItem.Visible = false;
                    toolStripMenuItem1.Visible = false;
                    verificaPedidoToolStripMenuItem.Visible = true;
                    requisiçãoToolStripMenuItem.Visible = true;
                    gerarOcorrênciasToolStripMenuItem.Visible = true;
                    relatórioDeOcorrênciasToolStripMenuItem.Visible = false;
                    açõesNoSistemaToolStripMenuItem.Visible = false;
                    progressõesToolStripMenuItem.Visible = true;
                    cadastrarToolStripMenuItem.Visible = false;
                    listaDeProgressõesToolStripMenuItem.Visible = true;
                    alunosToolStripMenuItem.Visible = false;
                    comonentesToolStripMenuItem.Visible = false;
                    cópiasPorProfessorToolStripMenuItem.Visible = true;
                    horáriosToolStripMenuItem.Visible = false;
                    locaisItensToolStripMenuItem.Visible = false;
                }
                else if (func1 == "Coordenador Pedagógico")
                {
                    cadastrosToolStripMenuItem.Visible = true;
                    usuárioToolStripMenuItem.Visible = false;
                    cursosToolStripMenuItem.Visible = false;
                    cópiasToolStripMenuItem.Visible = true;
                    imprimirToolStripMenuItem.Visible = false;
                    relatórioToolStripMenuItem.Visible = false;
                    toolStripMenuItem1.Visible = false;
                    verificaPedidoToolStripMenuItem.Visible = true;
                    requisiçãoToolStripMenuItem.Visible = true;
                    gerarOcorrênciasToolStripMenuItem.Visible = true;
                    relatórioDeOcorrênciasToolStripMenuItem.Visible = false;
                    açõesNoSistemaToolStripMenuItem.Visible = false;
                    progressõesToolStripMenuItem.Visible = true;
                    cadastrarToolStripMenuItem.Visible = false;
                    listaDeProgressõesToolStripMenuItem.Visible = true;
                    alunosToolStripMenuItem.Visible = false;
                    comonentesToolStripMenuItem.Visible = false;
                    cópiasPorProfessorToolStripMenuItem.Visible = true;
                    horáriosToolStripMenuItem.Visible = false;
                    locaisItensToolStripMenuItem.Visible = false;
                }
                else if (func1 == "Diretoria de Serviços")
                {
                    cadastrosToolStripMenuItem.Visible = false;
                    usuárioToolStripMenuItem.Visible = false;
                    cursosToolStripMenuItem.Visible = false;
                    cópiasToolStripMenuItem.Visible = false;
                    imprimirToolStripMenuItem.Visible = true;
                    relatórioToolStripMenuItem.Visible = true;
                    gerarOcorrênciasToolStripMenuItem.Visible = false;
                    relatórioDeOcorrênciasToolStripMenuItem.Visible = false;
                    toolStripMenuItem1.Visible = false;
                    verificaPedidoToolStripMenuItem.Visible = false;
                    requisiçãoToolStripMenuItem.Visible = false;
                    açõesNoSistemaToolStripMenuItem.Visible = true;
                    progressõesToolStripMenuItem.Visible = false;
                    cadastrarToolStripMenuItem.Visible = false;
                    listaDeProgressõesToolStripMenuItem.Visible = false;
                    alunosToolStripMenuItem.Visible = false;
                    comonentesToolStripMenuItem.Visible = false;
                    ocorrênciasToolStripMenuItem.Visible = false;
                    cópiasPorProfessorToolStripMenuItem.Visible = true;
                    agendaToolStripMenuItem.Visible = true;
                    horáriosToolStripMenuItem.Visible = true;
                    locaisItensToolStripMenuItem.Visible = true;
                    agendaToolStripMenuItem1.Visible = true;
                }
            }
        }


        private void requisiçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cont = cq.CarregaGridReq().Rows.Count;
            if (cont == 0)
            {
                string msg = "Não existe requisição para liberar.";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                var peq = new frmReq(nome);
                if (Application.OpenForms.OfType<frmReq>().Count() > 0)
                {
                    Application.OpenForms[peq.Name].Focus();
                }
                else
                {
                    peq.Show();
                }
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            re.Professor = nome;
            int cont = cq.Impressao().Rows.Count;
            if (cont == 0)
            {
                string msg = "Não existe pedido(s) de cópia(s).";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                var peq = new frmImp(nome);
                if (Application.OpenForms.OfType<frmImp>().Count() > 0)
                {
                    Application.OpenForms[peq.Name].Focus();
                }
                else
                {
                    peq.Show();
                }
            }
        }

        private void verificaPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                var peq = new frmVerifica(nome);
                if (Application.OpenForms.OfType<frmVerifica>().Count() > 0)
                {
                    Application.OpenForms[peq.Name].Focus();
                }
                else
                {
                    peq.Show();
                }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var peq = new frmCopiaProf(nome);
            if (Application.OpenForms.OfType<frmCopiaProf>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void relatórioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var peq = new frmCalend();
            if (Application.OpenForms.OfType<frmCalend>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
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


        private void impressõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmRelatorio(nome);
            if (Application.OpenForms.OfType<frmRelatorio>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void açõesNoSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ca.Requisicao().Rows.Count == 0)
            {
                string msg = "NÃO EXISTE NENHUMA AÇÃO REGISTRADA.";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                var peq = new frmAcoes();
                if (Application.OpenForms.OfType<frmAcoes>().Count() > 0)
                {
                    Application.OpenForms[peq.Name].Focus();
                }
                else
                {
                    peq.Show();
                }
            }
        }

        private void gerarOcorrênciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmOcorrencias(nome);
            if (Application.OpenForms.OfType<frmOcorrencias>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void relatórioDeOcorrênciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmRelOc();
            if (Application.OpenForms.OfType<frmRelOc>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmProgressao();
            if (Application.OpenForms.OfType<frmProgressao>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void listaDeProgressõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmProgControle(nome, cod);
            if (Application.OpenForms.OfType<frmProgControle>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void cadastrarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var peq = new frmCadAluno();
            if (Application.OpenForms.OfType<frmCadAluno>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void baixarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmAltSitAluno();
            if (Application.OpenForms.OfType<frmAltSitAluno>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void comonentesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmComponente();
            if (Application.OpenForms.OfType<frmComponente>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void testeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmRelatorioOc();
            if (Application.OpenForms.OfType<frmRelatorioOc>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void cópiasPorProfessorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmRelCopiasProf();
            if (Application.OpenForms.OfType<frmRelCopiasProf>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void testeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var peq = new frmCarregando();
            if (Application.OpenForms.OfType<frmCarregando>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void agendaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var peq = new frmAgenda(nome);
            if (Application.OpenForms.OfType<frmAgenda>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void horáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmHorario();
            if (Application.OpenForms.OfType<frmHorario>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void locaisItensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmCadItensAgenda();
            if (Application.OpenForms.OfType<frmCadItensAgenda>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }
    }
}

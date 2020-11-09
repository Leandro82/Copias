using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing.Imaging;
using System.Data.OleDb;
using System.Timers;
using System.Reflection;
using System.Web.Services;

namespace Copias
{
    class ConectaReq
    {
        public MySqlConnection conexao;
        //string caminho = "Persist Security Info=false;SERVER=10.66.122.42;DATABASE=copias;UID=secac;pwd=secac";
        string nm, directorio;
        static System.Windows.Forms.Timer tp = new System.Windows.Forms.Timer();
        byte[] buffer = null;

        public static void aaas_Tick(object sender, EventArgs e)
        {
            tp.Stop();
        }

        public static void Tempo()
        {
            tp.Interval = 2000;
            tp.Tick += new EventHandler(aaas_Tick);
            tp.Start();
        }

        public string Endereco()
        {
            StringConexao str = new StringConexao();
            return str.Endereco();
        }

        //Faz a contagem que é apresentada no menu requisição na tela principal
        public DataTable PrincipalReq()
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, data, prof, quant, curso, frente, autCoord,dtEntrega, autoriza FROM requisicao WHERE autCoord!='Sim' order by data";
                MySqlDataAdapter comandos = new MySqlDataAdapter(selecionar, conexao);
                DataTable dt = new System.Data.DataTable();
                comandos.Fill(dt);
                conexao.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        //Faz a contagem que é apresentada no menu Impressão na tela principal
        public DataTable PrincipalImp()
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, data, prof, quant, curso, frente, autCoord,dtEntrega, autoriza FROM requisicao WHERE autCoord='Sim' AND ocultar='Não'order by data";
                MySqlDataAdapter comandos = new MySqlDataAdapter(selecionar, conexao);
                DataTable dt = new System.Data.DataTable();
                comandos.Fill(dt);
                conexao.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        //--------------------------------------
        //Cadastra requisição sem anexar arquivo
        public void cadastro(Requisicao re)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string inserir = "INSERT INTO requisicao(nome,nmArquivo, data, hora, prof, quant, total, curso, frente, dtEntrega, autCoord, ocultar,destino)VALUES('" + re.Nome + "','" + re.Nome + "','" + Convert.ToDateTime(re.Data).ToString("yyyy-MM-dd") + "','" + re.Hora + "','" + re.Professor + "','" + re.QtCopias + "','" + re.Paginas + "','" + re.Curso + "','" + re.FrenteVerso + "','" + Convert.ToDateTime(re.Entrega).ToString("yyyy-MM-dd") + "','" + re.Autoriza + "','" + re.Ocultar + "','" + re.Destino + "')";
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }


        //Cadastra requisição com anexo
        public void cadastroArquivo(Requisicao re)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string inserir = "INSERT INTO requisicao(nome, data, hora, prof, quant, total, curso, frente, dtEntrega, nmArquivo, arquivo, ocultar, autCoord, destino) VALUES(@nome, @data, @hora, @prof, @quant, @total, @curso, @frente, @dtEntrega, @nmArquivo, @arquivo, @ocultar, @autCoord, @destino)";
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                MySqlParameter prt = new MySqlParameter();
                MySqlParameter prt1 = new MySqlParameter();

                StreamReader str = new StreamReader(re.Caminho);
                byte[] buffer = new byte[str.BaseStream.Length];
                str.BaseStream.Read(buffer, 0, buffer.Length);

                str.Close();
                str.Dispose();

                prt.ParameterName = "@arquivo";
                prt.MySqlDbType = MySqlDbType.MediumBlob;
                prt.SourceColumn = "arquivo";
                prt.Value = buffer;

                comandos.Parameters.Add(prt);
                comandos.Parameters.AddWithValue("@nome", re.Nome);
                comandos.Parameters.AddWithValue("@data", Convert.ToString(Convert.ToDateTime(BuscaHoraServidor()).ToString("yyyy-MM-dd")).ToString());
                comandos.Parameters.AddWithValue("@hora", String.Format("{0:t}", Convert.ToDateTime(BuscaHoraServidor())));
                comandos.Parameters.AddWithValue("@prof", re.Professor);
                comandos.Parameters.AddWithValue("@quant", Convert.ToInt32(re.QtCopias));
                comandos.Parameters.AddWithValue("@total", Convert.ToInt32(re.Paginas));
                comandos.Parameters.AddWithValue("@curso", re.Curso);
                comandos.Parameters.AddWithValue("@frente", re.FrenteVerso);
                comandos.Parameters.AddWithValue("@dtEntrega", Convert.ToDateTime(re.Entrega));
                comandos.Parameters.AddWithValue("@nmArquivo", re.Arquivo);
                comandos.Parameters.AddWithValue("@ocultar", re.Ocultar);
                comandos.Parameters.AddWithValue("@autCoord", re.Autoriza);
                comandos.Parameters.AddWithValue("@destino", re.Destino);

                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }


        //Altera requisição sem anexo
        public void atualizar(Requisicao re)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE requisicao SET nome='" + re.Nome + "' ,nmArquivo= '" + re.Nome + "', dtEntrega = '" + Convert.ToDateTime(re.Entrega).ToString("yyyy-MM-dd") + "', prof = '" + re.Professor + "', quant = '" + re.QtCopias + "',total = '" + re.Paginas + "', curso = '" + re.Curso + "', frente = '" + re.FrenteVerso + "'WHERE cod = '" + re.Codigo + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }


        //Altera requisição com anexo
        public void alteraArquivo(Requisicao re)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string inserir = "UPDATE requisicao SET nome=@nome, data=@data, prof=@prof, quant=@quant, total=@total, curso=@curso, frente=@frente, dtEntrega=@dtEntrega, nmArquivo=@nmArquivo, arquivo=@arquivo WHERE cod='" + re.Codigo + "'";
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                MySqlParameter prt = new MySqlParameter();
                MySqlParameter prt1 = new MySqlParameter();

                StreamReader str = new StreamReader(re.Caminho);
                byte[] buffer = new byte[str.BaseStream.Length];
                str.BaseStream.Read(buffer, 0, buffer.Length);

                str.Close();
                str.Dispose();

                prt.ParameterName = "@arquivo";
                prt.MySqlDbType = MySqlDbType.MediumBlob;
                prt.SourceColumn = "arquivo";
                prt.Value = buffer;

                comandos.Parameters.Add(prt);
                comandos.Parameters.AddWithValue("@nome", re.Nome);
                comandos.Parameters.AddWithValue("@data", DateTime.Today);
                comandos.Parameters.AddWithValue("@prof", re.Professor);
                comandos.Parameters.AddWithValue("@quant", Convert.ToInt32(re.QtCopias));
                comandos.Parameters.AddWithValue("@total", Convert.ToInt32(re.Paginas));
                comandos.Parameters.AddWithValue("@curso", re.Curso);
                comandos.Parameters.AddWithValue("@frente", re.FrenteVerso);
                comandos.Parameters.AddWithValue("@dtEntrega", Convert.ToDateTime(re.Entrega));
                comandos.Parameters.AddWithValue("@nmArquivo", re.Arquivo);
                comandos.Parameters.AddWithValue("@ocultar", re.Ocultar);
                comandos.Parameters.AddWithValue("@autCoord", re.Autoriza);
                comandos.Parameters.AddWithValue("@destino", re.Destino);

                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void excluir(Requisicao re)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "DELETE FROM requisicao WHERE cod = '" + re.Codigo + "'";
                MySqlCommand comando = new MySqlCommand(alterar, conexao);
                comando.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }


        public void liberaReq(Requisicao re)
        {

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE requisicao SET coord='" + re.Coordenador + "' , autCoord = '" + re.Autoriza + "', dataImp = '" + Convert.ToDateTime(re.DataImpressao).ToString("yyyy-MM-dd") + "', destino= '" + re.Destino + "', autoriza='" + re.NaoAutoriza + "' WHERE cod = '" + re.Codigo + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void FinalizaReq(Requisicao re)
        {

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE requisicao SET respImp='" + re.RespImpressao + "' , ocultar = '" + re.Ocultar + "', dataImp = '" + Convert.ToDateTime(re.DataImpressao).ToString("yyyy-MM-dd") + "', destino = '" + re.Destino + "'WHERE cod = '" + re.Codigo + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void naoAutorizar(Requisicao re)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE requisicao SET coord= '" + re.Coordenador + "', autoriza='" + re.NaoAutoriza + "', destino='" + re.Destino + "', autCoord='" + re.Autoriza + "'WHERE cod = '" + re.Codigo + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        //Seleciona dados da requisição com referência (professor)
        public DataTable porProfessor(Requisicao re)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, prof, nmArquivo, dtEntrega, quant, total, curso, frente FROM requisicao WHERE prof= '" + re.Professor + "'AND ocultar='Não'order by data";
                MySqlDataAdapter comandos = new MySqlDataAdapter(selecionar, conexao);
                DataTable dt = new System.Data.DataTable();
                comandos.Fill(dt);
                conexao.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        //Seleciona dados da requisição no Grid de impressão
        public DataTable Geral(Requisicao re)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, prof, nmArquivo, dtEntrega, quant, total, curso, frente FROM requisicao WHERE ocultar='Não'order by data";
                MySqlDataAdapter comandos = new MySqlDataAdapter(selecionar, conexao);
                DataTable dt = new System.Data.DataTable();
                comandos.Fill(dt);
                conexao.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        //Carrega o Grid na tela de Impressão
        public DataTable Impressao()
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, prof, nmArquivo, dtEntrega, quant, total, curso, frente, coord FROM requisicao WHERE ocultar='Não'AND autCoord='Sim'order by data";
                MySqlDataAdapter comandos = new MySqlDataAdapter(selecionar, conexao);
                DataTable dt = new System.Data.DataTable();
                comandos.Fill(dt);
                conexao.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void SalvaArquivo(Requisicao re)
        {
            try
            {
                conexao.Open();
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = conexao;
                comando.CommandText = "SELECT nmArquivo, arquivo FROM requisicao WHERE cod = '" + re.Codigo + "'";
                comando.CommandType = CommandType.Text;
                MySqlDataReader dataReader = comando.ExecuteReader();
                

                if (dataReader.Read())
                {
                    buffer = (byte[])dataReader.GetValue(1);
                    nm = dataReader.GetString(0);
                }

                conexao.Close();

                int cont = buffer.Count();

                if (cont == 0)
                {
                    string msg = "Não existe anexo para essa requisição";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    new System.Threading.Thread(delegate()
                    {
                        CarregaArquivo();
                    }).Start();
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void CarregaArquivo()
        {
            System.Threading.Thread arquivo1 = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
           {
               
               using (FolderBrowserDialog dirDialog = new FolderBrowserDialog())
               {
                   
                       DialogResult res = dirDialog.ShowDialog();
                       if (res == DialogResult.OK)
                       {
                           directorio = dirDialog.SelectedPath;
                           StreamWriter str = new StreamWriter(directorio + '\\' + nm);
                           str.BaseStream.Write(buffer, 0, buffer.Length);
                           str.Close();
                           str.Dispose();
                       }
                       else if(res == DialogResult.Cancel)
                       {
                           //MessageBox.Show("Foi");
                       }
                       
               }
           }));
            arquivo1.SetApartmentState(System.Threading.ApartmentState.STA);
            arquivo1.IsBackground = false;
            arquivo1.Start();
        }


        
        public DataTable CarregaRelatorio(Requisicao re)
        {
            DataTable dt = new System.Data.DataTable();
            try
            {
                if (re.Arquivo == "Geral")
                {
                    conexao = new MySqlConnection(Endereco());
                    conexao.Open();
                    string selecionar = "SELECT cod, prof, nmArquivo, total, coord, dataImp, arquivo FROM requisicao WHERE ocultar='Sim'order by prof AND cod";
                    MySqlDataAdapter comandos = new MySqlDataAdapter(selecionar, conexao);
                    comandos.Fill(dt);
                    conexao.Close();
                }
                else if(re.Arquivo == "Prof")
                {
                    conexao = new MySqlConnection(Endereco());
                    conexao.Open();
                    string selecionar = "SELECT cod, prof, nmArquivo, total, coord, dataImp, arquivo FROM requisicao WHERE ocultar='Sim'AND prof= '" + re.Professor + "'order by cod";
                    MySqlDataAdapter comandos = new MySqlDataAdapter(selecionar, conexao);
                    comandos.Fill(dt);
                    conexao.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        //Carrega o Grid na tela de Requisição
        public DataTable CarregaGridReq()
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, data,nmArquivo, prof, quant, curso, frente, autCoord,dtEntrega, autoriza FROM requisicao WHERE ocultar='Não'order by data";
                MySqlDataAdapter comandos = new MySqlDataAdapter(selecionar, conexao);
                DataTable dt = new System.Data.DataTable();
                comandos.Fill(dt);
                conexao.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        //Carrega o Grid na tela de Verificação do professor
        public DataTable CarregaGridDestino(Requisicao re)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, prof, coord, nmArquivo, curso, data, destino, autoriza, respImp FROM requisicao WHERE prof= '" + re.Professor + "'order by data";
                MySqlDataAdapter comandos = new MySqlDataAdapter(selecionar, conexao);
                DataTable dt = new System.Data.DataTable();
                comandos.Fill(dt);
                conexao.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public DataTable CarregaGridCopiasProf()
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, nome FROM usuario WHERE func1='Professor'order by nome";
                MySqlDataAdapter comandos = new MySqlDataAdapter(selecionar, conexao);
                DataTable dt = new System.Data.DataTable();
                comandos.Fill(dt);
                conexao.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public String ContaCopias(Requisicao re)
        {
            string quant;
            using (MySqlConnection cn = new MySqlConnection())
            {
                cn.ConnectionString = Endereco();
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter("SELECT SUM(total) from requisicao WHERE prof = '"+ re.Professor +"'", cn);
                    cn.Open();
                    DataTable dt = new System.Data.DataTable();
                    sda.Fill(dt);
                    int cont = dt.Rows.Count;
                    if (cont > 0)
                    {
                        quant = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        quant = "";
                    }
                }
                catch (MySqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    conexao.Close();
                }
                return quant;
            }
        }

        public String BuscaHoraServidor()
        {
            string data;
            using (MySqlConnection cn = new MySqlConnection())
            {
                cn.ConnectionString = Endereco();
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter("SELECT CURTIME()", cn);
                    cn.Open();
                    DataTable dt = new System.Data.DataTable();
                    sda.Fill(dt);
                    int cont = dt.Rows.Count;
                    if (cont > 0)
                    {
                        data = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        data = "";
                    }
                }
                catch (MySqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cn.Close();
                }
                return data;
            }
        }

        public String BuscaDiaServidor()
        {
            string dia;
            using (MySqlConnection cn = new MySqlConnection())
            {
                cn.ConnectionString = Endereco();
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter("SELECT DAYOFWEEK('"+Convert.ToDateTime(BuscaDataServidor()).ToString("yyyy-MM-dd")+"')", cn);
                    cn.Open();
                    DataTable dt = new System.Data.DataTable();
                    sda.Fill(dt);
                    int cont = dt.Rows.Count;
                    if (cont > 0)
                    {
                        dia = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        dia = "";
                    }
                }
                catch (MySqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cn.Close();
                }
                return dia;
            }
        }

        public String BuscaDataServidor()
        {
            string data;
            using (MySqlConnection cn = new MySqlConnection())
            {
                cn.ConnectionString = Endereco();
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter("SELECT NOW()", cn);
                    cn.Open();
                    DataTable dt = new System.Data.DataTable();
                    sda.Fill(dt);
                    int cont = dt.Rows.Count;
                    if (cont > 0)
                    {
                        data = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        data = "";
                    }
                }
                catch (MySqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cn.Close();
                }
                return data;
            }
        }

        public void libData(Requisicao re)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE libdata SET libData='" + re.LibData + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public String BuscaLibData()
        {
            string data;
            using (MySqlConnection cn = new MySqlConnection())
            {
                cn.ConnectionString = Endereco();
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter("SELECT libData from libdata", cn);
                    cn.Open();
                    DataTable dt = new System.Data.DataTable();
                    sda.Fill(dt);
                    int cont = dt.Rows.Count;
                    if (cont > 0)
                    {
                        data = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        data = "";
                    }
                }
                catch (MySqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cn.Close();
                }
                return data;
            }
        }

        public String BuscaUltCod()
        {
            string cod;
            using (MySqlConnection cn = new MySqlConnection())
            {
                cn.ConnectionString = Endereco();
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter("SELECT cod from requisicao ORDER BY cod DESC", cn);
                    cn.Open();
                    DataTable dt = new System.Data.DataTable();
                    sda.Fill(dt);
                    int cont = dt.Rows.Count;
                    if (cont > 0)
                    {
                        cod = dt.Rows[0]["cod"].ToString();
                    }
                    else
                    {
                        cod = "";
                    }
                }
                catch (MySqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cn.Close();
                }
                return cod;
            }
        }
    }
}

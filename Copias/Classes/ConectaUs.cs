using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.OleDb;

namespace Copias
{
    class ConectaUs
    {
        public MySqlConnection conexao;
        string acesso = "";
        //string caminho = "Persist Security Info=false;SERVER=10.66.122.42;DATABASE=copias;UID=secac;pwd=secac";

        public string Endereco()
        {
            StringConexao str = new StringConexao();
            return str.Endereco();
        }

        public void cadastro(Usuario us)
        {
            us.Acesso = acesso;
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string inserir = "INSERT INTO usuario(nome, email, login, senha, func1, func2, acesso)VALUES('" + us.Nome + "','" + us.Email + "','" + us.Login + "','" + us.Senha + "','"+ us.Profissao1 +"','"+ us.Profissao2 +"','" + us.Acesso + "')";
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void atualizarTudo(Usuario us)
        {
            us.Acesso = acesso;

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE usuario SET senha='" + us.Senha + "' , email='" + us.Email + "', login = '" + us.Login + "', func1 = '" + us.Profissao1 + "', func2 = '" + us.Profissao2 + "',acesso = '" + us.Acesso + "'WHERE nome = '" + us.Nome + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void atualizar(Usuario us)
        {
            us.Acesso = acesso;

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE usuario SET email= '" + us.Email + "', func1 = '" + us.Profissao1 + "', func2 = '" + us.Profissao2 + "'WHERE nome = '" + us.Nome + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void excluir(Usuario us)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "DELETE FROM usuario WHERE nome = '" + us.Nome + "'";
                MySqlCommand comando = new MySqlCommand(alterar, conexao);
                comando.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void alteraCad(Usuario us)
        {
            us.Acesso = "Sim";

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE usuario SET login = '" + us.Login + "', senha = '" + us.Senha + "', acesso = '"+ us.Acesso +"' WHERE nome = '" + us.Nome + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void Acesso(Usuario us)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string inserir = "SELECT fuc1, func2 FROM usuario WHERE cod='" + us.Codigo +"'";
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        // Carrega a função dataGrid na tela de cadastro de usuário
        public DataTable Grid()
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string carrega = "SELECT nome, email, login, func1, func2 FROM usuario ORDER BY nome";
                MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
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

        //Faz a verificação no cadastro do usuário, se o cadastro já existe ou não
        public DataTable GridView(Usuario us)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string carrega = "SELECT nome FROM usuario WHERE nome = '" + us.Nome + "'";
                MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
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

        //Verifica a função do usuário para acesso de menus
        public DataTable Menu(Usuario us)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string carrega = "SELECT func1, func2 FROM usuario WHERE cod='" + us.Codigo + "' or nome='" + us.Nome + "'";
                MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
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

        //Carrega o combo2 (Professores) na tela USUÁRIOS
        public DataTable Professor()
        {
            conexao = new MySqlConnection(Endereco());
            conexao.Open();
            string vSQL = "Select nome FROM usuario WHERE func1= 'Professor'ORDER BY nome";
            MySqlDataAdapter vDataAdapter = new MySqlDataAdapter(vSQL, conexao);
            DataTable vTable = new DataTable();
            vDataAdapter.Fill(vTable);
            conexao.Close();
            return vTable;
        }

        public DataTable Email(Usuario us)
        {
            conexao = new MySqlConnection(Endereco());
            conexao.Open();
            string vSQL = "SELECT email FROM usuario WHERE nome='" + us.Nome + "'";
            MySqlDataAdapter vDataAdapter = new MySqlDataAdapter(vSQL, conexao);
            DataTable vTable = new DataTable();
            vDataAdapter.Fill(vTable);
            conexao.Close();
            return vTable;
        }

        public Boolean VerificaLogin(Usuario us)
        {
            bool result = false;
            using (MySqlConnection cn = new MySqlConnection())
            {
                cn.ConnectionString = Endereco();
                try
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuario WHERE login= '" + us.Login + "' AND senha= '" + us.Senha + "'", cn);
                    cn.Open();
                    MySqlDataReader dados = cmd.ExecuteReader();
                    result = dados.HasRows;
                }
                catch (MySqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cn.Close();
                }
            }
            return result;
        }

        public String BuscaCodigo(Usuario us)
        {
            string cod;
            using (MySqlConnection cn = new MySqlConnection())
            {
                cn.ConnectionString = Endereco();
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter("SELECT cod, login FROM usuario WHERE login= '" + us.Login + "' AND senha='" + us.Senha + "'", cn);
                    cn.Open();
                    DataTable dt = new System.Data.DataTable();
                    sda.Fill(dt);
                    int cont = dt.Rows.Count;
                    if (cont > 0)
                    {
                        cod = Convert.ToString(dt.Rows[0]["cod"].GetHashCode());
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
            }
            return cod;
        }

        public String BuscaNome(Usuario us)
        {
            string nome;
            using (MySqlConnection cn = new MySqlConnection())
            {
                cn.ConnectionString = Endereco();
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter("SELECT nome, login FROM usuario WHERE login= '" + us.Login + "' AND senha='" + us.Senha + "'", cn);
                    cn.Open();
                    DataTable dt = new System.Data.DataTable();
                    sda.Fill(dt);
                    int cont = dt.Rows.Count;
                    if (cont > 0)
                    {
                        nome = dt.Rows[0]["nome"].ToString();
                    }
                    else
                    {
                        nome = "";
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
            }
            return nome;
        }

        public Boolean VerificaAcesso(Usuario us)
        {
            bool acesso = false;
            using (MySqlConnection cn = new MySqlConnection())
            {
                cn.ConnectionString = Endereco();
                try
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuario WHERE acesso= 'Sim'AND nome='" + BuscaNome(us) + "'", cn);
                    cn.Open();
                    MySqlDataReader dados = cmd.ExecuteReader();
                    acesso = dados.HasRows;
                }
                catch (MySqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cn.Close();
                }
            }
            return acesso;
        }

        // Carrega a função dataGrid na tela de cadastro de usuário
        public DataTable selecionaSenha(Usuario us)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string carrega = "SELECT nome, email, senha FROM usuario WHERE email='"+us.Email+"'";
                MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
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
    }
}

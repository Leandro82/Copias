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

namespace Copias
{
    class ConectaAc
    {
        public MySqlConnection conexao;
        //string caminho = "Persist Security Info=false;SERVER=10.66.122.42;DATABASE=copias;UID=secac;pwd=secac;Allow User Variables=True;Convert Zero Datetime=True;default command timeout=0";

        public string Endereco()
        {
            StringConexao str = new StringConexao();
            return str.Endereco();
        }

        public void cadastro(Acoes re)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string inserir = "INSERT INTO acoes(requisicao,nome,acao, data, hora)VALUES('" + re.Requisição + "','"+ re.Nome + "','" + re.Acao + "','" + Convert.ToDateTime(re.Data).ToString("yyyy-MM-dd") + "','" + re.Hora + "')";
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public DataTable BuscaAcoes(Acoes ac)
        {
            DataTable dt = new System.Data.DataTable();
            try
            {
                if (ac.Pesquisa == "Data")
                {
                    conexao = new MySqlConnection(Endereco());
                    conexao.Open();
                    string selecionar = "SELECT requisicao, nome, acao, data, hora FROM acoes WHERE data='"+Convert.ToDateTime(ac.Data).ToString("yyyy-MM-dd")+"'ORDER BY requisicao";
                    MySqlDataAdapter comandos = new MySqlDataAdapter(selecionar, conexao);
                    comandos.Fill(dt);
                    conexao.Close();
                }
                else if (ac.Pesquisa == "Usuario")
                {
                    conexao = new MySqlConnection(Endereco());
                    conexao.Open();
                    string selecionar = "SELECT requisicao, nome, acao, data, hora FROM acoes WHERE nome='" + ac.Nome + "'ORDER BY requisicao";
                    MySqlDataAdapter comandos = new MySqlDataAdapter(selecionar, conexao);
                    comandos.Fill(dt);
                    conexao.Close();
                }
                else if (ac.Pesquisa == "Requisicao")
                {
                    conexao = new MySqlConnection(Endereco());
                    conexao.Open();
                    string selecionar = "SELECT requisicao, nome, acao, data, hora FROM acoes WHERE requisicao='" + ac.Requisição + "'ORDER BY requisicao";
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

        public DataTable Usuario()
        {
            conexao = new MySqlConnection(Endereco());
            conexao.Open();
            string vSQL = "Select DISTINCT nome FROM acoes ORDER BY nome";
            MySqlDataAdapter vDataAdapter = new MySqlDataAdapter(vSQL, conexao);
            DataTable vTable = new DataTable();
            vDataAdapter.Fill(vTable);
            conexao.Close();
            return vTable;
        }

        public DataTable Requisicao()
        {
            conexao = new MySqlConnection(Endereco());
            conexao.Open();
            string vSQL = "Select DISTINCT requisicao FROM acoes ORDER BY requisicao";
            MySqlDataAdapter vDataAdapter = new MySqlDataAdapter(vSQL, conexao);
            DataTable vTable = new DataTable();
            vDataAdapter.Fill(vTable);
            conexao.Close();
            return vTable;
        }
    }
}

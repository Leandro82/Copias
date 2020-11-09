using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace Copias
{
    class ConectaAluno
    {
        public MySqlConnection conexao;
        //string caminho = "Persist Security Info=false;SERVER=10.66.122.42;DATABASE=copias;UID=secac;pwd=secac;Allow User Variables=True;Convert Zero Datetime=True;default command timeout=0";

        public string Endereco()
        {
            StringConexao str = new StringConexao();
            return str.Endereco();
        }

        public void cadastro(Alunos al)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string inserir = "INSERT INTO alunos(nome, turma)VALUES('" + al.Nome + "','" + al.Turma + "')";
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public DataTable BuscaAlunos(Alunos al)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, nome, turma, situacao FROM alunos WHERE turma='" + al.Turma + "' AND (situacao is null or situacao='') ORDER BY nome";
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

        public void BaixarAlunos(Alunos al)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE alunos SET situacao='" + al.Situacao + "'WHERE cod = '" + al.Codigo + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public DataTable Alunos(Alunos al)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, nome, turma, situacao FROM alunos WHERE turma='" + al.Turma + "'ORDER BY nome";
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
    }
}

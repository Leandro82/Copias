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
    class ConectaCurso
    {
        public MySqlConnection conexao;
        //string caminho = "Persist Security Info=false;SERVER=10.66.122.42;DATABASE=copias;UID=secac;pwd=secac";

        public string Endereco()
        {
            StringConexao str = new StringConexao();
            return str.Endereco();
        }

        public void cadastro(Curso us)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string inserir = "INSERT INTO curso(nome, modulo, situacao, coord, coord2, tipo)VALUES('" + us.Nome + "','" + us.Modulo + "','" + us.Situacao +"','" + us.Coordenador + "','" + us.Coordenador2 + "','" + us.Tipo + "')";
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void atualizar(Curso us)
        {

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE curso SET situacao ='" + us.Situacao + "', modulo = '" + us.Modulo + "', coord = '" + us.Coordenador +"', coord2 = '" + us.Coordenador2 + "', tipo='" + us.Tipo + "' WHERE nome = '" + us.Nome + "'AND modulo = '" + us.Modulo + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void excluir(Curso us)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "DELETE FROM curso WHERE cod = '" + us.Codigo + "'";
                MySqlCommand comando = new MySqlCommand(alterar, conexao);
                comando.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public DataTable Curso()
        {
            conexao = new MySqlConnection(Endereco());
            conexao.Open();
            string vSQL = "SELECT modulo, nome, tipo FROM curso WHERE situacao = 'Ativo' ORDER BY tipo, cod";
            MySqlDataAdapter vDataAdapter = new MySqlDataAdapter(vSQL, conexao);
            DataTable vTable = new DataTable();
            vDataAdapter.Fill(vTable);
            conexao.Close();
            return vTable;
        }

        //Verifica se o curso já está cadastrado
        public DataTable VerificaCadastro(Curso us)
        {
            conexao = new MySqlConnection(Endereco());
            conexao.Open();
            string vSQL = "SELECT nome FROM curso WHERE nome = '" + us.Nome + "'AND modulo = '" + us.Modulo + "'";
            MySqlDataAdapter vDataAdapter = new MySqlDataAdapter(vSQL, conexao);
            DataTable vTable = new DataTable();
            vDataAdapter.Fill(vTable);
            conexao.Close();
            return vTable;
        }

        //Verifica se o módulo alterado já está cadastrado
        public DataTable VerificaAlteracao(Curso us)
        {
            conexao = new MySqlConnection(Endereco());
            conexao.Open();
            string vSQL = "SELECT nome FROM curso WHERE nome = '" + us.Nome + "'AND modulo = '" + us.Modulo + "' AND situacao = '" + us.Situacao + "'";
            MySqlDataAdapter vDataAdapter = new MySqlDataAdapter(vSQL, conexao);
            DataTable vTable = new DataTable();
            vDataAdapter.Fill(vTable);
            conexao.Close();
            return vTable;
        }

        //Carrega o Grid na tela de cadastro de Cursos
        public DataTable CarregaGridCurso(Curso us)
        {
            DataTable vTable = new DataTable();
            if (us.Situacao == "Ativo" || us.Situacao == "Inativo")
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string vSQL = "SELECT cod, nome, modulo, situacao, coord, coord2, tipo FROM curso WHERE situacao = '" + us.Situacao + "'ORDER BY cod";
                MySqlDataAdapter vDataAdapter = new MySqlDataAdapter(vSQL, conexao);
                vDataAdapter.Fill(vTable);
                conexao.Close();
            }
            else if (us.Situacao == "Todos")
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string vSQL = "SELECT cod, nome, modulo, situacao, coord, coord2, tipo FROM curso ORDER BY cod";
                MySqlDataAdapter vDataAdapter = new MySqlDataAdapter(vSQL, conexao);              
                vDataAdapter.Fill(vTable);
                conexao.Close();
            }
            return vTable;
        }

        //Carrega o combo Coordenador na tela de cadastro de curso
        public DataTable Coordenador()
        {
            conexao = new MySqlConnection(Endereco());
            conexao.Open();
            string vSQL = "Select nome FROM usuario WHERE func2 = 'Coordenador' ORDER BY nome";
            MySqlDataAdapter vDataAdapter = new MySqlDataAdapter(vSQL, conexao);
            DataTable vTable = new DataTable();
            vDataAdapter.Fill(vTable);
            conexao.Close();
            return vTable;
        }

        public DataTable CoordEmail(Curso cs)
        {
            conexao = new MySqlConnection(Endereco());
            conexao.Open();
            string vSQL = "Select coord, coord2 FROM curso WHERE  nome= '" + cs.Nome + "'";
            MySqlDataAdapter vDataAdapter = new MySqlDataAdapter(vSQL, conexao);
            DataTable vTable = new DataTable();
            vDataAdapter.Fill(vTable);
            conexao.Close();
            return vTable;
        }

        public DataTable EmailCopia(Usuario us)
        {
            conexao = new MySqlConnection(Endereco());
            conexao.Open();
            string vSQL = "Select nome, email FROM usuario WHERE  nome= '" + us.Nome + "' OR nome= '" + us.Profissao2 + "' ORDER BY nome";
            MySqlDataAdapter vDataAdapter = new MySqlDataAdapter(vSQL, conexao);
            DataTable vTable = new DataTable();
            vDataAdapter.Fill(vTable);
            conexao.Close();
            return vTable;
        }

    }

}

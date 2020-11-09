using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Copias
{
    class ConectaItens
    {
        public MySqlConnection conexao;

        public string Endereco()
        {
            StringConexao str = new StringConexao();
            return str.Endereco();
        }

        public void CadCategoria(Itens it)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string inserir = "INSERT INTO categorias(categoria)VALUES('" + it.Categoria + "')";
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void CadItensCategoria(Itens it)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string inserir = "INSERT INTO itenscategoria(item, codCat)VALUES('" + it.ItensCategoria + "','" + it.CodCategoria + "')";
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void AtualizaItensCategoria(Itens it)
        {

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE itenscategoria SET item='" + it.ItensCategoria + "' , categoria = '" + it.Categoria + "'WHERE codItem= '" + it.Codigo + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void AtualizaCategoria(Itens it)
        {

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE categorias SET categoria='" + it.Categoria + "' WHERE codCat= '" + it.Codigo + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void excluirItem(Itens it)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "DELETE FROM itenscategoria WHERE codItem = '" + it.Codigo + "'";
                MySqlCommand comando = new MySqlCommand(alterar, conexao);
                comando.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public DataTable SelecionaItensCategoria(Itens it)
        {

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT codItem, item, categoria FROM itenscategoria WHERE codCat='" + it.CodCategoria + "' ORDER BY item";
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

        public DataTable SelecionaCategoria()
        {

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT codCat, categoria FROM categorias ORDER BY categoria";
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

        public DataTable CodCategoria(Itens it)
        {

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT codCat FROM categorias WHERE categoria='" + it.Categoria + "'";
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

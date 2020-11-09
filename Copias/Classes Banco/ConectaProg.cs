using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace Copias
{
    class ConectaProg
    {
        public MySqlConnection conexao;
        //string caminho = "Persist Security Info=false;SERVER=10.66.122.42;DATABASE=copias;UID=secac;pwd=secac";

        public string Endereco()
        {
            StringConexao str = new StringConexao();
            return str.Endereco();
        }

        public void cadastro(Progressao pr)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string inserir = "INSERT INTO progressao(portaria, nome, componente, professor, turma, modSerieAtual, semestreAno, entrega)VALUES('" + pr.Portaria + "','" + pr.Nome + "','" + pr.Componente + "','" + pr.Professor + "','" + pr.Turma + "','" + pr.Atual + "','" + pr.Semetre + "','" + pr.Entrega + "')";
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void atualizar(Progressao pr)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE progressao SET portaria='"+ pr.Portaria + "',nome='" + pr.Nome + "' , componente = '" + pr.Componente + "', professor = '" + pr.Professor + "' ,turma = '" + pr.Turma + "', modSerieAtual = '" + pr.Atual + "', semestreAno = '" + pr.Semetre + "', entrega = '" + pr.Entrega + "', devolucao = '" + pr.Devolucao + "'WHERE portaria = '" + pr.Portaria + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void excluir(Progressao pr)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string excluir = "DELETE FROM progressao WHERE portaria = '" + pr.Portaria + "'";
                MySqlCommand comando = new MySqlCommand(excluir, conexao);
                comando.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public DataTable BuscaProgressao(Progressao pr)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT portaria, nome, componente, professor, turma, modSerieAtual, semestreAno, entrega, devolucao FROM progressao WHERE portaria='" + pr.Portaria + "'";
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

        public DataTable Progressao()
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, portaria, nome, componente, professor, turma, modSerieAtual, semestreAno, entrega, devolucao FROM progressao order by cod desc";
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

        public DataTable ListaProgressao()
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT portaria, nome, componente, professor, turma, modSerieAtual, semestreAno, entrega, devolucao FROM progressao WHERE devolucao!='Sim' OR devolucao is null";
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

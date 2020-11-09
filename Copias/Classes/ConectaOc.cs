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


namespace Copias
{
    class ConectaOc
    {
        public MySqlConnection conexao;
        //string caminho = "Persist Security Info=false;SERVER=10.66.122.42;DATABASE=copias;UID=secac;pwd=secac";

        public string Endereco()
        {
            StringConexao str = new StringConexao();
            return str.Endereco();
        }

        public void cadastro(Ocorrencia oc)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string inserir = "INSERT INTO ocorrencia(aluno, serie, sigla, data, prof, motivo)VALUES('" + oc.Aluno + "','" + oc.Serie + "','" + oc.Sigla + "','" + Convert.ToDateTime(oc.Data).ToString("yyyy-MM-dd") + "','" + oc.Professor + "','" + oc.Motivo + "')";
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        //Altera requisição sem anexo
        public void atualizar(Ocorrencia oc)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE ocorrencia SET aluno='" + oc.Aluno + "', serie= '" + oc.Serie + "', sigla='" + oc.Sigla + "',data = '" + Convert.ToDateTime(oc.Data).ToString("yyyy-MM-dd") + "', motivo = '" + oc.Motivo + "'WHERE cod = '" + oc.Codigo + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        //Altera requisição sem anexo
        public void atualizarGerados(Ocorrencia oc)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE ocorrencia SET gerado='" + oc.Gerado + "'WHERE cod = '" + oc.Codigo + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void excluir(Ocorrencia oc)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "DELETE FROM ocorrencia WHERE cod = '" + oc.Codigo + "'";
                MySqlCommand comando = new MySqlCommand(alterar, conexao);
                comando.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        //Alimenta o form de atualização e exclusão
        public DataTable carregaGridOc(Ocorrencia oc)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, aluno, serie, sigla, data, prof, motivo FROM ocorrencia";
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

        //Alimenta o relatório de ocorrências por nome
        public DataTable carregaGridOcNome(Ocorrencia oc)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, aluno, serie, sigla, data, prof, motivo FROM ocorrencia WHERE aluno like '%" + oc.Aluno + "%' AND (gerado is null OR gerado='Não')";
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

        //Alimenta o relatório de ocorrências por série
        public DataTable carregaGridOcSerie(Ocorrencia oc)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, aluno, serie, sigla, data, prof, motivo FROM ocorrencia WHERE serie='" + oc.Serie + "' AND (gerado is null OR gerado='Não')";
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

        //Alimenta o relatório de ocorrências por data
        public DataTable carregaGridOcData(Ocorrencia oc)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, aluno, serie, sigla, data, prof, motivo FROM ocorrencia WHERE data='" + Convert.ToDateTime(oc.Data).ToString("yyyy-MM-dd") + "' AND (gerado is null OR gerado='Não')";
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

        //Alimenta o relatório de ocorrências por professor
        public DataTable carregaGridOcProf(Ocorrencia oc)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, aluno, serie, sigla, data, prof, motivo FROM ocorrencia WHERE prof='" + oc.Professor + "' AND (gerado is null OR gerado='Não')";
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

        //Alimenta o relatório com todas as ocorrências
        public DataTable carregaGridOcTd(Ocorrencia oc)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, aluno, serie, sigla, data, prof, motivo, gerado FROM ocorrencia ORDER BY cod DESC";
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

        //Alimenta o relatório com novas ocorrências
        public DataTable carregaGridOcNovas()
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, aluno, serie, sigla, data, prof, motivo, gerado FROM ocorrencia WHERE gerado is null OR gerado='Não' ORDER BY cod DESC";
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

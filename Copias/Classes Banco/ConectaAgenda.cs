using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Copias
{
    class ConectaAgenda
    {
        public MySqlConnection conexao;

        public string Endereco()
        {
            StringConexao str = new StringConexao();
            return str.Endereco();
        }

        public void Agendar(Agenda ag)
        {
            try
            {
                if (ag.MesmoPeriodo == "Ok")
                {
                    conexao = new MySqlConnection(Endereco());
                    conexao.Open();
                    string inserir = "INSERT INTO agenda(data, curso, periodo, coincide, responsavel, agendado, aula, outros)VALUES('" + Convert.ToDateTime(ag.Data).ToString("yyyy-MM-dd") + "', '" + ag.Curso + "', '" + ag.Periodo + "', '" + ag.Coincide + "', '" + ag.Responsavel + "', '" + ag.Local + "', '" + ag.Aula + "', '" + ag.Outros + "')";
                    MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                    comandos.ExecuteNonQuery();
                    conexao.Close();
                }
                else if (ag.MesmoPeriodo == "")
                {
                    conexao = new MySqlConnection(Endereco());
                    conexao.Open();
                    string inserir = "INSERT INTO agenda(data, periodo, coincide, responsavel, agendado, aula, outros)VALUES('" + Convert.ToDateTime(ag.Data).ToString("yyyy-MM-dd") + "', '" + ag.Periodo + "', '" + ag.Coincide + "', '" + ag.Responsavel + "', '" + ag.Local + "', '" + ag.Aula + "', '" + ag.Outros + "')";
                    MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                    comandos.ExecuteNonQuery();
                    conexao.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void excluir(Agenda ag)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "DELETE FROM agenda WHERE cod = '" + ag.Codigo + "'";
                MySqlCommand comando = new MySqlCommand(alterar, conexao);
                comando.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public DataTable SelecionaAgenda(Agenda ag)
        {

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, data, curso, periodo, coincide, responsavel, agendado, aula, outros from agenda WHERE data= '" + Convert.ToDateTime(ag.Data).ToString("yyyy-MM-dd") + "' AND agendado= '" + ag.Local + "'";
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

        public DataTable Responsavel(Agenda ag)
        {

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, responsavel from agenda WHERE cod= '" + ag.Codigo + "'";
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

        public DataTable Professor(Usuario us)
        {

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, nome, email, func1, func2 from usuario WHERE nome= '" + us.Nome + "'";
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

        public DataTable QuemAgendou(Agenda ag)
        {

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, data, curso, periodo, coincide, responsavel, agendado, aula, outros from agenda WHERE cod= '" + ag.Codigo + "'";
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

        public DataTable MesmoHorario(Agenda ag)
        {

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, data, curso, periodo, coincide, responsavel, agendado, aula, outros from agenda WHERE data= '" + Convert.ToDateTime(ag.Data).ToString("yyyy-MM-dd") + "' AND agendado= '" + ag.Local + "' AND coincide= '" + ag.MesmoPeriodo + "' OR periodo= '" + ag.Periodo + "'";
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

        public DataTable Coincide(Agenda ag)
        {

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT cod, data, curso, periodo, coincide, responsavel, agendado, aula, outros from agenda WHERE data= '" + Convert.ToDateTime(ag.Data).ToString("yyyy-MM-dd") + "' AND agendado= '" + ag.Local + "' AND aula= '" + ag.Aula + "'";
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

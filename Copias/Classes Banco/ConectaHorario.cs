using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Copias
{
    class ConectaHorario
    {
        public MySqlConnection conexao;
        //string caminho = "Persist Security Info=false;SERVER=10.66.122.42;DATABASE=copias;UID=secac;pwd=secac;Allow User Variables=True;Convert Zero Datetime=True;default command timeout=0";

        public string Endereco()
        {
            StringConexao str = new StringConexao();
            return str.Endereco();
        }

        public void cadastroHorarios(Horario hr)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string inserir = "INSERT INTO horario(pri_inicio, pri_final, seg_inicio, seg_final, ter_inicio, ter_final, qua_inicio, qua_final, qui_inicio, qui_final, sex_inicio, sex_final, set_inicio, set_final, oit_inicio, oit_final, periodo)VALUES('" + hr.PrimeiraAulaInicio + "', '" + hr.PrimeiraAulaFim + "', '" + hr.SegundaAulaInicio + "', '" + hr.SegundaAulaFim + "', '" + hr.TerceiraAulaInicio + "', '" + hr.TerceiraAulaFim + "', '" + hr.QuartaAulaInicio + "', '" + hr.QuartaAulaFim + "', '" + hr.QuintaAulaInicio + "', '" + hr.QuintaAulaFim + "', '" + hr.SextaAulaInicio + "', '" + hr.SextaAulaFim + "', '" + hr.SetimaAulaInicio + "', '" + hr.SetimaAulaFim + "', '" + hr.OitavaAulaInicio + "', '" + hr.OitavaAulaFim + "', '" + hr.Periodo + "')";
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void AtualizaHorarios(Horario hr)
        {

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE horario SET pri_inicio='" + hr.PrimeiraAulaInicio + "' , pri_final = '" + hr.PrimeiraAulaFim + "', seg_inicio = '" + hr.SegundaAulaInicio + "', seg_final= '" + hr.SegundaAulaFim + "', ter_inicio='" + hr.TerceiraAulaInicio + "', ter_final= '" + hr.TerceiraAulaFim + "', qua_inicio= '" + hr.QuartaAulaInicio + "', qua_final= '" + hr.QuartaAulaFim + "', qui_inicio= '" + hr.QuintaAulaInicio + "', qui_final= '" + hr.QuintaAulaFim + "', sex_inicio= '" + hr.SextaAulaInicio + "', sex_final='" + hr.SextaAulaFim + "', set_inicio= '" + hr.SetimaAulaInicio + "', set_final= '" + hr.SetimaAulaFim + "', oit_inicio= '" + hr.OitavaAulaInicio + "', oit_final= '" + hr.OitavaAulaFim + "'WHERE periodo= '" + hr.Periodo + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public DataTable SelecionaHorarios(Horario hr)
        {

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT pri_inicio, pri_final, seg_inicio, seg_final, ter_inicio, ter_final, qua_inicio, qua_final, qui_inicio, qui_final, sex_inicio, sex_final, set_inicio, set_final, oit_inicio, oit_final, periodo FROM horario WHERE periodo= '" + hr.Periodo + "'";
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

        public DataTable SelecionaOutroHorario(Horario hr)
        {

            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT pri_inicio, pri_final, seg_inicio, seg_final, ter_inicio, ter_final, qua_inicio, qua_final, qui_inicio, qui_final, sex_inicio, sex_final, set_inicio, set_final, oit_inicio, oit_final, periodo FROM horario where periodo= '" + hr.Periodo + "'";
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

        public DataTable Curso()
        {
            conexao = new MySqlConnection(Endereco());
            conexao.Open();
            string vSQL = "SELECT modulo, nome, tipo, num_aulas FROM curso WHERE situacao = 'Ativo' ORDER BY tipo, cod";
            MySqlDataAdapter vDataAdapter = new MySqlDataAdapter(vSQL, conexao);
            DataTable vTable = new DataTable();
            vDataAdapter.Fill(vTable);
            conexao.Close();
            return vTable;
        }

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

        public DataTable NumAulas(Horario hr)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string selecionar = "SELECT num_aulas FROM curso WHERE nome='" + hr.Curso + "'";
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

        public DataTable HorariodeAulas(Horario hr)
        {
            try
            {
                DataTable dt = new System.Data.DataTable();
                if (hr.Periodo == "")
                {
                    conexao = new MySqlConnection(Endereco());
                    conexao.Open();
                    string selecionar = "SELECT num_aulas, periodo FROM curso WHERE nome='" + hr.Curso + "' AND modulo='" + hr.Modulo + "'";
                    MySqlDataAdapter comandos = new MySqlDataAdapter(selecionar, conexao);
                    comandos.Fill(dt);
                    conexao.Close();
                }
                else if (hr.Periodo != "")
                {
                    conexao = new MySqlConnection(Endereco());
                    conexao.Open();
                    string selecionar = "SELECT num_aulas, periodo FROM curso WHERE nome='" + hr.Curso + "' AND modulo='" + hr.Modulo + "'";
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
    }
}

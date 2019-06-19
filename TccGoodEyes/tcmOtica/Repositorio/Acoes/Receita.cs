using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tccGoodEyes.Models;
using tccGoodEyes.Repositorio;
using System.Data;

namespace tccGoodEyes.Repositorio.Acoes
{
    public class Receita
    {
        Conexao con = new Conexao();

        public void inserirReceita(ReceitaModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_inserirReceita", con.conectarBD());

            cmd.Parameters.AddWithValue("$cd_cliente", dto.cd_cliente);
            cmd.Parameters.AddWithValue("$olho_direito", dto.olho_direito);
            cmd.Parameters.AddWithValue("$olho_esquerdo", dto.olho_esquerdo);
            cmd.Parameters.AddWithValue("$distancia_pupilar", dto.distancia_pupilar);
            cmd.Parameters.AddWithValue("$nm_oftalmo", dto.nm_oftalmo);
            cmd.Parameters.AddWithValue("$sobrenome_oftalmo", dto.sobrenome_oftalmo);
            cmd.Parameters.AddWithValue("$dt_receita", dto.dt_receita);
            cmd.Parameters.AddWithValue("$dt_validade", dto.dt_validade);
            cmd.Parameters.AddWithValue("$observacao", dto.observacao);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }

        //-------------------------------------------------------------------------------------------------------------------------------

        public void deletarReceita(ReceitaModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_deleteReceita", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_receita", MySqlDbType.VarChar).Value = dto.cd_receita;

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void atualizarReceita(ReceitaModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_alteraReceita", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_receita", MySqlDbType.VarChar).Value = dto.cd_receita;
            cmd.Parameters.AddWithValue("$cd_cliente", MySqlDbType.VarChar).Value = dto.cd_cliente;
            cmd.Parameters.AddWithValue("$olho_direito", MySqlDbType.VarChar).Value = dto.olho_direito;
            cmd.Parameters.AddWithValue("$olho_esquerdo", MySqlDbType.VarChar).Value = dto.olho_esquerdo;
            cmd.Parameters.AddWithValue("$distancia_pupilar", MySqlDbType.VarChar).Value = dto.distancia_pupilar;
            cmd.Parameters.AddWithValue("$nm_oftalmo", MySqlDbType.VarChar).Value = dto.nm_oftalmo;
            cmd.Parameters.AddWithValue("$sobrenome_oftalmo", MySqlDbType.VarChar).Value = dto.sobrenome_oftalmo;
            cmd.Parameters.AddWithValue("$dt_receita", MySqlDbType.VarChar).Value = dto.dt_receita;
            cmd.Parameters.AddWithValue("$dt_validade", MySqlDbType.VarChar).Value = dto.dt_validade;
            cmd.Parameters.AddWithValue("$observacao", MySqlDbType.VarChar).Value = dto.observacao;

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //---------------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<ReceitaModel> consultaReceita()
        {
            MySqlCommand cmd = new MySqlCommand("pa_exibirReceita", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<ReceitaModel> listaReceita = new List<ReceitaModel>();

            while (rd.Read())
            {

                ReceitaModel dto = new ReceitaModel();

                dto.cd_receita = rd[0].ToString();
                dto.cd_cliente = rd[1].ToString();
                dto.olho_direito = rd[2].ToString();
                dto.olho_esquerdo = rd[3].ToString();
                dto.distancia_pupilar = rd[4].ToString();
                dto.nm_oftalmo = rd[5].ToString();
                dto.sobrenome_oftalmo = rd[7].ToString();
                dto.dt_validade = rd[8].ToString();
                dto.observacao = rd[9].ToString();


                listaReceita.Add(dto);
            }
            rd.Close();
            IEnumerable<ReceitaModel> lista = listaReceita;
            return lista;


        }

        //---------------------------------------------------------------------------------------------------------------------------------

        public ReceitaModel pesquisarPorCodigo(string cd_receita)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbReceita where cd_receita=@cd_receita", con.conectarBD());
            cmd.Parameters.Add("@cd_receita", MySqlDbType.VarChar).Value = cd_receita;
            MySqlDataReader rd = cmd.ExecuteReader();

            ReceitaModel dto = new ReceitaModel();

            while (rd.Read())
            {
                dto.cd_receita = rd[0].ToString();
                dto.cd_cliente = rd[1].ToString();
                dto.olho_direito = rd[2].ToString();
                dto.olho_esquerdo = rd[3].ToString();
                dto.distancia_pupilar = rd[4].ToString();
                dto.nm_oftalmo = rd[5].ToString();
                dto.sobrenome_oftalmo = rd[7].ToString();
                dto.dt_validade = rd[8].ToString();
                dto.observacao = rd[9].ToString();
            }
            rd.Close();

            return dto;


        }
        //---------------------------------------------------------------------------------------------------------------------------------

        public bool VerificaSePossuiReceita(ClienteModel dto)
        {


            MySqlCommand cmd = new MySqlCommand("select count(*) from tbReceita where cd_cliente=@cd_cliente", con.conectarBD());
            cmd.Parameters.Add("@cd_cliente", MySqlDbType.VarChar).Value = dto.cd_cliente;
            int retorno = Convert.ToInt32(cmd.ExecuteScalar());

            if (retorno == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public List<ReceitaModel> pesquisarPorCliente(ClienteModel cli)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarReceita", con.conectarBD());
            cmd.Parameters.Add("$cd_cliente", MySqlDbType.VarChar).Value = cli.cd_cliente;
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.desconectarBD();


            List<ReceitaModel> list = new List<ReceitaModel>();

            foreach (DataRow rd in dt.Rows)
            {
                ReceitaModel dto = new ReceitaModel();
                dto.cd_receita = rd[0].ToString();
                dto.cd_cliente = rd[1].ToString();
                dto.olho_direito = rd[2].ToString();
                dto.olho_esquerdo = rd[3].ToString();
                dto.distancia_pupilar = rd[4].ToString();
                dto.nm_oftalmo = rd[5].ToString();
                dto.sobrenome_oftalmo = rd[7].ToString();
                dto.dt_validade = rd[8].ToString();
                dto.observacao = rd[9].ToString();
                list.Add(dto);
            }

            return list;
        }


        public string getUltimoCadastrado(string cd_cliente)
        {
            MySqlCommand cmd = new MySqlCommand("pa_ultimoReceitaCliente", con.conectarBD());
            cmd.Parameters.Add("$cd_cliente", MySqlDbType.VarChar).Value = cd_cliente;
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            string codigo = string.Empty;
            while (rd.Read())
            {
                codigo = rd[0].ToString();
            }

            rd.Close();

            return codigo;
        }

    }
}

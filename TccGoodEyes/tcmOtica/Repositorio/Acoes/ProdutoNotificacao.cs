using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using tccGoodEyes.Models;
using tccGoodEyes.Repositorio;

namespace tccGoodEyes.Repositorio.Acoes
{
    public class ProdutoNotificacao
    {
        Conexao con = new Conexao();

        public void inserirFavoritos(ProdutoNotificacaoModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_inserirNotificacao", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("$cd_notificacao", MySqlDbType.VarChar).Value = dto.cd_notificacao;
            cmd.Parameters.AddWithValue("$cd_cliente", MySqlDbType.VarChar).Value = dto.cd_cliente;
            cmd.Parameters.AddWithValue("$cd_produto", MySqlDbType.VarChar).Value = dto.cd_produto;

            ;

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //-------------------------------------------------------------------------------------------------------------------------------

        public void deletarFavoritos(string cd_notificacao)
        {
            MySqlCommand cmd = new MySqlCommand("pa_deleteNotificacao", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_notificacao", MySqlDbType.VarChar).Value = cd_notificacao;

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //------------------------------------------------------------------------------------------------------------------------------
        public void atualizarFavoritos(ProdutoNotificacaoModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_alterarNotificacao", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_notificacao", MySqlDbType.VarChar).Value = dto.cd_notificacao;
            cmd.Parameters.AddWithValue("$cd_produto", MySqlDbType.VarChar).Value = dto.cd_produto;
            cmd.Parameters.AddWithValue("$cd_cliente", MySqlDbType.VarChar).Value = dto.cd_cliente;
            


            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }

        //-------------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<ProdutoNotificacaoModel> exibirFavoritos()
        {
            MySqlCommand cmd = new MySqlCommand("pa_exibirNotificacao", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<ProdutoNotificacaoModel> listaFavoritos = new List<ProdutoNotificacaoModel>();

            while (rd.Read())
            {

                ProdutoNotificacaoModel dto = new ProdutoNotificacaoModel();
                dto.cd_notificacao = rd[0].ToString();
                dto.cd_produto = rd[1].ToString();
                dto.cd_cliente = rd[2].ToString();

                listaFavoritos.Add(dto);
            }
            rd.Close();
            IEnumerable<ProdutoNotificacaoModel> lista = listaFavoritos;
            return lista;


        }

        //-------------------------------------------------------------------------------------
        public bool verificaSeJaFavoritou(string cd_produto, string cd_cliente)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbProdutoNotificacao where cd_produto = @cd_produto and cd_cliente = @cd_cliente", con.conectarBD());
            cmd.Parameters.AddWithValue("@cd_produto", MySqlDbType.VarChar).Value = cd_produto;
            cmd.Parameters.AddWithValue("@cd_cliente", MySqlDbType.VarChar).Value = cd_cliente;

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
        //-------------------------------------------------------------------------------------------------------------------------------

        public List<ProdutoNotificacaoModel> verificaFavoritos(string cd_cliente)
        {
            MySqlCommand cmd = new MySqlCommand("pa_verificaNotificacao", con.conectarBD());
            cmd.Parameters.AddWithValue("$cd_cliente", MySqlDbType.VarChar).Value = cd_cliente;
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<ProdutoNotificacaoModel> listaFavoritos = new List<ProdutoNotificacaoModel>();

            while (rd.Read())
            {
                ProdutoNotificacaoModel dto = new ProdutoNotificacaoModel();
                dto.cd_produto = rd[0].ToString();
                dto.cd_notificacao = rd[1].ToString();
                listaFavoritos.Add(dto);
            }
            rd.Close();

            return listaFavoritos;


        }
    }

}


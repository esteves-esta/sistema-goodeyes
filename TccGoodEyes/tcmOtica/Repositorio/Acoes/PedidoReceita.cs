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
    public class PedidoReceita
    {
        Conexao con = new Conexao();

        public void inserirPedidoReceita(PedidoReceitaModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_inserirPedidoReceita", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("$cd_item", dto.cd_item);
            cmd.Parameters.AddWithValue("$cd_pedido", dto.cd_pedido);
            cmd.Parameters.AddWithValue("$cd_receita", dto.cd_receita);

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }
        //-------------------------------------------------------------------------------------------------------------------------------

        public void deletarPedidoReceita(PedidoReceitaModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_deletePedidoReceita", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_pedido", MySqlDbType.VarChar).Value = dto.cd_pedido;

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //------------------------------------------------------------------------------------------------------------------------------
        public void atualizarPedidoReceita(PedidoReceitaModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_alterarPedidoReceita", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_item", dto.cd_item);
            cmd.Parameters.AddWithValue("$cd_pedido", MySqlDbType.VarChar).Value = dto.cd_pedido;
            cmd.Parameters.AddWithValue("$cd_receita", MySqlDbType.VarChar).Value = dto.cd_receita;
            cmd.Parameters.AddWithValue("$cd_pedidoreceita", MySqlDbType.VarChar).Value = dto.cd_pedidoreceita;

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }

        //------------------------------------------------------------------------------------------------------------------------------
        public bool verificaTemReceita(string cd_pedido)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbPedidoReceita where cd_pedido = @cd_pedido", con.conectarBD());
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@cd_pedido", MySqlDbType.VarChar).Value = cd_pedido;

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

        //------------------------------------------------------------------------------------------------------------------------------
        public string getCodReceita(string cd_pedido)
        {
            MySqlCommand cmd = new MySqlCommand("select cd_receita from tbPedidoReceita where cd_pedido = @cd_pedido", con.conectarBD());

            cmd.Parameters.Add("@cd_pedido", MySqlDbType.VarChar).Value = cd_pedido;
            MySqlDataReader rd = cmd.ExecuteReader();

            string cd_receita = string.Empty;

            while (rd.Read())
            {
                cd_receita = rd[0].ToString();
            }
            rd.Close();
            return cd_receita;
        }

        //-------------------------------------------------------------------------------------

        public IEnumerable<ReceitaModel> consultaPedidoReceita(string cd_cliente, string cd_pedido)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarPedidoReceita", con.conectarBD());
            cmd.Parameters.Add("$cd_cliente", MySqlDbType.VarChar).Value = cd_cliente;
            cmd.Parameters.Add("$cd_pedido", MySqlDbType.VarChar).Value = cd_pedido;
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader rd = cmd.ExecuteReader();
            List<ReceitaModel> listaP = new List<ReceitaModel>();

            while (rd.Read())
            {
                ReceitaModel dto = new ReceitaModel();
                dto.cd_pedido = rd[0].ToString();
                dto.cd_receita = rd[1].ToString();
                dto.olho_direito = rd[2].ToString();
                dto.olho_esquerdo = rd[3].ToString();
                dto.distancia_pupilar = rd[4].ToString();
                dto.nm_oftalmo = rd[5].ToString();
                dto.sobrenome_oftalmo = rd[6].ToString();
                dto.dt_receita = rd[7].ToString();
                dto.dt_validade = rd[8].ToString();
                dto.observacao = rd[9].ToString();
                dto.cd_produto = rd[10].ToString();

                listaP.Add(dto);
            }

            rd.Close();
            IEnumerable<ReceitaModel> lista = listaP;
            return lista;
        }

    }
}

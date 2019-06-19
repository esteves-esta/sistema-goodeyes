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
    public class PedidoItem
    {
        Conexao con = new Conexao();

        public void inserirPedidoItens(PedidoItemModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_inserirPedidoItens", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_pedido", dto.cd_pedido);
            cmd.Parameters.AddWithValue("$cd_produto", dto.cd_produto);
            cmd.Parameters.AddWithValue("$qt_item", dto.qt_item);

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //-------------------------------------------------------------------------------------------------------------------------------

        public void deletarPediddoItens(PedidoItemModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_deletePedidoItens", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_pedido", MySqlDbType.VarChar).Value = dto.cd_pedido;

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //------------------------------------------------------------------------------------------------------------------------------
        public void atualizarPedidoItens(PedidoItemModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_alterarPedidoItens", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_pedido", MySqlDbType.VarChar).Value = dto.cd_pedido;
            cmd.Parameters.AddWithValue("$cd_produto", MySqlDbType.VarChar).Value = dto.cd_produto;
            cmd.Parameters.AddWithValue("$qt_item", MySqlDbType.VarChar).Value = dto.qt_item;
            cmd.Parameters.AddWithValue("$vl_subtotal", MySqlDbType.VarChar).Value = dto.vl_subtotal;



            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }

        //-------------------------------------------------------------------------------------------------------------------------------

        /*  public IEnumerable<pedidoDTO> exibirPedidos()
          {
              MySqlCommand cmd = new MySqlCommand("pa_exibirPedido1", con.conectarBD());
              cmd.CommandType = CommandType.StoredProcedure;
              MySqlDataReader rd = cmd.ExecuteReader();

              List<favoritosDTO> listaFavoritos = new List<favoritosDTO>();

              while (rd.Read())
              {

                  favoritosDTO dto = new favoritosDTO();
                  dto.dtPedido = rd[0].ToString();
                  dto.codProd = rd[1].ToString();
                  dto.codCli = rd[2].ToString();

                  listaFavoritos.Add(dto);
              }
              rd.Close();
              IEnumerable<favoritosDTO> lista = listaFavoritos;
              return lista;


          }
          */
//------------------------------------------------------------------------------------------------
        public IEnumerable<ProdutoModel> consultaPedidoItens(string cd_cliente, string cd_pedido)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarPedidoItens", con.conectarBD());
            cmd.Parameters.Add("$cd_cliente", MySqlDbType.VarChar).Value = cd_cliente;
            cmd.Parameters.Add("$cd_pedido", MySqlDbType.VarChar).Value = cd_pedido;
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader rd = cmd.ExecuteReader();
            List<ProdutoModel> listaP = new List<ProdutoModel>();

            while (rd.Read())
            {
                ProdutoModel dto = new ProdutoModel();
                dto.cd_pedido = rd[0].ToString();
                dto.caminho_imagem = rd[1].ToString();
                dto.nm_marca = rd[2].ToString();
                dto.vl_preco_unitario = rd[3].ToString();
                dto.qt_item = rd[4].ToString();
                dto.vl_subtotal = rd[5].ToString();
                dto.descricao = rd[6].ToString();
                dto.aspecto = rd[7].ToString();
                listaP.Add(dto);
            }

            rd.Close();
            IEnumerable<ProdutoModel> lista = listaP;
            return lista;
        }



        public string ultimoCodigo(string cd_cliente)
        {
            MySqlCommand cmd = new MySqlCommand("pa_ultimoItemPedidoCliente", con.conectarBD());
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


  
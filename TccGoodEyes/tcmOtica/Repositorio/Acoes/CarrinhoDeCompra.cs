using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using tccGoodEyes.Models;
using tccGoodEyes.Repositorio;

namespace tccGoodEyes.Repositorio.Acoes
{

    public class CarrinhoDeCompra
    {
        Conexao con = new Conexao();

        public void inserirCarrinho(CarrinhoDeCompraModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_inserirCarrinhoCompra", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("$cd_produto", MySqlDbType.VarChar).Value = dto.cd_produto;
            cmd.Parameters.AddWithValue("$cd_cliente", MySqlDbType.VarChar).Value = dto.cd_cliente;
            cmd.Parameters.AddWithValue("$qt_item", MySqlDbType.VarChar).Value = dto.qt_item;
            
            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //-------------------------------------------------------------------------------------------------------------------------------

        public void deletarCarrinho(ClienteModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_deleteCarrinhoCompra", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_cliente", MySqlDbType.VarChar).Value = dto.cd_cliente;

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }

        //-------------------------------------------------------------------------------------------------------------------------------

        public void deletarCarrinho(string codCarrinho)
        {
            MySqlCommand cmd = new MySqlCommand("pa_deleteCarrinhoCompra2", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_carrinho", MySqlDbType.VarChar).Value = codCarrinho;

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //------------------------------------------------------------------------------------------------------------------------------
        public void atualizarCarrinho(CarrinhoDeCompraModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_alterarCarrinhoCompra", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_carrinho", MySqlDbType.VarChar).Value = dto.cd_carrinho;
            cmd.Parameters.AddWithValue("$cd_produto", MySqlDbType.VarChar).Value = dto.cd_produto;
            cmd.Parameters.AddWithValue("$cd_cliente", MySqlDbType.VarChar).Value = dto.cd_cliente;
            cmd.Parameters.AddWithValue("$qt_item", MySqlDbType.VarChar).Value = dto.qt_item;
            

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }

        //-------------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<CarrinhoDeCompraModel> consultaCarrinho()
        {
            MySqlCommand cmd = new MySqlCommand("pa_exibirCarrinhoCompra", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<CarrinhoDeCompraModel> listaCarrinho = new List<CarrinhoDeCompraModel>();

            while (rd.Read())
            {

                CarrinhoDeCompraModel dto = new CarrinhoDeCompraModel();
                dto.cd_produto = rd[0].ToString();
                dto.cd_cliente = rd[1].ToString();
                dto.qt_item = rd[2].ToString();

                listaCarrinho.Add(dto);
            }
            rd.Close();
            IEnumerable<CarrinhoDeCompraModel> lista = listaCarrinho;
            return lista;

        }

        //-------------------------------------------------------------------------------------

        public IEnumerable<ProdutoModel> pesquisarCarrinho(string cd_cliente)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarCarrinhoCompra", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("$cd_cliente", MySqlDbType.VarChar).Value = cd_cliente;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<ProdutoModel> listaCarrinho = new List<ProdutoModel>();

            while (rd.Read())
            {

                ProdutoModel dto = new ProdutoModel();
                dto.cd_carrinho = rd[0].ToString(); //codigo do carrinho
                dto.cd_produto = rd[1].ToString();
                dto.caminho_imagem = rd[2].ToString();
                dto.nm_marca = rd[3].ToString();
                dto.vl_preco_unitario = rd[4].ToString();
                dto.qt_item = rd[5].ToString();

                listaCarrinho.Add(dto);
            }
            rd.Close();
            IEnumerable<ProdutoModel> lista = listaCarrinho;
            return lista;


        }

        public List<ProdutoModel> pesquisarCarrinhoLista(string cd_cliente)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarCarrinhoCompra", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("$cd_cliente", MySqlDbType.VarChar).Value = cd_cliente;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<ProdutoModel> listaCarrinho = new List<ProdutoModel>();

            while (rd.Read())
            {

                ProdutoModel dto = new ProdutoModel();
                dto.cd_carrinho = rd[0].ToString(); //codigo do carrinho
                dto.cd_produto = rd[1].ToString();
                dto.caminho_imagem = rd[2].ToString();
                dto.nm_marca = rd[3].ToString();
                dto.vl_preco_unitario = rd[4].ToString();
                dto.qt_item = rd[5].ToString();

                listaCarrinho.Add(dto);
            }
            rd.Close();
            
            return listaCarrinho;


        }

        //-------------------------------------------------------------------------------------


        public string Count(string tabela)
        {
            string c = string.Empty;

            MySqlCommand cmd = new MySqlCommand("select Count(*) from " + tabela, con.conectarBD());
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                c = dr[0].ToString();
            }
            dr.Close();
            return c;

        }
        //-------------------------------------------------------------------------------------


        public bool VerificaCarrinhoVazio(ClienteModel dto)
        {

            MySqlCommand cmd = new MySqlCommand("pa_verificaCarrinhoCompra", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("$cd_cliente", MySqlDbType.VarChar).Value = dto.cd_cliente;

            int retorno = Convert.ToInt32(cmd.ExecuteScalar());

            if (retorno == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //-------------------------------------------------------------------------------------
        public string getUltimoCadastrado(string cod)
        {
            MySqlCommand cmd = new MySqlCommand("pa_ultimoCarrinhoCliente", con.conectarBD());
            cmd.Parameters.Add("$cd_cliente", MySqlDbType.VarChar).Value = cod;
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

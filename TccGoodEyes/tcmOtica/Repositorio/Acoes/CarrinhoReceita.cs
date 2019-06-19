using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using tccGoodEyes.Repositorio;
using tccGoodEyes.Models;

namespace tccGoodEyes.Repositorio.Acoes
{
    public class CarrinhoReceita
    {
        Conexao con = new Conexao();

        public void inserirPedidoReceita(CarrinhoReceitaModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_inserirCarrinhoReceita", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("$cd_cliente", MySqlDbType.VarChar).Value = dto.cd_cliente;
            cmd.Parameters.AddWithValue("$cd_carrinho", MySqlDbType.VarChar).Value = dto.cd_carrinhoreceita;
            cmd.Parameters.AddWithValue("$cd_receita", MySqlDbType.VarChar).Value = dto.cd_receita;



            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //-------------------------------------------------------------------------------------------------------------------------------

        public void deletarCarrinhoReceitaPorCliente(string cd_cliente)
        {
            MySqlCommand cmd = new MySqlCommand("pa_deleteCarrinhoReceitaCliente", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_cliente", MySqlDbType.VarChar).Value = cd_cliente;

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }


        public void deletarCarrinhoReceitaPorProduto(string cd_carrinhoreceita)
        {
            MySqlCommand cmd = new MySqlCommand("pa_deleteCarrinhoReceita", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_carrinhoreceita", MySqlDbType.VarChar).Value = cd_carrinhoreceita;

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }

        //-------------------------------------------------------------------------------------
        public bool verificaReceita(string cd_carrinho)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbCarrinhoReceita where cd_carrinho = @cd_carrinho", con.conectarBD());
            cmd.Parameters.AddWithValue("@cd_carrinho", MySqlDbType.VarChar).Value = cd_carrinho;

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




        public string pesquisarCodCarrinhoReceita(string cd_carrinho)
        {
            MySqlCommand cmd = new MySqlCommand("select cd_receita from tbCarrinhoReceita where cd_carrinho = @cd_carrinho", con.conectarBD());
            cmd.Parameters.AddWithValue("@cd_carrinho", MySqlDbType.VarChar).Value = cd_carrinho;

            MySqlDataReader rd = cmd.ExecuteReader();

            string codReceita = string.Empty;
            while (rd.Read())
            {

                codReceita = rd[0].ToString();

            }
            rd.Close();

            return codReceita;


        }


    }
}
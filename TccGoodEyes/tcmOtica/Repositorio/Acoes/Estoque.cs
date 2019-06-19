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
    public class Estoque
    {
        Conexao con = new Conexao();

        public void inserirEstoque(EstoqueModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_inserirEstoque", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_estoque", MySqlDbType.VarChar).Value = dto.cd_estoque;
            cmd.Parameters.AddWithValue("$cd_produto", MySqlDbType.VarChar).Value = dto.cd_produto;
            cmd.Parameters.AddWithValue("$qt_estoque", MySqlDbType.VarChar).Value = dto.qt_estoque;

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //-------------------------------------------------------------------------------------------------------------------------------

        public void deletarEstoque(EstoqueModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_deleteEstoque", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_estoque", MySqlDbType.VarChar).Value = dto.cd_estoque;

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //------------------------------------------------------------------------------------------------------------------------------
        public void atualizarEstoque(EstoqueModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_alterarEstoque", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("$cd_produto", MySqlDbType.VarChar).Value = dto.cd_produto;
            cmd.Parameters.AddWithValue("$qt_estoque", MySqlDbType.VarChar).Value = dto.qt_estoque;


            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }

        //-------------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<EstoqueModel> consultaEstoque()
        {
            MySqlCommand cmd = new MySqlCommand("pa_exibirEstoque", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<EstoqueModel> listaEstoque = new List<EstoqueModel>();

            while (rd.Read())
            {

                EstoqueModel dto = new EstoqueModel();
                dto.cd_estoque = rd[0].ToString();
                dto.cd_produto = rd[1].ToString();
                dto.qt_estoque = rd[2].ToString();

                listaEstoque.Add(dto);
            }
            rd.Close();
            IEnumerable<EstoqueModel> lista = listaEstoque;
            return lista;
        }

        

        public int verificaEstoque(EstoqueModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_verificaEstoque", con.conectarBD());
            cmd.Parameters.Add("$cd_produto", MySqlDbType.VarChar).Value = dto.cd_produto;
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader rd = cmd.ExecuteReader();
            int qtde = 0;
            while (rd.Read())
            {
                qtde = Convert.ToInt32(rd[0]);
            }
            rd.Close();
            return qtde;
        }




    }
}
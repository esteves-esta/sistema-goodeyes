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
    public class Pagamento
    {
        Conexao con = new Conexao();

        public void inserirFormaPagamento(PagamentoModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_inserirFormaPagamento", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_pedido", dto.cd_pedido);
            cmd.Parameters.AddWithValue("$tipo_pagamento", dto.tipo_pagamento);
            cmd.Parameters.AddWithValue("$parcelamento", dto.parcelamento);

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }

        //-------------------------------------------------------------------------------------------------------------------------------

        public void deletarFormaPagamento(PagamentoModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_deleteFormaPagamento", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_pagamento", MySqlDbType.VarChar).Value = dto.cd_pagamento;

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void atualizarFormaPagamento(PagamentoModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_alterarFormaPagamento", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_pagamento", MySqlDbType.VarChar).Value = dto.cd_pagamento;
            cmd.Parameters.AddWithValue("$cd_pedido", dto.cd_pedido);
            cmd.Parameters.AddWithValue("$tipo_pagamento", dto.tipo_pagamento);
            cmd.Parameters.AddWithValue("$parcelamento", dto.parcelamento);
            cmd.Parameters.AddWithValue("$vl_total", MySqlDbType.VarChar).Value = dto.vl_total;



            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //---------------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<PagamentoModel> consultaFormaPagamento()
        {
            MySqlCommand cmd = new MySqlCommand("pa_exibirFormaPagamento", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;


            MySqlDataReader rd = cmd.ExecuteReader();

            List<PagamentoModel> listaFormaPagamento = new List<PagamentoModel>();

            while (rd.Read())
            {

                PagamentoModel dto = new PagamentoModel();

                dto.cd_pagamento = rd[0].ToString();
                dto.cd_pedido = rd[1].ToString();
                dto.tipo_pagamento = rd[2].ToString();
                dto.parcelamento = rd[3].ToString();
                dto.vl_total = rd[4].ToString();

            }
            rd.Close();
            IEnumerable<PagamentoModel> lista = listaFormaPagamento;
            return lista;


        }

        //---------------------------------------------

        public PagamentoModel pesquisaFormaPagamento(string cd_cliente, string cd_pedido)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarFormaPagamento", con.conectarBD());
            cmd.Parameters.Add("$cd_cliente", MySqlDbType.VarChar).Value = cd_cliente;
            cmd.Parameters.Add("$cd_pedido", MySqlDbType.VarChar).Value = cd_pedido;
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader rd = cmd.ExecuteReader();
            PagamentoModel dto = new PagamentoModel();

            while (rd.Read())
            {
                dto.cd_pagamento = rd[0].ToString();
                dto.cd_pedido = rd[1].ToString();
                dto.tipo_pagamento = rd[2].ToString();
                dto.parcelamento = rd[3].ToString();
                dto.vl_total = rd[4].ToString();
            }

            rd.Close();

            return dto;

        }

     

        //---------------------------------------------

        public PagamentoModel consultaFormaPagamento(ClienteModel dtoX)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarFormaPagamentoCli", con.conectarBD());
            cmd.Parameters.Add("$cd_cliente", MySqlDbType.VarChar).Value = dtoX.cd_cliente;
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader rd = cmd.ExecuteReader();
            
            PagamentoModel dto = new PagamentoModel();
            while (rd.Read())
            {
                dto.cd_pagamento = rd[0].ToString();
                dto.cd_pedido = rd[1].ToString();
                dto.tipo_pagamento = rd[2].ToString();
                dto.parcelamento = rd[3].ToString();
                dto.vl_total = rd[4].ToString();
            }

            rd.Close();
            return dto;
        }

        //---------------------------------------------

        public IEnumerable<PagamentoModel> consultaFormaPagamentoLista(ClienteModel dtoX)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarFormaPagamentoCli", con.conectarBD());
            cmd.Parameters.Add("$cd_cliente", MySqlDbType.VarChar).Value = dtoX.cd_cliente;
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader rd = cmd.ExecuteReader();
            List<PagamentoModel> listaformas = new List<PagamentoModel>();

            while (rd.Read())
            {
                PagamentoModel dto = new PagamentoModel();
                dto.cd_pagamento = rd[0].ToString();
                dto.cd_pedido = rd[1].ToString();
                dto.tipo_pagamento = rd[2].ToString();
                dto.parcelamento = rd[3].ToString();
                dto.vl_total = rd[4].ToString();
                listaformas.Add(dto);
            }

            rd.Close();
            IEnumerable<PagamentoModel> lista = listaformas;
            return lista;
        }
    }
}
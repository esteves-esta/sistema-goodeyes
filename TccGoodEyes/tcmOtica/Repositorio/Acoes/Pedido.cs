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
    public class Pedido
    {

        Conexao con = new Conexao();

        public void inserirPedido(PedidoModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_inserirPedido", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$status_pedido", dto.status_pedido);
            cmd.Parameters.AddWithValue("$cd_cliente",dto.cd_cliente);
            cmd.Parameters.AddWithValue("$dt_pedido", dto.dt_pedido);

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //-------------------------------------------------------------------------------------------------------------------------------

        public void deletarPediddo(PedidoModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_deletePedido", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_pedido", MySqlDbType.VarChar).Value = dto.cd_pedido;

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //------------------------------------------------------------------------------------------------------------------------------
        public void atualizarPedido(PedidoModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_alterarPedido", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_pedido", MySqlDbType.VarChar).Value = dto.cd_pedido;
            cmd.Parameters.AddWithValue("$cd_cliente", MySqlDbType.VarChar).Value = dto.cd_cliente;
            cmd.Parameters.AddWithValue("$dt_pedido", MySqlDbType.VarChar).Value = dto.dt_pedido;
            cmd.Parameters.AddWithValue("$status_pedido", MySqlDbType.VarChar).Value = dto.status_pedido;

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }

        //-------------------------------------------------------------------------------------------------------------------------------

 
        public List<VendasModel> consultaVendas()
        {
            MySqlCommand cmd = new MySqlCommand("pa_exibirVendas", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<VendasModel> listaF = new List<VendasModel>();

            while (rd.Read())
            {
                VendasModel venda = new VendasModel();
                venda.cd_pedido = rd[0].ToString();
                venda.cd_cliente = rd[1].ToString();
                venda.nm_cliente = rd[2].ToString();
                venda.dt_pedido = rd[3].ToString();
                venda.parcelamento = rd[4].ToString();
                switch (rd[5].ToString())
                {
                    case "1":
                        venda.status_pedido = "Pagamento Pendente";
                        break;
                    case "2":
                        venda.status_pedido = "Pagamento Confirmado";
                        break;
                    case "3":
                        venda.status_pedido = "Produto em Transporte";
                        break;
                    case "4":
                        venda.status_pedido = "Enviado";
                        break;
                    case "5":
                        venda.status_pedido = "Pedido Cancelado";
                        break;
                }
                venda.vl_total = rd[6].ToString();
                listaF.Add(venda);
            }

            rd.Close();

            return listaF;
        }

        //-------------------------------------------------------------------------------------------------------------------------------


        public IEnumerable<VendasModel> consultaVendasLista()
        {
            MySqlCommand cmd = new MySqlCommand("pa_exibirVendas", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<VendasModel> listaF = new List<VendasModel>();

            while (rd.Read())
            {
                VendasModel venda = new VendasModel();
                venda.cd_pedido = rd[0].ToString();
                venda.cd_cliente = rd[1].ToString();
                venda.nm_cliente = rd[2].ToString();
                venda.dt_pedido = rd[3].ToString();
                venda.parcelamento = rd[4].ToString();
                switch (rd[5].ToString()) {
                    case "1":
                        venda.status_pedido = "Pagamento Pendente";
                        break;
                    case "2":
                        venda.status_pedido = "Pagamento Confirmado";
                        break;
                    case "3":
                        venda.status_pedido = "Produto em Transporte";
                        break;
                    case "4":
                        venda.status_pedido = "Enviado";
                        break;
                    case "5":
                        venda.status_pedido = "Pedido Cancelado";
                        break;
                }
                venda.vl_total = rd[6].ToString();
                listaF.Add(venda);
            }

            rd.Close();
            IEnumerable<VendasModel> lista = listaF;
            return listaF;
        }

        //-------------------------------------------------------------------------------------------------------------------------------


        public IEnumerable<VendasModel> pesquisarVendasLista(VendasModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarVendas", con.conectarBD());
            cmd.Parameters.Add("$nome", MySqlDbType.VarChar).Value = dto.nm_cliente;
            cmd.Parameters.Add("$sobrenome", MySqlDbType.VarChar).Value = dto.cliente_sobrenome;
            cmd.Parameters.Add("$dt_pedido", MySqlDbType.VarChar).Value = dto.dt_pedido;
            cmd.Parameters.Add("$parcelamento", MySqlDbType.VarChar).Value = dto.parcelamento;
            cmd.Parameters.Add("$status_pedido", MySqlDbType.VarChar).Value = dto.status_pedido;

            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<VendasModel> listaF = new List<VendasModel>();

            while (rd.Read())
            {
                VendasModel venda = new VendasModel();
                venda.cd_pedido = rd[0].ToString();
                venda.cd_cliente = rd[1].ToString();
                venda.nm_cliente = rd[2].ToString();
                venda.dt_pedido = rd[3].ToString();
                venda.parcelamento = rd[4].ToString();

                switch (rd[5].ToString())
                {
                    case "1":
                        venda.status_pedido = "Pagamento Pendente";
                        break;
                    case "2":
                        venda.status_pedido = "Pagamento Confirmado";
                        break;
                    case "3":
                        venda.status_pedido = "Produto em Transporte";
                        break;
                    case "4":
                        venda.status_pedido = "Enviado";
                        break;
                    case "5":
                        venda.status_pedido = "Pedido Cancelado";
                        break;
                }

                venda.vl_total = rd[6].ToString();
                listaF.Add(venda);
            }

            rd.Close();
            IEnumerable<VendasModel> lista = listaF;
            return listaF;
        }

        //----------------------------------------------

        public IEnumerable<PedidoModel> consultaPedido(ClienteModel dtoX)
        {
            MySqlCommand cmd = new MySqlCommand("pa_exibirPedidoCliente", con.conectarBD());
            cmd.Parameters.Add("$cd_cliente", MySqlDbType.VarChar).Value = dtoX.cd_cliente;
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader rd = cmd.ExecuteReader();
            List<PedidoModel> listaP = new List<PedidoModel>();

            while (rd.Read())
            {
                PedidoModel dto = new PedidoModel();
                dto.cd_pedido = rd[0].ToString();
                dto.dt_pedido = rd[1].ToString();
                dto.qt_item = rd[2].ToString();
                dto.vl_total = rd[3].ToString();
                //dto.status_pedido = rd[4].ToString();
                switch (rd[4].ToString())
                {
                    case "1":
                        dto.status_pedido = "Pagamento Pendente";
                        break;
                    case "2":
                        dto.status_pedido = "Pagamento Confirmado";
                        break;
                    case "3":
                        dto.status_pedido = "Produto em Transporte";
                        break;
                    case "4":
                        dto.status_pedido = "Enviado";
                        break;
                    case "5":
                        dto.status_pedido = "Pedido Cancelado";
                        break;
                }
                listaP.Add(dto);
            }

            rd.Close();
            IEnumerable<PedidoModel> lista = listaP;
            return lista;
        }

        //----------------------------------------------

        public IEnumerable<PedidoModel> consultaPedidoCod(string cd_pedido)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarPedidoCod", con.conectarBD());
            cmd.Parameters.Add("$cd_pedido", MySqlDbType.VarChar).Value = cd_pedido;
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader rd = cmd.ExecuteReader();
            List<PedidoModel> listaP = new List<PedidoModel>();

            while (rd.Read())
            {
                PedidoModel dto = new PedidoModel();
                dto.cd_pedido = rd[0].ToString();
                dto.dt_pedido = rd[1].ToString();
                dto.qt_item = rd[2].ToString();
                dto.vl_total = rd[3].ToString();
               // dto.status_pedido = rd[4].ToString();
                switch (rd[4].ToString())
                {
                    case "1":
                        dto.status_pedido = "Pagamento Pendente";
                        break;
                    case "2":
                        dto.status_pedido = "Pagamento Confirmado";
                        break;
                    case "3":
                        dto.status_pedido = "Produto em Transporte";
                        break;
                    case "4":
                        dto.status_pedido = "Enviado";
                        break;
                    case "5":
                        dto.status_pedido = "Pedido Cancelado";
                        break;
                }

                listaP.Add(dto);
            }

            rd.Close();
            IEnumerable<PedidoModel> lista = listaP;
            return lista;
        }


        //--------------------------------------------------------------------------------------------------------------------------
        public string getUltimoCadastrado(string cd_cliente)
        {
            MySqlCommand cmd = new MySqlCommand("pa_ultimoPedidoCliente", con.conectarBD());
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


        //--------------------------------------------------------------------------------------------------------------------------
        public bool verificaSeExistePedido(string cd_cliente)
        {
            MySqlCommand cmd = new MySqlCommand("pa_verificaPedidos", con.conectarBD());
            cmd.Parameters.Add("$cd_cliente", MySqlDbType.VarChar).Value = cd_cliente;
            cmd.CommandType = CommandType.StoredProcedure;

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


        //--------------------------------------------------------------------------------------------------------------------------


    }
}

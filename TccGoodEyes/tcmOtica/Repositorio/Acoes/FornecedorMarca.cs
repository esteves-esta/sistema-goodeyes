using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tccGoodEyes.Models;
using tccGoodEyes.Repositorio;

namespace tccGoodEyes.Repositorio.Acoes
{
    public class FornecedorMarca
    {
        Conexao con = new Conexao();

        public void inserirFornecedorMarcaBLL(FornecedorMarcaModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_inserirFornecedorMarca", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$nm_marca", dto.nm_marca);
            cmd.Parameters.AddWithValue("$cd_fornecedor", dto.cd_fornecedor);
            
            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //-------------------------------------------------------------------------------------------------------------------------------

        public void deletarFornecedorMarca(string nm_marca)
        {
            MySqlCommand cmd = new MySqlCommand("pa_deleteFornecedorMarca", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$nm_marca", nm_marca);

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //------------------------------------------------------------------------------------------------------------------------------
        public void atualizarFornecedorMarca(FornecedorMarcaModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_alterarFornecedorMarca", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$nm_marca", dto.nm_marca);
            cmd.Parameters.AddWithValue("$cd_fornecedor", dto.cd_fornecedor);

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }

        //-------------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<FornecedorModel> exibirfornMarcas()
        {
            MySqlCommand cmd = new MySqlCommand("pa_exibirFornecedorMarca", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<FornecedorModel> listaFornecedorMarca = new List<FornecedorModel>();

            while (rd.Read())
            {

                FornecedorModel dto = new FornecedorModel();
                dto.nm_marca = rd[0].ToString();
                dto.cd_fornecedor = rd[1].ToString();
                dto.nome = rd[2].ToString();
                dto.sobrenome = rd[3].ToString();

                listaFornecedorMarca.Add(dto);
            }
            rd.Close();
            IEnumerable<FornecedorModel> lista = listaFornecedorMarca;
            return lista;


        }

        //-------------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<FornecedorModel> pesqfornMarcas(string nm_marca)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesqFornecedorMarca", con.conectarBD());
            cmd.Parameters.AddWithValue("$nm_marca", nm_marca);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<FornecedorModel> listaFornecedorMarca = new List<FornecedorModel>();

            while (rd.Read())
            {

                FornecedorModel dto = new FornecedorModel();
                dto.nm_marca = rd[0].ToString();
                dto.cd_fornecedor = rd[1].ToString();
                dto.nome = rd[2].ToString();
                dto.sobrenome = rd[3].ToString();
                listaFornecedorMarca.Add(dto);
            }
            rd.Close();
            IEnumerable<FornecedorModel> lista = listaFornecedorMarca;
            return lista;


        }

        //-------------------------------------------------------------------------------------------------------------------------------

        public FornecedorMarcaModel pesquisarMarca(string nm_marca)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarMarca", con.conectarBD());
            cmd.Parameters.AddWithValue("$nm_marca", nm_marca);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            FornecedorMarcaModel dto = new FornecedorMarcaModel();
            while (rd.Read())
            {
                dto.nm_marca = rd[0].ToString();
                dto.cd_fornecedor = rd[1].ToString();
            }
            rd.Close();

            return dto;
        }



        public IEnumerable<FornecedorMarcaModel> exibirfornecedorMarcas()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT nm_marca, cd_fornecedor FROM tbfornecedormarca order by cd_fornecedor asc; ", con.conectarBD());

            MySqlDataReader rd = cmd.ExecuteReader();

            List<FornecedorMarcaModel> listaFornecedorMarca = new List<FornecedorMarcaModel>();

            while (rd.Read())
            {

                FornecedorMarcaModel dto = new FornecedorMarcaModel();
                dto.nm_marca = rd[0].ToString();
                dto.cd_fornecedor = rd[1].ToString();

                listaFornecedorMarca.Add(dto);
            }
            rd.Close();
            IEnumerable<FornecedorMarcaModel> lista = listaFornecedorMarca;
            return lista;


        }
    

        //-------------------------------------------------------------------------------------

        public SelectList consultaMarca()
        {
            MySqlCommand cmd = new MySqlCommand("pa_consultarMarcasOculos", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.desconectarBD();

            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Value = row["nm_marca"].ToString()
                });
            }
            return new SelectList(list, "Value", "Value");
        }

        public SelectList consultaMarcaLente()
        {
            MySqlCommand cmd = new MySqlCommand("pa_consultarMarcasLente", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.desconectarBD();

            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in dt.Rows)
            {
                    list.Add(new SelectListItem()
                    {
                        Value = row["nm_marca"].ToString()
                    });
                
            }
            return new SelectList(list, "Value", "Value");
        }

        public List<string> consultaMarcaLista()
        {
            MySqlCommand cmd = new MySqlCommand("pa_consultarMarcasOculos", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.desconectarBD();


            List<string> list = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                if (!list.Contains(row["nm_marca"].ToString()))
                {
                    list.Add(row["nm_marca"].ToString());
                }
            }
            return list;
        }

        public List<string> consultaMarcaListaLente()
        {
            MySqlCommand cmd = new MySqlCommand("pa_consultarMarcasLente", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.desconectarBD();

            List<string> list = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                if (!list.Contains(row["nm_marca"].ToString()))
                {
                    list.Add(row["nm_marca"].ToString());
                }
            }
            return list;
        }

        public List<ProdutoModel> consultaMarcaLenteprod()
        {
            MySqlCommand cmd = new MySqlCommand("pa_consultarMarcasLente", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.desconectarBD();

            List<ProdutoModel> list = new List<ProdutoModel>();

            foreach (DataRow row in dt.Rows)
            {
                ProdutoModel dto = new ProdutoModel();
                dto.cd_produto = row[0].ToString();
                dto.nm_marca = row[1].ToString();
                dto.descricao = row[2].ToString();
                dto.aspecto = row[3].ToString();
                dto.vl_preco_unitario = row[4].ToString();
                dto.garantia = row[5].ToString();
                list.Add(dto);
            }
            return list;
        }

    }
}
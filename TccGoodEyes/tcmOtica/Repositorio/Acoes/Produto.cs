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
    public class Produto
    {
        Conexao con = new Conexao();

        public void inserirProduto(ProdutoModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_inserirProduto", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$nm_marca", dto.nm_marca);
            cmd.Parameters.AddWithValue("$tipo", dto.tipo);
            cmd.Parameters.AddWithValue("$descricao", dto.descricao);
            cmd.Parameters.AddWithValue("$aspecto", dto.aspecto);
            cmd.Parameters.AddWithValue("$vl_preco_unitario", dto.vl_preco_unitario.Replace(',', '.'));
            cmd.Parameters.AddWithValue("$garantia", dto.garantia);
            cmd.Parameters.AddWithValue("$caminho_imagem", dto.caminho_imagem);

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //-------------------------------------------------------------------------------------------------------------------------------

        public void deletarProduto(string cd_produto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_deleteProduto", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_produto", MySqlDbType.VarChar).Value = cd_produto;

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //-------------------------------------------------------------------------------------------------------------------------------

        public void diminuirestoque(string cd_produto, string qt_itens)
        {
            MySqlCommand cmd = new MySqlCommand("pa_diminuiEstoque", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_produto", cd_produto);
            cmd.Parameters.AddWithValue("$qt_itens", qt_itens);

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        
        //------------------------------------------------------------------------------------------------------------------------------
        public void atualizarProduto(ProdutoModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_alterarProduto", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_produto", dto.cd_produto);
            cmd.Parameters.AddWithValue("$nm_marca", dto.nm_marca);
            cmd.Parameters.AddWithValue("$tipo", dto.tipo);
            cmd.Parameters.AddWithValue("$descricao", dto.descricao);
            cmd.Parameters.AddWithValue("$aspecto", dto.aspecto);
            cmd.Parameters.AddWithValue("$vl_preco_unitario", dto.vl_preco_unitario.Replace(',', '.'));
            cmd.Parameters.AddWithValue("$garantia", dto.garantia);
            cmd.Parameters.AddWithValue("$caminho_imagem", dto.caminho_imagem);


            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }

        //-------------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<ProdutoModel> consultaProduto()
        {
            MySqlCommand cmd = new MySqlCommand("pa_exibirProduto", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<ProdutoModel> listaProduto = new List<ProdutoModel>();

            while (rd.Read())
            {

                ProdutoModel dto = new ProdutoModel();
                dto.cd_produto = rd[0].ToString();
                dto.nm_marca = rd[1].ToString();
                dto.tipo = rd[2].ToString();
                dto.descricao = rd[3].ToString();
                dto.aspecto = rd[4].ToString();
                dto.vl_preco_unitario = rd[5].ToString();
                dto.garantia = rd[6].ToString();
                dto.caminho_imagem = rd[7].ToString();
                

                listaProduto.Add(dto);
            }

            rd.Close();
            IEnumerable<ProdutoModel> lista = listaProduto;
            return lista;


        }

        //-------------------------------------------------------------------------------------

        public IEnumerable<ProdutoModel> pesquisarProduto(ProdutoModel dtoX)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarProduto2", con.conectarBD());
            cmd.Parameters.Add("nm_marca", MySqlDbType.VarChar).Value = dtoX.nm_marca;
            cmd.Parameters.Add("$tipo", MySqlDbType.VarChar).Value = dtoX.tipo;
            cmd.Parameters.Add("$descricao", MySqlDbType.VarChar).Value = dtoX.descricao;
            cmd.Parameters.Add("$aspecto", MySqlDbType.VarChar).Value = dtoX.aspecto;
            string[] preco = dtoX.vl_preco_unitario.Split('-');

            cmd.Parameters.Add("$precoMin", MySqlDbType.VarChar).Value = preco[0];
            cmd.Parameters.Add("$precoMax", MySqlDbType.VarChar).Value = preco[1];

            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<ProdutoModel> listaProduto = new List<ProdutoModel>();

            while (rd.Read())
            {

                ProdutoModel dto = new ProdutoModel();
                dto.cd_produto = rd[0].ToString();
                dto.nm_marca = rd[1].ToString();
                dto.tipo = rd[2].ToString();
                dto.descricao = rd[3].ToString();
                dto.aspecto = rd[4].ToString();
                dto.vl_preco_unitario = rd[5].ToString();
                dto.garantia = rd[6].ToString();
                dto.caminho_imagem = rd[7].ToString();


                listaProduto.Add(dto);
            }

            rd.Close();
            IEnumerable<ProdutoModel> lista = listaProduto;
            return lista;


        }

        //--------------------------------------------------------------------------------------------------------------------------

        public ProdutoModel pesquisaProdutoPorCodigo(string cd_produto)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbProduto where cd_produto=@cd_produto", con.conectarBD());
            cmd.Parameters.Add("@cd_produto", MySqlDbType.VarChar).Value = cd_produto;

            MySqlDataReader rd = cmd.ExecuteReader();

            ProdutoModel dto = new ProdutoModel();
            while (rd.Read())
            {
                dto.cd_produto = rd[0].ToString();
                dto.nm_marca = rd[1].ToString();
                dto.tipo = rd[2].ToString();
                dto.descricao = rd[3].ToString();
                dto.aspecto = rd[4].ToString();
                dto.vl_preco_unitario = rd[5].ToString();
                dto.garantia = rd[6].ToString();
                dto.caminho_imagem = rd[7].ToString();
            }

            rd.Close();

            return dto;
        }

        public IEnumerable<ProdutoModel> pesquisaCompletaProduto(ProdutoModel dtoX)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarProduto2", con.conectarBD());
            cmd.Parameters.Add("$nm_marca", MySqlDbType.VarChar).Value = dtoX.nm_marca;
            cmd.Parameters.Add("$tipo", MySqlDbType.VarChar).Value = dtoX.tipo;
            cmd.Parameters.Add("$descricao", MySqlDbType.VarChar).Value = dtoX.descricao;
            cmd.Parameters.Add("$aspecto", MySqlDbType.VarChar).Value = dtoX.aspecto;
            string[] preco = dtoX.vl_preco_unitario.Split('-');

            cmd.Parameters.Add("$precoMin", MySqlDbType.VarChar).Value = preco[0];
            cmd.Parameters.Add("$precoMax", MySqlDbType.VarChar).Value = preco[1];

            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader rd = cmd.ExecuteReader();

            List<ProdutoModel> listaP = new List<ProdutoModel>();

            while (rd.Read())
            {
                ProdutoModel dto = new ProdutoModel();
                dto.cd_produto = rd[0].ToString();
                dto.nm_marca = rd[1].ToString();
                dto.tipo = rd[2].ToString();
                dto.descricao = rd[3].ToString();
                dto.aspecto = rd[4].ToString();
                dto.vl_preco_unitario = rd[5].ToString();
                dto.garantia = rd[6].ToString();
                dto.caminho_imagem = rd[7].ToString();

                listaP.Add(dto);
            }

            rd.Close();
            IEnumerable<ProdutoModel> lista = listaP;
            return lista;
        }

        public string getUltimoCadastrado()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbProduto ORDER BY cd_produto DESC LIMIT 1", con.conectarBD());
            MySqlDataReader rd = cmd.ExecuteReader();

            string codigo = string.Empty;
            while (rd.Read())
            {
                codigo = rd[0].ToString();
            }

            rd.Close();

            return codigo;
        }

        public string getQuantidadeEstoque(string cd_produto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_consultarQtdEstoque", con.conectarBD());
            cmd.Parameters.Add("$cd_produto", MySqlDbType.VarChar).Value = cd_produto;

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
    
        public List<ProdutoModel> pesquisarTopPromo()
        {

            MySqlCommand cmd = new MySqlCommand("pa_consultarTopPromo", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure; cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader rd = cmd.ExecuteReader();
            List<ProdutoModel> listaP = new List<ProdutoModel>();

            while (rd.Read())
            {
                ProdutoModel dto = new ProdutoModel();
                dto.cd_produto = rd[0].ToString();
                dto.nm_marca = rd[1].ToString();
                dto.tipo = rd[2].ToString();
                dto.descricao = rd[3].ToString();
                dto.aspecto = rd[4].ToString();
                dto.vl_preco_unitario = rd[5].ToString();
                dto.garantia = rd[6].ToString();
                dto.caminho_imagem = rd[7].ToString();

                listaP.Add(dto);
            }

            rd.Close();

            return listaP;
        }


        public DataTable consultaprodutosMaisVendidos()
        {
            MySqlCommand cmd = new MySqlCommand("pa_produtosMaisVendidos", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure; 

            MySqlDataAdapter rd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable(); 
            rd.Fill(dt);

            con.desconectarBD();
            return dt;
        }

        public DataTable consultaprodutosMenosVendidos()
        {
            MySqlCommand cmd = new MySqlCommand("pa_produtosMenosVendidos", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataAdapter rd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            rd.Fill(dt);

            con.desconectarBD();
            return dt;
        }

        public IEnumerable<ProdutoModel> consultaprodutosForaEstoque()
        {
            MySqlCommand cmd = new MySqlCommand("pa_produtosForaEstoque", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader rd = cmd.ExecuteReader();

            List<ProdutoModel> listaProduto = new List<ProdutoModel>();

            while (rd.Read())
            {

                ProdutoModel dto = new ProdutoModel();
                dto.cd_produto = rd[0].ToString();
                dto.nm_marca = rd[1].ToString();

                listaProduto.Add(dto);
            }

            rd.Close();
            IEnumerable<ProdutoModel> lista = listaProduto;
            return lista;

        }



    }

}

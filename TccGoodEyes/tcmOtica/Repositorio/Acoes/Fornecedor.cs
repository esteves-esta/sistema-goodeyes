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
    public class Fornecedor
    {
        Conexao con = new Conexao();

        public void inserirFornecedor(FornecedorModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_inserirFornecedor", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$nome", dto.nome);
            cmd.Parameters.AddWithValue("$sobrenome", dto.sobrenome);
            cmd.Parameters.AddWithValue("$email", dto.email);
            cmd.Parameters.AddWithValue("$no_tel", dto.no_tel);
            cmd.Parameters.AddWithValue("$no_cnpj", dto.no_cnpj);
   
            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }

        //-------------------------------------------------------------------------------------------------------------------------------

        public List<string> listaCNPJ()
        {
            MySqlCommand cmd = new MySqlCommand("Select * from tbFornecedor", con.conectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.desconectarBD();


            List<string> list = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["no_cnpj"].ToString());
            }
            return list;
        }

        //-------------------------------------------------------------------------------------------------------------------------------

        public void deletarFornecedor(string cd_fornecedor)
        {
            MySqlCommand cmd = new MySqlCommand("pa_deleteFornecedor", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_fornecedor", MySqlDbType.VarChar).Value = cd_fornecedor;

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void atualizaForne(FornecedorModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_alterarFornecedor", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_fornecedor", dto.cd_fornecedor);
            cmd.Parameters.AddWithValue("$nome", dto.nome);
            cmd.Parameters.AddWithValue("$sobrenome", dto.sobrenome);
            cmd.Parameters.AddWithValue("$email", dto.email);
            cmd.Parameters.AddWithValue("$no_tel", dto.no_tel);
            cmd.Parameters.AddWithValue("$no_cnpj", dto.no_cnpj);

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //---------------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<FornecedorModel> consultaForne()
        {
            MySqlCommand cmd = new MySqlCommand("pa_exibirFornecedor", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<FornecedorModel> listaForne = new List<FornecedorModel>();

            while (rd.Read())
            {

                FornecedorModel dto = new FornecedorModel();

                dto.cd_fornecedor = rd[0].ToString();
                dto.nome = rd[1].ToString();
                dto.sobrenome = rd[2].ToString();
                dto.email = rd[3].ToString();
                dto.no_tel = rd[4].ToString();
                dto.no_cnpj = rd[5].ToString();

                listaForne.Add(dto);
            }
            rd.Close();

            IEnumerable<FornecedorModel> lista = listaForne;
            return lista;


        }


        //-------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<FornecedorModel> pesquisaForn(FornecedorModel dtoX)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarFornecedor", con.conectarBD());
            
            cmd.Parameters.Add("$no_cnpj", MySqlDbType.VarChar).Value = dtoX.no_cnpj;
            cmd.Parameters.Add("$nm_marca", MySqlDbType.VarChar).Value = dtoX.nm_marca;
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<FornecedorModel> listaForne = new List<FornecedorModel>();
          
            while (rd.Read())
            {
                FornecedorModel dto = new FornecedorModel();

                dto.cd_fornecedor = rd[0].ToString();
                dto.nome = rd[1].ToString();
                dto.sobrenome = rd[2].ToString();
                dto.email = rd[3].ToString();
                dto.no_tel = rd[4].ToString();
                dto.no_cnpj = rd[5].ToString();

                listaForne.Add(dto);
            }

            rd.Close();
            IEnumerable<FornecedorModel> lista = listaForne;
            return lista;
        }

        //-------------------------------------------------------------------------------------------------------------------------


        public string getUltimoCadastrado()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbFornecedor ORDER BY cd_fornecedor DESC LIMIT 1", con.conectarBD());
            MySqlDataReader rd = cmd.ExecuteReader();

            string codigo = string.Empty;
            while (rd.Read())
            {
                codigo = rd[0].ToString();
            }

            rd.Close();

            return codigo;
        }
        //-----------------------------------------------------------------------------------------------------------
        public FornecedorModel pesquisaFornCod(string cd_fornecedor)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarFornecedorCod", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("$cd_fornecedor", MySqlDbType.VarChar).Value = cd_fornecedor;

            MySqlDataReader rd = cmd.ExecuteReader();
            

            FornecedorModel dto = new FornecedorModel();

            while (rd.Read())
            {
                dto.cd_fornecedor = rd[0].ToString();
                dto.nome = rd[1].ToString();
                dto.sobrenome = rd[2].ToString();
                dto.email = rd[3].ToString();
                dto.no_tel = rd[4].ToString();
                dto.no_cnpj = rd[5].ToString();
            }

            rd.Close();
         
            return dto;
        }


        //-------------

        public List<FornecedorModel> listarFornecedores()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbFornecedor", con.conectarBD());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.desconectarBD();

            List<FornecedorModel> list = new List<FornecedorModel>();

            foreach (DataRow rd in dt.Rows)
            {
                FornecedorModel dto = new FornecedorModel();
                dto.cd_fornecedor = rd[0].ToString();
                dto.nome = rd[1].ToString();
                dto.sobrenome = rd[2].ToString();
                list.Add(dto);
            }
            return list;
        }
    }
}
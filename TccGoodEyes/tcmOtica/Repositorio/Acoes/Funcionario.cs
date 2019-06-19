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
    public class Funcionario
    {

        Conexao con = new Conexao();

        public void inserirFuncionario(FuncionarioModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_inserirFuncionario", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$nome", dto.nome);
            cmd.Parameters.AddWithValue("$cd_login", dto.cd_login);
            cmd.Parameters.AddWithValue("$sobrenome", dto.sobrenome);
            cmd.Parameters.AddWithValue("$no_cpf", dto.no_cpf);
            cmd.Parameters.AddWithValue("$no_tel", dto.no_tel);
            cmd.Parameters.AddWithValue("$no_cel", dto.no_cel);
            cmd.Parameters.AddWithValue("$email", dto.email.ToLower());
            cmd.Parameters.AddWithValue("$dt_nascimento", dto.dt_nascimento);
            cmd.Parameters.AddWithValue("$nm_rua", dto.nm_rua);
            cmd.Parameters.AddWithValue("$no_rua", dto.no_rua);
            cmd.Parameters.AddWithValue("$no_cep", dto.no_cep);
            cmd.Parameters.AddWithValue("$bairro", dto.bairro);
            cmd.Parameters.AddWithValue("$cidade", dto.cidade);
            cmd.Parameters.AddWithValue("$sg_uf", dto.sg_uf);
            cmd.Parameters.AddWithValue("$estado", dto.estado);
            cmd.Parameters.AddWithValue("$cargo", dto.cargo);
            cmd.Parameters.AddWithValue("$complemento", dto.complemento);


            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }

        //-------------------------------------------------------------------------------------------------------------------------------

        public void deletarFuncionario(string cd_funcionario)
        {
            MySqlCommand cmd = new MySqlCommand("pa_deleteFuncionario", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_login", MySqlDbType.VarChar).Value = cd_funcionario;

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void atualizarFuncionario(FuncionarioModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_alterarFuncionario", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_funcionario", MySqlDbType.VarChar).Value = dto.cd_funcionario;
            cmd.Parameters.AddWithValue("$nome", dto.nome);
            cmd.Parameters.AddWithValue("$sobrenome", dto.sobrenome);
            cmd.Parameters.AddWithValue("$no_cpf", dto.no_cpf);
            cmd.Parameters.AddWithValue("$no_tel", dto.no_tel);
            cmd.Parameters.AddWithValue("$no_cel", dto.no_cel);
            cmd.Parameters.AddWithValue("$email", dto.email.ToLower());
            cmd.Parameters.AddWithValue("$dt_nascimento", dto.dt_nascimento);
            cmd.Parameters.AddWithValue("$nm_rua", dto.nm_rua);
            cmd.Parameters.AddWithValue("$no_rua", dto.no_rua);
            cmd.Parameters.AddWithValue("$no_cep", dto.no_cep);
            cmd.Parameters.AddWithValue("$bairro", dto.bairro);
            cmd.Parameters.AddWithValue("$cidade", dto.cidade);
            cmd.Parameters.AddWithValue("$sg_uf", dto.sg_uf);
            cmd.Parameters.AddWithValue("$estado", dto.estado);
            cmd.Parameters.AddWithValue("$cargo", dto.cargo);
            cmd.Parameters.AddWithValue("$complemento", dto.complemento);

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //---------------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<FuncionarioModel> consultaFuncionario()
        {
            MySqlCommand cmd = new MySqlCommand("pa_exibirFuncionario", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<FuncionarioModel> listaFuncionario = new List<FuncionarioModel>();

            while (rd.Read())
            {

                FuncionarioModel dto = new FuncionarioModel();
                dto.cd_funcionario = rd[0].ToString();
                dto.cd_login = rd[1].ToString();
                dto.email = rd[2].ToString();
                dto.nome = rd[3].ToString();
                dto.sobrenome = rd[4].ToString();
                dto.no_cpf = rd[5].ToString();
                dto.cargo = rd[6].ToString();
                dto.no_tel = rd[7].ToString();
                dto.no_cel = rd[8].ToString();
                dto.dt_nascimento= rd[9].ToString();
                dto.nm_rua = rd[10].ToString();
                dto.no_rua = rd[11].ToString();
                dto.no_cep = rd[12].ToString();
                dto.bairro = rd[13].ToString();
                dto.cidade = rd[14].ToString();
                dto.estado = rd[15].ToString();
                dto.sg_uf = rd[16].ToString();
                dto.complemento = rd[17].ToString();


                listaFuncionario.Add(dto);
            }

            rd.Close();
            IEnumerable<FuncionarioModel> lista = listaFuncionario;
            return lista;
        }


        public string pesquisarFuncionarioPorEmail(string email)
        {

            string nome = string.Empty;

            MySqlCommand cmd = new MySqlCommand("select * from tbFuncionario where email=@email", con.conectarBD());
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                nome = dr[3].ToString() + " " + dr[4].ToString();
            }
            dr.Close();
            return nome;

        }

        //--------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<FuncionarioModel> pesquisarFuncionario (FuncionarioModel dtoX)
        { 
            List<FuncionarioModel> listaFuncionario = new List<FuncionarioModel>();
        
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarFuncionario", con.conectarBD());
            cmd.Parameters.Add("$nome", MySqlDbType.VarChar).Value = dtoX.nome;
            cmd.Parameters.Add("$sobrenome", MySqlDbType.VarChar).Value = dtoX.sobrenome;
            cmd.Parameters.Add("$no_cpf", MySqlDbType.VarChar).Value = dtoX.no_cpf;
            cmd.Parameters.Add("$cargo", MySqlDbType.VarChar).Value = dtoX.cargo;
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

         
            while (rd.Read())
            {

                FuncionarioModel dto = new FuncionarioModel();
                dto.cd_funcionario = rd[0].ToString();
                dto.cd_login = rd[1].ToString();
                dto.email = rd[2].ToString();
                dto.nome = rd[3].ToString();
                dto.sobrenome = rd[4].ToString();
                dto.no_cpf = rd[5].ToString();
                dto.cargo = rd[6].ToString();
                dto.no_tel = rd[7].ToString();
                dto.no_cel = rd[8].ToString();
                dto.dt_nascimento = rd[9].ToString();
                dto.nm_rua = rd[10].ToString();
                dto.no_rua = rd[11].ToString();
                dto.no_cep = rd[12].ToString();
                dto.bairro = rd[13].ToString();
                dto.cidade = rd[14].ToString();
                dto.estado = rd[15].ToString();
                dto.sg_uf = rd[16].ToString();
                dto.complemento = rd[17].ToString();

                listaFuncionario.Add(dto);
            }

     
            rd.Close();

            IEnumerable<FuncionarioModel> lista = listaFuncionario;

            return lista;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public FuncionarioModel pesquisarFunc(FuncionarioModel dtoX)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarFuncionarioCod", con.conectarBD());
            cmd.Parameters.Add("$cd_funcionario", MySqlDbType.VarChar).Value = dtoX.cd_funcionario;
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            FuncionarioModel dto = new FuncionarioModel();

            while (rd.Read())
            {
                dto.cd_funcionario = rd[0].ToString();
                dto.cd_login = rd[1].ToString();
                dto.email = rd[2].ToString();
                dto.nome = rd[3].ToString();
                dto.sobrenome = rd[4].ToString();
                dto.no_cpf = rd[5].ToString();
                dto.cargo = rd[6].ToString();
                dto.no_tel = rd[7].ToString();
                dto.no_cel = rd[8].ToString();
                dto.dt_nascimento = rd[9].ToString();
                dto.nm_rua = rd[10].ToString();
                dto.no_rua = rd[11].ToString();
                dto.no_cep = rd[12].ToString();
                dto.bairro = rd[13].ToString();
                dto.cidade = rd[14].ToString();
                dto.estado = rd[15].ToString();
                dto.sg_uf = rd[16].ToString();
                dto.complemento = rd[17].ToString();
            }


            rd.Close();

            return dto;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public List<string> listaNomeFuncionario()
        {
            MySqlCommand cmd = new MySqlCommand("Select * from tbFuncionario", con.conectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.desconectarBD();


            List<string> list = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["nome"].ToString() + ' ' + row["sobrenome"].ToString());
            }
            return list;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public List<string> listaCPFFuncionario()
        {
            MySqlCommand cmd = new MySqlCommand("Select * from tbFuncionario", con.conectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.desconectarBD();


            List<string> list = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["no_cpf"].ToString());
            }
            return list;
        }
       

    }
}
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
    public class Cliente
    {
        Conexao con = new Conexao();

        public void inserirCliente(ClienteModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_inserirCliente", con.conectarBD());
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
            cmd.Parameters.AddWithValue("$no_cep", dto.no_cep) ;
            cmd.Parameters.AddWithValue("$bairro", dto.bairro) ;
            cmd.Parameters.AddWithValue("$cidade", dto.cidade);
            cmd.Parameters.AddWithValue("$sg_uf", dto.sg_uf);
            cmd.Parameters.AddWithValue("$estado", dto.estado);
            cmd.Parameters.AddWithValue("$complemento", dto.complemento);


            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }

        //-------------------------------------------------------------------------------------------------------------------------------

        public void deletarCliente(ClienteModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_deleteCliente", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_cliente", MySqlDbType.VarChar).Value = dto.cd_cliente;

            cmd.ExecuteNonQuery();
            con.desconectarBD();
        }
        //-------------------------------------------------------------------------------------------------------------------
        public void atualizarCliente(ClienteModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_alterarCliente", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_cliente", dto.cd_cliente);
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
            cmd.Parameters.AddWithValue("$complemento", dto.complemento);

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //---------------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<ClienteModel> consultaCliente()
        {
            MySqlCommand cmd = new MySqlCommand("pa_exibirCliente", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<ClienteModel> listaCliente = new List<ClienteModel>();

            while (rd.Read())
            {

                ClienteModel dto = new ClienteModel();
                dto.cd_cliente = rd[0].ToString();
                dto.email = rd[1].ToString();
                dto.nome = rd[2].ToString();
                dto.sobrenome = rd[3].ToString();
                dto.no_cpf = rd[4].ToString();
                dto.no_tel = rd[5].ToString();
                dto.no_cel = rd[6].ToString();
                dto.dt_nascimento = rd[7].ToString();
                dto.nm_rua = rd[8].ToString();
                dto.no_rua = rd[9].ToString();
                dto.no_cep = rd[10].ToString();
                dto.bairro = rd[11].ToString();
                dto.cidade = rd[12].ToString();
                dto.estado = rd[13].ToString();
                dto.sg_uf = rd[14].ToString();
                dto.complemento = rd[15].ToString();

                listaCliente.Add(dto);
            }
            rd.Close();
            IEnumerable<ClienteModel> lista = listaCliente;
            return lista;
        }

        //***************
        public ClienteModel pesquisarClienteporEmail(string email)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbCliente where email=@email", con.conectarBD());
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;

            MySqlDataReader rd = cmd.ExecuteReader();

            ClienteModel dto = new ClienteModel();
            while (rd.Read())
            {
                dto.cd_cliente = rd[0].ToString();
                dto.cd_login = rd[1].ToString();
                dto.email = rd[2].ToString();
                dto.nome = rd[3].ToString();
                dto.sobrenome = rd[4].ToString();
                dto.no_cpf = rd[5].ToString();
                dto.no_tel = rd[6].ToString();
                dto.no_cel = rd[7].ToString();
                dto.dt_nascimento = rd[8].ToString();
                dto.nm_rua = rd[9].ToString();
                dto.no_rua = rd[10].ToString();
                dto.no_cep = rd[11].ToString();
                dto.bairro = rd[12].ToString();
                dto.cidade = rd[13].ToString();
                dto.estado = rd[14].ToString();
                dto.sg_uf = rd[15].ToString();
                dto.complemento = rd[16].ToString();
            }

            rd.Close();

            return dto;
        }

        //--------------------------------------------------------------------------------------------------------------------


        public ClienteModel pesquisarCliente(ClienteModel cli)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarCliente", con.conectarBD());
            cmd.Parameters.Add("$cd_cliente", MySqlDbType.VarChar).Value = cli.cd_cliente;
            cmd.Parameters.Add("$nome", MySqlDbType.VarChar).Value = cli.nome;
            cmd.Parameters.Add("$no_cpf", MySqlDbType.VarChar).Value = cli.no_cpf;
            cmd.Parameters.Add("$sobrenome", MySqlDbType.VarChar).Value = cli.sobrenome;

            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            ClienteModel dto = new ClienteModel();

            while (rd.Read())
            {
                dto.cd_cliente = rd[0].ToString();
                dto.cd_login = rd[1].ToString();
                dto.email = rd[2].ToString();
                dto.nome = rd[3].ToString();
                dto.sobrenome = rd[4].ToString();
                dto.no_cpf = rd[5].ToString();
                dto.no_tel = rd[6].ToString();
                dto.no_cel = rd[7].ToString();
                dto.dt_nascimento = rd[8].ToString();
                dto.nm_rua = rd[9].ToString();
                dto.no_rua = rd[10].ToString();
                dto.no_cep = rd[11].ToString();
                dto.bairro = rd[12].ToString();
                dto.cidade = rd[13].ToString();
                dto.estado = rd[14].ToString();
                dto.sg_uf = rd[15].ToString();
                dto.complemento = rd[16].ToString();
            }

            rd.Close();

            return dto;
        }

        public IEnumerable<ClienteModel> pesquisarClienteLista(ClienteModel cli)
        {
            MySqlCommand cmd = new MySqlCommand("pa_pesquisarCliente", con.conectarBD());
            cmd.Parameters.Add("$cd_cliente", MySqlDbType.VarChar).Value = cli.cd_cliente;
            cmd.Parameters.Add("$nome", MySqlDbType.VarChar).Value = cli.nome;
            cmd.Parameters.Add("$no_cpf", MySqlDbType.VarChar).Value = cli.no_cpf;
            cmd.Parameters.Add("$sobrenome", MySqlDbType.VarChar).Value = cli.sobrenome;

            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<ClienteModel> listaCliente = new List<ClienteModel>();

            while (rd.Read())
            {

                ClienteModel dto = new ClienteModel();
                dto.cd_cliente = rd[0].ToString();
                dto.cd_login = rd[1].ToString();
                dto.email = rd[2].ToString();
                dto.nome = rd[3].ToString();
                dto.sobrenome = rd[4].ToString();
                dto.no_cpf = rd[5].ToString();
                dto.no_tel = rd[6].ToString();
                dto.no_cel = rd[7].ToString();
                dto.dt_nascimento = rd[8].ToString();
                dto.nm_rua = rd[9].ToString();
                dto.no_rua = rd[10].ToString();
                dto.no_cep = rd[11].ToString();
                dto.bairro = rd[12].ToString();
                dto.cidade = rd[13].ToString();
                dto.estado = rd[14].ToString();
                dto.sg_uf = rd[15].ToString();
                dto.complemento = rd[16].ToString();

                listaCliente.Add(dto);
            }
            rd.Close();
            IEnumerable<ClienteModel> lista = listaCliente;
            return lista;


        }
        //--------------------------------------------------------------------------------------------------------------------

        public List<string> listaNomeCliente()
        {
            MySqlCommand cmd = new MySqlCommand("Select * from tbCliente", con.conectarBD());
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
        //--------------------------------------------------------------------------------------------------------------------

        public List<string> listaCPFCliente()
        {
            MySqlCommand cmd = new MySqlCommand("Select * from tbCliente", con.conectarBD());
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
        //--------------------------------------------------------------------------------------------------------------------

        public bool verificaEmail(string email)
        {


            MySqlCommand cmd = new MySqlCommand("pa_verificaEmail", con.conectarBD());
            cmd.Parameters.Add("$email", MySqlDbType.VarChar).Value = email;
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

    }
}

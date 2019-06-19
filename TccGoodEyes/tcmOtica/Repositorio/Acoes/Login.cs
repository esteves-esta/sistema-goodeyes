using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using tccGoodEyes.Models;
using tccGoodEyes.Repositorio;

namespace tccGoodEyes.Repositorio.Acoes
{
    public class Login

    {
        Conexao con = new Conexao();

        public void inserirLogin(LoginModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_inserirLogin", con.conectarBD());


            cmd.Parameters.AddWithValue("$email", dto.email.ToLower());
            cmd.Parameters.AddWithValue("$senha", dto.senha);
            cmd.Parameters.AddWithValue("$nivel_acesso", Convert.ToInt32(1));

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //-------------------------------------------------------------------------------------------------------------------------------

        public void deletarLogin(LoginModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_deleteLogin", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_login", MySqlDbType.VarChar).Value = dto.cd_login;

            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }
        //------------------------------------------------------------------------------------------------------------------------------
        public void atualizarLogin(LoginModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_alterarLogin", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("$cd_login", dto.cd_login);
            cmd.Parameters.AddWithValue("$senha", dto.senha);
            cmd.Parameters.AddWithValue("$nivel_acesso", dto.nivel_acesso);
            cmd.Parameters.AddWithValue("$email", dto.email);


            cmd.ExecuteNonQuery();
            con.desconectarBD();

        }

        //-------------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<LoginModel> consultarLogin()
        {
            MySqlCommand cmd = new MySqlCommand("pa_exibirLogin", con.conectarBD());
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader rd = cmd.ExecuteReader();

            List<LoginModel> listaLogin = new List<LoginModel>();

            while (rd.Read())
            {

                LoginModel dto = new LoginModel();
                dto.cd_login = rd[0].ToString();
                dto.email = rd[1].ToString();
                dto.senha = rd[2].ToString();
                dto.nivel_acesso = rd[3].ToString();

                listaLogin.Add(dto);
            }
            rd.Close();
            IEnumerable<LoginModel> lista = listaLogin;
            return lista;
        }


        public bool autenticaUsu(LoginModel dto)
        {
            MySqlCommand cmd = new MySqlCommand("pa_autenticaUsu", con.conectarBD());
            cmd.Parameters.Add("$email", MySqlDbType.VarChar).Value = dto.email;
            cmd.Parameters.Add("$senha", MySqlDbType.VarChar).Value = dto.senha;
            cmd.CommandType = CommandType.StoredProcedure;

            int retorno = Convert.ToInt32(cmd.ExecuteScalar());

            if (retorno == 0)
            {
                return false;
            }
            else
            {
                FormsAuthentication.SetAuthCookie(dto.email, true);

                return true;
            }

        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public string getCodigo(LoginModel dto)
        {

            string cd_login = string.Empty;

            MySqlCommand cmd = new MySqlCommand("select cd_login from tbLogin where email = @email", con.conectarBD());
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = dto.email;
          
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cd_login = dr[0].ToString();
            }
            dr.Close();
            return cd_login;

        }

        public int verificaNivel(LoginModel dto)
        {

            int nivel = 0;

            MySqlCommand cmd = new MySqlCommand("pa_verificaNivel", con.conectarBD());
            cmd.Parameters.Add("$cd_login", MySqlDbType.VarChar).Value = dto.cd_login;
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                nivel = Convert.ToInt32(dr[3]);
            }
            dr.Close();
            return nivel;

        }

        //-------------------------------------------------------------------------------------------------------------------------------


        public bool verificaEmail(LoginModel dto)
        {

            MySqlCommand cmd = new MySqlCommand("pa_verificaEmail", con.conectarBD());
            cmd.Parameters.Add("$email", MySqlDbType.VarChar).Value = dto.email;
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

        //-------------------------------------------------------------------------------------------------------------------------------

        public void deslogar()
        {

            FormsAuthentication.SignOut();
        }

    }
}


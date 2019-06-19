using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tccGoodEyes.Models;

namespace tccGoodEyes.Repositorio

{
    public class Conexao
    {

        //  MySqlConnection con = new MySqlConnection("Server=localhost;Database=bd_goodeyes2;user=root;pwd=1234567; SslMode=none");

        MySqlConnection con = new MySqlConnection("server=localhost; User Id=root; database=bd_goodeyes2; password=1234; SslMode=none");

        public MySqlConnection conectarBD()
        {
            try
            {
            con.Open();
            }
            catch (Exception e)
            {
               
            }
            return con;
        }


        public MySqlConnection desconectarBD()
        {
            try
            {
                con.Close();
            }
            catch (Exception e)
            {
                // MessageBox.Show("Erro na Conexão");
            }
            return con;
        }

    }
}

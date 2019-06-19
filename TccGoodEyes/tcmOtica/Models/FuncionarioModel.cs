using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tccGoodEyes.Models {
    public class FuncionarioModel {
        public string cd_funcionario { get; set; }
        public string cd_login { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string no_cpf { get; set; }
        public string email { get; set; }
        public string cargo { get; set; }
        public string no_tel { get; set; }
        public string no_cel { get; set; }
        public string dt_nascimento { get; set; }
        public string nm_rua { get; set; }
        public string no_rua { get; set; }
        public string no_cep { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string sg_uf { get; set; }
        public string complemento { get; set; }
    }
}
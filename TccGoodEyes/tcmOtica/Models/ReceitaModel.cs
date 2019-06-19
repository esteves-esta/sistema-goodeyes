using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tccGoodEyes.Models {
    public class ReceitaModel {

        public string cd_cliente { get; set; }
        public string cd_pedido { get; set; }
        public string cd_receita { get; set; }
        public string olho_direito { get; set; }
        public string olho_esquerdo { get; set; }
        public string distancia_pupilar { get; set; }
        public string nm_oftalmo { get; set; }
        public string sobrenome_oftalmo { get; set; }
        public string dt_receita { get; set; }
        public string dt_validade { get; set; }
        public string observacao { get; set; }
        public string cd_produto { get; set; }

    }

}
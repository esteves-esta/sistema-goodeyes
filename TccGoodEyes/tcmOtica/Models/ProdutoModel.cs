using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tccGoodEyes.Models {
    public class ProdutoModel {

        
        public string cd_produto { get; set; }
        public string nm_marca { get; set; }
        public string tipo { get; set; }
        public string descricao { get; set; }
        public string aspecto { get; set; }

        public string cd_carrinho { get; set; }
        public string cd_pedido { get; set; }
        public string qt_item { get; set; }
        public string vl_subtotal { get; set; }

        public string vl_preco_unitario { get; set; }
        public string garantia { get; set; }
        public string caminho_imagem { get; set; }

        public string qt_estoque { get; set; }
        public string cd_receita { get; set; }

    }


}
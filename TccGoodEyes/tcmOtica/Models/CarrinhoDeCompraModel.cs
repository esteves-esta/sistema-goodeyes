using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tccGoodEyes.Models {
    public class CarrinhoDeCompraModel {
     
        public string cd_carrinho { get; set; }
        public string cd_produto { get; set; }
        public string cd_cliente { get; set; }
        public string qt_item { get; set; }
    }
}
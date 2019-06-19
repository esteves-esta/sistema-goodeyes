using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tccGoodEyes.Models {
    public class PedidoItemModel {

        public string cd_item { get; set; }
        public string cd_pedido { get; set; }
        public string cd_produto { get; set; }
        public string qt_item { get; set; }
        public string vl_subtotal { get; set; }
    }
}
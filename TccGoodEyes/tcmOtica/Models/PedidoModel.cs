using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tccGoodEyes.Models {
    public class PedidoModel {
        public string cd_pedido { get; set; }
        public string cd_cliente { get; set; }
        public string dt_pedido { get; set; }
        public string qt_item { get; set; }
        public string vl_total { get; set; }
        public string status_pedido { get; set; }
    }
}
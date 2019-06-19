using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tccGoodEyes.Models {
    public class PagamentoModel {


        public string cd_pagamento { get; set; }
        public string cd_pedido { get; set; }
        public string tipo_pagamento { get; set; }
        public string parcelamento { get; set; }
        public string vl_total { get; set; }
       
    }
}
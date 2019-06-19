using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tccGoodEyes.Models
{
    public class VendasModel
    {

        public String cd_pedido { get; set; }

        public String cd_cliente { get; set; }

        public String nm_cliente { get; set; }

        public String cliente_sobrenome { get; set; }

        public String dt_pedido { get; set; }

        public String parcelamento { get; set; }

        public String vl_total { get; set; }

        public string status_pedido { get; set; }
    }
}
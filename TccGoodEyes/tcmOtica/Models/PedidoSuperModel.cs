using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tccGoodEyes.Models;

namespace tccGoodEyes.Models
{
    public class PedidoSuperModel
    {


        public ClienteModel clienteModel { get; set; }
        public PagamentoModel pagamentoModel { get; set; }

        public IEnumerable<PedidoModel> pedidoModelLista;
        public IEnumerable<ProdutoModel> produtoModelLista;
        public IEnumerable<ReceitaModel> receitaModelLista;
        public IEnumerable<PagamentoModel> pagamentoModelLista { get; set; }
    }
}
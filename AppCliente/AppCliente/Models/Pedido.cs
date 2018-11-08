using System;
using System.Collections.Generic;
using System.Text;

namespace AppCliente.Models
{
    public class Pedido
    {
        public string KeyPedido { get;  set; }
        public string Cliente { get;  set; }
        public decimal Preco { get;  set; }
        public string Produto { get;  set; }
        public int IdVendedor { get;  set; }

        public Pedido() { }
    }
}

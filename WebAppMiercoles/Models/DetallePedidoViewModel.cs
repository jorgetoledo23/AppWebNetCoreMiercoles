using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMiercoles.Models
{
    public class DetallePedidoViewModel
    {
        public Pedido Pedido { get; set; }

        public List<DetallePedido> ListaDetalle { get; set; }
    }
}

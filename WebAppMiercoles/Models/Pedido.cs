using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMiercoles.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }

        public DateTime Fecha { get; set; }

        public Cliente Cliente { get; set; }

        public int Total { get; set; }

        public List<DetallePedido> ListaDetallePedido { get; set; }

    }
}

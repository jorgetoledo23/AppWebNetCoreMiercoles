using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMiercoles.Models
{
    public class DetallePedido
    {
        public int DetallePedidoId { get; set; }

        public int ProductoId { get; set; }

        public Producto Producto { get; set; }

        public int PedidoId { get; set; }

        public Pedido Pedido { get; set; }

        public int Cantidad { get; set; }



    }
}

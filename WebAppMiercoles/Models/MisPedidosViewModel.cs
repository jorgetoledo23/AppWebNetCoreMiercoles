using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMiercoles.Models
{
    public class MisPedidosViewModel
    {
        public Cliente Cliente { get; set; }
        public List<Pedido> ListaPedidos { get; set; }
    }
}

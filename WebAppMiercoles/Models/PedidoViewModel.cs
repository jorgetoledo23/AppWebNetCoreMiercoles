using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMiercoles.Models
{
    public class PedidoViewModel
    {
        public Cliente Cliente { get; set; }

        public List<ItemCarro> ItemsCarro { get; set; }

        public double Total { get; set; }
    }
}

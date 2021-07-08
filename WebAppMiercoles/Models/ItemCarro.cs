using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMiercoles.Models
{
    public class ItemCarro
    {
        public int ItemCarroId { get; set; }

        public int Cantidad { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public string CarroCompraId { get; set; }

    }
}

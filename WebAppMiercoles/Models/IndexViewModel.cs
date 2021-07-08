using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMiercoles.Models
{
    public class IndexViewModel
    {
        public List<Categoria> ListaCategorias { get; set; }

        public int CantidadPaginas { get; set; }

        public int Pagina { get; set; }
    }
}

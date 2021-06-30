using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMiercoles.Models
{
    public class Producto
    {

        public int ProductoId { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int Precio { get; set; }

        public int Stock { get; set; }

        [Display(Name = "Oferta?")]
        public Boolean enOferta { get; set; }

        [Display(Name = "Precio Oferta")]
        public int precioOferta { get; set; }

        public string Imagen { get; set; }

        [NotMapped]
        [Display(Name = "Imagen")]
        public IFormFile imagenFile { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }


    }
}

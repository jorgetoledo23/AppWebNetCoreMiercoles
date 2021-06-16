using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMiercoles.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Imagen { get; set; }

        [NotMapped]
        [DisplayName("Imagen")]
        public IFormFile ImagenFile { get; set; }

    }
}

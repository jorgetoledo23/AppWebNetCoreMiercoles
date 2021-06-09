﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    }
}

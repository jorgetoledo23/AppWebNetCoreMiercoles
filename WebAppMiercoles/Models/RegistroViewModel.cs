using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMiercoles.Models
{
    public class RegistroViewModel
    {
        [Required]
        public string Rut { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Pass { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public string Ciudad { get; set; }

        [Required]
        public string CodigoPostal { get; set; }

        [Required(ErrorMessage = "Telefono es Obligatorio")]
        public string Telefono { get; set; }


    }
}

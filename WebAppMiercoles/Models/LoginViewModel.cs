using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMiercoles.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email es Obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contraseña es Obligatorio")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }


    }
}

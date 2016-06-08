using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSinAuth.Models
{
    public class CrearEditarUsuarioModel
    {
        [Required]
        public string Correo { get; set; }

        [Required]
        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public DateTime? FechaNacimiento { get; set; }
    }
}

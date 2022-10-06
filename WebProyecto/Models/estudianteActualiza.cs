using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebProyecto.Models
{
    public class estudianteActualiza
    {

    
        [MaxLength(20)]
        public string Nombre { get; set; }

      
        [MaxLength(20)]
        public string primerApellido { get; set; }

       
        [MaxLength(20)]
        public string SegundoApellido { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebProyecto.Models
{
    public class cursoactualiza
    {
      
            [Required]
            [MaxLength(10)]
            public string codigocarrera { get; set; }



            [Required]
            [MaxLength(30)]
            public string nombrecurso { get; set; }

        
    }
}
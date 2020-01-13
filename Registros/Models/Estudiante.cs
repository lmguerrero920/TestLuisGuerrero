using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Registros.Models 
{ 
    public class Estudiante
    {
        [Key]
        public int IdEstudiante { get; set; }

        [Required(ErrorMessage ="Debes ingresar un {0}")]
        [Display(Name="Correo Electronico")]
      //Campo Unico  [Remote("IsTagAvailble", "EstudiantesController", ErrorMessage = "Tag Already Exist.")]

        public string Correo { get; set; }

        [Required(ErrorMessage = "Debes ingresar un {0}")]
        [Display(Name = "Contraseña")]

        public string Clave { get; set; }

        [Required(ErrorMessage = "Debes ingresar un {0}")]
        [Display(Name = "Nombres completos")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debes ingresar un {0}")]
        [Display(Name = "Apellidos Completos")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Debes ingresar un {0}")]
        [Display(Name = "Fecha de nacimiento")]
        [DisplayFormat(DataFormatString ="{0:yyyy/MM/dd}",ApplyFormatInEditMode =true)]
        public DateTime Nacimiento { get; set; }


    }
}
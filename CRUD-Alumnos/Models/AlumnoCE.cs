using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Alumnos.Models
{
    public class AlumnoCE
    {

        //[Required]
        //public int Id { get; set; }
        //[Required]

        //public string Nombres { get; set; }
        //[Required]
        //public string Apellidos { get; set; }
        ////[Required]
        //[Range(1, 99)]
        //public int Edad { get; set; }
        //[Required]
        //public string Sexo { get; set; }
        //public System.DateTime FechaRegistro { get; set; }

    }

    public partial class Alumno
    {
        public int Estado { get; set; }
        [DisplayName("Nombres y Apellidos")]
        public string NombreCompleto { get { return Nombres + " " + Apellidos; } }

    }
}
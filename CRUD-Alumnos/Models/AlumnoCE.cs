using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Alumnos.Models
{
    public class AlumnoCE // Es una clase auxiliar que me permite quitar o agregar campos segun necesite en el conjunto de datos a utilizar.
    {
        public int Id { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required(ErrorMessage ="Este campo es requerido")]
        public string Apellidos { get; set; }
        //[Required]
        [Range(1, 99)]
        public int Edad { get; set; }
        [Required]
        [Display(Name ="Sexo (M/F)")]
        [RegularExpression("[MF]")]
        public string Sexo { get; set; }
        [Display(Name = "Ciudad")]
        public int CodCiudad { get; set; }
        [Display(Name = "Ciudad")]
        public string NombreCiudad { get; set; }

        public string NombreCompleto { get { return Nombres + " " + Apellidos; } } // No hay duplicidad de nombres porque son clases distintas.

        public System.DateTime FechaRegistro { get; set; }
    }

    [MetadataType(typeof(AlumnoCE))]
    public partial class Alumno
    {
        public int Estado { get;}
        [DisplayName("Nombres y Apellidos")]
        public string NombreCompleto { get { return Nombres + " " + Apellidos; } }

        public string NombreCiudad { get; set; }
    }
}

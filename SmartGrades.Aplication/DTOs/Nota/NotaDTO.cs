using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrades.Application.DTOs.Nota
{
    public class NotaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la nota es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El valor de la nota es obligatorio.")]
        [Range(0, 5, ErrorMessage = "El valor de la nota debe estar entre 0 y 5.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "El id del estudiante es obligatorio.")]
        public int IdEstudiante { get; set; }

        [Required(ErrorMessage = "El id del profesor es obligatorio.")]
        public int IdProfesor { get; set; }
    }
}

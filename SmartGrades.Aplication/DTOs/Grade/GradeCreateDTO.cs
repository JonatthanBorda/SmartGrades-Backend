using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrades.Application.DTOs.Nota
{
    public class GradeCreateDTO
    {
        [Required(ErrorMessage = "El nombre de la nota es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "El valor de la nota es obligatorio.")]
        [Range(0, 5, ErrorMessage = "El valor de la nota debe estar entre 0 y 5.")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "El id del estudiante es obligatorio.")]
        public int IdStudent{ get; set; }

        [Required(ErrorMessage = "El id del profesor es obligatorio.")]
        public int IdTeacher { get; set; }
    }
}

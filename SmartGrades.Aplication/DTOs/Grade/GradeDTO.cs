using SmartGrades.Application.DTOs.Estudiante;
using SmartGrades.Application.DTOs.Profesor;
using System.ComponentModel.DataAnnotations;

namespace SmartGrades.Application.DTOs.Nota
{
    public class GradeDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la nota es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "El valor de la nota es obligatorio.")]
        [Range(0, 5, ErrorMessage = "El valor de la nota debe estar entre 0 y 5.")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "El estudiante es obligatorio.")]
        public StudentDTO Student { get; set; }

        [Required(ErrorMessage = "El profesor es obligatorio.")]
        public TeacherDTO Teacher { get; set; }
    }
}

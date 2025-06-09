using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrades.Domain.Entities
{
    public class Nota
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public decimal Valor { get; set; }

        //Llaves foráneas a Estudiante y Profesor:
        public int IdEstudiante { get; set; }
        public required Estudiante Estudiante { get; set; }

        public int IdProfesor { get; set; }
        public required Profesor Profesor { get; set; }
    }
}

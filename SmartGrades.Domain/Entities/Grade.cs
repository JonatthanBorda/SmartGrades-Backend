using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrades.Domain.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Value { get; set; }

        //Llaves foráneas a Estudiante y Profesor:
        public int IdStudent { get; set; }
        public required Student Student { get; set; }

        public int IdTeacher { get; set; }
        public required Teacher Teacher { get; set; }
    }
}

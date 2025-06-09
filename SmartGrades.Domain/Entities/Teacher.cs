using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrades.Domain.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        //Relación: 1:N Un profesor puede tener muchas notas.
        public ICollection<Grade>? Grades { get; set; }
    }
}

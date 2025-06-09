using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrades.Domain.Entities
{
    public class Profesor
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

        //Relación: 1:N Un profesor puede tener muchas notas.
        public ICollection<Nota>? Notas { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrades.Domain.Entities
{
    public class Estudiante
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

        //Relación: 1:N Un estudiante puede tener muchas notas.
        public ICollection<Nota>? Notas { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrades.Application.DTOs.Grade
{
    public record GradeDTOs(
        int Id,
        string Name,
        decimal Value,
        StudentDTOs Student,
        TeacherDTOs Teacher
    );

    public record StudentDTOs(
        int Id,
        string Name
    );

    public record TeacherDTOs(
        int Id,
        string Name
    );
}

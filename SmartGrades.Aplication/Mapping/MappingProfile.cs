using AutoMapper;
using SmartGrades.Application.DTOs.Estudiante;
using SmartGrades.Application.DTOs.Nota;
using SmartGrades.Application.DTOs.Profesor;
using SmartGrades.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrades.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Estudiante:
            CreateMap<Estudiante, EstudianteDTO>().ReverseMap();
            CreateMap<Estudiante, EstudianteCreateDTO>().ReverseMap();
            
            //Profesor:
            CreateMap<Profesor, ProfesorDTO>().ReverseMap();
            CreateMap<Profesor, ProfesorCreateDTO>().ReverseMap();

            //Nota:
            CreateMap<Nota, NotaDTO>().ReverseMap();
            CreateMap<Nota, NotaCreateDTO>().ReverseMap();
        }
    }
}

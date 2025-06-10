using AutoMapper;
using SmartGrades.Application.DTOs.Estudiante;
using SmartGrades.Application.DTOs.Grade;
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
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Student, StudentCreateDTO>().ReverseMap();
            
            //Profesor:
            CreateMap<Teacher, TeacherDTO>().ReverseMap();
            CreateMap<Teacher, TeacherCreateDTO>().ReverseMap();

            //Nota:
            CreateMap<Grade, GradeDTO>()
            .ForMember(dest => dest.Student, opt => opt.MapFrom(src => src.Student))
            .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => src.Teacher))
            .ReverseMap();
            CreateMap<GradeCreateDTO, Grade>();
            CreateMap<GradeUpdateDTO, Grade>();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartGrades.Application.DTOs.Nota;
using SmartGrades.Application.Interfaces;
using SmartGrades.Application.Services;
using SmartGrades.Domain.Entities;

namespace SmartGrades.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        private readonly IMapper _mapper;

        public GradesController(IGradeService gradeService, IMapper mapper)
        {
            _gradeService = gradeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var grades = await _gradeService.GetAllAsync();
            var gradesDTO = _mapper.Map<IEnumerable<GradeDTO>>(grades);
            return Ok(gradesDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var grade = await _gradeService.GetByIdAsync(id);
            if (grade == null) return NotFound();
            var gradeDTO = _mapper.Map<GradeDTO>(grade);
            return Ok(gradeDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GradeCreateDTO gradeCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var grade = _mapper.Map<Grade>(gradeCreateDTO);
            await _gradeService.AddAsync(grade);
            var gradeResult = _mapper.Map<GradeDTO>(grade);
            return CreatedAtAction(nameof(Get), new { id = grade.Id }, gradeResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] GradeDTO gradeDTO)
        {
            if (id != gradeDTO.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var grade = _mapper.Map<Grade>(gradeDTO);
            await _gradeService.UpdateAsync(grade);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _gradeService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("ByStudent/{idStudent}")]
        public async Task<IActionResult> GetNotasByEstudiante(int idStudent)
        {
            var grades = await _gradeService.GetNotasByEstudianteAsync(idStudent);
            var dtoGrades = _mapper.Map<IEnumerable<GradeDTO>>(grades);
            return Ok(dtoGrades);
        }

        [HttpGet("ByTeacher/{idTeacher}")]
        public async Task<IActionResult> GetNotasByProfesor(int idTeacher)
        {
            var grades = await _gradeService.GetNotasByProfesorAsync(idTeacher);
            var dtoGrades = _mapper.Map<IEnumerable<GradeDTO>>(grades);
            return Ok(dtoGrades);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter(
        [FromQuery] string? name,
        [FromQuery] int? idStudent,
        [FromQuery] int? idTeacher,
        [FromQuery] string? orderBy = "Id",
        [FromQuery] bool desc = false,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
        {
            var result = await _gradeService.GetFilteredAsync(name, idStudent, idTeacher, orderBy, desc, page, pageSize);
            return Ok(result);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartGrades.Application.DTOs.Profesor;
using SmartGrades.Application.Interfaces;
using SmartGrades.Application.Services;
using SmartGrades.Domain.Entities;

namespace SmartGrades.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherService teacherService, IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _teacherService.GetAllAsync();
            var teachersDTO = _mapper.Map<IEnumerable<TeacherDTO>>(teachers);
            return Ok(teachersDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher == null) return NotFound();
            var teacherDTO = _mapper.Map<TeacherDTO>(teacher);
            return Ok(teacherDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TeacherCreateDTO teacherCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teacher = _mapper.Map<Teacher>(teacherCreateDTO);
            await _teacherService.AddAsync(teacher);
            var teacherResult = _mapper.Map<TeacherDTO>(teacher);
            return CreatedAtAction(nameof(Get), new { id = teacher.Id }, teacherResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TeacherDTO teacherDTO)
        {
            if (id != teacherDTO.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teacher = _mapper.Map<Teacher>(teacherDTO);
            await _teacherService.UpdateAsync(teacher);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _teacherService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter(
        [FromQuery] string? name,
        [FromQuery] string? orderBy = "Id",
        [FromQuery] bool desc = false,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
        {
            var result = await _teacherService.GetFilteredAsync(name, orderBy, desc, page, pageSize);
            return Ok(result);
        }
    }
}

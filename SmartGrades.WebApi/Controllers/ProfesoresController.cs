using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartGrades.Application.DTOs.Profesor;
using SmartGrades.Application.Interfaces;
using SmartGrades.Domain.Entities;

namespace SmartGrades.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesoresController : ControllerBase
    {
        private readonly ITeacherService _profesorService;
        private readonly IMapper _mapper;

        public ProfesoresController(ITeacherService profesorService, IMapper mapper)
        {
            _profesorService = profesorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var profesores = await _profesorService.GetAllAsync();
            var profesoresDTO = _mapper.Map<IEnumerable<TeacherDTO>>(profesores);
            return Ok(profesoresDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var profesor = await _profesorService.GetByIdAsync(id);
            if (profesor == null) return NotFound();
            var profesorDTO = _mapper.Map<TeacherDTO>(profesor);
            return Ok(profesorDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TeacherCreateDTO profesorCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var profesor = _mapper.Map<Teacher>(profesorCreateDTO);
            await _profesorService.AddAsync(profesor);
            var profesorResult = _mapper.Map<TeacherDTO>(profesor);
            return CreatedAtAction(nameof(Get), new { id = profesor.Id }, profesorResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TeacherDTO profesorDTO)
        {
            if (id != profesorDTO.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var profesor = _mapper.Map<Teacher>(profesorDTO);
            await _profesorService.UpdateAsync(profesor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _profesorService.DeleteAsync(id);
            return NoContent();
        }
    }
}

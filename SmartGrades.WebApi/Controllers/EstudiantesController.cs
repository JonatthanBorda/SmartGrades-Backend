using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartGrades.Application.DTOs.Estudiante;
using SmartGrades.Application.Interfaces;
using SmartGrades.Domain.Entities;

namespace SmartGrades.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudianteService _estudianteService;
        private readonly IMapper _mapper;

        public EstudiantesController(IEstudianteService estudianteService, IMapper mapper)
        {
            _estudianteService = estudianteService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var estudiantes = await _estudianteService.GetAllAsync();
            var estudiantesDTO = _mapper.Map<IEnumerable<EstudianteDTO>>(estudiantes);
            return Ok(estudiantesDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var estudiante = await _estudianteService.GetByIdAsync(id);
            if (estudiante == null) return NotFound();
            var estudianteDTO = _mapper.Map<EstudianteDTO>(estudiante);
            return Ok(estudianteDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EstudianteCreateDTO estudianteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var estudiante = _mapper.Map<Estudiante>(estudianteDTO);
            await _estudianteService.AddAsync(estudiante);
            var estudianteResult = _mapper.Map<EstudianteDTO>(estudiante);
            return CreatedAtAction(nameof(Get), new { id = estudiante.Id }, estudianteResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EstudianteDTO estudianteDTO)
        {
            if (id != estudianteDTO.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var estudiante = _mapper.Map<Estudiante>(estudianteDTO);
            await _estudianteService.UpdateAsync(estudiante);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _estudianteService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter(
        [FromQuery] string? nombre,
        [FromQuery] string? orderBy = "Id",
        [FromQuery] bool desc = false,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
        {
            var result = await _estudianteService.GetFilteredAsync(nombre, orderBy, desc, page, pageSize);
            return Ok(result);
        }
    }
}

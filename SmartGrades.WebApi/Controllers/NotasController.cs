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
    public class NotasController : ControllerBase
    {
        private readonly INotaService _notaService;
        private readonly IMapper _mapper;

        public NotasController(INotaService notaService, IMapper mapper)
        {
            _notaService = notaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notas = await _notaService.GetAllAsync();
            var notasDTO = _mapper.Map<IEnumerable<NotaDTO>>(notas);
            return Ok(notasDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var nota = await _notaService.GetByIdAsync(id);
            if (nota == null) return NotFound();
            var notaDTO = _mapper.Map<NotaDTO>(nota);
            return Ok(notaDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NotaCreateDTO notaCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nota = _mapper.Map<Nota>(notaCreateDTO);
            await _notaService.AddAsync(nota);
            var notaResult = _mapper.Map<NotaDTO>(nota);
            return CreatedAtAction(nameof(Get), new { id = nota.Id }, notaResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] NotaDTO notaDTO)
        {
            if (id != notaDTO.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nota = _mapper.Map<Nota>(notaDTO);
            await _notaService.UpdateAsync(nota);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _notaService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("ByEstudiante/{idEstudiante}")]
        public async Task<IActionResult> GetNotasByEstudiante(int idEstudiante)
        {
            var notas = await _notaService.GetNotasByEstudianteAsync(idEstudiante);
            var dtoNotas = _mapper.Map<IEnumerable<NotaDTO>>(notas);
            return Ok(dtoNotas);
        }

        [HttpGet("ByProfesor/{idProfesor}")]
        public async Task<IActionResult> GetNotasByProfesor(int idProfesor)
        {
            var notas = await _notaService.GetNotasByProfesorAsync(idProfesor);
            var dtoNotas = _mapper.Map<IEnumerable<NotaDTO>>(notas);
            return Ok(dtoNotas);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter(
        [FromQuery] string? nombre,
        [FromQuery] int? idEstudiante,
        [FromQuery] int? idProfesor,
        [FromQuery] string? orderBy = "Id",
        [FromQuery] bool desc = false,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
        {
            var result = await _notaService.GetFilteredAsync(nombre, idEstudiante, idProfesor, orderBy, desc, page, pageSize);
            return Ok(result);
        }

    }
}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fortytwo.PracticalTest.Application.Features;
using Fortytwo.PracticalTest.Domain.Entities;
using Fortytwo.PracticalTest.Api.Dto;

namespace Fortytwo.PracticalTest.Api.Controllers
{
    [ApiController]
    [Route("todos")]
    [Authorize]
    public class TodosController(TodoService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Todo>>> GetAll()
        {
            var items = await service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Todo>> GetById([FromRoute] int id, CancellationToken ct)
        {
            var item = await service.GetAsync(id, ct);
            if (item is null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> Create([FromBody] TodoCreateDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var created = await service.CreateAsync(dto.Title, dto.IsCompleted);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}

using API.Application.Services;
using API.Infrastructure.Data;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class CursoController : ControllerBase
    {
        private readonly IReadRepository<Curso> _repository;
        private readonly IReadRepository<Categoria> _repositoryCategoria;
        private readonly ICursoService _service;

        public CursoController(IReadRepository<Curso> repository,IReadRepository<Categoria> repositoryCategoria, ICursoService service)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _repositoryCategoria = repositoryCategoria ?? throw new ArgumentNullException(nameof(repositoryCategoria));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.FindAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_repository.FindByCondition(x => x.Id == id));
        }

        [HttpGet("categoria")]
        public IActionResult GetCategoria()
        {
            return Ok(_repositoryCategoria.FindAll());
        }

        [HttpPost("add-curso")]
        public async Task<Curso> CreateCurso(Curso curso)
        {
            var result = await _service.Create(curso);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(Guid id)
        {
             await _service.Delete(id);
            return Ok();
        }

        [HttpPut("update-curso")]
        public async Task<Curso> UpdateCurso(Curso curso)
        {
            var result = await _service.Update(curso);
            return result;
        }
    }
}

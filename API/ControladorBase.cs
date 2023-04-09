using GisaApiArq.Dominio;
using GisaApiArq.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GisaApiArq.API
{
    public abstract class ControladorBase<T> : ControllerBase where T : EntidadeBase
    {

        protected IRepositorioBase<T> _repositorio;
        private readonly ILogger<ControladorBase<T>> _logger;

        public ControladorBase(IRepositorioBase<T> repositorio, ILogger<ControladorBase<T>> logger)
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        [HttpGet]
        public virtual IActionResult ObterTodos()
        {
            _repositorio.ObterTodos();
            return Ok(_repositorio.ObterTodos());
        }

        [HttpGet("{id}")]
        public virtual IActionResult ObterPorId(long id)
        {
            return Ok(_repositorio.ObterPorId(id));
        }

        [HttpPost]
        public virtual IActionResult Inserir(T entidade)
        {
            _repositorio.Inserir(entidade);
            return Ok();
        }

        [HttpPut]
        public virtual IActionResult Atualizar(T entidade)
        {
            _repositorio.Atualizar(entidade);
            return Ok();
        }

        [HttpDelete]
        public virtual IActionResult Remover(long id)
        {
            _repositorio.Remover(id);
            return Ok();
        }
    }
}

using GisaApiArq.Dominio;
using GisaApiArq.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GisaApiArq.API
{
    public abstract class ControladorCrudBase<T> : ControladorBase<T> where T : EntidadeBase
    {

        protected IRepositorioCrudBase<T> _repositorio;

        public ControladorCrudBase(IRepositorioCrudBase<T> repositorio, ILogger<ControladorCrudBase<T>> logger) : base(logger)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public virtual IActionResult ObterTodos()
        {
            _logger.LogInformation("Acionado ObterTodos");
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

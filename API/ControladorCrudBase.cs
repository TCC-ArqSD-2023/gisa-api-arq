using GisaApiArq.Dominio;
using GisaApiArq.Infra;
using GisaApiArq.Servicos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GisaApiArq.API
{
    public abstract class ControladorCrudBase<T> : ControladorBase<T> where T : EntidadeBase
    {
        protected new readonly IServicoCrudBase<T> _servico;

        protected ControladorCrudBase(ILogger<ControladorBase<T>> logger, IServicoCrudBase<T> servico) : base(logger, servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public virtual IActionResult ObterTodos()
        {
            _logger.LogInformation("Acionado ObterTodos");
            return Ok(_servico.ObterTodos().Value);
        }

        [HttpGet("{id}")]
        public virtual IActionResult ObterPorId(long id)
        {
            return Ok(_servico.ObterPorId(id).Value);
        }

        [HttpPost]
        public virtual IActionResult Inserir(T entidade)
        {
            _servico.Inserir(entidade);
            return Ok();
        }

        [HttpPut]
        public virtual IActionResult Atualizar(T entidade)
        {
            _servico.Atualizar(entidade);
            return Ok();
        }

        [HttpDelete]
        public virtual IActionResult Remover(long id)
        {
            _servico.Remover(id);
            return Ok();
        }
    }
}

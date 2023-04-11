using GisaApiArq.Dominio;
using GisaApiArq.Infra;
using GisaApiArq.Servicos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GisaApiArq.API
{
    public abstract class ControladorCrudBase<T, DTO> : ControladorBase<T, DTO> where T : EntidadeBase where DTO : class
    {
        protected new readonly IServicoCrudBase<T> _servico;

        protected ControladorCrudBase(ILogger<ControladorCrudBase<T, DTO>> logger, IServicoCrudBase<T> servico) : base(logger, servico)
        {
            _servico = servico;
        }

        [HttpPost]
        public virtual IActionResult Inserir(long id, DTO dto)
        {
            _servico.Inserir(converterDTO(dto));
            return Ok();
        }

        [HttpGet]
        public virtual IActionResult ObterTodos([FromQuery] int skip = 0,[FromQuery] int take = 50)
        {
            _logger.LogInformation("Acionado ObterTodos");
            return Ok(_servico.ObterTodos().Value);
        }

        [HttpGet("{id}")]
        public virtual IActionResult ObterPorId(long id)
        {
            return Ok(_servico.ObterPorId(id).Value);
        }

        [HttpPut("{id}")]
        public virtual IActionResult Atualizar(long id, [FromBody] DTO dto)
        {
            _servico.Atualizar(converterDTO(dto));
            return Ok();
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Remover(long id)
        {
            _servico.Remover(id);
            return Ok();
        }

        protected T converterDTO(DTO dto)
        {
            if (dto is T)
                return dto as T;

            //_logger.LogError("Não foi possível converter DTO.");
            throw new NotImplementedException("Não foi possível converter DTO.");
        }
    }
}

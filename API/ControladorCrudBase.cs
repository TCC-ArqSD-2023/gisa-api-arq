using AutoMapper;
using GisaApiArq.Dominio;
using GisaApiArq.Dominio.Erros;
using GisaApiArq.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace GisaApiArq.API
{
    public abstract class ControladorCrudBase<T, DTO> : ControladorBase<T, DTO> where T : EntidadeBase where DTO : class
    {
        protected new readonly IServicoCrudBase<T> _servico;

        protected ControladorCrudBase(ILogger<ControladorCrudBase<T, DTO>> logger, IServicoCrudBase<T> servico, IMapper mapper) : base(logger, servico, mapper)
        {
            _servico = servico;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<FluentResults.Error>), StatusCodes.Status500InternalServerError)]
        public virtual IActionResult Inserir(DTO dto)
        {
            _logger.LogInformation($"Acionado recurso {nameof(Inserir)}.");
            var resultado = _servico.Inserir(converterDTO(dto));

            if (resultado.IsFailed)
                return retornarErroGenerico(resultado.Errors);

            var entidade = resultado.Value;
            
            return CreatedAtAction(nameof(ObterPorId), new {id = entidade.Id}, entidade);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<FluentResults.Error>), StatusCodes.Status500InternalServerError)]
        public virtual IActionResult ObterTodos([FromQuery] int skip = 0,[FromQuery] int take = 50)
        {
            _logger.LogInformation($"Acionado recurso {nameof(ObterTodos)}. Skip {skip}, Take {take}.");

            var resultado = _servico.ObterTodos();
            
            if (resultado.IsFailed)
                return retornarErroGenerico(resultado.Errors.FirstOrDefault()?.Message);

            return Ok(resultado.Value);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<FluentResults.Error>), StatusCodes.Status500InternalServerError)]
        public virtual IActionResult ObterPorId(long id)
        {
            _logger.LogInformation($"Acionado recurso {nameof(ObterPorId)}.");

            var resultado = _servico.ObterPorId(id);
            if (resultado.IsFailed)
            {
                if(resultado.HasError<NaoEncontradoError>())
                    return NotFound();

                return retornarErroGenerico(resultado.Errors.FirstOrDefault()?.Message);
            }

            return Ok(_servico.ObterPorId(id).Value);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<FluentResults.Error>), StatusCodes.Status500InternalServerError)]
        public virtual IActionResult Atualizar(long id, [FromBody] DTO dto)
        {
            _logger.LogInformation($"Acionado recurso {nameof(Atualizar)}.");

            var resultado = _servico.Atualizar(id, converterDTO(dto));

            if (resultado.IsFailed)
                return retornarErroGenerico(resultado.Errors.FirstOrDefault()?.Message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<FluentResults.Error>), StatusCodes.Status500InternalServerError)]
        public virtual IActionResult Remover(long id)
        {
            _logger.LogInformation($"Acionado recurso {nameof(Remover)}.");
            
            var resultado = _servico.Remover(id);

            if (resultado.IsFailed)
                return retornarErroGenerico(resultado.Errors.FirstOrDefault()?.Message);

            return NoContent();
        }

        protected T converterDTO(DTO dto)
        {
            if (dto is T)
                return dto as T;

            return _mapper.Map<T>(dto);
        }

        protected IActionResult retornarErroGenerico(object? retorno)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, retorno);
        }
    }
}

using AutoMapper;
using GisaApiArq.Dominio;
using GisaApiArq.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisaApiArq.API
{
    public abstract class ControladorBase<T, DTO> : ControllerBase where T : EntidadeBase where DTO : class
    {
        protected readonly ILogger<ControladorBase<T, DTO>> _logger;
        protected readonly IServicoBase<T> _servico;
        protected readonly IMapper _mapper;


        public ControladorBase(ILogger<ControladorBase<T, DTO>> logger, IServicoBase<T> servico, IMapper mapper)
        {
            _logger = logger;
            _servico = servico;
            _mapper = mapper;
        }

        protected virtual T converterDTO(DTO dto)
        {
            if (dto is T)
                return dto as T;

            return _mapper.Map<T>(dto);
        }

        protected virtual IActionResult retornarErroGenerico(string retorno)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, retorno);
        }

        protected virtual IActionResult retornarErroGenerico(IEnumerable<string> retorno)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, retorno);
        }

        protected virtual IActionResult retornarErroGenerico(object? retorno)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, retorno);
        }
    }
}

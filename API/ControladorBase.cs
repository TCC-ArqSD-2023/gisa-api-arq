using GisaApiArq.Dominio;
using GisaApiArq.Servicos;
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

        public ControladorBase(ILogger<ControladorBase<T, DTO>> logger, IServicoBase<T> servico)
        {
            _logger = logger;
            _servico = servico;
        }
    }
}

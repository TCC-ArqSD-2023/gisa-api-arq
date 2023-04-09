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
    public class ControladorBase<T> : ControllerBase where T : EntidadeBase
    {
        protected readonly ILogger<ControladorBase<T>> _logger;
        protected readonly IServicoBase<T> _servico;

        public ControladorBase(ILogger<ControladorBase<T>> logger, IServicoBase<T> servico)
        {
            _logger = logger;
            _servico = servico;
        }
    }
}

using AutoMapper;
using GisaApiArq.Dominio;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisaApiArq.Infra
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : EntidadeBase
    {
        protected readonly ILogger<RepositorioBase<T>> _logger;
        protected readonly IMapper _mapper;

        public RepositorioBase(ILogger<RepositorioBase<T>> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
    }
}

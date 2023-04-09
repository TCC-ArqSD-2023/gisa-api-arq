using FluentResults;
using GisaApiArq.Dominio;
using GisaApiArq.Infra;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisaApiArq.Servicos
{
    public class ServicoCrudBase<T> : ServicoBase<T>, IServicoCrudBase<T> where T : EntidadeBase
    {
        protected new readonly IRepositorioCrudBase<T> _repositorio;
        public ServicoCrudBase(ILogger<ServicoBase<T>> logger, IRepositorioCrudBase<T> repositorio) : base(logger, repositorio)
        {
            _repositorio = repositorio;
        }

        public virtual Result<IEnumerable<T>> ObterTodos()
        {
            return Result.Ok(_repositorio.ObterTodos());
        }

        public virtual Result<T?> ObterPorId(long id)
        {
            return Result.Ok(_repositorio.ObterPorId(id));
        }

        public virtual Result Inserir(T entidade)
        {
            _repositorio.Inserir(entidade);
            return Result.Ok();
        }

        public virtual Result Atualizar(T entidade)
        {
            _repositorio.Atualizar(entidade);
            return Result.Ok();
        }

        public virtual Result Remover(long id)
        {
            _repositorio.Remover(id);
            return Result.Ok();
        }
    }
}

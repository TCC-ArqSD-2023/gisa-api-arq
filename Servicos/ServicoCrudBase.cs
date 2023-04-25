using AutoMapper;
using FluentResults;
using GisaApiArq.Dominio;
using GisaApiArq.Dominio.Erros;
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
        public ServicoCrudBase(ILogger<ServicoBase<T>> logger, IRepositorioCrudBase<T> repositorio, IMapper mapper) : base(logger, repositorio, mapper)
        {
            _repositorio = repositorio;
        }

        public virtual Result<IEnumerable<T>> ObterTodos()
        {
            try
            {
                return Result.Ok(_repositorio.ObterTodos());
            }
            catch (Exception ex)
            {
                return Result.Fail(new ErroInternoError(ex.Message));
            }
        }

        public virtual Result<T?> ObterPorId(long id)
        {
            try
            {
                var entidade = _repositorio.ObterPorId(id);
                if (entidade == null)
                    return Result.Fail(new NaoEncontradoError(id, nameof(T)));

                return Result.Ok<T?>(entidade);
            } catch (Exception ex)
            {
                return Result.Fail(new ErroInternoError(ex.Message));
            }
        }
        public Result<IEnumerable<T>> ObterPorIds(IEnumerable<long> ids)
        {
            if (ids == null || ids.Count() == 0)
                return Result.Fail("Lista de IDs de parâmetro vazia.");

            try
            {
                return Result.Ok(_repositorio.ObterTodos().Where(t => ids.Contains(t.Id)));
            }
            catch (Exception ex)
            {
                return Result.Fail(new ErroInternoError(ex.Message));
            }
        }


        public virtual Result<T> Inserir(T entidade)
        {
            try
            {
                _repositorio.Inserir(entidade);
                return Result.Ok(entidade);

            }
            catch (Exception ex)
            {
                return Result.Fail(new ErroInternoError(ex.Message));
            }
        }

        public virtual Result Atualizar(long id, T entidade)
        {
            try
            {
                entidade.Id = id;

                var resultado = ObterPorId(id);
                if (resultado.IsFailed)
                    return Result.Fail(resultado.Errors);

                var entidadeBanco = resultado.Value;
                _mapper.Map(entidade, entidadeBanco);

                _repositorio.Atualizar(entidadeBanco);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(new ErroInternoError(ex.Message));
            }
        }

        public virtual Result Remover(long id)
        {
            try
            {
                _repositorio.Remover(id);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(new ErroInternoError(ex.Message));
            }
        }
    }
}

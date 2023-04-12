using AutoMapper;
using GisaApiArq.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisaApiArq.Infra
{
    public class RepositorioCrudBase<T> : RepositorioBase<T>, IRepositorioCrudBase<T> where T : EntidadeBase
    {
        protected readonly DbContext _contexto;
        protected DbSet<T> _colecao;

        public RepositorioCrudBase(DbContext contexto, ILogger<RepositorioBase<T>> logger, IMapper mapper) : base(logger, mapper)
        {
            _contexto = contexto;
            _colecao = _contexto.Set<T>();
        }

        public virtual IEnumerable<T> ObterTodos()
        {
            return _colecao.AsEnumerable();
        }

        public virtual T? ObterPorId(long id)
        {
            return _colecao.SingleOrDefault(e => e.Id == id);
        }

        public virtual void Inserir(T entidade)
        {
            if (entidade == null)
            {
                throw new ArgumentNullException("entidade");
            }
            _colecao.Add(entidade);
            _contexto.SaveChanges();
        }

        public virtual void Atualizar(T entidade)
        {
            _colecao.Attach(entidade);
            _contexto.Entry(entidade).State = EntityState.Modified;

            _contexto.SaveChanges();
        }

        public virtual void Remover(long id)
        {
            var entidade = ObterPorId(id);
            if (entidade == null)
            {
                throw new ArgumentNullException("entidade");
            }

            _colecao.Remove(entidade);
            _contexto.SaveChanges();
        }
    }
}

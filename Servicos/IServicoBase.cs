using FluentResults;
using GisaApiArq.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisaApiArq.Servicos
{
    public interface IServicoBase<T> where T : EntidadeBase
    {
        Result<IEnumerable<T>> ObterTodos();
        Result<T?> ObterPorId(long id);
        Result Inserir(T entidade);
        Result Atualizar(T entidade);
        Result Remover(long id);
    }
}

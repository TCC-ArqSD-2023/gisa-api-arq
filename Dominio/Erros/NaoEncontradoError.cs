using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisaApiArq.Dominio.Erros
{
    public class NaoEncontradoError : Error
    {
        public NaoEncontradoError() : base("Não encontrado") { }
        public NaoEncontradoError(long id) : base($"Entidade de id {id} não encontrada.") { }
        public NaoEncontradoError(long id, string nomeEntidade) : base($"{nomeEntidade} de id {id} não encontrado.") { }
    }
}

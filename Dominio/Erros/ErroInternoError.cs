using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisaApiArq.Dominio.Erros
{
    public class ErroInternoError : Error
    {
        public ErroInternoError(string mensagem) : base(mensagem) { }
    }
}

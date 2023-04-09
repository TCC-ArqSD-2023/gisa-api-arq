using GisaApiArq.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisaApiArq.Infra
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : EntidadeBase
    {
    }
}

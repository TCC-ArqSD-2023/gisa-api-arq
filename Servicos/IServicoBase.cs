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
    }
}

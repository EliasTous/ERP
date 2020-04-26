using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.NationalQuota
{
   public interface INationalQuotaRepository : IRepository<IEntity, string>
    {
    }
}

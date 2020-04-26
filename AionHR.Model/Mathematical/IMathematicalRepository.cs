using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SelfService
{
    public interface IMathematicalRepository : IRepository<IEntity, string>
    {
    }
}

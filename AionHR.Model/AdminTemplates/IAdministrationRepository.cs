using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.AdminTemplates
{
    public interface IAdministrationRepository: IRepository<IEntity, string>, ICommonRepository
    {
        
    }
}

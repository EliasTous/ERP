using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TaskManagement
{
    public interface ITaskManagementRepository:IRepository<IEntity,string>
    {
    }
}

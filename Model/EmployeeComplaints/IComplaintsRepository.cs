using Infrastructure.Domain;
using Model.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EmployeeComplaints
{
   public  interface IComplaintsRepository:IRepository<Complaint, string>,ICommonRepository
    {

    }
}

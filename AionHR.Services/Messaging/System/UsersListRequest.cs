using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.System
{
    public class UsersListRequest:ListRequest
    {
        public string DepartmentId { get; set; }

        public string PositionId { get; set; }
        public string SortBy { get; set; }

        private Dictionary<string, string> parameters;

        public override Dictionary<string,string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_departmentId",DepartmentId);
                parameters.Add("_positionId",PositionId);
                parameters.Add("_sortBy", SortBy);
                return parameters; 
            }
        }
    }
}

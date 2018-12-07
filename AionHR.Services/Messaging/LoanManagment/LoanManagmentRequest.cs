using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.LoanManagment
{
    public class LoanCommentsListRequest : ListRequest
    {
        public int loanId { get; set; }

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_loanId", loanId.ToString());

                return parameters;
            }
        }

    }



    public class LoanManagementListRequest : ListRequest
    {

        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public int BranchId { get; set; }
        public int Status { get; set; }
        public int DivisionId { get; set; }
        public int approverId { get; set; }
        public string LoanId { get; set; }


        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_employeeId", EmployeeId.ToString());
                parameters.Add("_departmentId", DepartmentId.ToString());
                parameters.Add("_branchId", BranchId.ToString());
                parameters.Add("_divisionId", DivisionId.ToString());
                parameters.Add("_status", Status.ToString());
                parameters.Add("_sortBy", SortBy.ToString());
                parameters.Add("_approverId", approverId.ToString());
                parameters.Add("_loanId", LoanId);


                return parameters;
            }
        }

        public string SortBy { get; set; }
    }

    public class LoanDeductionListRequest : ListRequest
    {

    
      
      
        public string LoanId { get; set; }


        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_loanId", LoanId);
               
                return parameters;
            }
        }

      
    }
    public class LoanApprovalListRequest : ListRequest
    {




        public string LoanId { get; set; }


        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_loanId", LoanId);

                return parameters;
            }
        }


    }
}




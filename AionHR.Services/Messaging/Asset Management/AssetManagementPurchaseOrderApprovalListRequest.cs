using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Asset_Management
{
   public class AssetManagementPurchaseOrderApprovalListRequest:ListRequest
    {
       
        
        public int? BranchId { get; set; }
        public int? DepartmentId { get; set; }
        public int? Status { get; set; }
        
        public int? approverId { get; set; }
       
       
        public string poId { get; set; }
       
        

      
        public override Dictionary<string, string> Parameters
        {
            get
            {
                
                if (BranchId == null)
                    BranchId = 0;
                if (DepartmentId == null)
                    DepartmentId = 0;



                parameters = base.Parameters;
               

          
                parameters.Add("_branchId", BranchId.ToString());
                parameters.Add("_departmentId", DepartmentId.ToString());

                parameters.Add("_status", Status.ToString());
              
                parameters.Add("_approverId", approverId.ToString());
                
             
               
                parameters.Add("_poId", poId.ToString());
             


                return parameters;
            }
        }

        public string SortBy { get; set; }
    }
}


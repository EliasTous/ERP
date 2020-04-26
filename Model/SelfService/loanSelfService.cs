using Infrastructure.Domain;
using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SelfService
{
    [ClassIdentifier("60105", "60")]
    public class loanSelfService: ModelBase, IEntity
    {
     
        public string employeeId { get; set; }

        public string apStatus { get; set; }

        public string loanRef { get; set; }
        [PropertyID("60105_01")]
        [ApplySecurity]
        public string branchId { get; set; }
      
        public int? ltId { get; set; }
       
        public DateTime date { get; set; }
       
        public int? currencyId { get; set; }
       
        public double? amount { get; set; }
        
        public string purpose { get; set; }
        
        


       
        public DateTime? effectiveDate { get; set; }
       
        public string employeeRef { get; set; }

        [PropertyID("60105_01")]
        [ApplySecurity]
        public string branchName { get; set; }
       
        // get
        public string employeeName { get; set; }
      
        public string ltName { get; set; }
       
        public string currencyRef { get; set; }
       
        public double deductedAmount { get; set; }
        [PropertyID("60105_02")]
        [ApplySecurity]
        public short? ldMethod { get; set; }
        [PropertyID("60105_03")]
        [ApplySecurity]
        public double? ldValue { get; set; }

    }
}

  

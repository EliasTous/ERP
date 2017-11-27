using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.SelfService
{
    [ClassIdentifier("31000", "31")]
    public class MyInfo : ModelBase, IEntity
    {

       
        public string reference { get; set; }


        [PropertyID("31000_24")]
        [ApplySecurity]
        public EmployeeName name { get; set; }
       
        public int? scId { get; set; }
     
        public string scName { get; set; }

        
        public string firstName { get; set; }
        [PropertyID("31000_03")]
        [ApplySecurity]
        public string middleName { get; set; }
       
        public string lastName { get; set; }
        [PropertyID("31000_05")]
        [ApplySecurity]
        public string familyName { get; set; }
      

        public string idRef { get; set; }
        [PropertyID("31000_07")]
        [ApplySecurity]
        public string homeMail { get; set; }
        
        public string workMail { get; set; }
      
        public short gender { get; set; }
        [PropertyID("31000_10")]
        [ApplySecurity]
        public string mobile { get; set; }
        [PropertyID("31000_11")]
        [ApplySecurity]
        public short? religion { get; set; }
        [PropertyID("31000_12")]
        [ApplySecurity]
        public DateTime? birthDate { get; set; }
       
        public int? nationalityId { get; set; }
       
        public int? vsId { get; set; }
       
        public int? caId { get; set; }
        [PropertyID("31000_16")]
        [ApplySecurity]
        public string placeOfBirth { get; set; }
        
        public DateTime? hireDate { get; set; }

     
        public string countryName { get; set; }


        public string DateTime { get; set; }
       
        public int? positionId { get; set; }
       
        public string positionName { get; set; }
       
        public int? departmentId { get; set; }
       
        public string departmentName { get; set; }

        public string mainDept { get; set; }
      
        public int? branchId { get; set; }
        
        public int? divisionId { get; set; }
       
        public string divisionName { get; set; }
       
        public string branchName { get; set; }


        public int? lastDayWork { get; set; }
        public DateTime? contractEndingDate { get; set; }
        public string lengthOfService { get; set; }
        public int? sponsorId { get; set; }
        public string sponsorName { get; set; }
        
        public string vsName { get; set; }
       
        public string caName { get; set; }
        public bool isInactive { get; set; }
      
        public string pictureUrl { get; set; }

      
        public string reportToId { get; set; }

        public string fullName { get; set; }
       
        public EmployeeName reportToName { get; set; }
    }

  
}


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
    public class MyInfo : ModelBase, IEntity
    {

        [PropertyID("31000_01")]
        [ApplySecurity]
        public string reference { get; set; }


        [PropertyID("31000_24")]
        [ApplySecurity]
        public EmployeeName name { get; set; }
        [PropertyID("31000_25")]
        [ApplySecurity]
        public int? scId { get; set; }
        [PropertyID("31000_25")]
        [ApplySecurity]
        public string scName { get; set; }

        [PropertyID("31000_02")]
        [ApplySecurity]
        public string firstName { get; set; }
        [PropertyID("31000_03")]
        [ApplySecurity]
        public string middleName { get; set; }
        [PropertyID("31000_04")]
        [ApplySecurity]
        public string lastName { get; set; }
        [PropertyID("31000_05")]
        [ApplySecurity]
        public string familyName { get; set; }
        [PropertyID("31000_06")]
        [ApplySecurity]

        public string idRef { get; set; }
        [PropertyID("31000_07")]
        [ApplySecurity]
        public string homeMail { get; set; }
        [PropertyID("31000_08")]
        [ApplySecurity]
        public string workMail { get; set; }
        [PropertyID("31000_09")]
        [ApplySecurity]
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
        [PropertyID("31000_13")]
        [ApplySecurity]
        public int? nationalityId { get; set; }
        [PropertyID("31000_14")]
        [ApplySecurity]
        public int? vsId { get; set; }
        [PropertyID("31000_15")]
        [ApplySecurity]
        public int? caId { get; set; }
        [PropertyID("31000_16")]
        [ApplySecurity]
        public string placeOfBirth { get; set; }
        [PropertyID("31000_17")]
        [ApplySecurity]
        public DateTime? hireDate { get; set; }

        [PropertyID("31000_13")]
        [ApplySecurity]
        public string countryName { get; set; }


        public string DateTime { get; set; }
        [PropertyID("31000_18")]
        [ApplySecurity]
        public int? positionId { get; set; }
        [PropertyID("31000_18")]
        [ApplySecurity]
        public string positionName { get; set; }
        [PropertyID("31000_19")]
        [ApplySecurity]
        public int? departmentId { get; set; }
        [PropertyID("31000_19")]
        [ApplySecurity]
        public string departmentName { get; set; }

        public string mainDept { get; set; }
        [PropertyID("31000_20")]
        [ApplySecurity]
        public int? branchId { get; set; }
        [PropertyID("31000_21")]
        [ApplySecurity]
        public int? divisionId { get; set; }
        [PropertyID("31000_21")]
        [ApplySecurity]
        public string divisionName { get; set; }
        [PropertyID("31000_20")]
        [ApplySecurity]
        public string branchName { get; set; }


        public int? lastDayWork { get; set; }
        public DateTime? contractEndingDate { get; set; }
        public string lengthOfService { get; set; }
        public int? sponsorId { get; set; }
        public string sponsorName { get; set; }
        [PropertyID("31000_14")]
        [ApplySecurity]
        public string vsName { get; set; }
        [PropertyID("31000_15")]
        [ApplySecurity]
        public string caName { get; set; }
        public bool isInactive { get; set; }
        [PropertyID("31000_23")]
        [ApplySecurity]
        public string pictureUrl { get; set; }

        [PropertyID("31000_22")]
        [ApplySecurity]
        public string reportToId { get; set; }

        public string fullName { get; set; }
        [PropertyID("31000_22")]
        [ApplySecurity]
        public EmployeeName reportToName { get; set; }
    }

  
}


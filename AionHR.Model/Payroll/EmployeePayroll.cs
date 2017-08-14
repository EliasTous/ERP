﻿using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51021", "51")]
    public class EmployeePayroll
    {
        [PropertyID("51021_01")]
        [ApplySecurity]
        public EmployeeName name { get; set; }
        [PropertyID("51021_02")]
        [ApplySecurity]
        public string branchName { get; set; }
        [PropertyID("51021_03")]
        [ApplySecurity]

        public string departmentName { get; set; }
        [PropertyID("51021_04")]
        [ApplySecurity]

        public string currencyRef { get; set; }
        [PropertyID("51021_05")]
        [ApplySecurity]

        public string calendarDays { get; set; }
        [PropertyID("51021_06")]
        [ApplySecurity]
        public string calendarMinutes { get; set; }
        [PropertyID("51021_07")]
        [ApplySecurity]
        public string workingDays { get; set; }
        [PropertyID("51021_08")]
        [ApplySecurity]
        public string workingMinutes { get; set; }
        [PropertyID("51021_09")]
        [ApplySecurity]
        public double basicAmount { get; set; }
        [PropertyID("51021_10")]
        [ApplySecurity]
        public double taxAmount { get; set; }
        [PropertyID("51021_11")]
        [ApplySecurity]
        public double netSalary { get; set; }
        [PropertyID("51021_12")]
        [ApplySecurity]

        public double dAmount { get; set; }
        [PropertyID("51021_13")]
        [ApplySecurity]
        public double eAmount { get; set; }
        [PropertyID("51021_04")]
        [ApplySecurity]
        public string currencyName { get; set; }
        public string seqNo { get; set; }
       

        public string payId { get; set; }
    }
}

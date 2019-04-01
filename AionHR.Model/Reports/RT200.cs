using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{

  
    public class CurrentEntitlementDeduction
    {
        public string name { get; set; }

        public double amount { get; set; }

        public string AmountString { get; set; }

        public bool isTaxable { get; set; }

        public override Boolean Equals(Object obj)
        {
            return (obj as CurrentEntitlementDeduction).name.ToLower() == name.ToLower();
        }

    }

    public class CurrentEntitlementDeductionComparer : IEqualityComparer<CurrentEntitlementDeduction>
    {
        public bool Equals(CurrentEntitlementDeduction one, CurrentEntitlementDeduction two)
        {
            // Adjust according to requirements.
            return one.Equals(two);

        }

        public int GetHashCode(CurrentEntitlementDeduction item)
        {
            return StringComparer.InvariantCultureIgnoreCase
                                 .GetHashCode(item.name);

        }
    }

    public class CurrentEntitlementDeductionCollection : ArrayList, ITypedList
    {
        public CurrentEntitlementDeductionCollection(IList s) : base(s) { }

        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {


            return TypeDescriptor.GetProperties(typeof(CurrentEntitlementDeduction));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "EntitlementDeductions";
        }
    }

    public class CurrentPayrollLine
    {
        public string name { get; set; }
        public string employeeRef { get; set; }

        public string branchName { get; set; }
        public string departmentName { get; set; }
        public string countryName { get; set; }

        public double essAmount { get; set; }

        public double cssAmount { get; set; }

        //public int days { get; set; }

        //public int calendarDays { get; set; }

        public double basicAmount { get; set; }

        public string BasicAmountString
        {
            get
            {
                //return currencyRef + String.Format("{0:n0}", basicAmount);
                return basicAmount.ToString();
            }
        }

        private List<CurrentEntitlementDeduction> entitlements;
        private List<CurrentEntitlementDeduction> deductions;

        public CurrentEntitlementDeductionCollection Entitlements
        {
            get
            {
                List<CurrentEntitlementDeduction> all = new List<CurrentEntitlementDeduction>();
                all.AddRange(entitlements.Where(x => x.isTaxable));
                all.Add(new CurrentEntitlementDeduction() { name = taxableTotalString, amount = TaxableEntitlementsTotal, AmountString = String.Format("{0:n0}", TaxableEntitlementsTotal) });
                all.AddRange(entitlements.Where(x => !x.isTaxable));
                all.Add(new CurrentEntitlementDeduction() { name = eAmountString, amount = EntitlementsTotal, AmountString =  String.Format("{0:n0}", EntitlementsTotal) });

                all.AddRange(deductions);
                all.Add(new CurrentEntitlementDeduction() { name = dAmountString, amount = DeductionsTotal, AmountString =  String.Format("{0:n0}", DeductionsTotal) });
                all.Add(new CurrentEntitlementDeduction() { name = netSalary, amount = NetSalary, AmountString =  String.Format("{0:n0}", NetSalary) });
                all.Add(new CurrentEntitlementDeduction() { name = essString, amount = essAmount, AmountString = String.Format("{0:n0}", essAmount) });
                all.Add(new CurrentEntitlementDeduction() { name = cssString, amount = cssAmount, AmountString = String.Format("{0:n0}", cssAmount) });
                //all = all.Select(item => {
                //    item.AmountString = Regex.Replace(item.AmountString, @"\s+", ""); // remove all white spaces
                //    item.name = Regex.Replace(item.name, @"\s+", ""); // remove all white spaces
                //    return item; // return processed item...
                //}).ToList();
                return new CurrentEntitlementDeductionCollection(all);
            }
        }

        private string eAmountString;
        private string dAmountString;
        private string taxableTotalString;
        private string netSalary;
        private string essString;
        private string cssString;
       
        public string currencyRef { get; set; }

        public double EntitlementsTotal { get { return entitlements.Sum(x => x.isTaxable?0:x.amount ); } }
        public double TaxableEntitlementsTotal { get { return basicAmount + entitlements.Sum(x => x.isTaxable ?  x.amount:0); } }

        public CurrentEntitlementDeductionCollection Deductions { get { return new CurrentEntitlementDeductionCollection(deductions); } }

        public double DeductionsTotal { get { return deductions.Sum(x => x.amount); } }

        public double NetSalary { get { return /*basicAmount +*/ EntitlementsTotal + TaxableEntitlementsTotal - DeductionsTotal; } }

        public void AddEn(CurrentEntitlementDeduction en)
        {
            entitlements[entitlements.IndexOf(en)].amount = en.amount;
            entitlements[entitlements.IndexOf(en)].isTaxable = en.isTaxable;
            entitlements[entitlements.IndexOf(en)].AmountString = String.Format("{0:n0}", en.amount);
        }
        
        public void AddDe(CurrentEntitlementDeduction de)
        {
            deductions[deductions.IndexOf(de)].amount = de.amount;
            deductions[deductions.IndexOf(de)].AmountString = String.Format("{0:n0}", de.amount);
        }
        public CurrentPayrollLine(HashSet<CurrentEntitlementDeduction> en, HashSet<CurrentEntitlementDeduction> de, List<RT200> details,string taxable, string eString, string dString, string netString,string ess, string css)
        {
            entitlements = new List<CurrentEntitlementDeduction>();
            deductions = new List<CurrentEntitlementDeduction>();
            en.ToList().ForEach(x => entitlements.Add(new CurrentEntitlementDeduction() { name = x.name }));
            de.ToList().ForEach(x => deductions.Add(new CurrentEntitlementDeduction() { name = x.name }));

            if (details.Count > 0)
            {
                basicAmount = details[0].basicAmount;
                branchName = details[0].branchName;
                departmentName = details[0].departmentName;
                countryName = details[0].countryName;
                name = details[0].employeeName.fullName;
                employeeRef = details[0].employeeName.reference;
                currencyRef = details[0].currencyRef;
                essAmount = details[0].essAmount;
                cssAmount = details[0].cssAmount;
               
            }
            foreach (var item in details)
            {
                if (string.IsNullOrEmpty(item.edName))
                    continue;
                if (item.edType == 1)
                    AddEn(new CurrentEntitlementDeduction() { amount = item.edAmount, name = item.edName, isTaxable=item.isTaxable });
                else
                    AddDe(new CurrentEntitlementDeduction() { amount = item.edAmount, name = item.edName , isTaxable=item.isTaxable});


            }
            eAmountString = eString;
            dAmountString = dString;
            netSalary = netString;
            taxableTotalString = taxable;
            cssString = css;
            essString = ess;
        }

    }

    public class CurrentPayrollLineCollection : ArrayList, ITypedList
    {


        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors != null && listAccessors.Length > 0)
            {
                PropertyDescriptor listAccessor = listAccessors[listAccessors.Length - 1];
                if (listAccessor.PropertyType.Equals(typeof(CurrentEntitlementDeductionCollection)))
                    return TypeDescriptor.GetProperties(typeof(CurrentEntitlementDeduction));




            }
            return TypeDescriptor.GetProperties(typeof(CurrentPayrollLine));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "CurrentPayrollLines";
        }
    }


    [ClassIdentifier("80200", "80")]
    public class RT200
    {
        public EmployeeName employeeName { get; set; }

        public string branchName { get; set; }
        public string departmentName { get; set; }
        public string countryName { get; set; }

        //public int seqNo { get; set; }

        public double basicAmount { get; set; }

        public double eAmount { get; set; }
        public double dAmount { get; set; }
        public double netSalary { get; set; }
        public string edName { get; set; }

        public double essAmount { get; set; }

        public double cssAmount { get; set; }

        public int paymentMethod { get; set; }
        public double edAmount { get; set; }
        public bool isTaxable { get; set; }

        public string currencyRef { get; set; }

        public int edType { get; set; }
    }

    public class CurrentPayrollSet
    {
        public CurrentPayrollLineCollection Payrolls { get; set; }

        public CurrentEntitlementDeductionCollection Names { get; set; }

        public CurrentEntitlementDeductionCollection Totals
        {
            get
            {
                if (Payrolls.Count == 0)
                    return new CurrentEntitlementDeductionCollection(new List<CurrentEntitlementDeduction>());
                List<CurrentEntitlementDeduction> totals = new List<CurrentEntitlementDeduction>();
                List<CurrentPayrollLine> lines = Payrolls.Cast<CurrentPayrollLine>().ToList();

                for (int i = 0; i < lines[0].Entitlements.Count; i++)
                {
                    totals.Add(new CurrentEntitlementDeduction() { amount = lines.Sum(x => (x.Entitlements[i] as CurrentEntitlementDeduction).amount) });
                }

                return new CurrentEntitlementDeductionCollection(totals);


            }
        }
        public CurrentEntitlementDeductionCollection Headers
        {
            get
            {
                List<CurrentEntitlementDeduction> l = new List<CurrentEntitlementDeduction>();
                l.Add(new CurrentEntitlementDeduction() { name = taxableString });
                for (int i = 1; i < Names.Count; i++)
                {
                    if(i == taxableIndex + 1)
                        l.Add(new CurrentEntitlementDeduction() { name =EnString });
                    else if (i == DIndex+2)
                        l.Add(new CurrentEntitlementDeduction() { name = DeString });
                    
                    else
                        l.Add(new CurrentEntitlementDeduction());

                }
                return new CurrentEntitlementDeductionCollection(l);
            }
        }

        public int DIndex { get; set; }
        public int taxableIndex { get; set; }
        public double TotalBasics
        {
            get
            {
                List<CurrentPayrollLine> lines = Payrolls.Cast<CurrentPayrollLine>().ToList();
                return lines.Sum(x => x.basicAmount);
            }
        }
        private string EnString;
        private string DeString;
        private string taxableString;
        public CurrentPayrollSet(string entitlementsString,string taxable, string deductionsString)
        {
            EnString = entitlementsString;
            DeString = deductionsString;
            taxableString = taxable;
        }

        public string branchName { get; set; }

        public string departmentName { get; set; }

        public string countryName { get; set; }
    }

    public class CurrentPayrollCollection : ArrayList, ITypedList
    {


        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors != null && listAccessors.Length > 0)
            {
                PropertyDescriptor listAccessor = listAccessors[listAccessors.Length - 1];
                if (listAccessor.PropertyType.Equals(typeof(CurrentEntitlementDeductionCollection)))
                    return TypeDescriptor.GetProperties(typeof(CurrentEntitlementDeduction));
                else if (listAccessor.PropertyType.Equals(typeof(CurrentPayrollLineCollection)))
                    return TypeDescriptor.GetProperties(typeof(CurrentPayrollLine));
            }
            return TypeDescriptor.GetProperties(typeof(CurrentPayrollSet));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "CurrentPayroll";
        }
    }
}

using AionHR.Model.Employees.Profile;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{


    public class DepartmentEntitlementDeduction
    {
        public string name { get; set; }

        public double amount { get; set; }

        public string AmountString { get; set; }

        public bool isTaxable { get; set; }

        public override Boolean Equals(Object obj)
        {
            return (obj as DepartmentEntitlementDeduction).name.ToLower() == name.ToLower();
        }

    }

    public class DepartmentEntitlementDeductionComparer : IEqualityComparer<DepartmentEntitlementDeduction>
    {
        public bool Equals(DepartmentEntitlementDeduction one, DepartmentEntitlementDeduction two)
        {
            // Adjust according to requirements.
            return one.Equals(two);

        }

        public int GetHashCode(DepartmentEntitlementDeduction item)
        {
            return StringComparer.InvariantCultureIgnoreCase
                                 .GetHashCode(item.name);

        }
    }

    public class DepartmentEntitlementDeductionCollection : ArrayList, ITypedList
    {
        public DepartmentEntitlementDeductionCollection(IList s) : base(s) { }

        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {


            return TypeDescriptor.GetProperties(typeof(DepartmentEntitlementDeduction));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "EntitlementDeductions";
        }
    }

    public class DepartmentPayrollLine
    {
     //   public string name { get; set; }

     //   public string branchName { get; set; }
          public string departmentName { get; set; }
       // public string countryName { get; set; }

        public double essAmount { get; set; }

        public double cssAmount { get; set; }
        public DateTime payDate { get; set; }
        public string payDateString { get; set; }
        public int calendarDays { get; set; }
        public DateTime startDate { get; set; }
        public string startDateString { get; set; }

        public DateTime endDate { get; set; }
        public string endDateString { get; set; }
        public int fiscalYear { get; set; }

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

        private List<DepartmentEntitlementDeduction> entitlements;
        private List<DepartmentEntitlementDeduction> deductions;

        public DepartmentEntitlementDeductionCollection Entitlements
        {
            get
            {
                List<DepartmentEntitlementDeduction> all = new List<DepartmentEntitlementDeduction>();
                all.AddRange(entitlements.Where(x => x.isTaxable));
                all.Add(new DepartmentEntitlementDeduction() { name = taxableTotalString, amount = TaxableEntitlementsTotal, AmountString = String.Format("{0:n0}", TaxableEntitlementsTotal) });
                all.AddRange(entitlements.Where(x => !x.isTaxable));
                all.Add(new DepartmentEntitlementDeduction() { name = eAmountString, amount = EntitlementsTotal, AmountString = String.Format("{0:n0}", EntitlementsTotal) });

                all.AddRange(deductions);
                all.Add(new DepartmentEntitlementDeduction() { name = dAmountString, amount = DeductionsTotal, AmountString = String.Format("{0:n0}", DeductionsTotal) });
                all.Add(new DepartmentEntitlementDeduction() { name = netSalary, amount = NetSalary, AmountString = String.Format("{0:n0}", NetSalary) });
                all.Add(new DepartmentEntitlementDeduction() { name = essString, amount = essAmount, AmountString = String.Format("{0:n0}", essAmount) });
                all.Add(new DepartmentEntitlementDeduction() { name = cssString, amount = cssAmount, AmountString = String.Format("{0:n0}", cssAmount) });
                //all = all.Select(item => {
                //    item.AmountString = Regex.Replace(item.AmountString, @"\s+", ""); // remove all white spaces
                //    item.name = Regex.Replace(item.name, @"\s+", ""); // remove all white spaces
                //    return item; // return processed item...
                //}).ToList();
                return new DepartmentEntitlementDeductionCollection(all);
            }
        }

        private string eAmountString;
        private string dAmountString;
        private string taxableTotalString;
        private string netSalary;
        private string essString;
        private string cssString;

        //public string currencyRef { get; set; }

        public double EntitlementsTotal { get { return entitlements.Sum(x => x.isTaxable ? 0 : x.amount); } }
        public double TaxableEntitlementsTotal { get { return basicAmount + entitlements.Sum(x => x.isTaxable ? x.amount : 0); } }

        public DepartmentEntitlementDeductionCollection Deductions { get { return new DepartmentEntitlementDeductionCollection(deductions); } }

        public double DeductionsTotal { get { return deductions.Sum(x => x.amount); } }

        public double NetSalary { get { return basicAmount + EntitlementsTotal + TaxableEntitlementsTotal - DeductionsTotal; } }

        public void AddEn(DepartmentEntitlementDeduction en)
        {
            entitlements[entitlements.IndexOf(en)].amount = en.amount;
            entitlements[entitlements.IndexOf(en)].isTaxable = en.isTaxable;
            entitlements[entitlements.IndexOf(en)].AmountString = String.Format("{0:n0}", en.amount);
        }

        public void AddDe(DepartmentEntitlementDeduction de)
        {
            deductions[deductions.IndexOf(de)].amount = de.amount;
            deductions[deductions.IndexOf(de)].AmountString = String.Format("{0:n0}", de.amount);
        }
        public DepartmentPayrollLine(HashSet<DepartmentEntitlementDeduction> en, HashSet<DepartmentEntitlementDeduction> de, List<RT503> details, string taxable, string eString, string dString, string netString, string ess, string css,string format)
        {
            entitlements = new List<DepartmentEntitlementDeduction>();
            deductions = new List<DepartmentEntitlementDeduction>();
            en.ToList().ForEach(x => entitlements.Add(new DepartmentEntitlementDeduction() { name = x.name }));
            de.ToList().ForEach(x => deductions.Add(new DepartmentEntitlementDeduction() { name = x.name }));

            if (details.Count > 0)
            {
                basicAmount = details[0].basicAmount;
                payDate= details[0].payDate;
                payDateString = payDate.ToString(format);
                calendarDays = details[0].calendarDays;
                endDate = details[0].endDate;
                endDateString = endDate.ToString(format); 
                startDate = details[0].startDate;
                startDateString = startDate.ToString(format); 
                fiscalYear = details[0].fiscalYear;
                basicAmount = details[0].basicAmount;
                departmentName = details[0].departmentName;
                essAmount = details[0].essAmount;
                cssAmount = details[0].cssAmount;
                //branchName = details[0].branchName;

                //countryName = details[0].countryName;
                //name = details[0].employeeName.fullName;
                //currencyRef = details[0].currencyRef;


            }
            foreach (var item in details)
            {
                if (item.edType == 1)
                    AddEn(new DepartmentEntitlementDeduction() { amount = item.edAmount, name = item.edName, isTaxable = item.isTaxable });
                else
                    AddDe(new DepartmentEntitlementDeduction() { amount = item.edAmount, name = item.edName, isTaxable = item.isTaxable });


            }
            eAmountString = eString;
            dAmountString = dString;
            netSalary = netString;
            taxableTotalString = taxable;
            cssString = css;
            essString = ess;
        }

    }

    public class DepartmentPayrollLineCollection : ArrayList, ITypedList
    {


        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors != null && listAccessors.Length > 0)
            {
                PropertyDescriptor listAccessor = listAccessors[listAccessors.Length - 1];
                if (listAccessor.PropertyType.Equals(typeof(DepartmentEntitlementDeductionCollection)))
                    return TypeDescriptor.GetProperties(typeof(DepartmentEntitlementDeduction));




            }
            return TypeDescriptor.GetProperties(typeof(DepartmentPayrollLine));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return " DepartmentPayrollLines";
        }
    }


    
    public class RT503
    {
        //public EmployeeName employeeName { get; set; }

        // public string branchName { get; set; }
        public string departmentName { get; set; }
        public DateTime payDate { get; set; }
        //public string countryName { get; set; }
        public int calendarDays { get; set; }
        //public int seqNo { get; set; }
        public DateTime endDate { get; set; }
        public int fiscalYear { get; set; }
        public DateTime startDate { get; set; }
        public double basicAmount { get; set; }
        public double eAmount { get; set; }
        public double dAmount { get; set; }
        public double netSalary { get; set; }
        public string edName { get; set; }
        public double essAmount { get; set; }
        public double cssAmount { get; set; }

     //   public int paymentMethod { get; set; }
        public double edAmount { get; set; }
        public bool isTaxable { get; set; }

       // public string currencyRef { get; set; }

        public int edType { get; set; }
    }

    public class DepartmentPayrollSet
    {
        public DepartmentPayrollLineCollection Payrolls { get; set; }

        public DepartmentEntitlementDeductionCollection Names { get; set; }

        public DepartmentEntitlementDeductionCollection Totals
        {
            get
            {
                if (Payrolls.Count == 0)
                    return new DepartmentEntitlementDeductionCollection(new List<DepartmentEntitlementDeduction>());
                List<DepartmentEntitlementDeduction> totals = new List<DepartmentEntitlementDeduction>();
                List<DepartmentPayrollLine> lines = Payrolls.Cast<DepartmentPayrollLine>().ToList();

                for (int i = 0; i < lines[0].Entitlements.Count; i++)
                {
                    totals.Add(new DepartmentEntitlementDeduction() { amount = lines.Sum(x => (x.Entitlements[i] as DepartmentEntitlementDeduction).amount) });
                }

                return new DepartmentEntitlementDeductionCollection(totals);


            }
        }
        public DepartmentEntitlementDeductionCollection Headers
        {
            get
            {
                List<DepartmentEntitlementDeduction> l = new List<DepartmentEntitlementDeduction>();
                l.Add(new DepartmentEntitlementDeduction() { name = taxableString });
                for (int i = 1; i < Names.Count; i++)
                {
                    if (i == taxableIndex + 1)
                        l.Add(new DepartmentEntitlementDeduction() { name = EnString });
                    else if (i == DIndex + 2)
                        l.Add(new DepartmentEntitlementDeduction() { name = DeString });

                    else
                        l.Add(new DepartmentEntitlementDeduction());

                }
                return new DepartmentEntitlementDeductionCollection(l);
            }
        }

        public int DIndex { get; set; }
        public int taxableIndex { get; set; }
        public double TotalBasics
        {
            get
            {
                List<DepartmentPayrollLine> lines = Payrolls.Cast<DepartmentPayrollLine>().ToList();
                return lines.Sum(x => x.basicAmount);
            }
        }
        private string EnString;
        private string DeString;
        private string taxableString;
        public DepartmentPayrollSet(string entitlementsString, string taxable, string deductionsString)
        {
            EnString = entitlementsString;
            DeString = deductionsString;
            taxableString = taxable;
        }

        //public string branchName { get; set; }

        public string departmentName { get; set; }

      //  public string countryName { get; set; }
    }

    public class DepartmentPayrollCollection : ArrayList, ITypedList
    {


        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors != null && listAccessors.Length > 0)
            {
                PropertyDescriptor listAccessor = listAccessors[listAccessors.Length - 1];
                if (listAccessor.PropertyType.Equals(typeof(DepartmentEntitlementDeductionCollection)))
                    return TypeDescriptor.GetProperties(typeof(DepartmentEntitlementDeduction));
                else if (listAccessor.PropertyType.Equals(typeof(DepartmentPayrollLineCollection)))
                    return TypeDescriptor.GetProperties(typeof(DepartmentPayrollLine));
            }
            return TypeDescriptor.GetProperties(typeof(DepartmentPayrollSet));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return " DepartmentPayroll";
        }
    }
}

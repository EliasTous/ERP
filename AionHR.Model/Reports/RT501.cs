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
    public class EntitlementDeduction
    {
        public string name { get; set; }

        public double amount { get; set; }

        public override Boolean Equals(Object obj)
        {
            return (obj as EntitlementDeduction).name.ToLower() == name.ToLower();
        }

    }
    public class EntitlementDeductionComparer : IEqualityComparer<EntitlementDeduction>
    {
        public bool Equals(EntitlementDeduction one, EntitlementDeduction two)
        {
            // Adjust according to requirements.
            return one.Equals(two);

        }

        public int GetHashCode(EntitlementDeduction item)
        {
            return StringComparer.InvariantCultureIgnoreCase
                                 .GetHashCode(item.name);

        }
    }
    public class PayrollLine
    {
        public string name { get; set; }

        public int days { get; set; }

        public int calendarDays { get; set; }

        public double basicAmount { get; set; }

        private List<EntitlementDeduction> entitlements;
        private List<EntitlementDeduction> deductions;

        public EntitlementDeductionCollection Entitlements
        {
            get
            {
                List<EntitlementDeduction> all = new List<EntitlementDeduction>();
                all.AddRange(entitlements);
                all.Add(new EntitlementDeduction() { name = eAmountString, amount = EntitlementsTotal });

                all.AddRange(deductions);
                all.Add(new EntitlementDeduction() { name = dAmountString, amount = DeductionsTotal });
                all.Add(new EntitlementDeduction() { name = netSalary, amount = NetSalary });
                return new EntitlementDeductionCollection(all);
            }
        }

        private string eAmountString;
        private string dAmountString;
        private string netSalary;
            
        public double EntitlementsTotal { get { return entitlements.Sum(x => x.amount); } }

        public EntitlementDeductionCollection Deductions { get { return new EntitlementDeductionCollection(deductions); } }

        public double DeductionsTotal { get { return deductions.Sum(x => x.amount); } }

        public double NetSalary { get { return basicAmount + EntitlementsTotal + DeductionsTotal; } }

        public void AddEn(EntitlementDeduction en)
        {
            entitlements[entitlements.IndexOf(en)].amount = en.amount;
        }

        public void AddDe(EntitlementDeduction de)
        {
            deductions[deductions.IndexOf(de)].amount = de.amount;
        }
        public PayrollLine(HashSet<EntitlementDeduction> en, HashSet<EntitlementDeduction> de, List<RT501> details,string eString, string dString, string netString)
        {
            entitlements = new List<EntitlementDeduction>();
            deductions = new List<EntitlementDeduction>();
            en.ToList().ForEach(x => entitlements.Add(new EntitlementDeduction() { name = x.name }));
            de.ToList().ForEach(x => deductions.Add(new EntitlementDeduction() { name = x.name }));

            if (details.Count > 0)
            {
                basicAmount = details[0].basicAmount;
                name = details[0].employeeName.fullName;
                days = details[0].days;
                calendarDays = details[0].calendarDays;
            }
            foreach (var item in details)
            {
                if (item.edType == 1)
                    AddEn(new EntitlementDeduction() { amount = item.edAmount, name = item.edName });
                else
                    AddDe(new EntitlementDeduction() { amount = item.edAmount, name = item.edName });


            }
            eAmountString = eString;
            dAmountString = dString;
            netSalary = netString;
        }

    }
    public class EntitlementDeductionCollection : ArrayList, ITypedList
    {
        public EntitlementDeductionCollection(IList s) : base(s) { }

        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {


            return TypeDescriptor.GetProperties(typeof(EntitlementDeduction));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "EntitlementDeductions";
        }
    }
    public class PayrollLineCollection : ArrayList, ITypedList
    {


        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors != null && listAccessors.Length > 0)
            {
                PropertyDescriptor listAccessor = listAccessors[listAccessors.Length - 1];
                if (listAccessor.PropertyType.Equals(typeof(EntitlementDeductionCollection)))
                    return TypeDescriptor.GetProperties(typeof(EntitlementDeduction));




            }
            return TypeDescriptor.GetProperties(typeof(PayrollLine));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "PayrollLines";
        }
    }

    public class RT501
    {
        public EmployeeName employeeName { get; set; }

        public int days { get; set; }

        public string payRef { get; set; }

        public DateTime payDate { get; set; }

        public int calendarDays { get; set; }

        public DateTime endDate { get; set; }

        public string fiscalYear { get; set; }
        public string notes { get; set; }
        public DateTime startDate { get; set; }
        public int seqNo { get; set; }
        public int workingDays { get; set; }
        public double basicAmount { get; set; }

        public double eAmount { get; set; }
        public double dAmount { get; set; }
        public double netSalary { get; set; }
        public string edName { get; set; }

        public int paymentMethod { get; set; }
        public double edAmount { get; set; }

        public int edType { get; set; }
    }

    public class MonthlyPayrollSet
    {
        public PayrollLineCollection Payrolls { get; set; }

        public EntitlementDeductionCollection Names { get; set; }

        public EntitlementDeductionCollection Totals
        {
            get
            {
                if (Payrolls.Count == 0)
                    return new EntitlementDeductionCollection(new List<EntitlementDeduction>());
                List<EntitlementDeduction> totals = new List<EntitlementDeduction>();
                List<PayrollLine> lines = Payrolls.Cast<PayrollLine>().ToList();
               
                for (int i = 0; i < lines[0].Entitlements.Count; i++)
                {
                    totals.Add(new EntitlementDeduction() { amount = lines.Sum(x => (x.Entitlements[i] as EntitlementDeduction).amount) });
                }

                return new EntitlementDeductionCollection(totals);
                
                
            }
        }

        public double TotalBasics
        {
            get
            {
                List<PayrollLine> lines = Payrolls.Cast<PayrollLine>().ToList();
                return lines.Sum(x => x.basicAmount);
            }
        }
    }

    public class MonthlyPayrollCollection : ArrayList, ITypedList
    {


        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors != null && listAccessors.Length > 0)
            {
                PropertyDescriptor listAccessor = listAccessors[listAccessors.Length - 1];
                if (listAccessor.PropertyType.Equals(typeof(EntitlementDeductionCollection)))
                    return TypeDescriptor.GetProperties(typeof(EntitlementDeduction));
                else if (listAccessor.PropertyType.Equals(typeof(PayrollLineCollection)))
                    return TypeDescriptor.GetProperties(typeof(PayrollLine));





            }
            return TypeDescriptor.GetProperties(typeof(MonthlyPayrollSet));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "MonthlyPayroll";
        }
    }
}
using AionHR.Model.Attributes;
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

        public string AmountString { get; set; }

        public override Boolean Equals(Object obj)
        {
            return (obj as EntitlementDeduction).name.ToLower() == name.ToLower();
        }
        public bool isTaxable { get; set; }

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

        public string BasicAmountString
        {
            get
            {
                return String.Format("{0:n0}", basicAmount);
            }
        }
        public double essAmount { get; set; }

        public double cssAmount { get; set; }
        public double netSalary { get; set; }
        private List<EntitlementDeduction> entitlements;
        private List<EntitlementDeduction> deductions;

        public EntitlementDeductionCollection Entitlements
        {
            get
            {


                List<EntitlementDeduction> all = new List<EntitlementDeduction>();
                all.AddRange(entitlements.Where(x => x.isTaxable));
                all.Add(new EntitlementDeduction() { name = taxableTotalString, amount = TaxableEntitlementsTotal, AmountString = String.Format("{0:n0}", TaxableEntitlementsTotal) });
                all.AddRange(entitlements.Where(x => !x.isTaxable));
                all.Add(new EntitlementDeduction() { name = eAmountString, amount = EntitlementsTotal, AmountString = String.Format("{0:n0}", EntitlementsTotal) });

                all.AddRange(deductions);
                all.Add(new EntitlementDeduction() { name = dAmountString, amount = DeductionsTotal, AmountString = String.Format("{0:n0}", DeductionsTotal) });
                all.Add(new EntitlementDeduction() { name = net, amount = Net, AmountString = String.Format("{0:n0}", Net) });
                all.Add(new EntitlementDeduction() { name = essString, amount = essAmount, AmountString = String.Format("{0:n0}", essAmount) });
                all.Add(new EntitlementDeduction() { name = cssString, amount = cssAmount, AmountString = String.Format("{0:n0}", cssAmount) });
                all.Add(new EntitlementDeduction() { name = netSalaryString, amount = netSalary, AmountString = String.Format("{0:n0}", netSalary) });

                //all = all.Select(item => {
                //    item.AmountString = Regex.Replace(item.AmountString, @"\s+", ""); // remove all white spaces
                //    item.name = Regex.Replace(item.name, @"\s+", ""); // remove all white spaces
                //    return item; // return processed item...
                //}).ToList();
                return new EntitlementDeductionCollection(all);
            }
        }

        private string eAmountString;
        private string dAmountString;
        private string net;
        private string taxableTotalString;
        private string essString;
        private string cssString;
        private string netSalaryString;
        public double TaxableEntitlementsTotal { get { return basicAmount + entitlements.Sum(x => x.isTaxable ? x.amount : 0); } }

        public string currencyRef { get; set; }

        public double EntitlementsTotal { get { return entitlements.Sum(x => x.amount); } }

        public EntitlementDeductionCollection Deductions { get { return new EntitlementDeductionCollection(deductions); } }

        public double DeductionsTotal { get { return deductions.Sum(x => x.amount); } }

        public double Net { get { return basicAmount + EntitlementsTotal + DeductionsTotal; } }

        public void AddEn(EntitlementDeduction en)
        {
            entitlements[entitlements.IndexOf(en)].amount = en.amount;

            entitlements[entitlements.IndexOf(en)].isTaxable = en.isTaxable;
            entitlements[entitlements.IndexOf(en)].AmountString = String.Format("{0:n0}", en.amount);
        }

        public void AddDe(EntitlementDeduction de)
        {
            deductions[deductions.IndexOf(de)].amount = de.amount;
            deductions[deductions.IndexOf(de)].AmountString = String.Format("{0:n0}", de.amount);
        }
        public PayrollLine(HashSet<EntitlementDeduction> en, HashSet<EntitlementDeduction> de, List<RT501> details, string taxable, string eString, string dString, string netString, string ess, string css, string format, string netSalaryString)
        {
            try
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
                    currencyRef = details[0].currencyRef;
                    calendarDays = details[0].calendarDays;



                    name = details[0].employeeName.fullName;
                    currencyRef = details[0].currencyRef;
                    essAmount = details[0].essAmount;
                    cssAmount = details[0].cssAmount;
                    netSalary = details[0].netSalary;


                }
                foreach (var item in details)
                {
                    if (item.edType == 1)
                        AddEn(new EntitlementDeduction() { amount = item.edAmount, name = item.edName, isTaxable = item.isTaxable });
                    else
                        AddDe(new EntitlementDeduction() { amount = item.edAmount, name = item.edName, isTaxable = item.isTaxable });


                }
                eAmountString = eString;
                dAmountString = dString;
                net = netString;
                taxableTotalString = taxable;
                cssString = css;
                essString = ess;
                this.netSalaryString = netSalaryString;
            }
            catch (Exception exp)
            {

            }
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

    [ClassIdentifier("80501", "80")]
    public class RT501
    {
        public EmployeeName employeeName { get; set; }

        public string departmentName { get; set; }
        public string branchName { get; set; }

        public string positionName { get; set; }

        public string idRef { get; set; }

        public int days { get; set; }

        public string payRef { get; set; }

        public DateTime payDate { get; set; }

        public int calendarDays { get; set; }

        public DateTime endDate { get; set; }
        public string endDateString { get; set; }

        public string fiscalYear { get; set; }
        public string notes { get; set; }
        public DateTime startDate { get; set; }
        public string  startDateString { get; set; }
        public int seqNo { get; set; }
        public int workingDays { get; set; }
        public double basicAmount { get; set; }

        public double eAmount { get; set; }
        public double dAmount { get; set; }

        public string edName { get; set; }

        public int paymentMethod { get; set; }
        public double edAmount { get; set; }

        public string currencyRef { get; set; }
        public int currencyProfileId { get; set; }

        public int edType { get; set; }
        public double cssAmount { set; get; }
        public double netSalary { set; get; }

        public double essAmount { set; get; }
        public bool isTaxable { get; set; }

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
        public EntitlementDeductionCollection Headers
        {
            //get
            //{
            //    List<EntitlementDeduction> l = new List<EntitlementDeduction>();
            //    l.Add(new EntitlementDeduction() { name = EnString });
            //    for (int i = 0; i < Names.Count; i++)
            //    {
            //        if (i == DIndex)
            //            l.Add(new EntitlementDeduction() { name = DeString });

            //        else
            //            l.Add(new EntitlementDeduction());

            //    }
            //    return new EntitlementDeductionCollection(l);
            //}
            get
            {
                List<EntitlementDeduction> l = new List<EntitlementDeduction>();
                l.Add(new EntitlementDeduction() { name = taxableString });
                for (int i = 1; i < Names.Count; i++)
                {
                    if (i == taxableIndex + 1)
                        l.Add(new EntitlementDeduction() { name = EnString });
                    else if (i == DIndex + 2)
                        l.Add(new EntitlementDeduction() { name = DeString });

                    else
                        l.Add(new EntitlementDeduction());

                }
                return new EntitlementDeductionCollection(l);
            }
        }

        public int DIndex { get; set; }
        public double TotalBasics
        {
            get
            {
                List<PayrollLine> lines = Payrolls.Cast<PayrollLine>().ToList();
                return lines.Sum(x => x.basicAmount);
            }
        }
        private string EnString;
        private string DeString;
        private string taxableString;

        public MonthlyPayrollSet(string entitlementsString, string taxable, string deductionsString)
        {
            EnString = entitlementsString;
            DeString = deductionsString;
            taxableString = taxable;

        }

        public string PayPeriodString { get; set; }

        public string PayDate { get; set; }
        public string PayDateString { set; get; }
        public int taxableIndex { get; set; }
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
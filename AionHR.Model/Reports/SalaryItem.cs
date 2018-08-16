using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    public class SalaryItem
    {


        public string departmentName { get; set; }

        public string branchName { get; set; }


        public DateTime payDate { get; set; }


        public long calendarDays { get; set; }


        public DateTime endDate { get; set; }


        public long fiscalYear { get; set; }


        public DateTime startDate { get; set; }


        public long basicAmount { get; set; }


        public long eAmount { get; set; }


        public long dAmount { get; set; }


        public long cssAmount { get; set; }


        public long essAmount { get; set; }


        public long netSalary { get; set; }


        public string edName { get; set; }


        public long edType { get; set; }


        public bool isTaxable { get; set; }


        public long edAmount { get; set; }


        public static List<SalaryItem> GetItems()
        {
            return new List<SalaryItem>()
            {
                new SalaryItem() {

            departmentName= "الحسابات المالية",

            branchName= "الحسابات المالية",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 4688,

            eAmount= 10312,

            dAmount= 1455,

            cssAmount= 125,

            essAmount= 0,

            netSalary= 13545,

            edName= "بدل السكن",

            edType= 1,

            isTaxable= true,

            edAmount= 1562

        },

        new SalaryItem() {

            departmentName= "الحسابات المالية",

            branchName= "الحسابات المالية",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 4688,

            eAmount= 10312,

            dAmount= 1455,

            cssAmount= 125,

            essAmount= 0,

            netSalary= 13545,

            edName= "بدل الانتقال",

            edType= 1,

            isTaxable= false,

            edAmount= 2188

        },

        new SalaryItem() {

            departmentName= "الحسابات المالية",

            branchName= "الحسابات المالية",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 4688,

            eAmount= 10312,

            dAmount= 1455,

            cssAmount= 125,

            essAmount= 0,

            netSalary= 13545,

            edName= "بدل اتصالات",

            edType= 1,

            isTaxable= false,

            edAmount= 875

        },

        new SalaryItem() {

            departmentName= "الحسابات المالية",

            branchName= "الحسابات المالية",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

           basicAmount= 4688,

            eAmount= 10312,

            dAmount= 1455,

            cssAmount= 125,

            essAmount= 0,

            netSalary= 13545,

            edName= "بدل اضافى",

            edType= 1,

            isTaxable= false,

            edAmount= 1750

        },

        new SalaryItem() {

            departmentName= "الحسابات المالية",

            branchName= "الحسابات المالية",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 4688,

            eAmount= 10312,

            dAmount= 1455,

            cssAmount= 125,

            essAmount= 0,

            netSalary= 13545,

            edName= "بدل طبيعة عمل",

            edType= 1,

            isTaxable= false,

            edAmount= 2187

        },

        new SalaryItem() {

            departmentName= "الحسابات المالية",

            branchName= "الحسابات المالية",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

           basicAmount= 4688,

            eAmount= 10312,

            dAmount= 1455,

            cssAmount= 125,

            essAmount= 0,

            netSalary= 13545,

            edName= "بدل تغذية",

            edType= 1,

            isTaxable= false,

            edAmount= 1750

        },

        new SalaryItem() {

            departmentName= "الحسابات المالية",

            branchName= "الحسابات المالية",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 4688,

            eAmount= 10312,

            dAmount= 1455,

            cssAmount= 125,

            essAmount= 0,

            netSalary= 13545,

            edName= "خصم التاخيرات",

            edType= 2,

            isTaxable= false,

            edAmount= -255

        },

        new SalaryItem() {

            departmentName= "الحسابات المالية",

            branchName= "الحسابات المالية",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 4688,

            eAmount= 10312,

            dAmount= 1455,

            cssAmount= 125,

            essAmount= 0,

            netSalary= 13545,

            edName= "خصم الغياب دون إذن ",

            edType= 2,

            isTaxable= false,

            edAmount= -999

        },

        new SalaryItem() {

            departmentName= "حسابات المبيعات",

            branchName= "حسابات المبيعات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

           endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 14813,

            eAmount= 34887,

            dAmount= 4518,

            cssAmount= 395,

            essAmount= 0,

            netSalary= 45182,

            edName= "بدل السكن",

            edType= 1,

            isTaxable= true,

            edAmount= 4937

        },

        new SalaryItem() {

            departmentName= "حسابات المبيعات",

            branchName= "حسابات المبيعات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 14813,

            eAmount= 34887,

            dAmount= 4518,

            cssAmount= 395,

            essAmount= 0,

            netSalary= 45182,

            edName= "بدل الانتقال",

            edType= 1,

            isTaxable= false,

            edAmount= 7488

        },

        new SalaryItem() {

            departmentName= "حسابات المبيعات",

            branchName= "حسابات المبيعات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 14813,

            eAmount= 34887,

            dAmount= 4518,

            cssAmount= 395,

            essAmount= 0,

            netSalary= 45182,

            edName= "بدل اتصالات",

            edType= 1,

            isTaxable= false,

            edAmount= 2995

        },

        new SalaryItem() {

            departmentName= "حسابات المبيعات",

            branchName= "حسابات المبيعات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 14813,

            eAmount= 34887,

            dAmount= 4518,

            cssAmount= 395,

            essAmount= 0,

            netSalary= 45182,

            edName= "بدل اضافى",

            edType= 1,

            isTaxable= false,

            edAmount= 5990

        },

        new SalaryItem() {

            departmentName= "حسابات المبيعات",

            branchName= "حسابات المبيعات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 14813,

            eAmount= 34887,

            dAmount= 4518,

            cssAmount= 395,

            essAmount= 0,

            netSalary= 45182,

            edName= "بدل طبيعة عمل",

            edType= 1,

            isTaxable= false,

            edAmount= 7487

        },

        new SalaryItem() {

            departmentName= "حسابات المبيعات",

            branchName= "حسابات المبيعات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 14813,

            eAmount= 34887,

            dAmount= 4518,

            cssAmount= 395,

            essAmount= 0,

            netSalary= 45182,

            edName= "بدل تغذية",

            edType= 1,

            isTaxable= false,

            edAmount= 5990

        },

        new SalaryItem() {

            departmentName= "حسابات المبيعات",

            branchName= "حسابات المبيعات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

           endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 14813,

            eAmount= 34887,

            dAmount= 4518,

            cssAmount= 395,

            essAmount= 0,

            netSalary= 45182,

            edName= "خصم السلف",

            edType= 2,

            isTaxable= false,

            edAmount= 0

        },

        new SalaryItem() {

            departmentName= "حسابات المبيعات",

            branchName= "حسابات المبيعات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 14813,

            eAmount= 34887,

            dAmount= 4518,

            cssAmount= 395,

            essAmount= 0,

            netSalary= 45182,

            edName= "خصم التاخيرات",

            edType= 2,

            isTaxable= false,

            edAmount= -3918

        },

        new SalaryItem() {

            departmentName= "حسابات المبيعات",

            branchName= "حسابات المبيعات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 14813,

            eAmount= 34887,

            dAmount= 4518,

            cssAmount= 395,

            essAmount= 0,

            netSalary= 45182,

            edName= "خصم الغياب دون إذن ",

            edType= 2,

            isTaxable= false,

            edAmount= -499

        },

        new SalaryItem() {

            departmentName= "قسم تقنية ونظم المعلومات",

            branchName= "قسم تقنية ونظم المعلومات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 3375,

            eAmount= 8125,

            dAmount= 0,

            cssAmount= 90,

            essAmount= 0,

            netSalary= 11500,

            edName= "بدل السكن",

            edType= 1,

            isTaxable= true,

            edAmount= 1125

        },

        new SalaryItem() {

            departmentName= "قسم تقنية ونظم المعلومات",

            branchName= "قسم تقنية ونظم المعلومات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 3375,

            eAmount= 8125,

            dAmount= 0,

            cssAmount= 90,

            essAmount= 0,

            netSalary= 11500,

            edName= "بدل الانتقال",

            edType= 1,

            isTaxable= false,

            edAmount= 1750

        },

        new SalaryItem() {

            departmentName= "قسم تقنية ونظم المعلومات",

            branchName= "قسم تقنية ونظم المعلومات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 3375,

            eAmount= 8125,

            dAmount= 0,

            cssAmount= 90,

            essAmount= 0,

            netSalary= 11500,

            edName= "بدل اتصالات",

            edType= 1,

            isTaxable= false,

            edAmount= 700

        },

        new SalaryItem() {

            departmentName= "قسم تقنية ونظم المعلومات",

            branchName= "قسم تقنية ونظم المعلومات",

           payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 3375,

            eAmount= 8125,

            dAmount= 0,

            cssAmount= 90,

            essAmount= 0,

            netSalary= 11500,

            edName= "بدل اضافى",

            edType= 1,

            isTaxable= false,

            edAmount= 1400

        },

        new SalaryItem() {

            departmentName= "قسم تقنية ونظم المعلومات",

            branchName= "قسم تقنية ونظم المعلومات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 3375,

            eAmount= 8125,

            dAmount= 0,

            cssAmount= 90,

            essAmount= 0,

            netSalary= 11500,

            edName= "بدل طبيعة عمل",

            edType= 1,

            isTaxable= false,

            edAmount= 1750

        },

        new SalaryItem() {

            departmentName= "قسم تقنية ونظم المعلومات",

            branchName= "قسم تقنية ونظم المعلومات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 3375,

            eAmount= 8125,

            dAmount= 0,

            cssAmount= 90,

            essAmount= 0,

            netSalary= 11500,

            edName= "بدل تغذية",

            edType= 1,

            isTaxable= false,

            edAmount= 1400

        },

        new SalaryItem() {

            departmentName= "قسم تقنية ونظم المعلومات",

            branchName= "قسم تقنية ونظم المعلومات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 3375,

            eAmount= 8125,

            dAmount= 0,

            cssAmount= 90,

            essAmount= 0,

            netSalary= 11500,

            edName= "خصم السلف",

            edType= 2,

            isTaxable= false,

            edAmount= 0

        },

        new SalaryItem() {

            departmentName= "قسم تقنية ونظم المعلومات",

            branchName= "قسم تقنية ونظم المعلومات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 3375,

            eAmount= 8125,

            dAmount= 0,

            cssAmount= 90,

            essAmount= 0,

            netSalary= 11500,

            edName= "بدل السكن",

            edType= 1,

            isTaxable= true,

            edAmount= 562

        },

        new SalaryItem() {

            departmentName= "قسم تقنية ونظم المعلومات",

            branchName= "قسم تقنية ونظم المعلومات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 3375,

            eAmount= 8125,

            dAmount= 0,

            cssAmount= 90,

            essAmount= 0,

            netSalary= 11500,

            edName= "بدل الانتقال",

            edType= 1,

            isTaxable= false,

            edAmount= 562

        },

        new SalaryItem() {

            departmentName= "قسم تقنية ونظم المعلومات",

            branchName= "قسم تقنية ونظم المعلومات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 3375,

            eAmount= 8125,

            dAmount= 0,

            cssAmount= 90,

            essAmount= 0,

            netSalary= 11500,

            edName= "بدل اتصالات",

            edType= 1,

            isTaxable= false,

            edAmount= 225

        },

        new SalaryItem() {

            departmentName= "قسم تقنية ونظم المعلومات",

            branchName= "قسم تقنية ونظم المعلومات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 3375,

            eAmount= 8125,

            dAmount= 0,

            cssAmount= 90,

            essAmount= 0,

            netSalary= 11500,

            edName= "بدل اضافى",

            edType= 1,

            isTaxable= false,

            edAmount= 1450

        },

        new SalaryItem() {

            departmentName= "قسم تقنية ونظم المعلومات",

            branchName= "قسم تقنية ونظم المعلومات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 3375,

            eAmount= 8125,

            dAmount= 0,

            cssAmount= 90,

            essAmount= 0,

            netSalary= 11500,

            edName= "بدل طبيعة عمل",

            edType= 1,

            isTaxable= false,

            edAmount= 563

        },

        new SalaryItem() {

            departmentName= "قسم تقنية ونظم المعلومات",

            branchName= "قسم تقنية ونظم المعلومات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 3375,

            eAmount= 8125,

            dAmount= 0,

            cssAmount= 90,

            essAmount= 0,

            netSalary= 11500,

            edName= "بدل تغذية",

            edType= 1,

            isTaxable= false,

            edAmount= 450

        },

        new SalaryItem() {

            departmentName= "قسم تقنية ونظم المعلومات",

            branchName= "قسم تقنية ونظم المعلومات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 3375,

            eAmount= 8125,

            dAmount= 0,

            cssAmount= 90,

            essAmount= 0,

            netSalary= 11500,

            edName= "خصم السلف",

            edType= 2,

            isTaxable= false,

            edAmount= 0

        },

        new SalaryItem() {

            departmentName= "قسم تقنية ونظم المعلومات",

            branchName= "قسم تقنية ونظم المعلومات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 3375,

            eAmount= 8125,

            dAmount= 0,

            cssAmount= 90,

            essAmount= 0,

            netSalary= 11500,

            edName= "خصم التاخيرات",

            edType= 2,

            isTaxable= false,

            edAmount= -543

        },

        new SalaryItem() {

            departmentName= "قسم تقنية ونظم المعلومات",

            branchName= "قسم تقنية ونظم المعلومات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 3375,

            eAmount= 8125,

            dAmount= 0,

            cssAmount= 90,

            essAmount= 0,

            netSalary= 11500,

            edName= "خصم الغياب",

            edType= 2,

            isTaxable= false,

            edAmount= -183

        },

        new SalaryItem() {

            departmentName= "قسم تقنية ونظم المعلومات",

            branchName= "قسم تقنية ونظم المعلومات",

            payDate= new DateTime(2018,7,21),

            calendarDays= 0,

            endDate= new DateTime(2018,7,21),

            fiscalYear= 2018,

            startDate= new DateTime(2018,6,22),

            basicAmount= 3375,

            eAmount= 8125,

            dAmount= 0,

            cssAmount= 90,

            essAmount= 0,

            netSalary= 11500,

            edName= "خصم الغياب دون إذن ",

            edType= 2,

            isTaxable= false,

            edAmount= -366

        },

        //new SalaryItem() {

        //    departmentName= "قسم تقنية ونظم المعلومات",

        //    branchName= "قسم تقنية ونظم المعلومات",

        //    payDate= new DateTime(2018,7,21),

        //    calendarDays= 0,

        //    endDate= new DateTime(2018,7,21),

        //    fiscalYear= 2018,

        //    startDate= new DateTime(2018,6,22),

        //    basicAmount= 1688,

        //    eAmount= 3812,

        //    dAmount= 1092,

        //    cssAmount= 45,

        //    essAmount= 0,

        //    netSalary= 4408,

        //   edName= "بدل السكن",

        //    edType= 1,

        //    isTaxable= true,

        //    edAmount= 1125

        //},

        //new SalaryItem() {

        //    departmentName= "قسم تقنية ونظم المعلومات",

        //    branchName= "قسم تقنية ونظم المعلومات",

        //    payDate= new DateTime(2018,7,21),

        //    calendarDays= 0,

        //    endDate= new DateTime(2018,7,21),

        //    fiscalYear= 2018,

        //    startDate= new DateTime(2018,6,22),

        //    basicAmount= 1688,

        //    eAmount= 3812,

        //    dAmount= 1092,

        //    cssAmount= 45,

        //    essAmount= 0,

        //    netSalary= 4408,

        //    edName= "بدل الانتقال",

        //    edType= 1,

        //    isTaxable= false,

        //    edAmount= 1750

        //},

        //new SalaryItem() {

        //    departmentName= "قسم تقنية ونظم المعلومات",

        //    branchName= "قسم تقنية ونظم المعلومات",

        //    payDate= new DateTime(2018,7,21),

        //    calendarDays= 0,

        //    endDate= new DateTime(2018,7,21),

        //    fiscalYear= 2018,

        //    startDate= new DateTime(2018,6,22),

        //    basicAmount= 1688,

        //    eAmount= 3812,

        //    dAmount= 1092,

        //    cssAmount= 45,

        //    essAmount= 0,

        //    netSalary= 4408,

        //    edName= "بدل اتصالات",

        //    edType= 1,

        //    isTaxable= false,

        //    edAmount= 700

        //},

        //new SalaryItem() {

        //    departmentName= "قسم تقنية ونظم المعلومات",

        //    branchName= "قسم تقنية ونظم المعلومات",

        //    payDate= new DateTime(2018,7,21),

        //    calendarDays= 0,

        //    endDate= new DateTime(2018,7,21),

        //    fiscalYear= 2018,

        //    startDate= new DateTime(2018,6,22),

        //    basicAmount= 1688,

        //    eAmount= 3812,

        //    dAmount= 1092,

        //    cssAmount= 45,

        //    essAmount= 0,

        //    netSalary= 4408,

        //    edName= "بدل اضافى",

        //    edType= 1,

        //    isTaxable= false,

        //    edAmount= 1400

        //},

        //new SalaryItem() {

        //    departmentName= "قسم تقنية ونظم المعلومات",

        //    branchName= "قسم تقنية ونظم المعلومات",

        //    payDate= new DateTime(2018,7,21),

        //    calendarDays= 0,

        //    endDate= new DateTime(2018,7,21),

        //    fiscalYear= 2018,

        //    startDate= new DateTime(2018,6,22),

        //    basicAmount= 1688,

        //    eAmount= 3812,

        //    dAmount= 1092,

        //    cssAmount= 45,

        //    essAmount= 0,

        //    netSalary= 4408,

        //    edName= "بدل طبيعة عمل",

        //    edType= 1,

        //    isTaxable= false,

        //    edAmount= 1750

        //},

        //new SalaryItem() {

        //    departmentName= "قسم تقنية ونظم المعلومات",

        //    branchName= "قسم تقنية ونظم المعلومات",

        //    payDate= new DateTime(2018,7,21),

        //    calendarDays= 0,

        //    endDate= new DateTime(2018,7,21),

        //    fiscalYear= 2018,

        //    startDate= new DateTime(2018,6,22),

        //    basicAmount= 1688,

        //    eAmount= 3812,

        //    dAmount= 1092,

        //    cssAmount= 45,

        //    essAmount= 0,

        //    netSalary= 4408,

        //    edName= "بدل تغذية",

        //    edType= 1,

        //    isTaxable= false,

        //    edAmount= 1400

        //},

        //new SalaryItem() {

        //    departmentName= "قسم تقنية ونظم المعلومات",

        //    branchName= "قسم تقنية ونظم المعلومات",

        //    payDate= new DateTime(2018,7,21),

        //    calendarDays= 0,

        //    endDate= new DateTime(2018,7,21),

        //    fiscalYear= 2018,

        //    startDate= new DateTime(2018,6,22),

        //    basicAmount= 1688,

        //    eAmount= 3812,

        //    dAmount= 1092,

        //    cssAmount= 45,

        //    essAmount= 0,

        //    netSalary= 4408,

        //    edName= "خصم السلف",

        //    edType= 2,

        //    isTaxable= false,

        //    edAmount= 0

        //},

        //new SalaryItem() {

        //    departmentName= "قسم تقنية ونظم المعلومات",

        //    branchName= "قسم تقنية ونظم المعلومات",

        //    payDate= new DateTime(2018,7,21),

        //    calendarDays= 0,

        //    endDate= new DateTime(2018,7,21),

        //    fiscalYear= 2018,

        //    startDate= new DateTime(2018,6,22),

        //    basicAmount= 1688,

        //    eAmount= 3812,

        //    dAmount= 1092,

        //    cssAmount= 45,

        //    essAmount= 0,

        //    netSalary= 4408,

        //    edName= "بدل السكن",

        //    edType= 1,

        //    isTaxable= true,

        //    edAmount= 562

        //},

        //new SalaryItem() {

        //    departmentName= "قسم تقنية ونظم المعلومات",

        //    branchName= "قسم تقنية ونظم المعلومات",

        //    payDate= new DateTime(2018,7,21),

        //    calendarDays= 0,

        //    endDate= new DateTime(2018,7,21),

        //    fiscalYear= 2018,

        //    startDate= new DateTime(2018,6,22),

        //    basicAmount= 1688,

        //    eAmount= 3812,

        //    dAmount= 1092,

        //    cssAmount= 45,

        //    essAmount= 0,

        //    netSalary= 4408,

        //    edName= "بدل الانتقال",

        //    edType= 1,

        //    isTaxable= false,

        //    edAmount= 562

        //},

        //new SalaryItem() {

        //    departmentName= "قسم تقنية ونظم المعلومات",

        //    branchName= "قسم تقنية ونظم المعلومات",

        //    payDate= new DateTime(2018,7,21),

        //    calendarDays= 0,

        //    endDate= new DateTime(2018,7,21),

        //    fiscalYear= 2018,

        //    startDate= new DateTime(2018,6,22),

        //    basicAmount= 1688,

        //    eAmount= 3812,

        //    dAmount= 1092,

        //    cssAmount= 45,

        //    essAmount= 0,

        //    netSalary= 4408,

        //    edName= "بدل اتصالات",

        //    edType= 1,

        //    isTaxable= false,

        //    edAmount= 225

        //},

        //new SalaryItem() {

        //    departmentName= "قسم تقنية ونظم المعلومات",

        //    branchName= "قسم تقنية ونظم المعلومات",

        //    payDate= new DateTime(2018,7,21),

        //    calendarDays= 0,

        //    endDate= new DateTime(2018,7,21),

        //    fiscalYear= 2018,

        //    startDate= new DateTime(2018,6,22),

        //    basicAmount= 1688,

        //    eAmount= 3812,

        //    dAmount= 1092,

        //    cssAmount= 45,

        //    essAmount= 0,

        //    netSalary= 4408,

        //    edName= "بدل اضافى",

        //    edType= 1,

        //    isTaxable= false,

        //    edAmount= 1450

        //},

        //new SalaryItem() {

        //    departmentName= "قسم تقنية ونظم المعلومات",

        //    branchName= "قسم تقنية ونظم المعلومات",

        //    payDate= new DateTime(2018,7,21),

        //    calendarDays= 0,

        //    endDate= new DateTime(2018,7,21),

        //    fiscalYear= 2018,

        //    startDate= new DateTime(2018,6,22),

        //    basicAmount= 1688,

        //    eAmount= 3812,

        //    dAmount= 1092,

        //    cssAmount= 45,

        //    essAmount= 0,

        //    netSalary= 4408,

        //    edName= "بدل طبيعة عمل",

        //    edType= 1,

        //    isTaxable= false,

        //    edAmount= 563

        //},

        //new SalaryItem() {

        //    departmentName= "قسم تقنية ونظم المعلومات",

        //    branchName= "قسم تقنية ونظم المعلومات",

        //    payDate= new DateTime(2018,7,21),

        //    calendarDays= 0,

        //    endDate= new DateTime(2018,7,21),

        //    fiscalYear= 2018,

        //    startDate= new DateTime(2018,6,22),

        //    basicAmount= 1688,

        //    eAmount= 3812,

        //    dAmount= 1092,

        //    cssAmount= 45,

        //    essAmount= 0,

        //    netSalary= 4408,

        //    edName= "بدل تغذية",

        //    edType= 1,

        //    isTaxable= false,

        //    edAmount= 450

        //},

        //new SalaryItem() {

        //    departmentName= "قسم تقنية ونظم المعلومات",

        //    branchName= "قسم تقنية ونظم المعلومات",

        //    payDate= new DateTime(2018,7,21),

        //    calendarDays= 0,

        //    endDate= new DateTime(2018,7,21),

        //    fiscalYear= 2018,

        //    startDate= new DateTime(2018,6,22),

        //    basicAmount= 1688,

        //    eAmount= 3812,

        //    dAmount= 1092,

        //    cssAmount= 45,

        //    essAmount= 0,

        //    netSalary= 4408,

        //    edName= "خصم السلف",

        //    edType= 2,

        //    isTaxable= false,

        //    edAmount= 0

        //},

        //new SalaryItem() {

        //    departmentName= "قسم تقنية ونظم المعلومات",

        //    branchName= "قسم تقنية ونظم المعلومات",

        //    payDate= new DateTime(2018,7,21),

        //    calendarDays= 0,

        //    endDate= new DateTime(2018,7,21),

        //    fiscalYear= 2018,

        //    startDate= new DateTime(2018,6,22),

        //    basicAmount= 1688,

        //    eAmount= 3812,

        //    dAmount= 1092,

        //    cssAmount= 45,

        //    essAmount= 0,

        //    netSalary= 4408,

        //    edName= "خصم التاخيرات",

        //    edType= 2,

        //    isTaxable= false,

        //    edAmount= -543

        //},

        //new SalaryItem() {

        //    departmentName= "قسم تقنية ونظم المعلومات",

        //    branchName= "قسم تقنية ونظم المعلومات",

        //    payDate= new DateTime(2018,7,21),

        //    calendarDays= 0,

        //    endDate= new DateTime(2018,7,21),

        //    fiscalYear= 2018,

        //    startDate= new DateTime(2018,6,22),

        //    basicAmount= 1688,

        //    eAmount= 3812,

        //    dAmount= 1092,

        //    cssAmount= 45,

        //    essAmount= 0,

        //    netSalary= 4408,

        //    edName= "خصم الغياب",

        //    edType= 2,

        //    isTaxable= false,

        //    edAmount= -183

        //},

        //new SalaryItem() {

        //    departmentName= "قسم تقنية ونظم المعلومات",

        //    branchName= "قسم تقنية ونظم المعلومات",

        //    payDate= new DateTime(2018,7,21),

        //    calendarDays= 0,

        //    endDate= new DateTime(2018,7,21),

        //    fiscalYear= 2018,

        //    startDate= new DateTime(2018,6,22),

        //    basicAmount= 1688,

        //    eAmount= 3812,

        //    dAmount= 1092,

        //    cssAmount= 45,

        //    essAmount= 0,

        //    netSalary= 4408,

        //    edName= "خصم الغياب دون إذن ",

        //    edType= 2,

        //    isTaxable= false,

        //    edAmount= -366

        //}

            };
        }
    }
}

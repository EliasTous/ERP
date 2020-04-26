using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{

    public  static partial class ClassId
    {
        public const int NQIN = 10101;  // Industry
        public const int NQBS = 10102;  // Business Size
        public const int NQLE = 10103;  // Levels
        public const int NQCI = 10104;  // Citizenship
        public const int NQPA = 10105;  // point aquisition
        public const int NQLA = 10106;  // level aquisition

        public const int SYFO = 20101;  // folders
        public const int SYUS = 20102;  // users
        public const int SYCU = 20103;  // currencies
        public const int SYDT = 20104;  // document type
        public const int SYNA = 20105;  // nationalities
        public const int SYST = 20106;  // state
        public const int SYGO = 20107;  // gov org

        public const int SYRW = 20201;  // right to work
        public const int SYAA = 20202;  // auto alert
        public const int SYDE = 20203;  // defaults

        public const int CSDI = 21101;  // divisions
        public const int CSBR = 21102;  // branches
        public const int CSPO = 21103;  // positions
        public const int CSDE = 21104;  // departments
        public const int CSAP = 21105;  // approvals

        public const int DMDT = 22101;  // document types
        public const int DMDO = 22201;  // documents

        public const int BEBE = 25101;  // benefits
        public const int BESC = 25102;  // schedules

        public const int BEBA = 25201;  // aquisition

        public const int EPBT = 31101;  // bonus types
        public const int EPCL = 31102;  // certificates levels
        public const int EPCT = 31103;  // check types
        public const int EPDT = 31104;  // document types
        public const int EPSC = 31105;  // salary change reason
        public const int EPED = 31106;  // entitlements and deductions
        public const int EPST = 31107;  // employment status type
        public const int EPSP = 31108;  // sponsors
        public const int EPRT = 31109;  // relation types
        public const int EPTR = 31110;  // termination reason
        public const int EPNP = 31111;  // notice period

        public const int EPEM = 31201;  // employee record
        public const int EPBC = 31202;  // background check
        public const int EPBO = 31203;  // bonus
        public const int EPCE = 31204;  // certificates
        public const int EPDO = 31205;  // documents
        public const int EPSA = 31206;  // salary
        public const int EPJI = 31207;  // job info
        public const int EPNO = 31208;  // notes
        public const int EPIM = 31209;  // IMPORT EMPLOYEE NOTES
        public const int EPRW = 31210;  // right to work
        public const int EPSH = 31211;  // sponsor history
        public const int EPEC = 31212;  // emergency contacts
        public const int EPCO = 31213;  // contact
        public const int EPTE = 31214;  // termination
        public const int EPRE = 31215;  // hire info
        public const int EPDE = 31216;  // dependents
        public const int EPPE = 31217;  // penalty

        public const int TABM = 41101;  // biometric device
        public const int TARO = 41102;  // router
        public const int TAGF = 41103;  // geofence
        public const int TADT = 41104;  // day type
        public const int TASC = 41105;  // schedule
        public const int TACA = 41106;  // calendar

        public const int TAFS = 41201;  // flat schedule
        public const int TAAD = 41202;  // attendance day
        public const int TATV = 41203;  // time variation
        public const int TATA = 41204;  // time approval
        public const int TAIM = 41205;  // import time attendance

        public const int LMLT = 42101;  // leave type     
        public const int LMVS = 42102;  // vacation schedule
        public const int LMLS = 42103;  // leave schedule

        public const int LMLR = 42201;  // leave requests
        public const int LMIM = 42202;  // import leaves

        public const int LTLT = 45101;  // loan type

        public const int LTLR = 45201;  // loan request
        public const int LTLD = 45202;  // loan deductions
        public const int LTIM = 45203;  // import loan

        public const int PYYE = 51101;  // fiscal years
        public const int PYTC = 51102;  // time codes
        public const int PYSS = 51103;  // social security schedules
        public const int PYPC = 51104;  // pay codes 
        public const int PYIS = 51105;  // indemnity schedules
        public const int PYPT = 51106;  // penalty types
        public const int PYBA = 51107;  // banks

        public const int PYHE = 51201; // generate payroll
        public const int PYLP = 51202; // leave payment
        public const int PYFS = 51203; // final settlement

        public const int SSEM = 60101; // personal info
        public const int SSFS = 60102; // flat schedule
        public const int SSAT = 60103; // attendance
        public const int SSTV = 60104; // time variation
        public const int SSTA = 60105; // time approval
        public const int SSLR = 60106; // leave request
        public const int SSLO = 60107; // loan request
        public const int SSPE = 60108; // Penalty
        public const int SSPY = 60109; // Payroll
        public const int SSUS = 60110; // reset password

        public const int AATE = 70101; // template
        public const int AAPN = 70102; // process notification
        public const int AABC = 70103; // business partner category
        public const int AADC = 70104; // document category

        public const int AABP = 70201; // business partner 
        public const int AADO = 70202; // document 

        public const int RT101 = 80101; // age breakdown
        public const int RT102 = 80102; // hiring and termination
        public const int RT103 = 80103; // head count
        public const int RT104 = 80104; // years in service
        public const int RT105 = 80105; // Job history
        public const int RT106 = 80106; // turnover rate
        public const int RT107 = 80107; // missing info
        public const int RT108 = 80108; // employee details
        public const int RT109 = 80109; // employee right-to-work
        public const int RT111 = 80110; // bank accounts
        public const int RT112 = 80111; // employee notes
        public const int RT113 = 80112; // branch workforce
        public const int RT114 = 80113; // skills

        public const int RT200 = 80201; // current salaries
        public const int RT201 = 80202; // salary history
        public const int RT202 = 80203; // last salary change
        public const int RT203 = 80204; // point-in-time salary 

        public const int RT302 = 80301; // attendance summary
        public const int RT303 = 80302; // detailed attendance
        public const int RT305 = 80303; // time variations
        public const int RT306 = 80304; // time approvals
        public const int RT307 = 80305; // approver performance

        public const int RT401 = 80401; // loan listing
        public const int RT402 = 80402; // statement of loan

        public const int RT501 = 80501; // payroll report
        public const int RT502 = 80502; // period time codes
        public const int RT503 = 80503; // job info payroll
        public const int RT504 = 80504; // bank transfer

        public const int RT601 = 80601; // leave list
        public const int RT602 = 80602; // leave balance

        public const int RT802 = 80701; // audit trail
        public const int RT803 = 80702; // users
        public const int RT804 = 80703; // users access

        public const int DASH = 81101;  // dashboard desktop

        public const int ALSG = 90101;  // security groups

        public static short moduleId(int _classId)
        {
            short _ = (short)(_classId / 1000);
            return _;
        }

      
        public const int SYSI = 20011;  // sign-in
       
        public const int SYAT = 20050;  // attachments
        public const int SYTL = 20060;  // transaction log
        public const int SYAB = 20070;  // address book
     

        

        // media gallery
        public const int MGMC = 23010;
        public const int MGME = 23020;

        // company news
        public const int CNNW = 24010;

        // recruitement
        public const int REAA = 30;
        public const int REAI = 30;
        public const int REAP = 30;
        public const int REAS = 30;
        public const int REOO = 30;
        public const int REOP = 30;
        public const int REOS = 30;
        public const int RERE = 30;
        public const int RESN = 30;

        // **************************************************** EMPLOYEE PROFILE

    

        // asset
        public const int EPAC = 31010;
        public const int EPAA = 31011;

     

     
        public const int EPSD = 31063;  // salary detail

       
        public const int EPEH = 31071;  // employment 

        // job info
    

        // sponsor
      

        //********************************** task management

        public const int TMTT = 32000;  // task type
        public const int TMTA = 32010;  // task
        public const int TMTC = 32020;  // task comments

        //********************************** TIME ATTENDANCE

    
        // device
        public const int TADE = 41040;

        // schedule
    
        public const int TASD = 41051;  // sched days
        public const int TASB = 41052;  // sched breaks

        // attendance
        public const int TACH = 41060;  // punches
        public const int TAAS = 41061;  // shifts
     

       
        // calendar
  
        public const int TACY = 41071;  // years
        public const int TACD = 41072;  // days


        public const int TAOT = 41080;
        // leave management
        
      
        public const int LMVP = 42012;  // schedule period
     
        public const int LMLD = 42021;  // leave day

        // case management
        public const int CMCA = 43000;  // case 
        public const int CMCC = 43001;  // case comments

        // complaints
        public const int ECCO = 44000;

        // loans
       
        public const int LTLC = 45011;  // comments

        // payroll ***************************************************************************

       
        public const int PYPE = 51011; // fiscal period
       
        public const int PYEM = 51021; // payroll employee
        public const int PYED = 51022; // payroll employee entitlements and deductions
      


        

        // reports ***************************************************************************

        // employee reports
     

        // attendance reports
        public const int RT301 = 80301; // daily attendance

        // leave reports
     

        // company reports
        public const int RT801 = 80801; // sign-in trail
     

        // dash board ***************************************************************************

      
        public const int ALUS = 90102;  // security groups users 
        public const int ALCL = 90103;  // security groups class access
        public const int ALCP = 90104;  // security groups class property access
        public const int ALUC = 90104;  // use / class access

    




    }

    public static class TransactionTypes
    {
        public const short ADD = 1;
        public const short EDIT = 2;
        public const short DEL = 3;
        public const short EXEC = 4;
        public const short SET_ARRAY = 5;
        // reading
        public const short GET = 11;
        public const short QRY = 12;
        // mail
        public const short MAIL = 21;

    }

   

}

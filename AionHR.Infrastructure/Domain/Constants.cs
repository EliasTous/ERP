using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Infrastructure.Domain
{

    public  static partial class ClassId
    {
        public const int SYFO = 20000;  // folders
        public const int SYUS = 20010;  // users
        public const int SYSI = 20011;  // sign-in
        public const int SYCU = 20020;  // currencies
        public const int SYDE = 20030;  // defaults
        public const int SYNA = 20040;  // nationalities
        public const int SYAT = 20050;  // attachments
        public const int SYTL = 20060;  // transaction log
        public const int SYAB = 20070;  // address book
        public const int SYST = 20071;  // address book
        public const int SYDT = 20080;  // document type
        public const int SYRW = 20081;  // right to work
        public const int SYAA = 20090;  // auto alert

        // company structure
        public const int CSDI = 21010;  // divisions
        public const int CSBR = 21020;  // branches
        public const int CSPO = 21030;  // positions
        public const int CSDE = 21040;  // departments

        // document management (company level)
        public const int DMDT = 22010;
        public const int DMDO = 22020;

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

        // master
        public const int EPEM = 31000;

        // asset
        public const int EPAC = 31010;
        public const int EPAA = 31011;

        // background check
        public const int EPBC = 31020;
        public const int EPCT = 31021;

        // bonus
        public const int EPBT = 31030;
        public const int EPBO = 31031;

        // skills
        public const int EPCL = 31040;
        public const int EPCE = 31041;

        // documents
        public const int EPDT = 31050;
        public const int EPDO = 31051;

        // salary
        public const int EPSC = 31060;  // salary change reason
        public const int EPED = 31061;  // entitlements and deductions
        public const int EPSA = 31062;  // salary
        public const int EPSD = 31063;  // salary detail

        // employment
        public const int EPST = 31070;  // status type
        public const int EPEH = 31071;  // employment 

        // job info
        public const int EPJI = 31080;

        // notes
        public const int EPNO = 31090;

        // right to work
        public const int EPRW = 31100;

        // sponsor
        public const int EPSP = 31110;  // sponsor address
        public const int EPSH = 31111;  // sponsor history

        // emergency
        public const int EPRT = 31120;  // relation type
        public const int EPEC = 31121;  // emergency contact
        public const int EPCO = 31122;  // contact

        // termination
        public const int EPTR = 31130;  // termination reason
        public const int EPTE = 31131;  // termination

        // recruitment info
        public const int EPNP = 31140;  // notice period
        public const int EPRE = 31141;  // recruitment info

        //********************************** task management

        public const int TMTT = 32000;  // task type
        public const int TMTA = 32010;  // task
        public const int TMTC = 32020;  // task comments

        //********************************** TIME ATTENDANCE

        // biometric
        public const int TABM = 41000;

        // router
        public const int TARO = 41010;

        // geofence
        public const int TAGF = 41020;

        // day type
        public const int TADT = 41030;

        // device
        public const int TADE = 41040;

        // schedule
        public const int TASC = 41050;  // schedule
        public const int TASD = 41051;  // sched days
        public const int TASB = 41052;  // sched breaks

        // attendance
        public const int TACH = 41060;  // punches
        public const int TAAS = 41061;  // shifts
        public const int TAAD = 41062;  // days


        // calendar
        public const int TACA = 41070;  // header
        public const int TACY = 41071;  // years
        public const int TACD = 41072;  // days

        // leave management
        public const int LMLT = 42000;  // leave type     
        public const int LMVS = 42010;  // scehdule
        public const int LMVP = 42012;  // schedule period
        public const int LMLR = 42020;  // leave
        public const int LMLD = 42021;  // leave day

        // case management
        public const int CMCA = 43000;  // case 
        public const int CMCC = 43001;  // case comments

        // complaints
        public const int ECCO = 44000;

        // loans
        public const int LTLT = 45000;  // loan type
        public const int LTLR = 45050;  // request
        public const int LTLC = 45011;  // comments

        // payroll ***************************************************************************

        public const int PYYE = 51010; // fiscal year
        public const int PYPE = 51011; // fiscal period
        public const int PYHE = 51020; // payroll header
        public const int PYEM = 51021; // payroll employee
        public const int PYED = 51022; // payroll employee entitlements and deductions

        // reports ***************************************************************************

        // employee reports
        public const int RT101 = 80101; // age breakdown
        public const int RT102 = 80102; // hiring and termination
        public const int RT103 = 80103; // head count
        public const int RT104 = 80104; // years in service
        public const int RT105 = 80105; // Job history
        public const int RT106 = 80106; // turnover rate

        // salary reports
        public const int RT201 = 80201; // salary history
        public const int RT202 = 80202; // last salary change
        public const int RT203 = 80203; // point-in-time salary 

        // attendance reports
        public const int RT301 = 80301; // daily attendance

        // leave reports
        public const int RT601 = 80601; // 

        // company reports
        public const int RT801 = 80801; // sign-in trail
        public const int RT802 = 80802; // audit trail

        // dash board ***************************************************************************

        public const int ALSG = 90101;  // security groups
        public const int ALUS = 90102;  // security groups users 
        public const int ALCL = 90103;  // security groups class access
        public const int ALCP = 90104;  // security groups class property access
        public const int ALUC = 90104;  // use / class access

        public const int DASH = 81100;  // dashboard

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Infrastructure.Domain
{

    public static class ClassId
    {
        public const int SY = 20; // system
        public const int CS = 21; // company structure
        public const int DM = 22; // documents
        public const int MG = 23; // media
        public const int CN = 24; // news
        public const int RE = 30; // recruitment
        public const int EP = 31; // employees
        public const int TM = 32; // tasks
        public const int TA = 41; // attendance
        public const int LM = 42; // leave
        public const int CM = 43; // cases
        public const int EC = 44; // complaints
        public const int LO = 45; // loans
        public const int RT = 80; // reports

        // system
        public const int SYUS = 20010;
        public const int SYCU = 20020;
        public const int SYDE = 20030;
        public const int SYNA = 20040;
        public const int SYAT = 20050;
        public const int SYTL = 20060;

        // company structure
        public const int CSDI = 21010;
        public const int CSBR = 21020;
        public const int CSPO = 21030;
        public const int CSDE = 21040;
        public const int CSFI = 20080;

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
        public const int EPSC = 31060;
        public const int EPED = 31061;
        public const int EPSA = 31062;
        public const int EPSD = 31063;

        // employment
        public const int EPST = 31070;
        public const int EPEH = 31071;

        // job info
        public const int EPJI = 31080;

        // notes
        public const int EPNO = 31090;

        // right to work
        public const int EPRW = 31100;

        // sponsor
        public const int EPSP = 31110;
        public const int EPSH = 31111;

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
        public const int TASC = 41050;
        public const int TASD = 41051;
        public const int TASB = 41052;

        // attendance
        public const int TACH = 41060;
        public const int TAAS = 41061;
        public const int TAAD = 41062;

        // calendar
        public const int TACA = 41070;
        public const int TACY = 41071;
        public const int TACD = 41072;

        // leave type
        public const int LMLT = 42000;

        // scehdule
        public const int LMVS = 42010;
        public const int LMVP = 42012;

        // leave
        public const int LMLR = 42020;

        // case management
        public const int CMCA = 43000;
        public const int CMCC = 43001;

        // complaints
        public const int ECCO = 44000;

        // Task Management
        public const int TMTA = 32010;

        
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

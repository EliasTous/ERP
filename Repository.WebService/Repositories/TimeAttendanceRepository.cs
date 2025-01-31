﻿using Infrastructure.Configuration;
using Infrastructure.Domain;
using Model;
using Model.Attendance;
using Model.Dashboard;
using Model.HelpFunction;
using Model.Payroll;
using Model.TimeAttendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.WebService.Repositories
{
    public class TimeAttendanceRepository:Repository<IEntity,string>, ITimeAttendanceRepository
    {
        private string serviceName = "TA.asmx/";

        public TimeAttendanceRepository()
        {
            base.ServiceURL = ApplicationSettingsFactory.GetApplicationSettings().BaseURL + serviceName;

            ChildGetLookup.Add(typeof(DayType), "getDT");
            ChildGetLookup.Add(typeof(AttendanceSchedule), "getSC");
            ChildGetLookup.Add(typeof(AttendanceScheduleDay), "getSD");
            ChildGetLookup.Add(typeof(WorkingCalendar), "getCA");
            ChildGetLookup.Add(typeof(CalendarYear), "getCY");
            ChildGetLookup.Add(typeof(CalendarDay), "getCD");
            ChildGetLookup.Add(typeof(BiometricDevice), "getBM");
            ChildGetLookup.Add(typeof(Router), "getRO");
            ChildGetLookup.Add(typeof(Geofence), "getGF");
            ChildGetLookup.Add(typeof(Time), "getTA");
            ChildGetLookup.Add(typeof(DashBoardTimeVariation), "getTV");
            ChildGetLookup.Add(typeof(MonthlyLatenessPeriod), "getMLP");



            ChildGetAllLookup.Add(typeof(DayType), "qryDT");
            ChildGetAllLookup.Add(typeof(AttendanceSchedule), "qrySC");
            ChildGetAllLookup.Add(typeof(AttendanceScheduleDay), "qrySD");
            ChildGetAllLookup.Add(typeof(AttendanceBreak), "qrySB");
            ChildGetAllLookup.Add(typeof(WorkingCalendar), "qryCA");
            ChildGetAllLookup.Add(typeof(CalendarYear), "qryCY");
            ChildGetAllLookup.Add(typeof(CalendarDay), "qryCD");
            ChildGetAllLookup.Add(typeof(BiometricDevice), "qryBM");
            ChildGetAllLookup.Add(typeof(AttendanceDay), "qryAD");
            ChildGetAllLookup.Add(typeof(CheckMonitor), "qryCM");
            ChildGetAllLookup.Add(typeof(ActiveCheck), "qryAC");
            ChildGetAllLookup.Add(typeof(ActiveAbsence), "qryAA");
            ChildGetAllLookup.Add(typeof(ActiveLate), "qryAL");
            ChildGetAllLookup.Add(typeof(ActiveLeave), "qryAV");
            ChildGetAllLookup.Add(typeof(ActiveOut), "qryAO");
            ChildGetAllLookup.Add(typeof(MissedPunch), "qryMP");
            ChildGetAllLookup.Add(typeof(Router), "qryRO");
            ChildGetAllLookup.Add(typeof(Geofence), "qryGF");
            ChildGetAllLookup.Add(typeof(AttendanceShift), "qryAS");
            ChildGetAllLookup.Add(typeof(LeaveCalendarDay), "qryCD2");
            ChildGetAllLookup.Add(typeof(OvertimeSetting), "qryOT");
            ChildGetAllLookup.Add(typeof(FlatSchedule), "qryFS");
            ChildGetAllLookup.Add(typeof(PendingTA), "pendingTA");
            ChildGetAllLookup.Add(typeof(Time), "qryTA");
            ChildGetAllLookup.Add(typeof(DashBoardTimeVariation), "qryTV");
            ChildGetAllLookup.Add(typeof(DashBoardTimeVariation2), "qryMLD");
            ChildGetAllLookup.Add(typeof(PendingPunch), "qryPP");
            ChildGetAllLookup.Add(typeof(TimePerformance), "qryTP");
            ChildGetAllLookup.Add(typeof(FlatPunch), "qryFP");
            ChildGetAllLookup.Add(typeof(UnschedulePunch), "qryUP");
            ChildGetAllLookup.Add(typeof(UnschedulePunchDetails), "qryUPD");
            ChildGetAllLookup.Add(typeof(MonthlyLateness), "qryML");
            ChildGetAllLookup.Add(typeof(MonthlyLatenessPeriod), "getMLP");
            ChildGetAllLookup.Add(typeof(DashBoardTimeVariation3), "qryATV");
            ChildGetAllLookup.Add(typeof(RT308A), "qryCH");


            ChildAddOrUpdateLookup.Add(typeof(DayType), "setDT");
            ChildAddOrUpdateLookup.Add(typeof(AttendanceSchedule), "setSC");
            ChildAddOrUpdateLookup.Add(typeof(AttendanceScheduleDay), "setSD");
            ChildAddOrUpdateLookup.Add(typeof(AttendanceBreak[]), "arrSB");
            ChildAddOrUpdateLookup.Add(typeof(CalendarDay[]), "arrCD");
            ChildAddOrUpdateLookup.Add(typeof(WorkingCalendar), "setCA");
            ChildAddOrUpdateLookup.Add(typeof(CalendarYear), "setCY");
            ChildAddOrUpdateLookup.Add(typeof(CalendarDay), "setCD");
            ChildAddOrUpdateLookup.Add(typeof(BiometricDevice), "setBM");
            ChildAddOrUpdateLookup.Add(typeof(CalendarPattern), "batCD");
            ChildAddOrUpdateLookup.Add(typeof(SchedulePattern), "batSD");
            ChildAddOrUpdateLookup.Add(typeof(Router), "setRO");
            ChildAddOrUpdateLookup.Add(typeof(Geofence), "setGF");
            ChildAddOrUpdateLookup.Add(typeof(AttendanceShift), "setAS");
            ChildAddOrUpdateLookup.Add(typeof(AttendanceShift[]), "arrAS");
            ChildAddOrUpdateLookup.Add(typeof(Check), "setCH");
            ChildAddOrUpdateLookup.Add(typeof(CalendarAlternation), "altCD");
            ChildAddOrUpdateLookup.Add(typeof(OvertimeSetting), "setOT");
            ChildAddOrUpdateLookup.Add(typeof(GenerateAttendanceDay), "genFS");
            ChildAddOrUpdateLookup.Add(typeof(FlatSchedule), "setFS");
            ChildAddOrUpdateLookup.Add(typeof(Time), "setTA");
            ChildAddOrUpdateLookup.Add(typeof(PendingPunch), "processPP");
            ChildAddOrUpdateLookup.Add(typeof(DashBoardTimeVariation), "setTV");
            ChildAddOrUpdateLookup.Add(typeof(RejectTimeVariationc), "setRTV");
            ChildAddOrUpdateLookup.Add(typeof(MailFlatShedule), "mailFS");
            ChildAddOrUpdateLookup.Add(typeof(OverrideTimeVariation), "overrideTV");
            ChildAddOrUpdateLookup.Add(typeof(processUnscheduledPunch), "processUP");
            ChildAddOrUpdateLookup.Add(typeof(SyncActivity), "syncTV");
            ChildAddOrUpdateLookup.Add(typeof(ResetFlatSchedule), "resFS");
            ChildAddOrUpdateLookup.Add(typeof(GenerationHeader), "genML");

            ChildDeleteLookup.Add(typeof(AttendanceBreak), "delSB");
            ChildDeleteLookup.Add(typeof(AttendanceSchedule), "delSC");
            ChildDeleteLookup.Add(typeof(WorkingCalendar), "delCA");
            ChildDeleteLookup.Add(typeof(DayType), "delDT");
            ChildDeleteLookup.Add(typeof(BiometricDevice), "delBM");
            ChildDeleteLookup.Add(typeof(Router), "delRO");
            ChildDeleteLookup.Add(typeof(Geofence), "delGF");
            ChildDeleteLookup.Add(typeof(AttendanceShift), "delAS");
            ChildDeleteLookup.Add(typeof(OvertimeSetting), "delOT");
            ChildDeleteLookup.Add(typeof(FlatSchedule), "delFS");
            ChildDeleteLookup.Add(typeof(FlatScheduleRange), "delRangeFS");
            ChildDeleteLookup.Add(typeof(PendingPunch), "delPP");


            //Flat Schedule



        }
    }
}

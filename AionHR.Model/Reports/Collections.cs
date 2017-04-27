using System.Collections;
using System.ComponentModel;
namespace AionHR.Model.Reports
{

    public class AttendanceCollection : ArrayList, ITypedList
    {
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return TypeDescriptor.GetProperties(typeof(Attendance));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "Attendances";
        }
    }
    public class DaysCollection : ArrayList, ITypedList
    {
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return TypeDescriptor.GetProperties(typeof(Day));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "Days";
        }
    }

    public class MonthlyEmployeeAttendanceCollection : ArrayList, ITypedList
    {
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors != null && listAccessors.Length > 0)
            {
                PropertyDescriptor listAccessor = listAccessors[listAccessors.Length - 1];
                if (listAccessor.PropertyType.Equals(typeof(EmployeeAttendanceCollection)))
                    return TypeDescriptor.GetProperties(typeof(EmployeeAttendances));
                else if (listAccessor.PropertyType.Equals(typeof(DaysCollection)))
                    return TypeDescriptor.GetProperties(typeof(Day));

                else if (listAccessor.PropertyType.Equals(typeof(AttendanceCollection)))
                    return TypeDescriptor.GetProperties(typeof(Attendance));

            }
            return TypeDescriptor.GetProperties(typeof(MonthAttendance));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "MonthlyEmployeeAttendance";
        }
    }

    public class EmployeeAttendanceCollection : ArrayList, ITypedList
    {
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors != null && listAccessors.Length > 0)
            {
                PropertyDescriptor listAccessor = listAccessors[listAccessors.Length - 1];
                if (listAccessor.PropertyType.Equals(typeof(AttendanceCollection)))
                    return TypeDescriptor.GetProperties(typeof(Attendance));

            }
            return TypeDescriptor.GetProperties(typeof(EmployeeAttendances));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "EmployeeAttendances";
        }
    }
}
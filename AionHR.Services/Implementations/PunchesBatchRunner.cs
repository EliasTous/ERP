using Infrastructure.Domain;
using Infrastructure.Session;
using Infrastructure.Tokens;
using Model.Attendance;
using Model.Employees.Profile;
using Model.System;
using Model.TimeAttendance;
using Services.Interfaces;
using Services.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
   public class PunchesBatchRunner: ImportBatchRunner<Check>
    {
        ITimeAttendanceService timeAttendance;
        IEmployeeService employee; 
        Dictionary<string, int> udId=new Dictionary<string, int>();
        string UnknownError;




        public PunchesBatchRunner(ISessionStorage store, IEmployeeService employee, ISystemService system, ITimeAttendanceService timeAttendance,string UnknownError) :base(system, employee)
        {
            this.timeAttendance = timeAttendance;
            this.UnknownError = UnknownError;
            this.employee = employee;
            this.SessionStore = store;
            SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());


            BatchStatus = new BatchOperationStatus() { classId = ClassId.TAIM, processed = 0, tableSize = 0, status = 0 };
            errors = new List<Check>();
            udId = new Dictionary<string, int>();
         
            //this.InactivePref = InactivePref;
            //this.NameFormat = NameFormat;
            FillUdId();

            //FillCaId();

        }

        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in errors)
            {
                if (errorMessages[i] != null)
                    b.AppendLine(error.employeeRef + "," + error.clockStamp + "," + error.udId + "," + errorMessages[i++].Replace('\r', ' ').Replace(',', ';'));
                else
                {
                    i++;
                    b.AppendLine(error.employeeRef + "," + error.clockStamp + "," + error.udId);
                }

            }


            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".txt";


            File.WriteAllText(path, csv.ToString(),Encoding.UTF8);
        }

        protected override void PreProcessElement(Check item)
        {
            //if (udId.ContainsKey(item.udIdRef))
            //    item.udId = udId[item.udIdRef].ToString();
            //else
            //    item.udId = item.udIdRef; 

            //item.udIdRef = null;
            
        
         
          
        }

        protected override void ProcessElement(Check item)
        {
            PostRequest<Check> req = new PostRequest<Check>();
            req.entity = item;

            PostResponse<Check> resp =timeAttendance.ChildAddOrUpdate<Check>(req);
            if (!resp.Success)
            {
                if (!string.IsNullOrEmpty(resp.Error))
                  errorMessages.Add(resp.Error);
                              
                else
                    errorMessages.Add(UnknownError);
                errors.Add(item);

            }
        }
        private void FillUdId()
        {
            try
            {
                ListRequest request = new ListRequest();

                request.Filter = "";
                ListResponse<BiometricDevice> BdResponse = timeAttendance.ChildGetAll<BiometricDevice>(request);
                BdResponse.Items.ForEach(x =>
                {
                    if (!this.udId.ContainsKey(x.reference))


                        this.udId.Add(x.reference, Convert.ToInt32(x.recordId));
                });
            }
            catch { }
        }
        //private void FillEmployeeId()
        //{
        //    EmployeeListRequest empRequest = GetEmployeeRequest();




        //    ListResponse<Employee> emps = employee.GetAll<Employee>(empRequest);
        //    emps.Items.ForEach(x => this.employeeId.Add(x.reference, Convert.ToInt32(x.recordId)));
        //}
        //private EmployeeListRequest GetEmployeeRequest()
        //{
          
        //    EmployeeListRequest empRequest = new EmployeeListRequest();

        //    empRequest.IncludeIsInactive =Convert.ToInt32( InactivePref);
            

          
        //    empRequest.BranchId =  "0";
        //    empRequest.DepartmentId = "0";
        //    empRequest.filterField = "0";
        //    //empRequest.PositionId = d.PositionId.HasValue ? d.PositionId.Value.ToString() : "0";
        //    //empRequest.DivisionId = d.DivisionId.HasValue ? d.DivisionId.Value.ToString() : "0";



        //    empRequest.SortBy = NameFormat;
        
        //        empRequest.SortBy = "reference";
          
            
        //        empRequest.Size = "100000";
        //        empRequest.StartAt = "0";




        //    empRequest.Filter = "";

        //    return empRequest;
        //}
       


    }
}

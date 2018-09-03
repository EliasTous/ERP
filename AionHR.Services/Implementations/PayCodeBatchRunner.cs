using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.Session;
using AionHR.Infrastructure.Tokens;
using AionHR.Model.Company.Structure;
using AionHR.Model.Employees.Profile;
using AionHR.Model.Payroll;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AionHR.Services.Implementations
{
   public class PayCodeBatchRunner : ImportBatchRunner<PayCode>
    {
        private IPayrollService _payrollService;
        private Dictionary<string, string> payCodes;
     
        public PayCodeBatchRunner(ISessionStorage store, ISystemService system, ICompanyStructureService main, IPayrollService payrollService) : base(system, main)
        {
            this.SessionStore = store;
            SessionHelper h = new SessionHelper(store, new APIKeyBasedTokenGenerator());



            BatchStatus = new BatchOperationStatus() { classId = ClassId.PYPC, processed = 0, tableSize = 0, status = 0 };
            _payrollService = payrollService;
            payCodes = new Dictionary<string, string>();
        

        }

        protected override void PostProcessElements()
        {
            StringBuilder b = new StringBuilder();
            int i = 0;
            foreach (var error in errors)
            {
                b.AppendLine(error.payCode + ","
                    + error.name + "," +

                    errorMessages[i++].Replace('\r', ' ').Replace(',', ';')

                    );

            }
            string csv = b.ToString();
            string path = OutputPath + BatchStatus.classId.ToString() + ".csv";


            File.WriteAllText(path, csv.ToString());
        }

        protected override void PreProcessElement(PayCode item)
        {
           
          
        }

       

        protected override void ProcessElement(PayCode item)
        {
            PostRequest<PayCode> req = new PostRequest<PayCode>();

            req.entity = item;

            PostResponse<PayCode> resp = _payrollService.ChildAddOrUpdate<PayCode>(req);

            if (!resp.Success)
            {
                errors.Add(item);
                errorMessages.Add(resp.Summary);
                return;
            }
          
        }

    }

}

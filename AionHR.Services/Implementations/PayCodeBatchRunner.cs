using Infrastructure.Domain;
using Infrastructure.Session;
using Infrastructure.Tokens;
using Model.Company.Structure;
using Model.Employees.Profile;
using Model.Payroll;
using Model.System;
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

using AionHR.Model.AdminTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging
{
public class TemplateBodyListReuqest:ListRequest
    {
        public int TemplateId { get; set; }

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_teId", TemplateId.ToString());
                

                return parameters;
            }
        }
    }
    public class TemplateBodyRecordRequest : RecordRequest
    {
        public int TemplateId { get; set; }
        public int LanguageId { get; set; }
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_teId", TemplateId.ToString());
                parameters.Add("_languageId", LanguageId.ToString());

                return parameters;
            }
        }
    }

    public class EmployeeTemplatePreviewRecordRequest:RecordRequest
    {
        public int TemplateId { get; set; }
        public int LanguageId { get; set; }
        public int EmployeeId { get; set; }
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_teId", TemplateId.ToString());
                parameters.Add("_languageId", LanguageId.ToString());
                parameters.Add("_employeeId", EmployeeId.ToString());

                return parameters;
            }
        }
    }
    public class DocumentTransfersRecordRequest : RecordRequest
    {
        public int DocumentId { get; set; }
        public int SeqNo { get; set; }
        
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_doId", DocumentId.ToString());
                parameters.Add("_seqNo", SeqNo.ToString());

                return parameters;
            }
        }
    }
    public class DocumentTransfersListRequest:ListRequest
    {
        public string DocumentId { get; set; }

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_employeeId", "0");
                parameters.Add("_doId", DocumentId);
                parameters.Add("_apStatus", "0");
                return parameters;

            }
        }
    }
    public class DocumentListRequest : ListRequest
    {
        public int Status { get; set; }

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                
                parameters.Add("_statusId", Status.ToString());
                return parameters;

            }
        }
    }
}

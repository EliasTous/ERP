using Infrastructure.Domain;
using Services.Messaging;
using Services.Messaging.System;
using System.Collections.Generic;

namespace Services.Messaging.TaskSchedule
{
    public class TaskReportsListRequest : ListRequest
    {
        public string taskId { get; set; }

        /// <summary>
        /// /// parameter list shipped with the web request
        /// </summary>
        public override Dictionary<string, string> Parameters
        {

            get
            {
                parameters = new Dictionary<string, string>();

                parameters.Add("_taskId", taskId);


                return parameters;
            }
        }
    }


    public class TaskReceiverListRequest : RecordRequest
    {
        public string taskId { get; set; }
        public string seqNo { get; set; }

        /// <summary>
        /// /// parameter list shipped with the web request
        /// </summary>
        public override Dictionary<string, string> Parameters
        {

            get
            {
                parameters = new Dictionary<string, string>();

                parameters.Add("_taskId", taskId);
                parameters.Add("_seqNo", seqNo);


                return parameters;
            }
        }
    }

    public class TaskReportListRequest : RecordRequest
    {
        public string taskId { get; set; }
        public string reportId { get; set; }

        /// <summary>
        /// /// parameter list shipped with the web request
        /// </summary>
        public override Dictionary<string, string> Parameters
        {

            get
            {
                parameters = new Dictionary<string, string>();

                parameters.Add("_taskId", taskId);
                parameters.Add("_reportId", reportId);


                return parameters;
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.CompanyStructure
{
  public  class WorkSequenceListRequest :ListRequest
    {
        public string wfId { get; set; }



        /// <summary>
        /// /// parameter list shipped with the web request
        /// </summary>
        public override Dictionary<string, string> Parameters
        {

            get
            {
                parameters = new Dictionary<string, string>() ;

                parameters.Add("_wfId", wfId);


                return parameters;
            }
        }
    }

    public class WorkSequenceRecordRequest : RecordRequest
    {
        public string wfId { get; set; }
        public string seqNo { get; set; }



        /// <summary>
        /// /// parameter list shipped with the web request
        /// </summary>
        public override Dictionary<string, string> Parameters
        {

            get
            {
                parameters = new Dictionary<string, string>();

                parameters.Add("_wfId", wfId);
                parameters.Add("_seqNo", seqNo);



                return parameters;
            }
        }
    }
}

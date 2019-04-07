﻿using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.WebService;
using AionHR.Model.Employees.Profile;
using AionHR.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees
{
    /// <summary>
    /// Interface for the EmployeeRepository
    /// </summary>
   public interface IEmployeeRepository:IRepository<Employee,string>,ICommonRepository
    {

        PostWebServiceResponse UploadEmployeePhoto(Attachement at, string fileName, byte[] fileData, Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);
        PostWebServiceResponse ShareEmployeeAttachments(ShareAttachment at, List<string> fileNames, List<byte[]> filesData, Dictionary<string, string> Headers = null, Dictionary<string, string> QueryStringParams = null);
    }
}

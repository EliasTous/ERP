﻿using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Employees;
using Services.Messaging;
using Infrastructure.Session;
using Model.Employees.Profile;
using Infrastructure.Domain;
using Infrastructure.WebService;
using Model.System;
using Services.Messaging.Employees;

namespace Services.Implementations
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, SessionHelper sessionHelper) : base(sessionHelper)
        {
            _employeeRepository = employeeRepository;
        }



        public PostResponse<SalaryDetail> DeleteSalaryDetails(int SalaryId)
        {
            PostResponse<SalaryDetail> response;
            var headers = SessionHelper.GetAuthorizationHeadersForUser();

            SalaryDetail breaks = new SalaryDetail() { salaryId = SalaryId, comments = "", edId = 0, edName = "", includeInTotal = false, fixedAmount = 0, pct = 0, seqNo = 0 };
            var webResponse = GetRepository().ChildDelete<SalaryDetail>(breaks, headers);
            response = CreateServiceResponse<PostResponse<SalaryDetail>>(webResponse);

            return response;
        }

        public override RecordResponse<T> Get<T>(RecordRequest request)
        {
            RecordResponse<T> f = base.Get<T>(request);
            if (f != null && f.result != null)
                if (string.IsNullOrEmpty(((f.result) as Employee).pictureUrl))
                    ((f.result) as Employee).pictureUrl = "images/empPhoto.jpg";
            return f;


        }

        public override ListResponse<T> GetAll<T>(ListRequest request)
        {
            ListResponse<T> list = base.GetAll<T>(request);
            list.Items.ForEach(t => { if (string.IsNullOrEmpty((t as Employee).pictureUrl)) (t as Employee).pictureUrl = "images/empPhoto.jpg"; });
            return list;
        }

        public PostResponse<Attachement> UploadEmployeePhoto(EmployeeUploadPhotoRequest request)
        {
            PostResponse<Attachement> response;
            var headers = SessionHelper.GetAuthorizationHeadersForUser();
            PostWebServiceResponse webResponse = _employeeRepository.UploadEmployeePhoto(request.entity, request.photoName, request.photoData, headers);
            response = CreateServiceResponse<PostResponse<Attachement>>(webResponse);
            if (webResponse != null)
                response.recordId = webResponse.recordId;
            return response;
        }
        public PostResponse<ShareAttachment> ShareEmployeeAttachments(ShareAttachmentPostRequest request)
        {
            PostResponse<ShareAttachment> response;
            var headers = SessionHelper.GetAuthorizationHeadersForUser();
            PostWebServiceResponse webResponse = _employeeRepository.ShareEmployeeAttachments(request.entity, request.FileNames, request.FilesData, headers);
            response = CreateServiceResponse<PostResponse<ShareAttachment>>(webResponse);
            if (webResponse != null)
                response.recordId = webResponse.recordId;
            return response;
        }
        protected override dynamic GetRepository()
        {
            return _employeeRepository;
        }
    }
}

using AionHR.Infrastructure.Domain;
using AionHR.Infrastructure.Session;
using AionHR.Services.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Interfaces
{
    public interface IBaseService
    {
        SessionHelper SessionHelper { get; set; }


        RecordResponse<T> Get<T>(RecordRequest request);

        ListResponse<T> GetAll<T>(ListRequest request);

        PostResponse<T> AddOrUpdate<T>(PostRequest<T> request);

        PostResponse<T> Delete<T>(PostRequest<T> request);

        RecordResponse<TChild> ChildGetRecord<TChild>(RecordRequest request);
        ListResponse<TChild> ChildGetAll<TChild>(ListRequest request);

        PostResponse<TChild> ChildAddOrUpdate<TChild>(PostRequest<TChild> request);

        PostResponse<TChild> ChildDelete<TChild>(PostRequest<TChild> request);

    }
}

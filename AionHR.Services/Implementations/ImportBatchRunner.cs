using AionHR.Infrastructure.Session;
using AionHR.Model.System;
using AionHR.Services.Interfaces;
using AionHR.Services.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
    public abstract class ImportBatchRunner<T>
    {

       protected ISystemService _systemService;

        protected IBaseService service;

        public ImportBatchRunner(ISystemService systemService,IBaseService mainService)
        {
            _systemService = systemService;
            service = mainService;
            errors = new List<T>();
            errorMessages = new List<string>();
        }
        public List<T> Items { get; set; }

        protected List<T> errors { get; set; }

        protected List<string> errorMessages { get; set; }
        public string OutputPath { get; set; }

        public ISessionStorage SessionStore { get; set; }

        protected BatchOperationStatus BatchStatus { get; set; }
        private void handle(object state)
        {
            SetPreprocessingStarted();
            PreProcessElements();
            SetStarted();
            
            int i = 0;
            
            int stepSize =Items.Count>100? Items.Count / 100:1;
            
            foreach (var item in Items)
            {
                ProcessElement(item);
                if ((i++) % stepSize == 0)
                    ReportProgress(Items.Count, i);
            }
            PostProcessElements();
            SetFinished();
        }
        public virtual void Process()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(handle));

        }

        protected void SetStarted()
        {
            BatchStatus.status = 2;
            PostRequest<BatchOperationStatus> req = new PostRequest<BatchOperationStatus>();
            req.entity = BatchStatus;
            _systemService.ChildAddOrUpdate<BatchOperationStatus>(req);
        }
        protected void SetPreprocessingStarted()
        {
            BatchStatus.status = 1;
            PostRequest<BatchOperationStatus> req = new PostRequest<BatchOperationStatus>();
            req.entity = BatchStatus;
            _systemService.ChildAddOrUpdate<BatchOperationStatus>(req);
        }
    

        protected void SetFinished()
        {
            BatchStatus.status = 3;
            PostRequest<BatchOperationStatus> req = new PostRequest<BatchOperationStatus>();
            req.entity = BatchStatus;
            _systemService.ChildAddOrUpdate<BatchOperationStatus>(req);

        }

        protected virtual void ReportProgress(int total, int processed)
        {
            BatchStatus.tableSize = total;
            BatchStatus.processed = processed;
            
            PostRequest<BatchOperationStatus> req = new PostRequest<BatchOperationStatus>();
            req.entity = BatchStatus;
            _systemService.ChildAddOrUpdate<BatchOperationStatus>(req);
        }

        protected virtual void ProcessElement(T item)
        {
            PostRequest<T> req = new PostRequest<T>();
            req.entity = item;
            
            PostResponse<T> resp = service.ChildAddOrUpdate<T>(req);
            if (!resp.Success)
            {
                errors.Add(item);
                errorMessages.Add(resp.Summary);
            }
        }

        protected  void PreProcessElements()
        {
            int stepSize = Items.Count > 100 ? Items.Count / 100 : 1;
            
            int i = 0;
            foreach (var item in Items)
            {
                PreProcessElement(item);
                if ((i++) % stepSize == 0)
                    ReportProgress(Items.Count, i);
            }
        }

        protected abstract void PreProcessElement(T item);
        protected abstract void PostProcessElements();




    }
}

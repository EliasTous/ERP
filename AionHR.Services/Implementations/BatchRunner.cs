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
    public abstract class BatchRunner<T>
    {

        ISystemService _systemService;

        public BatchRunner(ISystemService systemService)
        {
            _systemService = systemService;
        }
        public List<T> Items { get; set; }

        public ISessionStorage SessionStore { get; set; }

        protected BatchOperationStatus BatchStatus { get; set; }
        private void handle(object state)
        {
            SetStarted();
            PreProcessElements();
            int i = 0;
            int stepSize = Items.Count / 100;
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
            BatchStatus.status = 1;
            PostRequest<BatchOperationStatus> req = new PostRequest<BatchOperationStatus>();
            req.entity = BatchStatus;
            _systemService.ChildAddOrUpdate<BatchOperationStatus>(req);
        }

        protected void SetFinished()
        {
            BatchStatus.status = 2;
            PostRequest<BatchOperationStatus> req = new PostRequest<BatchOperationStatus>();
            req.entity = BatchStatus;
            _systemService.ChildAddOrUpdate<BatchOperationStatus>(req);

        }

        protected void ReportProgress(int total, int processed)
        {
            BatchStatus.tableSize = total;
            BatchStatus.processed = processed;

            PostRequest<BatchOperationStatus> req = new PostRequest<BatchOperationStatus>();
            req.entity = BatchStatus;
            _systemService.ChildAddOrUpdate<BatchOperationStatus>(req);
        }

        protected abstract void ProcessElement(T item);

        protected abstract void PreProcessElements();
        protected abstract void PostProcessElements();




    }
}

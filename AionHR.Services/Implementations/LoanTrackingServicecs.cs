﻿using AionHR.Infrastructure.Session;
using AionHR.Model.LoadTracking;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
    public class LoanTrackingService : BaseService, ILoanTrackingService
    {
        ILoanTrackingRepository _repository;
        public LoanTrackingService(ILoanTrackingRepository _repo, SessionHelper helper):base(helper)
        {
            _repository = _repo;

        }
        protected override dynamic GetRepository()
        {
            return _repository;
        }
    }
}

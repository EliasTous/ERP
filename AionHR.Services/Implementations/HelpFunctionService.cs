﻿using AionHR.Infrastructure.Session;
using AionHR.Model.HelpFunction;
using AionHR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Implementations
{
  public  class HelpFunctionService : BaseService, IHelpFunctionService
    {
        private IHelpFunctionRepository _helpFunctionRepository;

        public HelpFunctionService(IHelpFunctionRepository complaintsRepository, SessionHelper sessionHelper) : base(sessionHelper)
        {
            _helpFunctionRepository = complaintsRepository;
        }

        protected override dynamic GetRepository()
        {
            return _helpFunctionRepository;
        }
    }
}

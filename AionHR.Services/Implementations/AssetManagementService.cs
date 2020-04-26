using Infrastructure.Session;
using Model.AssetManagementRepository;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Implementations
{
  public  class AssetManagementService : BaseService, IAssetManagementService
    {
        private IAssetManagementRepository _AssetManagementRepository;

        public AssetManagementService(IAssetManagementRepository complaintsRepository, SessionHelper sessionHelper) : base(sessionHelper)
        {
            _AssetManagementRepository = complaintsRepository;
        }

        protected override dynamic GetRepository()
        {
            return _AssetManagementRepository;
        }
    }
}

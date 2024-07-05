using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using ClinicSystem.Offers;
using Mofleet.Domain.Offers;
using Mofleet.Domain.RequestForQuotations;

namespace Mofleet.BackGroundJobs
{
    public class RejectAllOffersForUserIfHeDidnotMakeAnyChangeBGJ : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRequestForQuotationManager _requestForQuotationManager;
        private readonly IOfferManager _offerManager;
        private readonly IOfferAppService _offerAppService;
        public RejectAllOffersForUserIfHeDidnotMakeAnyChangeBGJ(AbpTimer timer, IUnitOfWorkManager unitOfWorkManager,
            IRequestForQuotationManager requestForQuotationManager, IOfferManager offerManager, IOfferAppService offerAppService) : base(timer)
        {
            Timer.Period = 3600000;//1 Hour 
            _unitOfWorkManager = unitOfWorkManager;
            _requestForQuotationManager = requestForQuotationManager;
            _offerManager = offerManager;
            _offerAppService = offerAppService;
        }

        protected override async void DoWork()
        {
            using (var unitOfWork = _unitOfWorkManager.Begin())
            {
                var requests = await _requestForQuotationManager.GetAllRequestHadOffersAndUserDidnotMakeAnyChangeForAutoRejectItAsync();
                foreach (var request in requests)
                {
                    var offers = await _offerManager.GetAllOffersApprovedWithThisRequest(request.Id);
                    await _requestForQuotationManager.MakeRequestAsPossible(request);
                    await _offerManager.MakeOffersRejectByUserAsyn(offers);
                    await _offerAppService.NoticOtherCompaniesForPossibleRequest(offers);

                }
                unitOfWork.Complete();
            }

        }
    }
}

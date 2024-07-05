using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Microsoft.EntityFrameworkCore;
using Mofleet.Authorization.Users;
using Mofleet.Domain.Offers;
using Mofleet.Domain.RequestForQuotations;
using Mofleet.RequestForQuotations;
using System;
using System.Linq;
using static Mofleet.Enums.Enum;

namespace Mofleet.BackGroundJobs
{
    public class ConfirmRequestIfUserDidnotConfirmedBGJ : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<RequestForQuotation, long> _requestForQuotationRepository;
        private readonly IOfferManager _offerManager;
        private readonly IRequestForQuotationAppService _requestForQuotationAppService;
        private readonly UserManager _userManager;

        public ConfirmRequestIfUserDidnotConfirmedBGJ(AbpTimer timer,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<RequestForQuotation, long> requestForQuotationRepository,
            IOfferManager offerManager,
            IRequestForQuotationAppService requestForQuotationAppService
,
            UserManager userManager) : base(timer)
        {
            Timer.Period = 3600000;//1 Hour 
            _unitOfWorkManager = unitOfWorkManager;
            _requestForQuotationRepository = requestForQuotationRepository;
            _offerManager = offerManager;
            _requestForQuotationAppService = requestForQuotationAppService;
            _userManager = userManager;
        }

        protected override async void DoWork()
        {
            using (var unitOfWork = _unitOfWorkManager.Begin())
            {
                var requests = await _requestForQuotationRepository.GetAll()
                              .AsTracking()
                              .Include(x => x.AttributeForSourceTypeValues)
                              .Where(x => x.Statues == RequestForQuotationStatues.FinishByCompany && x.ConfirmFinishDateByCompany.Value.AddHours(24) <= DateTime.UtcNow)
                              .ToListAsync();
                foreach (var request in requests)
                {
                    request.Statues = RequestForQuotationStatues.Finished;
                    request.FinishedDate = DateTime.UtcNow;
                    var offerId = await _offerManager.MakeOfferFinishByRequestId(request.Id);
                    var selectedCompany = await _offerManager.GetSelectedCompanyByOfferId(offerId);
                    await _requestForQuotationAppService.GiftCompanyThatProvideOfferBySourceTypePoints(selectedCompany, request.SourceTypeId, request.AttributeForSourceTypeValues.Where(x => x.AttributeChoiceId.HasValue).Select(x => x.AttributeChoiceId.Value).ToList());
                    var code = (await _userManager.GetUserByIdAsync(request.UserId)).MediatorCode;
                    if (!string.IsNullOrWhiteSpace(code))
                        await _requestForQuotationAppService.GiveMediatorCommissionForFinishRequest(code, selectedCompany, offerId);
                }
                await UnitOfWorkManager.Current.SaveChangesAsync();
                unitOfWork.Complete();
            }
        }
    }
}

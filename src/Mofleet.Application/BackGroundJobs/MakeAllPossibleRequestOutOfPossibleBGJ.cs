using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Mofleet.Domain.RequestForQuotations;

namespace Mofleet.BackGroundJobs
{
    public class MakeAllPossibleRequestOutOfPossibleBGJ : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRequestForQuotationManager _requestForQuotationManager;

        public MakeAllPossibleRequestOutOfPossibleBGJ(AbpTimer timer, IUnitOfWorkManager unitOfWorkManager, IRequestForQuotationManager requestForQuotationManager) : base(timer)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _requestForQuotationManager = requestForQuotationManager;
            Timer.Period = 3600000;//1 Hour 
        }

        protected override async void DoWork()
        {
            using (var unitOfWork = _unitOfWorkManager.Begin())
            {
                await _requestForQuotationManager.MakeAllPossibleRequestsAfterCustomTimeOutOfPossible();
                unitOfWork.Complete();
            }
        }
    }
}

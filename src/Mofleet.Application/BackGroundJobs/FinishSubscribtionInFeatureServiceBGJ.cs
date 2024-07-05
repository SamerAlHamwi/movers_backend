using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Mofleet.Domain.Companies;
using Mofleet.Domain.CompanyBranches;

namespace Mofleet.BackGroundJobs
{
    public class FinishSubscribtionInFeatureServiceBGJ : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ICompanyManager _companyManager;
        private readonly ICompanyBranchManager _companyBranchManager;
        public FinishSubscribtionInFeatureServiceBGJ(AbpTimer timer,
                    IUnitOfWorkManager unitOfWorkManager,
                    ICompanyManager companyManager,
                    ICompanyBranchManager companyBranchManager) : base(timer)
        {
            Timer.Period = 86400000;//24 Hour 
            _unitOfWorkManager = unitOfWorkManager;
            _companyManager = companyManager;
            _companyBranchManager = companyBranchManager;
        }

        protected async override void DoWork()
        {
            using (var unitOfWork = _unitOfWorkManager.Begin())
            {
                await _companyManager.MakeAllCompaniesNotFeatureIfTimeEndedAsync();
                await _companyBranchManager.MakeAllCompanyBranchesNotFeatureIfTimeEndedAsync();
                await UnitOfWorkManager.Current.SaveChangesAsync(); unitOfWork.Complete();
            }
        }
    }
}

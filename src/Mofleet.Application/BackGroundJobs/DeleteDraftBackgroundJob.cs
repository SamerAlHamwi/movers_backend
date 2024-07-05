using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Mofleet.Domain.Drafts;


namespace Mofleet.BackGroundJobs
{
    public class DeleteDraftBackgroundJob : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IDraftManager _draftManger;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public DeleteDraftBackgroundJob(AbpTimer timer, IDraftManager draftManger, IUnitOfWorkManager unitOfWorkManager) : base(timer)
        {

            Timer.Period = 86400000;//24 Hours 
            _draftManger = draftManger;
            _unitOfWorkManager = unitOfWorkManager;


        }

        protected async override void DoWork()
        {
            using (var unitOfWork = _unitOfWorkManager.Begin())
            {
                await _draftManger.DeleteAllOldDrafts();
                await _unitOfWorkManager.Current.SaveChangesAsync();
                unitOfWork.Complete();
            }
        }
    }
}






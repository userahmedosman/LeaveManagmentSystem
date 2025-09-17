using LeaveManagmentSystem.ViewModels.LeaveTypes;
using LeaveManagmentSystem.ViewModels.Periods;

namespace LeaveManagmentSystem.Services.PeriodService
{
    public interface IPeriodService
    {
        Task CreateAsync(PeriodCreateVM createVm);
        Task EditPeriodNameAsync(PeriodEditVM editVM);
        Task<bool> CheckPeriodIsInitiatedAsync();
        Task RemoveOldPeriodAsync(int periodId);
        Task<List<PeriodReadVM>> GetAllPeriodsAsync();
        Task<T> GetPeriodByIdAysnc<T>(int id) where T : class;
        bool CheckPeriodToCreateIsValid(PeriodCreateVM period);
        Task<bool> PeriodExists(int id);
    }
}

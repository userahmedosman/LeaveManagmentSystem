using LeaveManagmentSystem.ViewModels.LeaveAllocations;
using LeaveManagmentSystem.ViewModels.LeaveTypes;

namespace LeaveManagmentSystem.Services.LeaveTypeService
{
    public interface ILeaveTypesService
    {
        Task CreateAsync(LeaveTypeCreateVM createVm);
        Task EditAsync(LeaveTypeEditVM editVM);
        Task<List<LeaveTypeReadOnlyVM>> GetAllAsync();
        Task<T?> GetByIdAsync<T>(int id) where T : class;
        Task<bool> IsLeaveTypeNameRepeatedAsync(string name);
        Task<bool> IsLeaveTypeNameRepeatedForEditAsync(int id, LeaveTypeEditVM editVm);
        Task<bool> CheckDayExceedLeaveTypeDate(int leaveTypeId, int Days);
        public bool LeaveTypeExists(int id);
        Task Remove(int id);
    }
}
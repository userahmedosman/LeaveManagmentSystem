using LeaveManagmentSystem.ViewModels.LeaveTypes;

namespace LeaveManagmentSystem.Services
{
    public interface ILeaveTypesService
    {
        Task CreateAsync(CreateVM createVm);
        Task EditAsync(EditVM editVM);
        Task<List<ReadVM>> GetAllAsync();
        Task<T?> GetByIdAsync<T>(int id) where T : class;
        Task<bool> IsLeaveTypeNameRepeatedAsync(string name);
        Task<bool> IsLeaveTypeNameRepeatedForEditAsync(int id, EditVM editVm);
        public bool LeaveTypeExists(int id);
        Task Remove(int id);
    }
}
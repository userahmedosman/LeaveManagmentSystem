using LeaveManagmentSystem.Models;
using LeaveManagmentSystem.ViewModels.LeaveAllocations;

namespace LeaveManagmentSystem.Services.LeaveAllocationService
{
    public interface ILeaveAllocationService
    {
        Task AllocateLeave(string employeeId);
        Task<List<EmployeeListVM>> GetAllEmployees();
        Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId);
        Task<LeaveAllocationEditVM> GetEmployeeAllocation(int id);
        Task EditAllocation(LeaveAllocationEditVM leaveAllocationEditVM);
    }
}

using LeaveManagmentSystem.ViewModels.LeaveReqests;
namespace LeaveManagmentSystem.Services.LeaveRequests
{
    public interface ILeaveRequestService
    {

        Task CreateAsync(LeaveRequestCreateVM create);

        Task<List<LeaveRequestReadOnlyVM>> GetPersonalLeaveRequestAsync();

        Task<List<LeaveRequestReadOnlyVM>> AdminGetEmployeeLeaveRequestAsync(string employeeId);

        Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequestsAsync();

        Task<T?> GetLeaveRequestAsync<T>(int id) where T : class;
        Task CancelLeaveRequest(LeaveRequestDetailVM model);

        Task ReviewLeaveRequestAsync(ReviewLeaveRequestVM model);
      
        Task<bool> CheckRequestDaysExceedAllocationDaysAsync(LeaveRequestCreateVM model);
    }
}
using AutoMapper;
using LeaveManagmentSystem.Models;
using LeaveManagmentSystem.ViewModels.LeaveAllocations;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagmentSystem.Services.LeaveAllocationService;

public class LeaveAllocationService : ILeaveAllocationService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public LeaveAllocationService(
        ApplicationDbContext dbContext, 
        IHttpContextAccessor httpContext,
        UserManager<ApplicationUser> userManager,
        IMapper mapper) {

        this._dbContext = dbContext;
        this._httpContext = httpContext;
        this._userManager = userManager;
        this._mapper = mapper;
    }

    public async Task AllocateLeave(string employeeId)
    {
       
        try
        {
            var leaveTypes = await _dbContext.LeaveTypes
                .Where(q => !q.LeaveAllocations
                .Any(x => x.EmployeeId == employeeId))
                .ToListAsync();

            var currentDate = DateTime.Now;

            var period = await _dbContext.Periods.SingleAsync(p => p.EndDate.Year == currentDate.Year);
            var monthsRemaining = period.EndDate.Month - currentDate.Month;

            foreach (var leaveType in leaveTypes) {
               
                var accurateRate = decimal.Divide(leaveType.LeaveDurationInDays, 12);
                var leaveAllocation = new LeaveAllocation
                {
                    EmployeeId = employeeId,
                    LeaveTypeId = leaveType.Id,
                    PeriodId = period.Id,
                    Days = Convert.ToInt32(Math.Ceiling(accurateRate * monthsRemaining)),
                };

                

                _dbContext.Add(leaveAllocation);
                
            }
            await _dbContext.SaveChangesAsync();

        }
        catch (Exception)
        {

            throw;
        }
    }



    public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId)
    {
        var allocations = await GetAllocations(userId);
        var allocationsVMList = _mapper.Map<List<LeaveAllocationVM>>(allocations);

        var user = await GetCurrentUser(userId);

        var leaveTypesCount = await _dbContext.LeaveTypes.CountAsync();
        var employeeVM = new EmployeeAllocationVM
        {
            BirthDate = user.BirthDate,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Id = user.Id,
            LeaveAllocations = allocationsVMList,
            IsCompletedAllocation = leaveTypesCount == allocations.Count
        };

        return employeeVM;

    }

    public async Task<List<EmployeeListVM>> GetAllEmployees()
    {
        var users = await _userManager.GetUsersInRoleAsync(Roles.Employee);

        var employees = _mapper.Map<List<EmployeeListVM>>(users);

        return employees;
    }

    public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int id)
    {
        var allocation = await _dbContext.LeaveAllocations
            .Include(al => al.LeaveType)
            .Include(al => al.Employee)
            .FirstOrDefaultAsync(x => x.Id == id);

        var allocationVM = _mapper.Map<LeaveAllocationEditVM>(allocation);

        return allocationVM;
    }

    public async Task EditAllocation(LeaveAllocationEditVM allocationEditVM)
    {
        await _dbContext.LeaveAllocations
            .Where(q => q.Id == allocationEditVM.Id)
            .ExecuteUpdateAsync(s => s.SetProperty(p => p.Days, allocationEditVM.Days));
    }
    private async Task<List<LeaveAllocation>> GetAllocations(string? userId)
    {
        var user = await GetCurrentUser(userId);

        var leaveAllocations = await _dbContext.LeaveAllocations
            .Include(lt => lt.LeaveType)
            .Include(p => p.Period)
            .Where(la => la.EmployeeId == user.Id && la.Period.EndDate.Year == DateTime.Now.Year)
            .ToListAsync();

        return leaveAllocations;
    }

    private async Task<ApplicationUser> GetCurrentUser(string? userId)
    {
        var user = string.IsNullOrEmpty(userId)? await _userManager.GetUserAsync(_httpContext?.HttpContext?.User!)
            : await _userManager.FindByIdAsync(userId);

        return user!;
    }

 

}

using AutoMapper;
using LeaveManagmentSystem.Models;
using LeaveManagmentSystem.ViewModels.LeaveReqests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LeaveManagmentSystem.Services.LeaveRequests
{
    public partial class LeaveRequestService : ILeaveRequestService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public LeaveRequestService(
            ApplicationDbContext dbContext, 
            IMapper mapper,
             UserManager<ApplicationUser> userManager,
             IHttpContextAccessor httpContext 
            ) {
            this._dbContext = dbContext;
            this._mapper = mapper;
            _userManager = userManager;
            _httpContext = httpContext;
        }
        public async Task CancelLeaveRequest(LeaveRequestDetailVM model)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var leaveRequest = await _dbContext.LeaveRequests
               .FirstOrDefaultAsync(q => q.Id == model.Id);


                leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Cancelled;

                var user = await _userManager.GetUserAsync(_httpContext.HttpContext.User);
                var requestedNumOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
                var leaveTypeId = await _dbContext.LeaveTypes
                    .Where(q => q.Name == model.LeaveType)
                    .Select(q => q.Id)
                    .FirstOrDefaultAsync();

                var allocationToDeduct = await _dbContext.LeaveAllocations
                    .FirstAsync(q => q.LeaveTypeId == leaveTypeId && q.EmployeeId == user.Id);
                allocationToDeduct.Days = allocationToDeduct.Days + requestedNumOfDays;

                _dbContext.Update(allocationToDeduct);
                _dbContext.Update(leaveRequest);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
           
        }
        public async Task<T?> GetLeaveRequestAsync<T>(int id) where T : class
        {
            var leaveRequest = await _dbContext.LeaveRequests
                .Include(q => q.Employee)
                .Include(q => q.Reviewer)
                .Include(q => q.LeaveType)
                .Include(q => q.LeaveRequestStatus)
                .FirstOrDefaultAsync(q => q.Id == id);
            if (leaveRequest == null)
            {
                throw new Exception($"Leave request with id {id} not found.");
            }

            return _mapper.Map<T>(leaveRequest);
        }

        public async Task<bool> CheckRequestDaysExceedAllocationDaysAsync(LeaveRequestCreateVM model)
        {
            var requestedNumOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
            var user = await _userManager.GetUserAsync(_httpContext.HttpContext.User);
        
            var allocation = await _dbContext.LeaveAllocations
                   .FirstAsync(q => q.LeaveTypeId == model.LeaveTypeId && q.EmployeeId == LeaveRequestCreateVM.EmployeeId);

            return allocation.Days < requestedNumOfDays;
        }

        public async Task CreateAsync(LeaveRequestCreateVM model)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var leaveRequest = _mapper.Map<LeaveRequest>(model);
                
                var user = await _userManager.GetUserAsync(_httpContext.HttpContext.User);
                leaveRequest.EmployeeId = LeaveRequestCreateVM.EmployeeId;
                leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Pending;
                _dbContext.Add(leaveRequest);

                var requestedNumOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;

                var allocationToDeduct = await _dbContext.LeaveAllocations
                    .FirstAsync(q => q.LeaveTypeId == model.LeaveTypeId && q.EmployeeId == LeaveRequestCreateVM.EmployeeId);
                allocationToDeduct.Days = allocationToDeduct.Days - requestedNumOfDays;

                _dbContext.Update(allocationToDeduct);

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }



        }

        public async Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequestsAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContext.HttpContext.User);

            var leaveRequests = await _dbContext.LeaveRequests
                .Include(q => q.LeaveType)
                .Where(q => q.EmployeeId != user.Id)
                .ToListAsync();

            var leaveRequestModels = leaveRequests.Select(q => new LeaveRequestReadOnlyVM
            {
                Id = q.Id,
                StartDate = q.StartDate,
                EndDate = q.EndDate,
                LeaveType = q.LeaveType.Name,
                NumberOfDays = q.EndDate.DayNumber - q.StartDate.DayNumber,
                LeaveRequestStatusEnum = (LeaveRequestStatusEnum)q.LeaveRequestStatusId
                
            }).ToList();

            var model = new EmployeeLeaveRequestListVM
            {
                TotalRequests = leaveRequests.Count,
                ApprovedRequests = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Approved),
                PendingRequests = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Pending),
                DeclinedRequests = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Declined),
                CancelledRequests = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Cancelled),
                LeaveRequests = leaveRequestModels
            };

            return model;
        }

        public async Task<List<LeaveRequestReadOnlyVM>> GetPersonalLeaveRequestAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContext.HttpContext.User);

            var leaveRequests = await _dbContext.LeaveRequests
                .Include(q => q.LeaveType)
                .Include(q => q.LeaveRequestStatus)
                .Where(q => q.EmployeeId == user.Id)
                .ToListAsync();

            return _mapper.Map<List<LeaveRequestReadOnlyVM>>(leaveRequests);
        }

        public async Task<List<LeaveRequestReadOnlyVM>> AdminGetEmployeeLeaveRequestAsync(string employeeId)
        {
            var leaveRequests = await _dbContext.LeaveRequests
               .Include(q => q.LeaveType)
               .Include(q => q.LeaveRequestStatus)
               .Where(q => q.EmployeeId == employeeId)
               .ToListAsync();

            return _mapper.Map<List<LeaveRequestReadOnlyVM>>(leaveRequests);
        }

        public async Task ReviewLeaveRequestAsync(ReviewLeaveRequestVM model)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
               
                var leaveRequest = await _dbContext.LeaveRequests
                .FirstOrDefaultAsync(q => q.Id == model.Id);
                if (leaveRequest == null)
                {
                    throw new Exception($"Leave request with id {model.Id} not found.");
                }
                var approved = model.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Approved;
                leaveRequest.ReviewComment = model.ReviewComment;
                leaveRequest.LeaveRequestStatusId = approved ? (int)LeaveRequestStatusEnum.Approved : (int)LeaveRequestStatusEnum.Declined;
                var user = await _userManager.GetUserAsync(_httpContext.HttpContext.User);
                leaveRequest.ReviewerId = user.Id;
                if(!approved)
                {
                    var requestedNumOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber;
                    var allocationToDeduct = await _dbContext.LeaveAllocations
                        .FirstAsync(q => q.LeaveTypeId == leaveRequest.LeaveTypeId && q.EmployeeId == leaveRequest.EmployeeId);
                    allocationToDeduct.Days = allocationToDeduct.Days + requestedNumOfDays;
                    _dbContext.Update(allocationToDeduct);
                }
             
                    _dbContext.Update(leaveRequest);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

       
    }
}

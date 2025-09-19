using LeaveManagmentSystem.Services.LeaveRequests;
using LeaveManagmentSystem.Services.LeaveTypeService;
using LeaveManagmentSystem.ViewModels.LeaveReqests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace LeaveManagmentSystem.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveTypesService _leaveTypesService;
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly UserManager<ApplicationUser> _userManager;

        public LeaveRequestController(ILeaveTypesService leaveTypesService, 
            ILeaveRequestService leaveRequestService,
             UserManager<ApplicationUser> userManager) {
            this._leaveTypesService = leaveTypesService;
            this._leaveRequestService = leaveRequestService;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Create(string requesterId, int? LeaveTypeId)
        {
            var leaveTypes = await _leaveTypesService.GetAllAsync();
            var leaveTypesList = new SelectList(leaveTypes, "Id", "Name", LeaveTypeId);
            var model = new LeaveRequestCreateVM
            {
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                LeaveTypes = leaveTypesList
                
            };
            LeaveRequestCreateVM.EmployeeId = requesterId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestCreateVM model)
        {
            if (await _leaveRequestService.CheckRequestDaysExceedAllocationDaysAsync(model))
            {
                ModelState.AddModelError(string.Empty, "You have excceded allocation days ");
                ModelState.AddModelError(nameof(model.StartDate), "The requested allocation days exceeded the maximum amount of available days ");
            }
            if (ModelState.IsValid)
            {
                await _leaveRequestService.CreateAsync(model);
                var redirect = User.IsInRole(Roles.Admin) ? nameof(AdminRequestList) : nameof(MyLeaveRequest);
                
                return RedirectToAction(redirect);
            }
            var leaveTypes = await _leaveTypesService.GetAllAsync();

            model.LeaveTypes = new SelectList(leaveTypes, "Id", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelRequest(int id)
        {
            var leaveRequest = await _leaveRequestService.GetLeaveRequestAsync<LeaveRequestDetailVM>(id);
            if (leaveRequest == null) { return NotFound(); }

            await _leaveRequestService.CancelLeaveRequest(leaveRequest);
            return RedirectToAction(nameof(MyLeaveRequest));
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> AdminRequestList()
        {
            var list = await _leaveRequestService.AdminGetAllLeaveRequestsAsync();
           
            return View(list);
        }

        public async Task<IActionResult> MyLeaveRequest()
        {
            var list = await _leaveRequestService.GetPersonalLeaveRequestAsync();
            foreach (var item in list)
            {
                item.NumberOfDays = (item.EndDate.DayNumber - item.StartDate.DayNumber);


            }
            return View(list);
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> EmployeeLeaveRequests(string employeeId)
        {
            var list = await _leaveRequestService.AdminGetEmployeeLeaveRequestAsync(employeeId);
            foreach (var item in list)
            {
                item.NumberOfDays = (item.EndDate.DayNumber - item.StartDate.DayNumber);


            }
            return View(list);
        }


        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ApproveRequest(int id)
        {
            var leaveRequest = await _leaveRequestService.GetLeaveRequestAsync<ReviewLeaveRequestVM>(id);
            if (leaveRequest == null) { return NotFound(); }
            return View(leaveRequest);
        }

        [Authorize(Roles =Roles.Admin)]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveRequest(ReviewLeaveRequestVM model)
        {

            model.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Approved;
            await _leaveRequestService.ReviewLeaveRequestAsync(model);
            return RedirectToAction(nameof(AdminRequestList));
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeclineRequest(int id)
        {
            var leaveRequest = await _leaveRequestService.GetLeaveRequestAsync<ReviewLeaveRequestVM>(id);
            if (leaveRequest == null) { return NotFound(); }
            return View(leaveRequest);
        }

        [Authorize(Roles = Roles.Admin)]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeclineRequest(ReviewLeaveRequestVM model)
        {
            model.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Declined;
            await _leaveRequestService.ReviewLeaveRequestAsync(model);
            return RedirectToAction(nameof(AdminRequestList));
        }



        public async Task<IActionResult> Details(int id)
        {
            var details = await _leaveRequestService.GetLeaveRequestAsync<LeaveRequestDetailVM>(id);
            if (details == null) { return NotFound(); }
            details.NumberOfDays = (details.EndDate.DayNumber - details.StartDate.DayNumber);
            return View(details);
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> AdminRequestDetails(int id)
        {
            var details = await _leaveRequestService.GetLeaveRequestAsync<LeaveRequestAdminDetailVM>(id);
            if (details == null) { return NotFound(); }
            details.NumberOfDays = (details.EndDate.DayNumber - details.StartDate.DayNumber);
            return View(details);
        }


    }
}

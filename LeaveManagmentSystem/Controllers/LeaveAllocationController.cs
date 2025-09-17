using LeaveManagmentSystem.Models;
using LeaveManagmentSystem.Services.LeaveAllocationService;
using LeaveManagmentSystem.Services.LeaveTypeService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LeaveManagmentSystem.Controllers;

[Authorize]
public class LeaveAllocationController : Controller
{
    private readonly ILeaveAllocationService _leaveAllocationService;
    private readonly ILeaveTypesService _leaveTypesService;

    public LeaveAllocationController(ILeaveAllocationService leaveAllocationService, ILeaveTypesService leaveTypesService) {
        this._leaveAllocationService = leaveAllocationService;
        this._leaveTypesService = leaveTypesService;
    }

    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Index()
    {
        var employeesList = await _leaveAllocationService.GetAllEmployees();

        return View(employeesList);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AllocateLeave(string? id)
    {
        await _leaveAllocationService.AllocateLeave(id!);

        return RedirectToAction(nameof(Details), new {userId = id});
    }

    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> EditAllocation(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        var employeeAllocationVM = await _leaveAllocationService.GetEmployeeAllocation(id.Value);

        if (employeeAllocationVM == null) {
         return NotFound();
        }

        return View(employeeAllocationVM);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAllocation(LeaveAllocationEditVM leaveAllocationEditVM)
    {
        if (await _leaveTypesService.CheckDayExceedLeaveTypeDate(leaveAllocationEditVM.LeaveType.Id, leaveAllocationEditVM.Days)) {
            ModelState.AddModelError("Days", "Allocation day excedded the maxium leave type day");
            return View(leaveAllocationEditVM);
        }
        if (ModelState.IsValid)
        {
            await _leaveAllocationService.EditAllocation(leaveAllocationEditVM);
            return RedirectToAction(nameof(Details), new { userId = leaveAllocationEditVM.Employee.Id });
        }
        int days = leaveAllocationEditVM.Days;
        leaveAllocationEditVM = await _leaveAllocationService.GetEmployeeAllocation(leaveAllocationEditVM.Id);
        leaveAllocationEditVM.Days = days;
        return View(leaveAllocationEditVM);
    }

    public async Task<IActionResult> Details(string? userId)
    {
       var employeeAllocationVM =  await _leaveAllocationService.GetEmployeeAllocations(userId);

        return View(employeeAllocationVM);
    }
}

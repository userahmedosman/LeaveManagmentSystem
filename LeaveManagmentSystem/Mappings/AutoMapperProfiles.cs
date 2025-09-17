using AutoMapper;
using LeaveManagmentSystem.Models;
using LeaveManagmentSystem.ViewModels.LeaveAllocations;
using LeaveManagmentSystem.ViewModels.LeaveTypes;
using LeaveManagmentSystem.ViewModels.Periods;


namespace LeaveManagmentSystem.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles() {
            //LeaveType
            CreateMap<LeaveType, LeaveTypeReadOnlyVM>();
            CreateMap<LeaveTypeCreateVM, LeaveType>();
            CreateMap<LeaveType, LeaveTypeEditVM>().ReverseMap();

            //Period
            CreateMap<Period, PeriodReadVM>();
            CreateMap<PeriodCreateVM, Period>();
            CreateMap<Period, PeriodEditVM>().ReverseMap();

            //LeaveAllocation
            CreateMap<LeaveAllocation, LeaveAllocationVM>();
            CreateMap<ApplicationUser, EmployeeListVM>(); 
            CreateMap<LeaveAllocation, LeaveAllocationEditVM>();

        }
    }
}

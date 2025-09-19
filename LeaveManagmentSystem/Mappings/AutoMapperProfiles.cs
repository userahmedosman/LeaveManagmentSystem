using AutoMapper;
using LeaveManagmentSystem.Models;
using LeaveManagmentSystem.ViewModels.LeaveAllocations;
using LeaveManagmentSystem.ViewModels.LeaveReqests;
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

            //LeaveRequest
            CreateMap<LeaveRequestCreateVM, LeaveRequest>();
            CreateMap<LeaveRequest, LeaveRequestReadOnlyVM>()
                .ForMember(dest => dest.LeaveType, opt => opt.MapFrom(src => src.LeaveType.Name))
                .ForMember(dest => dest.NumberOfDays, opt => opt.MapFrom(src => src.EndDate.DayNumber - src.StartDate.DayNumber))
                .ForMember(dest => dest.LeaveRequestStatusEnum, opt => opt.MapFrom(src => src.LeaveRequestStatusId));
            
               CreateMap<LeaveRequest, LeaveRequestDetailVM>()
              .ForMember(dest => dest.LeaveType, opt => opt.MapFrom(src => src.LeaveType.Name))
              .ForMember(dest => dest.NumberOfDays, opt => opt.MapFrom(src => src.EndDate.DayNumber - src.StartDate.DayNumber))
              .ForMember(dest => dest.LeaveRequestStatusEnum, opt => opt.MapFrom(src => src.LeaveRequestStatusId));

            CreateMap<LeaveRequest, LeaveRequestAdminDetailVM>()
                .ForMember(dest => dest.LeaveRequestStatusEnum, opt => opt.MapFrom(src => src.LeaveRequestStatusId))
                 .ForMember(dest => dest.LeaveType, opt => opt.MapFrom(src => src.LeaveType.Name));

            CreateMap<LeaveRequest, ReviewLeaveRequestVM>();



        }
    }
}

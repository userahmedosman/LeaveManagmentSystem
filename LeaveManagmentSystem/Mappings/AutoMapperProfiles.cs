using AutoMapper;
using LeaveManagmentSystem.Models;
using LeaveManagmentSystem.ViewModels.LeaveTypes;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace LeaveManagmentSystem.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles() {
            CreateMap<LeaveType, ReadVM>();
            CreateMap<CreateVM, LeaveType>();
            CreateMap<LeaveType, EditVM>().ReverseMap();
        }
    }
}

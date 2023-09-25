using AutoMapper;
using VisitorManagement.Application.DTOs;
using VisitorManagement.Domain.Common;

namespace VisitorManagement.Application.MappingProfile
{
    public class VisitorManagementAutoMapperProfile : Profile
    {
        public VisitorManagementAutoMapperProfile()
        {
            CreateMap<HostVisitorRequest, HostVisitorRequestDTO>()
            .ForMember(dest => dest.HostVisitorRequestId, opt => opt.MapFrom(src => src.HostVisitorRequestId))
            .ForMember(dest => dest.VisitorFullName, opt => opt.MapFrom(src => src.VisitorName))
            .ForMember(dest => dest.VisitorEmailAddress, opt => opt.MapFrom(src => src.VisitorEmail))
            .ForMember(dest => dest.VisitorPhone, opt => opt.MapFrom(src => src.VisitorContactNumber))
            .ForMember(dest => dest.ArrivalDateTime, opt => opt.MapFrom(src => src.VisitorArrivalDateTime))
            .ForMember(dest => dest.CheckOutDateTime, opt => opt.MapFrom(src => src.VisitorCheckOutDateTime))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.VisitDuration))
            .ForMember(dest => dest.VisitPurpose, opt => opt.MapFrom(src => src.VisitorVisitPurpose))
            .ForMember(dest => dest.RequestingHostId, opt => opt.MapFrom(src => src.HostId))
            .ForMember(dest => dest.RequestingHostName, opt => opt.MapFrom(src => src.HostName)).ReverseMap();

            CreateMap<AdminApprovalStatus, AdminApprovalStatusDTO>().ReverseMap();
        }
    }
    
}
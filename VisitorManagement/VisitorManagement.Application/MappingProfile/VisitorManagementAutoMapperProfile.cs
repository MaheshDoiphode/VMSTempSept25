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
            .ForMember(dest => dest.RequestId, opt => opt.MapFrom(src => src.HostVisitorRequestId))
            .ForMember(dest => dest.VisitorFullName, opt => opt.MapFrom(src => src.VisitorName))
            .ForMember(dest => dest.VisitorEmailAddress, opt => opt.MapFrom(src => src.VisitorEmail))
            .ForMember(dest => dest.VisitorPhone, opt => opt.MapFrom(src => src.VisitorContactNumber))
            .ForMember(dest => dest.ArrivalDateTime, opt => opt.MapFrom(src => src.VisitorArrivalDateTime))
            .ForMember(dest => dest.CheckOutDateTime, opt => opt.MapFrom(src => src.VisitorCheckOutDateTime))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.VisitDuration))
            .ForMember(dest => dest.VisitPurpose, opt => opt.MapFrom(src => src.VisitorVisitPurpose))
            .ForMember(dest => dest.RequestingHostId, opt => opt.MapFrom(src => src.HostId))
            .ForMember(dest => dest.RequestingHostName, opt => opt.MapFrom(src => src.HostName)).ReverseMap();

            CreateMap<AdminApprovalStatus, AdminApprovalStatusDTO>()
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(src => src.HostVisitorRequestId)).ReverseMap();

            CreateMap<VisitorEntity, VisitorEntityDTO>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.VisitorEntityId))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.VisitorName))
           .ForMember(dest => dest.ContactNumber, opt => opt.MapFrom(src => src.VisitorContactNumber))
           .ForMember(dest => dest.PersonalIdType, opt => opt.MapFrom(src => src.VisitorPersonalIdType))
           .ForMember(dest => dest.PersonalIdNumber, opt => opt.MapFrom(src => src.VisitorPersonalIdNumber))
           .ForMember(dest => dest.PersonalIdCardImage, opt => opt.MapFrom(src => src.VisitorPersonalIdCardImage))
           .ForMember(dest => dest.PersonalImage, opt => opt.MapFrom(src => src.VisitorPersonalImage)).ReverseMap();

        }
    }
    
}
using AutoMapper;
using ScheduleInDDD.Core.SyncedAggregates;
using ScheduleInDDD.Models.AppointmentType;

namespace ScheduleInDDD.MappingProfiles
{
    public class AppointmentTypeProfile : Profile
    {
        public AppointmentTypeProfile()
        {
            CreateMap<AppointmentType, AppointmentTypeDto>()
                .ForMember(dto => dto.AppointmentTypeId, options => options.MapFrom(src => src.Id));
            CreateMap<AppointmentTypeDto, AppointmentType>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.AppointmentTypeId));
        }
    }
}

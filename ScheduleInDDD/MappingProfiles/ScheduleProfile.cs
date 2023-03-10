using AutoMapper;
using ScheduleInDDD.Core.ScheduleAggregate;
using ScheduleInDDD.Models.Schedule;

namespace ScheduleInDDD.MappingProfiles
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<Schedule, ScheduleDto>()
              .ForPath(dto => dto.AppointmentIds, options => options.MapFrom(src => src.Appointments.Select(x => x.Id)));
            CreateMap<ScheduleDto, Schedule>();
            CreateMap<CreateScheduleRequest, Schedule>();
            CreateMap<UpdateScheduleRequest, Schedule>();
            CreateMap<DeleteScheduleRequest, Schedule>();
        }
    }
}

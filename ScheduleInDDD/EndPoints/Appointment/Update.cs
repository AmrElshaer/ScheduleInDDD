using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PluralsightDdd.SharedKernel.Interfaces;
using ScheduleInDDD.Core.ScheduleAggregate.Specifications;
using ScheduleInDDD.Core.SyncedAggregates;
using ScheduleInDDD.Models.Appointment;
using Swashbuckle.AspNetCore.Annotations;
using ScheduleEntity = ScheduleInDDD.Core.ScheduleAggregate.Schedule;
namespace ScheduleInDDD.EndPoints.Appointment
{
    public class Update : EndpointBaseAsync
    .WithRequest<UpdateAppointmentRequest>
    .WithActionResult<UpdateAppointmentResponse>
    {
        private readonly IRepository<ScheduleEntity> _scheduleRepository;
        private readonly IReadRepository<ScheduleEntity> _scheduleReadRepository;
        private readonly IReadRepository<AppointmentType> _appointmentTypeRepository;
        private readonly IMapper _mapper;

        public Update(IRepository<ScheduleEntity> scheduleRepository,
          IReadRepository<ScheduleEntity> scheduleReadRepository,
          IReadRepository<AppointmentType> appointmentTypeRepository,
          IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _scheduleReadRepository = scheduleReadRepository;
            _appointmentTypeRepository = appointmentTypeRepository;
            _mapper = mapper;
        }

        [HttpPut(UpdateAppointmentRequest.Route)]
        [SwaggerOperation(
            Summary = "Updates an Appointment",
            Description = "Updates an Appointment",
            OperationId = "appointments.update",
            Tags = new[] { "AppointmentEndpoints" })
        ]
        public override async Task<ActionResult<UpdateAppointmentResponse>> HandleAsync(UpdateAppointmentRequest request,
          CancellationToken cancellationToken)
        {
            var response = new UpdateAppointmentResponse(request.CorrelationId());

            var apptType = await _appointmentTypeRepository.GetByIdAsync(request.AppointmentTypeId);

            var spec = new ScheduleByIdWithAppointmentsSpec(request.ScheduleId); // TODO: Just get that day's appointments
            var schedule = await _scheduleReadRepository.GetBySpecAsync(spec);

            var apptToUpdate = schedule.Appointments
                                  .FirstOrDefault(a => a.Id == request.Id);
            apptToUpdate.UpdateAppointmentType(apptType, schedule.AppointmentUpdatedHandler);
            apptToUpdate.UpdateRoom(request.RoomId);
            apptToUpdate.UpdateStartTime(request.Start, schedule.AppointmentUpdatedHandler);
            apptToUpdate.UpdateTitle(request.Title);
            apptToUpdate.UpdateDoctor(request.DoctorId);

            await _scheduleRepository.UpdateAsync(schedule);

            var dto = _mapper.Map<AppointmentDto>(apptToUpdate);
            response.Appointment = dto;

            return Ok(response);
        }
    }
}

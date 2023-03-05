using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PluralsightDdd.SharedKernel.Interfaces;
using ScheduleInDDD.Core.ScheduleAggregate.Specifications;
using ScheduleInDDD.Models.Appointment;
using ScheduleInDDD.Models.Schedule;
using Swashbuckle.AspNetCore.Annotations;
using ScheduleEntity = ScheduleInDDD.Core.ScheduleAggregate.Schedule;
namespace ScheduleInDDD.EndPoints.Appointment
{
    public class Delete : EndpointBaseAsync
    .WithRequest<DeleteAppointmentRequest>
    .WithActionResult<DeleteAppointmentResponse>
    {
        private readonly IReadRepository<ScheduleEntity> _scheduleReadRepository;
        private readonly IRepository<ScheduleEntity> _scheduleRepository;
        private readonly IMapper _mapper;

        public Delete(IRepository<ScheduleEntity> scheduleRepository, IReadRepository<ScheduleEntity> scheduleReadRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _scheduleReadRepository = scheduleReadRepository;
            _mapper = mapper;
        }

        [HttpDelete(DeleteAppointmentRequest.Route)]
        [SwaggerOperation(
            Summary = "Deletes an Appointment",
            Description = "Deletes an Appointment",
            OperationId = "appointments.delete",
            Tags = new[] { "AppointmentEndpoints" })
        ]
        public override async Task<ActionResult<DeleteAppointmentResponse>> HandleAsync([FromRoute] DeleteAppointmentRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteAppointmentResponse(request.CorrelationId());

            var spec = new ScheduleByIdWithAppointmentsSpec(request.ScheduleId); // TODO: Just get that day's appointments
            var schedule = await _scheduleReadRepository.GetBySpecAsync(spec);

            var apptToDelete = schedule.Appointments.FirstOrDefault(a => a.Id == request.AppointmentId);
            if (apptToDelete == null) return NotFound();

            schedule.DeleteAppointment(apptToDelete);

            await _scheduleRepository.UpdateAsync(schedule);

            // verify we can still get the schedule
            response.Schedule = _mapper.Map<ScheduleDto>(await _scheduleReadRepository.GetBySpecAsync(spec));

            return Ok(response);
        }
    }
}

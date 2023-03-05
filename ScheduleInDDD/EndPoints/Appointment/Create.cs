using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PluralsightDdd.SharedKernel.Interfaces;
using PluralsightDdd.SharedKernel;
using ScheduleInDDD.Core.SyncedAggregates;
using ScheduleInDDD.Models.Appointment;
using Swashbuckle.AspNetCore.Annotations;
using ScheduleEntity = ScheduleInDDD.Core.ScheduleAggregate.Schedule;
using AppointmentEntity = ScheduleInDDD.Core.ScheduleAggregate.Appointment;
using Ardalis.ApiEndpoints;
using ScheduleInDDD.Core.ScheduleAggregate.Specifications;

namespace ScheduleInDDD.EndPoints.Appointment
{
    public class Create : EndpointBaseAsync
   .WithRequest<CreateAppointmentRequest>
   .WithActionResult<CreateAppointmentResponse>
    {
        private readonly IRepository<ScheduleEntity> _scheduleRepository;
        private readonly IReadRepository<AppointmentType> _appointmentTypeReadRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<Create> _logger;

        public Create(IRepository<ScheduleEntity> scheduleRepository,
          IReadRepository<AppointmentType> appointmentTypeReadRepository,
          IMapper mapper,
          ILogger<Create> logger)
        {
            _scheduleRepository = scheduleRepository;
            _appointmentTypeReadRepository = appointmentTypeReadRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost(CreateAppointmentRequest.Route)]
        [SwaggerOperation(
            Summary = "Creates a new Appointment",
            Description = "Creates a new Appointment",
            OperationId = "appointments.create",
            Tags = new[] { "AppointmentEndpoints" })
        ]
        public override async Task<ActionResult<CreateAppointmentResponse>> HandleAsync(CreateAppointmentRequest request,
          CancellationToken cancellationToken)
        {
            var response = new CreateAppointmentResponse(request.CorrelationId());

            var spec = new ScheduleByIdWithAppointmentsSpec(request.ScheduleId); // TODO: Just get that day's appointments
            var schedule = await _scheduleRepository.GetBySpecAsync(spec);

            var appointmentType = await _appointmentTypeReadRepository.GetByIdAsync(request.AppointmentTypeId);
            var appointmentStart = request.DateOfAppointment;
            var timeRange = new DateTimeOffsetRange(appointmentStart, TimeSpan.FromMinutes(appointmentType.Duration));

            var newAppointment = new AppointmentEntity(Guid.NewGuid(), request.AppointmentTypeId, request.ScheduleId,
              request.ClientId, request.SelectedDoctor, request.PatientId, request.RoomId, timeRange, request.Title);

            schedule.AddNewAppointment(newAppointment);

            await _scheduleRepository.UpdateAsync(schedule);
            _logger.LogInformation($"Appointment created for patient {request.PatientId} with Id {newAppointment.Id}");

            var dto = _mapper.Map<AppointmentDto>(newAppointment);
            _logger.LogInformation(dto.ToString());
            response.Appointment = dto;

            return Ok(response);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PluralsightDdd.SharedKernel.Interfaces;
using ScheduleInDDD.Core.ScheduleAggregate.Specifications;
using ScheduleInDDD.Core.ScheduleAggregate;
using ScheduleInDDD.Models.Appointment;
using Swashbuckle.AspNetCore.Annotations;
using Ardalis.ApiEndpoints;
using ScheduleEntity = ScheduleInDDD.Core.ScheduleAggregate.Schedule;
using ScheduleInDDD.Core.SyncedAggregates.Specifications;

namespace ScheduleInDDD.EndPoints.Appointment
{
    public class GetById : EndpointBaseAsync
  .WithRequest<GetByIdAppointmentRequest>
  .WithActionResult<GetByIdAppointmentResponse>
    {
        private readonly IReadRepository<ScheduleEntity> _scheduleRepository;
        private readonly IReadRepository<Client> _clientRepository;
        private readonly IMapper _mapper;

        public GetById(IReadRepository<ScheduleEntity> scheduleRepository,
          IReadRepository<Client> clientRepository,
          IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        [HttpGet(GetByIdAppointmentRequest.Route)]
        [SwaggerOperation(
            Summary = "Get an Appointment by Id",
            Description = "Gets an Appointment by Id",
            OperationId = "appointments.GetById",
            Tags = new[] { "AppointmentEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdAppointmentResponse>> HandleAsync([FromRoute] GetByIdAppointmentRequest request, CancellationToken cancellationToken)
        {
            var response = new GetByIdAppointmentResponse(request.CorrelationId());

            var spec = new ScheduleByIdWithAppointmentsSpec(request.ScheduleId); // TODO: Just get that day's appointments
            var schedule = await _scheduleRepository.GetBySpecAsync(spec);

            var appointment = schedule.Appointments.FirstOrDefault(a => a.Id == request.AppointmentId);
            if (appointment == null) return NotFound();

            response.Appointment = _mapper.Map<AppointmentDto>(appointment);

            // load names
            var clientSpec = new ClientByIdIncludePatientsSpecification(appointment.ClientId);
            var client = await _clientRepository.GetBySpecAsync(clientSpec);
            var patient = client.Patients.First(p => p.Id == appointment.PatientId);

            response.Appointment.ClientName = client.FullName;
            response.Appointment.PatientName = patient.Name;

            return Ok(response);
        }
    }
}

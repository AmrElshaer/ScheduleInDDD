using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PluralsightDdd.SharedKernel.Interfaces;
using ScheduleInDDD.Core.ScheduleAggregate.Specifications;
using ScheduleInDDD.Core.ScheduleAggregate;
using ScheduleInDDD.Core.SyncedAggregates.Specifications;
using ScheduleInDDD.Models.Appointment;
using Swashbuckle.AspNetCore.Annotations;
using Ardalis.ApiEndpoints;
using ScheduleEntity = ScheduleInDDD.Core.ScheduleAggregate.Schedule;
using ScheduleInDDD.Core.Interfaces;
using ScheduleInDDD.Core.Exceptions;

namespace ScheduleInDDD.EndPoints.Appointment
{
    public class List : EndpointBaseAsync
    .WithRequest<ListAppointmentRequest>
    .WithActionResult<ListAppointmentResponse>
    {
        private readonly IReadRepository<ScheduleEntity> _scheduleRepository;
        private readonly IReadRepository<Client> _clientRepository;
        private readonly IMapper _mapper;
        private readonly IApplicationSettings _settings;
        private readonly ILogger<List> _logger;

        public List(IReadRepository<ScheduleEntity> scheduleRepository,
          IReadRepository<Client> clientRepository,
          IMapper mapper,
          IApplicationSettings settings,
          ILogger<List> logger)
        {
            _scheduleRepository = scheduleRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
            _settings = settings;
            _logger = logger;
        }

        [HttpGet(ListAppointmentRequest.Route)]
        [SwaggerOperation(
            Summary = "List Appointments",
            Description = "List Appointments",
            OperationId = "appointments.List",
            Tags = new[] { "AppointmentEndpoints" })
        ]
        public override async Task<ActionResult<ListAppointmentResponse>> HandleAsync([FromRoute] ListAppointmentRequest request,
          CancellationToken cancellationToken)
        {
            var response = new ListAppointmentResponse(request.CorrelationId());
            ScheduleEntity schedule = null;
            if (request.ScheduleId == Guid.Empty)
            {
                return NotFound();
            }

            // TODO: Get date from API request and use a specification that only includes appointments on that date.
            var spec = new ScheduleByIdWithAppointmentsSpec(request.ScheduleId);
            schedule = await _scheduleRepository.GetBySpecAsync(spec);
            if (schedule == null) throw new ScheduleNotFoundException($"No schedule found for id {request.ScheduleId}.");

            int conflictedAppointmentsCount = schedule.Appointments
              .Count(a => a.IsPotentiallyConflicting);
            _logger.LogInformation($"API:ListAppointments There are now {conflictedAppointmentsCount} conflicted appointments.");

            var myAppointments = _mapper.Map<List<AppointmentDto>>(schedule.Appointments);

            // load names - only do this kind of thing if you have caching!
            // N+1 query problem
            // Possibly use custom SQL or view or stored procedure instead
            foreach (var appt in myAppointments)
            {
                var clientSpec = new ClientByIdIncludePatientsSpecification(appt.ClientId);
                var client = await _clientRepository.GetBySpecAsync(clientSpec);
                var patient = client.Patients.First(p => p.Id == appt.PatientId);

                appt.ClientName = client.FullName;
                appt.PatientName = patient.Name;
            }

            response.Appointments = myAppointments.OrderBy(a => a.Start).ToList();
            response.Count = response.Appointments.Count;

            return Ok(response);
        }

    }
}

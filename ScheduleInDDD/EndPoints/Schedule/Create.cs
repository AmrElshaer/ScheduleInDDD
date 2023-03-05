using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PluralsightDdd.SharedKernel.Interfaces;
using ScheduleInDDD.Models.Schedule;
using Swashbuckle.AspNetCore.Annotations;
using ScheduleEntity = ScheduleInDDD.Core.ScheduleAggregate.Schedule;
using Ardalis.ApiEndpoints;
namespace ScheduleInDDD.EndPoints.Schedule
{
    public class Create : EndpointBaseAsync
   .WithRequest<CreateScheduleRequest>
   .WithActionResult<CreateScheduleResponse>
    {
        private readonly IRepository<ScheduleEntity> _repository;
        private readonly IMapper _mapper;

        public Create(IRepository<ScheduleEntity> repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("api/schedules")]
        [SwaggerOperation(
            Summary = "Creates a new Schedule",
            Description = "Creates a new Schedule",
            OperationId = "schedules.create",
            Tags = new[] { "ScheduleEndpoints" })
        ]
        public override async Task<ActionResult<CreateScheduleResponse>> HandleAsync(CreateScheduleRequest request,
          CancellationToken cancellationToken)
        {
            var response = new CreateScheduleResponse(request.CorrelationId());

            var toAdd = _mapper.Map<ScheduleEntity>(request);
            toAdd = await _repository.AddAsync(toAdd);

            var dto = _mapper.Map<ScheduleDto>(toAdd);
            response.Schedule = dto;

            return Ok(response);
        }
    }
}

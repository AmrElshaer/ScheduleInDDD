using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PluralsightDdd.SharedKernel.Interfaces;
using ScheduleInDDD.Models.Schedule;
using Swashbuckle.AspNetCore.Annotations;
using ScheduleEntity = ScheduleInDDD.Core.ScheduleAggregate.Schedule;
namespace ScheduleInDDD.EndPoints.Schedule
{
    public class GetById : EndpointBaseAsync
     .WithRequest<GetByIdScheduleRequest>
     .WithActionResult<GetByIdScheduleResponse>
    {
        private readonly IReadRepository<ScheduleEntity> _repository;
        private readonly IMapper _mapper;

        public GetById(IReadRepository<ScheduleEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("api/schedules/{ScheduleId}")]
        [SwaggerOperation(
            Summary = "Get a Schedule by Id",
            Description = "Gets a Schedule by Id",
            OperationId = "schedules.GetById",
            Tags = new[] { "ScheduleEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdScheduleResponse>> HandleAsync([FromRoute] GetByIdScheduleRequest request, CancellationToken cancellationToken)
        {
            var response = new GetByIdScheduleResponse(request.CorrelationId());

            var schedule = await _repository.GetByIdAsync(request.ScheduleId);
            if (schedule is null) return NotFound();

            response.Schedule = _mapper.Map<ScheduleDto>(schedule);

            return Ok(response);
        }
    }
}

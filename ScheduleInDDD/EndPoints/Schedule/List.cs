using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PluralsightDdd.SharedKernel.Interfaces;
using ScheduleInDDD.Models.Schedule;
using Swashbuckle.AspNetCore.Annotations;
using ScheduleEntity = ScheduleInDDD.Core.ScheduleAggregate.Schedule;
namespace ScheduleInDDD.EndPoints.Schedule
{
    public class List : EndpointBaseAsync
   .WithRequest<ListScheduleRequest>
   .WithActionResult<ListScheduleResponse>
    {
        private readonly IReadRepository<ScheduleEntity> _repository;
        private readonly IMapper _mapper;

        public List(IReadRepository<ScheduleEntity> repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet(ListScheduleRequest.Route)]
        [SwaggerOperation(
            Summary = "List Schedules",
            Description = "List Schedules",
            OperationId = "schedules.List",
            Tags = new[] { "ScheduleEndpoints" })
        ]
        public override async Task<ActionResult<ListScheduleResponse>> HandleAsync([FromQuery] ListScheduleRequest request,
          CancellationToken cancellationToken)
        {
            var response = new ListScheduleResponse(request.CorrelationId());

            var schedules = await _repository.ListAsync();
            if (schedules is null) return NotFound();

            response.Schedules = _mapper.Map<List<ScheduleDto>>(schedules);
            response.Count = response.Schedules.Count;

            return Ok(response);
        }
    }
}

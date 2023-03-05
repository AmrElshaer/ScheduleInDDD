using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PluralsightDdd.SharedKernel.Interfaces;
using ScheduleInDDD.Models.Schedule;
using Swashbuckle.AspNetCore.Annotations;
using ScheduleEntity = ScheduleInDDD.Core.ScheduleAggregate.Schedule;
namespace ScheduleInDDD.EndPoints.Schedule
{
    public class Update : EndpointBaseAsync
  .WithRequest<UpdateScheduleRequest>
  .WithActionResult<UpdateScheduleResponse>
    {
        private readonly IRepository<ScheduleEntity> _repository;
        private readonly IMapper _mapper;

        public Update(IRepository<ScheduleEntity> repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPut("api/schedules")]
        [SwaggerOperation(
            Summary = "Updates a Schedule",
            Description = "Updates a Schedule",
            OperationId = "schedules.update",
            Tags = new[] { "ScheduleEndpoints" })
        ]
        public override async Task<ActionResult<UpdateScheduleResponse>> HandleAsync(UpdateScheduleRequest request,
          CancellationToken cancellationToken)
        {
            var response = new UpdateScheduleResponse(request.CorrelationId());

            var toUpdate = _mapper.Map<ScheduleEntity>(request);
            await _repository.UpdateAsync(toUpdate);

            var dto = _mapper.Map<ScheduleDto>(toUpdate);
            response.Schedule = dto;

            return Ok(response);
        }
    }
}

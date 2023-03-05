using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PluralsightDdd.SharedKernel.Interfaces;
using ScheduleInDDD.Models.Schedule;
using Swashbuckle.AspNetCore.Annotations;
using ScheduleEntity = ScheduleInDDD.Core.ScheduleAggregate.Schedule;
namespace ScheduleInDDD.EndPoints.Schedule
{
    public class Delete : EndpointBaseAsync
    .WithRequest<DeleteScheduleRequest>
    .WithActionResult<DeleteScheduleResponse>
    {
        private readonly IRepository<ScheduleEntity> _repository;
        private readonly IMapper _mapper;

        public Delete(IRepository<ScheduleEntity> repository,
          IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpDelete(DeleteScheduleRequest.Route)]
        [SwaggerOperation(
            Summary = "Deletes a Schedule",
            Description = "Deletes a Schedule",
            OperationId = "schedules.delete",
            Tags = new[] { "ScheduleEndpoints" })
        ]
        public override async Task<ActionResult<DeleteScheduleResponse>> HandleAsync([FromRoute] DeleteScheduleRequest request,
          CancellationToken cancellationToken)
        {
            var response = new DeleteScheduleResponse(request.CorrelationId());

            var toDelete = _mapper.Map<ScheduleEntity>(request);
            await _repository.DeleteAsync(toDelete);

            return Ok(response);
        }
    }
}

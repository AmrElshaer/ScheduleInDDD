using MediatR;

namespace ScheduleInDDD.Infrastructure.Data.Sync
{
  public class CreateDoctorCommand : IRequest<int>
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
}

using System.Threading.Tasks;

namespace ScheduleInDDD.Core.Interfaces
{
  public interface ITokenClaimsService
  {
    Task<string> GetTokenAsync(string userName);
  }
}

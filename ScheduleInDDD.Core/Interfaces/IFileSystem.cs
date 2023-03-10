using System.Threading.Tasks;

namespace ScheduleInDDD.Core.Interfaces
{
  public interface IFileSystem
  {
    Task<bool> SavePicture(string pictureName, string pictureBase64);
  }
}

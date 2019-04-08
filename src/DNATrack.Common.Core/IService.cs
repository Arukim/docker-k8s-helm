using System.Threading.Tasks;

namespace DNATrack.Common.Core
{
    public interface IService
    {
        Task StartAsync();
        Task StopAsync();
    }
}

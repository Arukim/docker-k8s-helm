using System.Threading.Tasks;

namespace DNATrack.Services.Analysis
{
    class Program
    {
        static async Task Main(string[] args) => await new Application().Run(args);
    }
}

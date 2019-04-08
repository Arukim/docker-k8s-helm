using System.Threading.Tasks;

namespace DNATrack.Jobs.Validator
{
    class Program
    {
        static async Task Main(string[] args) => await new Application().Run(args);
    }
}

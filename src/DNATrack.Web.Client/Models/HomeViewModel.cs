using DNATrack.Persistence.Entities;
using System.Collections.ObjectModel;

namespace DNATrack.Web.Client.Models
{
    public class HomeViewModel
    {
        public int Skip { get; set; }
        public ReadOnlyCollection<Trace> Traces { get; set; }
    }
}

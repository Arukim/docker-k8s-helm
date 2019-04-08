using System;

namespace DNATrack.Common.Messaging.Commands
{
    public class NewTrace
    {
        public Guid BatchId { get; set; }
        public int TraceNumber { get; set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DNATrack.Persistence.Entities
{
    public class Trace
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("batchId")]
        public Guid BatchId { get; set; }

        [BsonElement("traceNumber")]
        public int TraceNumber { get; set; }

        [BsonElement("dna")]
        public byte[] DNA { get; set; }

        [BsonElement("lastValidation")]
        public DateTime? LastValidation { get; set; }
    }
}

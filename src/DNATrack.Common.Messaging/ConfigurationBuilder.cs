using System;

namespace DNATrack.Common.Messaging
{
    public static class ConfigurationBuilder
    {
        public static Uri GetEndpoint(RabbitMQConfiguration config, string queue)
            => new Uri($"{config.Endpoint}/{queue}");
    }
}

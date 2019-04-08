namespace DNATrack.Common.Core
{
    public static class Constants
    {
        public const string LinuxConfigPath = "/etc/config/appsettings.json";
        public static class ConfigSections
        {
            public const string Rabbit = "rabbit";
            public const string Mongo = "mongo";
        }

        public static class Queues
        {
            public const string NewTrace = "new_trace";
        }
    }
}

namespace CardPro.Server.Settings
{
    public class CacheSettings
    {
        public TimeSpan AbsoluteExpirationRelativeToNow { get; set; }
        public TimeSpan SlidingExpiration { get; set; }
    }
}

namespace CardPro.Server.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public TimeSpan AbsoluteExpirationRelativeToNow { get; set; }
    }
}

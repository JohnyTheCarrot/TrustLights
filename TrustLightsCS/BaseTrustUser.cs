namespace TrustLightsCS
{
    public class BaseTrustUser
    {
        public string person_name { get; set; }
        public string newsletter { get; set; }
        public bool ipcam_only { get; set; }
        public BaseTrustHome[] homes { get; set; }
        public object[] cameras { get; set; }
    }
}

namespace valet.lib.Config
{
    public class ValetOptions
    {
        public bool EnableAuthRepositories { get; set; } = false;
        public bool EnablePasswordHasher { get; set; } = false;
        public bool EnableTokenJwtGeneration { get; set; } = false;
        public bool EnableValetSwaggerGen { get; set; } = false;
    }
}

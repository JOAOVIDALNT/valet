using System.Reflection;

namespace valet.lib.Config
{
    /// <summary>
    /// Configuration options to enable or disable Valet modules such as authentication,
    /// password hashing, and Swagger generation.
    /// </summary>
    public class ValetOptions
    {
        private readonly HashSet<Assembly> _useCasesAssemblies = new();

        public IReadOnlyCollection<Assembly> UseCasesAssemblies => _useCasesAssemblies;
        
        /// <summary>
        /// Gets or sets a value indicating whether Valet authentication services should be enabled.
        /// </summary>
        public bool EnableValetAuth { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether Valet password hashing services should be enabled.
        /// </summary>
        public bool EnableValetHash { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether Valet Swagger generation with JWT support should be enabled.
        /// </summary>
        public bool EnableValetSwaggerGen { get; set; } = false;
        
        /// <summary>
        /// Gets or sets a value indicating whether entity auditing should be enabled.
        /// </summary>
        public bool EnableValetAuditing { get; set; } = false;
        
        public ValetOptions AddUseCasesFrom<T>()
        {
            _useCasesAssemblies.Add(typeof(T).Assembly);
            return this;
        }
    }
}

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using valet.lib.Core.Audition;

namespace valet.lib.Config
{

    /// <summary>
    /// Represents the configuration options used to compose Valet features
    /// and infrastructure behaviors during service registration.
    /// </summary>
    /// <remarks>
    /// This class follows a fluent configuration style. Features are enabled
    /// explicitly through intention-revealing methods such as <see cref="UseAuth"/>,
    /// <see cref="UseSwaggerGen"/>, <see cref="UseAuditing"/> and <see cref="AddUseCasesFrom{T}"/>.
    /// </remarks>
    public class ValetOptions
    {
        private readonly HashSet<Assembly> _useCasesAssemblies = new();
        private readonly HashSet<Type> _interceptors = new();

        /// <summary>
        /// Gets the collection of assemblies that will be scanned for UseCase types.
        /// </summary>
        /// <remarks>
        /// Assemblies are added through <see cref="AddUseCasesFrom{T}"/>.
        /// </remarks>
        public IReadOnlyCollection<Assembly> UseCasesAssemblies => _useCasesAssemblies;
        /// <summary>
        /// Gets the collection of interceptor types that will be registered
        /// and applied to the configured <see cref="DbContext"/>.
        /// </summary>
        /// <remarks>
        /// Interceptors are added through feature methods such as <see cref="UseAuditing"/>.
        /// </remarks>
        public IReadOnlyCollection<Type> Interceptors => _interceptors;
        
        internal bool ValetAuthFeature { get; private set; }
        
        internal bool ValetSwaggerGenFeature { get; private set; }
        
        /// <summary>
        /// Registers the assembly that contains the specified type for automatic
        /// UseCase discovery and dependency injection.
        /// </summary>
        /// <typeparam name="T">
        /// Any type located in the target assembly to be scanned.
        /// </typeparam>
        /// <returns>The current <see cref="ValetOptions"/> instance.</returns>
        public ValetOptions AddUseCasesFrom<T>()
        {
            _useCasesAssemblies.Add(typeof(T).Assembly);
            return this;
        }
        
        /// <summary>
        /// Enables auditing support by registering the default
        /// <see cref="AuditInterceptor"/>.
        /// </summary>
        /// <returns>The current <see cref="ValetOptions"/> instance.</returns>
        public ValetOptions UseAuditing()
        {
            _interceptors.Add(typeof(AuditInterceptor));
            return this;
        }

        /// <summary>
        /// Enables Valet authentication services and related infrastructure.
        /// </summary>
        /// <returns>The current <see cref="ValetOptions"/> instance.</returns>
        public ValetOptions UseAuth()
        {
            ValetAuthFeature = true;
            return this;
        }
        
        /// <summary>
        /// Enables Swagger generation with JWT security configuration.
        /// </summary>
        /// <returns>The current <see cref="ValetOptions"/> instance.</returns>
        public ValetOptions UseSwaggerGen()
        {
            ValetSwaggerGenFeature = true;
            return this;
        }
    }
}

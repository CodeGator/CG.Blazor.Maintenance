using Microsoft.Extensions.DependencyInjection;
using System;

namespace CG.Blazor.Maintenance
{
    /// <summary>
    /// This class is an implementation of a service provider.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Yes, yes, I know. Service locator is bad. Service locator is an 
    /// anti-pattern. Service locator is the devil. Service locator eats newborn 
    /// babies for lunch and smashes baby puppies for fun. I know, I do! But, 
    /// I need a service provider within a static callback, and so ... It seems 
    /// we are reduced to associating with the hustlers and the thieves of the 
    /// software engineering world. Forever unclean!!
    /// </para>
    /// </remarks>
    internal class ServiceLocator
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This propertyt contains a reference to a service provider.
        /// </summary>
        public static IServiceProvider ServiceProvider { get; set; }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method returns an instance of the specified service type.
        /// </summary>
        /// <typeparam name="T">The type of service to return.</typeparam>
        /// <returns>An object of ttype <typeparamref name="T"/>.</returns>
        public static T GetRequiredService<T>() where T : class
        {
            // Defer to the provider.
            return ServiceProvider.GetRequiredService<T>();
        }

        // *******************************************************************

        /// <summary>
        /// This method returns an <see cref="IServiceScope"/> instance.
        /// </summary>
        /// <returns>An <see cref="IServiceScope"/> instance.</returns>
        public static IServiceScope CreateScope()
        {
            // Defer to the provider.
            return ServiceProvider.CreateScope();
        }

        #endregion
    }
}

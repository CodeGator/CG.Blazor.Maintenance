using CG.Blazor.Maintenance.Options;
using CG.Blazor.Maintenance.Rules;
using CG.Blazor.Maintenance.Services;
using CG.Blazor.Plugins;
using CG.DataProtection;
using CG.Validations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CG.Blazor.Maintenance
{
    /// <summary>
    /// This class represents the plugin module's startup logic.
    /// </summary>
    public class Module : ModuleBase
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <inheritdoc/>
        public override void ConfigureServices(
            IServiceCollection serviceCollection,
            IConfiguration configuration
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
                .ThrowIfNull(configuration, nameof(configuration));

            // Configure the plugin options.
            serviceCollection.ConfigureOptions<PluginOptions>(
                DataProtector.Instance(), // <-- default data protector.
                configuration
                );

            // Register our custom url rewriting rule.
            serviceCollection.AddSingleton<MaintenanceModeRule>();

            // Register our custom service.
            serviceCollection.AddSingleton<IMaintenanceModeService, MaintenanceModeService>();
        }

        // *******************************************************************

        /// <inheritdoc/>
        public override void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(app, nameof(app))
                .ThrowIfNull(env, nameof(env));

            // Setup the service locator.
            ServiceLocator.ServiceProvider = app.ApplicationServices;

            // Get the maintenance rule.
            var rule = app.ApplicationServices.GetRequiredService<MaintenanceModeRule>();

            // Add the rule to the framework.
            app.UseRewriter(new RewriteOptions().Add(rule));
        }

        #endregion
    }
}

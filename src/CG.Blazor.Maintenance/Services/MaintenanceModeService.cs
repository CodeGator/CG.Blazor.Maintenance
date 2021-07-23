using CG.Blazor.Maintenance.Options;
using CG.Validations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CG.Blazor.Maintenance.Services
{
    /// <summary>
    /// This class is a default implementation of the <see cref="IMaintenanceModeService"/>
    /// interface.
    /// </summary>
    internal class MaintenanceModeService : IMaintenanceModeService
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields

        /// <summary>
        /// This field contains a logger.
        /// </summary>
        private readonly ILogger<MaintenanceModeService> _logger;

        /// <summary>
        /// This field contains the plugin options.
        /// </summary>
        private readonly IOptions<PluginOptions> _pluginOptions;

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="MaintenanceModeService"/>
        /// class.
        /// </summary>
        public MaintenanceModeService(
            IOptions<PluginOptions> pluginOptions,
            ILogger<MaintenanceModeService> logger
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(pluginOptions, nameof(pluginOptions))
                .ThrowIfNull(logger, nameof(logger));

            // Save the references.
            _pluginOptions = pluginOptions;
            _logger = logger;
        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <inheritdoc/>
        public virtual bool IsInMaintenanceMode()
        {
            // For now, this is the only way we have to detect maintenance mode.
            if (_pluginOptions.Value.MaintenanceMode)
            {
                return true;
            }

            // Not in maintenance mode.
            return false;
        }

        #endregion
    }
}

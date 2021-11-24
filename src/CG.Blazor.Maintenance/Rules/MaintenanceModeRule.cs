using CG.Blazor.Maintenance.Services;
using CG.Validations;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Logging;

namespace CG.Blazor.Maintenance.Rules
{
    /// <summary>
    /// This class represents a rule for rewriting urls during maintenance mode.
    /// </summary>
    internal class MaintenanceModeRule : IRule
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields

        /// <summary>
        /// This field contains a logger.
        /// </summary>
        private readonly ILogger<MaintenanceModeRule> _logger;

        /// <summary>
        /// This field contains a maintenance service.
        /// </summary>
        private readonly IMaintenanceModeService _service;

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="MaintenanceModeRule"/>
        /// class.
        /// </summary>
        /// <param name="service">The maintenance service to use with the rule.</param>
        /// <param name="logger">The logger to use with the rule.</param>
        public MaintenanceModeRule(
            IMaintenanceModeService service,
            ILogger<MaintenanceModeRule> logger
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(service, nameof(service))
                .ThrowIfNull(logger, nameof(logger));

            // Save the references.
            _service = service;
            _logger = logger;
        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <inheritdoc/>
        public void ApplyRule(RewriteContext context)
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(context, nameof(context));

            // Are we, in fact, in maintenance mode?
            if (_service.IsInMaintenanceMode())
            {
                // If we get here then we are in maintenance mode, so, we
                //   want to rewrite ALL url's to our maintenance page.

                // Redirect everything except the maintenance page.
                if (!context.HttpContext.Request.Path.StartsWithSegments("/maintenance"))
                {
                    // Tell the world what we're doing.
                    _logger.LogInformation(
                        $"Rewriting url '{context.HttpContext.Request.Path.Value}' to '/maintainance'"
                        );

                    // Rewrite the url.
                    context.HttpContext.Request.Path = "/maintenance";
                }
            }
            else
            {
                // If we get here then we are NOT in maintenance mode, so, we
                //   want to prevent the maintenance page from ever showing.

                // Redirect away from the maintenance page.
                if (context.HttpContext.Request.Path.StartsWithSegments("/maintenance"))
                {
                    // Tell the world what we're doing.
                    _logger.LogInformation(
                        $"Rewriting url '{context.HttpContext.Request.Path.Value}' to '/'"
                        );

                    // Rewrite the url.
                    context.HttpContext.Request.Path = "/";
                }
            }
        }

        #endregion
    }
}

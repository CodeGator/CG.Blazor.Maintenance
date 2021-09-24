using CG.Blazor.Maintenance.Services;
using CG.Validations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CG.Blazor.Maintenance
{
    /// <summary>
    /// This class utility exposes a static callback for hooking into client-
    /// side navigation events during maintenance mode.
    /// </summary>
    public static class MaintenanceRedirector
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method may be wired up to Blazor's <see cref="Router.OnNavigateAsync"/>
        /// callback, in the App.razor file, in order to gain the ability to 
        /// redirect client-side navigation events to server-side navigation
        /// events, during maintenance mode.
        /// </summary>
        /// <param name="navigationContext">The navigation context to use for 
        /// the operation.</param>
        /// <param name="navigationManager">The navigation manager to use for
        /// the operation.</param>
        /// <returns>A task to perform the operation.</returns>
        public static Task OnNavigateAsync(
            NavigationContext navigationContext,
            NavigationManager navigationManager
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(navigationContext, nameof(navigationContext))
                .ThrowIfNull(navigationManager, nameof(navigationManager));

            // Get a logger.
            var logger = ServiceLocator
                .GetRequiredService<ILogger<Module>>();

            // Get the maintenance mode service.
            var maintenanceModeService = ServiceLocator
                .GetRequiredService<IMaintenanceModeService>();

            // Are we, in fact, in maintenance mode?
            if (maintenanceModeService.IsInMaintenanceMode())
            {
                // If we get here then we are in maintenance mode, so,
                //   we shouldn't allow anything but the maintenance 
                //   page to display.

                // If we redirect these urls we tend to break Blazor.
                if (navigationContext.Path.StartsWith("/_blazor") ||
                    navigationContext.Path.StartsWith("/service-worker.js"))
                {
                    return Task.CompletedTask; // Nothing to do.
                }

                // Are we navigating to anything besides the maintenance page?
                if (!navigationContext.Path.StartsWith("/maintenance") && 
                    !navigationContext.Path.StartsWith("maintenance"))
                {
                    try 
                    {
                        // Tell the world what we are about to do.
                        logger.LogInformation(
                            "Rewriting url '{Url1}' to '/maintenance'",
                            navigationContext.Path
                            );

                        // Navigate using force (to fetch from the server).
                        navigationManager.NavigateTo(
                            "/maintenance",
                            true
                            );
                    }
                    catch (Exception ex)
                    {
                        // NOTE: Not sure there's really anything to do about this,
                        //   it probably just means the callback was triggered before
                        //   the navigation manager was initialized. In any event, we
                        //   don't control the callback or the manager, so, pfft. 

                        // Tell the world what happened.
                        logger.LogWarning(
                            ex,
                            "Failed to redirect to the maintenance page."
                            );
                    } 
                }
            }
            else
            {
                // If we get here then we are NOT in maintenance mode, so, 
                //   we should allow ALL pages except the maintenance page.

                // Are we navigating to anything besides the maintenance page?
                if (navigationContext.Path.StartsWith("/maintenance") ||
                    navigationContext.Path.StartsWith("maintenance"))
                {
                    try
                    {
                        // Tell the world what we are about to do.
                        logger.LogInformation(
                            "Rewriting url '{Url1}' to '/'",
                            navigationContext.Path
                            );

                        // Navigate to the root.
                        navigationManager.NavigateTo(
                            "/",
                            true
                            );
                    }
                    catch (Exception ex)
                    {
                        // NOTE: Not sure there's really anything to do about this,
                        //   it probably just means the callback was triggered before
                        //   the navigation manager was initialized. In any event, we
                        //   don't control the callback or the manager, so, pfft. 

                        // Tell the world what happened.
                        logger.LogWarning(
                            ex,
                            "Failed to redirect away from the maintenance page."
                            );
                    }
                }
            }

            // Return the task.
            return Task.CompletedTask;
        }

        #endregion
    }
}

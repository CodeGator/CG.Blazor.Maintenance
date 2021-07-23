using System;

namespace CG.Blazor.Maintenance.Services
{
    /// <summary>
    /// This interface represents an object that manages maintenance mode
    /// for a Blazor website.
    /// </summary>
    internal interface IMaintenanceModeService
    {
        /// <summary>
        /// This method indicates whether the website is currently in 
        /// maintenance mode, or not.
        /// </summary>
        /// <returns>True if the website is currently maintenance mode;
        /// False otherwise.
        bool IsInMaintenanceMode();
    }
}

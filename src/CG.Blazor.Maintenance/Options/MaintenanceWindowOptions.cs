using CG.Options;
using System;

namespace CG.Blazor.Maintenance.Options
{
    /// <summary>
    /// This class contains configuration settings related to an automated
    /// maintenance window.
    /// </summary>
    public class MaintenanceWindowOptions : OptionsBase
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains an optional CRON expression for scheduling 
        /// an automated maintenance window. If this property isn't specified,
        /// no maintenance window is opened.
        /// </summary>
        public string CRON { get; set; }

        /// <summary>
        /// This property contains an optional duration for an automated 
        /// maintenance window. If this property isn't specified, it defaults
        /// to a fifteen minute duration.
        /// </summary>
        public TimeSpan? Duration { get; set; }

        #endregion
    }
}

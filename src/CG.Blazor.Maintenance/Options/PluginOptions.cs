using CG.Options;
using System;

namespace CG.Blazor.Maintenance.Options
{
    /// <summary>
    /// This class containes configuration settings related to the setup plugin.
    /// </summary>
    public class PluginOptions : OptionsBase
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property indicates whether maintenance mode has been manually 
        /// enabled, or not.
        /// </summary>
        public bool MaintenanceMode { get; set; }

        #endregion
    }
}

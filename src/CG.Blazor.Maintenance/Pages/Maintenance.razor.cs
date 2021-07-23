using CG.Blazor.Maintenance.Options;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System;

namespace CG.Blazor.Maintenance.Pages
{
    /// <summary>
    /// This class is the code-behind for the <see cref="Maintenance"/> razor page.
    /// </summary>
    public partial class Maintenance
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the plugin options.
        /// </summary>
        [Inject]
        private IOptions<PluginOptions> PluginOptions { get; set; }

        #endregion
    }
}

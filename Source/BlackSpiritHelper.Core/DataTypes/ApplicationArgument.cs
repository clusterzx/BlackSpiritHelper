﻿namespace BlackSpiritHelper.Core
{
    /// <summary>
    /// List of all possible arguments.
    /// Use ToString() to get the name of a argument.
    /// </summary>
    public enum ApplicationArgument
    {
        /// <summary>
        /// Contains version - ClickOnce AsAdministrator requirement to pass the version to the new process after restart
        /// Type: string
        /// </summary>
        Version,

        /// <summary>
        /// Indicates, the app is restarted after freshly after new updated deploy.
        /// Type: bool
        /// Reading values is not required. It exists in arguments = True, it does not exists in arguments = False
        /// </summary>
        UpdateRestart,
    }
}

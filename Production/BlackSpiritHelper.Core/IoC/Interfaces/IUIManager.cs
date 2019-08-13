﻿using System;
using System.Threading.Tasks;

namespace BlackSpiritHelper.Core
{
    /// <summary>
    /// The UI manager that handles any UI interaction in the application.
    /// </summary>
    public interface IUIManager
    {
        /// <summary>
        /// DIsplays a single message box to the user.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        Task ShowMessage(MessageBoxDialogViewModel viewModel);

        /// <summary>
        /// Open file browser dialog.
        /// </summary>
        /// <param name="action">Action on success/select browser item.</param>
        /// <returns></returns>
        Task ShowFolderBrowserDialog(Action<string> action);

        /// <summary>
        /// Open overlay window.
        /// </summary>
        void OpenOverlay();

        /// <summary>
        /// Close overlay window.
        /// </summary>
        void CloseOverlay();

        /// <summary>
        /// Open progress window.
        /// </summary>
        /// <param name="viewModel"></param>
        void OpenProgressWindow(ProgressDialogViewModel viewModel = null);

        /// <summary>
        /// Close progress window.
        /// </summary>
        void CloseProgressWindow();
    }
}

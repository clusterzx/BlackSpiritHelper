﻿using System;
using System.Windows.Threading;

namespace BlackSpiritHelper.Core
{
    public class TimerListItemViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// DIspatcherTimer to control the timer.
        /// </summary>
        private DispatcherTimer mTimer;

        #endregion

        #region Public Properties

        /// <summary>
        /// The group id the timer belongs to.
        /// </summary>
        public byte GroupID { get; set; }

        /// <summary>
        /// Timer title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Text shortcut to show instead of image icon. Only if there is no image icon.
        /// </summary>
        public string IconTitleShortcut { get; set; }

        /// <summary>
        /// Icon background if there is no image icon.
        /// Color representation in HEX.
        /// f.e. FA9BC2
        /// </summary>
        public string IconBackgroundHEX { get; set; }

        /// <summary>
        /// Formatted time output.
        /// </summary>
        public string CountdownTimeFormat { get; private set; }

        /// <summary>
        /// Countdown before timer starts.
        /// </summary>
        public TimeSpan CountdownDuration { get; set; }

        /// <summary>
        /// Button control.
        /// </summary>
        public TimerState State { get; set; }

        /// <summary>
        /// Says, the timer is currently playing (True) or it is another state (False).
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Says, if the timer is in infinite loop.
        /// </summary>
        public bool IsLoopActive { get; set; }

        /// <summary>
        /// Says, if the timer is in warning time (less than X).
        /// </summary>
        public bool IsWarningTime { get; set; }

        #endregion

        #region Constructor

        public TimerListItemViewModel()
        {
            mTimer = new DispatcherTimer();
        }

        #endregion
    }
}

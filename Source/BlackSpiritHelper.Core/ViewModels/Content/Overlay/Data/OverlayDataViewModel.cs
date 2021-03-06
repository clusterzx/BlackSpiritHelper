﻿using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace BlackSpiritHelper.Core
{
    public class OverlayDataViewModel : ADataContentBaseViewModel<OverlayDataViewModel>
    {
        #region Private Members

        /// <summary>
        /// Says, if the overlay is shown (opened) or not.
        /// </summary>
        private bool mIsOpened = false;

        /// <summary>
        /// Allow opening the overlay on application start.
        /// </summary>
        private bool mAllowOpenOnStart = true;

        #endregion

        #region Public Properties

        /// <summary>
        /// Says, if the overlay is shown (opened) or not.
        /// </summary>
        [XmlIgnore]
        public bool IsOpened
        {
            get => mIsOpened;
            set
            {
                mIsOpened = value;
                OpenOnStart = AllowOpenOnStart ? value : false;
            }
        }

        /// <summary>
        /// Allow opening the overlay on application start.
        /// </summary>
        public bool AllowOpenOnStart
        {
            get => mAllowOpenOnStart;
            set
            {
                mAllowOpenOnStart = value;
                OpenOnStart = value ? IsOpened : false;
            }
        }

        /// <summary>
        /// Says, if the overlay should be opened on application start (the next start).
        /// </summary>
        public bool OpenOnStart { get; set; } = false;

        /// <summary>
        /// X axis position of the overlay object.
        /// </summary>
        public double PosX { get; set; } = 0;

        /// <summary>
        /// Y axis position of the overlay object.
        /// </summary>
        public double PosY { get; set; } = 0;

        /// <summary>
        /// Says, if the dragging with overlay object is locked or not.
        /// </summary>
        public bool IsDraggingLocked { get; set; } = false;

        /// <summary>
        /// Overlay orientation.
        /// </summary>
        public Orientation OverlayOrientation { get; set; } = Orientation.Horizontal;

        /// <summary>
        /// List of all types of <see cref="Orientation"/>.
        /// </summary>
        [XmlIgnore]
        public Orientation[] OrientationList { get; private set; } = (Orientation[])Enum.GetValues(typeof(Orientation));

        /// <summary>
        /// Overlay size.
        /// <see cref="OverlaySize"/> handles the sizes in pixels.
        /// </summary>
        public OverlaySize OverlaySize { get; set; } = OverlaySize.Normal;

        /// <summary>
        /// List of all types of <see cref="OverlaySize"/>.
        /// </summary>
        [XmlIgnore]
        public OverlaySize[] OverlaySizeList { get; private set; } = (OverlaySize[])Enum.GetValues(typeof(OverlaySize));

        /// <summary>
        /// Get pixel size string for style application.
        /// </summary>
        [XmlIgnore]
        public double OverlaySizeStyleValue => (double)OverlaySize;

        /// <summary>
        /// Get pixel size string for Width style application.
        /// </summary>
        [XmlIgnore]
        public double OverlaySizeStyleWidthValue => GetUserPreferredOverlaySizeWidthStyleValue();

        /// <summary>
        /// Get pixel size string for Height style application.
        /// </summary>
        [XmlIgnore]
        public double OverlaySizeStyleHeightValue => GetUserPreferredOverlaySizeHeightStyleValue();

        [XmlIgnore]
        public override bool IsRunning
        {
            get => false;
            protected set => throw new NotImplementedException();
        }

        #endregion

        #region Commands

        /// <summary>
        /// The command to lock overlay dragging.
        /// </summary>
        [XmlIgnore]
        public ICommand LockOverlayDraggingCommand { get; set; }

        /// <summary>
        /// The command to show main window.
        /// </summary>
        [XmlIgnore]
        public ICommand ShowMainWindowCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public OverlayDataViewModel()
        {
            // Create commands.
            CreateCommands();
        }

        protected override void InitRoutine(params object[] parameters)
        {
        }

        protected override void SetDefaultsRoutine()
        {
        }

        protected override void DisposeRoutine()
        {
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Create commands.
        /// </summary>
        private void CreateCommands()
        {
            LockOverlayDraggingCommand = new RelayCommand(() => LockOverlayCommandMethod());
            ShowMainWindowCommand = new RelayCommand(async () => await ShowMainWindowCommandMethodAsync());
        }

        /// <summary>
        /// Lock/Unlock Overlay dragging.
        /// </summary>
        private void LockOverlayCommandMethod()
        {
            IsDraggingLocked = !IsDraggingLocked;
        }

        /// <summary>
        /// Trigger to show main window.
        /// </summary>
        private async Task ShowMainWindowCommandMethodAsync()
        {
            IoC.UI.ShowMainWindow();

            await Task.Delay(1);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Transform size according to orientation into Width style value.
        /// </summary>
        /// <returns></returns>
        private double GetUserPreferredOverlaySizeWidthStyleValue()
        {
            if (OverlayOrientation == Orientation.Vertical)
                return (double)OverlaySize;

            return double.NaN; // Auto.
        }

        /// <summary>
        /// Transform size according to orientation into Height style value.
        /// </summary>
        /// <returns></returns>
        private double GetUserPreferredOverlaySizeHeightStyleValue()
        {
            if (OverlayOrientation == Orientation.Horizontal)
                return (double)OverlaySize;

            return double.NaN; // Auto.
        }

        #endregion
    }
}

﻿namespace BlackSpiritHelper.Core
{
    /// <summary>
    /// Subclass for <see cref="IoC"/>. There you define all application data structures.
    /// F.e. Timer structure is a bit complex with its groups <see cref="TimerDataViewModel"/>. You have to bind the same view model to multiple views.
    /// FOr this reason, you define these data structures here to easily access it as a singleton.
    /// </summary>
    public class ApplicationDataContent
    {
        #region Public Properties

        /// <summary>
        /// Data structure for the preferences.
        /// </summary>
        public PreferencesDataViewModel PreferencesData { get; private set; }

        /// <summary>
        /// Data structure for timers with its groups.
        /// </summary>
        public TimerDataViewModel TimerData { get; private set; }

        /// <summary>
        /// Data structure for schedule.
        /// </summary>
        public ScheduleDataViewModel ScheduleData { get; private set; }

        /// <summary>
        /// Data structure for the overlay.
        /// </summary>
        public OverlayDataViewModel OverlayData { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// Call <see cref="Setup"/> method right after creating the instance.
        /// </summary>
        public ApplicationDataContent()
        {
        }

        /// <summary>
        /// Option to load user data on application load in order you want.
        /// </summary>
        public void Setup()
        {
            // Preferences.
            PreferencesData = IoC.SettingsStorage.PreferencesData ?? PreferencesDataViewModel.NewDataInstance;
            PreferencesData.Setup();
            PreferencesData.SetDefaults();

            // Timer.
            TimerData = IoC.SettingsStorage.TimerData ?? TimerDataViewModel.NewDataInstance;
            TimerData.Setup();
            TimerData.SetDefaults();

            // Schedule.
            ScheduleData = IoC.SettingsStorage.ScheduleData ?? ScheduleDataViewModel.NewDataInstance;
            ScheduleData.Setup();
            ScheduleData.SetDefaults();

            // Overlay.
            OverlayData = IoC.SettingsStorage.OverlayData ?? OverlayDataViewModel.NewDataInstance;
            OverlayData.Setup();
            OverlayData.SetDefaults();
        }

        /// <summary>
        /// Unset application's data.
        /// "Prepare to die."
        /// </summary>
        public void Unset()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Save all user data (on application exit).
        /// </summary>
        public void SaveUserData()
        {
            // Clear previously saved data, first.
            ClearSavedPreferences();
            ClearSavedTimerData();
            ClearSavedScheduleData();
            ClearSavedOverlayData();

            // Clear saved data commit.
            IoC.SettingsStorage.Save();

            // Save new data.
            SaveApplicationData();
            SaveNewPreferences();
            SaveNewTimerData();
            SaveNewScheduleData();
            SaveNewOverlayData();

            // Save commit.
            IoC.SettingsStorage.Save();

            // Log it.
            IoC.Logger.Log("User data saved!", LogLevel.Info);

            //
            //XmlSerializer mySerializer = new XmlSerializer(typeof(TimerDataViewModel));
            //StreamWriter myWriter = new StreamWriter("prefs.xml");
            //mySerializer.Serialize(myWriter, xb);
            //myWriter.Close();
            //
        }

        #endregion

        #region Private Methods - Save

        #region Application Data

        /// <summary>
        /// Save application data.
        /// </summary>
        private void SaveApplicationData()
        {
            // Save last opened page.
            IoC.SettingsStorage.LastOpenedPage = (byte)IoC.Application.CurrentPage;

            // Save if user wants to run application As Administrator at startup.
            IoC.SettingsStorage.ForceToRunAsAdministrator = IoC.DataContent.PreferencesData.ForceToRunAsAdministrator;

            // Save size of the MainWindow.
            IoC.SettingsStorage.MainWindowSize = IoC.UI.GetMainWindowSize();
        }

        #endregion
        
        #region Timer

        /// <summary>
        /// Save new Timer Page data.
        /// </summary>
        private void SaveNewTimerData()
        {
            // Freeze all running timers, first.
            foreach (TimerGroupDataViewModel g in TimerData.GroupList)
                foreach (TimerItemDataViewModel t in g.TimerList)
                    if (t.IsRunning)
                        t.TimerFreeze();

            // Save new data.
            IoC.SettingsStorage.TimerData = TimerData;
        }

        /// <summary>
        /// Clear saved Timer Page data.
        /// </summary>
        private void ClearSavedTimerData()
        {
            // Clear previous save.
            IoC.SettingsStorage.TimerData = null;
        }

        #endregion
        
        #region Preferences

        /// <summary>
        /// Save new user preferences.
        /// </summary>
        private void SaveNewPreferences()
        {
            // Save new data.
            IoC.SettingsStorage.PreferencesData = PreferencesData;
        }

        /// <summary>
        /// Clear saved user preferences.
        /// </summary>
        private void ClearSavedPreferences()
        {
            // Clear previous save.
            IoC.SettingsStorage.PreferencesData = null;
        }

        #endregion

        #region Overlay

        /// <summary>
        /// Save new overlay settings.
        /// </summary>
        private void SaveNewOverlayData()
        {
            // Save new data.
            IoC.SettingsStorage.OverlayData = OverlayData;
        }

        /// <summary>
        /// Clear saved overlay settings.
        /// </summary>
        private void ClearSavedOverlayData()
        {
            // Clear previous save.
            IoC.SettingsStorage.OverlayData = null;
        }

        #endregion

        #region Schedule

        /// <summary>
        /// Save new overlay settings.
        /// </summary>
        private void SaveNewScheduleData()
        {
            // Save new data.
            IoC.SettingsStorage.ScheduleData = ScheduleData;
        }

        /// <summary>
        /// Clear saved Schedule settings.
        /// </summary>
        private void ClearSavedScheduleData()
        {
            // Clear previous save.
            IoC.SettingsStorage.ScheduleData = null;
        }

        #endregion

        #endregion
    }
}

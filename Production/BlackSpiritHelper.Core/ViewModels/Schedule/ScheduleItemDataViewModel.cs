﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace BlackSpiritHelper.Core
{
    public class ScheduleItemDataViewModel : BaseViewModel
    {
        #region Private Properties

        /// <summary>
        /// Says, if the template is initialized.
        /// </summary>
        private bool mIsInitialized = false;

        /// <summary>
        /// Says if the item is predefined or not.
        /// </summary>
        private bool mIsPredefined = false;

        /// <summary>
        /// Flag that counts number of changes, but only the one can be done per <see cref="mIgnoreListMoveCounterFlagTime"/> time period.
        /// </summary>
        private static int mIgnoreListMoveCounterFlag = 0;

        /// <summary>
        /// Time period (delay) during which <see cref="OnItemIgnoredMoveAsync"/> cannot be done.
        /// </summary>
        private static TimeSpan mIgnoreListMoveCounterFlagTime = TimeSpan.FromSeconds(5);

        #endregion

        #region Public Properties

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; } = "NoName";

        /// <summary>
        /// Representing color.
        /// </summary>
        public string ColorHEX { get; set; } = "000000";

        /// <summary>
        /// Says if the item is predefined or not.
        /// </summary>
        [XmlIgnore]
        public bool IsPredefined => mIsPredefined;

        #endregion

        #region Commands

        /// <summary>
        /// Command to add item to ignored list.
        /// </summary>
        [XmlIgnore]
        public ICommand AddItemToIgnoredCommand { get; set; }

        /// <summary>
        /// Command to remove item from ignored list.
        /// </summary>
        [XmlIgnore]
        public ICommand RemoveItemFromIgnoredCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ScheduleItemDataViewModel()
        {
            // Create commands.
            CreateCommands();
        }

        /// <summary>
        /// Initialize the instance.
        /// </summary>
        /// <param name="isPredefined"></param>
        public void Init(bool isPredefined = false)
        {
            if (mIsInitialized)
                return;
            mIsInitialized = true;

            mIsPredefined = isPredefined;
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Create commands.
        /// </summary>
        private void CreateCommands()
        {
            AddItemToIgnoredCommand = new RelayParameterizedCommand(async (parameter) => await AddItemToIgnoredAsync(parameter));
            RemoveItemFromIgnoredCommand = new RelayParameterizedCommand(async (parameter) => await RemoveItemFromIgnoredAsync(parameter));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Add item to ignore list.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task AddItemToIgnoredAsync(object parameter)
        {
            if (parameter == null || !parameter.GetType().Equals(typeof(string)))
                return;
            string par = (string)parameter;

            // Get item.
            var item = IoC.DataContent.ScheduleDesignModel.GetItemByName(par);
            if (item == null)
            {
                IoC.Logger.Log($"No item found under the name '{par}'!", LogLevel.Warning);
                return;
            }

            // Add.
            IoC.DataContent.ScheduleDesignModel.ItemIgnoredList.Add(item.Name);
            IoC.DataContent.ScheduleDesignModel.ItemIgnoredListPresenter.Add(item);

            // Resort.
            IoC.DataContent.ScheduleDesignModel.SortItemIgnoredList();

            // Procedure after move.
            IoC.Task.Run(async () => await OnItemIgnoredMoveAsync());

            await Task.Delay(1);
        }

        /// <summary>
        /// Remove item from ignore list.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task RemoveItemFromIgnoredAsync(object parameter)
        {
            if (parameter == null || !parameter.GetType().Equals(typeof(string)))
                return;
            string par = (string)parameter;

            // Get item.
            var item = IoC.DataContent.ScheduleDesignModel.GetItemByName(par);
            if (item == null)
            {
                IoC.Logger.Log($"No item found under the name '{par}'!", LogLevel.Warning);
                return;
            }

            // Remove.
            IoC.DataContent.ScheduleDesignModel.ItemIgnoredList.RemoveAll(o => o.ToLower().Equals(item.Name.ToLower()));
            IoC.DataContent.ScheduleDesignModel.ItemIgnoredListPresenter.Remove(item);

            // Resort - to update list.
            IoC.DataContent.ScheduleDesignModel.SortItemIgnoredList();

            // Procedure after move.
            IoC.Task.Run(async () => await OnItemIgnoredMoveAsync());

            await Task.Delay(1);
        }

        /// <summary>
        /// This method is done only if no items is moved from or to ignored list within <see cref="mIgnoreListMoveCounterFlagTime"/>.
        /// </summary>
        /// <returns></returns>
        private async Task OnItemIgnoredMoveAsync()
        {
            mIgnoreListMoveCounterFlag++;
            await Task.Delay(mIgnoreListMoveCounterFlagTime);
            mIgnoreListMoveCounterFlag--;

            if (mIgnoreListMoveCounterFlag > 0)
                return;

            IoC.DataContent.ScheduleDesignModel.FindAndRemarkIgnored();

            await Task.Delay(1);
        }

        #endregion
    }
}

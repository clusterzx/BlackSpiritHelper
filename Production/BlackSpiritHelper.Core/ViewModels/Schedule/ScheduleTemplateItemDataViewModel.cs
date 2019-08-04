﻿using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace BlackSpiritHelper.Core
{
    [Serializable]
    public class ScheduleTemplateDayDataViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Day of the week.
        /// </summary>
        public DayOfWeek DayOfWeek { get; set; }

        /// <summary>
        /// List of all time events.
        /// </summary>
        public ObservableCollection<ScheduleTemplateDayTimeDataViewModel> TimeList { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ScheduleTemplateDayDataViewModel()
        {
        }

        #endregion
    }
}

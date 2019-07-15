﻿using System.Globalization;
using System.Windows.Controls;

namespace BlackSpiritHelper.Core
{
    /// <summary>
    /// Rule for the property <see cref="TimerGroupViewModel.Title"/>.
    /// </summary>
    public class TimerGroupTitleRule : BaseRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string val = GetBoundValue(value) as string;

            // Check data type.
            if (val == null)
                return new ValidationResult(false, "Not a character sequence.");

            val = val.Trim();

            // Check conditions.
            if (val.Length < TimerGroupViewModel.TitleAllowMinChar || val.Length > TimerGroupViewModel.TitleAllowMaxChar)
                return new ValidationResult(false, $"Please enter a group title in the length: {TimerGroupViewModel.TitleAllowMinChar} - {TimerGroupViewModel.TitleAllowMaxChar}.");

            // Check allowed characters.
            if (!StringUtils.CheckAlphanumericString(val, true, true))
                return new ValidationResult(false, "Illegal characters. Group title can contain letters, numbers, underscores and spaces, only.");

            return ValidationResult.ValidResult;
        }
    }
}
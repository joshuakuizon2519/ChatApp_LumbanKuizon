using ChatApp_LumbanKuizon.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChatApp_LumbanKuizon.Converter
{
    public class IsOwnerConverter
    {
        DataClass dataClass = DataClass.GetInstance;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool retVal = false;
            string[] players = value as string[];

            if (players[0].Equals(dataClass.loggedInUser.uid))
            {
                retVal = true;
            }
            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

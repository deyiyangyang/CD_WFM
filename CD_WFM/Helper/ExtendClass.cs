using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFM.Helper
{
    public static class ExtendClass
    {
        public static string ChangeSecondToHHMMSS(this int seconds)
        {

                int hour = seconds / 3600;
                int minute = (seconds % 3600) / 60;
                int second = seconds - hour * 3600 - minute * 60;
                return hour.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0') + ":" + second.ToString().PadLeft(2, '0');

        }

        public static int GetDuration(this DateTime dtEnd,DateTime dtST)
        {
            TimeSpan ts = dtEnd - dtST;
            return (int)ts.TotalSeconds;

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Media;

namespace Hackatown_2018
{
    public class Alarm
    {
        public Context Context { get; private set; }
        private AlarmManager Manager { get; set; }
        private Intent CurrentIntent { get; set; }
        private PendingIntent PendingIntent { get; set; }
        public DateTime DesiredTimeArrival { get; set; }
        public DateTime PreparationTime { get; set; }
        public DateTime DateTime { get; set; }

        public Alarm(Context context)
        {
            Context = context;
            Manager = Context.GetSystemService(Context.AlarmService) as AlarmManager;
            DateTime = new DateTime(2018, 1, 21, 4, 26, 5);
        }

        public void StartAlarm()
        {
            long interval = GetMilliSec(DateTime);
            if (interval > 0)
            {
                CurrentIntent = new Intent(Context, typeof(AlarmReceiver));
                string message = "On se réveille";
                CurrentIntent.PutExtra("message", message);
                PendingIntent = PendingIntent.GetBroadcast(Context, 0, CurrentIntent, 0);

                Manager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + interval, PendingIntent);
            }

        }

        public long GetMilliSec(DateTime time)
        {
            TimeSpan interval = time - DateTime.Now;
            return Convert.ToInt64(interval.TotalMilliseconds);
        }

    }
}
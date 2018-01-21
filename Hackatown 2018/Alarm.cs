using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
//using Hackatown_2018.Broadcast;

namespace Hackatown_2018
{
    public class Alarm
    {
        public Context Context { get; private set; }
        private AlarmManager Manager { get; set; }
        private Intent CurrentIntent { get; set; }
        public string Time { get; set; }
        public string DayOfWeek { get; set; }
        public string DesiredTimeArrival { get; set; }
        public Alarm(Context context)
        {
            Context = context;
            Manager = Context.GetSystemService(Context.AlarmService) as AlarmManager;
        }
        public Alarm(string time, string desiredTimeArrival, string dayOfWeek)
        {
            Time = time;
            DesiredTimeArrival = desiredTimeArrival;
            DayOfWeek = dayOfWeek;
        }
        public void StartAlarm(bool isNotif, bool isRepeat)
        {
            PendingIntent pending;
            //CurrentIntent = new Intent(Context, typeof(AlarmReceiver));
            pending = PendingIntent.GetBroadcast(Context, 0, CurrentIntent, 0);

            Manager.Set(AlarmType.RtcWakeup, SystemClock.ElapsedRealtime(), pending);
        }

        public void StopAlarm()
        {

        }

    }
}
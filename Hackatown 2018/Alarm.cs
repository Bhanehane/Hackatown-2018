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
        public TimeSpan AlarmTime { get; private set; }
        public double[] Position { get; private set; }
        public double[] Destination { get; private set; }

        public Alarm(Context context, DateTime preparationTime, DateTime desiredTimeArrival, double[] position, double[] destination)
        {
            Context = context;
            Manager = Context.GetSystemService(Context.AlarmService) as AlarmManager;
            DesiredTimeArrival = desiredTimeArrival;
            PreparationTime = preparationTime;
            Position = position;
            Destination = destination;
            AlarmTime = TimeSpan.Zero;
        }

        public void StartAlarm()
        {
            long interval = Convert.ToInt64(AlarmTime.TotalMilliseconds);
            CurrentIntent = new Intent(Context, typeof(AlarmReceiver));
            string message = "On se réveille";
            CurrentIntent.PutExtra("message", message);
            PendingIntent = PendingIntent.GetBroadcast(Context, 0, CurrentIntent, 0);

            Manager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + interval, PendingIntent);

        }

        public long GetMilliSecFromNowTo(DateTime time)
        {
            TimeSpan interval = time - DateTime.Now;
            return Convert.ToInt64(interval.TotalMilliseconds);
        }
        public void CalculateAlarmTime(DateTime preparationTime, DateTime desiredTimeArrival)
        {
            AlarmTime = desiredTimeArrival - preparationTime;
        }
    }
}
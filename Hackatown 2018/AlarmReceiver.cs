using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;

namespace Hackatown_2018
{
    [BroadcastReceiver(Enabled = true)]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, "This is my alarm", ToastLength.Short).Show();
            string message = intent.GetStringExtra("message");

            Notification.Builder builder = new Notification.Builder(context).
                SetAutoCancel(true).
                SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Alarm), Stream.Alarm).
                SetContentTitle("Alarm").
                SetContentText(message).
                SetVibrate(new long[] { 0, 500, 300, 500, 300 }).
                SetPriority(4).
                SetSmallIcon(Resource.Drawable.ic_notif);

            NotificationManager notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;
            Notification build = builder.Build();
            notificationManager.Notify(0, build);
        }
    }
}
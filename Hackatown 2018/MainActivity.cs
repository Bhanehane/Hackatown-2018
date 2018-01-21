using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;
using System;

namespace Hackatown_2018
{
    [Activity(Label = "Hackatown_2018", MainLauncher = true)]
    public class MainActivity : Activity
    {
        MyOwnAdapter Adapter;
        private List<Alarm> alarms = new List<Alarm>() { };
        private ListView myList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            // Set our view from the "main" layout resource
            myList = FindViewById<ListView>(Resource.Id.listText);


            Adapter = new MyOwnAdapter(this, alarms);
            myList.Adapter = Adapter;

            var intentNewItem = new Intent(this, typeof(ActivityNewItem));

            //FindViewById<Button>(Resource.Id.addAlarm).Click += (e, o) =>
            //{
            //    adapter.AddItem(new Alarm("00:00", "00:01", "D"));
            //    myList.Adapter = adapter;
            //};
            FindViewById<Button>(Resource.Id.addAlarm).Click += (e, o) =>
            {
                StartActivityForResult(intentNewItem, 1);
            };
            
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                int[] alarm = data.GetIntArrayExtra("alarmTime");
                int[] prepTime = data.GetIntArrayExtra("prepTime");
                double[] positionsI = data.GetDoubleArrayExtra("positionI");
                double[] positionsF = data.GetDoubleArrayExtra("positionF");
                TimeSpan prep = new TimeSpan(prepTime[0], prepTime[1], 0);
                DateTime desire = new DateTime(alarm[0], alarm[1], alarm[2], alarm[3], alarm[4], 0);
                Adapter.AddItem(new Alarm(this, prep, desire, positionsI, positionsF));
                myList.Adapter = Adapter;
            }
        }

    }

}


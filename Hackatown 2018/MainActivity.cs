using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace Hackatown_2018
{
    [Activity(Label = "Hackatown_2018", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private List<Alarm> alarms;
        private ListView myList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            // Set our view from the "main" layout resource
            myList = FindViewById<ListView>(Resource.Id.listText);

            alarms = new List<Alarm>();
            alarms.Add(new Alarm("7:00", "10:00", "L"));
            alarms.Add(new Alarm("5:00", "8:00", "J"));

            MyOwnAdapter adapter = new MyOwnAdapter(this, alarms);
            myList.Adapter = adapter;
        }


    }

}


using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;

namespace Hackatown_2018
{
    [Activity(Label = "Hackatown_2018", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private List<Alarm> alarms = new List<Alarm>() { };
        private ListView myList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            // Set our view from the "main" layout resource
            myList = FindViewById<ListView>(Resource.Id.listText);


            MyOwnAdapter adapter = new MyOwnAdapter(this, alarms);
            myList.Adapter = adapter;

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
                string Alarm = data.GetStringExtra("alarm");
            }
        }

    }

}


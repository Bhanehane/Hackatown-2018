using Android.App;
using Android.Widget;
using Android.OS;
using System.Net;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Gms.Location;
using System.IO;
using System.Globalization;
using Android.Content;
using Android.Views.InputMethods;




namespace Hackatown_2018
{
    [Activity(Label = "Hackatown_2018", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            Button butt = FindViewById<Button>(Resource.Id.omaewa);


            butt.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(Maps));

                StartActivityForResult(intent, 1);
            };



        }




    }
}


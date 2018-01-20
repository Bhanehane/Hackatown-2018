using Android.App;
using Android.Widget;
using Android.OS;

namespace Hackatown_2018
{
    [Activity(Label = "Hackatown_2018", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            // Set our view from the "main" layout resource

        }


    }
}


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

namespace Hackatown_2018
{
    [Activity(Label = "ActivityNewItem", MainLauncher = false)]
    public class ActivityNewItem : Activity
    {
        DialogDate dialogDate;
        DialogTime dialogTime;
        DialogTime dialogTimeDesired;
        Button btnDate;
        Button btnTime;
        Button btnTimeDesired;
        Button btnInitPos;
        Button btnDest;
        Button btnConfirm;
        double[] positionsI;
        double[] positionsF;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewItemLayout);

            btnDate = FindViewById<Button>(Resource.Id.btnDate);
            btnTime = FindViewById<Button>(Resource.Id.btnTime);
            btnTimeDesired = FindViewById<Button>(Resource.Id.btnTimeDesired);
            btnInitPos = FindViewById<Button>(Resource.Id.btnPosActuelle);
            btnDest = FindViewById<Button>(Resource.Id.btnDest);
            btnConfirm = FindViewById<Button>(Resource.Id.btnConfirmerNew);

            dialogDate = new DialogDate(btnDate);
            dialogTime = new DialogTime(btnTime);
            dialogTimeDesired = new DialogTime(btnTimeDesired);
            // Create your application here

            btnDate.Click += (e, o) =>
            {
                FragmentTransaction fragTrans = FragmentManager.BeginTransaction();
                dialogDate.Show(fragTrans, "dialog fragment");
                TryActivateConfirm();
            };
            btnTime.Click += (e, o) =>
            {
                FragmentTransaction fragTrans = FragmentManager.BeginTransaction();
                dialogTime.Show(fragTrans, "dialog fragment");
                TryActivateConfirm();
            };
            btnTimeDesired.Click += (e, o) =>
            {
                FragmentTransaction fragTrans = FragmentManager.BeginTransaction();
                dialogTimeDesired.Show(fragTrans, "dialog fragment");
                TryActivateConfirm();
            };
            btnInitPos.Click += (e, o) =>
            {
                var intent = new Intent(this, typeof(Maps));
                StartActivityForResult(intent, 1);
                TryActivateConfirm();
            };
            btnDest.Click += (e, o) =>
            {
                var intent = new Intent(this, typeof(Maps));
                StartActivityForResult(intent, 2);
                TryActivateConfirm();
            };
            btnConfirm.Click+=(e,o)=>
            {
                int[] alarmT = new int[5] { dialogDate.Time.Year, dialogDate.Time.Month, dialogDate.Time.Day, dialogTime.Time.Hour, dialogTime.Time.Minute };
                Intent.PutExtra("alarmTime", alarmT);
                int [] prepT = new int[2] { dialogTimeDesired.Time.Hour, dialogTimeDesired.Time.Minute};
                Intent.PutExtra("prepTime", prepT);
                Intent.PutExtra("positionI", positionsI);
                Intent.PutExtra("positionF", positionsF);


                SetResult(Result.Ok, Intent);
                Finish();
            };
        }
        void TryActivateConfirm()
        {
            if(btnTime.Text != "Sélectionner l'heure d'arrivée" && 
                btnDate.Text != "Sélectionner une date" && 
                btnTimeDesired.Text != "Sélectionner le temps de préparation" && 
                btnInitPos.Text != "Sélectionner votre position actuelle" &&
                btnDest.Text != "Sélectionner la destination")
            {
                btnConfirm.Enabled = true;
            }
        }
        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            if (btnConfirm != null)
            {
                TryActivateConfirm();
            }
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                if(requestCode == 1)
                {
                    positionsI = data.GetDoubleArrayExtra("Coordonnées");
                    btnInitPos.Text = data.GetStringExtra("Adresse");
                }
                else
                {
                    positionsF = data.GetDoubleArrayExtra("Coordonnées");
                    btnDest.Text = data.GetStringExtra("Adresse");
                }
                
            }
        }
    }
}
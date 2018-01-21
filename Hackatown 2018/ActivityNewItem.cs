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
        //Maps InitPosAct;
        //Maps DestAct;
        Button btnDate;
        Button btnTime;
        Button btnTimeDesired;
        Button btnInitPos;
        Button btnDest;
        Button btnConfirm;

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
            //InitPosAct = new Maps(btnInitPos);
            //DestAct = new Maps(btnDest);
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
                //var intent = new Intent(this, typeof(Maps));
                //StartActivity(intent);
                TryActivateConfirm();
            };
            btnDest.Click += (e, o) =>
            {
                //var intent = new Intent(this, typeof(Maps));
                //StartActivity(intent);
                TryActivateConfirm();
            };
            btnConfirm.Click+=(e,o)=>
            {
                //Mauvais constructeur
                string alarmT = dialogDate.Time.Year.ToString() + ';' + dialogDate.Time.Month.ToString() + ';' + dialogDate.Time.Day.ToString() + ';' 
                               + dialogTime.Time.Hour.ToString()+';'+dialogTime.Time.Minute.ToString();
                Intent.PutExtra("alarm", alarmT);
                //int[] prép = new int[2] { 0, 0 };
                //Intent.PutExtra("Prep", prép);
                //double[] pos = new double[4] { 0, 0, 0, 0 };
                //Intent.PutExtra("Pos", pos);
                SetResult(Result.Ok, Intent);
                Finish();
            };
        }
        void TryActivateConfirm()
        {
            if(btnTime.Text != "Sélectionner l'heure d'arrivée" && 
                btnDate.Text != "Sélectionner une date" && 
                btnTimeDesired.Text != "Sélectionner le temps de préparation" //&& 
               // btnInitPos.Text != "Sélectionner votre position actuelle" && 
               /* btnDest.Text != "Sélectionner la destination"*/)
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
    }
}
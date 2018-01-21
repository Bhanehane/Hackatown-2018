using System;
using System.Collections;
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
        //DialogTime dialogTimeDesired;
        Button btnDate;
        Button btnTime;
        //Button btnTimeDesired;
        Button btnInitPos;
        Button btnDest;
        Button btnConfirm;
        double[] positionsI;
        double[] positionsF;
        Spinner SpHour;
        Spinner SpMin;
        ArrayAdapter AdapterH;
        ArrayAdapter AdapterM;
        string[] ArrayH;
        string[] ArrayM;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewItemLayout);

            btnDate = FindViewById<Button>(Resource.Id.btnDate);
            btnTime = FindViewById<Button>(Resource.Id.btnTime);
            //btnTimeDesired = FindViewById<Button>(Resource.Id.btnTimeDesired);
            btnInitPos = FindViewById<Button>(Resource.Id.btnPosActuelle);
            btnDest = FindViewById<Button>(Resource.Id.btnDest);
            btnConfirm = FindViewById<Button>(Resource.Id.btnConfirmerNew);

            SpHour = FindViewById<Spinner>(Resource.Id.spHour);
            SpMin = FindViewById<Spinner>(Resource.Id.spMinute);

            dialogDate = new DialogDate(btnDate);
            dialogTime = new DialogTime(btnTime);
            //dialogTimeDesired = new DialogTime(btnTimeDesired);
            // Create your application here

            ArrayH = new string[24];
            for(int i = 0; i<24;i++)
            {
                ArrayH[i] = i.ToString("D2");
            }
            ArrayM = new string[60];
            for(int i = 0; i< 60;i++)
            {
                ArrayM[i] = i.ToString("D2");
            }
            AdapterH = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, ArrayH);
            AdapterM = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, ArrayM);
            SpHour.Adapter = AdapterH;
            SpMin.Adapter = AdapterM;


            btnDate.Click += (e, o) =>
            {
                FragmentTransaction fragTrans = FragmentManager.BeginTransaction();
                dialogDate.Show(fragTrans, "dialog fragment");
            };
            btnTime.Click += (e, o) =>
            {
                FragmentTransaction fragTrans = FragmentManager.BeginTransaction();
                dialogTime.Show(fragTrans, "dialog fragment");
            };
            //btnTimeDesired.Click += (e, o) =>
            //{
            //    FragmentTransaction fragTrans = FragmentManager.BeginTransaction();
            //    dialogTimeDesired.Show(fragTrans, "dialog fragment");
            //};
            btnInitPos.Click += (e, o) =>
            {
                var intent = new Intent(this, typeof(Maps));
                StartActivityForResult(intent, 1);
            };
            btnDest.Click += (e, o) =>
            {
                var intent = new Intent(this, typeof(Maps));
                StartActivityForResult(intent, 2);
            };
            btnConfirm.Click+=(e,o)=>
            {
                int[] alarmT = new int[5] { dialogDate.Time.Year, dialogDate.Time.Month, dialogDate.Time.Day, dialogTime.Time.Hour, dialogTime.Time.Minute };
                Intent.PutExtra("alarmTime", alarmT);
                int[] prepT = new int[2] { SpHour.SelectedItemPosition, SpMin.SelectedItemPosition };
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
                //btnTimeDesired.Text != "Sélectionner le temps de préparation" && 
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
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
    class DialogTime : DialogFragment
    {
        Button BtnTime;
        DateTime time;
        public DateTime Time
        {
            get
            {
                return time;
            }
            private set
            {
                time = value;
            }
        }
        TimePicker timePicker;

        public DialogTime(Button button)
        {
            BtnTime = button;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.LayoutPickerTime, container, false);
            Time = DateTime.Now;
            timePicker = view.FindViewById<TimePicker>(Resource.Id.timePicker1);

            view.FindViewById<Button>(Resource.Id.btnTimeConfirm).Click += (e, o) =>
            {
                Dismiss();
                Time = new DateTime(1, 1, 1, timePicker.Hour, timePicker.Minute, 0);//the date is unimportant
                BtnTime.Text = string.Format("{0:t}", Time);
            };


            return view;
        }
    }
}
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
    class DialogDate : DialogFragment
    {
        Button BtnDate;
        DatePicker datePicker;
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
        public DialogDate(Button button)
            :base()
        {
            BtnDate = button;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            Time = DateTime.Now;
            View view = inflater.Inflate(Resource.Layout.LayoutPicketDate, container, false);
            datePicker = view.FindViewById<DatePicker>(Resource.Id.datePicker1);
            view.FindViewById<Button>(Resource.Id.btnDateConfirm).Click += (e, o) =>
            {
                Dismiss();
                Time = datePicker.DateTime;
                BtnDate.Text = String.Format("{0:dd/MM/yy}", Time);
            };





            return view;
            
        }
    }
}
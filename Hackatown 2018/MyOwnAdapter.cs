﻿using System;
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
    public class MyOwnAdapter : BaseAdapter<Alarm>
    {
        private List<Alarm> Items;
        private Context TheContext;

        public MyOwnAdapter(Context context, List<Alarm> items)
        {
            Items = items;
            TheContext = context;
        }


        public override Alarm this[int position]
        {
            get
            {
                return Items[position];
            }
        }

        public override int Count
        {
            get
            {
                return Items.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(TheContext).Inflate(Resource.Layout.AlarmLayout, null, false);
            }
            TextView alarmTime = row.FindViewById<TextView>(Resource.Id.alarmTime);
            TextView arrivalTime = row.FindViewById<TextView>(Resource.Id.arrivalTime);
            TextView dayOfWeek = row.FindViewById<TextView>(Resource.Id.dayOfWeek);

            
            alarmTime.Text = string.Format("{0:t}",Items[position].AlarmTime);
            arrivalTime.Text = string.Format("{0:t}", Items[position].DesiredTimeArrival);
            dayOfWeek.Text = Items[position].DesiredTimeArrival.DayOfWeek.ToString()[0].ToString() + Items[position].DesiredTimeArrival.DayOfWeek.ToString()[1].ToString();
            ChangeDayOfTheWeek(dayOfWeek);

            return row;
        }
        public void AddItem(Alarm item)
        {
            Items.Add(item);
        }
        public void RemoveItem(int position)
        {
            Items[position].CancelAlarm();
            Items.RemoveAt(position);
        }
        public void ChangeDayOfTheWeek(TextView txt)
        {
            if(txt.Text == "Mo")
            {
                txt.Text = "L";
            }
            if (txt.Text == "Tu")
            {
                txt.Text = "Ma";
            }
            if (txt.Text == "We")
            {
                txt.Text = "Me";
            }
            if (txt.Text == "Th")
            {
                txt.Text = "J";
            }
            if (txt.Text == "Fr")
            {
                txt.Text = "V";
            }
            if (txt.Text == "Sa")
            {
                txt.Text = "S";
            }
            if (txt.Text == "Su")
            {
                txt.Text = "D";
            }
        }

    }
}
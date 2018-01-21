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

            alarmTime.Text = string.Format("{0:HH:mm}", Items[position].AlarmTime);
            //arrivalTime.Text = Items[position].DesiredTimeArrival;
            //dayOfWeek.Text = Items[position].DayOfWeek;
            return row;
        }
        public void AddItem(Alarm item)
        {
            Items.Add(item);
        }
    }
}
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
    class EditDialog : DialogFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.Edit, container, false);

            view.FindViewById<Button>(Resource.Id.btnErase).Click += (e, o) =>
            {

            };
            view.FindViewById<Button>(Resource.Id.btnEdit).Click += (e, o) =>
            {
                Dismiss();
            };

            return View;
        }
    }
}
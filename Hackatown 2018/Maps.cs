using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Gms.Maps.Model;
using Android.Gms.Maps;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Android.Views.InputMethods;
using System.Globalization;

namespace Hackatown_2018
{
    [Activity(Label = "Maps")]
    public class Maps : Activity, IOnMapReadyCallback
    {
        const int radius = 50;
        Button Button1 { get; set; }
        Button Button2 { get; set; }
        AutoCompleteTextView EditText { get; set; }
        private GoogleMap gmap;
        private LatLng position;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.LayoutMap);
            position = new LatLng(45.50438399999999, -73.61288289999999);

            // Create your application here
            SetUpMap();
        }

        private void SetUpMap()
        {
            if (gmap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
                Button1 = FindViewById<Button>(Resource.Id.button1);
                Button2 = FindViewById<Button>(Resource.Id.button2);
                EditText = FindViewById<AutoCompleteTextView>(Resource.Id.text1);


            }
        }
        public void OnMapReady(GoogleMap googlemap)
        {

            gmap = googlemap;
            LatLng latLng = new LatLng(45.50438399999999, -73.61288289999999);
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latLng, 15);
            gmap.MoveCamera(camera);

            MarkerOptions options = new MarkerOptions()
            .SetPosition(latLng);
            gmap.AddMarker(options);


            Button1.Click += Button1_Click;
            Button2.Click += Button2_Click;
            EditText.TextChanged += EditText_TextChanged;
            int temps = GetTimeFromAPI(45.636030,-73.833548,45.642220,-73.842470);

        }

        private void EditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var possibleLocations = LocationAutocomplete(EditText.Text, position, radius);
            EditText.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, possibleLocations);
        }

       

        private void Button1_Click(object sender, EventArgs e)
        {
            

            double[] latLng = GeoCode(EditText.Text);
            position = new LatLng(latLng[0], latLng[1]);
            gmap.Clear();
            MarkerOptions options = new MarkerOptions()
            .SetPosition(position);
            gmap.AddMarker(options);
            gmap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(position, 15));
            DismissKeyboard();

        }
        private void Button2_Click(object sender, EventArgs e)
        {
            double[] latLng = GeoCode(EditText.Text);
            Intent.PutExtra("Coordonnées", latLng);
            Intent.PutExtra("Adresse", EditText.Text);
            SetResult(Result.Ok, Intent);
            Finish();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringBrowsed"></param>
        /// <param name="location">Central point of search</param>
        /// <param name="radius">radius of search in kilometers</param>
        /// <returns></returns>
        private string[] LocationAutocomplete(string stringBrowsed, LatLng location, int radius)
        {
            string[] locationsCompleted;
            string html = string.Empty;
            double[] pos = new double[2] { location.Latitude, location.Longitude };
            string url = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=" + stringBrowsed +
                "&location=" + pos[0].ToString("G",new CultureInfo("en-US")) + ',' + pos[1].ToString("G",new CultureInfo("en-US")) + "&radius=" + radius + "000&strictbounds" + "&key=AIzaSyCSu8kH3Rmg-ruB4bccMBrGXDhTuMb3yiI";


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            dynamic jsonResponse = JsonConvert.DeserializeObject(html);
            locationsCompleted = new string[jsonResponse.predictions.Count];

            for (int i = 0; i < jsonResponse.predictions.Count; i++)
            {
                locationsCompleted[i] = jsonResponse.predictions[i].description;
            }


            return locationsCompleted;

        }

        private int GetTimeFromAPI(double lat1, double long1, double lat2, double long2)
        {
            string html = string.Empty;
            string[] sortie1;
            int resultat = 0;
            string url = @"https://maps.googleapis.com/maps/api/directions/json?origin=" +
                lat1.ToString("G",new CultureInfo("en-US")) + ',' + long1.ToString("G",new CultureInfo("en-US")) + 
                "&destination=" + lat2.ToString("G",new CultureInfo("en-US")) + ',' + long2.ToString("G",new CultureInfo("en-US")) + "&key=AIzaSyCevyyL4bEYdWVoaH9fCecYk9JEfQtblo8";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            dynamic jsonResponse = JsonConvert.DeserializeObject(html);
            string txt = jsonResponse.routes[0].legs[0].duration.text;
            sortie1 = txt.Split(' ');
            if (sortie1[1][0] == 'h')
            {
                resultat = int.Parse(sortie1[0]) * 60;
            }
            else
            {
                resultat = int.Parse(sortie1[0]);
            }
            return resultat;

        }
        /// <summary>
        /// Find the latitude and the longitude of destination by his address
        /// </summary>
        /// <param name="adresse">String representing the adress of the destination Ex:43 rue dolbeau, Blainville,...</param>
        /// <returns>Array of double the first value is the latitude and the second the longitude</returns>
        private double[] GeoCode(string adresse)
        {
            try
            {
                string html = string.Empty;
                string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + adresse + "&key=AIzaSyDgnnWsHI6g0-ZTApRmnJfpoU7YXL2Cur4";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
                dynamic jsonResponse = JsonConvert.DeserializeObject(html);
                double lat = jsonResponse.results[0].geometry.location.lat;
                double longi = jsonResponse.results[0].geometry.location.lng;

                double[] returnValue = new double[2] { lat, longi };
                return returnValue;
            }
            catch
            {
                return new double[2] { 45.50438399999999, -73.61288289999999 };
            }
        }
        private void DismissKeyboard()
        {
            var view = CurrentFocus;
            if (view != null)
            {
                var imm = (InputMethodManager)GetSystemService(InputMethodService);
                imm.HideSoftInputFromWindow(view.WindowToken, 0);
            }
        }
    }
}
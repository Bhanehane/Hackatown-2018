﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Media;
using System.IO;


namespace Hackatown_2018
{
    public class Alarm
    {
        public Context Context { get; private set; }
        private AlarmManager Manager { get; set; }
        private Intent CurrentIntent { get; set; }
        private PendingIntent PendingIntent { get; set; }
        public DateTime DesiredTimeArrival { get; set; }
        public DateTime DepartureTime { get; set; }
        public TimeSpan PreparationTime { get; set; }
        public TimeSpan TravelTime { get; set; }
        public DateTime AlarmTime { get; private set; }
        public double[] Position { get; private set; }
        public double[] Destination { get; private set; }
        public bool DepartureTimeIsInThePast { get; set; }
        public bool IsActivated { get; set; }

        public Alarm(Context context, TimeSpan preparationTime, DateTime desiredTimeArrival, double[] position, double[] destination)
        {
            DepartureTimeIsInThePast = false;
            Context = context;
            Manager = Context.GetSystemService(Context.AlarmService) as AlarmManager;
            DesiredTimeArrival = desiredTimeArrival;
            PreparationTime = preparationTime;
            Position = position;
            Destination = destination;
            CalculateTravelTimeWithTraffic();
            StartAlarm();
        }

        public void StartAlarm()
        {
            if (IsActivated)
            {
                CancelAlarm();
            }
            long interval = GetMilliSecFromNowTo(AlarmTime);
            CurrentIntent = new Intent(Context, typeof(AlarmReceiver));
            string message = "On se réveille";
            CurrentIntent.PutExtra("message", message);
            PendingIntent = PendingIntent.GetBroadcast(Context, 0, CurrentIntent, 0);

            Manager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + interval, PendingIntent);
            IsActivated = true;
        }

        public long GetMilliSecFromNowTo(DateTime time)
        {
            TimeSpan interval = time - DateTime.Now;
            return Convert.ToInt64(interval.TotalMilliseconds);
        }
        public void CalculateAlarmTime(TimeSpan preparationTime, DateTime departureTime)
        {
            AlarmTime = departureTime.Subtract(preparationTime);
        }


        public void CalculateTravelTimeWithTraffic()
        {
            int timeNeeded = GetTimeFromAPI(Position[0], Position[1], Destination[0], Destination[1]);
            DateTime date = DesiredTimeArrival.Subtract(new TimeSpan(0, timeNeeded, 0));
            int[] timeAndTraffic = GetTimeFromAPI(Position[0], Position[1], Destination[0], Destination[1],date);
            TimeSpan travel = new TimeSpan(0, timeAndTraffic[0] + timeAndTraffic[1], 0);

            while(travel > DesiredTimeArrival- date)
            {
                date = date.Subtract(new TimeSpan(0, 1, 0));
                timeAndTraffic = GetTimeFromAPI(Position[0], Position[1], Destination[0], Destination[1], date);
                travel = new TimeSpan(0, timeAndTraffic[0] + timeAndTraffic[1], 0);
            }

            DepartureTime = date;
            TravelTime = new TimeSpan(0, timeNeeded, 0);
            CalculateAlarmTime(PreparationTime, DepartureTime);
        }

        public long ConvertToTimestamp(DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000000;
            return epoch - 7200;
        }

        private int[] GetTimeFromAPI(double lat1, double long1, double lat2, double long2, DateTime date)
        {
            string txt ="";
            string txt2="";
            string html = string.Empty;
            string[] sortie1;
            string[] sortie2;
            int[] resultat = new int[2];
            long timeStamp = ConvertToTimestamp(date);
            string url = @"https://maps.googleapis.com/maps/api/directions/json?origin=" + 
                lat1.ToString("G",new CultureInfo("en-US")) + ',' + long1.ToString("G", new CultureInfo("en-US")) +
                "&destination=" + lat2.ToString("G", new CultureInfo("en-US")) + ',' + long2.ToString("G", new CultureInfo("en-US"))
                + "&departure_time=" + timeStamp.ToString() + "&key=AIzaSyDwgUfhlc26aVBOm2NZUAA5vQHixHIvyp0";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (System.IO.Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            dynamic jsonResponse = JsonConvert.DeserializeObject(html);
            try
            {
                txt = jsonResponse.routes[0].legs[0].duration.text;
                txt2 = jsonResponse.routes[0].legs[0].duration_in_traffic.text;
                
            }
            catch
            {
                DepartureTimeIsInThePast = true;
            }

            if (!DepartureTimeIsInThePast)
            {
                sortie1 = txt.Split(' ');
                sortie2 = txt2.Split(' ');
                if (sortie1[1][0] == 'h')
                {
                    resultat[0] = int.Parse(sortie1[0]) * 60;
                }
                else
                {
                    resultat[0] = int.Parse(sortie1[0]);
                }
                if (sortie2[1][0] == 'h')
                {
                    resultat[1] = int.Parse(sortie2[0]) * 60;
                }
                else
                {
                    resultat[1] = int.Parse(sortie2[0]);
                }
            }

            return resultat;
        }

        private int GetTimeFromAPI(double lat1, double long1, double lat2, double long2)
        {
            string html = string.Empty;
            string[] sortie1;
            int resultat = 0;
            string url = @"https://maps.googleapis.com/maps/api/directions/json?origin=" +
                lat1.ToString("G", new CultureInfo("en-US")) + ',' + long1.ToString("G", new CultureInfo("en-US")) +
                "&destination=" + lat2.ToString("G", new CultureInfo("en-US")) + ',' + long2.ToString("G", new CultureInfo("en-US")) + "&key=AIzaSyCevyyL4bEYdWVoaH9fCecYk9JEfQtblo8";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (System.IO.Stream stream = response.GetResponseStream())
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
        public void CancelAlarm()
        {
            Manager.Cancel(PendingIntent);
            IsActivated = false;
        }
    }
}
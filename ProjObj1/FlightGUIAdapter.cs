using Avalonia.Rendering.Composition;
using Mapsui.Extensions;
using Mapsui.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace PROJOBJ1
{
    public class FlightGUIAdapter : FlightGUI
    {
        public FlightGUIAdapter(Flight flight, Airport origin,Airport target)
        {
           
            WorldPosition position = GetInterpolatedPosition(flight,origin,target);
            //if (flight.Longitude == null || flight.Latitude==null)
            //{
                //flight.Longitude = (float) position.Longitude;
                //.Latitude = (float) position.Latitude;
            //}
            
            double angle = GetRotationAngle((double) flight.Longitude,(double) flight.Latitude, position.Longitude, position.Latitude);
            flight.Latitude =(float) position.Latitude;
            flight.Longitude =(float) position.Longitude;
            ID = flight.ID;
            WorldPosition = position;
            MapCoordRotation = angle;
        }
        private WorldPosition GetInterpolatedPosition(Flight flight, Airport originAirport, Airport targetAirport)
        {
            DateTime cur = DateTime.Now;
            DateTime lTime = ParseStringTimeDate(flight.LandingTime);
            DateTime TTime = ParseStringTimeDate(flight.TakeoffTime);
            if (lTime<TTime)
            {
                lTime = lTime.AddDays(1);
            }
            if (!(lTime > cur && TTime < cur))
            {
                return new WorldPosition(originAirport.Latitude, originAirport.Longitude);
            }

            double LatSpeed, LonSpeed;
            double seconds = (cur - TTime).TotalSeconds;
            if (flight.Latitude == null || flight.Longitude == null)
            {
                (LatSpeed, LonSpeed) = GetSpeed(flight, originAirport, targetAirport);
                flight.Latitude = originAirport.Latitude + (float) (seconds * LatSpeed);
                flight.Longitude = originAirport.Latitude + (float)(seconds * LonSpeed);
                return new WorldPosition(originAirport.Latitude + seconds * LatSpeed,
                    originAirport.Longitude + seconds * LonSpeed);
            }
            (LatSpeed, LonSpeed) = GetSpeed(flight,targetAirport);
            flight.Latitude += (float) LatSpeed;
            flight.Longitude += (float) LonSpeed;
            return new WorldPosition(flight.Latitude!.Value, flight.Longitude!.Value);
            return new WorldPosition(flight.Latitude!.Value+LatSpeed,flight.Longitude!.Value+LonSpeed);
        }
        private (double, double) GetSpeed(Flight flight,Airport originAirport, Airport targetAirport)
        {
            double seconds = CalculateSeconds(flight.TakeoffTime, flight.LandingTime);
            double LaSpeed = (targetAirport.Latitude -originAirport.Latitude) / seconds;
            double LoSpeed = (targetAirport.Longitude - originAirport.Longitude) / seconds;
            return (LaSpeed, LoSpeed);
        }

        private (double, double) GetSpeed(Flight flight, Airport targetAirport)
        {
            double seconds = CalculateSeconds(flight.TakeoffTime, flight.LandingTime);
            double Lat = flight.Latitude!.Value;
            double Lon = flight.Longitude!.Value;
            double LaSpeed = (targetAirport.Latitude - Lat) / seconds;
            double LoSpeed = (targetAirport.Longitude - Lon) / seconds;
            return (LaSpeed, LoSpeed);
        }
        public static double CalculateSeconds(string startTime, string endTime)
        {
            DateTime sTime = ParseStringTimeDate(startTime);
            DateTime eTime = ParseStringTimeDate(endTime);
            if (eTime < sTime)
                eTime = eTime.AddDays(1);
            TimeSpan timeDifference = eTime - sTime;
            return timeDifference.TotalSeconds;
        }
        public static DateTime ParseStringTimeDate(string time)
        {
            string[] h = time.Split(':');
            int hour = int.Parse(h[0]);
            int min = int.Parse(h[1]);
            return DateTime.Today.AddHours(hour).AddMinutes(min);
        }
        public static double GetRotationAngle(double startLon, double startLat, double endLon, double endLat)
        {
            (double x_s, double y_s) = SphericalMercator.FromLonLat(startLon, startLat);
            (double x_f, double y_f) = SphericalMercator.FromLonLat(endLon, endLat);
            double angle = Math.PI / 2 - Math.Atan2(y_f - y_s, x_f - x_s);
            return angle;
        }
    }
}

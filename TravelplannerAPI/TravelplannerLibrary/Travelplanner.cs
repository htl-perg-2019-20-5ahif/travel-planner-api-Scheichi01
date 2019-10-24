using System;
using System.Linq;

namespace TravelplannerLibrary
{
    public class Travelplanner
    {
        private readonly Schedule[] schedules;

        public Travelplanner(Schedule[] schedules)
        {
            this.schedules = schedules;
        }

        public Route FindConnection(string from, string to, string start)
        {
            if (from.Equals("Linz") && to.Equals("Linz")) { return null; }
            if (from.Equals("Linz")) { return FindConnectionFromLinz(to, start); }
            if (to.Equals("Linz")) { return FindConnectionToLinz(from, start); }
            Route depRoute = FindConnectionToLinz(from, start);
            Route arrRoute = FindConnectionFromLinz(to, depRoute.ArrivalTime);
            if (depRoute == null || arrRoute == null) return null;
            return new Route
            {
                Depart = depRoute.Depart,
                DepartureTime = depRoute.DepartureTime,
                Arrive = arrRoute.Arrive,
                ArrivalTime = arrRoute.ArrivalTime
            };
        }

        public Route FindConnectionFromLinz(string to, string start)
        {
            if (to == null || start == null) return null;
            var schedule = schedules.FirstOrDefault(s => s.City.Equals(to));
            var toCity = schedule.FromLinz.FirstOrDefault(f => f.Leave.CompareTo(start) >= 0);
            if (toCity == null) { return null; }
            return new Route
            {
                Arrive = to,
                ArrivalTime = toCity.Arrive,
                Depart = "Linz",
                DepartureTime = toCity.Leave
            };
        }

        public Route FindConnectionToLinz(string from, string start)
        {
            if (from == null || start == null) return null;
            var schedule = schedules.FirstOrDefault(s => s.City.Equals(from));
            var fromCity = schedule.ToLinz.FirstOrDefault(f => f.Leave.CompareTo(start) >= 0);
            if (fromCity == null) { return null; }
            return new Route
            {
                Arrive = "Linz",
                ArrivalTime = fromCity.Arrive,
                Depart = from,
                DepartureTime = fromCity.Leave
            };
        }

        public Time FindConnection(string destination, string start)
        {
            if (destination == null || start == null) return null;
            var schedule = schedules.FirstOrDefault(s => s.City.Equals(destination));
            var Time = schedule.ToLinz.FirstOrDefault(f => f.Leave.CompareTo(start) >= 0);
            return Time;
        }
    }
}

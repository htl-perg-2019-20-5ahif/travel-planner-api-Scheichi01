using System;
using System.Collections.Generic;
using System.Text;

namespace TravelplannerLibrary
{
    public class Time
    {
        public string Leave { get; set; }
        public string Arrive { get; set; }
    }

    public class Schedule
    {
        public string City { get; set; }
        public List<Time> ToLinz { get; set; }
        public List<Time> FromLinz { get; set; }
    }

    public class Route
    {
        public string Depart { get; set; }
        public string DepartureTime { get; set; }
        public string Arrive { get; set; }
        public string ArrivalTime { get; set; }
    }
}

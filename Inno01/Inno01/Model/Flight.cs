﻿using Inno01.Helpers;
using System.Collections.Generic;
using System.Linq;


namespace Inno01.Model
{
    public class Flight
    {
        private static List<Flight> Flights = new List<Flight>();

        public string Id { get; set; }
        public City Origin { get; set; }
        public City Destination { get; set; }
        public int TimeIntervale { get; set; }
        public int Distance { get; set; }
        public Airline Airline { get; set; }


        public static void AddFlight(Flight f)
        {
            if (Flights == null)
            {
                Flights = new List<Flight>();
            }

            Flights.Add(f);
        }

        public static List<Flight> GetFlights()
        {
            if(Flights != null) { 
                return Flights.ToList();
            }
            return new List<Flight>();
        }

        public ICollection<Flight> GetFlightsByCities(City origin, City dest)
        {
            var result = Flights.Where(c => 
            (c.Origin == origin || origin == null)
            && (c.Destination == dest || dest == null)).ToArray();
            return result;
        }

        public override string ToString()
        {
            
            return Origin.Name +" -> "+ Destination.Name+" ("+ Distance+" km) "+TimeHelper.ConvertToHM(TimeIntervale);
        }
    }
}

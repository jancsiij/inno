
using System.Collections.Generic;

using System.Linq;


namespace Inno01.Model
{
    public class Airline
    {
  
        private static List<Airline> Airlines;
  
        public int Id { get; set; }


        public string Name { get; set; }

        public List<Flight> Flights { get; set; }

        static Airline()
        {
            Airlines = new List<Airline>();
        }


        public static void AddAirline(Airline a)
        {
            Airlines.Add(a);
        }

        public void AddFlight(Flight f)
        {
            if(Flights == null)
            {
                Flights = new List<Flight>();
            }
            Flights.Add(f);
        }


        public List<Flight> GetFlights()
        {

            return Flights;
        }

        public static ICollection<Airline> GetAirlines()
        {
            if (Airlines != null)
            {
                return Airlines.ToList();
            }
            return new List<Airline>();
        }


    }
}

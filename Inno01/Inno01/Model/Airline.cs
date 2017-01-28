using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inno01.Model
{
    class Airline
    {
        private static ICollection<Airline> Airlines;

        public string Name { get; set; }
        //public int Id { get; set; }
        private ICollection<Flight> Flights { get; set; }

        public Airline(string name)
        {
            Name = name;
        }

        public static void AddAirline(Airline a)
        {
            Airlines.Add(a);
        }

        public static ICollection<Flight> GetAllFlights()
        {
            List<Flight> result = new List<Flight>();
            foreach (Airline a in Airlines)
            {
                result.AddRange(a.GetFlights());
            }
            return result.ToArray();
        }

        public  ICollection<Flight> GetFlights()
        {
            return Flights.ToArray();
        }

        public static ICollection<Airline> GetAirlines()
        {
            return Airlines.ToArray();
        }
    }
}

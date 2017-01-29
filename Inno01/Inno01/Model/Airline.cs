using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inno01.Model
{
    public class Airline
    {
        [NotMapped]
        private static List<Airline> Airlines;
        [Key]
        public int Id { get; set; }


        public string Name { get; set; }

        public ICollection<Flight> Flights { get; set; }

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


        public ICollection<Flight> GetFlights()
        {
            return Flights.ToList();
        }

        public static ICollection<Airline> GetAirlines()
        {
            if (Airlines != null)
            {
                return Airlines.ToList();
            }
            return new List<Airline>();
        }

        //public static ICollection<Flight> GetAllFlights()
        //{
        //    List<Flight> result = new List<Flight>();
        //    foreach (Airline a in Airlines)
        //    {
        //        result.AddRange(a.GetFlights());
        //    }
        //    return result.ToArray();
        //}
    }
}

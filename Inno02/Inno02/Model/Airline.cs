using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inno01.Model
{
    class Airline
    {

        public int Id { get; set; }
        public string Name { get; set; }
        ICollection<Flight> Flights { get; set; }

        static Airline()
        {
        }

        public Airline(string name)
        {
            Name = name;
            Flights = new List<Flight>();
        }

 

        public void AddFlight(Flight f)
        {
            Flights.Add(f);
        }



    }
}

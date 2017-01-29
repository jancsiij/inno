using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inno01.Model
{
    class Flight
    {

        public int Id { get; set; }
        public City From { get; set; }
        public City To { get; set; }
        public int TimeIntervale { get; set; }
        public float Distance { get; set; }

        public Flight(int id, City from, City to, int time, float distance)
        {

        }



        public ICollection<Flight> GetFlightsByCities(City from, City to)
        {
            var result = Flights.Where(c => 
            (c.From == from || from == null)
            && (c.To == to || to == null)).ToArray();
            return result;
        }
    }
}

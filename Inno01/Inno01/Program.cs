

using Inno01.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Inno01
{
    class Program
    {

        static void Main(string[] args)
        {
            new DoStuff().Start();
        }
    }

    class DoStuff
    {
        private List<City> _cities;
        private List<Airline> _airlines;
        //private List<Flight> _cities;


        public void Start()
        {
            ReadData();

            City lowest = _cities.OrderBy(c => c.Population).First();
            City hightest = _cities.OrderByDescending(c => c.Population).First();



            Console.ReadLine();
        }

        private void ReadData()
        {
            var path = Directory.GetCurrentDirectory().Replace("bin\\Debug", string.Empty);


            XDocument doc = XDocument.Load(path + @"Data\flight.xml");

            var xCities = doc.Root.Element("cities");
            _cities = ParseCities(xCities);


            var xAirlines = doc.Root.Element("airlines");
            _airlines = ParseAirlines(xAirlines);

            var xFlights = doc.Root.Element("flights");
            ParseFlights(xFlights);
        }

        private void ParseFlights(XElement xFlights)
        {
            var flights = xFlights.Elements("flight").Select(f =>
                new Flight
                {
                    Distance = (int.Parse(f.Element("distance").Value.ToString())),
                    //Id = (int.Parse(f.Element("id").Value.ToString())),
                    TimeIntervale = (int.Parse(f.Element("time").Value.ToString())),
                   Origin = _cities.SingleOrDefault(c => c.Id == f.Element("origin").Value),
                        Destination = _cities.SingleOrDefault(c => c.Id == f.Element("destination").Value),
                    Airline = _airlines.SingleOrDefault(a => a.Id == int.Parse(f.Element("airlineId").Value.ToString()))
                }).ToList();

            foreach (Flight f in flights)
            {
                Flight.AddFlight(f);

                if (f.Airline != null)
                {
                    var airline = _airlines.Single(a => a.Id == f.Airline.Id);
                    airline.AddFlight(f);
                }
            }

        }

        private List<Airline> ParseAirlines(XElement xAirlines)
        {

            var airlines = xAirlines.Elements("airline").Select(c =>
                          new Airline
                          {
                              Id = int.Parse(c.Element("id").Value),
                              Name = c.Element("name").Value.ToString()
                          }
                          )
                          .ToList();

            return airlines;
        }

        private List<City> ParseCities(XElement xCities)
        {

            var cities = xCities.Elements("city").Select(c =>
                new City
                {

                    Id = c.Element("id").Value.ToString(),
                    Name = c.Element("name").Value.ToString(),
                    Population = int.Parse(c.Element("population").Value)
                }).ToList();

            return cities;
        }
    
}
}

using Inno01.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Inno01
{
    public class DoStuff
    {
        private List<City> _cities;
        private List<Airline> _airlines;
        //private List<Flight> _cities;


        public void Start()
        {
            ReadData();

            if (_cities != null && _airlines != null)
            {
                City lowest = _cities.OrderBy(c => c.Population).First();
                City hightest = _cities.OrderByDescending(c => c.Population).First();

                Console.WriteLine("A legkissebb város: " + lowest);
                Console.WriteLine("A legnagyobb város: " + hightest);

                foreach (Airline item in _airlines)
                {
                    if (item.GetFlights() != null)
                    {
                        var graph = new Graph
                        {
                            Nodes = _cities,
                            Edges = item.GetFlights()
                        };

                        var dijkstra = new Dijkstra(graph);

                        var shortest = dijkstra.Shortestpath(lowest, hightest);

                        Console.WriteLine("\n" + item.Name + ": ");

                        ShowResult(shortest);
                    }
                   
                }

              

                Console.ReadLine();
            }
        }

        private void ShowResult(List<Flight> shortest)
        {
            if (shortest.Count() != 0)
            {
                foreach (Flight item in shortest)
                {
                    Console.WriteLine(item);
                }
            }
            else {
                Console.WriteLine("Nincs útvonal!");
            }

            
        }

        private void ReadData()
        {
            var path = Directory.GetCurrentDirectory().Replace("bin\\Debug", string.Empty);

            try
            {
                XDocument doc = XDocument.Load(path + @"Data\flight.xml");

                var xCities = doc.Root.Element("cities");
                _cities = ParseCities(xCities);


                var xAirlines = doc.Root.Element("airlines");
                _airlines = ParseAirlines(xAirlines);

                var xFlights = doc.Root.Element("flights");
                ParseFlights(xFlights);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("XLM datasource not found!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error occured while parsing XML data. File may contain incorrect data!");
            }


        }

        private void ParseFlights(XElement xFlights)
        {
            var flights = xFlights.Elements("flight").Select(f =>
                new Flight
                {
                    Distance = (int.Parse(f.Element("distance").Value.ToString())),
                    Id = f.Element("id").Value.ToString(),
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

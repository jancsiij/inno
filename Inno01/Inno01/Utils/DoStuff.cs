using Inno01.Helpers;
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
                        Console.WriteLine("\n" + item.Name + ": ");

                        Plan(graph, lowest, hightest, false);
                    }
                }

                var allGraph = new Graph { Edges = Flight.GetFlights(), Nodes = _cities };

                Console.WriteLine("\nBármely légitársasággal a legrövidebb út:");
                Plan(allGraph, lowest, hightest, true);

                Console.ReadLine();
            }
        }

        private void Plan(Graph g, City origin, City dest, bool detailed)
        {
            var dijkstra = new Dijkstra(g);

            var shortest = dijkstra.Shortestpath(origin, dest);


            ShowResult(shortest, detailed);
        }

        private void ShowResult(List<Flight> shortest, bool detailed)
        {
            int totalTime = 0;

            if (shortest != null)
            {
                foreach (Flight item in shortest)
                {
                    if (detailed)
                    {
                        Console.WriteLine(item.Airline.Name + " :" + item);
                    }
                    else
                    {
                        Console.WriteLine(item);
                    }

                    totalTime += item.TimeIntervale;
                    if (shortest.Last() != item)
                    {
                        var waitTime = 60 - item.TimeIntervale % 60;
                        totalTime += waitTime;
                        Console.WriteLine("Várazokás járatra: " + TimeHelper.ConvertToHM(waitTime));
                    }
                }
                Console.WriteLine("----------");
                Console.WriteLine("Összesen:" + TimeHelper.ConvertToHM(totalTime));
            }
            else
            {
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
                Console.WriteLine("XLM adatforrsá nem található");
            }
            catch (FormatException)
            {
                Console.WriteLine("Hiba törtönt beolvasás során. Helytelen adat!");
            }


        }

        private void ParseFlights(XElement xFlights)
        {
            var flights = xFlights.Elements("flight").Select(f =>
                new Flight
                {
                    Distance = (int.Parse(f.Element("distance").Value)),
                    Id = f.Element("id").Value.ToString(),
                    TimeIntervale = (int.Parse(f.Element("time").Value)),
                    Origin = _cities.SingleOrDefault(c => c.Id == f.Element("origin").Value),
                    Destination = _cities.SingleOrDefault(c => c.Id == f.Element("destination").Value),
                    Airline = _airlines.SingleOrDefault(a => a.Id == int.Parse(f.Element("airlineId").Value))
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
                              Name = c.Element("name").Value
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

                    Id = c.Element("id").Value,
                    Name = c.Element("name").Value,
                    Population = int.Parse(c.Element("population").Value)
                }).ToList();

            return cities;
        }

    }
}

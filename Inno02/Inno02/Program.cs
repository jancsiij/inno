

using Inno01.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Inno02
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Directory.GetCurrentDirectory().Replace("bin\\Debug",string.Empty);


            XDocument doc = XDocument.Load(path + @"Data\flight.xml");

            var xCities= doc.Root.Element("cities");
            ParseCities(xCities);


            var xAirlines = doc.Root.Element("airlines");
            ParseFlights(xAirlines);


 


            //var xmlDoc = new XmlDocument();
            //xmlDoc.Load(@"D:\Dokumentumok\git\szakdolg\project\Inno01\Inno01\Data\cities.xml");
            //var itemNodes = xmlDoc.SelectNodes("city");

            //List<City> cities = new List<City>();

            //foreach (XmlNode node in itemNodes)
            //{
            //    var city = new City {
            //        Id = node.SelectSingleNode("id").InnerText,
            //        Name = node.SelectSingleNode("Name").InnerText,
            //        Population = int.Parse(node.SelectSingleNode("population").InnerText)                    
            //    };
            //    cities.Add(city);

            //}




            //var faszom = from c in doc.Root.Elements("city")
            //             select 

            Console.ReadLine();
        }

        private static void ParseFlights(XElement xAirlines)
        {
            var air = xAirlines.Elements("airline");

            var airlines = xAirlines.Elements("airline").Select(c =>
                          new Airline(
                              c.Element("name").Value.ToString()
                          )
                          .ToList();

            var airlines = Airline.GetAirlines();
                   
        }

        private static void ParseCities(XElement xCities)
        {

            var cities=  xCities.Elements("city").Select(c =>
                new City
                {

                    Id = c.Element("id").Value.ToString(),
                    Name = c.Element("name").Value.ToString(),
                    Population = int.Parse(c.Element("population").Value)
                }).ToList();

            foreach (City item in cities)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}

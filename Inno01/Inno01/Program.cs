

using Inno01.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Inno01
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load(@"D:\Dokumentumok\git\szakdolg\project\Inno01\Inno01\Data\cities.xml");
            var cities = doc.Root.Elements("city").Select(c=> 
                          new City
                         {

                             Id = c.Element("id").Value.ToString(),
                             Name = c.Element("name").Value.ToString(),
                             Population = int.Parse(c.Element("population").Value)
                         }).ToList();

            

            doc.Root.Elements("airlines").Select(c =>
                          new 
                          {
                              Airline.
                              Name = c.Element("name").Value.ToString(),
                          }).ToList();


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

            foreach (City item in cities)
            {
                Console.WriteLine(item.Name);
            }


            //var faszom = from c in doc.Root.Elements("city")
            //             select 

            Console.ReadLine();
        }
    }
}

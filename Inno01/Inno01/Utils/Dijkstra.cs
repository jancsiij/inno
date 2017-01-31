using Inno01.Model;
using System.Collections.Generic;
using System.Linq;


namespace Inno01
{
    public class Dijkstra
    {

        List<City> visitedNodes = new List<City>();
        List<City> unVisitedNodes;

        List<Flight> visitedEdges = new List<Flight>();
        List<Flight> unVisitedEdges;

        public Dijkstra(Graph g)
        {
            unVisitedNodes = new List<City>(g.Nodes);
            unVisitedEdges = new List<Flight>(g.Edges);
        }

        public List<Flight> Shortestpath(City source, City dest)
        {
            Stack<City> previous = new Stack<City>();
            //Dictionary<City, City> previous = new Dictionary<City, City>();
            //Dictionary<City, int> distance = new Dictionary<City, int>();


            City current = source;
            City closest = new City();

            while (current != dest && unVisitedNodes.Count() >= 0 )
            {
               
                //GetDistance(source, current);

                var neighbours = FindNeigbours(current);

                if (neighbours.Count() != 0)
                {
                    int dist = int.MaxValue;
                    Flight shortestEdge = new Flight();

                    foreach (City item in neighbours)
                    {
                        var edge = GetShortestEdge(current, item);
                        if (edge.Distance < dist)
                        {
                            dist = edge.Distance;
                            closest = item;
                            shortestEdge = edge;
                        }
                    }
                    previous.Push(current);
                    visitedEdges.Add(shortestEdge);
                    //distance.Add(closest, edge.d);
                    current = closest;

                    unVisitedNodes.Remove(current);
                    visitedNodes.Add(current);
                }
                
                else if(previous.Count() != 0)
                {
                    visitedEdges.Remove(visitedEdges.Last());
                    current = previous.Pop();
                }

                else
                {
                    visitedEdges = null;
                    break;
                }
                
            }


            return visitedEdges;
        }

        private List<City> FindNeigbours(City current)
        {
            List<City> neighbors = new List<City>();
            foreach (Flight item in unVisitedEdges)
            {
                if(item.Origin == current && !visitedNodes.Contains(item.Destination))
                {
                    neighbors.Add(item.Destination);
                }
                
            }
            return neighbors;
        }

        private Flight GetShortestEdge(City source, City current)
        {
            //if (current == source)
            //{
            //    return 0;
            //}
            var edge = unVisitedEdges
                    .Where(u => u.Origin == source && u.Destination == current)
                    .OrderBy(u=>u.TimeIntervale)
                    .FirstOrDefault();

            //if (edge !=null)
            //{
            //    return edge.Distance;
            //}

            return edge;
        }
    }


}

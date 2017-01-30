using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inno01.Model
{
    public class City
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }

    public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
           
            return ((City)obj).Id == Id;    
        }

        public override string ToString()
        {
            return Name + " " + Id;
        }
    }

}

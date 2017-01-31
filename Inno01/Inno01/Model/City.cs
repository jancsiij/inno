

namespace Inno01.Model
{
    public class City
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }


        public override string ToString()
        {
            return Name + " " + Population +" lakos";
        }
    }

}

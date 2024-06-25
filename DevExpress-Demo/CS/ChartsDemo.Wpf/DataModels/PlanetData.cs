using System.Collections.Generic;

namespace ChartsDemo {

    public class PlanetInfo {
        readonly string planet;
        readonly double mass;
        readonly int moonsNumber;

        public string Planet { get { return this.planet; } }
        public double Mass { get { return this.mass; } }
        public int MoonsNumber { get { return this.moonsNumber; } }

        public PlanetInfo(string planet, double mass, int moonsNumber) {
            this.planet = planet;
            this.mass = mass;
            this.moonsNumber = moonsNumber;
        }
    }


    public static class PlanetData {
        public static List<PlanetInfo> Data {
            get {
                return new List<PlanetInfo>() {
                    new PlanetInfo("Mercury", 0.06, 0),
                    new PlanetInfo("Venus", 0.82, 0),
                    new PlanetInfo("Earth", 1, 1),
                    new PlanetInfo("Mars", 0.11, 2),
                    new PlanetInfo("Jupiter", 318, 69),
                    new PlanetInfo("Saturn", 95, 62),
                    new PlanetInfo("Uranus", 14.6, 27),
                    new PlanetInfo("Neptune", 17.2, 14)
                };
            }
        }
    }

}

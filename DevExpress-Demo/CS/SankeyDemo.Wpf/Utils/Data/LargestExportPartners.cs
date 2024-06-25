using System.Collections.Generic;

namespace SankeyDemo {
    static class LargestExportPartners {
        public static List<Export> GetData() {
            return new List<Export>() {
                new Export("United States", "Canada", 1.553 * 0.18),
                new Export("United States", "Mexico", 1.553 * 0.157),
                new Export("United States", "China", 1.553 * 0.084),
                new Export("United States", "Japan", 1.553 * 0.044),

                new Export("Germany", "United States", 1.434 * 0.088),
                new Export("Germany", "France", 1.434 * 0.082),
                new Export("Germany", "China", 1.434 * 0.068),
                new Export("Germany", "Netherlands", 1.434 * 0.067),
                new Export("Germany", "United Kingdom", 1.434 * 0.066),
                new Export("Germany", "Italy", 1.434 * 0.051),

                new Export("France", "Germany", 0.549 * 0.148),
                new Export("France", "Spain", 0.549 * 0.077),
                new Export("France", "Italy", 0.549 * 0.075),
                new Export("France", "United States", 0.549 * 0.072),

                new Export("Italy", "Germany", 0.496 * 0.125),
                new Export("Italy", "France", 0.496 * 0.103),
                new Export("Italy", "United States", 0.496 * 0.09),

                new Export("United Kingdom", "United States", 0.441 * 0.132),
                new Export("United Kingdom", "Germany", 0.441 * 0.105),
                new Export("United Kingdom", "France", 0.441 * 0.074),
                new Export("United Kingdom", "Netherlands", 0.441 * 0.062),

                new Export("Canada", "United States", 0.4235 * 0.764),
                new Export("Canada", "China", 0.4235 * 0.043),

                new Export("China", "United States", 2.216 * 0.192),
                new Export("China", "Japan", 2.216 * 0.059),
                new Export("China", "South Korea", 2.216 * 0.044),

                new Export("Australia", "China", 0.2172 * 0.218),
                new Export("Australia", "United States", 0.2172 * 0.125),
                new Export("Australia", "Argentina", 0.2172 * 0.081),
                new Export("Australia", "Netherlands", 0.2172 * 0.043),

                new Export("Mexico", "United States", 0.409 * 0.799),

                new Export("Japan", "United States", 0.6889 * 0.194),
                new Export("Japan", "China", 0.6889 * 0.19),
                new Export("Japan", "South Korea", 0.6889 * 0.076),

                new Export("Brazil", "China", 0.2172 * 0.218),
                new Export("Brazil", "United States", 0.2172 * 0.125),
                new Export("Brazil", "Argentina", 0.2172 * 0.081),
            };
        }
    }
}

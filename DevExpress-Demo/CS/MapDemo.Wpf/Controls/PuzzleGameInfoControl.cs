using System;
using System.Linq;
using System.Windows;

namespace MapDemo {
    public class PuzzleGameInfoControl : VisibleControl {
        public static readonly DependencyProperty SolvedCountriesCountProperty = DependencyProperty.Register("SolvedCountriesCount",
            typeof(int), typeof(PuzzleGameInfoControl), new PropertyMetadata(0));

        public int SolvedCountriesCount {
            get { return (int)GetValue(SolvedCountriesCountProperty); }
            set { SetValue(SolvedCountriesCountProperty, value); }
        }

        public PuzzleGameInfoControl() {
            DefaultStyleKey = typeof(PuzzleGameInfoControl);
        }
    }
}

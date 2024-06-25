using DevExpress.Mvvm.POCO;
using System;

namespace RichEditDemo {
    public class StatisticsViewModel {
        public static StatisticsViewModel Create(bool includeTextboxes, Func<bool, StatisticsData> getsStatisctics) {
            return ViewModelSource.Create(() => new StatisticsViewModel(includeTextboxes, getsStatisctics));
        }
        protected StatisticsViewModel(bool includeTextboxes, Func<bool, StatisticsData> getsStatisctics) {
            this.getsStatisctics = getsStatisctics;
            IncludeTextboxes = includeTextboxes;
        }
        readonly Func<bool, StatisticsData> getsStatisctics;
        public virtual StatisticsData Statistics { get; protected set; }
        bool includeTextboxes;
        public virtual bool IncludeTextboxes
        {
            get { return includeTextboxes; }
            set
            {
                includeTextboxes = value;
                Statistics = getsStatisctics(IncludeTextboxes);
            }
        }
    }
    public class StatisticsData {
        public StatisticsData(int noSpacesCharacterCount, int withSpacesCharacterCount, int wordCount, int paragraphCount) {
            NoSpacesCharacterCount = noSpacesCharacterCount;
            WithSpacesCharacterCount = withSpacesCharacterCount;
            WordCount = wordCount;
            ParagraphCount = paragraphCount;
        }

        public int NoSpacesCharacterCount { get; private set; }
        public int WithSpacesCharacterCount { get; private set; }
        public int WordCount { get; private set; }
        public int ParagraphCount { get; private set; }
    }
}

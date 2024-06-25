using System.Collections.Generic;
using System.Windows.Media;
using DevExpress.Mvvm;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public class DailyWeatherViewModel {
        const string vostokStationName = "Vostok Station";
        const string deathValleyName = "Death Valley, NV";

        readonly List<WeatherItem> weather;

        public List<WeatherItem> Weather { get { return this.weather; } }

        public DailyWeatherViewModel() {
            ExtremeWeatherData data = new ExtremeWeatherData();
            List<WeatherRecord> valleyData = data.LoadDeathValleyData();
            List<WeatherRecord> vostokData = data.LoadVostokStationData();

            Palette palette = new OfficePalette();
            this.weather = new List<WeatherItem>() {
                new WeatherItem(valleyData, false, palette[1], deathValleyName),
                new WeatherItem(valleyData, true, palette[1], deathValleyName),
                new WeatherItem(vostokData, false, palette[0], vostokStationName),
                new WeatherItem(vostokData, true, palette[0], vostokStationName),
            };
        }

        public virtual IChartAnimationService ChartAnimationService { get { return null; } }
        public void OnModuleLoaded() {
            if (ChartAnimationService != null)
                ChartAnimationService.Animate();
        }
    }

    public class WeatherItem : BindableBase {
        public int AverageLineThickness {
            get { return GetProperty(() => AverageLineThickness); }
            set { SetProperty(() => AverageLineThickness, value); }
        }
        public List<WeatherRecord> Data { get; private set; }
        public bool IsAverageWeather { get; private set; }
        public Color Color { get; private set; }
        public string Name { get; private set; }

        public WeatherItem(List<WeatherRecord> data, bool isAverageWeather, Color color, string name) {
            Data = data;
            IsAverageWeather = isAverageWeather;
            Color = color;
            Name = name;
            AverageLineThickness = 2;
        }
    }
}

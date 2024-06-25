using System;

namespace GridDemo {
    public class DataUpdateAnimationViewModel : StockViewModelBase {
        protected override double GetDeltaPrice(double currentPrice, bool dataGeneration) {
            if(!dataGeneration && Random.NextDouble() > 0.3)
                return 0;
            double delta = (Random.NextDouble() * 0.2 - 0.1) * currentPrice;
            return delta > 0 ? Math.Max(delta, 0.01) : Math.Min(delta, -0.01);
        }
    }
}

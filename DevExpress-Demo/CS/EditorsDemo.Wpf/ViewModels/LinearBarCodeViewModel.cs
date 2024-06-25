using DevExpress.XtraPrinting.BarCode.Native;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EditorsDemo {
    public class LinearBarCodeViewModel {
        public LinearBarCodeViewModel() {
            this.BarCodeSymbology = BarCodeSymbology.Code128;
            this.Text = "101";
            this.BarCodeModule = 3;
            this.AutoModule = true;
            this.ShowText = true;
        }
        public virtual bool ShowText { get; set; }
        public virtual bool AutoModule { get; set; }
        public virtual string Text { get; set; }
        public virtual double BarCodeModule { get; set; }
        public virtual BarCodeSymbology BarCodeSymbology { get; set; }
        public IEnumerable<BarCodeSymbology> BarCodeTypes { get { return GetBarCodeTypes(); } }
        protected virtual IEnumerable<BarCodeSymbology> GetBarCodeTypes() {
            return Enum.GetValues(typeof(BarCodeSymbology)).Cast<BarCodeSymbology>().TakeWhile(bcs =>
                bcs != BarCodeSymbology.QRCode &&
                bcs != BarCodeSymbology.DataMatrix &&
                bcs != BarCodeSymbology.DataMatrixGS1 &&
                bcs != BarCodeSymbology.PDF417);
        }
    }
    public class BarCode2DViewModel : LinearBarCodeViewModel {
        public BarCode2DViewModel() {
            this.BarCodeSymbology = BarCodeSymbology.QRCode;
        }
        protected override IEnumerable<BarCodeSymbology> GetBarCodeTypes() {
            return new BarCodeSymbology[] { BarCodeSymbology.QRCode, BarCodeSymbology.DataMatrix, BarCodeSymbology.DataMatrixGS1, BarCodeSymbology.PDF417 };
        }
    }
}

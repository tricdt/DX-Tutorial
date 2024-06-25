using System;
using System.Globalization;
using DevExpress.Office.NumberConverters;
using DevExpress.XtraRichEdit;

namespace RichEditDemo {
    public static class AutoCorrectHelper {
        public static void AutoCorrect(AutoCorrectEventArgs e) {
            AutoCorrectInfo info = e.AutoCorrectInfo;
            e.AutoCorrectInfo = null;

            if (info.Text.Length <= 0 || !info.Text.Contains("%"))
                return;
            int characterPosition = info.Text.IndexOf("%");
            int decrementCount = info.Text.Length - characterPosition - 1;
            for (int i = 0; i < decrementCount; i++)
                info.DecrementEndPosition();

            for (; ; ) {
                if (!info.DecrementStartPosition())
                    return;

                if (IsSeparator(info.Text[0]))
                    return;

                if (info.Text[0] == '%') {
                    string replaceString = CalculateFunction(info.Text);
                    if (!string.IsNullOrEmpty(replaceString)) {
                        info.ReplaceWith = replaceString;
                        e.AutoCorrectInfo = info;
                    }
                    return;
                }
            }
        }
        static bool IsSeparator(char ch) {
            return ch != '%' && (ch == '\r' || ch == '\n' || char.IsPunctuation(ch) || char.IsSeparator(ch) || char.IsWhiteSpace(ch));
        }
        static string CalculateFunction(string name) {
            name = name.ToLower();

            if (name.Length > 2 && name[0] == '%' && name.EndsWith("%")) {
                int value;
                if (int.TryParse(name.Substring(1, name.Length - 2), out value)) {
                    OrdinalBasedNumberConverter converter = OrdinalBasedNumberConverter.CreateConverter(NumberingFormat.CardinalText, LanguageId.English);
                    return converter.ConvertNumber(value);
                }
            }

            switch (name) {
                case "%date%":
                    return DateTime.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
                case "%time%":
                    return DateTime.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern);
                case "%bye%":
                    return "Yours sincerely,\r\nDavid B. Smith";
                default:
                    return String.Empty;
            }
        }
    }
}

using System;

namespace PivotGridDemo {
    public enum MoonPhase {
        NewMoon,
        WaxingCrescentMoon,
        FirstQuarterMoon,
        WaxingGibbousMoon,
        FullMoon,
        WaningGibbousMoon,
        LastQuarterMoon,
        WaningCrescentMoon
    }
    public static class MoonPhaseCalculator {
        public static MoonPhase CalculateMoonPhase(DateTime date) {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            double moonCycle = 29.53;
            double daysInMonth = 30.6;
            double daysInYear = 365.25;
            int phasesCount = 8;

            if(month < 3) {
                year--;
                month += 12;
            }
            month++;
            double totalDaysElapsed = daysInYear * year + daysInMonth * month + day - 694039.09;
            double phase = totalDaysElapsed / moonCycle;
            int result = (int)Math.Round(GetFracPart(phase) * phasesCount);
            return MoonPhaseFromInt(result % phasesCount);
        }
        static double GetFracPart(double value) {
            return value - Math.Floor(value);
        }
        static MoonPhase MoonPhaseFromInt(int phase) {
            switch(phase) {
            case 0:
                return MoonPhase.NewMoon;
            case 1:
                return MoonPhase.WaxingCrescentMoon;
            case 2:
                return MoonPhase.FirstQuarterMoon;
            case 3:
                return MoonPhase.WaxingGibbousMoon;
            case 4:
                return MoonPhase.FullMoon;
            case 5:
                return MoonPhase.WaningGibbousMoon;
            case 6:
                return MoonPhase.LastQuarterMoon;
            case 7:
                return MoonPhase.WaningCrescentMoon;
            default:
                throw new ArgumentException("Phase must be between 0 and 7", "phase");
            }
        }
    }
}

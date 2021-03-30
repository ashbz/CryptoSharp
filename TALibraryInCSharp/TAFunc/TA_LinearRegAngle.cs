using System;
namespace TALibraryInCSharp
     {
     public partial class TACore
     { 
        public static RetCode LinearRegAngle(int startIdx, int endIdx, double[] inReal, int optInTimePeriod, ref int outBegIdx, ref int outNBElement, double[] outReal)
        {
            if (startIdx < 0)
            {
                return RetCode.OutOfRangeStartIndex;
            }
            if ((endIdx < 0) || (endIdx < startIdx))
            {
                return RetCode.OutOfRangeEndIndex;
            }
            if (inReal == null)
            {
                return RetCode.BadParam;
            }
            if (optInTimePeriod == -2147483648)
            {
                optInTimePeriod = 14;
            }
            else if ((optInTimePeriod < 2) || (optInTimePeriod > 0x186a0))
            {
                return RetCode.BadParam;
            }
            if (outReal == null)
            {
                return RetCode.BadParam;
            }
            int lookbackTotal = LinearRegAngleLookback(optInTimePeriod);
            if (startIdx < lookbackTotal)
            {
                startIdx = lookbackTotal;
            }
            if (startIdx > endIdx)
            {
                outBegIdx = 0;
                outNBElement = 0;
                return RetCode.Success;
            }
            int outIdx = 0;
            int today = startIdx;
            double SumX = (optInTimePeriod * (optInTimePeriod - 1)) * 0.5;
            double SumXSqr = ((optInTimePeriod * (optInTimePeriod - 1)) * ((optInTimePeriod * 2) - 1)) / 6;
            double Divisor = (SumX * SumX) - (optInTimePeriod * SumXSqr);
            while (true)
            {
                if (today > endIdx)
                {
                    outBegIdx = startIdx;
                    outNBElement = outIdx;
                    return RetCode.Success;
                }
                double SumXY = 0.0;
                double SumY = 0.0;
                int i = optInTimePeriod;
                while (true)
                {
                    i--;
                    if (i == 0)
                    {
                        break;
                    }
                    double tempValue1 = inReal[today - i];
                    SumY += tempValue1;
                    SumXY += i * tempValue1;
                }
                double m = ((optInTimePeriod * SumXY) - (SumX * SumY)) / Divisor;
                outReal[outIdx] = Math.Atan(m) * 57.295779513082323;
                outIdx++;
                today++;
            }
        }
        public static RetCode LinearRegAngle(int startIdx, int endIdx, float[] inReal, int optInTimePeriod, ref int outBegIdx, ref int outNBElement, double[] outReal)
        {
            if (startIdx < 0)
            {
                return RetCode.OutOfRangeStartIndex;
            }
            if ((endIdx < 0) || (endIdx < startIdx))
            {
                return RetCode.OutOfRangeEndIndex;
            }
            if (inReal == null)
            {
                return RetCode.BadParam;
            }
            if (optInTimePeriod == -2147483648)
            {
                optInTimePeriod = 14;
            }
            else if ((optInTimePeriod < 2) || (optInTimePeriod > 0x186a0))
            {
                return RetCode.BadParam;
            }
            if (outReal == null)
            {
                return RetCode.BadParam;
            }
            int lookbackTotal = LinearRegAngleLookback(optInTimePeriod);
            if (startIdx < lookbackTotal)
            {
                startIdx = lookbackTotal;
            }
            if (startIdx > endIdx)
            {
                outBegIdx = 0;
                outNBElement = 0;
                return RetCode.Success;
            }
            int outIdx = 0;
            int today = startIdx;
            double SumX = (optInTimePeriod * (optInTimePeriod - 1)) * 0.5;
            double SumXSqr = ((optInTimePeriod * (optInTimePeriod - 1)) * ((optInTimePeriod * 2) - 1)) / 6;
            double Divisor = (SumX * SumX) - (optInTimePeriod * SumXSqr);
            while (true)
            {
                if (today > endIdx)
                {
                    outBegIdx = startIdx;
                    outNBElement = outIdx;
                    return RetCode.Success;
                }
                double SumXY = 0.0;
                double SumY = 0.0;
                int i = optInTimePeriod;
                while (true)
                {
                    i--;
                    if (i == 0)
                    {
                        break;
                    }
                    double tempValue1 = inReal[today - i];
                    SumY += tempValue1;
                    SumXY += i * tempValue1;
                }
                double m = ((optInTimePeriod * SumXY) - (SumX * SumY)) / Divisor;
                outReal[outIdx] = Math.Atan(m) * 57.295779513082323;
                outIdx++;
                today++;
            }
        }
        public static int LinearRegAngleLookback(int optInTimePeriod)
        {
            if (optInTimePeriod == -2147483648)
            {
                optInTimePeriod = 14;
            }
            else if ((optInTimePeriod < 2) || (optInTimePeriod > 0x186a0))
            {
                return -1;
            }
            return (optInTimePeriod - 1);
        }
     }
}

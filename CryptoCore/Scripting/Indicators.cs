using CryptoCore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TALibraryInCSharp;
using static CryptoCore.Classes.CandleInfo;
using static CryptoCore.Classes.Globals;

namespace CryptoCore.Scripting
{
    public static class Indicators
    {
        public static double[] SimpleMovingAverage(int Period, double[] Input)
        {
            int outBegIdx = 0;
            int outNbElement = 0;


            double[] output = new double[Input.Length];

            var res = TACore.MovingAverage(0, Input.Length - 1, Input, Period, TACore.MAType.Sma, ref outBegIdx, ref outNbElement, output);

            return NormalizeArray(output,Period);
        }


        public static double[] ExponentialMovingAverage(int Period, double[] Input)
        {
            int outBegIdx = 0;
            int outNbElement = 0;


            double[] output = new double[Input.Length];

            var res = TACore.MovingAverage(0, Input.Length - 1, Input, Period, TACore.MAType.Ema, ref outBegIdx, ref outNbElement, output);

            return NormalizeArray(output, Period);
        }


        public static double[] ATR(int Period, double[] Highs, double[] Lows, double[] Closes)
        {
            int outBegIdx = 0;
            int outNbElement = 0;


            double[] output = new double[Highs.Length];

            var res = TACore.Atr(0, Highs.Length - 1, Highs, Lows, Closes, Period, ref outBegIdx, ref outNbElement, output);

            return NormalizeArray(output, Period);
        }

        public static double[] WilliamsR(int Period, double[] Highs, double[] Lows, double[] Closes)
        {
            int outBegIdx = 0;
            int outNbElement = 0;


            double[] output = new double[Highs.Length];

            var res = TACore.WillR(0, Highs.Length - 1, Highs, Lows, Closes, Period, ref outBegIdx, ref outNbElement, output);

            return NormalizeArray(output, Period);
        }

        public static double[] ADX(int Period, double[] Highs, double[] Lows, double[] Closes)
        {
            int outBegIdx = 0;
            int outNbElement = 0;


            double[] output = new double[Highs.Length];

            var res = TACore.Adx(0, Highs.Length - 1, Highs, Lows, Closes, Period, ref outBegIdx, ref outNbElement, output);

            return NormalizeArray(output, Period);
        }


        public static double[] StandardDeviation(int Period, float StDev, double[] Input)
        {
            int outBegIdx = 0;
            int outNbElement = 0;


            double[] output = new double[Input.Length];

            var res = TACore.StdDev(0, Input.Length - 1, Input, Period, StDev, ref outBegIdx, ref outNbElement, output);

            return NormalizeArray(output, Period);
        }


        public static double[] CCI(int Period, double[] Highs,double[] Lows, double[] Closes)
        {
            int outBegIdx = 0;
            int outNbElement = 0;


            double[] output = new double[Highs.Length];

            var res = TACore.Cci(0, Highs.Length - 1, Highs,Lows,Closes,Period, ref outBegIdx, ref outNbElement, output);

            return NormalizeArray(output, Period);
        }


        public static double[] ParabolicSAR( double[] Highs, double[] Lows)
        {
            int outBegIdx = 0;
            int outNbElement = 0;


            double[] output = new double[Highs.Length];

            var res = TACore.Sar(0, Lows.Length - 1, Highs, Lows,0.2f,0.2f, ref outBegIdx, ref outNbElement, output);

            return output;// NormalizeArray(output, 1);
        }

        public static BollingerBandsData BollingerBands(int Period, float StDev, double[] Input)
        {
            var bb = new BollingerBandsData();

            int outBegIdx = 0;
            int outNbElement = 0;

            bb.Upper = new double[Input.Length];
            bb.Middle = new double[Input.Length];
            bb.Lower = new double[Input.Length];

            var res = TACore.Bbands(0, Input.Length - 1, Input, Period, StDev, StDev, TACore.MAType.Sma, ref outBegIdx, ref outNbElement, bb.Upper,bb.Middle,bb.Lower);

            bb.Upper = NormalizeArray(bb.Upper, Period);
            bb.Middle = NormalizeArray(bb.Middle, Period);
            bb.Lower = NormalizeArray(bb.Lower, Period);

            return bb;
        }


        public static double[] RSI(int Period, double[] Input)
        {
            int outBegIdx = 0;
            int outNbElement = 0;


            double[] output = new double[Input.Length];

            var res = TACore.Rsi(0, Input.Length - 1, Input, Period, ref outBegIdx, ref outNbElement, output);

            return NormalizeArray(output, Period);
        }



        public static MacdData MACD(int FastPeriod, int SlowPeriod, double[] Input)
        {
            int outBegIdx = 0;
            int outNbElement = 0;


            double[] output = new double[Input.Length];
            double[] outputSignal = new double[Input.Length];
            double[] outputHistogram = new double[Input.Length];

            var res = TACore.Macd(0, Input.Length - 1, Input, FastPeriod, SlowPeriod,9, ref outBegIdx, ref outNbElement, output,outputSignal,outputHistogram);

            var mData = new MacdData();

            mData.FastOutput = NormalizeArray(output, FastPeriod);
            mData.SlowOutput = NormalizeArray(outputSignal, SlowPeriod);

            return mData;
        }



        private static double[] NormalizeArray(double[] input, int period)
        {
            double[] finalOutput = new double[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                if (i < period)
                {
                    finalOutput[i] = 0;
                }
                else
                {
                    finalOutput[i] = input[i - period];
                }
            }

            return finalOutput;
        }

    }
}

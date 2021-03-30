using System;
namespace TALibraryInCSharp
     {
     public partial class TACore
     { 
        public static RetCode Div(int startIdx, int endIdx, double[] inReal0, double[] inReal1, ref int outBegIdx, ref int outNBElement, double[] outReal)
        {
            if (startIdx < 0)
            {
                return RetCode.OutOfRangeStartIndex;
            }
            if ((endIdx < 0) || (endIdx < startIdx))
            {
                return RetCode.OutOfRangeEndIndex;
            }
            if (inReal0 == null)
            {
                return RetCode.BadParam;
            }
            if (inReal1 == null)
            {
                return RetCode.BadParam;
            }
            if (outReal == null)
            {
                return RetCode.BadParam;
            }
            int i = startIdx;
            int outIdx = 0;
            while (i <= endIdx)
            {
                outReal[outIdx] = inReal0[i] / inReal1[i];
                i++;
                outIdx++;
            }
            outNBElement = outIdx;
            outBegIdx = startIdx;
            return RetCode.Success;
        }
        public static RetCode Div(int startIdx, int endIdx, float[] inReal0, float[] inReal1, ref int outBegIdx, ref int outNBElement, double[] outReal)
        {
            if (startIdx < 0)
            {
                return RetCode.OutOfRangeStartIndex;
            }
            if ((endIdx < 0) || (endIdx < startIdx))
            {
                return RetCode.OutOfRangeEndIndex;
            }
            if (inReal0 == null)
            {
                return RetCode.BadParam;
            }
            if (inReal1 == null)
            {
                return RetCode.BadParam;
            }
            if (outReal == null)
            {
                return RetCode.BadParam;
            }
            int i = startIdx;
            int outIdx = 0;
            while (i <= endIdx)
            {
                outReal[outIdx] = ((double)inReal0[i]) / ((double)inReal1[i]);
                i++;
                outIdx++;
            }
            outNBElement = outIdx;
            outBegIdx = startIdx;
            return RetCode.Success;
        }
        public static int DivLookback()
        {
            return 0;
        }
     }
}

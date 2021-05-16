using System;
using System.Collections.Generic;
using System.Text;

namespace CGT_ProjectWPF.Data_Structures
{
    class AnalyzeData
    {
        public float LF_TotalTime = 0;
        public float ISA_TotalTime = 0;
        public float LF_MinTime = float.MaxValue;
        public float ISA_MinTime = float.MaxValue;
        public float LF_MaxTime = 0;
        public float ISA_MaxTime = 0;
        public int LF_TimeCount = 0;
        public int ISA_TimeCount = 0;

        public int LF_TotalChromaticSum = 0;
        public int ISA_TotalChromaticSum = 0;
        public int LF_MinChromaticSum = int.MaxValue;
        public int ISA_MinChromaticSum = int.MaxValue;
        public int LF_MaxChromaticSum = 0;
        public int ISA_MaxChromaticSum = 0;
        public int LF_ChromaticSumCount = 0;
        public int ISA_ChromaticSumCount = 0;
        public int getLF_TimeAvg()
        {
            return (int)(LF_TotalTime / LF_TimeCount);
        }
        public int getISA_TimeAvg()
        {
            return (int)(ISA_TotalTime / ISA_TimeCount);
        }
        public int getLF_ChromAvg()
        {
            return (int)(LF_TotalChromaticSum / LF_ChromaticSumCount);
        }
        public int getISA_ChromAvg()
        {
            return (int)(ISA_TotalChromaticSum / ISA_ChromaticSumCount);
        }
        public void addLF_Time(float time)
        {
            LF_TotalTime += time;
            if (time < LF_MinTime) LF_MinTime = time;
            if (time > LF_MaxTime) LF_MaxTime = time;
            LF_TimeCount++;
        }
        public void addISA_Time(float time)
        {
            ISA_TotalTime += time;
            if (time < ISA_MinTime) ISA_MinTime = time;
            if (time > ISA_MaxTime) ISA_MaxTime = time;
            ISA_TimeCount++;
        }
        public void addLF_ChromSum(int newSum)
        {
            LF_ChromaticSumCount++;
            LF_TotalChromaticSum += newSum;
            if (newSum < LF_MinChromaticSum) LF_MinChromaticSum = newSum;
            if (newSum > LF_MaxChromaticSum) LF_MaxChromaticSum = newSum;
        }
        public void addISA_ChromSum(int newSum)
        {
            ISA_ChromaticSumCount++;
            ISA_TotalChromaticSum += newSum;
            if (newSum < ISA_MinChromaticSum) ISA_MinChromaticSum = newSum;
            if (newSum > ISA_MaxChromaticSum) ISA_MaxChromaticSum = newSum;
        }
    }
}

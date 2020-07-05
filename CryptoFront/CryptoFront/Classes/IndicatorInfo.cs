using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoFront.Classes
{
    public class IndicatorInfo
    {
        public string Name;
        public int Period = 14;
        public Color LineColor = Color.Firebrick;
        public bool Visible;
        public string InputValue;
        public float StdDev = 2.0f;

        public int FastPeriod = 12;
        public int SlowPeriod = 26;

        public Color FastColor = Color.LimeGreen;
        public Color SlowColor = Color.Orange;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCore.Scripting
{
    public static class Helper
    {
        public static void Log(string message)
        {
            
        }

        public static float GetPercentage(double current, double max)
        {

            var tmp = (current / max) * 100f;
            return (float)tmp;
        }

    }
}

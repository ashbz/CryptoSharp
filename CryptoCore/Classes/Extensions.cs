using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCore.Classes
{
    public static class Extensions
    {

        public static decimal Truncate(this decimal dec)
        {
            return decimal.Round(dec, 2, MidpointRounding.AwayFromZero);
        }


    }
}

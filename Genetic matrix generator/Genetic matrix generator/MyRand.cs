using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_matrix_generator
{
    public class MyRand
    {
        public static Random rnd;

        public static void setSeed(int seed = 0)
        {
            rnd = new Random(seed);
        }


        /// Returns random number in range[ range[0] , range[1] ]
        public static int Next(int [] range)
        {
            if (range == null)
                throw new Exception("Invalid range value for getting next random number!");

            return rnd.Next(range[0], range[1] + 1); // TODO: Overflow if range[1] is only 1's in binary...
        }
    }
}

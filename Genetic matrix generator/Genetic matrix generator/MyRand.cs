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

        // Returns a list of @count random numbers (different!) in the range[range[0], range[1]) !!
        // If @count is bigger than the range- returns empty list.
        public static List<int> NextRandsInRange(int a, int b, int count)
        {
            List<int> rands = new List<int>(count); // by default the elements are 0
            int tmp;

            if (count > b - a)
                return new List<int>();

            rands.ForEach(x => x = -1); 

            for (int i = 0; i < count; ++i)
            {
                do
                {
                    tmp = rnd.Next(a, b);
                } while (rands.Contains(tmp));
                rands.Add(tmp);
            }

            return rands;
        }
    }
}

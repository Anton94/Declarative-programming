using System;
using System.Collections.Generic;
using System.Linq;

namespace Genetic_matrix_generator
{
    public static class ListExtensions
    {
        public static List<T> listAdd<T>(this List<T> list, T t)
        {
            list.Add(t);
            return list;
        }
    }
    public class Program
    {
        public static Random rnd; // Random seed.
        public static bool printDebugInfo;


        //// Adds a matrix to the list and returns the list.
        //private static List<Matrix> listAdd(Matrix m, List<Matrix> list)
        //{
        //    list.Add(m);
        //    return list;
        //}

        // Prints the matrixes in the given @list.
        private static void listPrint(List<Matrix> list)
        {
            Console.WriteLine("Matrixes:");
            list.ForEach(m => Console.Write(m));
        }

        // This function is only for debugging purposes, I can put list[rnd.Next(0, list.Count)
        private static Matrix getRandFromList(List<Matrix> list)
        {
            int idx = rnd.Next(0, list.Count);
            if (printDebugInfo)
            {
                listPrint(list);
                Console.Write("Chosen index: " + idx + " and matrix:\n" + list[idx] + "\n");
            }

            return list[idx]; 
        }

        // The iterator...
        // When (if) the populatons dies- returns DefaultEmpty...
        public static IEnumerable<Matrix> generateMatrix(Matrix a, Matrix b)
        {
            List<Matrix> list = new List<Matrix> { a, b };
            
            while (list.Count >= 1) // Till the population dies...
            {
                // Every iteration to know exacly how many alive matrixes I have, so I can stop the cycle...
                // Generate the new matrix (with life + 1, because I will decrement it in the next step)
                // Add it to the list
                // Reduce lifes & filter
                // Return the last added
                yield return (list = list.listAdd(new Matrix(a.n).setAvarage(getRandFromList(list), getRandFromList(list))). // Generate the new matrix and adds it to the list
                                Select(m => m.DecLife()).Where<Matrix>(m => m.life > 0).ToList() // Map & filter
                            ).DefaultIfEmpty(). // If the Matrixes in the list are with life 1 and the generated is born dead(life = random-ziro + 1), then he will get life 0 in the 'map' and will be discard in the 'filter'
                            Last(); // Last element(the new one)
            }
        }

        public static void Main(string[] args)
        {
            Matrix.lifeMin = 0;
            Matrix.lifeMax = 5;
            rnd = new Random(0);
            printDebugInfo = true;
            int SIZE = 5, COUNT = 5;

            // Generates @COUNT matrixes with sizes @SIZEx@SIZE 
            // and values - chooses two random matrixes of already generated (and alive) once and makes average.
            // printDebugInfo- prints (or not) info for the chosen random matrixes and current matrixes in the list.
            generateMatrix(new Matrix(SIZE, 1.0) { life = 2 }, new Matrix(SIZE, 5.0) { life = 3 }).
                            Take(COUNT).ToList().
                            ForEach(m => Console.WriteLine("New generated:\n" + m));
            // The debugging info will be before the new generated and the life will not be with the generated value
            // because first generates the @COUNT matrixes, and after that prints them
            // so the generator modifies the generated once, decrementing their lifes...    



            // TO TEST FOR ME
            // to see what happens with different life ranges(when the population will die).

            //testForFun(17);
            //testForFun(17);
        }

        ////Prints how many generations will make till the population dies(range[0, @b])
        //public static void testForFun(int b)
        //{
        //    Matrix.lifeMin = 0;
        //    int count = 0, SIZE = 2;
        //    printDebugInfo = false;
        //    for (int i = 2; i <= b; ++i)
        //    {
        //        count = 0;
        //        Matrix.lifeMax = i;
        //        generateMatrix(new Matrix(SIZE, 1.0) { life = rnd.Next(2, Matrix.lifeMax) }, new Matrix(SIZE, 5.0) { life = rnd.Next(2, Matrix.lifeMax) }).
        //                        ToList().
        //                        ForEach(m => ++count);
        //        Console.WriteLine("Life range = [0, " + i.ToString() + "] total " + count.ToString() + " members born and now dead");
        //    }
        //}
    }
}

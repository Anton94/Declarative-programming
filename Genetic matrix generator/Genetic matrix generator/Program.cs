using System;
using System.Collections.Generic;
using System.Linq;

namespace Genetic_matrix_generator
{ 
    public class Program
    {
        // Random seed.
        public static Random rnd;


        // Adds a matrix to the list and returns the list.
        private static List<Matrix> listAdd(Matrix m, List<Matrix> list)
        {
            list.Add(m);
            return list;
        }

        // Prints the matrixes in the given @list.
        private static void listPrint(List<Matrix> list)
        {
            Console.WriteLine("Matrixes:");
            list.ForEach(m => Console.Write(m));
        }

        // This function is only for debugging purposes, I can put list[rnd.Next(0, list.Count)
        private static Matrix getRandFromList(List<Matrix> list, bool printDebugInfo)
        {
            int idx = rnd.Next(0, list.Count);
            if (printDebugInfo) Console.Write("Chosen index: " + idx + " and matrix:\n" + list[idx]);

            return list[idx]; 
        }

        // The iterator...
        public static IEnumerable<Matrix> generateMatrix(Matrix a, Matrix b, bool printDebugInfo = false)
        {
            List<Matrix> list = new List<Matrix> { a, b };
            
            while (true)
            {
                if (printDebugInfo) listPrint(list);
                yield return listAdd(
                            // First - find the new element depending on the current states of the matrixes in the list.
                            new Matrix(a.n).setAvarage(getRandFromList(list, printDebugInfo), getRandFromList(list, printDebugInfo)),
                            // after that apply the decrementing of matrixes lifes("map") and get those with lifes <= 0 out of the list ("filter").
                            list = list.Select(m => m.DecLife()).ToList().Where<Matrix>(m => m.life > 0).ToList()
                            ) // And add the new generated matrix to the list - "listAdd".
                            .Where<Matrix>(m => m.life > 0).ToList(). // Only removes the new one if it has life 0, costly operation .. I dont like it..                            
                            Last(); // Returns the last added element to the list.
                
                // Generate the new element
                // Reduce life of matrixes, filter the "dead" matrixes
                // Add the new element to the list
                // Filter the "dead" matrixes(because the new one could be born dead)
            }
        }

        // Generates @COUNT matrixes with sizes @SIZEx@SIZE 
        // and values - chooses two random matrixes of already generated (and alive) once and makes average.
        // printDebugInfo- prints (or not) info for the chosen random matrixes and current matrixes in the list.
        public static void test(int SIZE, int COUNT, bool printDebugInfo)
        {
            Matrix.lifeMin = 0;
            Matrix.lifeMax = 5;
            rnd = new Random(0);

            generateMatrix(new Matrix(SIZE, 1.0) { life = 2 }, new Matrix(SIZE, 5.0) { life = 3 }, printDebugInfo).
                            Take(COUNT).ToList().
                            ForEach(m => Console.WriteLine("New generated:\n" + m));
            // The debugging info will be before the new generated and the life will not be with the generated value
            // because first generates the @COUNT matrixes, and after that prints them
            // so the generator modifies the generated once, decrementing their lifes...
        }

        public static void Main(string[] args)
        {
            // Test with:  
            // matrixes size, 
            // number of matrixes to generate,
            // number of matrixes, which is taken the avarage of to generate the new one,
            // with (true) or without(false) debuging info for the list of matrixes and choosen random matrixes.
            test(5, 4, true);            
        }
    }
}

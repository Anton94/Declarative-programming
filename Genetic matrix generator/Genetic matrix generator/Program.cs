using System;
using System.Collections.Generic;
using System.Linq;

namespace Genetic_matrix_generator
{ 
    public class Program
    {
        // Generates @count random indexes 
        // and after that returns a list with @count "pointers" to the matrixes on thows random generated indexes of the given @list.
        private static List<Matrix> listGetRandomElements(List<Matrix> list, int count, bool printDebugInfo)
        {
            List<int> rands = MyRand.NextRandsInRange(0, list.Count, count);
            
            // Only for debugging purposes...
            if (printDebugInfo)
            {
                Console.Write("Chosen indexes: ");
                foreach (int i in rands)
                    Console.Write(i + " ");

                Console.Write("\nAnd matrixes:\n");
                foreach (int i in rands)
                    Console.Write(list[i] + "\n");
            }
            return rands.Select(i => list[i]).ToList() ;
        }

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
        
        // The iterator...
        public static IEnumerable<Matrix> generateMatrix(List<Matrix> inputList, int numOfParents = 2, int COUNT = int.MaxValue, bool printDebugInfo = false) // A bad style of infinity... but good enough for now...
        {
            int n = inputList[0].size; // Just to be more clear, I can use @a.size otherwise...

            List<Matrix> list = new List<Matrix>(inputList);
            
            for (int i = 0; i < COUNT; ++i)
            {
                if (printDebugInfo)
                    listPrint(list);
                yield return listAdd
                            (
                            // First - find the new element depending on the current states of the matrixes in the list.
                            new Matrix(n).setAvarage(listGetRandomElements(list, numOfParents, printDebugInfo)),
                            // after that apply the decrementing of matrixes lifes("map") and get those with lifes <= 0 out of the list ("filter").
                            list = list.Select(m => m.DecLife()).ToList().Where<Matrix>(m => m.life > 0).ToList()
                            ). // And add the new generated matrix to the list - "listAdd".
                            Last(); // Returns the last added element to the list.
            }
        }

        // Generates @COUNT matrixes with sizes @SIZEx@SIZE and making values depending on @numParents "parent" random matrixes of already generated (and alive).
        // and depends on printDebugInfo- prints (or not) info for the chosen random matrixes and current matrixes in the list
        public static void test(int SIZE, int COUNT, int numParents, bool printDebugInfo)
        {
            Matrix.setLifeRange(numParents, numParents + 3); // Setting life range of the matrixes
            MyRand.setSeed(); // And the seed for the random numbers...

            // Just to generate some starting matrixes(depending on the @numParents, the lifes and starting matrixes must be grater than @numParents, otherwise the population may die...)
            List<Matrix> inputList = new List<Matrix>();
            for (int i = 0; i < numParents; ++i)
            {
                inputList.Add(new Matrix(SIZE, i, numParents + (i % 3)));
            }

            foreach (Matrix m in generateMatrix(inputList, numParents, COUNT, printDebugInfo))
            {
                Console.WriteLine("New generated:\n" + m);
            }
        }

        public static void Main(string[] args)
        {
            // Test with:  
            // matrixes size, 
            // number of matrixes to generate,
            // number of matrixes, which is taken the avarage of to generate the new one,
            // with (true) or without(false) debuging info for the list of matrixes and choosen random matrixes.
            test(5, 4, 2, true);            
        }
    }
}

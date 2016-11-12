using System;
using System.Collections.Generic;
using System.Linq;

namespace Genetic_matrix_generator
{ 
    public class Program
    {
        private static Matrix[] getTwoRandomElementsFromCollection(List<Matrix> list)
        {
            int[] rands = MyRand.NextPair(0, list.Count);
            return new Matrix[] { list[rands[0]], list[rands[1]] };
        }

        private static Matrix listAdd(List<Matrix> list, Matrix m)
        {
            list.Add(m);
            return m;
        }
        public static IEnumerable<Matrix> generateMatrix(Matrix a, Matrix b, int COUNT = int.MaxValue) // A bad style of infinity... but good enough for now...
        {
            int n = a.size; // Just to be more clear, I can use @a.size otherwise...
            if (a.size != b.size)
                throw new Exception("Wrong input data for the generator...");

            List<Matrix> list = new List<Matrix> { a, b };
            
            for (int i = 0; i < COUNT; ++i)
            {
                yield return listAdd(list,
                                 //   Select(m => m.DecLife()).ToList().
                                  //  Where<Matrix>(m => m.life > 0).ToList(), 
                                    new Matrix(n).setAvarageOfTwoMatrixes(getTwoRandomElementsFromCollection(list))
                                    );
            }
        }

        public static void test(int SIZE, int COUNT)
        {
            Matrix a = new Matrix(SIZE, 1.0, 3);
            Matrix b = new Matrix(SIZE, 2.0, 2);
            
            foreach (Matrix m in generateMatrix(a, b, COUNT))
            {
                Console.WriteLine(m);
            }
        }

        public static void Main(string[] args)
        {
            Matrix.setLifeRange(2, 5); // Setting life range of the matrixes
            MyRand.setSeed(); // And the seed for the random numbers...
            test(5, 5);
            
        }
    }
}

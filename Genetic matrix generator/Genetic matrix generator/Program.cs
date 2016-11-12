using System;
using System.Collections.Generic;

namespace Genetic_matrix_generator
{ 
    public class Program
    {
        public static IEnumerable<Matrix> generateMatrix(Matrix a, Matrix b, int COUNT = int.MaxValue) // A bad style of infinity... but good enough for now...
        {
            int n = a.size; // Just to be more clear, I can use @a.size otherwise...
            if (a.size != b.size)
                throw new Exception("Wrong input data for the generator...");

            
            for (int i = 0; i < COUNT; ++i)
            {
                yield return new Matrix(n).setAvarageOfTwoMatrixes(a, b);
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

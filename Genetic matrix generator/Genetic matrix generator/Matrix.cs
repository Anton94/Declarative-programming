using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_matrix_generator
{
    public class Matrix
    {
        public double[,] matrix;
        public int size;
        public int life;
        public static int[] lifeRange;

        public static void setLifeRange(int minValue, int maxValue) // range == [min, max]
        {
            if (minValue > maxValue)
                throw new Exception("Invalid range for the life of the matrixes!");

            lifeRange = new int[2];
            lifeRange[0] = minValue;
            lifeRange[1] = maxValue;
        }

        public Matrix(int n)
        {
            createMatrix(n);
        }
        public Matrix(int n, double val, int life)
        {
            createMatrix(n);
            setDefaultValues(val);
            this.life = life;
        }

        public Matrix setAvarage(List<Matrix> m) // expect at least two element array!
        {
            // TODO: check matrix sizes if needed
            //    throw new Exception("Error in set avarage of matrix!");
            if (m.Count <= 0)
                return this;

            for (int i = 0; i < size; ++i)
                for (int j = 0; j < size; ++j)
                    this[i, j] = sum(m, i, j) / m.Count; // Very bad, cache misses a lot but for now is good...

            return this;
        }

        private double sum(List<Matrix> matrixes, int i, int j)
        {
            double sum = 0.0;

            matrixes.ForEach(m => sum += m[i, j]);

            return sum;
        }

        public Matrix DecLife()
        {
            life -= 1;
            return this;
        }

        public Matrix setLife()
        {
            life = MyRand.Next(lifeRange); // Returns random number in range[ lifeRange[0] , lifeRange[1] ]

            return this;
        }

        public double this[int i, int j]
        {
            get
            {
                return matrix[i, j];
            }
            set
            {
                matrix[i, j] = value;
            }
        }

        public override string ToString()
        {
            string res = "";

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    res += matrix[i, j].ToString() + " ";
                }
                res += "\n";
            }
            res += "Life = " + life.ToString() + " \n";

            return res;
        }

        // Sets all cells to @val value...
        public Matrix setDefaultValues(double val)
        {
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    matrix[i, j] = val;
                }
            }

            return this;
        }

        // Multiplies the value in the cell by it's i * j indexes
        public Matrix multiplyByIndexes()
        {
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    matrix[i, j] *= (i + 1) * (j + 1);
                }
            }

            return this;
        }

        private void createMatrix(int n)
        {
            size = n;
            matrix = new double[size, size];
            setLife();
        }
    }
}

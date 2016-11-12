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

        public Matrix setAvarageOfTwoMatrixes(Matrix a, Matrix b)
        {
            if (size != a.size || a.size != b.size)
                throw new Exception("Error in set avarage values of two matrixes!");

            for (int i = 0; i < size; ++i)
                for (int j = 0; j < size; ++j)
                    this[i, j] = (a[i, j] + b[i, j]) / 2.0;

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

        public void setDefaultValues(double val)
        {
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    matrix[i, j] = val;
                }
            }
        }

        private void createMatrix(int n)
        {
            size = n;
            matrix = new double[size, size];
            setLife();
        }
    }
}

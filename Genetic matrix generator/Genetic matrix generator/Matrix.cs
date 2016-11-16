using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_matrix_generator
{
    public class Matrix
    {
        public double[][] matrix;
        public int n;
        public int life;
        public static int lifeMin, lifeMax;

        public Matrix(int n, double val = 0.0)
        {
            this.n = n;
            matrix = new double[n][].
                        Select(row => row = new double[n].
                            Select(element => element = val).
                            ToArray()).
                        ToArray();
            life = Program.rnd.Next(lifeMin, lifeMax + 1);
        }

        public Matrix setAvarage(Matrix a, Matrix b) // expect at least two element array!
        {
            foreach(int i in Enumerable.Range(0, matrix[0].Count()))
                foreach(int j in Enumerable.Range(0, matrix[0].Count())) // TODO: if .Count is O(n) then cache it...
                    matrix[i][j] = (a.matrix[i][j] + b.matrix[i][j]) / 2.0; // I need to know which indexes are at this place in the matrix

            return this;
        }

        public Matrix DecLife()
        {
            life -= 1;
            return this;
        }

        public override string ToString()
        {
            string res = "";

            foreach (int i in Enumerable.Range(0, matrix[0].Count()))
            { 
                foreach (int j in Enumerable.Range(0, matrix[0].Count()))
                {
                    res += matrix[i][j].ToString() + " ";
                }
                res += "\n";
            }
            res += "Life = " + life.ToString() + " \n";

            return res;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Genetic_matrix_generator
{
    public class Matrix
    {
        public double[] matrix;
        public int n;
        public int life;
        public static int lifeMin, lifeMax;

        public Matrix(int n, double val = 0.0)
        {
            this.n = n;
            matrix = new double[n * n].Select(x => x = val).ToArray();
            life = Program.rnd.Next(lifeMin, lifeMax + 1);
        }

        public Matrix setAvarage(Matrix a, Matrix b)
        {
            foreach (int i in Enumerable.Range(0, matrix.Count()))
                matrix[i] = (a.matrix[i] + b.matrix[i]) * 0.5;

            return this;
        }

        public Matrix DecLife()
        {
            life -= 1;
            return this;
        }

        public override string ToString()
        {
            // string.Join(" ", matrix.Select(x => x.ToString()).ToArray()
            //     -> makes a string with all values, separate by space.
            // Now i need to split it in @n groups and add '\n' between them
            // ([^\s]?*\s) - detects the number and the space after it
            // {" + n.ToString() + "} - makes it @n times
            return String.Join("\n", Regex.Matches(String.Join(" ", matrix.Select(x => x.ToString()).ToArray()), 
                                    @"([^\s]*?\s){" + n.ToString() + @"}").
                                OfType<Match>().
                                Select(x => x.Value).
                                ToArray()
                            ) +
                            "\nLife = " + life.ToString() + " \n";
        }
    }
}

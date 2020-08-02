using System;
using Solutions;

namespace SolutionDebug
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] test9 = new int[] { 5, 1, 1 };
            Solution.NextPermutation (test9);


            Console.WriteLine(test9.ToString());
        }
    }
}

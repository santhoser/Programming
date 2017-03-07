using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palindrome
{
    /*
    A palindromic number reads the same both ways.The largest palindrome made 
    from the product of two 2-digit numbers is 9009 = 91 × 99.

    Write a method that finds and returns the largest palindrome made from the product of two 3-digit numbers 
    as well as the two 3-digit numbers.
    */
    public class Program
    {
        static void Main(string[] args)
        {
            int maxi = 0;
            int maxj = 0;
            int maxProd = 0;
            for (int i = 999; i > 100; i--)
            {
                for (int j = 999; j > 100; j--)
                {
                    int product = i * j;
                    if (IsPalindrome(product))
                    {
                        if (product > maxProd)
                        {
                            maxi = i;
                            maxj = j;
                            maxProd = product;
                        }

                    }
                }
            }
            Console.WriteLine(string.Format("The highest Palindrome number made from the product of two 3-digit numbers is {0}*{1}={2}", maxi, maxj, maxProd));
            Console.ReadKey();
        }

        public static bool IsPalindrome(int number)
        {
            string numberString = number.ToString();
            string reverseString = string.Empty;
            for (int i = numberString.Length - 1; i >= 0; --i)
            {
                reverseString += numberString[i];
            }
            return (numberString == reverseString);
        }
    }
}

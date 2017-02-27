using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanCalculator
{
    /// <summary>
    /// Accepts 2 roman numbers and print its sum in roman. (Consider values only of a sum less than 3000)
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("************************************");
            Console.WriteLine("******* Romans Calculator****************");
            Console.WriteLine("************************************");
            Console.WriteLine("\n");

            string roman1 = string.Empty;
            string roman2 = string.Empty;
            Console.Write("Enter the First Roman number : ");
            roman1 = Console.ReadLine();
            Console.Write("Enter the Second Roman number : ");
            roman2 = Console.ReadLine();

            // Validates the input
            if (IsValidRoman(roman1) && IsValidRoman(roman2))
            {
                // Gets the numberic equivalent of the roman input
                int numbericValueRoman1 = NumeralValueOfRoman(roman1);
                int numbericValueRoman2 = NumeralValueOfRoman(roman2);

                if (IsNotOutOfRangeRoman(numbericValueRoman1) && IsNotOutOfRangeRoman(numbericValueRoman2))
                {
                    // Adds the numeric total
                    int resultValue = numbericValueRoman1 + numbericValueRoman2;
                    if (IsNotOutOfRangeRoman(resultValue))
                    {
                        //Converts the NUmeric sum to Roman and prints the result
                        Console.WriteLine("The result is : " + ConvertToRoman(resultValue));
                    }
                    else {
                        Console.WriteLine("The result is greater than 3000");
                    }
                }
                else
                {
                    Console.WriteLine("Number to be less than or equal to 3000");
                }
            }
            else
            {
                Console.WriteLine("One of your input is invalid.");
            }



            

            Console.ReadKey();

        }

        /// <summary>
        ///  Returns the Numeric value corresponding to the input string
        /// </summary>
        /// <param name="roman"></param>
        /// <returns></returns>
        private static int NumeralValueOfRoman(string roman)
        {
            int numericValue = 0;

            for (int i = 0; i < roman.Length; i++)
            {
                string current = roman[i].ToString();
                if (i < (roman.Length - 1))
                {
                    string nxt = roman[i + 1].ToString();
                    if (IsNextHigherRank(current, nxt))
                    {
                        current += nxt;
                        i += 1;
                    }
                }
                numericValue += GetNumericForRoman(current);
            }
            return numericValue;

        }

        /// <summary>
        /// Returns the Numeric number corresponding to the input string/char.
        /// </summary>
        /// <param name="romanChar"></param>
        /// <returns></returns>
        private static int GetNumericForRoman(string romanChar)
        {
            List<RomanTable> romanTable = RomanDictionary();
            var intValue = romanTable.FirstOrDefault(r => r.RomanValue == romanChar).IntegerValue;
            return intValue;
        }

        /// <summary>
        /// Checks the adjust character on right is of greater value
        /// </summary>
        /// <param name="romanChar"></param>
        /// <param name="nextRomanChar"></param>
        /// <returns></returns>
        private static bool IsNextHigherRank(string romanChar, string nextRomanChar)
        {
            List<RomanTable> romanTable = RomanDictionary();
            var rank1 = romanTable.FirstOrDefault(r => r.RomanValue == romanChar).Rank;
            var rank2 = romanTable.FirstOrDefault(r => r.RomanValue == romanChar).Rank;
            return (rank1 < rank2);
        }

        /// <summary>
        /// Converts integer to Roman
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static string ConvertToRoman(int number)
        {
            string romanValue = string.Empty;
            if (number >= 1)
            {
                // Loop through each roman character from highest 
                List<RomanTable> orderedTable = RomanDictionary().OrderByDescending(o => o.Rank).ToList();
                foreach (var item in orderedTable)
                {
                    while (number >= item.IntegerValue)
                    {
                        romanValue = romanValue + RomanString(item.IntegerValue);
                        number -= item.IntegerValue;
                    }
                }
            }
            return romanValue;
        }

        /// <summary>
        /// Returns Roman Equvalent
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static string RomanString(int n)
        {
            string romanString = string.Empty;
            romanString = RomanDictionary().FirstOrDefault(r => r.IntegerValue == n).RomanValue;
            return romanString;
        }

        /// <summary>
        /// List of Roman Characters 
        /// </summary>
        /// <returns></returns>
        private static List<RomanTable> RomanDictionary()
        {

            List<RomanTable> romanTable = new List<RomanCalculator.RomanTable>();

            romanTable.Add(new RomanTable { IntegerValue = 1, RomanValue = "I", Rank = 1 });
            romanTable.Add(new RomanTable { IntegerValue = 4, RomanValue = "IV", Rank = 2 });
            romanTable.Add(new RomanTable { IntegerValue = 5, RomanValue = "V", Rank = 3 });
            romanTable.Add(new RomanTable { IntegerValue = 9, RomanValue = "IX", Rank = 4 });
            romanTable.Add(new RomanTable { IntegerValue = 10, RomanValue = "X", Rank = 5 });
            romanTable.Add(new RomanTable { IntegerValue = 40, RomanValue = "XL", Rank = 6 });
            romanTable.Add(new RomanTable { IntegerValue = 50, RomanValue = "L", Rank = 7 });
            romanTable.Add(new RomanTable { IntegerValue = 90, RomanValue = "XC", Rank = 8 });
            romanTable.Add(new RomanTable { IntegerValue = 100, RomanValue = "C", Rank = 9 });
            romanTable.Add(new RomanTable { IntegerValue = 400, RomanValue = "CD", Rank = 10 });
            romanTable.Add(new RomanTable { IntegerValue = 500, RomanValue = "D", Rank = 11 });
            romanTable.Add(new RomanTable { IntegerValue = 900, RomanValue = "CM", Rank = 12 });
            romanTable.Add(new RomanTable { IntegerValue = 1000, RomanValue = "M", Rank = 13 });
            return romanTable;
        }


        /// <summary>
        /// Validates the Input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool IsValidRoman(string input)
        {
            bool isValid = false;
            List<RomanTable> romanTable = RomanDictionary();
            foreach (var item in input)
            {
                isValid = romanTable.Exists(r => r.RomanValue == item.ToString());
            }
            return isValid;
        }


        private static bool IsNotOutOfRangeRoman(int input)
        {
            return input <= 3000;

        }

    }
    
}

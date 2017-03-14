using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency
{
    class Program
    {
        /// <summary>
        /// Given a Amount, List the Australian Currencies required to make up the Amount
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Write("Enter the Amount : ");
            int amount = Convert.ToInt32(Console.ReadLine());
            var currencies = RequiredCurrencies2(amount); // RequiredCurrencies(amount);
            var currencySet = currencies.GroupBy(a => a).Select(s => new { Currency = s.Key, Count = s.Count() });

            foreach (var set in currencySet)
            {
                Console.WriteLine(string.Format("{0} no of {1} Dollars", set.Count, set.Currency));
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Using Iteration
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        private static List<int> RequiredCurrencies(int amount)
        {
            List<int> currencies = new List<int>();
            List<int> audCurrencies = AustalianCurrencies();
            while (amount > 0)
            {
                int maxCur = audCurrencies.Where(a => a <= amount).Max();
                amount -= maxCur;
                currencies.Add(maxCur);
            }

            return currencies;
        }

        /// <summary>
        /// Using Recursion
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        private static List<int> RequiredCurrencies2(int amount)
        {
            return Recursive(amount, new List<int>());
        }

        private static List<int> Recursive(int amount, List<int> result)
        {
            if (amount > 0)
            {
                int maxCur = AustalianCurrencies().Where(a => a <= amount).Max();
                result.Add(maxCur);
                Recursive(amount - maxCur, result);
            }
            return result;
        }

        private static List<int> AustalianCurrencies()
        {
            List<int> currencies = new List<int> { 100, 50, 20, 10, 5, 2, 1 };
            return currencies;

        }
    }
}

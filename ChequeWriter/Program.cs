using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChequeWriter
{
    /// <summary>
    /// Accept an amount in numbers (between 0.00 and 2 billion) and print it in english as dollars and cents. 
    /// </summary>
    public class Program
    {

        static void Main(string[] args)
        {
            // Declare the input and output strings

            string inputMonetaryValueInNumberic = string.Empty;
            string outputMonetaryValueInEnglish = string.Empty;



            Console.WriteLine("************************************");
            Console.WriteLine("*******Cheque Writer****************");
            Console.WriteLine("************************************");
            Console.WriteLine("\n\n\n");

            Console.Write("Enter the monetary value : ");
            inputMonetaryValueInNumberic = Console.ReadLine();
            inputMonetaryValueInNumberic = inputMonetaryValueInNumberic.TrimStart('0');
            Console.WriteLine(inputMonetaryValueInNumberic);
            if (IsValidateMonetaryValue(inputMonetaryValueInNumberic))
            {
                outputMonetaryValueInEnglish = ConvertToEnglish(inputMonetaryValueInNumberic);
                Console.WriteLine(outputMonetaryValueInEnglish);
            }
            else
            {
                Console.WriteLine("Error : Invalid Input.");
            }

            Console.ReadKey();

        }

        /// <summary>
        /// Input Validation Logic
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool IsValidateMonetaryValue(string input)
        {
            bool isValid = true;
            double val = 0;
            if (!double.TryParse(input, out val))
            {
                isValid = false;
            }
            if (input.Contains('.'))
            {
                if (input.Substring(0, input.IndexOf('.')).Length > 10)
                {
                    isValid = false;
                }
                else if (input.Substring(input.IndexOf('.')).Length > 3)
                {
                    isValid = false;
                }
            }
            else
            {
                if (input.Length > 10)
                {
                    isValid = false;
                }
            }


            Regex regex = new Regex(@"[0-9]+(\.[0-9][0-9]?)?");
            Match match = regex.Match(input);
            if (!match.Success)
            {
                isValid = false;
            }

            return (isValid);
        }

        private static string ConvertToEnglish(string monetaryValue)
        {
            string englishOutput = string.Empty;
            string dollarPart = string.Empty;
            string cents = string.Empty;
            if (monetaryValue.Contains('.'))
            {
                cents = CovertCentsToEnglish(monetaryValue.Substring(monetaryValue.IndexOf(".") + 1));
                dollarPart = monetaryValue.Substring(0, monetaryValue.IndexOf("."));
            }
            else
            {
                dollarPart = monetaryValue;
            }
            string dollars = ConvertDollarToEnglish(dollarPart);
            englishOutput = string.IsNullOrWhiteSpace(dollars) ? string.Empty : dollars + " Dollars " + cents;

            return englishOutput;
        }

        /// <summary>
        /// Logic - Covert the Dollar value to English
        /// </summary>
        /// <param name="dollarPart"></param>
        /// <returns></returns>
        private static string ConvertDollarToEnglish(string dollarPart)
        {

            double dollarValue = Convert.ToDouble(dollarPart); 
            string englishValue = string.Empty;
            string and = string.Empty;
            // Using Stack to store each place
            Stack<string> english = new Stack<string>();

            if (dollarValue > 0)
            {
                dollarPart = dollarPart.TrimStart('0');

                int dollarPartLength = dollarPart.Length;
                int place = 0;
                for (int i = dollarPartLength - 1; i >= 0; i--)
                {
                    place += 1;
                    switch (place)
                    {
                        case 1:
                            english.Push(OnesPlace(dollarPart[i].ToString()));
                            break;
                        case 2:
                            if (Convert.ToInt32(dollarPart[i].ToString() + dollarPart[i + 1].ToString()) < 20)
                            {
                                english.Pop();
                                english.Push(TensPlace(dollarPart[i].ToString() + dollarPart[i + 1].ToString()).ToString());

                            }
                            else
                            {
                                english.Push(TensPlace(dollarPart[i].ToString() + "0"));
                            }

                            break;
                        case 3:

                            if (Convert.ToInt32(dollarPart[i].ToString()) > 0)
                            {
                                and = PushAndIfRequired(english);
                                english.Push(OnesPlace(dollarPart[i].ToString()) + " Hundred " + and);
                            }
                            break;

                        case 4:
                            if (Convert.ToInt32(dollarPart[i].ToString()) > 0)
                            {
                                and = PushAndIfRequired(english);
                                english.Push(OnesPlace(dollarPart[i].ToString()) + " Thousand " + and);
                            }
                            break;

                        case 5:
                            if (Convert.ToInt32(dollarPart[i].ToString()) > 0)
                            {
                                bool isPoped = false;
                                if (Convert.ToInt32(dollarPart[i].ToString() + dollarPart[i + 1].ToString()) < 20)
                                {
                                    if (english.Peek().ToString().Contains("Thousand"))
                                    {
                                        english.Pop();
                                        isPoped = true;
                                    }
                                    and = PushAndIfRequired(english);
                                    english.Push(TensPlace(dollarPart[i].ToString() + dollarPart[i + 1].ToString()) + (isPoped ? " Thousand " + and : string.Empty));
                                }
                                else
                                {
                                    and = PushAndIfRequired(english);
                                    english.Push(TensPlace(dollarPart[i].ToString() + "0") + (isPoped ? " Thousand " + and : string.Empty));
                                }
                            }
                            break;

                        case 6:
                            if (Convert.ToInt32(dollarPart[i].ToString()) > 0)
                            {
                                if (english.Peek().ToString().Contains("Thousand") || CheckSecondLastItemInStackContains(english, "Thousand"))
                                {
                                    and = PushAndIfRequired(english);
                                    english.Push(OnesPlace(dollarPart[i].ToString()) + " Hundred " + and);
                                }
                                else
                                {
                                    and = PushAndIfRequired(english);
                                    english.Push(OnesPlace(dollarPart[i].ToString()) + " Hundred Thousand " + and);
                                }
                            }
                            break;
                        case 7:
                            if (Convert.ToInt32(dollarPart[i].ToString()) > 0)
                            {
                                english.Push(OnesPlace(dollarPart[i].ToString()) + " Million " );
                            }
                            break;
                        case 8:
                            if (Convert.ToInt32(dollarPart[i].ToString()) > 0)
                            {
                                bool isPoped = false;
                                if (Convert.ToInt32(dollarPart[i].ToString() + dollarPart[i + 1].ToString()) < 20)
                                {
                                    if (english.Peek().ToString().Contains("Million"))
                                    {
                                        english.Pop();
                                        isPoped = true;
                                    }
                                    english.Push(TensPlace(dollarPart[i].ToString() + dollarPart[i + 1].ToString()) + (isPoped ? " Million " : string.Empty));

                                }
                                else
                                {
                                    english.Push(TensPlace(dollarPart[i].ToString() + "0") + (isPoped ? " Million " : string.Empty));
                                }
                            }
                            break;

                        case 9:
                            if (Convert.ToInt32(dollarPart[i].ToString()) > 0)
                            {
                                if (english.Peek().ToString().Contains("Million") || CheckSecondLastItemInStackContains(english, "Million"))
                                {
                                    english.Push(OnesPlace(dollarPart[i].ToString()) + " Hundred ");
                                }
                                else
                                {
                                    english.Push(OnesPlace(dollarPart[i].ToString()) + " Hundred Million ");
                                }
                            }

                            break;
                        case 10:
                            if (Convert.ToInt32(dollarPart[i].ToString()) > 0)
                            {
                                english.Push(OnesPlace(dollarPart[i].ToString()) + " Billion ");
                            }
                            break;
                        default:
                            break;
                    }
                    
                }
                while (english.Count > 0)
                {
                    englishValue += english.Pop().ToString();
                }
            }
            return englishValue;
        }

        /// <summary>
        /// Check for second top item in a Stack
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private static bool CheckSecondLastItemInStackContains(Stack<string> stack, string item)
        {
            string temp = stack.Pop();
            bool returnValue = false;
            if (stack.Count > 0 && stack.Peek().Contains(item))
            {
                returnValue = true;
            }
            stack.Push(temp);
            return returnValue;
        }

        /// <summary>
        /// Convert Cents to English
        /// </summary>
        /// <param name="decimalPart"></param>
        /// <returns></returns>
        private static string CovertCentsToEnglish(string decimalPart)
        {
            int decimalValue = Convert.ToInt32(decimalPart);
            string cents = "";
            if (decimalValue > 0)
            {
                if (decimalValue < 10)
                {
                    cents = OnesPlace(decimalPart);
                }
                else if (decimalValue < 20)
                {
                    cents = TensPlace(decimalPart);
                }
                else
                {
                    cents = TensPlace(decimalPart[0].ToString() + "0") + " " + OnesPlace(decimalPart[1].ToString());
                }
            }

            return string.IsNullOrWhiteSpace(cents) ? string.Empty : " AND " + cents + " CENTS";
        }

        // Convert the last digit to English
        private static string OnesPlace(string onesPlaceValue)
        {
            int value = Convert.ToInt32(onesPlaceValue);
            string englishEquvalent = string.Empty;
            Dictionary<int, string> ones = new Dictionary<int, string>();
            ones.Add(1, " One");
            ones.Add(2, " Two");
            ones.Add(3, " Three");
            ones.Add(4, " Four");
            ones.Add(5, " Five");
            ones.Add(6, " Six");
            ones.Add(7, " Seven");
            ones.Add(8, " Eight");
            ones.Add(9, " Nine");

            if (ones.ContainsKey(value))
            {
                englishEquvalent = ones[value];
            }

            return englishEquvalent;

        }

        // Convert the tenth place to English
        private static string TensPlace(string tensPlaceValue)
        {
            string englishEquvalent = string.Empty;
            int value = Convert.ToInt32(tensPlaceValue);
            Dictionary<int, string> tens = new Dictionary<int, string>();
            tens.Add(10, "Ten");
            tens.Add(20, "Twenty");
            tens.Add(30, "Thirty");
            tens.Add(40, "Forty");
            tens.Add(50, "Fifty");
            tens.Add(60, "Sixty");
            tens.Add(70, "Seventy");
            tens.Add(80, "Eighty");
            tens.Add(90, "Ninty");
            tens.Add(11, "Eleven");
            tens.Add(12, "Twelve");
            tens.Add(13, "Thrteen");
            tens.Add(14, "Fourteen");
            tens.Add(15, "Fifteen");
            tens.Add(16, "Sixteen");
            tens.Add(17, "Seventeen");
            tens.Add(18, "Eighteen");
            tens.Add(19, "Ninteen");
            if (tens.ContainsKey(value))
            {
                englishEquvalent = tens[value];
            }

            return englishEquvalent;

        }
        private static string PushAndIfRequired(Stack<string> stack)
        {
            return (stack.Count > 0) ? " AND " : string.Empty;
        }
    }


}

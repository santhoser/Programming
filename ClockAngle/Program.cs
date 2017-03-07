using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockAngle
{
    class Program
    {
        //Write a method that returns the angle between the hour and minute hand of a clock.

        static void Main(string[] args)
        {
            Console.Write("Enter the Hour Hand (0 - 12) : ");
            int hourHand = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the Minute Hand (0 - 60) : ");
            int minuteHand = Convert.ToInt32(Console.ReadLine());
            Console.Write(string.Format("The angle between hour hand and minute hand is {0} degree", GetAngle(hourHand, minuteHand)));
            Console.ReadKey();
        }

        public static double GetAngle(int hourHand, int minuteHand)
        {
            double oneMinuteAngle = (360 / 60);
            double oneHourAngle = (360 / 12);

            double hourA = oneHourAngle * hourHand;
            double minuteA = oneMinuteAngle * minuteHand;

            return (Math.Abs(hourA - minuteA));
        }
    }
}

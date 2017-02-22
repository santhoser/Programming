using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{

    static void Main(String[] args)
    {
        string a = Console.ReadLine();
        string b = Console.ReadLine();
        for (int i = 0; i < a.Length; i++)
        {
            for (int j = 0; j < b.Length; j++)
            {
                if (a[i] == b[j])
                {
                    a = a.Remove(i, 1);
                    b = b.Remove(j, 1);
                    i -= 1;
                    break;
                }
            }
        }


        for (int i = 0; i < b.Length; i++)
        {
            for (int j = 0; j < a.Length; j++)
            {
                if (b[i] == a[j])
                {
                    a = a.Remove(j, 1);
                    b = b.Remove(i, 1);
                    i -= 1;
                    break;
                }
            }
        }
        Console.WriteLine(a.Length + b.Length);
        Console.ReadKey();
    }
}

using System;
class Solution
{
    static void Main(String[] args)
    {

        int n = Convert.ToInt32(Console.ReadLine());
        String[] rocks = new String[n];
        for (int i = 0; i < n; i++)
        {
            rocks[i] = Console.ReadLine();
        }
        int c;
        int result = 0;
        int flag = 1;

        for (int j = 0; j < 26; j++)
        {
            c = 0;
            for (int i = 0; i < rocks.Length; i++)
            {
                int l = rocks[i].Length;
                for (int k = 0; k < l; k++)
                {
                    if (97 + j == rocks[i][k])
                    {
                        flag = 1;
                        break;
                    }
                    flag = 0;
                }
                if (flag == 0)
                    break;
                else
                    c++;
            }
            if (c == rocks.Length)
                result++;
        }
        Console.WriteLine(result);
    }
}
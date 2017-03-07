using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueUsingStack
{
    /*
     Implement a queue with methods void Enqueue(T) and T Dequeue() with the following conditions
    - Your queue may only use stacks as member variables. 
        You can assume a stack has the following methods void Push(T), T Pop(), int Size()
    - The Dequeue method must be O(1)
     * */

    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    public class Queue<T> where T : class
    {
        private Stack<T> input = new Stack<T>();
        private Stack<T> output = new Stack<T>();
        public void Enqueue(T t)
        {
            input.Push(t);
        }

        public T Dequeue()
        {
            if (output.Count == 0)
            {
                while (input.Count != 0)
                {
                    output.Push(input.Pop());
                }
            }
            return output.Pop();
        }
    }
}

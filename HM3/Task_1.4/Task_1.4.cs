using System;
using System.Collections.Generic;

namespace Task_1._4
{
    class Task_1_4
    {
        static void Main()
        {
            Console.WriteLine("\"Checking parentheses for pairedness\" by st. Dovhal Vladyslav");
            Console.WriteLine("RULE: You enter a mathematical expression and the compiler checks the parentheses.\n If there is an error, it will display it.\n");

            bool error = false;
            var stk = new Stack<BracketAndIndex>();
            var openBracket = new Dictionary<char, int>() { { '<', 1 }, { '{', 2 }, { '[', 3 }, { '(', 4 } };
            var closeBracket = new Dictionary<char, int>() { { '>', 1 }, { '}', 2 }, { ']', 3 }, { ')', 4 } };
            Console.Write("Enter the math expression: ");
            IsLineEmpty(out string exp);

            for (int i = 0; i < exp.Length; i++)
            {
                if (Check(exp[i], "<>{}[]()"))
                    if (stk.Count == 0)
                    {
                        if (!closeBracket.ContainsKey(exp[i])) stk.Push(new BracketAndIndex(exp[i], i));
                        else
                        {
                            Console.WriteLine($"Error in position {i}");
                            error = true;
                            break;
                        }
                    }
                    else
                    {
                        if (openBracket.ContainsKey(exp[i])) stk.Push(new BracketAndIndex(exp[i], i));
                        else if (openBracket[stk.Peek().bracket] == closeBracket[exp[i]]) stk.Pop();
                        else
                        {
                            Console.WriteLine($"Error in position {i}");
                            error = true;
                            break;
                        }
                    }
            }

            if (stk.Count != 0 && error == false) Console.WriteLine($"Error in position {stk.Peek().ind}");
            else Console.WriteLine("There are no mistakes in your expression!");

            Console.ReadLine();
        }

        internal struct BracketAndIndex
        {
            public char bracket;
            public int ind;
            public BracketAndIndex(char Bracket, int Ind)
            {
                bracket = Bracket;
                ind = Ind;
            }
        }

        internal static void IsLineEmpty(out string temp)
        {
            temp = Console.ReadLine();
            while (string.IsNullOrEmpty(temp))
            {
                Console.WriteLine("Line is not can be empty. Try again");
                temp = Console.ReadLine();
            }
        }

        internal static bool Check(char temp, string parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                if (temp == parameters[i]) return true;
            }
            return false;
        }
    }
}



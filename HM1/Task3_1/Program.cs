using System;
using System.Collections.Generic;
using System.Text;

namespace Task3_1
{
    class Program
    {
        static void Main(string[] args)
        {          
            bool dialogueMode;
            if (args != null && args.Length > 0) dialogueMode = false;
            else dialogueMode = true;
            const string Numbers = "0123456789.,";
            const string DifOps = "*/%p+-!";
            if (dialogueMode == true) Console.WriteLine("Simple Calculator developed by st. Dovhal Vladyslav");
            if (dialogueMode == true) Console.WriteLine("Enter the example you want to solve, then press \"Enter\". The free version allows a maximum of 2 operands.\n" +
                "Actions are available to you:\n\t\t+,\t\t-,\n\t\t* (x),\t\t/ (\\),\n\t\t%,\t\tpow(p),\n\t\t!,\t\t^,\n\t\t&,\t\t|.\n");
            while (true)
            {
            start:
                var input = new StringBuilder(Console.ReadLine().ToLower().Replace(" ", "").Replace("\t", "").Replace("\\", "/").Replace("x", "*").Replace("pow", "p"));
                var str = new StringBuilder();
                List<double> arr2 = new List<double>();
                double response = 0;
                int count = 0;
                bool GoStart;
                if (dialogueMode == true && input.ToString() == "e*it")
                {
                    Console.WriteLine("See you soon!");
                    return;
                }
                if (dialogueMode == true && input.ToString() == "help")
                {
                    Console.WriteLine("Actions are available to you:\n\t\t+,\t\t-,\n\t\t* (x),\t\t/ (\\),\n\t\t%,\t\tpow(p),\n\t\t!,\t\t^,\n\t\t&,\t\t|.\n");
                    goto start;
                }
                if (string.IsNullOrEmpty(input.ToString()))
                {
                    if (dialogueMode == true) Console.WriteLine("ERROR! Example can't be empty. Try again:\n");
                    if (dialogueMode == true) goto start;
                    else
                    {
                        Console.WriteLine(-1);
                        return;
                    }       
                }//проверка строки на пустоту
                for (int i = 0; i < input.Length; i++)
                {
                    if (Check(input[i], Numbers))
                    {
                        count = 0;
                        if (arr2.Count == 2)
                        {
                            if (dialogueMode == true) Console.WriteLine("ERROR! There can be only 1 or 2 numbers. Try again:\n");
                            if (dialogueMode == true) goto start;
                            else
                            {
                                Console.WriteLine(-1);
                                return;
                            }
                        }
                        while (Check(input[i], Numbers))
                        {
                            str.Append(input[i], 1);
                            if (i + 1 < input.Length) i++;
                            else break;
                        }
                        try
                        {
                            arr2.Add(double.Parse(str.ToString()));
                            str.Clear();
                        }
                        catch (OverflowException)
                        {
                            if (dialogueMode == true) Console.WriteLine("ERROR! The number out of the range. Try again:\n");
                            if (dialogueMode == true) goto start;
                            else
                            {
                                Console.WriteLine(-1);
                                return;
                            }
                        }
                    }
                    if (Check(input[i], DifOps)) count++;
                    else count = 0;
                    if (count == 3)
                    {
                        if (dialogueMode == true) Console.WriteLine("ERROR! Incorrect input. Try again:\n");
                        if (dialogueMode == true) goto start;
                        else
                        {
                            Console.WriteLine(-1);
                            return;
                        }
                    }
                }//поиск, контроль и запись чисел в массив. если 3 операции подряд - запись неправильная
                if (arr2.Count != 0) response = arr2[0];
                else
                {
                    if (dialogueMode == true) Console.WriteLine("ERROR! Incorrect input. Try again:\n");
                    if (dialogueMode == true) goto start;
                    else
                    {
                        Console.WriteLine(-1);
                        return;
                    }
                }//сохраняем первое число     
                if (input[0] == '-' && arr2.Count == 2) arr2[0] = -arr2[0];//если два числа и первый минус, первое число умножаем на -1
                if (arr2.Count == 2)
                {
                    input.Replace("++", "+");
                    input.Replace("--", "+");
                    input.Replace("+-", "-");
                    input.Replace("-+", "-");
                }//заменяем неудобные конструкции                
                count = 0;
                if (Check('&', input.ToString())) count++;
                if (Check('|', input.ToString())) count++;
                if (Check('^', input.ToString())) count++;
                if (count > 0)
                {
                    for (int j = 0; j < input.Length; j++)
                    {
                        if (Check(input[j], DifOps) || count != 1)
                        {
                            if (dialogueMode == true) Console.WriteLine("ERROR! Incorrect input. Try again:\n");
                            if (dialogueMode == true) goto start;
                            else
                            {
                                Console.WriteLine(-1);
                                return;
                            }
                        }
                    }
                    for (int j = 0; j < input.Length; j++)
                    {
                        if (Check(input[j], "^&|"))
                            switch (input[j])
                            {
                                case '^':
                                case '&':
                                case '|':
                                    CheckSyntaxBinaryBit(dialogueMode, input, arr2, ref j, input[j], ref response, out GoStart);
                                    if (GoStart == true)
                                    {
                                        if (dialogueMode == true) goto start;
                                        else
                                        {
                                            Console.WriteLine(-1);
                                            return;
                                        }
                                    }
                                    break;
                            }
                    }
                }//проверка на наличие битовых операций 
                else for (int i = 0; i < input.Length; i++)
                    {
                        if (!Check(input[i], Numbers))
                        {
                            switch (input[i])
                            {
                                case 'p':
                                case '%':
                                case '/':
                                case '*':
                                    CheckSyntaxBinary(dialogueMode, input, arr2, Numbers, ref i, input[i], ref response, out GoStart);
                                    if (GoStart == true)
                                    {
                                        if (dialogueMode == true) goto start;
                                        else
                                        {
                                            Console.WriteLine(-1);
                                            return;
                                        }
                                    };
                                    break;
                                case '-':
                                case '+':
                                    CheckSyntaxBinaryPlusMinus(dialogueMode, input, arr2, Numbers, ref i, input[i], ref response, out GoStart);
                                    if (GoStart == true)
                                    {
                                        if (dialogueMode == true) goto start;
                                        else
                                        {
                                            Console.WriteLine(-1);
                                            return;
                                        }
                                    };
                                    break;
                                case '!':
                                    {
                                        if (i == 0 && arr2.Count == 1 && arr2[0] >= 0) response = -1 * arr2[0];
                                        else
                                        if (i + 1 == input.Length && arr2.Count == 1 && arr2[0] >= 0) response = Factorial((int)arr2[0]);
                                        else
                                        {
                                            if (dialogueMode == true) Console.WriteLine("ERROR! Incorrect input. Try again:\n");
                                            if (dialogueMode == true) goto start;
                                            else
                                            {
                                                Console.WriteLine(-1);
                                                return;
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    if (dialogueMode == true) Console.WriteLine("ERROR! Incorrect input. Try again:\n");
                                    if (dialogueMode == true) goto start;
                                    else
                                    {
                                        Console.WriteLine(-1);
                                        return;
                                    }
                            }
                        }
                    }   //остальные случаи
                Console.WriteLine(response);
                if (dialogueMode == true) Console.WriteLine();
                else return;
            }
        }
        static bool Check(char temp, string parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                if (temp == parameters[i]) return true;
            }
            return false;
        }
        static int Factorial(int x)
        {
            if (x == 0)
            {
                return 1;
            }
            else
            {
                return x * Factorial(x - 1);
            }
        }
        static void CheckSyntaxBinary(in bool dialogueMode, in StringBuilder input, in List<double> arr2, in string Numbers, ref int i, char symb, ref double response, out bool GoStart)
        {
            GoStart = false;
            if (i + 1 == input.Length || i == 0 || arr2.Count == 1)
            {
                Console.WriteLine("ERROR! Incorrect input. Try again:\n");
                GoStart = true;
                return;
            }
            else
            if (!Check(input[i + 1], Numbers))
            {
                switch (input[i + 1])
                {
                    case '+':
                        switch (symb)
                        {
                            case '%':
                                response = arr2[0] % (arr2[1]);
                                break;
                            case '/':
                                response = arr2[0] / (arr2[1]);
                                break;
                            case 'p':
                                response = Math.Pow(arr2[0], (arr2[1]));
                                break;
                            case '*':
                                response = arr2[0] * (arr2[1]);
                                break;
                        }
                        i += 2;
                        break;
                    case '-':
                        switch (symb)
                        {
                            case '%':
                                response = arr2[0] % (-arr2[1]);
                                break;
                            case '/':
                                response = arr2[0] / (-arr2[1]);
                                break;
                            case 'p':
                                response = Math.Pow(arr2[0], (-arr2[1]));
                                break;
                            case '*':
                                response = arr2[0] * (-arr2[1]);
                                break;
                        }
                        i += 2;
                        break;
                    default:
                        if (dialogueMode == true) Console.WriteLine("ERROR! Incorrect input. Try again:\n");
                        GoStart = true;
                        return;
                }
            }
            else if (symb == '/' && arr2[1] == 0)
            {
                if (dialogueMode == true) Console.WriteLine("ERROR! Division by zero. Try again:\n");
                GoStart = true;
                return;
            }
            else
            {
                switch (symb)
                {
                    case '%':
                        response = arr2[0] % (arr2[1]);
                        break;
                    case '/':
                        response = arr2[0] / (arr2[1]);
                        break;
                    case 'p':
                        response = Math.Pow(arr2[0], (arr2[1]));
                        break;
                    case '*':
                        response = arr2[0] * (arr2[1]);
                        break;
                }
                i++;
            }
        }
        static void CheckSyntaxBinaryBit(in bool dialogueMode, in StringBuilder input, in List<double> arr2, ref int j, char symb, ref double response, out bool GoStart)
        {
            GoStart = false;
            if (j + 1 == input.Length || j == 0 || arr2.Count == 1 || input[j + 1] == symb)
            {
                if (dialogueMode == true) Console.WriteLine("ERROR! Incorrect input. Try again:\n");
                GoStart = true;
                return;
            }
            else
            {
                switch (symb)
                {
                    case '^':
                        response = (int)arr2[0] ^ (int)arr2[1];
                        break;
                    case '&':
                        response = (int)arr2[0] & (int)arr2[1];
                        break;
                    case '|':
                        response = (int)arr2[0] | (int)arr2[1];
                        break;
                }
                j += 1;
            }
        }
        static void CheckSyntaxBinaryPlusMinus(in bool dialogueMode, in StringBuilder input, in List<double> arr2, in string Numbers, ref int i, char symb, ref double response, out bool GoStart)
        {
            GoStart = false;
            if (i + 1 < input.Length)
            {
                if (input[i + 1] == symb)
                    switch (symb)
                    {
                        case '-':
                            response = arr2[0] - 1;
                            i += 2;
                            break;
                        case '+':
                            response = arr2[0] + 1;
                            i += 2;
                            break;
                        default:
                            if (dialogueMode == true) Console.WriteLine("ERROR! Incorrect input. Try again:\n");
                            GoStart = true;
                            return;
                    }
                else if (arr2.Count == 2)
                {
                    switch (symb)
                    {
                        case '-':
                            response = arr2[0] - arr2[1];
                            break;
                        case '+':
                            response = arr2[0] + arr2[1];
                            break;
                    }
                    i++;
                }
                else if (symb == '-' && i == 0 && arr2.Count == 1 && Check(input[^1], Numbers))
                {
                    response = -1 * arr2[0];
                    i++;
                }
                else
                {
                    if (dialogueMode == true) Console.WriteLine("ERROR! Incorrect input. Try again:\n");
                    GoStart = true;
                    return;
                }
            }
            else
            {
                if (dialogueMode == true) Console.WriteLine("ERROR! Incorrect input. Try again:\n");
                GoStart = true;
                return;
            }
        }
    }
}

using System;
using System.Text;

namespace Task2_1
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Rock-Paper-Scissors developed by st. Dovhal Vladyslav");
            int person = 0, computer = 0;
            while (true)
            {
                Console.WriteLine("Rock > scissors; scissors > paper; paper > rock.");
                Console.WriteLine("Commands: \"rock\", \"scissors\", \"paper\", \"exit\"");
                string command;
                Random rnd = new Random();
                while (true)
                {
                    Console.Write("\nWrite command: ");
                    command = Console.ReadLine().ToLower();
                    if (command != "exit" && command != "rock" && command != "scissors"
                        && command != "paper")
                    {
                        Console.WriteLine("ERROR: incorrect input data, write right command.\n" +
                            "Commands: \"rock\", \"scissors\", \"paper\", \"exit\"");
                        continue;
                    }
                    Console.WriteLine("Well done!\n");
                    break;
                }
                if (command == "exit") break;
                switch (rnd.Next(1, 4))
                {
                    case 1:
                        Console.WriteLine("Computer`s response: \"paper\";");
                        if (command == "scissors") { Console.WriteLine("YOU WIN!\n\n"); person++; }
                        if (command == "rock") { Console.WriteLine("UNFORTUNATELY, YOU LOSE!\n\n"); computer++; }
                        if (command == "paper") Console.WriteLine("DRAW!\n\n");
                        break;
                    case 2:
                        Console.WriteLine("Computer`s response: \"scissors\";");
                        if (command == "rock") { Console.WriteLine("YOU WIN!\n\n"); person++; }
                        if (command == "paper") { Console.WriteLine("UNFORTUNATELY, YOU LOSE!\n\n"); computer++; }
                        if (command == "scissors") Console.WriteLine("DRAW!\n\n");
                        break;
                    case 3:
                        Console.WriteLine("Computer`s response: \"rock\";");
                        if (command == "paper") { Console.WriteLine("YOU WIN!\n\n"); person++; }
                        if (command == "scissors") { Console.WriteLine("UNFORTUNATELY, YOU LOSE!\n\n"); computer++; }
                        if (command == "rock") Console.WriteLine("DRAW!\n\n");
                        break;
                }
            }
            Console.WriteLine($"\tCOUNT\t\t\nYOU: {person}\tCOMPUTER: {computer}");          
        }
    }
}

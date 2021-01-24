using System;

namespace Task2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0) Solution(false);
            else Solution(true);
        }
        static void AreaCircle(in double _r, out double area) => area = Math.Round(3.14 * _r * _r, 2);
        static void AreaSquare(in double _a, out double area) => area = _a * _a;
        static void AreaRect(in double _a, in double _b, out double area) => area = _a * _b;
        static void AreaTriangle(in double _a, in double _b, in double _c, out double area)
        {
            double p = (_a + _b + _c) / 2;
            area = Math.Sqrt(p * (p - _a) * (p - _b) * (p - _c));
        }
        static void Solution(bool _dialogmode)
        {
            double area;
            string input;
            if (_dialogmode == true) Console.WriteLine("Area Calculator developed by st. Dovhal Vladyslav");
            while (true)
            {
                area = 0;
                if (_dialogmode == true) Console.WriteLine("\nAvailable shapes: \"circle\", \"square\", \"rect\", \"triangle\".");
                if (_dialogmode == true) Console.Write("enter shape and its side(s) through the space: ");
                input = Console.ReadLine().ToLower();
                if (_dialogmode == true && input == "exit") break;
                string[] subs = input.Split(' ');
                if (subs.Length != 2 && subs.Length != 3 && subs.Length != 4)
                {
                    if (_dialogmode == true)
                    {
                        Console.WriteLine("ERROR! Too little/many input data");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine(-1);
                        return;
                    }
                }
                if (subs[0] != "circle" && subs[0] != "square" && subs[0] != "rect" && subs[0] != "triangle")
                {
                    if (_dialogmode == true)
                    {
                        Console.WriteLine("ERROR! Incorrect shape");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine(-1);
                        return;
                    }
                }
                int i = 1;
                bool error = false;
                double[] sides = new double[subs.Length - 1];
                while (i < subs.Length)
                {
                    if (!double.TryParse(subs[i], out sides[i - 1]))
                    {
                        if (_dialogmode == true) Console.WriteLine("ERROR! Sides must be numbers");
                        error = true;
                        break;
                    }
                    if (sides[i - 1] <= 0)
                    {
                        if (_dialogmode == true) Console.WriteLine("ERROR! Sides must be > 0");
                        error = true;
                        break;
                    }
                    i++;
                }
                if (error == true)
                {
                    if (_dialogmode == true) continue;
                    else
                    {
                        Console.WriteLine(-1);
                        return;
                    }
                }
                switch (subs[0])
                {
                    case "circle":
                        if (subs.Length != 2)
                        {
                            if (_dialogmode == true)
                            {
                                Console.WriteLine("ERROR! Circle have only radious");
                                continue;
                            }
                            else
                            {
                                Console.WriteLine(-1);
                                return;
                            }
                        }
                        AreaCircle(sides[0], out area);
                        break;
                    case "square":
                        if (subs.Length != 2)
                        {
                            if (_dialogmode == true)
                            {
                                Console.WriteLine("ERROR! Square have only side");
                                continue;
                            }
                            else
                            {
                                Console.WriteLine(-1);
                                return;
                            }
                        }
                        AreaSquare(sides[0], out area);
                        break;
                    case "rect":
                        if (subs.Length != 3)
                        {
                            if (_dialogmode == true)
                            {
                                Console.WriteLine("ERROR! Rect have only 2 sides");
                                continue;
                            }
                            else
                            {
                                Console.WriteLine(-1);
                                return;
                            }
                        }
                        AreaRect(sides[0], sides[1], out area);
                        break;
                    case "triangle":
                        if (subs.Length != 4)
                        {
                            if (_dialogmode == true)
                            {
                                Console.WriteLine("ERROR! Triangle have only 3 sides");
                                continue;
                            }
                            else
                            {
                                Console.WriteLine(-1);
                                return;
                            }
                        }
                        AreaTriangle(sides[0], sides[1], sides[2], out area);
                        break;
                }
                if (_dialogmode == true) Console.Write("Shape`s area:\t");
                Console.WriteLine(area);
                if (_dialogmode != true) break;
            }
        }

    }
}

// Program.cs
using CalculatorLibrary;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            int operationCount = 0;
            
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;

                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }

                
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tq - Square Root");
                Console.WriteLine("\te - 10^x");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Handle single operand operations
                if (op == "q" || op == "e")
                {
                    try
                    {
                        result = PerformSingleOperandOperation(cleanNum1, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else 
                        {
                            Console.WriteLine("Your result: {0:0.##}\n", result);
                            operationCount++;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("It seems as though there is a problem with what you typed\n - Details: " + e.Message);
                    }
                }
                else
                {
                    // Ask for second number for two-operand operations
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();

                    double cleanNum2 = 0;
                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput2 = Console.ReadLine();
                    }

                    
                    if (op == null || !Regex.IsMatch(op, "[a|s|m|d|p]"))
                    {
                        Console.WriteLine("Error: Unrecognized input.");
                    }
                    else
                    {
                        try
                        {
                            if (op == "p")
                            {
                                result = Math.Pow(cleanNum1, cleanNum2);
                            }
                            else
                            {
                                result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                            }
                            
                            if (double.IsNaN(result))
                            {
                                Console.WriteLine("This operation will result in a mathematical error.\n");
                            }
                            else 
                            {
                                Console.WriteLine("Your result: {0:0.##}\n", result);
                                operationCount++;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                        }
                    }
                }
                Console.WriteLine("------------------------\n");

                
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n");
            }
            
            calculator.Finish();
            Console.WriteLine($"\nTotal times calcultor was used: {operationCount}");
            return;
        }

        static double PerformSingleOperandOperation(double num, string operation)
        {
            return operation switch
            {
                "q" => Math.Sqrt(num),
                "e" => Math.Pow(10, num),
                _ => double.NaN
            };
        }
    }
    
}
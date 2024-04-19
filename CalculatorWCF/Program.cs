using CalculatorService;

namespace CalculatorWCF
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("WCF kalkulátor teszt program");
            int intA = 0, intB = 0;
            char operation = default;
            GetNumber(ref intA, "az első");
            Console.WriteLine();
            GetNumber(ref intB, "a második");
            Console.Write("\nMilyen műveletet szeretne végrehajtani? (+, -, *, /): ");
            GetOperation(ref operation);
            await Calculate(intA, intB, operation);
            Console.ReadKey();
        }

        static void GetNumber(ref int num, string order)
        {
            bool success = false;
            do
            {
                Console.Write($"Kérem adja meg {order} számot: ");
                var input = Console.ReadKey();
                try
                {
                    string c = input.KeyChar.ToString();
                    num = Convert.ToInt32(c);
                    success = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nNem egy számot adott meg.");
                }
            } while (!success);
        }

        static void GetOperation(ref char operation)
        {
            bool success = false;
            do
            {
                char[] operationArr = ['+', '-', '*', '/'];
                var input = Console.ReadKey();
                char o = input.KeyChar;
                if (operationArr.Contains(o))
                {
                    operation = o;
                    success = true;
                }
            } while (!success);
        }

        static async Task Calculate(int num1, int num2, char operation)
        {
            Console.WriteLine($"\nA következő művelet eredménye:");
            CalculatorSoapClient client = new CalculatorSoapClient(CalculatorSoapClient.EndpointConfiguration.CalculatorSoap);
            int? result = null;
            switch (operation)
            {
                case '+':
                    result = await client.AddAsync(num1, num2);
                    break;
                case '-':
                    result = await client.SubtractAsync(num1, num2);
                    break;
                case '*':
                    result = await client.MultiplyAsync(num1, num2);
                    break;
                case '/':
                    result = await client.DivideAsync(num1, num2);
                    break;
                default:
                    break;
            }
            Console.WriteLine($"{num1} {operation} {num2} = {result}");
        }
    }
}

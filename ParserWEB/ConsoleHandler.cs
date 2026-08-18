
namespace ParserWEB
{
    internal class ConsoleHandler
    {
        internal string InputConsole(string inputMassage)
        {
            string? input = null;

            while (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(inputMassage);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Вы ничего не ввели, попробуйте еще раз.");
                }
                else
                {
                    Console.WriteLine($"Вы ввели: {input}");
                }
            }
            return input;
        }      
    }
}

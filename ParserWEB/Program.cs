using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;


namespace ParserWEB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var consoleInput = new ConsoleHandler();
            var searchQuery = consoleInput.InputConsole();

            var sourceForSearch = "https://yandex.ru/images/";
            var yandexParser = new Parser();
            yandexParser.spiderMain(searchQuery, sourceForSearch);


            // Debug.WriteLine(searchQuery);
            
        }

        
    }
}
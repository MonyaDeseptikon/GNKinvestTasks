
namespace ParserWEB
{
    public class Program
    {
        static void Main(string[] args)
        {
            var consoleInput = new ConsoleHandler();
            var searchQuery = consoleInput.InputConsole("Введите тему для поиска фотографии:");

            var sourceForSearch = "https://yandex.ru/images/";
            var yandexParser = new Parser();
            List<string> imgFiles = yandexParser.spiderMain(searchQuery, sourceForSearch);

            var savePDF = new PDFHandler();
            savePDF.SavePDF(imgFiles, searchQuery);

            DeleteTempImg(imgFiles);

        }

        private static void DeleteTempImg(List<string> imageFiles)
        {
            foreach (string file in imageFiles)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                else { Console.WriteLine($"Не могу получить доступ к файлу {file}"); }
            }
        }
    }
}
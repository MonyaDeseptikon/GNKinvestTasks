using System;
using System.Collections.Generic;
using System.Text;

namespace ParserWEB
{
    public class ConsoleHandler
    {
        public string InputConsole()
        {
            string? input = null;

            while (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Введите тему для поиска фотографии\n");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Вы ничего не ввели, попробуйте еще раз.\n");
                }
                else
                {
                    Console.WriteLine($"Выбранная вами тема для поиска фото: {input}\n");
                }
            }
            return input;
        }

        private bool InputCheck(string input)
        {
            return string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input);

        }
    }
}

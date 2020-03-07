using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleFile
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var text = ReadFile();

            Console.WriteLine("Кол-во предложений");
            Console.Out.WriteLine(CountSentence(text));
            Console.WriteLine("Кол-во слов");
            Console.Out.WriteLine(CountWord(text));

            Console.WriteLine("Реверс строки");
            var sentenceText = SentenceSplitting(text);
            Console.Out.WriteLine(ReverseSentence(sentenceText, 3));

            var sortedText = SortText(text);
            if (WriteFile(sortedText))
            {
                Console.WriteLine("Отсортированный текст записан в файл");
            }
        }

        private static string ReverseSentence(string[] masText,int index)
        {
            var result = "";
            if (masText.Length > index - 1 && index > 0){
                result = new string(masText[index-1].ToCharArray().Reverse().ToArray());
            }
            else
            {
                result = $"Нет {index} предложения";
            }
            return result;
        }

        private static string ReadFile()
        {
            var result = "";
            try
            {
                var text = new StreamReader("textSample.txt");
                while (text.EndOfStream != true)
                {
                    result += text.ReadLine();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        private static bool WriteFile(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;
            try
            {
                using (var sw = new StreamWriter("textOutput.txt", false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private static string SortText(string text)
        {
            var mas = WordSplitting(text);
            Array.Sort(mas);
            var result= mas.Aggregate("", (current, res) => current + (res + " "));
            return result.Trim();
        }

        private static string[] WordSplitting(string text)
        {
            const string pattern = @"[^a-zA-Z0-9_'’]+";
            var result = Regex.Split(text, pattern)
                .Where(s => !string.IsNullOrEmpty(s)).ToArray(); ;
            return result;
        }

        private static string[] SentenceSplitting(string text)
        {
            const string pattern = @"[\?\.!]";
            var result = Regex.Split(text.Trim(), pattern)
                .Where(s => !string.IsNullOrEmpty(s)).ToArray();
            return result;
        }

        private static int CountWord(string text)
        {
            var result = WordSplitting(text).Length;
            return result;
        }

        private static int CountSentence(string text)
        {
            var result = SentenceSplitting(text).Length;
            return result;
        }

    }
}

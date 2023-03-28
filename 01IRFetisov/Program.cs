using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Text;
using static System.Text.CodePagesEncodingProvider;

namespace FetisovDictionary
{
    class Fetisov01Dictionary
    {
        static void Main(string[] args)
        {
            int counterCollectionWords = 0;
            long collectionSize = 0;
            long dictionarySize;

            string path = "C:\\Users\\PC\\source\\repos\\01IRFetisov\\01IRFetisov\\Collections\\";
            string dictPath = "C:\\Users\\PC\\source\\repos\\01IRFetisov\\01IRFetisov\\Dictionaty.txt";

            HashSet<string> hs = new HashSet<string>();

            char[] delimiterChars = { ' ', ' ', ',', '.', '—', '-', '–', '?', ';', ':', ')', '(', '[', ']', '!', '_', '=', '1', '2',
                    '3', '4', '5', '6', '7', '8', '9', '0', '\"', '\'', '«', '»', '…', '°', '$', '\t' };

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");

            FileStream fs = File.Create(dictPath);
            fs.Close();

            string[] allfiles = Directory.GetFiles(path);

            foreach (var file in allfiles)
            {
                var size = new System.IO.FileInfo(file);
                collectionSize += size.Length;

                using (StreamReader sr = new StreamReader(file, win1251))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] words = line.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var word in words)
                        {
                            counterCollectionWords++;
                            hs.Add(word.ToLower());
                        }
                    }
                }

                using (StreamWriter sw = new StreamWriter(dictPath))
                {
                    foreach (var word in hs)
                    {
                        sw.WriteLine(word);
                    }
                }
            }

            var dSize = new System.IO.FileInfo(dictPath);
            dictionarySize = dSize.Length;

            DirectoryInfo collInfo = new DirectoryInfo(path);
            DirectoryInfo dictInfo = new DirectoryInfo(dictPath);

            Console.WriteLine("The total number of words in the collection: " + counterCollectionWords);
            Console.WriteLine("Collection size(bytes): " + collectionSize);
            Console.WriteLine("Dictionary size(bytes): " + dictionarySize);
            Console.WriteLine("\nDictionary was created in:\n" + dictInfo.FullName);
            Console.WriteLine("\nCollection is located in:\n" + collInfo.FullName + "\n\n");

        }
    }
}

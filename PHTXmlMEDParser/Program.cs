using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PHTXmlMEDParser
{
    class Program
    {
        static int TotalLinesProcessed = 0;
        static int TotalWordsProccessed = 0;
        static int TotalPhrasesProccessed = 0;
        static int TotalDuplicates = 0;

        
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;

            string fileName = @"C:\Users\brianblo\Documents\Visual Studio 2012\Projects\PHTXmlMEDParser\Middle English Dictionary\Middle English Dictionary Additions.htm";
            string directoryName = @"C:\Users\brianblo\Documents\Visual Studio 2012\Projects\PHTXmlMEDParser\Middle English Dictionary";

//            ProcessFile(fileName, 2, "//b[@class=\"entry\"]");

            ProcessDirectory(directoryName, 2, "//b[@class=\"entry\"]");
//            ProcessDirectory(@"C:\Users\brianblo\Documents\Visual Studio 2012\Projects\PHTXmlMEDParser\Middle English Vocabulary",2, "LI");

            TimeSpan duration = DateTime.Now.Subtract(start);

            Console.WriteLine("");
            Console.WriteLine("Duration=" + duration);
            Console.WriteLine("");
            Console.WriteLine("TotalLines: " + TotalLinesProcessed);
            Console.WriteLine("TotalWords: " + TotalWordsProccessed);
            Console.WriteLine("TotalPhrases: " + TotalPhrasesProccessed);
            Console.WriteLine("TotalDuplicates: " + TotalDuplicates);
            Console.WriteLine("");
            Console.WriteLine("Press return to end.");

            // Suspend the screen.
            Console.ReadLine();
        }

        static void ProcessDirectory(string path, int domain, string xslSelector)
        {
            string[] files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                ProcessFile(file, domain, xslSelector);
            }
        }

        static void ProcessFile(string fileName, int domain, string xslSelector)
        {
            int lineCounter = 0;
            int wordCounter = 0;
            int phraseCounter = 0;
            int duplicateCount = 0;

            Console.WriteLine("Processing File: " + fileName);

            HtmlDocument doc = new HtmlDocument();
            doc.Load(fileName);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xslSelector);

            foreach (var node in nodes)
            {
                string[] lines = HttpUtility.HtmlDecode(node.InnerText).Replace("\r", "").Replace("\n", "").Replace("\t", " ").Split(',');
                foreach(var l in lines)
                {
                    lineCounter++;

                    var line = l.Trim();

                    if (line.Length > 0)
                    {
                        if (line.Contains(' '))
                        {
                            if (PHTWords.PHTWords.AddWord(line, domain))
                            {
                                phraseCounter++;
                            }
                            else
                            {
                                duplicateCount++;
                            }

                            string[] words = line.Split(' ');

                            foreach (var word in words)
                            {
                                if (PHTWords.PHTWords.AddWord(line, domain))
                                {
                                    wordCounter++;
                                }
                                else
                                {
                                    duplicateCount++;
                                }
                            }
                        }
                        else
                        {
                            if (PHTWords.PHTWords.AddWord(line, domain))
                            {
                                wordCounter++;
                            }
                            else
                            {
                                duplicateCount++;
                            }
                        }
                    }

                    if (lineCounter % 100 == 0)
                    {
                        Console.Write(".");
                    }
                }

            }

            TotalLinesProcessed += lineCounter;
            TotalWordsProccessed += wordCounter;
            TotalPhrasesProccessed += phraseCounter;
            TotalDuplicates += duplicateCount;

            Console.WriteLine("");
            Console.WriteLine("Lines: " + lineCounter);
            Console.WriteLine("Words: " + wordCounter);
            Console.WriteLine("Phrases: " + phraseCounter);
            Console.WriteLine("Duplicates: " + duplicateCount);
            Console.WriteLine("");
        }
    }
}

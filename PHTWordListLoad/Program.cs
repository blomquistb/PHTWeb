using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PHTWords;
using System.IO;
using System.Web;

namespace PHTWordListLoad
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

            ProccessCMUDict(@"..\..\WordSources\cmudict-0.7b.txt", 1);          // 2 hours to load            
            ProccessCMUDict(@"..\..\WordSources\cmudict_brian_adds.txt", 1);


            ProcessFile(@"..\..\WordSources\wordlist.txt", 1);  // 23 minutes

            ProcessDirectory(@"..\..\WordSources\scow-20150518-words", 1);    // 5 hours 50 minutes

            ProcessFile(@"..\..\WordSources\UKACD17.txt", 1);

            ProcessDirectory(@"..\..\WordSources\Categories", 1);   // 3 minutes


            ProccessBNCFreqFile(@"..\..\WordSources\bnc-corpus-freq-list.txt", 1);  // 51 minutes


            ProcessXMLDirectory(@"..\..\WordSources\Middle English\Middle English Dictionary", 2, "//b[@class=\"entry\"]"); // 7 minutes

            // Total load into Azure took 8 hours and 12 minutes

            WriteLine("");
            WriteLine("TotalLines: " + TotalLinesProcessed);
            WriteLine("TotalWords: " + TotalWordsProccessed);
            WriteLine("TotalPhrases: " + TotalPhrasesProccessed);
            WriteLine("TotalDuplicates: " + TotalDuplicates);

            WriteLine("");
            WriteLine("Updating Statistics...");
            PHTWords.PHTWords.UpdateStatistics();

            WriteLine("");
            WriteLine("Total Duration=" + DateTime.Now.Subtract(start));

            // Suspend the screen.
            WriteLine("");
            WriteLine("Press return to end.");
            Console.ReadLine();
        }

        static Regex CMUWordVariation = new Regex(@".+\(\d\)", RegexOptions.Compiled);
        static void ProccessCMUDict(string fileName, int domain)
        {
            DateTime sectionStart = DateTime.Now;

            int lineCounter = 0;
            int wordCounter = 0;
            int phraseCounter = 0;
            int duplicateCount = 0;

            string line;

            WriteLine("Processing CMU Pronuciation Dictionary File: " + fileName);

            using (System.IO.StreamReader file = new System.IO.StreamReader(fileName, Encoding.GetEncoding(1252), true))
            {
                while ((line = file.ReadLine()) != null)
                {
                    lineCounter++;

                    line = line.Trim();

                    if ((line.Length > 0) && !line.StartsWith(";;"))
                    {
                        if (!char.IsLetterOrDigit(line[0]))
                        {
                            // Skip punctuation pronunciation values.
                        }
                        else
                        {
                            var values = line.Split(new string[] { "  " }, StringSplitOptions.None);

                            if (values.Length != 2)
                            {
                                WriteLine(String.Format("Bad Entry({0}): {1}", lineCounter, line));
                            }
                            else
                            {
                                // Remove any (#) value indicating a pronunciation variation from the end of the word
                                var word = values[0];
                                if (CMUWordVariation.IsMatch(word))
                                {
                                    word = word.Substring(0, word.Length - 3);
                                }

                                // Convert the pronunciation to the simple ARPA bet phonemeset with consistent deviders and spacing.
                                var pronunciation = values[1];
                                var phonems = pronunciation.Split(' ');
                                for (int i = 0; i < phonems.Length; i++)
                                {
                                    if (PHTWords.PHTWords.Digits.Contains(phonems[i][phonems[i].Length - 1]))
                                    {
                                        phonems[i] = phonems[i].Substring(0, phonems[i].Length - 1);
                                    }

                                    if (phonems[i].Length < 2)
                                    {
                                        phonems[i] = phonems[i] + " ";
                                    }
                                }
                                pronunciation = string.Join("|", phonems) + "|";

                                ProcessWord(word, ref wordCounter, ref phraseCounter, ref duplicateCount, domain);

                                PHTWords.PHTWords.AddPronunciation(word, pronunciation);
                            }
                        }

                        if (lineCounter % 1000 == 0)
                        {
                            Console.Write(".");
                        }
                    }
                }
            }

            TotalLinesProcessed += lineCounter;
            TotalWordsProccessed += wordCounter;
            TotalPhrasesProccessed += phraseCounter;
            TotalDuplicates += duplicateCount;

            WriteLine("");
            WriteLine("Lines: " + lineCounter);
            WriteLine("Words: " + wordCounter);
            WriteLine("Phrases: " + phraseCounter);
            WriteLine("Duplicates: " + duplicateCount);
            WriteLine("");
            WriteLine("Duration=" + DateTime.Now.Subtract(sectionStart));
            WriteLine("");
        }

        static void ProcessDirectory(string path, int domain)
        {
            DateTime sectionStart = DateTime.Now;

            WriteLine("Processing Directory: " + path);

            string[] files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                ProcessFile(file, domain);
            }

            WriteLine("Directory Duration=" + DateTime.Now.Subtract(sectionStart));
            WriteLine("");
        }

        static void ProcessFile(string fileName, int domain)
        {
            DateTime sectionStart = DateTime.Now;

            int lineCounter = 0;
            int wordCounter = 0;
            int phraseCounter = 0;
            int duplicateCount = 0;

            string line;

            WriteLine("Processing File: " + fileName);

            using (System.IO.StreamReader file = new System.IO.StreamReader(fileName,Encoding.GetEncoding(1252), true))
            {
                while ((line = file.ReadLine()) != null)
                {
                    lineCounter++;

                    line = line.Trim();

                    if ((line.Length > 0) && !line.StartsWith(";;"))
                    {
                        ProcessWord(line, ref wordCounter, ref phraseCounter, ref duplicateCount, domain);
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

            WriteLine("");
            WriteLine("Lines: " + lineCounter);
            WriteLine("Words: " + wordCounter);
            WriteLine("Phrases: " + phraseCounter);
            WriteLine("Duplicates: " + duplicateCount);
            WriteLine("");
            WriteLine("Duration=" + DateTime.Now.Subtract(sectionStart));
            WriteLine("");
        }

        static void ProcessXMLDirectory(string path, int domain, string xslSelector)
        {
            DateTime sectionStart = DateTime.Now;

            string[] files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                ProcessXMLFile(file, domain, xslSelector);
            }

            WriteLine("Directory Duration=" + DateTime.Now.Subtract(sectionStart));
            WriteLine("");
        }

        static void ProcessXMLFile(string fileName, int domain, string xslSelector)
        {
            DateTime sectionStart = DateTime.Now;

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
                foreach (var l in lines)
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
            WriteLine("Duration=" + DateTime.Now.Subtract(sectionStart));
            WriteLine("");
        }

        static void ProcessWord(string word, ref int wordCounter, ref int phraseCounter, ref int duplicateCount, int domain)
        {
            int index = word.IndexOf('|');
            string written_out = "";
            if (index > -1) {
                written_out = word.Substring(index + 1);
                word = word.Substring(0, index);
            }

            if (word.Contains(' '))
            {
                ProcessCompoundWord(word, " ", ref wordCounter, ref phraseCounter, ref duplicateCount, domain);
            }
            else if (word.Contains('-'))
            {
                ProcessCompoundWord(word, "-", ref wordCounter, ref phraseCounter, ref duplicateCount, domain);
            }
            else
            {
                if (PHTWords.PHTWords.AddWord(word, domain))
                {
                    wordCounter++;
                }
                else
                {
                    duplicateCount++;
                }
            }
        }

        static void ProcessCompoundWord(string word, string seperator, ref int wordCounter, ref int phraseCounter, ref int duplicateCount, int domain)
        {
            // Make sure all the components of a compound word or phrase exist as seperate components in the Dictionary.
            //
            string[] sub_words = word.Split(new string[] { seperator }, StringSplitOptions.None);

            foreach (var sub_word in sub_words)
            {
                ProcessWord(sub_word, ref wordCounter, ref phraseCounter, ref duplicateCount, domain);
            }
            
            if (!seperator.Equals(" ")) // don't add phrases or full names with spaces but do add hyphenated words.
            {
                PHTWords.PHTWords.AddWord(word, domain);

                if (PHTWords.PHTWords.AddWord(word, domain))
                {
                    phraseCounter++;
                }
                else
                {
                    duplicateCount++;
                }


                // TODO: when we add compound words then try and calculate the pronunciation of them if we can.
                //string pronunciation = PHTWords.PHTWords.GetPronunciation(word, seperator);
                //if (!string.IsNullOrEmpty(pronunciation))
                //{
                //    PHTWords.PHTWords.AddPronunciation(word, pronunciation);
                //}
            }
            
        }

        static void ProccessBNCFreqFile(string fileName, int domain)
        {
            int lineCounter = 0;
            int wordCounter = 0;
            int duplicateCount = 0;

            string line;

            WriteLine("Processing BNC Freq File: " + fileName);

            using (System.IO.StreamReader file = new System.IO.StreamReader(fileName, Encoding.GetEncoding(1252), true))
            {
                PHTWords.PHTWords.ResetFrequencies(domain);

                while ((line = file.ReadLine()) != null)
                {
                    lineCounter++;
                    line = line.Trim();

                    var values = line.Split(' ');

                    if (values.Length != 4)
                    {
                        WriteLine(String.Format("Bad Entry({0}): {1}", lineCounter, line));
                    }
                    else
                    {
                        if (values[1].Equals(PHTWords.PHTWords.TrimPunctuation(values[1])) && PHTWords.PHTWords.AddFrequencyCount(values[1], domain, int.Parse(values[0])))
                        {
                            wordCounter++;
                        }
                        else
                        {
                            duplicateCount++;
                        }
                    }

                    if (lineCounter % 1000 == 0)
                    {
                        Console.Write(".");
                    }
                }
            }

            WriteLine("");
            WriteLine("Lines: " + lineCounter);
            WriteLine("Words: " + wordCounter);
            WriteLine("Ignored: " + duplicateCount);
            WriteLine("");
        }

        public static void WriteLine(string line)
        {
            Console.WriteLine(line);
            System.Diagnostics.Debug.WriteLine(line);
        }

    }
}

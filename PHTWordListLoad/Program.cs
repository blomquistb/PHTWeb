using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PHTWords;
using System.IO;

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

            // Single Test Filed: ProcessFile(@"C:\Users\brianblo\Documents\Visual Studio 2012\Projects\PHTWordListLoad\WordSources\scow-20150518-words\american-abbreviations.70", 1);
            //ProccessCMUDict(@"C:\Users\brianblo\Documents\Visual Studio 2012\Projects\PHTWordListLoad\cmudict_test.txt", 1);

            ProccessCMUDict(@"C:\Users\brianblo\Documents\Visual Studio 2012\Projects\PHTWordListLoad\cmudict_brian_adds.txt", 1);
            ProccessCMUDict(@"C:\Users\brianblo\Documents\Visual Studio 2012\Projects\PHTWordListLoad\cmudict-0.7b.txt", 1);
            /*

            ProcessDirectory(@"C:\Users\brianblo\Documents\Visual Studio 2012\Projects\PHTWordListLoad\WordSources\scow-20150518-words", 1);

            ProcessDirectory(@"C:\Users\brianblo\Documents\Visual Studio 2012\Projects\PHTWordListLoad\WordSources\scow-20150518-words", 1);

            ProcessFile(@"C:\Users\brianblo\Documents\Visual Studio 2012\Projects\PHTWordListLoad\WordSources\wordlist.txt", 1);

            ProcessFile(@"C:\Users\brianblo\Documents\Visual Studio 2012\Projects\PHTWordListLoad\WordSources\UKACD17.txt", 1);

            ProcessFile(@"C:\Users\brianblo\Documents\Visual Studio 2012\Projects\PHTWordListLoad\WordSources\AliceInWonderland.txt", 1);

            ProcessFile(@"C:\Users\brianblo\Documents\Visual Studio 2012\Projects\PHTWordListLoad\WordSources\BrianCutomWords.txt", 1);
            
            ProccessBNCFreqFile(@"C:\Users\brianblo\Documents\Visual Studio 2012\Projects\PHTWordListLoad\WordSources\bnc-corpus-freq-list.txt", 1);
            */


            TimeSpan duration = DateTime.Now.Subtract(start);

            WriteLine("");
            WriteLine("Duration=" + duration);

            // Suspend the screen.
            WriteLine("");
            WriteLine("Press return to end.");
            Console.ReadLine();
        }

        static Regex CMUWordVariation = new Regex(@".+\(\d\)", RegexOptions.Compiled);
        static void ProccessCMUDict(string fileName, int domain)
        {
            int lineCounter = 0;

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

                                //ProcessWord(word, ref wordCounter, ref phraseCounter, ref duplicateCount, domain, pronunciation);

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

            WriteLine("");
            WriteLine("Lines: " + lineCounter);
            WriteLine("");
        }

        static void ProcessDirectory(string path, int domain)
        {
            WriteLine("Processing Directory: " + path);

            string[] files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                ProcessFile(file, domain);
            }

            WriteLine("");
            WriteLine("TotalLines: " + TotalLinesProcessed);
            WriteLine("TotalWords: " + TotalWordsProccessed);
            WriteLine("TotalPhrases: " + TotalPhrasesProccessed);
            WriteLine("TotalDuplicates: " + TotalDuplicates);
        }

        static void ProcessFile(string fileName, int domain)
        {
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
        }

        static void ProcessWord(string word, ref int wordCounter, ref int phraseCounter, ref int duplicateCount, int domain)
        {
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
            
            if (PHTWords.PHTWords.AddWord(word, domain))
            {
                phraseCounter++;
            }
            else
            {
                duplicateCount++;
            }

            // TODO: leaving compound words out for now.
            //PHTWords.PHTWords.AddWord(word, domain);
            
            // TODO: when we add compound words then try and calculate the pronunciation of them if we can.
            //string pronunciation = PHTWords.PHTWords.GetPronunciation(word, seperator);
            //if (!string.IsNullOrEmpty(pronunciation))
            //{
            //    PHTWords.PHTWords.AddPronunciation(word, pronunciation);
            //}
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

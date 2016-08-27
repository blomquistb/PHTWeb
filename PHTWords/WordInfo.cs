using System;

namespace PHTWords
{
    public class WordInfo
    {
        public WordInfo(string value, int frequency, string pronunciation = "")
        {
            this.value = value;
            this.frequency = frequency;
            this.pronunciation = pronunciation;
        }

        public string value;
        public int frequency;
        public string pronunciation;
    }
}

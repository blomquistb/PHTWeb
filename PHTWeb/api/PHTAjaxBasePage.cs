using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHTWeb
{
    public class PHTAjaxBasePage : System.Web.UI.Page
    {
        private int[] dictionaries = null;
        private int minFrequency = -1;
        private int maxResults = -1;
        private int minWordLength = -1;
        private int maxWordLength = -1;


        public int[] Dictionaries
        {
            get
            {
                if (this.dictionaries == null)
                {
                    List<int> dictionaries = new List<int>();
                    string dictionariesStr = Request.Params["dictionaries"];
                    if (!String.IsNullOrEmpty(dictionariesStr))
                    {
                        for (int i = 0; i < dictionariesStr.Length; i++)
                        {
                            int dictionary = 0;
                            if (int.TryParse(dictionariesStr.Substring(i, 1), out dictionary))
                            {
                                dictionaries.Add(dictionary);
                            }
                        }
                    }

                    this.dictionaries = dictionaries.ToArray();
                }

                return this.dictionaries;
            }
        }

        public int MaxResults
        {
            get
            {
                if (this.maxResults < 0)
                {
                    int maxResults;
                    if (int.TryParse(Request.Params["maxResults"], out maxResults))
                    {
                        this.maxResults = maxResults;
                    }

                    if (this.maxResults < 1)
                    {
                        this.maxResults = 25;
                    }
                }

                return this.maxResults;
            }
        }

        public int MinFrequency
        {
            get
            {
                if (this.minFrequency < 0)
                {
                    int minFrequency;
                    if (int.TryParse(Request.Params["minFrequency"], out minFrequency))
                    {
                        this.minFrequency = minFrequency;
                    }

                    if (this.minFrequency < 0)
                    {
                        this.minFrequency = 0;
                    }
                }

                return this.minFrequency;
            }
        }

        public int MinWordLength
        {
            get
            {
                if (this.minWordLength < 0)
                {
                    int minWordLength;
                    if (int.TryParse(Request.Params["minWordLength"], out minWordLength))
                    {
                        this.minWordLength = minWordLength;
                    }

                    if (this.minWordLength < 0)
                    {
                        this.minWordLength = 0;
                    }
                }

                return this.minWordLength;
            }
        }

        public int MaxWordLength
        {
            get
            {
                if (this.maxWordLength < 0)
                {
                    int maxWordLength;
                    if (int.TryParse(Request.Params["maxWordLength"], out maxWordLength))
                    {
                        this.maxWordLength = maxWordLength;
                    }

                    if (this.maxWordLength < 0)
                    {
                        this.maxWordLength = 0;
                    }
                }

                return this.maxWordLength;
            }
        }

        public bool GetPronunciation
        {
            get
            {
                bool result = false;
                if (bool.TryParse(Request.Params["getPronunciation"], out result))
                {
                    return result;
                }

                return false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            Response.ContentType = "text/json";

            this.DisableClientCaching();

            base.OnLoad(e);
        }

        private void DisableClientCaching()
        {
            // Do any of these result in META tags e.g. <META HTTP-EQUIV="Expire" CONTENT="-1">
            // HTTP Headers or both?

            // Does this only work for IE?
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            // Is this required for FireFox? Would be good to do this without magic strings.
            // Won't it overwrite the previous setting
            Response.Headers.Add("Cache-Control", "no-cache, no-store");

            // Why is it necessary to explicitly call SetExpires. Presume it is still better than calling
            // Response.Headers.Add( directly
            Response.Cache.SetExpires(DateTime.UtcNow.AddYears(-1));
        }
    }
}
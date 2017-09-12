using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PHTWords;

namespace PHTWeb
{
    public partial class GetWordMatches : PHTAjaxBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<WordInfo> result = PHTWords.PHTWords.GetWordMatches(
                Request.Params["wordPatterns"],
                Request.Params["anagramPatterns"],
                Request.Params["cryptogramPatterns"],
                Request.Params["cryptoAnagramPatterns"],
                Request.Params["phoneticPatterns"],
                this.Dictionaries, 
                this.MaxResults, 
                this.MinFrequency, 
                this.MinWordLength, 
                this.MaxWordLength,
                this.GetPronunciation);

            Response.Write(JsonConvert.SerializeObject(result));
        }
    }
}
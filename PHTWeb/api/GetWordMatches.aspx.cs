﻿using Newtonsoft.Json;
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
                Request.QueryString["wordPatterns"],
                Request.QueryString["anagramPatterns"],
                Request.QueryString["cryptogramPatterns"],
                Request.QueryString["cryptoAnagramPatterns"],
                Request.QueryString["phoneticPatterns"],
                this.Dictionaries, 
                this.MaxResults, 
                this.MinFrequency, 
                this.MinWordLength, 
                this.MaxWordLength);

            Response.Write(JsonConvert.SerializeObject(result));
        }
    }
}
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PHTWeb.api
{
    public partial class GetRunners : PSTBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<PuzzleRunner> result = new List<PuzzleRunner>();

            result = GetRunners();

            Response.Write(JsonConvert.SerializeObject(result));
        }
    }
}
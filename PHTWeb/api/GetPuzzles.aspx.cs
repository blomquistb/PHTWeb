using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PHTWeb.api
{
    public partial class GetPuzzles : PSTBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<PuzzleInfo> result = new List<PuzzleInfo>();

            result = GetPuzzles();

            Response.Write(JsonConvert.SerializeObject(result));
        }
    }
}
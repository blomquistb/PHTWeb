using System;
using Newtonsoft.Json;

namespace PHTWeb.api
{
    public partial class SetRetrievedPuzzle : PSTBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetRetrievedPuzzle(Request.Params["id"], bool.Parse(Request.Params["retrieved"]));

            Response.Write(JsonConvert.SerializeObject(Request.Params["id"]));
        }
    }
}
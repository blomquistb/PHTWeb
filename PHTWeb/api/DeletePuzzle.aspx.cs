using System;
using Newtonsoft.Json;

namespace PHTWeb.api
{
    public partial class DeletePuzzle : PSTBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DeletePuzzle(Request.Params["id"]);

            Response.Write(JsonConvert.SerializeObject(Request.Params["id"]));
        }
    }
}
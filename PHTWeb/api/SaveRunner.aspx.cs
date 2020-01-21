using System;
using Newtonsoft.Json;

namespace PHTWeb.api
{
    public partial class SaveRunner : PSTBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PuzzleRunner result = new PuzzleRunner()
            {
                name = Request.Params["name"],
                color = Request.Params["color"],
                latitude = double.Parse(Request.Params["latitude"]),
                longitude = double.Parse(Request.Params["longitude"]),
            };

            SaveRunner(result);

            Response.Write(JsonConvert.SerializeObject(result));
        }
    }
}
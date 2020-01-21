using System;
using Newtonsoft.Json;

namespace PHTWeb.api
{
    public partial class SavePuzzle : PSTBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PuzzleInfo result = new PuzzleInfo() {
                type = Request.Params["type"],
                name = Request.Params["name"],
                location = Request.Params["location"],
                room = Request.Params["room"],
                notes = Request.Params["notes"],
            };

            // insert / update the puzzle information
            string id = Request.Params["id"];
            if (String.IsNullOrEmpty(id))
            {
                InsertPuzzle(result);
            }
            else
            {
                result.id = id;
                UpdatePuzzle(result);
            }

            Response.Write(JsonConvert.SerializeObject(result));
        }
    }
}
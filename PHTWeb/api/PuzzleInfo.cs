using System;

namespace PHTWeb.api
{
    public class PuzzleInfo
    {
        public PuzzleInfo()
        {
            id = Guid.NewGuid().ToString();
        }

        public string id;
        public string name;
        public string type;
        public string location;
        public string room;
        public string notes;
        public bool retrieved;
    }
}
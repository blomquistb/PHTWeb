using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;


namespace PHTWeb.api
{
    public class PSTBasePage : System.Web.UI.Page
    {
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

        private static SqlConnection GetConnection()
        {
            //SqlConnection result = new SqlConnection(@"Data Source=.; Initial Catalog=pht; Integrated Security=True");
            SqlConnection result = new SqlConnection(@"Server=tcp:pht.database.windows.net,1433;Initial Catalog=PHT;Persist Security Info=False;User ID=pht_user;Password=puzzleData1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            result.Open();

            return result;
        }
        private static void ReleaseConnection(SqlConnection conn)
        {
            conn.Close();
        }


        public List<PuzzleRunner> GetRunners()
        {
            List<PuzzleRunner> results = new List<PuzzleRunner>();

            var conn = GetConnection();

            try
            {
                string selectSql = @"SELECT name, color, latitude, longitude FROM PuzzleRunners WHERE DATEADD(minute, -5, SYSUTCDATETIME()) < age";
                SqlCommand selectCmd = new SqlCommand(selectSql, conn);

                SqlDataReader reader = selectCmd.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(new PuzzleRunner()
                    {
                        name = (string)reader["name"],
                        color = (string)reader["color"],
                        latitude = (double)reader["latitude"],
                        longitude = (double)reader["longitude"],
                    });
                }
            }
            finally
            {
                ReleaseConnection(conn);
            }

            return results;
        }

        public void SaveRunner(PuzzleRunner r)
        {
            var conn = GetConnection();

            try
            {
                string deleteSql = @"DELETE FROM PuzzleRunners WHERE name=@name";
                SqlCommand deleteCmd = new SqlCommand(deleteSql, conn);
                deleteCmd.Parameters.Add("@name", SqlDbType.NVarChar);
                deleteCmd.Parameters["@name"].Value = r.name;
                deleteCmd.ExecuteNonQuery();

                string insertSql = @"INSERT INTO PuzzleRunners (name, color, latitude, longitude) values (@name, @color, @latitude, @longitude)";
                SqlCommand insertCmd = new SqlCommand(insertSql, conn);
                insertCmd.Parameters.Add("@name", SqlDbType.NVarChar);
                insertCmd.Parameters.Add("@color", SqlDbType.NVarChar);
                insertCmd.Parameters.Add("@latitude", SqlDbType.Float);
                insertCmd.Parameters.Add("@longitude", SqlDbType.Float);

                insertCmd.Parameters["@name"].Value = r.name;
                insertCmd.Parameters["@color"].Value = r.color;
                insertCmd.Parameters["@latitude"].Value = r.latitude;
                insertCmd.Parameters["@longitude"].Value = r.longitude;

                insertCmd.ExecuteNonQuery();
            }
            finally
            {
                ReleaseConnection(conn);
            }
        }

        public List<PuzzleInfo> GetPuzzles()
        {
            List<PuzzleInfo> results = new List<PuzzleInfo>();

            var conn = GetConnection();

            try
            {
                string selectSql = @"SELECT id, type, name, location, room, notes, retrieved FROM PuzzleInfo";
                SqlCommand selectCmd = new SqlCommand(selectSql, conn);

                SqlDataReader reader = selectCmd.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(new PuzzleInfo()
                    {
                        id = (string)reader["id"],
                        type = (string)reader["type"],
                        name = (string)reader["name"],
                        location = (string)reader["location"],
                        room = (string)reader["room"],
                        notes = (string)reader["notes"],
                        retrieved = (bool)reader["retrieved"],
                    });
                }
            }
            finally
            {
                ReleaseConnection(conn);
            }

            return results;
        }

        public void InsertPuzzle(PuzzleInfo p)
        {
            var conn = GetConnection();

            try
            {
                string insertSql = @"INSERT INTO PuzzleInfo (id, type, name, location, room, notes) values (@id, @type, @name, @location, @room, @notes)";
                SqlCommand insertCmd = new SqlCommand(insertSql, conn);
                insertCmd.Parameters.Add("@id", SqlDbType.NVarChar);
                insertCmd.Parameters.Add("@type", SqlDbType.NVarChar);
                insertCmd.Parameters.Add("@name", SqlDbType.NVarChar);
                insertCmd.Parameters.Add("@location", SqlDbType.NVarChar);
                insertCmd.Parameters.Add("@room", SqlDbType.NVarChar);
                insertCmd.Parameters.Add("@notes", SqlDbType.NVarChar);

                insertCmd.Parameters["@id"].Value = p.id;
                insertCmd.Parameters["@type"].Value = p.type;
                insertCmd.Parameters["@name"].Value = p.name;
                insertCmd.Parameters["@location"].Value = p.location;
                insertCmd.Parameters["@room"].Value = p.room;
                insertCmd.Parameters["@notes"].Value = p.notes;

                insertCmd.ExecuteNonQuery();
            }
            finally
            {
                ReleaseConnection(conn);
            }
        }

        public void UpdatePuzzle(PuzzleInfo p)
        {
            var conn = GetConnection();

            try
            {
                string updateSql = @"UPDATE PuzzleInfo SET type=@type, name=@name, location=@location, room=@room, notes=@notes WHERE id=@id";
                SqlCommand updateCmd = new SqlCommand(updateSql, conn);
                updateCmd.Parameters.Add("@id", SqlDbType.NVarChar);
                updateCmd.Parameters.Add("@type", SqlDbType.NVarChar);
                updateCmd.Parameters.Add("@name", SqlDbType.NVarChar);
                updateCmd.Parameters.Add("@location", SqlDbType.NVarChar);
                updateCmd.Parameters.Add("@room", SqlDbType.NVarChar);
                updateCmd.Parameters.Add("@notes", SqlDbType.NVarChar);

                updateCmd.Parameters["@id"].Value = p.id;
                updateCmd.Parameters["@type"].Value = p.type;
                updateCmd.Parameters["@name"].Value = p.name;
                updateCmd.Parameters["@location"].Value = p.location;
                updateCmd.Parameters["@room"].Value = p.room;
                updateCmd.Parameters["@notes"].Value = p.notes;

                updateCmd.ExecuteNonQuery();
            }
            finally
            {
                ReleaseConnection(conn);
            }
        }

        public void SetRetrievedPuzzle(string id, bool retrieved)
        {
            var conn = GetConnection();

            try
            {
                string updateSql = @"UPDATE PuzzleInfo SET retrieved=@retrieved WHERE id=@id";
                SqlCommand updateCmd = new SqlCommand(updateSql, conn);
                updateCmd.Parameters.Add("@id", SqlDbType.NVarChar);
                updateCmd.Parameters.Add("@retrieved", SqlDbType.Bit);

                updateCmd.Parameters["@id"].Value = id;
                updateCmd.Parameters["@retrieved"].Value = retrieved;

                updateCmd.ExecuteNonQuery();
            }
            finally
            {
                ReleaseConnection(conn);
            }
        }

        public void DeletePuzzle(string id)
        {
            var conn = GetConnection();

            try
            {
                string deleteSql = @"DELETE FROM PuzzleInfo WHERE id=@id";
                SqlCommand deleteCmd = new SqlCommand(deleteSql, conn);
                deleteCmd.Parameters.Add("@id", SqlDbType.NVarChar);

                deleteCmd.Parameters["@id"].Value = id;

                deleteCmd.ExecuteNonQuery();
            }
            finally
            {
                ReleaseConnection(conn);
            }
        }
    }
}
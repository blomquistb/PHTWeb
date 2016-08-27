using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHTWords
{
    public static class PHTWords
    {
        public static string Punctuation
        {
            get
            {
                const string punctuation = ";:.,!?[]{}()\"'“”‘’";
                return punctuation;
            }
        }

        public static string Digits
        {
            get
            {
                const string digits = "0123456789";
                return digits;
            }
        }

        public static string Whitespace
        {
            get
            {
                const string whitespace = " \f\n\r\t\v";
                return whitespace;
            }
        }

        public static bool AddWord(string word, int dictionary = 0)
        {
            if (!String.IsNullOrEmpty(word))
            {
                string wordIdx = GetWordIdx(word);

                var conn = GetConnection();
                try
                {
                    string selectSql = "SELECT word_idx FROM Dictionary WHERE word_idx=@word_idx";
                    if (dictionary != 0)
                    {
                        selectSql = selectSql + " AND domain=@domain";
                    }

                    SqlCommand selectCmd = new SqlCommand(selectSql, conn);
                    selectCmd.Parameters.Add("@word_idx", SqlDbType.NVarChar);
                    selectCmd.Parameters["@word_idx"].Value = wordIdx;
                    if (dictionary != 0)
                    {
                        selectCmd.Parameters.Add("@domain", SqlDbType.Int);
                        selectCmd.Parameters["@domain"].Value = dictionary;
                    }

                    if (selectCmd.ExecuteScalar() == null)
                    {
                        string insertSql = "INSERT INTO Dictionary (length, frequency, domain, word_idx, anagram_idx, crypto_idx, crypto_anagram_idx) values (@length, @frequency, @domain, @word_idx, @anagram_idx, @crypto_idx, @crypto_anagram_idx)";
                        SqlCommand insertCmd = new SqlCommand(insertSql, conn);
                        insertCmd.Parameters.Add("@length", SqlDbType.Int);
                        insertCmd.Parameters.Add("@frequency", SqlDbType.Int);
                        insertCmd.Parameters.Add("@domain", SqlDbType.Int);
                        insertCmd.Parameters.Add("@word_idx", SqlDbType.NVarChar);
                        insertCmd.Parameters.Add("@anagram_idx", SqlDbType.NVarChar);
                        insertCmd.Parameters.Add("@crypto_idx", SqlDbType.NVarChar);
                        insertCmd.Parameters.Add("@crypto_anagram_idx", SqlDbType.NVarChar);

                        insertCmd.Parameters["@length"].Value = wordIdx.Length;
                        insertCmd.Parameters["@frequency"].Value = 0;
                        insertCmd.Parameters["@domain"].Value = dictionary;
                        insertCmd.Parameters["@word_idx"].Value = wordIdx;
                        insertCmd.Parameters["@crypto_idx"].Value = GetCryptoIdx(wordIdx);
                        insertCmd.Parameters["@anagram_idx"].Value = GetAnagramIdx(wordIdx);
                        insertCmd.Parameters["@crypto_anagram_idx"].Value = GetCryptoAnagramIdx(wordIdx);
                        insertCmd.ExecuteNonQuery();

                        return true;
                    }
                }
                finally
                {
                    ReleaseConnection(conn);
                }
            }

            return false;
        }

        public static void ResetFrequencies(int dictionary)
        {
            var conn = GetConnection();
            try
            {
                string updateSql = "UPDATE Dictionary SET frequency=@frequency";
                if (dictionary != 0)
                {
                    updateSql = updateSql + " WHERE domain=@domain";
                }

                SqlCommand updateCmd = new SqlCommand(updateSql, conn);
                
                updateCmd.Parameters.Add("@frequency", SqlDbType.Int);
                updateCmd.Parameters["@frequency"].Value = 0;
                
                if (dictionary != 0)
                {
                    updateCmd.Parameters.Add("@domain", SqlDbType.Int);
                    updateCmd.Parameters["@domain"].Value = dictionary;
                }

                updateCmd.ExecuteNonQuery();
            }
            finally
            {
                ReleaseConnection(conn);
            }
        }

        public static bool AddFrequencyCount(string word, int dictionary, int count)
        {
            bool result = false;

            var conn = GetConnection();
            try
            {
                string updateSql = "UPDATE Dictionary SET frequency=frequency+@frequency WHERE word_idx=@word_idx";
                if (dictionary != 0)
                {
                    updateSql = updateSql + " AND domain=@domain";
                }

                SqlCommand updateCmd = new SqlCommand(updateSql, conn);
                updateCmd.Parameters.Add("@word_idx", SqlDbType.NVarChar);
                updateCmd.Parameters["@word_idx"].Value = GetWordIdx(word);

                updateCmd.Parameters.Add("@frequency", SqlDbType.Int);
                updateCmd.Parameters["@frequency"].Value = count;
                
                if (dictionary != 0)
                {
                    updateCmd.Parameters.Add("@domain", SqlDbType.Int);
                    updateCmd.Parameters["@domain"].Value = dictionary;
                }

                result = updateCmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                ReleaseConnection(conn);
            }

            return result;
        }

        public static void AddPronunciation(string word, string pronunciation)
        {
            string wordIdx = GetWordIdx(word);

            // Strip dashes and spaces and any other punctuation and if it is not the same word call myself recursivly.
            //
            string compactWord = RemoveCharaters(Punctuation + "- ", wordIdx);
            if (!wordIdx.Equals(compactWord))
            {
                AddPronunciation(compactWord, pronunciation);
            }

            var conn = GetConnection();
            try
            {
                string selectSql = "SELECT word_idx FROM Pronunciations WHERE word_idx=@word_idx and pronunciation=@pronunciation";

                SqlCommand selectCmd = new SqlCommand(selectSql, conn);
                selectCmd.Parameters.Add("@word_idx", SqlDbType.NVarChar);
                selectCmd.Parameters["@word_idx"].Value = wordIdx;
                selectCmd.Parameters.Add("@pronunciation", SqlDbType.NVarChar);
                selectCmd.Parameters["@pronunciation"].Value = pronunciation;

                if (selectCmd.ExecuteScalar() == null)
                {
                    string insertSql = "INSERT INTO Pronunciations (word_idx, pronunciation) values (@word_idx, @pronunciation)";
                    SqlCommand insertCmd = new SqlCommand(insertSql, conn);
                    insertCmd.Parameters.Add("@word_idx", SqlDbType.NVarChar);
                    insertCmd.Parameters.Add("@pronunciation", SqlDbType.NVarChar);

                    insertCmd.Parameters["@word_idx"].Value = wordIdx;
                    insertCmd.Parameters["@pronunciation"].Value = pronunciation;
                    insertCmd.ExecuteNonQuery();
                }
            }
            finally
            {
                ReleaseConnection(conn);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="exclusions"></param>
        /// <param name="domain"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public static List<WordInfo> GetWordMatches(string wordPatterns, string anagramPatterns, string cryptogramPatterns, string cryptoAnagramPatterns, string phoneticPatterns,
                                                    int[] dictionaries, int maxResults = 25, int minFrequency = 0, int minWordLength = 0, int maxWordLength = 0)
        {
            List<WordInfo> results = new List<WordInfo>();

            if (dictionaries == null)
            {
                dictionaries = new int[0];
            }

            var conn = GetConnection();
            try
            {
                bool isFirstExpression = true;
                List<string> patternValues = new List<string>();
                StringBuilder selectSql = new StringBuilder("SELECT DISTINCT TOP ").Append(maxResults).Append(" d.word_idx as word_idx, d.frequency as frequency FROM Dictionary d");

                if (!String.IsNullOrEmpty(phoneticPatterns))
                {
                    selectSql.Append(" left outer join Pronunciations p on p.word_idx = d.word_idx");
                    AppendPatterns(selectSql, isFirstExpression, "p.pronunciation", phoneticPatterns, patternValues);
                    isFirstExpression = false;
                }

                if (!String.IsNullOrEmpty(wordPatterns))
                {
                    AppendPatterns(selectSql, isFirstExpression, "d.word_idx", wordPatterns, patternValues);
                    isFirstExpression = false;
                }

                if (!String.IsNullOrEmpty(anagramPatterns))
                {
                    AppendPatterns(selectSql, isFirstExpression, "d.anagram_idx", anagramPatterns, patternValues);
                    isFirstExpression = false;
                }

                if (!String.IsNullOrEmpty(cryptogramPatterns))
                {
                    AppendPatterns(selectSql, isFirstExpression, "d.crypto_idx", cryptogramPatterns, patternValues);
                    isFirstExpression = false;
                }

                if (!String.IsNullOrEmpty(cryptoAnagramPatterns))
                {
                    AppendPatterns(selectSql, isFirstExpression, "d.crypto_anagram_idx", cryptoAnagramPatterns, patternValues);
                    isFirstExpression = false;
                }

                if ((dictionaries != null) && (dictionaries.Length != 0))
                {
                    if (isFirstExpression)
                    {
                        selectSql.Append(" WHERE ");
                        isFirstExpression = false;
                    }
                    else
                    {
                        selectSql.Append(" AND ");
                    }

                    selectSql.Append("d.domain IN (");
                    for (int i = 0; i < dictionaries.Length; i++)
                    {
                        if (i != 0)
                        {
                            selectSql.Append(",");
                        }
                        selectSql.Append(dictionaries[i]);
                    }
                    selectSql.Append(")");
                }


                if (minFrequency > 0)
                {
                    if (isFirstExpression)
                    {
                        selectSql.Append(" WHERE ");
                        isFirstExpression = false;
                    }
                    else
                    {
                        selectSql.Append(" AND ");
                    }

                    selectSql.Append("d.frequency >= @minFrequency");
                }

                if (minWordLength > 0)
                {
                    if (isFirstExpression)
                    {
                        selectSql.Append(" WHERE ");
                        isFirstExpression = false;
                    }
                    else
                    {
                        selectSql.Append(" AND ");
                    }

                    selectSql.Append("d.length >= @minWordLength");
                }

                if (maxWordLength > 0)
                {
                    if (isFirstExpression)
                    {
                        selectSql.Append(" WHERE ");
                        isFirstExpression = false;
                    }
                    else
                    {
                        selectSql.Append(" AND ");
                    }

                    selectSql.Append("d.length <= @maxWordLength");
                }

                selectSql.Append(" ORDER BY frequency DESC");

                SqlCommand selectCmd = new SqlCommand(selectSql.ToString(), conn);

                for (int i = 0; i < patternValues.Count; i++)
                {
                    selectCmd.Parameters.Add("@patternValue" + i, SqlDbType.NVarChar);
                    selectCmd.Parameters["@patternValue" + i].Value = patternValues[i].ToUpperInvariant();
                }

                if (minFrequency > 0)
                {
                    selectCmd.Parameters.Add("@minFrequency", SqlDbType.Int);
                    selectCmd.Parameters["@minFrequency"].Value = minFrequency;
                }

                if (minWordLength > 0)
                {
                    selectCmd.Parameters.Add("@minWordLength", SqlDbType.Int);
                    selectCmd.Parameters["@minWordLength"].Value = minWordLength;
                }

                if (maxWordLength > 0)
                {
                    selectCmd.Parameters.Add("@maxWordLength", SqlDbType.Int);
                    selectCmd.Parameters["@maxWordLength"].Value = maxWordLength;
                }

                SqlDataReader reader = selectCmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new WordInfo((string)reader["word_idx"], (int)reader["frequency"]));
                }
            }
            finally
            {
                ReleaseConnection(conn);
            }

            return results;
        }

        private static StringBuilder AppendPatterns(StringBuilder selectSql, bool isFirstExpression, string field, string patterns, List<string> patternValues)
        {
            if (isFirstExpression)
            {
                selectSql.Append(" WHERE (");
                isFirstExpression = false;
            }
            else
            {
                selectSql.Append(" AND (");
            }

            string[] andClauses = patterns.Split('&');

            for (int i = 0; i < andClauses.Length; i++)
            {
                if (i > 0)
                {
                    selectSql.Append(") AND (");
                }

                string[] orClauses = andClauses[i].Split('|');

                for (int j = 0; j < orClauses.Length; j++)
                {
                    if (j > 0)
                    {
                        selectSql = selectSql.Append(" OR ");
                    }

                    if (orClauses[j].StartsWith("!"))
                    {
                        selectSql.Append(field).Append(" NOT LIKE @patternValue").Append(patternValues.Count).Append(" ESCAPE '\\'");
                        patternValues.Add(orClauses[j].Substring(1));
                    }
                    else
                    {
                        selectSql.Append(field).Append(" LIKE @patternValue").Append(patternValues.Count).Append(" ESCAPE '\\'");
                        patternValues.Add(orClauses[j]);
                    }
                }
            }

            selectSql.Append(")");

            return selectSql;
        }

        /// <summary>
        /// This method will uppercase the provided string after removing any trailing or leading Unicode whitespace and punctuation, 
        /// and any internal space characters. If null is provided then null is returned.
        /// </summary>
        /// <param name="word">The string to turn into a word index.</param>
        /// <returns>The word index value of the provided string.</returns>
        public static string GetWordIdx(string word)
        {
            if (!String.IsNullOrEmpty(word))
            {
                word = TrimPunctuation(word.Trim()).Replace(" ", "").ToUpperInvariant();
            }

            return word;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string GetAnagramIdx(string word)
        {
            if (word == null)
            {
                return null;
            }

            word = word.Replace(" ", "").ToUpperInvariant();

            char[] array = new char[word.Length];
            for (int i = 0; i < word.Length; i++)
            {
                array[i] = word[i];
            }

            Array.Sort(array);

            return String.Join<char>("", array);
        }

        /// <summary>
        /// This method will calculate and returns the crytpogram index of the provided string.
        /// </summary>
        /// <param name="word">The string to calculate a cryptogram index for.</param>
        /// <returns>A string that represents the cryptogram index of the provided string.</returns>
        public static string GetCryptoIdx(string word)
        {
            const string replacments = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            if (word == null)
            {
                return null;
            }

            word = word.Replace(" ", "").ToUpperInvariant();

            StringBuilder result = new StringBuilder(word.Length);

            Dictionary<char, char> map = new Dictionary<char, char>();

            for (int i = 0; i < word.Length; i++)
            {
                if (!map.ContainsKey(word[i]))
                {
                    map.Add(word[i], replacments[map.Count]);
                }

                result.Append(map[word[i]]);
            }

            return result.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string GetCryptoAnagramIdx(string word)
        {
            if (word == null)
            {
                return null;
            }

            word = word.Replace(" ", "").ToUpperInvariant();

            StringBuilder result = new StringBuilder(word.Length);

            Dictionary<char, int> counts = new Dictionary<char,int>();
            for (int i = 0; i < word.Length; i++)
            {
                if (counts.ContainsKey(word[i]))
                {
                    counts[word[i]] = counts[word[i]] + 1;
                }
                else
                {
                    counts.Add(word[i], 1);
                }
            }

            foreach (var key in counts.OrderBy(value => value.Value))
            {
                result.Append(key.Key, key.Value);
            }

            return GetCryptoIdx(result.ToString());
        }

        /// <summary>
        /// Trim any punctuation characters from both front and back of provided string.
        /// </summary>
        /// <param name="word">The string to trim punctuation from.</param>
        /// <returns>The provided string with any leading or trailing punctuation characters removed.</returns>
        public static string TrimPunctuation(string word)
        {
            if (!String.IsNullOrEmpty(word))
            {
                if (Punctuation.Contains(word[0]))
                {
                    return TrimPunctuation(word.Substring(1));
                }

                if (Punctuation.Contains(word[word.Length - 1]))
                {
                    return TrimPunctuation(word.Substring(0, word.Length - 1));
                }
            }

            return word;
        }

        /// <summary>
        /// Remove a characters in the set provided from the value provided and return the resulting string.
        /// </summary>
        /// <param name="chars">The set of characters to remove.</param>
        /// <param name="value">The string to remove characters from.</param>
        /// <returns>The provided value string with all characters in the remove set taken out.</returns>
        public static string RemoveCharaters(string chars, string value)
        {
            StringBuilder result = new StringBuilder(value.Length);

            for (int i = 0; i < value.Length; i++)
            {
                if (!chars.Contains(value[i]))
                {
                    result.Append(value[i]);
                }
            }

            return result.ToString(); ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetStartsWithPattern(string text, int length)
        {
            StringBuilder result = new StringBuilder(length);

            result.Append(PHTWords.EscapeString(text, "%^[]\\"));

            if (length == 0)
            {
                result.Append('%');
            }
            else
            {
                for (var i = length - text.Length; i > 0; i--)
                {
                    result.Append('_');
                }
            }

            return result.ToString();
        }

        public static string EscapeString(string source, string characterToEscape = null)
        {
            const char escapeChar = '\\';

            StringBuilder result = new StringBuilder(source.Length * 2);

            if (String.IsNullOrEmpty(characterToEscape))
            {
                characterToEscape = String.Empty;
            }

            for (int i = 0; i < source.Length; i++)
            {
                if ((source[i] == escapeChar) || (characterToEscape.Contains(source[i])))
                {
                    result.Append(escapeChar);
                }

                result.Append(source[i]);
            }

            return result.ToString();
        }


        private static SqlConnection GetConnection()
        {
            SqlConnection result = new SqlConnection(@"Data Source=.; Initial Catalog=pht; Integrated Security=True");
            result.Open();

            return result;
        }

        private static void ReleaseConnection(SqlConnection conn)
        {
            conn.Close();
        }
    }
}

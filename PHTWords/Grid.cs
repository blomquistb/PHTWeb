using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PHTWords
{
    public class Grid
    {
        public int minFrequency = 0;
        public int[] domains = {1};

        public int width = 0;
        public int height = 0;
        public List<List<GridCell>> rows = new List<List<GridCell>>();
        public List<GridPosition> positions = new List<GridPosition>();

        public Grid()
        {
        }

        public Grid(string text)
        {
            text = text.Trim();
            var lines = text.Split('\n');

            for (var row = 0; row < lines.Length; row++)
            {
                var letters = Regex.Replace(lines[row].Trim(), @"[\s]+", " ").Split(' ');
                    
                var cells = new List<GridCell>();

                for (var j = 0; j < letters.Length; j++)
                {
                    cells.Add(new GridCell(letters[j], letters[j].Equals("#")));
                }

                this.rows.Add(cells);

                if (cells.Count > this.width)
                {
                    this.width = cells.Count;
                }
            }

            this.height = this.rows.Count;

            // clean up the row lengths if needed.
            //
            for (var row = 0; row < this.height; row++)
            {
                if (this.rows[row].Count != this.width)
                {
                    for (var i = this.width - this.rows[row].Count; i >= 0; i--)
                    {
                        this.rows[row].Add(new GridCell("#", true));
                    }
                }
            }
        }

        public Grid(Grid grid)
        {
            if (grid != null)
            {
                this.domains = grid.domains;
                this.minFrequency = grid.minFrequency;
                this.width = grid.width;
                this.height = grid.height;
                for (var row = 0; row < grid.rows.Count; row++)
                {
                    this.rows.Add(new List<GridCell>());
                    for (var col = 0; col < grid.rows[row].Count; col++)
                    {
                        this.rows[row].Add(new GridCell(grid.rows[row][col].value, grid.rows[row][col].visited));
                    }
                }

                for (var i = 0; i < grid.positions.Count; i++)
                {
                    this.positions.Add(new GridPosition(grid.positions[i].row, grid.positions[i].col));
                }
            }
        }

        public List<GridSolution> Solve(int row, int col, int wordLength)
        {
            List<int> wordLengths = new List<int>(1);
            wordLengths.Add(wordLength);
            return this.Solve(row, col, wordLengths);
        }

        public List<GridSolution> Solve(int row, int col, List<int> wordLengths)
        {
            List<GridSolution> solutions = new List<GridSolution>();

            if ((wordLengths == null) || (wordLengths.Count == 0))
            {
                solutions = this.SolveInternal(row, col, "", 0);
            }
            else 
            {
                if (wordLengths.Count > 0)
                {
                    solutions = this.SolveInternal(row, col, "", wordLengths[0]);
                }

                for (int i = 1; i < wordLengths.Count; i++)
                {
                    var newSolutions = new List<GridSolution>();
                    for (var j = 0; j < solutions.Count; j++)
                    {
                        newSolutions = newSolutions.Concat(solutions[j].Solve(wordLengths[i])).ToList();
                    }
                    solutions = newSolutions;
                }
            }

            return solutions;
        }

        private List<GridSolution> SolveInternal(int row, int col, string text, int length)
        {
            List<GridSolution> solutions = new List<GridSolution>();

            if (row < 0 || col < 0 || row >= this.height || col >= this.width || this.rows[row][col].visited)
            {
                return solutions;
            }

            text = text + this.rows[row][col].value;
            this.rows[row][col].visited = true;
            this.positions.Add(new GridPosition(row, col));

            if ((length == 0) || (length == text.Length))
            {
                var words = PHTWords.GetWordMatches(PHTWords.EscapeString(text, "%^[]\\"), null, null, null, null, domains, 1, minFrequency);
                if (words.Count == 1)
                {
                    solutions.Add(new GridSolution(new Grid(this), words[0]));
                }
            }

            if (((length > 0) && (text.Length >= length)) || (PHTWords.GetWordMatches(PHTWords.GetStartsWithPattern(text, length), null, null, null, null, domains, 1, minFrequency).Count == 0))
            {
                this.rows[row][col].visited = false;
                this.positions.RemoveAt(this.positions.Count - 1);
                return solutions;
            }

            solutions = solutions.Concat(this.SolveInternal(row + 1, col - 2, text, length)).ToList();
            solutions = solutions.Concat(this.SolveInternal(row + 1, col - 2, text, length)).ToList();
            solutions = solutions.Concat(this.SolveInternal(row + 2, col - 1, text, length)).ToList();
            solutions = solutions.Concat(this.SolveInternal(row + 2, col + 1, text, length)).ToList();
            solutions = solutions.Concat(this.SolveInternal(row + 1, col + 2, text, length)).ToList();
            solutions = solutions.Concat(this.SolveInternal(row - 1, col + 2, text, length)).ToList();
            solutions = solutions.Concat(this.SolveInternal(row - 2, col + 1, text, length)).ToList();
            solutions = solutions.Concat(this.SolveInternal(row - 2, col - 1, text, length)).ToList();
            solutions = solutions.Concat(this.SolveInternal(row - 1, col - 2, text, length)).ToList();

            this.rows[row][col].visited = false;
            this.positions.RemoveAt(this.positions.Count - 1);
            return solutions;
        }

    }
}

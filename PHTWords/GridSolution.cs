using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHTWords
{
    public class GridSolution
    {
        public Grid grid = null;
        public WordInfo word = null;

        public GridSolution(Grid grid, WordInfo word)
        {
            this.grid = grid;
            this.word = word;
        }

        public List<GridSolution> Solve(int wordLength)
        {
            var solutions = new List<GridSolution>();

            var lastCell = this.grid.positions[this.grid.positions.Count - 1];
            var row = lastCell.row;
            var col = lastCell.col;

            solutions = solutions.Concat(this.grid.Solve(row + 1, col - 2, wordLength)).ToList();
            solutions = solutions.Concat(this.grid.Solve(row + 2, col - 1, wordLength)).ToList();
            solutions = solutions.Concat(this.grid.Solve(row + 2, col + 1, wordLength)).ToList();
            solutions = solutions.Concat(this.grid.Solve(row + 1, col + 2, wordLength)).ToList();
            solutions = solutions.Concat(this.grid.Solve(row - 1, col + 2, wordLength)).ToList();
            solutions = solutions.Concat(this.grid.Solve(row - 2, col + 1, wordLength)).ToList();
            solutions = solutions.Concat(this.grid.Solve(row - 2, col - 1, wordLength)).ToList();
            solutions = solutions.Concat(this.grid.Solve(row - 1, col - 2, wordLength)).ToList();

            for (var i = 0; i < solutions.Count; i++)
            {
                solutions[i].word.value = this.word.value + " " + solutions[i].word.value;
                solutions[i].word.frequency = (this.word.frequency + solutions[i].word.frequency) / 2;
            }

            return solutions;
        }
    }
}

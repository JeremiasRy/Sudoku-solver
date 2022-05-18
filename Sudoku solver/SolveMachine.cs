using System.Collections.Generic;
using System.Linq;

namespace Sudoku_solver
{
    public static class SolveMachine
    {
        /// <summary>
        /// Checks if branch has reached a deadend (So no possibilities for a value to input)
        /// </summary>
        /// <param name="boardToCheck">The branches gamestate to check</param>
        /// <returns>true or false</returns>
        public static bool DeadEnd(List<Square> boardToCheck)
        {
            var unSolved = boardToCheck.Where(x => !x.HasValue);
            int possibilities = 0;
            foreach (var square in unSolved) //possible moves once again
            {
                for (int i = 1; i <= 9; i++)
                {
                    if (square.CorrectValue(square.VLine, square.HLine, square.Box, i))
                    {
                        possibilities++;
                    }
                }
            }
            return possibilities == 0;
        }
        /// <summary>
        ///  Check which squares have the least amount of possibilities for a value.
        /// </summary>
        /// <param name="gameBoardToCheck">The branches gamestate which it uses to check the values</param>
        /// <returns>A list of squares that have least possible values to input</returns>
        static List<Square> LeastPossibleValues(List<Square> gameBoardToCheck)
        {

            foreach (var square in gameBoardToCheck.Where(x => !x.HasValue)) //possible moves
            {
                for (int i = 1; i <= 9; i++)
                {
                    if (square.CorrectValue(square.VLine, square.HLine, square.Box, i))
                    {
                        square.possiblevalues.Add(i);
                    }
                }
            }

            return gameBoardToCheck
                .Where(x => x.possiblevalues
                .Count() ==

                gameBoardToCheck
                .Where(x => x.possiblevalues
                .Count() > 0)
                .Min(x => x.possiblevalues
                .Count()))
                .ToList();
        }


        /// <summary>
        /// Starts solving the puzzle.
        /// First it solves the "sure" values (if it only has one possible value to put) and when it encounters multiple possibilities it creates the first branches.
        /// Then it calls to solveBranches() which keeps branching out until it finds the correct route.
        /// </summary>
        static public void SolvePuzzle(out int branchesUsed)
        {

            List<List<Square>> branches = new List<List<Square>>();
            var squareToSolve = LeastPossibleValues(Gameboard.GameSquares).First();
            while (squareToSolve.possiblevalues.Count() == 1)
            {
                SolveRound(squareToSolve, squareToSolve.possiblevalues.First(), Gameboard.GameSquares);
                squareToSolve = LeastPossibleValues(Gameboard.GameSquares).First();
            }

            if (squareToSolve.possiblevalues.Count() > 1)
            {
                var possibilities = new List<int>();
                foreach (int i in squareToSolve.possiblevalues)
                {
                    possibilities.Add(i);
                }
                branches = BranchOut(squareToSolve, possibilities, Gameboard.GameSquares);
            }
            var winningBranch = SolveBranches(branches, out int branchCount);
            branchesUsed = branchCount;

            foreach (var square in winningBranch)
            {
                var SquareToSet = Gameboard.GameSquares
                     .Where(x => x.Position == square.Position)
                     .First();
                SquareToSet.SetValue(square.Value);
                SquareToSet.possiblevalues.Clear();

            }
            return;
        }

        static void SolveRound(Square square, int value, List<Square> listToClean)
        {
            square.SetValue(value);
            foreach (var cleanSquare in listToClean) //Clean up possible values..
            {
                cleanSquare.possiblevalues.Clear();
            }
        }

        /// <summary>
        /// Creates the first branches
        /// </summary>
        /// <param name="squareToSolve">The square that had multiple possibilities</param>
        /// <param name="possibilities">list of all the possible values</param>
        /// <param name="fromWhereToBranch">The state of the game when branching occurs</param>
        /// <returns>A list of branches</returns>
        static public List<List<Square>> BranchOut(Square squareToSolve, List<int> possibilities, List<Square> fromWhereToBranch)
        {
            var branches = new List<List<Square>>();
            for (int i = 0; i < possibilities.Count(); i++)
            {
                var branch = new List<Square>();
                branches.Add(branch);
            }
            foreach (var branch in branches)
            {
                foreach (var square in fromWhereToBranch)
                {
                    branch.Add(new Square(square.vPos, square.hPos, square.box, square.Value, branch));
                }
            }
            for (int i = 0; i < possibilities.Count(); i++)
            {
                var solve = branches.ElementAt(i).Where(x => x.Position == squareToSolve.Position).First();
                SolveRound(solve, possibilities[i], branches.ElementAt(i));
            }
            return branches;
        }
        /// <summary>
        /// Creates a new gamestate or "branch"
        /// </summary>
        /// <param name="squareToSolve">the square that had more than one possibility</param>
        /// <param name="value">Value to put in the square</param>
        /// <param name="fromWhereToBranch"> The state of the board when branch was made</param>
        /// <returns>A gamestate where program can continue solving.</returns>
        static List<Square> AddBranch(Square squareToSolve, int value, List<Square> fromWhereToBranch)
        {
            var newBranch = new List<Square>();
            foreach (var square in fromWhereToBranch)
            {
                newBranch.Add(new Square(square.vPos, square.hPos, square.box, square.Value, newBranch));
            }
            var sqToSolve = newBranch.Where(x => x.Position == squareToSolve.Position).First();
            SolveRound(sqToSolve, value, newBranch);
            return newBranch;
        }

        /// <summary>
        /// Solve a branch using first possible values encountered to a deadend and check if it's completed if not delete branch.
        /// Save all other possibilities as branches.
        /// </summary>
        /// <param name="branches"> Stores all the possible branches a.k.a gameStates </param>
        /// <returns> A winning branch </returns>
        static List<Square> SolveBranches(List<List<Square>> branches, out int branchCount)
        {
            branchCount = 1;
            while (branches.Count > 1)
            {

                var branch = branches.First();

                while (!DeadEnd(branch))
                {
                    var nextSq = LeastPossibleValues(branch).First();
                    if (nextSq.possiblevalues.Count() == 1)
                    {
                        SolveRound(nextSq, nextSq.possiblevalues.First(), branch);
                    }
                    if (nextSq.possiblevalues.Count() > 1)
                    {
                        for (int j = 1; j < nextSq.possiblevalues.Count(); j++) //The method keeps going from first index (i == 0) and saves all other possibilities (i >= 1) for future.
                        {
                            branches.Add(AddBranch(nextSq, nextSq.possiblevalues[j], branch));
                            branchCount++;
                        }
                        SolveRound(nextSq, nextSq.possiblevalues.First(), branch);
                    }
                }
                if (branch.Where(x => x.IsCorrect).Count() == 81) //Check if everything is correct
                {
                    return branch;
                }
                branches.RemoveAt(0); //Remove the branch that didn't solve the puzzle.
            }
            return new List<Square>();

        }
    }
}


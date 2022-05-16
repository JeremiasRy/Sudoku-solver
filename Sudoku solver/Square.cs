using System.Collections.Generic;
using System.Linq;

namespace Sudoku_solver
{
    public class Square
    {
        public readonly int vPos;
        public readonly int hPos;
        public readonly int box;

        List<Square> myPlaceInTheUniverse = new List<Square>(); //Tells the solver in which branch this.square lives
        /// <summary>
        /// Gets all the squares from horizontal line, vertical line and the box in where this.square is.
        /// </summary>
        public List<Square> Box { get { return myPlaceInTheUniverse.Where(x => x.box == this.box).Where(x => x != this).ToList(); } }
        public List<Square> VLine { get { return myPlaceInTheUniverse.Where(x => x.vPos == this.vPos).Where(x => x != this).ToList(); } }
        public List<Square> HLine { get { return myPlaceInTheUniverse.Where(x => x.hPos == this.hPos).Where(x => x != this).ToList(); } }

        /// <summary>
        /// The solver uses this List to store possible values
        /// </summary>
        public List<int> possiblevalues = new List<int>();

        public int? Value { get; set; } //The actual value this square holds (can be null)
        public bool HasValue => Value != null; //Checks if the square has a value
        public int Position { get { return vPos * 10 + hPos; } } //A position of the square in the game table First int vertical position second horizontal.

        public bool IsCorrect { get { return CorrectValue(VLine, HLine, Box, Value); } } //Checks if the value is correct

        public Square(int vPos, int hPos, int box, List<Square> whereILive) // This is the "Main" constructor it creates the root game
        {
            this.vPos = vPos;
            this.hPos = hPos;
            this.box = box;
            myPlaceInTheUniverse = whereILive;
        }
        public Square(int vPos, int hPos, int box, int? value, List<Square> whereILive) // when the solver branches out it uses this constructor to create new squares;
        {
            this.vPos = vPos;
            this.hPos = hPos;
            this.box = box;
            this.Value = value;
            myPlaceInTheUniverse = whereILive;
        }


        /// <summary>
        /// Checks all the lines to see if the value already exists and then checks if it's null;
        /// </summary>
        /// <param name="verticalLine">The vertical line from the square</param>
        /// <param name="horizontalLine">The horizontal line from the square</param>
        /// <param name="box">the box where the square is</param>
        /// <param name="value">The value to check (used by the solver to get possibilities)</param>
        /// <returns>True if it fits false if it doesn't</returns>
        public bool CorrectValue(List<Square> verticalLine, List<Square> horizontalLine, List<Square> box, int? value)
        {
            if (verticalLine.Where(x => x.Value == value || value == null).Count() != 0
                || horizontalLine.Where(x => x.Value == value || value == null).Count() != 0
                || box.Where(x => x.Value == value || value == null).Count() != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

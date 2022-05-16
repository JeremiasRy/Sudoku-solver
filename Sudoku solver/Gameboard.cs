using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku_solver
{
    public static class Gameboard
    {
        public static Random Random = new Random();
        public const int BOARD_SIZE = 9;
        public const int BOX_SIZE = BOARD_SIZE / 3;
        static public bool Completed => GameSquares.Where(x => x.IsCorrect).Count() == BOARD_SIZE * BOARD_SIZE;

        public static List<Square> GameSquares { get; set; } = new List<Square>();

        /// <summary>
        /// I numbered the boxes from left to right top left being 1 and bottom right is 9;
        /// This is very messy I admit... But it works... I could just iterate over vertical/horizontal positions but meh..
        /// </summary>
        /// <param name="Position">The position of the square</param>
        /// <returns>A box number</returns>
        public static int GetBox(int Position)
        {
            if (Position == 11 || Position == 12 || Position == 13 || Position == 21 || Position == 22 || Position == 23 || Position == 31 || Position == 32 || Position == 33)
            {
                return 1;
            }
            if (Position == 14 || Position == 15 || Position == 16 || Position == 24 || Position == 25 || Position == 26 || Position == 34 || Position == 35 || Position == 36)
            {
                return 2;
            }
            if (Position == 17 || Position == 18 || Position == 19 || Position == 27 || Position == 28 || Position == 29 || Position == 37 || Position == 38 || Position == 39)
            {
                return 3;
            }
            if (Position == 41 || Position == 42 || Position == 43 || Position == 51 || Position == 52 || Position == 53 || Position == 61 || Position == 62 || Position == 63)
            {
                return 4;
            }
            if (Position == 44 || Position == 45 || Position == 46 || Position == 54 || Position == 55 || Position == 56 || Position == 64 || Position == 65 || Position == 66)
            {
                return 5;
            }
            if (Position == 47 || Position == 48 || Position == 49 || Position == 57 || Position == 58 || Position == 59 || Position == 67 || Position == 68 || Position == 69)
            {
                return 6;
            }
            if (Position == 71 || Position == 72 || Position == 73 || Position == 81 || Position == 82 || Position == 83 || Position == 91 || Position == 92 || Position == 93)
            {
                return 7;
            }
            if (Position == 74 || Position == 75 || Position == 76 || Position == 84 || Position == 85 || Position == 86 || Position == 94 || Position == 95 || Position == 96)
            {
                return 8;
            }
            else
                return 9;

        }
        static Gameboard() //Constructor for the main gameboard
        {
            for (int vertical = 1; vertical <= BOARD_SIZE; vertical++)
            {
                for (int horizontal = 1; horizontal <= BOARD_SIZE; horizontal++)
                {
                    int position = vertical * 10 + horizontal;
                    GameSquares.Add(new Square(vertical, horizontal, GetBox(position), GameSquares));
                }
            }
        }
    }
}

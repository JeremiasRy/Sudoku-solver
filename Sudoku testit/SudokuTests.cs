using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
namespace Sudoku_testit
{
    using Sudoku_solver;

    [TestClass]
    public class GameboardTests
    {
        [TestMethod]
        public void TestGameboard()
        {
            Assert.AreEqual(Gameboard.BOARD_SIZE * Gameboard.BOARD_SIZE, Gameboard.GameSquares.Count());
            Assert.AreEqual(false, Gameboard.Completed);
        }
    }
    [TestClass]
    public class SquareTests
    {
        [TestInitialize]
        public void TestInitializer()
        {
            foreach (var square in Gameboard.GameSquares)
            {
                square.Value = null;
            }
        }
        [TestMethod]
        public void TestSquare()
        {
            var square = Gameboard.GameSquares[0];
            var square2 = Gameboard.GameSquares[1];
            square.Value = 1;
            Assert.AreEqual(1, square.Value);
            Assert.AreEqual(true, square.IsCorrect);
            square2.Value = 1;
            Assert.AreEqual(false, square.IsCorrect);
            square2.Value = 2;
            Assert.AreEqual(true, square.IsCorrect);
        }
        [TestMethod]
        public void TestFillBox()
        {
            var firstBox = Gameboard.GameSquares.Where(x => x.box == 1).ToList();

            for (var i = 0; i < firstBox.Count(); i++)
            {
                firstBox[i].Value = i + 1;
            }

            Assert.AreEqual(9, firstBox.Where(x => x.IsCorrect).Count());
            var secondBox = Gameboard.GameSquares.Where(x => x.box == 2).ToList();

            for (var i = 0; i < secondBox.Count(); i++)
            {
                secondBox[i].Value = i + 1;
            }
            Assert.AreEqual(0, secondBox.Where(x => x.IsCorrect).Count());

        }
        [TestMethod]
        public void TestLines()
        {
            var vertiLine = Gameboard.GameSquares.Where(x => x.vPos == 1).ToList();
            for (var i = 0; i < vertiLine.Count(); i++)
            {
                vertiLine[i].Value = i + 1;
            }
            Assert.AreEqual(9, vertiLine.Where(x => x.IsCorrect).Count());

            var vertiLine2 = Gameboard.GameSquares.Where(x => x.vPos == 2).ToList();
            for (var i = 0; i < vertiLine2.Count(); i++)
            {
                vertiLine2[i].Value = 9 - i;
            }
            Assert.AreEqual(6, vertiLine2.Where(x => x.IsCorrect).Count());
            var horiLine = Gameboard.GameSquares.Where(x => x.hPos == 1).ToList();
            for (var i = 0; i < horiLine.Count(); i++)
            {
                horiLine[i].Value = i + 1;
            }
            Assert.AreEqual(4, vertiLine2.Where(x => x.IsCorrect).Count());
            Assert.AreEqual(7, horiLine.Where(x => x.IsCorrect).Count());
            var square = Gameboard.GameSquares.Where(x => x.Position == 33).First();
            Assert.IsNull(square.Value);
            square.Value = 1;
            Assert.IsFalse(square.IsCorrect);
            var firstBox = Gameboard.GameSquares.Where(x => x.box == 1).ToList();
            Assert.AreEqual(8, firstBox.Where(x => x.HasValue).Count());
        }
    }
    [TestClass]
    public class SolverTests
    {
        

        [TestInitialize]
        public void SolverInitializer()
        {
            foreach (var square in Gameboard.GameSquares)
            {
                square.Value = null;
            }
        }
        [TestMethod]
        public void TestEmptySudoku()
        {
            SolveMachine.SolvePuzzle();

            Assert.AreEqual(81, Gameboard.GameSquares.Where(x => x.IsCorrect).Count());
            Assert.IsTrue(Gameboard.Completed);

        }
        [TestMethod]
        public void TestWithOneRandomValue()
        {
            Gameboard.GameSquares.Skip(Gameboard.Random.Next(0, Gameboard.BOARD_SIZE * Gameboard.BOARD_SIZE)).First().Value = Gameboard.Random.Next(1, 9);
            SolveMachine.SolvePuzzle();

            Assert.AreEqual(81, Gameboard.GameSquares.Where(x => x.IsCorrect).Count());
            Assert.IsTrue(Gameboard.Completed);

        }
        [TestMethod]
        public void TestReadySudoku()
        {
            

            Gameboard.GameSquares[3].Value = 8;

            Gameboard.GameSquares[11].Value = 2;
            Gameboard.GameSquares[13].Value = 6;
            Gameboard.GameSquares[15].Value = 3;

            Gameboard.GameSquares[19].Value = 4;
            Gameboard.GameSquares[20].Value = 1;
            Gameboard.GameSquares[23].Value = 7;
            Gameboard.GameSquares[26].Value = 9;

            Gameboard.GameSquares[27].Value = 7;
            Gameboard.GameSquares[31].Value = 2;
            Gameboard.GameSquares[32].Value = 5;

            Gameboard.GameSquares[37].Value = 6;
            Gameboard.GameSquares[39].Value = 7;
            Gameboard.GameSquares[44].Value = 8;

            Gameboard.GameSquares[47].Value = 3;
            Gameboard.GameSquares[48].Value = 6;
            Gameboard.GameSquares[53].Value = 4;

            Gameboard.GameSquares[55].Value = 7;
            Gameboard.GameSquares[61].Value = 8;
            Gameboard.GameSquares[62].Value = 2;

            Gameboard.GameSquares[69].Value = 7;

            Gameboard.GameSquares[74].Value = 4;
            Gameboard.GameSquares[76].Value = 7;
            Gameboard.GameSquares[77].Value = 9;
            Gameboard.GameSquares[78].Value = 5;
            Gameboard.GameSquares[80].Value = 3;

            SolveMachine.SolvePuzzle();
            var str = Gameboard.GameSquares.Select(x => x.Value);
            var joined = string.Join(" - ", str);

            Assert.IsTrue(Gameboard.Completed);
        }
        [TestMethod]
        public void TestReadySudoku2()
        {
            Gameboard.GameSquares[0].Value = 9;

            Gameboard.GameSquares[10].Value = 1;
            Gameboard.GameSquares[12].Value = 9;
            Gameboard.GameSquares[14].Value = 6;
            Gameboard.GameSquares[17].Value = 2;

            Gameboard.GameSquares[24].Value = 6;

            Gameboard.GameSquares[28].Value = 6;
            Gameboard.GameSquares[32].Value = 3;
            Gameboard.GameSquares[33].Value = 2;
            Gameboard.GameSquares[35].Value = 1;

            Gameboard.GameSquares[43].Value = 8;

            Gameboard.GameSquares[46].Value = 4;
            Gameboard.GameSquares[48].Value = 5;
            Gameboard.GameSquares[51].Value = 7;

            Gameboard.GameSquares[56].Value = 5;
            Gameboard.GameSquares[57].Value = 6;
            Gameboard.GameSquares[59].Value = 9;
            Gameboard.GameSquares[61].Value = 2;
            Gameboard.GameSquares[62].Value = 3;

            Gameboard.GameSquares[67].Value = 8;
            Gameboard.GameSquares[69].Value = 4;
            Gameboard.GameSquares[71].Value = 6;

            Gameboard.GameSquares[73].Value = 2;
            Gameboard.GameSquares[75].Value = 7;
            Gameboard.GameSquares[78].Value = 9;
            Gameboard.GameSquares[79].Value = 5;

            SolveMachine.SolvePuzzle();
            var str = Gameboard.GameSquares.Select(x => x.Value);
            var joined = string.Join(" - ", str);

            Assert.IsTrue(Gameboard.Completed);

        }
    }
}

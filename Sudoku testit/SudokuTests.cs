using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
namespace Sudoku_testit
{
    using Sudoku_solver;

    [TestClass]
    public class GameboardTests
    {
        [TestMethod]
        public void TestGameboard()
        {
            Assert.AreEqual(81, Gameboard.GameSquares.Count());
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
                square.SetValue(null);
            }
        }
        [TestMethod]
        public void TestSquare()
        {
            var square = Gameboard.GameSquares[0];
            var square2 = Gameboard.GameSquares[1];
            square.SetValue(1);
            Assert.AreEqual(1, square.Value);
            Assert.AreEqual(true, square.IsCorrect);
            square2.SetValue(1);
            Assert.AreEqual(false, square.IsCorrect);
            square2.SetValue(2);
            Assert.AreEqual(true, square.IsCorrect);
        }
        [TestMethod]
        public void TestFillBox()
        {
            var firstBox = Gameboard.GameSquares.Where(x => x.box == 1).ToList();

            for (var i = 0; i < firstBox.Count(); i++)
            {
                firstBox[i].SetValue(i + 1);
            }

            Assert.AreEqual(9, firstBox.Where(x => x.IsCorrect).Count());
            var secondBox = Gameboard.GameSquares.Where(x => x.box == 2).ToList();

            for (var i = 0; i < secondBox.Count(); i++)
            {
                secondBox[i].SetValue(i + 1);
            }
            Assert.AreEqual(0, secondBox.Where(x => x.IsCorrect).Count());

        }
        [TestMethod]
        public void TestLines()
        {
            var vertiLine = Gameboard.GameSquares.Where(x => x.vPos == 1).ToList();
            for (var i = 0; i < vertiLine.Count(); i++)
            {
                vertiLine[i].SetValue(i + 1);
            }
            Assert.AreEqual(9, vertiLine.Where(x => x.IsCorrect).Count());

            var vertiLine2 = Gameboard.GameSquares.Where(x => x.vPos == 2).ToList();
            for (var i = 0; i < vertiLine2.Count(); i++)
            {
                vertiLine2[i].SetValue(9 - i);
            }
            Assert.AreEqual(6, vertiLine2.Where(x => x.IsCorrect).Count());
            var horiLine = Gameboard.GameSquares.Where(x => x.hPos == 1).ToList();
            for (var i = 0; i < horiLine.Count(); i++)
            {
                horiLine[i].SetValue(i + 1);
            }
            Assert.AreEqual(4, vertiLine2.Where(x => x.IsCorrect).Count());
            Assert.AreEqual(7, horiLine.Where(x => x.IsCorrect).Count());
            var square = Gameboard.GameSquares.Where(x => x.Position == 33).First();
            Assert.IsNull(square.Value);
            square.SetValue(1);
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
                square.SetValue(null);
            }
        }
        [TestMethod]
        public void TestEmptySudoku()
        {
            SolveMachine.SolvePuzzle(out int branchesUsed);

            Assert.AreEqual(81, Gameboard.GameSquares.Where(x => x.IsCorrect).Count());
            Assert.IsNotNull(branchesUsed); 
            Assert.IsTrue(Gameboard.Completed);

        }
        [TestMethod]
        public void TestWithOneRandomValue()
        {
            Random random = new Random();
            Gameboard.GameSquares.Skip(random.Next(0, 81)).First().SetValue(random.Next(1, 9));
            SolveMachine.SolvePuzzle(out int i);

            Assert.AreEqual(81, Gameboard.GameSquares.Where(x => x.IsCorrect).Count());
            Assert.IsTrue(Gameboard.Completed);

        }
        [TestMethod]
        public void TestReadySudoku()
        {

            Gameboard.GameSquares[3].SetValue(8);

            Gameboard.GameSquares[11].SetValue(2);
            Gameboard.GameSquares[13].SetValue(6);
            Gameboard.GameSquares[15].SetValue(3);

            Gameboard.GameSquares[19].SetValue(4);
            Gameboard.GameSquares[20].SetValue(1);
            Gameboard.GameSquares[23].SetValue(7);
            Gameboard.GameSquares[26].SetValue(9);

            Gameboard.GameSquares[27].SetValue(7);
            Gameboard.GameSquares[31].SetValue(2);
            Gameboard.GameSquares[32].SetValue(5);

            Gameboard.GameSquares[37].SetValue(6);
            Gameboard.GameSquares[39].SetValue(7);
            Gameboard.GameSquares[44].SetValue(8);

            Gameboard.GameSquares[47].SetValue(3);
            Gameboard.GameSquares[48].SetValue(6);
            Gameboard.GameSquares[53].SetValue(4);

            Gameboard.GameSquares[55].SetValue(7);
            Gameboard.GameSquares[61].SetValue(8);
            Gameboard.GameSquares[62].SetValue(2);

            Gameboard.GameSquares[69].SetValue(7);

            Gameboard.GameSquares[74].SetValue(4);
            Gameboard.GameSquares[76].SetValue(7);
            Gameboard.GameSquares[77].SetValue(9);
            Gameboard.GameSquares[78].SetValue(5);
            Gameboard.GameSquares[80].SetValue(3);

            SolveMachine.SolvePuzzle(out int i);
            var str = Gameboard.GameSquares.Select(x => x.Value);
            var joined = string.Join(" - ", str);

            Assert.IsTrue(Gameboard.Completed);
        }
        [TestMethod]
        public void TestReadySudoku2()
        {
            Gameboard.GameSquares[0].SetValue(9);

            Gameboard.GameSquares[10].SetValue(1);
            Gameboard.GameSquares[12].SetValue(9);
            Gameboard.GameSquares[14].SetValue(6);
            Gameboard.GameSquares[17].SetValue(2);

            Gameboard.GameSquares[24].SetValue(6);

            Gameboard.GameSquares[28].SetValue(6);
            Gameboard.GameSquares[32].SetValue(3);
            Gameboard.GameSquares[33].SetValue(2);
            Gameboard.GameSquares[35].SetValue(1);

            Gameboard.GameSquares[43].SetValue(8);

            Gameboard.GameSquares[46].SetValue(4);
            Gameboard.GameSquares[48].SetValue(5);
            Gameboard.GameSquares[51].SetValue(7);

            Gameboard.GameSquares[56].SetValue(5);
            Gameboard.GameSquares[57].SetValue(6);
            Gameboard.GameSquares[59].SetValue(9);
            Gameboard.GameSquares[61].SetValue(2);
            Gameboard.GameSquares[62].SetValue(3);

            Gameboard.GameSquares[67].SetValue(8);
            Gameboard.GameSquares[69].SetValue(4);
            Gameboard.GameSquares[71].SetValue(6);

            Gameboard.GameSquares[73].SetValue(2);
            Gameboard.GameSquares[75].SetValue(7);
            Gameboard.GameSquares[78].SetValue(9);
            Gameboard.GameSquares[79].SetValue(5);

            SolveMachine.SolvePuzzle(out int i);
            var str = Gameboard.GameSquares.Select(x => x.Value);
            var joined = string.Join(" - ", str);

            Assert.IsTrue(Gameboard.Completed);

        }
    }
}

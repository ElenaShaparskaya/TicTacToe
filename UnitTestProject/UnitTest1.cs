using System;
using TickTackToe;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class BlackBox
    {
        [TestMethod]
        public void TestInit1()
        {
            // Incorrect size
            var field = new Field();
            field.SetField(new CellType[,] {
                {CellType.X, CellType.O, CellType._},
                {CellType._, CellType.X, CellType._}
            });
            var cell = Calculation.FindNextMove(field, CellType.O);
           
                Assert.IsNull(cell);
           
        }

        [TestMethod]
        public void TestInit2()
        {
            // Incorrect size
            var cell = Calculation.FindNextMove(null, CellType.O);
            Assert.IsNull(cell);
        }

        [TestMethod]
        public void TestStep1()
        {
            var field = new Field();
            field.SetField(new CellType[,] {
                {CellType.X, CellType.O, CellType._, CellType._, CellType._},
                {CellType._, CellType.X, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._}
            });

            var cell = Calculation.FindNextMove(field, CellType.O);
            Assert.AreEqual(0, cell.H);
            Assert.AreEqual(1, cell.V);
        }

        [TestMethod]
        public void TestStep2()
        {
            var field = new Field();
            field.SetField(new CellType[,] {
                {CellType.X, CellType.O, CellType._, CellType._, CellType._},
                {CellType.O, CellType.X, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._}
            });
            var cell = Calculation.FindNextMove(field, CellType.X);
            Assert.AreEqual(2, cell.H);
            Assert.AreEqual(2, cell.V);
        }

        [TestMethod]
        public void TestStep3()
        {
            var field = new Field();
            field.SetField(new CellType[,] {
                {CellType.X, CellType.O, CellType._, CellType._, CellType._},
                {CellType.O, CellType.X, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType.X, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._}
            });
            var cell = Calculation.FindNextMove(field, CellType.O);
            Assert.AreEqual(1, cell.H);
            Assert.AreEqual(2, cell.V);
        }

        [TestMethod]
        public void TestStep4()
        {
            var field = new Field();
            field.SetField(new CellType[,] {
                {CellType.X, CellType.O, CellType._, CellType._, CellType._},
                {CellType.O, CellType.X, CellType._, CellType._, CellType._},
                {CellType._, CellType.O, CellType.X, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._}
            });
            var cell = Calculation.FindNextMove(field, CellType.X);
            Assert.AreEqual(3, cell.H);
            Assert.AreEqual(3, cell.V);
        }
    }
    [TestClass]
    public class WhiteBox
    {
        [TestMethod]
        public void TestStep1()
        {
            var field = new Field();
            field.SetField(new CellType[,] {
                {CellType.X, CellType.O, CellType._, CellType._, CellType._},
                {CellType._, CellType.X, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._}
            });

            var cell = Calculation.FindNextMove(field, CellType.O);

            Assert.AreEqual(0, cell.H);
            Assert.AreEqual(1, cell.V);
            Assert.AreEqual(2, Calculation.WhiteBoxMoves.Count);
            Assert.AreEqual(0, Calculation.WhiteBoxMoves[0].H);
            Assert.AreEqual(1, Calculation.WhiteBoxMoves[0].V);
            Assert.AreEqual(0, Calculation.WhiteBoxMoves[1].H);
            Assert.AreEqual(1, Calculation.WhiteBoxMoves[1].V);
        }

        [TestMethod]
        public void TestStep2()
        {
            var field = new Field();
            field.SetField(new CellType[,] {
                {CellType.X, CellType.O, CellType._, CellType._, CellType._},
                {CellType.O, CellType.X, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._}
            });
            var cell = Calculation.FindNextMove(field, CellType.X);

            Assert.AreEqual(2, cell.H);
            Assert.AreEqual(2, cell.V);
            Assert.AreEqual(3, Calculation.WhiteBoxMoves.Count);
            Assert.AreEqual(2, Calculation.WhiteBoxMoves[0].H);
            Assert.AreEqual(2, Calculation.WhiteBoxMoves[0].V);
            Assert.AreEqual(0, Calculation.WhiteBoxMoves[1].H);
            Assert.AreEqual(2, Calculation.WhiteBoxMoves[1].V);
            Assert.AreEqual(0, Calculation.WhiteBoxMoves[2].H);
            Assert.AreEqual(2, Calculation.WhiteBoxMoves[2].V);
        }

        [TestMethod]
        public void TestStep3()
        {
            var field = new Field();
            field.SetField(new CellType[,] {
                {CellType.X, CellType.O, CellType._, CellType._, CellType._},
                {CellType.O, CellType.X, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType.X, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._}
            });
            var cell = Calculation.FindNextMove(field, CellType.O);

            Assert.AreEqual(1, cell.H);
            Assert.AreEqual(2, cell.V);
            Assert.AreEqual(2, Calculation.WhiteBoxMoves.Count);
            Assert.AreEqual(1, Calculation.WhiteBoxMoves[0].H);
            Assert.AreEqual(2, Calculation.WhiteBoxMoves[0].V);
            Assert.AreEqual(0, Calculation.WhiteBoxMoves[1].H);
            Assert.AreEqual(2, Calculation.WhiteBoxMoves[1].V);
        }

        [TestMethod]
        public void TestStep4()
        {
            var field = new Field();
            field.SetField(new CellType[,] {
                {CellType.X, CellType.O, CellType._, CellType._, CellType._},
                {CellType.O, CellType.X, CellType._, CellType._, CellType._},
                {CellType._, CellType.O, CellType.X, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._},
                {CellType._, CellType._, CellType._, CellType._, CellType._}
            });
            var cell = Calculation.FindNextMove(field, CellType.X);

            Assert.AreEqual(3, cell.H);
            Assert.AreEqual(3, cell.V);
            Assert.AreEqual(4, Calculation.WhiteBoxMoves.Count);
            Assert.AreEqual(3, Calculation.WhiteBoxMoves[0].H);
            Assert.AreEqual(3, Calculation.WhiteBoxMoves[0].V);
            Assert.AreEqual(3, Calculation.WhiteBoxMoves[1].H);
            Assert.AreEqual(3, Calculation.WhiteBoxMoves[1].V);
            Assert.AreEqual(0, Calculation.WhiteBoxMoves[2].H);
            Assert.AreEqual(2, Calculation.WhiteBoxMoves[2].V);
            Assert.AreEqual(0, Calculation.WhiteBoxMoves[3].H);
            Assert.AreEqual(2, Calculation.WhiteBoxMoves[3].V);
        }
    }
}
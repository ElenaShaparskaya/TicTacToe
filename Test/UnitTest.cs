using NUnit.Framework;
using TickTackToe;

namespace Test
{
    public class BlackBox
    {
        [Test]
        public void TestInit1()
        {
            // Incorrect size
            var field = new Field();
            field.SetField(new CellType[,] {
                {CellType.X, CellType.O, CellType._},
                {CellType._, CellType.X, CellType._}
            });
            var cell = Calculation.FindNextMove(field, CellType.O);
            if (cell == null)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void TestInit2()
        {
            // Incorrect size
            var cell = Calculation.FindNextMove(null, CellType.O);
            if (cell == null)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
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
            if (cell.H == 0 && cell.V == 1)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
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
            if (cell.H == 2 && cell.V == 2)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
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
            if (cell.H == 1 && cell.V == 2)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
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
            if (cell.H == 3 && cell.V == 3)
                Assert.Pass();
            else
                Assert.Fail();
        }
    }

    public class WhiteBox
    {
        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

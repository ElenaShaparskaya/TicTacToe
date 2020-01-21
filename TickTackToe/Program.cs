using System;

namespace TickTackToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input size:");
            int size = 0;
            try
            {
                size = Convert.ToUInt16(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Incorrect size");
                Console.ReadKey();
                return;
            }

            var field = new Field();
            field.SetField(new CellType[size, size]);

            Console.WriteLine("Original field:");
            Console.WriteLine(field.ToString());

            for (var i = 0; i < field.Size * field.Size; i++)
            {
                var nextPlayer = (i % 2 == 0) ? (CellType.O) : (CellType.X);
                var cell = Calculation.FindNextMove(field, nextPlayer);
                if (cell == null) break;
                string player = (nextPlayer == CellType.O) ? ("O") : ("X");
                if (Calculation.WhiteBoxMoves?.Count > 0)
                {
                    Console.WriteLine("Possible moves:");
                    foreach (var c in Calculation.WhiteBoxMoves)
                        Console.WriteLine(c);
                }
                Console.WriteLine($"Next Move {player}: {cell}");
                field = Calculation.SetMove(field, cell, nextPlayer);

                Console.WriteLine(field.ToString());


            }
            Console.ReadLine();
        }
    }
}


using System.Collections.Generic;
using System.Linq;

namespace TickTackToe
{

    
    public class Calculation
    {
        // Возможные ходы для модульных тестов 
        public static List<Cell> WhiteBoxMoves { get; set; }

        // Основная функция вычисления хода
        public static Cell FindNextMove(Field field, CellType cellType)
        {
            // null-checking
            if (field == null) return null;
            // Минимальный размер поля 3 x 3
            if (field.Size < 3) return null;

            var possibleMoves = new List<Cell>();
            var list = new List<Section>();
             // Формируем список отрезков, начиная с самых больших
            // чтобы на нем сделать ход.
            // Например, если игра 4x4, то ищем сначала отрезки размером 3
            // и если находим, то выбираем один отрезок и делаем ход, продолжая его
            // Если не нашли ни одного отрезка длиною 3, то ищем отрезки длиною 2
            for (var i = field.Size - 1; i > 1; i--)
            {
                // Поиск диагоналей длиной i
                list.AddRange(FindHorizontal(field, cellType, i));
                list.AddRange(FindVertical(field, cellType, i));
                list.AddRange(FindDiagonalDown(field, cellType, i));
                list.AddRange(FindDiagonalUp(field, cellType, i));

                // Проверяем доступные ходы по найденным отрезкам текущей длины
                // и делаем ход для продолжения первого отрезка из  списка
                foreach (var section in list)
                {
                    var cell = ContinuedCell(field, section);
                    if (cell != null)
                        possibleMoves.Add(cell);
                }
            }

            var availablePoint = FindCell(field, cellType);
            // Есть ли хоть одна клетка на поле у указанного игрока?
            if (availablePoint != null)
            {
                // Если есть, значит ищем свободную ячейку вокруг этой ячейки
                var nearCell = GetNearCell(field, availablePoint);
                if (nearCell == null)
                {
                    // Не нашли доступную ячейку вокруг ячейки
                    // Ищем любую свободную ячейку на поле
                    var cell = FindCell(field, CellType._);
                    if (cell != null)
                        possibleMoves.Add(cell);
                    
                }
               
                // Нашли доступную ячейку вокруг ячейки
               
                if (nearCell != null)
                    possibleMoves.Add(nearCell);
            }

            // Не нашли ни одной ячейки для игрока, поэтому ставим ее на любую свободную ячейку
            var findCell = FindCell(field, CellType._);
            if (findCell != null)
                possibleMoves.Add(findCell);

            WhiteBoxMoves = possibleMoves;

            if (possibleMoves.Count > 0)
                return possibleMoves.First();

            return null;
        }
        
        // Функция для белого ящика


        // Установка новой ячейки на поле и вывод для визуализации
        public static Field SetMove(Field field, Cell cell, CellType cellType)
        {
            var newField = field;
            newField.SetCell(cell, cellType);
            return newField;
        }

        // Поиск ячейки по указанному типу
        private static Cell FindCell(Field field, CellType cellType)
        {
            for (var i = 0; i < field.Size; i++)
                for (var j = 0; j < field.Size; j++)
                    if (field.GetCell(i, j) == cellType)
                        return new Cell(i ,j);
            return null;
        }

        // Поиск свободной ячейки вокруг заданной
        private static Cell GetNearCell(Field field, Cell cell)
        {
            var h = cell.H;
            var v = cell.V;
            var points = new List<Cell>
            {
                new Cell(h - 1, v + 1),
                new Cell(h - 1, v),
                new Cell(h - 1, v - 1),
                new Cell(h, v - 1),
                new Cell(h + 1, v - 1),
                new Cell(h + 1, v),
                new Cell(h + 1, v + 1),
                new Cell(h, v + 1)
            };
            foreach (var p in points)
                if (IsAvailableCell(field, p))
                    return p;
            return null;
        }
        
        // Поиск ячейки для продолжения отрезка
        private static Cell ContinuedCell(Field field, Section section)
        {
            var type = section.GetSectionType();
            // Если тип отрезка горизонтальный
            if (type == SectionType.Horizontal)
            {
                // То пробуем ячейку до перед началом отрезка
                var h = section.BeginCell.H - 1;
                var v = section.BeginCell.V;
                // Ячейка доступна для хода?
                if (IsAvailableCell(field, new Cell(h, v))) 
                    return new Cell(h, v);

                // Пробуем ячейку после конца отрезка
                h = section.EndCell.H + 1;
                v = section.EndCell.V;
                // Ячейка доступна для хода?
                if (IsAvailableCell(field, new Cell(h, v)))
                    return new Cell(h, v);
            }

            if (type == SectionType.Vertical)
            {
                var h = section.BeginCell.H;
                var v = section.BeginCell.V - 1;
                if (IsAvailableCell(field, new Cell(h, v)))
                    return new Cell(h, v);

                h = section.EndCell.H;
                v = section.EndCell.V + 1;
                if (IsAvailableCell(field, new Cell(h, v)))
                    return new Cell(h, v);
            }

            if (type == SectionType.UpDiagonal)
            {
                var h = section.BeginCell.H - 1;
                var v = section.BeginCell.V + 1;
                if (IsAvailableCell(field, new Cell(h, v)))
                    return new Cell(h, v);

                h = section.EndCell.H + 1;
                v = section.EndCell.V - 1;
                if (IsAvailableCell(field, new Cell(h, v)))
                    return new Cell(h, v);
            }

            if (type == SectionType.DownDiagonal)
            {
                var h = section.BeginCell.H - 1;
                var v = section.BeginCell.V - 1;
                if (IsAvailableCell(field, new Cell(h, v)))
                    return new Cell(h, v);

                h = section.EndCell.H + 1;
                v = section.EndCell.V + 1;
                if (IsAvailableCell(field, new Cell(h, v)))
                    return new Cell(h, v);
            }

            return null;
        }

        // Проверка доступности ячейки для хода
        private static bool IsAvailableCell(Field field, Cell cell)
        {
            // В приеделах ли поля ячейка?
            var inField = cell.H >= 0 && cell.H < field.Size && 
                          cell.V >= 0 && cell.V < field.Size;
            if (!inField)
                return false;
            // Свободна ли ячейка?
            return field.GetCell(cell.H, cell.V) == CellType._;
        }

        private static List<Section> FindHorizontal(Field field, CellType cellType, int length)
        {
            var list = new List<Section>();
            for (var v = 0; v < field.Size; v++)
            {
                var count = 0;
                var horizontalBegin = 0;
                var verticalBegin = 0;
                for (var h = 0; h < field.Size; h++)
                {
                    if (field.GetCell(h, v) == cellType)
                    {
                        count++;
                        if (count == 1)
                        {
                            horizontalBegin = h;
                            verticalBegin = v;
                        }
                    }
                    else
                        count = 0;

                    if (count != length) continue;
                    var horizontalEnd = h;
                    var verticalEnd = v;

                    list.Add(new Section(horizontalBegin, verticalBegin,
                        horizontalEnd, verticalEnd));
                }
            }
            return list;
        }

        private static List<Section> FindVertical(Field field, CellType cellType, int length)
        {
            var list = new List<Section>();
            for (var h = 0; h < field.Size; h++)
            {
                var count = 0;
                var horizontalBegin = 0;
                var verticalBegin = 0;
                for (var v = 0; v < field.Size; v++)
                {
                    if (field.GetCell(h, v) == cellType)
                    {
                        count++;
                        if (count == 1)
                        {
                            horizontalBegin = h;
                            verticalBegin = v;
                        }
                    }
                    else
                        count = 0;

                    if (count != length) continue;
                    var horizontalEnd = h;
                    var verticalEnd = v;

                    list.Add(new Section(horizontalBegin, verticalBegin,
                        horizontalEnd, verticalEnd));
                }
            }
            return list;
        }

        // Поиск отрезка по диагонали
        private static List<Section> FindDiagonalDown(Field field, CellType cellType, int length)
        {
            var list = new List<Section>();

            const int hStep = 1;
            const int vStep = 1;

            var hStart = -field.Size + 1;
            var vStart = 0;

            for (var i = hStart; i < field.Size; i++)
            {
                var count = 0;
                var h = i;
                var v = vStart;
                var horizontalBegin = 0;
                var verticalBegin = 0;
                for (var j = 0; j < field.Size; j++)
                {
                    if (h >= 0 && h < field.Size)
                    {
                        if (field.GetCell(h, v) == cellType)
                        {
                            count++;
                            if (count == 1)
                            {
                                horizontalBegin = h;
                                verticalBegin = v;
                            }
                        }
                        else
                            count = 0;

                        if (count == length)
                        {
                            var horizontalEnd = h;
                            var verticalEnd = v;

                            list.Add(new Section(horizontalBegin, verticalBegin,
                                horizontalEnd, verticalEnd));
                        }
                    }

                    v += vStep;
                    h += hStep;
                }
            }
            return list;
        }

        private static List<Section> FindDiagonalUp(Field field, CellType cellType, int length)
        {
            var list = new List<Section>();
            const int hStep = 1;
            const int vStep = -1;

            var hStart = -field.Size + 1;
            var vStart = field.Size - 1;

            for (var i = hStart; i < field.Size; i++)
            {
                var count = 0;
                var h = i;
                var v = vStart;
                var horizontalBegin = 0;
                var verticalBegin = 0;
                for (var j = 0; j < field.Size; j++)
                {
                    if (h >= 0 && h < field.Size)
                    {
                        if (field.GetCell(h, v) == cellType)
                        {
                            count++;
                            if (count == 1)
                            {
                                horizontalBegin = h;
                                verticalBegin = v;
                            }
                        }
                        else
                            count = 0;

                        if (count == length)
                        {
                            var horizontalEnd = h;
                            var verticalEnd = v;

                            list.Add(new Section(horizontalBegin, verticalBegin,
                                horizontalEnd, verticalEnd));
                        }
                    }

                    v += vStep;
                    h += hStep;
                }
            }
            return list;
        }
    }
    
    // Возможные состояния ячейки
    public enum CellType {
       _, O, X
    }

    // Типы отрезков
    public enum SectionType
    {
        Horizontal, Vertical, UpDiagonal, DownDiagonal, Unknown
    }

    // Ячейка. Состоит из двух координат 
    // H - горизонталь, V - вертикаль
    public class Cell
    {
        public int H { get; }
        public int V { get; }
        public Cell(int h, int v)
        {
            H = h;
            V = v;
        }

        public override string ToString()
        {
            return $"[{H} {V}]";
        }
    }

    // Отрезок. Состоит из координат начала и конца
    public class Section
    {
        public Cell BeginCell { get; }
        public Cell EndCell { get; }

        public Section(int horizontalBegin, int verticalBegin,
            int horizontalEnd, int verticalEnd)
        {
            BeginCell = new Cell(horizontalBegin, verticalBegin);
            EndCell = new Cell(horizontalEnd, verticalEnd);
        }

        public override string ToString()
        {
            return $"[{BeginCell.H},{BeginCell.V}][{EndCell.H},{EndCell.V}]";
        }

        // Получение тип отрезка
        public SectionType GetSectionType()
        {
            if (BeginCell.H == EndCell.H)
                return SectionType.Vertical;
            if (BeginCell.V == EndCell.V)
                return SectionType.Horizontal;
            if (BeginCell.H < EndCell.H &&
                BeginCell.V < EndCell.V)
                return SectionType.DownDiagonal;
            if (BeginCell.H < EndCell.H &&
                BeginCell.V > EndCell.V)
                return SectionType.UpDiagonal;

            return SectionType.Unknown;
        }
    }

    // Поле. Содержит двумерный массив из ячеек
    public class Field
    {
        // Двумерный массив, где хранится поле
        private CellType[,] _cells;

        public CellType GetCell(int horizontal, int vertical) => _cells[vertical, horizontal];

        // Задать значение для ячейки
        public void SetCell(Cell cell, CellType cellType) => _cells[cell.V, cell.H] = cellType;

        // Получение размера поля
        public int Size => _cells.GetLength(1) == _cells.GetLength(0) ? _cells.GetLength(0) : -1;
   
        // Задать поле, подавая двумерный массив ячеек
        public void SetField(CellType[,] cellsType) => _cells = cellsType;

        public override string ToString()
        {
            var outString = string.Empty;
            for (var i = 0; i < _cells.GetLength(1); i++)
            {
                for (var j = 0; j < _cells.GetLength(0); j++)
                {
                    var cell = _cells[i, j];
                    switch (cell)
                    {
                        case CellType.O:
                            outString += "O ";
                            break;
                        case CellType.X:
                            outString += "X ";
                            break;
                        case CellType._:
                            outString += "_ ";
                            break;
                    }
                }
                outString += "\n";
            }
            return outString;
        }
    }
}

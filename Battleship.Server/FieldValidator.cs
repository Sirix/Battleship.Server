using System;
using System.Collections.Generic;
using System.Linq;
using Battleship.Server.Shared;

namespace Battleship.Server
{
    internal sealed class FieldValidator
    {
        public List<Ship> Ships = new List<Ship>();
        private bool[,] _field;

        public bool Validate(bool[][] field)
        {
            const int size = GameConfiguration.FieldSize;
            var array = new bool[size,size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    array[i, j] = field[i][j];

            return Validate(array);
        }

        public bool Validate(bool[,] field)
        {
            bool thrownOnNonFieldException = false;
#if DEBUG
            thrownOnNonFieldException = true;
#endif
            return InternalValidate(field, thrownOnNonFieldException);
        }

        private bool InternalValidate(bool[,] field, bool throwOnNonFieldException)
        {
            try
            {
                InternalValidate(field);
                return true;
            }
            catch (FieldValidationException)
            {
                return false;
            }
            catch (Exception)
            {
                if (throwOnNonFieldException)
                    throw;
                else
                    return false;
            }
        }

        private void InternalValidate(bool[,] field)
        {
            _field = field;
            Ships.Clear();

            var candidatesForSingleShip = new List<Cell>();

            var cells = new List<Cell>();

            //поиск вертикальных кораблей
            for (int x = 0; x < GameConfiguration.FieldSize; x++)
            {
                for (int y = 0; y < GameConfiguration.FieldSize; y++)
                {
                    if (field[x, y])
                        cells.Add(new Cell(x, y));
                    else
                    {
                        if (cells.Count == 1)
                        {
                            candidatesForSingleShip.Add(cells[0]);
                            cells.Clear();
                        }

                        if (cells.Count > 1)
                        {
                            AddShip(cells);
                            cells.Clear();
                        }
                    }
                }
                if (cells.Count == 1)
                    candidatesForSingleShip.Add(new Cell(x, GameConfiguration.FieldSize - 1));

                if (cells.Count > 1)
                    AddShip(cells);

                cells.Clear();
            }

            //поиск горизонтальных кораблей
            for (int y = 0; y < GameConfiguration.FieldSize; y++)
            {
                for (int x = 0; x < GameConfiguration.FieldSize; x++)
                {
                    if (field[x, y])
                        cells.Add(new Cell(x, y));
                    else
                    {
                        if (cells.Count == 1)
                        {
                            candidatesForSingleShip.Add(cells[0]);
                            cells.Clear();
                        }
                        if (cells.Count > 1)
                        {
                            AddShip(cells);
                            cells.Clear();
                        }
                    }
                }
                if (cells.Count == 1)
                    candidatesForSingleShip.Add(new Cell(GameConfiguration.FieldSize - 1, y));

                if (cells.Count > 1)
                    AddShip(cells);

                cells.Clear();
            }

            AddSingleShips(candidatesForSingleShip);
        }

        private void AddSingleShips(List<Cell> candidatesForSingleShip)
        {
            //кораблями, состоящими из 1 ячейки, являются те, что находятся в списке 2 раза
            //в результате 2 проходов анализатора
            var ships = new List<Cell>();
            foreach (Cell c in candidatesForSingleShip)
            {
                int findCount = candidatesForSingleShip.Count(t1 => c.Equals(t1));
                if (findCount == 2 && !ships.Contains(c))
                    ships.Add(c);
            }
            ships.ForEach(c => AddShip(new List<Cell> {c}));
        }

        private void AddShip(List<Cell> cells)
        {
            //размер корабля не соответствует заданным параметрам
            if (cells.Count == 0 || cells.Count > GameConfiguration.Ships.Keys.Max())
                throw new FieldValidationException();

            //На поле много таких кораблей
            if (Ships.Count(s => s.Size == cells.Count) >= GameConfiguration.Ships[cells.Count])
                throw new FieldValidationException();

            MarkShipOuterBox(cells);
            Ships.Add(new Ship(cells));
        }

        private void MarkShipOuterBox(IEnumerable<Cell> shipCells)
        {
            int topBorder, downBorder, leftBorder, rightBorder;

            if (shipCells.Count() != 1)
            {
                //определяем ориентацию корабля
                bool isHorizontal = shipCells.First().Y == shipCells.Last().Y;

                if (isHorizontal)
                    shipCells = shipCells.OrderBy(c => c.X);
                else
                    shipCells = shipCells.OrderBy(c => c.Y);
            }

            leftBorder = shipCells.First().X == 0 ? 0 : shipCells.First().X - 1;
            rightBorder = shipCells.Last().X == GameConfiguration.FieldSize - 1
                              ? GameConfiguration.FieldSize - 1
                              : shipCells.Last().X + 1;

            topBorder = shipCells.First().Y == 0 ? 0 : shipCells.First().Y - 1;
            downBorder = shipCells.Last().Y == GameConfiguration.FieldSize - 1
                             ? GameConfiguration.FieldSize - 1
                             : shipCells.Last().Y + 1;

            for (int x = leftBorder; x <= rightBorder; x++)
                for (int y = topBorder; y <= downBorder; y++)
                    //пропускаем клетки, занятые непосредственно кораблем
                    if (!shipCells.Any(c => c.X == x && c.Y == y))
                    {
                        if (_field[x, y]) //ошибка, корабли соприкасаются
                            throw new FieldValidationException();
                    }
        }
    }
}
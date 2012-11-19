using System.Collections.Generic;

namespace Battleship.Server.Shared
{
    public sealed class GameConfiguration
    {
        /// <summary>
        /// Размер стороны квадратного поля.
        /// </summary>
        public const int FieldSize = 10;
        /// <summary>
        /// Описывает количество кораблей. Ключ - тип корабля, значение - кол-во кораблей на поле.
        /// </summary>
        public static readonly Dictionary<int, int> Ships = new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 2 }, { 4, 1 } };
    }
}
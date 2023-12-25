using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static prog10.Enum;

namespace prog10
{
    internal class Arrow
    {
        public static int Show(int minstrelka, int maxstrelka)
        {
            int pos = minstrelka;
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
                Console.SetCursorPosition(0, pos);
                Console.WriteLine("  ");
                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (pos == minstrelka)
                    {
                        pos = maxstrelka;
                    }
                    pos--;
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    if (pos == maxstrelka)
                    {
                        pos = minstrelka;
                    }
                    pos++;
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    return (int)button.Back;
                }
                else if (key.Key == ConsoleKey.F1)
                {
                    return (int)button.Create;
                }
                else if (key.Key == ConsoleKey.Delete)
                {
                    return (int)button.F10;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    return pos;
                }
                else if (key.Key == ConsoleKey.S)
                {
                    return (int)button.Save;
                }
                else if (key.Key == ConsoleKey.OemPlus)
                {
                    return (int)button.Plus;
                }
                else if (key.Key == ConsoleKey.OemMinus)
                {
                    return (int)button.Minus;
                }
                else if (key.Key == ConsoleKey.F2)
                {
                    return (int)button.Search;
                }
                Console.SetCursorPosition(0, pos);
                Console.WriteLine("->");
            } while (key.Key != ConsoleKey.Enter);
            return pos;
        }
    }
}
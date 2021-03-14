using System;
using System.Drawing;
using System.Net.Mime;

namespace HConsole
{
    internal class Window
    {
        private char[][] WindowText { get; }

        public int Height { get; }

        public Window(char[][] array)
        {
            WindowText = array;
            Height = array.Length;
        }

        public int GetWidth(int line)
        {
            var width = 0;

            for (var i = 0; i < WindowText[line].Length; i++)
            {
                if (WindowText[line][i] != '\0')
                {
                    width = i;
                }
            }
            
            return width + 1;
        }

        public char GetChar(int x, int y)
        {
            return WindowText[y][x];
        }
        
        public char GetChar(Point point)
        {
            return WindowText[point.Y][point.X];
        }

        public bool Equals(Window other, Point point)
        {
            return GetChar(point) == other.GetChar(point);
        }
    }
}
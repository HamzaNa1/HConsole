using System;
using System.Collections.Generic;
using System.Linq;

namespace HConsole
{
    public static class MyConsole
    {
        private static readonly List<char> Buffer = new();
        private static Window _window;

        private const int Width = 250;

        public static void UpdateWindow()
        {
            var newWindow = GetWindowFromBuffer();
    
            if (_window == null)
            {
                _window = newWindow;
                Flush();
                return;
            }

            var smallerY = Math.Min(_window.Height, newWindow.Height);

            RemoveExtras(smallerY, newWindow);
            
            var points = CreatePoints(smallerY, newWindow);
            if (points == null)
            {
                _window = newWindow;
                Flush();
                return;
            }

            var groups = Group.CreateGroups(points, newWindow);
            
            _window = newWindow;
            Flush(groups);
            
            for(var i = 0; i < points.Count; i++)
                Pool.ReturnPoint(points[i]);
            for(var i = 0; i < groups.Count; i++)
                Pool.ReturnGroup(groups[i]);
        }

        private static Window GetWindowFromBuffer()
        {
            var window = new List<char[]>();

            var x = 0;
            var currentLine = new char[Width];

            for(var i = 0; i < Buffer.Count; i++)
            {
                var c = Buffer[i];
                
                if (c == '\n' || x >= Width - 1)
                {
                    window.Add((char[]) currentLine.Clone());
                    currentLine = new char[Width];
                    x = 0;
                    continue;
                }

                currentLine[x] = c;
                x += 1;
            }

            window.Add((char[]) currentLine.Clone());

            return new Window(window.ToArray());
        }
        
        private static List<Point> CreatePoints(int smallerY, Window newWindow)
        {
            var points = new List<Point>();

            for (var j = 0; j < smallerY; j++)
            {
                for (var i = 0; i < Width; i++)
                {
                    if (_window.GetChar(i, j) == newWindow.GetChar(i, j))
                        continue;

                    var point = Pool.GetPoint();
                    point.X = i;
                    point.Y = j;
                    
                    points.Add(point);

                    if (points.Count >= (smallerY * Width) / 4)
                    {
                        return null;
                    }
                }
            }

            return points;
        }

        private static void Flush()
        {
            Console.SetCursorPosition(0,0);
            Console.WriteLine(Buffer.ToArray());
            
            Console.SetCursorPosition(0,0);
        }
        
        private static void Flush(List<Group> groups)
        {
            for (var i = 0; i < groups.Count; i++)
            {
                var g = groups[i];
                
                Console.SetCursorPosition(g.Start.X, g.Start.Y);
                Console.Write(new string(g.Character, g.Count));
            }
            
            Console.SetCursorPosition(0,0);
        }
        
        private static void Flush(List<Point> points)
        {
            for(var i = 0; i < points.Count; i++)
            {
                var p = points[i];
                
                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(_window.GetChar(p.X, p.Y));
            }
            
            Console.SetCursorPosition(0,0);
        }
        
        public static void Write(string s)
        {
            Buffer.AddRange(s.ToCharArray());
        }
        
        public static void Write(char c)
        {
            Buffer.Add(c);
        }
        
        public static void Write(object obj)
        {
            Write(obj.ToString());
        }

        public static void WriteLine(string s)
        {
           Write(s + '\n');
        }

        public static void WriteLine(char c)
        {
            Write(c + '\n');
        }
        
        public static void WriteLine(object obj)
        {
            Write(obj.ToString() + '\n');
        }

        public static void WriteLine()
        {
            Write('\n');
        }

        public static void ClearBuffer()
        {
            Buffer.Clear();
        }
        
        
        public static void Clear()
        {
            Console.Clear();
        }

        private static void RemoveExtras(int smallerY, Window newWindow)
        {
            if (newWindow.Height < _window.Height)
                ClearRow(_window.Height, newWindow.Height);
            
            for (var j = 0; j < smallerY; j++)
            {
                ClearLine(j, newWindow.GetWidth(j), _window.GetWidth(j));
            }
        }

        private static void ClearRow(int endRow, int startRow = 0)
        {
            Console.SetCursorPosition(0, 0);
            for (var i = startRow; i < endRow; i++)
            {
                Console.Write(new string(' ', Width));
            }
            
            Console.SetCursorPosition(0, 0);
        }

        private static void ClearLine(int line, int start, int end)
        {
            if (start > end)
                return;
            
            Console.SetCursorPosition(start , line);
            Console.Write(new string(' ', end - start));
            
            Console.SetCursorPosition(0, 0);
        }
    }
}
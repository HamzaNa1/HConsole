using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HConsole
{
    internal readonly struct Group
    {
        public Point Start { get; }
        public char Character { get; }
        public int Count { get; }

        private Group(Point start, char c, int count)
        {
            Start = start;
            Character = c;
            Count = count;
        }

        public static List<Group> CreateGroups(List<Point> points, Window window)
        {
            if (window == null) 
                throw new ArgumentNullException(nameof(window));
            
            var groups = new List<Group>();
            
            if (points.Count == 0)
                return groups;

            var firstPoint = points[0];

            var start = firstPoint;
            var c = window.GetChar(firstPoint);
            var count = 1;

            if (points.Count == 1)
            {
                groups.Add(new Group(start, c, count));
                return groups;
            }

            for (var i = 1; i < points.Count; i++)
            {
                var currentPoint = points[i];
                var lastPoint = points[i - 1];

                if (currentPoint.X == lastPoint.X + 1 && currentPoint.Y == lastPoint.Y &&
                    window.GetChar(currentPoint) == window.GetChar(lastPoint))
                {
                    count += 1;
                }
                else
                {
                    groups.Add(new Group(start: start, c: c, count: count));
                    start = currentPoint;
                    c = window.GetChar(currentPoint);
                    count = 1;
                }

                groups.Add(new Group(start: start, c: c, count: count));
            }
            
            return groups;
        }
    }
}
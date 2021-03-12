using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HConsole
{
    internal struct Group
    {
        public Point Start { get; private set; }
        public char Character { get; private set; }
        public int Count { get; private set; }

        private Group(Point start, char c, int count)
        {
            Start = start;
            Character = c;
            Count = count;
        }

        private void Set(Point start, char c, int count)
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

            Group group;
            
            var firstPoint = points[0];

            var start = firstPoint;
            var c = window.GetChar(firstPoint);
            var count = 1;

            if (points.Count == 1)
            {
                 group = Pool.GetGroup();
                group.Set(start, c, count);
                groups.Add(group);
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
                    group = Pool.GetGroup();
                    group.Set(start, c, count);
                    groups.Add(group);
                    start = currentPoint;
                    c = window.GetChar(currentPoint);
                    count = 1;
                }

                group = Pool.GetGroup();
                group.Set(start, c, count);
                groups.Add(group);
            }

            return groups;
        }
    }
}
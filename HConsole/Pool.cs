using System.Collections.Generic;
using System.Drawing;

namespace HConsole
{
    internal static class Pool
    {
        private static readonly Queue<Group> GroupPool = new();
        private static readonly Queue<Point> PointPool = new(); 
        
        public static Group GetGroup()
        {
            if (GroupPool.Count == 0)
                return new Group();

            return GroupPool.Dequeue();
        }

        public static void ReturnGroup(Group group)
        {
            GroupPool.Enqueue(group);
        }
        
        public static Point GetPoint()
        {
            if (PointPool.Count == 0)
                return new Point();

            return PointPool.Dequeue();        
        }

        public static void ReturnPoint(Point point)
        {
            PointPool.Enqueue(point);
        }
    }
}
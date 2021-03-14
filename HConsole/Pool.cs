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
            return GroupPool.TryDequeue(out var group) ? group : new Group();
        }

        public static void ReturnGroup(Group group)
        {
            GroupPool.Enqueue(group);
        }
        
        public static Point GetPoint()
        {
            return PointPool.TryDequeue(out var point) ? point : new Point();
        }

        public static void ReturnPoint(Point point)
        {
            PointPool.Enqueue(point);
        }
    }
}
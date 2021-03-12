using System.Collections.Generic;
using System.Drawing;

namespace HConsole
{
    internal static class Pool
    {
        private static Queue<Group> groupPool = new Queue<Group>();
        private static Queue<Point> pointPool = new Queue<Point>(); 
        
        public static Group GetGroup()
        {
            if (groupPool.Count == 0)
            {
                return new Group();
            }

            return groupPool.Dequeue();
        }

        public static void ReturnGroup(Group group)
        {
            groupPool.Enqueue(group);
        }
        
        public static Point GetPoint()
        {
            if (pointPool.Count == 0)
            {
                return new Point();
            }

            return pointPool.Dequeue();        }

        public static void ReturnPoint(Point point)
        {
            pointPool.Enqueue(point);
        }
    }
}
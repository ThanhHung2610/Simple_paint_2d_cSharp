using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace _19120237_BT2
{
    class Line : Shape
    {
        public Line(List<Point> vertices, Point Start, Point End, float thick, Color color, Color color_fill, Boolean isFill) : base(vertices, Start, End, thick, color, color_fill, isFill)
        {
            this.vertices = new List<Point>();
            this.vertices.Add(Start);
            this.vertices.Add(End);
            //thiết lập các điểm điều khiển
            setControlPoints();
        }

        public override void showShape(OpenGL gl)
        {
            if (list_points.Count() != 0)
            {
                list_points.Clear();
            }
            DrawLine(vertices[0], vertices[1], gl);
        }

        public override void ShowEditShape(OpenGL gl)
        {
            showShape(gl);
            DrawSetControl(gl);
        }

        public override void setControlPoints()
        {
            ctrl_Points = new List<Point>();
            ctrl_Points.Add(pStart);
            ctrl_Points.Add(pEnd);
        }

        public override bool isOnShape(Point cur)
        {
            int n = list_points.Count();
            for (int i = 8; i < n - 8; i++)
            {
                if (Math.Abs(list_points[i].X - cur.X) < 5 && Math.Abs(list_points[i].Y - cur.Y) < 5)
                {
                    return true;
                }
            }
            return false;
        }

        public override Point getCenter()
        {
            //trả về trung điểm đoạn thẳng
            return new Point((vertices[0].X + vertices[1].X) / 2,(vertices[0].Y + vertices[1].Y) / 2);
        }


        public override short getShapeType()
        {
            return 0;
        }
    }
}

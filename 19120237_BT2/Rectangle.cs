using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace _19120237_BT2
{

    class Rectangle : Shape
    {
        //pStart là đỉnh 1, pEnd là đỉnh 3
        Point vertex2;
        Point vertex4;
        public Rectangle(List<Point> vertices, Point Start, Point End, float thick, Color color, Color color_fill, Boolean isFill) : base(vertices, Start, End, thick, color, color_fill, isFill)
        {
            vertex2.X = End.X;
            vertex2.Y = Start.Y;
            vertex4.X = Start.X;
            vertex4.Y = End.Y;
            this.vertices = new List<Point>();
            this.vertices.Add(pStart);
            this.vertices.Add(vertex2);
            this.vertices.Add(pEnd);
            this.vertices.Add(vertex4);
            // thiết lập các điểm điều khiển
            setControlPoints();
        }

        public override void showShape(OpenGL gl)
        {
            FillShape(gl);
            if (list_points.Count() != 0)
            {
                list_points.Clear();
            }    
            for (int i = 0; i < 3; i++)
            {
                DrawLine(vertices[i], vertices[i + 1], gl);
            }
            DrawLine(vertices[3], vertices[0], gl);

        }

        public override void editShape(List<Point> vertices, List<Point> points, List<Point> controlPoints, float thick, Color color, Color color_fill, bool isFill)
        {
            base.editShape(vertices, points, controlPoints, thick, color, color_fill, isFill);
        }
        public override void ShowEditShape(OpenGL gl)
        {
            showShape(gl);
            DrawSetControl(gl);
        }

        public override void FillShape(OpenGL gl)
        {
            if (this.isFill)
            {
                //tô loang
                if (false)
                {
                    //lấy seed là tâm hình chữ nhật
                    Point center = new Point((pStart.X + pEnd.X) / 2, gl.RenderContextProvider.Height - (pStart.Y + pEnd.Y) / 2);
                    this.fill.Floodfill(center.X, center.Y, color_fill, color, gl);
                }
                //tô quét
                fill.ScanFill(vertices, color_fill, gl);
            }
        }

        public override void setControlPoints()
        {
            Point topLeft, topMid, topRight, midLeft, midRight, bottomLeft, bottomMid, bottomRight;
            topLeft = new Point(pStart.X,  pStart.Y);
            topMid = new Point((int)Math.Round((pStart.X + pEnd.X) / 2.0),  pStart.Y);
            topRight = new Point(pEnd.X,  pStart.Y);
            midLeft = new Point(pStart.X, (int)Math.Round((pStart.Y + pEnd.Y) / 2.0));
            midRight = new Point(pEnd.X, (int)Math.Round((pStart.Y + pEnd.Y) / 2.0));
            bottomLeft = new Point(pStart.X,  pEnd.Y);
            bottomMid = new Point((int)Math.Round((pStart.X + pEnd.X) / 2.0),  pEnd.Y);
            bottomRight = new Point(pEnd.X,  pEnd.Y);

            ctrl_Points = new List<Point>();
            ctrl_Points.Add(topLeft);
            ctrl_Points.Add(topMid);
            ctrl_Points.Add(topRight);
            ctrl_Points.Add(midRight);
            ctrl_Points.Add(bottomRight);
            ctrl_Points.Add(bottomMid);
            ctrl_Points.Add(bottomLeft);
            ctrl_Points.Add(midLeft);
        }


        public override Point getCenter()
        {
            //trả về trung điểm đường chéo
            return new Point((int)Math.Round((vertices[0].X + vertices[2].X) / 2.0), (int)Math.Round((vertices[0].Y + vertices[2].Y) / 2.0));
        }


        public override short getShapeType()
        {
            return 3;
        }
    }
}

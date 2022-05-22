using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace _19120237_BT2
{
    class RegularHexagon : Shape
    {
        //AB là đáy trên, DE là đáy dưới, F là điểm trái nhất
        Point A, B, C, D, E, F;
        public RegularHexagon(List<Point> vertices, Point Start, Point End, float thick, Color color, Color color_fill, Boolean isFill) : base(vertices,Start, End, thick, color,color_fill,isFill)
        {
            int dy = End.Y - Start.Y;
            //Khoảng cách khối theo 0x
            int dx = End.X - Start.X;

            //tính cạnh của ngũ giác đều
            double a = Math.Abs(dx) / 2;
            double b = a / 2;
            double h = a * Math.Sqrt(3);
            double h1 = h / 2;
            //Nếu End nằm trên Start
            if (dy < 0)
            {
                D.Y = Start.Y;
                E.Y = Start.Y;
            }
            //End bằm dưới Start hay End.y = Start.y
            else
            {
                D.Y = End.Y;
                E.Y = End.Y;
            }

            F.Y = E.Y - (int)h1;
            C.Y = E.Y - (int)h1;
            A.Y = E.Y - (int)h;
            B.Y = E.Y - (int)h;

            //nếu End nằm bên phải Start
            if (dx > 0)
            {
                F.X = Start.X;
                C.X = End.X;
            }
            //nếu End nằm bên trái Start hay End.X = Start.X
            else
            {
                F.X = End.X;
                C.X = Start.X;
            }
            E.X = F.X + (int)Math.Round(b);
            D.X = C.X - (int)Math.Round(b);
            A.X = E.X;
            B.X = D.X;

            this.vertices = new List<Point>();
            this.vertices.Add(A);
            this.vertices.Add(B);
            this.vertices.Add(C);
            this.vertices.Add(D);
            this.vertices.Add(E);
            this.vertices.Add(F);

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
            for (int i = 0; i < 5; i++)
            {
                DrawLine(vertices[i], vertices[i + 1], gl);
            }
            DrawLine(vertices[5], vertices[0], gl);
        }

        public override void editShape(List<Point> vertices, List<Point> points, List<Point> controlPoints, float thick, Color color, Color color_fill, bool isFill)
        {
            base.editShape(vertices, points, controlPoints, thick, color, color_fill, isFill);
            this.vertices = new List<Point>(vertices);

        }
        public override void ShowEditShape(OpenGL gl)
        {
            showShape(gl);
            DrawSetControl(gl);
        }
        public override void FillShape(OpenGL gl)
        {
            if (isFill)
            {
                if (false)
                {
                    //lấy seed là điểm của đoạn thẳng CF
                    Point seed = new Point((C.X + F.X) / 2, gl.RenderContextProvider.Height - (C.Y + F.Y) / 2);
                    this.fill.Floodfill(seed.X, seed.Y, color_fill, color, gl);
                }
                fill.ScanFill(vertices, color_fill, gl);
            }
        }


        public override void setControlPoints()
        {
            Point topLeft, topMid, topRight, midLeft, midRight, bottomLeft, bottomMid, bottomRight;
            topLeft = new Point(F.X,  A.Y);
            topMid = new Point((F.X + C.X) / 2,  A.Y);
            topRight = new Point(C.X,  A.Y);

            midLeft = new Point(F.X,  F.Y);
            midRight = new Point(C.X,  C.Y);

            bottomLeft = new Point(F.X,  E.Y);
            bottomMid = new Point((F.X + C.X) / 2,  E.Y);
            bottomRight = new Point(C.X,  E.Y);

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
            return new Point((vertices[0].X + vertices[3].X) / 2, (vertices[0].Y + vertices[3].Y) / 2);
        }


        public override short getShapeType()
        {
            return 6;
        }
    }
}

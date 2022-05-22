using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace _19120237_BT2
{
    class RegularPentagon : Shape
    {
        //AB là đáy dưới, E là điểm trái nhất
        Point A, B, C, D, E;
        public RegularPentagon(List<Point> vertices, Point Start, Point End, float thick, Color color, Color color_fill, Boolean isFill) : base(vertices, Start, End, thick, color, color_fill, isFill)
        {
            int dy = End.Y - Start.Y;
            //Khoảng cách khối theo 0x
            int dx = End.X - Start.X;

            //tính cạnh của ngũ giác đều
            double alpha = 72 * 3.14 / 180;
            double a = Math.Abs(dx) / (2 * Math.Cos(alpha) + 1);
            double b = a * Math.Cos(alpha);
            double h = a * Math.Sin(alpha);
            double beta = 54 * 3.14 / 180;
            double h1 = a * Math.Cos(beta);
            //Nếu End nằm trên Start
            if (dy < 0)
            {
                A.Y = Start.Y;
                B.Y = Start.Y;
            }
            //End bằm dưới Start hay End.y = Start.y
            else
            {
                A.Y = End.Y;
                B.Y = End.Y;
            }

            D.X = (int)Math.Round((End.X + Start.X) / 2.0);
            D.Y = -(int)h1 - (int)h + A.Y;

            E.Y = -(int)h + A.Y;
            C.Y = E.Y;

            //nếu End nằm bên phải Start
            if (dx > 0)
            {
                E.X = Start.X;
                C.X = End.X;
            }
            //nếu End nằm bên trái Start hay End.X = Start.X
            else
            {
                E.X = End.X;
                C.X = Start.X;
            }
            A.X = E.X + (int)b;
            B.X = C.X - (int)b;

            this.vertices = new List<Point>();
            this.vertices.Add(A);
            this.vertices.Add(B);
            this.vertices.Add(C);
            this.vertices.Add(D);
            this.vertices.Add(E);
            //thiết lập các điểm điều khiển
            setControlPoints();

        }
        public override void showShape(OpenGL gl)
        {
            FillShape(gl);
            if (list_points.Count() != 0)
            {
                list_points.Clear();
            }
            for (int i = 0; i < 4; i++)
            { 
                DrawLine(vertices[i], vertices[i + 1], gl);
            }
            DrawLine(vertices[4], vertices[0], gl);
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
            if(isFill)
            {
                if (false)
                {
                    //lấy seed là trung điểm đoạn thẳng CE
                    Point seed = new Point((C.X + E.X) / 2, gl.RenderContextProvider.Height - (C.Y + E.Y) / 2);
                    this.fill.Floodfill(seed.X, seed.Y, color_fill, color, gl);
                }
                fill.ScanFill(vertices, color_fill, gl);
            }    
        }


        public override void setControlPoints()
        {
            Point topLeft, topMid, topRight, midLeft, midRight, bottomLeft, bottomMid, bottomRight;
            topLeft = new Point(E.X,  D.Y);
            topMid = new Point(D.X,  D.Y);
            topRight = new Point(C.X,  D.Y);

            midLeft = new Point(E.X, (int)Math.Round((A.Y + D.Y) / 2.0));
            midRight = new Point(C.X, (int)Math.Round((A.Y + D.Y) / 2.0));

            bottomLeft = new Point(E.X,  B.Y);
            bottomMid = new Point(D.X,  B.Y);
            bottomRight = new Point(C.X,  B.Y);

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

            this.vertices.Add(A);
            this.vertices.Add(B);
            this.vertices.Add(C);
            this.vertices.Add(D);
            this.vertices.Add(E);
            //trung điểm M của AE
            Point M = new Point((vertices[0].X + vertices[4].X) / 2, (vertices[0].Y + vertices[4].Y) / 2);
            // trung điểm N của BC
            Point N = new Point((vertices[1].X + vertices[2].X) / 2, (vertices[1].Y + vertices[2].Y) / 2);
            //trung điểm P của MN
            Point P = new Point((M.X + N.X) / 2, (M.Y + N.Y) / 2);

            //trọng tâm của hình là điểm G sao cho GD = 4DP
            Point G = new Point((vertices[4].X + 4 * P.X) / 5, (vertices[4].Y + 4 * P.Y) / 5);
            return G;
        }


        public override short getShapeType()
        {
            return 5;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace _19120237_BT2
{
    class Ellipse : Shape
    {
        //tâm elip là pStart
        int Rx, Ry;
        public Ellipse(List<Point> vertices, Point Start, Point End, float thick, Color color, Color color_fill, Boolean isFill) : base(vertices, Start, End, thick, color, color_fill, isFill)
        {
            //tạo list vertices chứa tâm (pStart0 và pEnd
            this.vertices = new List<Point>();
            this.vertices.Add(pStart);
            this.vertices.Add(pEnd);


            //Tính toán các điểm trên elip và thêm vào list_points
            calculateListPoints();



            //thiết lập các điểm điều khiển
            setControlPoints();
        }

        public override void calculateListPoints()
        {
            //Rx, Ry
            Rx = Math.Abs(vertices[1].X - vertices[0].X);
            Ry = Math.Abs(vertices[1].Y - vertices[0].Y);

            //tập hợp điểm trên đường elip
            if (list_points.Count() != 0)
            {
                list_points.Clear();
            }
            Point CenterPoint = new Point();
            //pStart.Y khi display
            CenterPoint.Y = vertices[0].Y;
            CenterPoint.X = vertices[0].X;

            //điểm đầu tiên
            Point point = new Point(0, Ry);
            //thêm 4 điểm nằm trên trục vào list
            Point point1 = new Point();
            //điểm 1 N(0,Ry)
            point1.X = CenterPoint.X;
            point1.Y = Ry + CenterPoint.Y;
            list_points.Add(point1);
            //điểm 3 L(0,-Ry)
            point1.Y = -Ry + CenterPoint.Y;
            list_points.Add(point1);
            //điểm 2 M(Rx,0)
            point1.X = Rx + CenterPoint.X;
            point1.Y = CenterPoint.Y;
            list_points.Add(point1);
            //điểm 4 K(-Rx,0)
            point1.X = -Rx + CenterPoint.X;
            list_points.Add(point1);

            //Ry^2
            int R2y = Ry * Ry;
            //Rx^2
            int R2x = Rx * Rx;
            //p0
            int p = R2y - R2x * Ry - R2x / 4;

            //lặp 1
            while (R2y * point.X < R2x * point.Y)
            {
                if (p < 0)
                {
                    point.X += 1;
                    p += 2 * R2y * point.X + R2y;
                }
                else
                {
                    point.X += 1;
                    point.Y -= 1;
                    p += 2 * R2y * point.X - 2 * R2x * point.Y + R2y;
                }
                //lấy đối xứng và tịnh tiến
                //điểm A(x,y)
                Point temp = new Point();
                temp.X = point.X + CenterPoint.X;
                temp.Y = point.Y + CenterPoint.Y;
                list_points.Add(temp);
                //điểm B(-x,y)
                Point temp1 = new Point();
                temp1.X = -point.X + CenterPoint.X;
                temp1.Y = point.Y + CenterPoint.Y;
                list_points.Add(temp1);
                //điểm C(-x,-y)
                Point temp2 = new Point();
                temp2.X = -point.X + CenterPoint.X;
                temp2.Y = -point.Y + CenterPoint.Y;
                list_points.Add(temp2);
                //điểm D(x,-y)
                Point temp3 = new Point();
                temp3.X = point.X + CenterPoint.X;
                temp3.Y = -point.Y + CenterPoint.Y;
                list_points.Add(temp3);
            }
            //lặp 2
            p = R2y * (point.X + 1 / 2) * (point.X + 1 / 2) + R2x * (point.Y - 1) * (point.Y - 1) - R2x * R2y;
            while (point.Y != 0)
            {
                if (p > 0)
                {
                    point.Y -= 1;
                    p += -2 * R2x * point.Y + R2x;
                }
                else
                {
                    point.Y -= 1;
                    point.X += 1;
                    p += 2 * R2y * point.X - 2 * R2x * point.Y + R2x;
                }
                //lấy đối xứng và tịnh tiến
                //điểm A(x,y)
                Point temp = new Point();
                temp.X = point.X + CenterPoint.X;
                temp.Y = point.Y + CenterPoint.Y;
                list_points.Add(temp);
                //điểm B(-x,y)
                Point temp1 = new Point();
                temp1.X = -point.X + CenterPoint.X;
                temp1.Y = point.Y + CenterPoint.Y;
                list_points.Add(temp1);
                //điểm C(-x,-y)
                Point temp2 = new Point();
                temp2.X = -point.X + CenterPoint.X;
                temp2.Y = -point.Y + CenterPoint.Y;
                list_points.Add(temp2);
                //điểm D(x,-y)
                Point temp3 = new Point();
                temp3.X = point.X + CenterPoint.X;
                temp3.Y = -point.Y + CenterPoint.Y;
                list_points.Add(temp3);
            }
        }

        public override void showShape(OpenGL gl)
        {
            //tô màu
            FillShape(gl);

            //vẽ elip với tập hợp điểm
            DrawListPoint(list_points, gl);
        }

        public override void editShape(List<Point> vertices, List<Point> points, List<Point> controlPoints, float thick, Color color, Color color_fill, bool isFill)
        {
            base.editShape(vertices, points, controlPoints, thick, color, color_fill, isFill);
            list_points = new List<Point>(points);
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
                { //lấy seed là tâm đường tròn
                    Point seed = new Point(pStart.X, pStart.Y);
                    this.fill.Floodfill(seed.X, seed.Y, color_fill, color, gl);
                }
                int ymin = int.MaxValue, ymax = int.MinValue;
                int n = list_points.Count();
                for (int i = 0; i < n; i++)
                {
                    int ycur = list_points[i].Y;
                    if (ycur > ymax)
                    {
                        ymax = ycur;
                    }
                    if (ycur < ymin)
                    {
                        ymin = ycur;
                    }
                }
                Point A, B;
                for (int y = ymin + 1; y < ymax; y++)
                {
                    A = list_points.Find(c => c.Y == y);
                    B = list_points.Find(c => c.Y == y && c.X != A.X);
                    if (A.IsEmpty || B.IsEmpty)
                    {
                        continue;
                    }
                    gl.Color(color_fill.R / 255.0, color_fill.G / 255.0, color_fill.B / 255.0);
                    gl.Begin(OpenGL.GL_LINES);
                    gl.Vertex(A.X, gl.RenderContextProvider.Height - A.Y);
                    gl.Vertex(B.X, gl.RenderContextProvider.Height - B.Y);
                    gl.End();
                }
            }
        }


        public override void setControlPoints()
        {
            Point topLeft, topMid, topRight, midLeft, midRight, bottomLeft, bottomMid, bottomRight;
            topLeft = new Point(vertices[0].X - Rx, (vertices[0].Y -Ry));
            topMid = new Point(vertices[0].X, (vertices[0].Y - Ry));
            topRight = new Point(vertices[0].X + Rx, (vertices[0].Y - Ry));

            midLeft = new Point(vertices[0].X - Rx, vertices[0].Y);
            midRight = new Point(vertices[0].X + Rx, vertices[0].Y);

            bottomLeft = new Point(vertices[0].X - Rx, (vertices[0].Y + Ry));
            bottomMid = new Point(vertices[0].X, (vertices[0].Y + Ry));
            bottomRight = new Point(vertices[0].X + Rx, (vertices[0].Y + Ry));

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
            //trả về tâm ellipse
            return vertices[0];
        }
        public override short getShapeType()
        {
            return 2;
        }
    }
}

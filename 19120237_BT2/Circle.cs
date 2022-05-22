using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace _19120237_BT2
{
    class Circle : Shape
    {
        //tâm đường tròn là  pStart ở class Shape
        double radius;
        public Circle(List<Point> vertices, Point Start, Point End, float thick, Color color, Color color_fill, Boolean isFill) : base(vertices, Start, End, thick, color, color_fill, isFill)
        {

            //tạo list vertices chứa tâm và 1 điểm nằm trên đường tròn
            this.vertices = new List<Point>();
            this.vertices.Add(pStart);
            this.vertices.Add(pEnd);

            // tính toán các điểm thuộc đường tròn và đưa vào list_point
            calculateListPoints();

            //thiết lập các điểm điều khiển
            setControlPoints();
        }

        public override void calculateListPoints()
        {
            radius = Math.Sqrt((vertices[1].X - vertices[0].X) * (vertices[1].X - vertices[0].X) + (vertices[1].Y - vertices[0].Y) * (vertices[1].Y - vertices[0].Y));

            int r = (int)Math.Round(radius);
            //tập hợp điểm trên đường tròn
            if (list_points.Count() != 0)
            {
                list_points.Clear();
            }
            Point CenterPoint = new Point();
            CenterPoint.Y = vertices[0].Y;
            CenterPoint.X = vertices[0].X;

            //điểm đầu tiên
            Point point = new Point(0, r);
            //thêm 4 điểm nằm trên trục vào list
            Point point1 = new Point();
            //điểm 1 N(0,r)
            point1.X = CenterPoint.X;
            point1.Y = r + CenterPoint.Y;
            list_points.Add(point1);
            //điểm 3 L(0,-r)
            point1.Y = -r + CenterPoint.Y;
            list_points.Add(point1);
            //điểm 2 M(r,0)
            point1.X = r + CenterPoint.X;
            point1.Y = CenterPoint.Y;
            list_points.Add(point1);
            //điểm 4 K(-r,0)
            point1.X = -r + CenterPoint.X;
            list_points.Add(point1);

            int p = 5 / 4 - r;

            //lặp
            while (point.X < point.Y)
            {
                if (p < 0)
                {
                    point.X += 1;
                    p += 2 * point.X + 1;
                }
                else
                {
                    point.X += 1;
                    point.Y -= 1;
                    p += 2 * point.X - 2 * point.Y + 1;
                }
                //điểm I(x,y) tịnh tiến point và thêm vào list
                Point temp = new Point();
                temp.X = point.X + CenterPoint.X;
                temp.Y = point.Y + CenterPoint.Y;
                list_points.Add(temp);
                //7 biến là 7 điểm đối xứng với temp qua các trục x=0,y=0,y=-x,y=x
                //lấy 7 điểm đối xứng và thêm vào list
                //điểm 1 A(y,x)
                Point temp1 = new Point();
                temp1.X = point.Y + CenterPoint.X;
                temp1.Y = point.X + CenterPoint.Y;
                list_points.Add(temp1);
                //điểm 2 B(y,-x)
                Point temp2 = new Point();
                temp2.X = point.Y + CenterPoint.X;
                temp2.Y = -point.X + CenterPoint.Y;
                list_points.Add(temp2);
                //điểm 3 C(x,-y)
                Point temp3 = new Point();
                temp3.X = point.X + CenterPoint.X;
                temp3.Y = -point.Y + CenterPoint.Y;
                list_points.Add(temp3);
                //điểm 4 D(-x,-y)
                Point temp4 = new Point();
                temp4.X = -point.X + CenterPoint.X;
                temp4.Y = -point.Y + CenterPoint.Y;
                list_points.Add(temp4);
                //điểm 5 E(-y,-x)
                Point temp5 = new Point();
                temp5.X = -point.Y + CenterPoint.X;
                temp5.Y = -point.X + CenterPoint.Y;
                list_points.Add(temp5);
                //điểm 6 F(-y,x)
                Point temp6 = new Point();
                temp6.X = -point.Y + CenterPoint.X;
                temp6.Y = point.X + CenterPoint.Y;
                list_points.Add(temp6);
                //điểm 7 G(-x,y)
                Point temp7 = new Point();
                temp7.X = -point.X + CenterPoint.X;
                temp7.Y = point.Y + CenterPoint.Y;
                list_points.Add(temp7);
            }

        }
        public override void showShape(OpenGL gl)
        {
            //tô màu
            FillShape(gl);
            //vẽ đường tròn với tập hợp điểm
            DrawListPoint(list_points, gl);
        }

        public override void editShape(List<Point> vertices, List<Point> points, List<Point> controlPoints, float thick, Color color, Color color_fill, bool isFill)
        {
            base.editShape(vertices, points,controlPoints, thick, color, color_fill, isFill);
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
                { 
                    //lấy seed là tâm đường tròn
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
            int r = (int)radius;
            Point topLeft, topMid, topRight, midLeft, midRight, bottomLeft, bottomMid, bottomRight;
            topLeft = new Point(pStart.X - r, (pStart.Y - r));
            topMid = new Point(pStart.X, (pStart.Y - r));
            topRight = new Point(pStart.X + r, (pStart.Y - r));

            midLeft = new Point(pStart.X - r, pStart.Y);
            midRight = new Point(pStart.X + r, pStart.Y);

            bottomLeft = new Point(pStart.X - r, (pStart.Y + r));
            bottomMid = new Point(pStart.X, (pStart.Y + r));
            bottomRight = new Point(pStart.X + r, (pStart.Y + r));

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
            //trả về tâm đường tròn
            return vertices[0];
        }

        public override short getShapeType()
        {
            return 1;
        }
    }

}


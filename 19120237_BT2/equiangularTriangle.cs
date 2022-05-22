using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace _19120237_BT2
{
    class equiangularTriangle : Shape
    {
        // đỉnh C
        Point VertexC;
        Point VertexB;//đỉnh B
        Point TopVertex; //dỉnh A
        public equiangularTriangle(List<Point> vertices, Point Start, Point End, float thick, Color color, Color color_fill, Boolean isFill) : base(vertices, Start, End, thick, color, color_fill, isFill)
        {
            //cạnh của tam giác là khoảng cách khối theo x của pEnd,pStart
            double a = Math.Abs(pEnd.X - pStart.X);
            //chiều cao của tam giác
            double h = Math.Sqrt(3) * a / 2;

            int dy = pEnd.Y - pStart.Y;
            //nếu dy > 0 thì pEnd là đỉnh C, ngược lại thì pStart là đỉnh C
            if (dy > 0)
            {
                VertexC = pEnd;
                //đỉnh B
                VertexB.X = pStart.X;
                VertexB.Y = pEnd.Y;
            }
            else
            {
                VertexC = pStart;
                //đỉnh B
                VertexB.X = pEnd.X;
                VertexB.Y = pStart.Y;
            }

            //top.x = I.x với I là trung điểm cạnh đáy
            TopVertex.X = (int)Math.Round((VertexB.X + VertexC.X) / 2.0);
            //top.y = I.y - h, vì top nằm trên cùng nên top.y < I.y
            TopVertex.Y = -(int)Math.Round(h) + VertexB.Y;


            this.vertices = new List<Point>();
            this.vertices.Add(TopVertex);
            this.vertices.Add(VertexB);
            this.vertices.Add(VertexC);


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
            for (int i = 0; i < 2; i++)
            {
                DrawLine(vertices[i], vertices[i + 1], gl);
            }
            DrawLine(vertices[2], vertices[0], gl);
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
                    //lấy seed là điểm nằm trên trung điểm cạnh đáy
                    Point seed = new Point((VertexB.X + VertexC.X) / 2, gl.RenderContextProvider.Height - ((VertexB.Y + VertexC.Y) / 2) + 1);
                    this.fill.Floodfill(seed.X, seed.Y, color_fill, color, gl);
                }
                fill.ScanFill(vertices, color_fill, gl);
            }    
        }

        public override void setControlPoints()
        {
            Point topLeft, topMid, topRight, midLeft, midRight, bottomLeft, bottomMid, bottomRight;
            topLeft = new Point(VertexB.X, TopVertex.Y);
            topMid = new Point(TopVertex.X, TopVertex.Y);
            topRight = new Point(VertexC.X, TopVertex.Y);

            midLeft = new Point(VertexB.X, (int)Math.Round((VertexB.Y + TopVertex.Y) / 2.0));
            midRight = new Point(VertexC.X, (int)Math.Round((VertexB.Y + TopVertex.Y) / 2.0));

            bottomLeft = new Point(VertexB.X, VertexB.Y);
            bottomMid = new Point((int)Math.Round((VertexB.X + VertexC.X) / 2.0), VertexB.Y);
            bottomRight = new Point(VertexC.X, VertexB.Y);

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
            //trả về trọng tâm tam giác
            return new Point((vertices[0].X + vertices[1].X + vertices[2].X) / 3, (vertices[0].Y + vertices[1].Y + vertices[2].Y) / 3);
        }


        public override short getShapeType()
        {
            return 4;
        }

    }
}

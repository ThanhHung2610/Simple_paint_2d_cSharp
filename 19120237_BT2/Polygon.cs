using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace _19120237_BT2
{
    class Polygon:Shape
    {
        public Polygon(List<Point> vertices, Point Start, Point End, float thick, Color color, Color color_fill, Boolean isFill) : base(vertices, Start, End, thick, color, color_fill, isFill)
        {
            isPolygon = true;
            this.vertices = new List<Point>(vertices);

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
            int n = vertices.Count();
            if(n==0)
            {
                return;
            }    
            for (int i = 0; i < n - 1; i++)
            {
                DrawLine(vertices[i], vertices[i + 1], gl);
            }
            DrawLine(vertices[n - 1], vertices[0], gl);
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
            if (isFill)
            {
                if (false)
                {
                    //lấy seed là điểm nằm trên trung điểm cạnh đáy
                    Point seed = new Point();
                    this.fill.Floodfill(seed.X, seed.Y, color_fill, color, gl);
                }
                fill.ScanFill(vertices, color_fill, gl);
            }
        }


        public override void setControlPoints()
        {
            int n = vertices.Count();
            ctrl_Points = new List<Point>();
            for (int i = 0; i < n; i++)
            {
                Point vertex = new Point(vertices[i].X, vertices[i].Y);
                ctrl_Points.Add(vertex);
            }    
        }

        public override Point getCenter()
        {
            //tìm ymin,ymax
            int ymin = int.MaxValue, ymax = int.MinValue;
            //tìm xmin, xmax
            int xmin = int.MaxValue, xmax = int.MinValue;
            for (int i = 0; i < vertices.Count(); i++)
            {
                if (xmin > vertices[i].X)
                    xmin = vertices[i].X;
                if (xmax < vertices[i].X)
                    xmax = vertices[i].X;
                if (ymin > vertices[i].Y)
                    ymin = vertices[i].Y;
                if (ymax < vertices[i].Y)
                    ymax = vertices[i].Y;
            }

            return new Point((int)Math.Round((xmin + xmax) / 2.0), (int)Math.Round((ymin + ymax) / 2.0));
        }


        public override short getShapeType()
        {
            return 7;
        }
    }
}

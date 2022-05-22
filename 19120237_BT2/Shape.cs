 using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace _19120237_BT2
{
    class Shape
    {
        //danh sách các đỉnh
        protected List<Point> vertices;
        //điểm bắt đầu và kết thúc để xử lý vẽ hình còn lại
        protected Point pStart, pEnd;
        //biến độ dày
        protected float thick;
        //màu của hình
        protected Color color;
        //màu tô
        protected Color color_fill;
        //chế độ tô màu
        protected Boolean isFill;
        //danh sách điểm tạo nên hình
        protected List<Point> list_points;
        //danh sác điểm điều khiển
        public List<Point> ctrl_Points;
        //class tô màu
        protected Fill fill;

        protected Boolean isPolygon;
        //lưu góc mà hình đã được quay -> trạng thái sau khi xoay
        public double angleRotating;
        public Shape(List<Point> vertices,Point Start, Point End, float thick, Color color, Color color_fill, Boolean isFill)
        {
            isPolygon = false;
            pStart = Start;
            pEnd = End;
            this.thick = thick;
            this.color = color;
            this.color_fill = color_fill;
            this.isFill = isFill;
            fill = new Fill();
            list_points = new List<Point>();
            angleRotating = 0;
        }

        public virtual void calculateListPoints()
        {

        }
        public virtual void showShape(OpenGL gl)
        {

        }

        public virtual void FillShape(OpenGL gl)
        {

        }

        public virtual void editShape(List<Point> vertices, List<Point> points,List<Point> controlPoints, float thick, Color color, Color color_fill, Boolean isFill)
        {
            this.thick = thick;
            this.color = color;
            this.color_fill = color_fill;
            this.isFill = isFill;
            this.vertices = new List<Point>(vertices);
            this.ctrl_Points = new List<Point>(controlPoints);
        }

        public virtual void ShowEditShape(OpenGL gl)
        {
           
        }
        //tạo danh sách điểm điều khiển
        public virtual void setControlPoints()
        {
            
        }
        //vẽ điểm điều khiển 
        public void DrawSetControl(OpenGL gl)
        {
            int n = ctrl_Points.Count();
            if(n == 8 && isPolygon != true)
            {
                int GLHeight = gl.RenderContextProvider.Height;
                Color a = Color.Indigo;
                gl.Color(a.R / 255.0, a.R / 255.0, a.B / 255.0);
                gl.LineWidth(1f);
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.LineStipple(4, 0xAAAA);
                gl.Begin(OpenGL.GL_LINE_STRIP);
                gl.Vertex(ctrl_Points[0].X + 1, GLHeight - ctrl_Points[0].Y);
                gl.Vertex(ctrl_Points[2].X + 1, GLHeight - ctrl_Points[2].Y);
                gl.Vertex(ctrl_Points[4].X + 1, GLHeight - ctrl_Points[4].Y);
                gl.Vertex(ctrl_Points[6].X + 1, GLHeight - ctrl_Points[6].Y);
                gl.Vertex(ctrl_Points[0].X + 1, GLHeight - ctrl_Points[0].Y);
                gl.End();
                gl.Flush();
                gl.Disable(OpenGL.GL_LINE_STIPPLE);

                for (int i = 0; i < n; i++)
                {
                    DrawControlPoint(gl, ctrl_Points[i]);
                }
            }
            //nếu là đường thẳng hoặc đa giác
            else
            {
                for (int i = 0; i < n; i++)
                {
                    DrawControlPoint(gl, ctrl_Points[i]);
                }
            }
        }
        public void DrawControlPoint(OpenGL gl,Point p)
        {
            gl.Color(0f, 0f, 0f, 0);
            gl.PointSize(7f);
            gl.Begin(OpenGL.GL_POINTS);
            gl.Vertex(p.X, gl.RenderContextProvider.Height - p.Y);
            gl.End();
            gl.Flush();

            Color c = new Color();
            c = Color.White;
            Rectangle rec = new Rectangle(null, new Point(p.X - 3, p.Y - 3), new Point(p.X + 3, p.Y + 3), 1f, c, c, false);
            rec.showShape(gl);
        }

        public void DrawListPoint(List<Point> points, OpenGL gl)
        {
            gl.Color(color.R / 255.0, color.G / 255.0, color.B / 255.0);
            gl.PointSize(thick);
            gl.Begin(OpenGL.GL_POINTS);
            for (int i = 0; i < points.Count(); i += 1)
            {
                gl.Vertex(points[i].X, gl.RenderContextProvider.Height - points[i].Y);
            }
            gl.End();
            gl.Flush();// Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
        }
        /*
        public void DrawLine(Point Start, Point End, OpenGL gl)
        {
            List<Point> points = new List<Point>();
            int dx = End.X - Start.X;
            int dy = End.Y - Start.Y;
            int stepX, stepY;
            Point temp = new Point(Start.X, gl.RenderContextProvider.Height - Start.Y);
            points.Add(temp);


            //xét dấu dx,dy
            if (dx < 0)
            {
                dx *= -1;
                stepX = -1;
            }
            else
            {
                stepX = 1;
            }
            if (dy < 0)
            {
                dy *= -1;
                stepY = -1;
            }
            else
            {
                stepY = 1;
            }

            //tìm tập điểm của đoạn thẳng
            //nếu |m| < 1
            if (dx > dy)
            {
                int p = 2 * dy - 2 * dx;
                while (temp.X != End.X)
                {
                    if (p < 0)
                    {
                        p += 2 * dy;
                    }
                    else
                    {
                        //nhân -1 cho stepY bởi vì tọa độ y khi vẽ = OpenGLControl.Height - Y
                        temp.Y += -1 * stepY;
                        p += 2 * dy - 2 * dx;
                    }
                    temp.X += stepX;
                    points.Add(temp);
                }

            }
            //nếu |m|>1
            else
            {
                int p = 2 * dx - 2 * dy;
                while (temp.Y != gl.RenderContextProvider.Height - End.Y)
                {
                    if (p < 0)
                    {
                        p += 2 * dx;
                    }
                    else
                    {
                        temp.X += stepX;
                        p += 2 * dx - 2 * dy;
                    }
                    //nhân -1 cho stepY bởi vì tọa độ y khi vẽ = OpenGLControl.Height - Y
                    temp.Y += -stepY;
                    points.Add(temp);
                }
            }

            //Vẽ đoạn thẳng
            DrawListPoint(points, gl);
        */
        public void DrawLine(Point Start, Point End, OpenGL gl)
        {
            int dx = End.X - Start.X;
            int dy = End.Y - Start.Y;
            int stepX, stepY;
            Point temp = new Point(Start.X, Start.Y);
            list_points.Add(temp);


            //xét dấu dx,dy
            if (dx < 0)
            {
                dx *= -1;
                stepX = -1;
            }
            else
            {
                stepX = 1;
            }
            if (dy < 0)
            {
                dy *= -1;
                stepY = -1;
            }
            else
            {
                stepY = 1;
            }

            //tìm tập điểm của đoạn thẳng
            //nếu |m| < 1
            if (dx > dy)
            {
                int p = 2 * dy - 2 * dx;
                while (temp.X != End.X)
                {
                    if (p < 0)
                    {
                        p += 2 * dy;
                    }
                    else
                    {
                        temp.Y += stepY;
                        p += 2 * dy - 2 * dx;
                    }
                    temp.X += stepX;
                    list_points.Add(temp);
                }

            }
            //nếu |m|>1
            else
            {
                int p = 2 * dx - 2 * dy;
                while (temp.Y != End.Y)
                {
                    if (p < 0)
                    {
                        p += 2 * dx;
                    }
                    else
                    {
                        temp.X += stepX;
                        p += 2 * dx - 2 * dy;
                    }
                    temp.Y += stepY;
                    list_points.Add(temp);
                }
            }

            //Vẽ đoạn thẳng
            DrawListPoint(list_points, gl);
        }
        //kiểm tra điểm nó nằm trong hình không
        public bool isInShape(Point cur)
        {
            //nếu cur nằm ngoài shape return false
            if (!list_points.Exists(c => c.Y > cur.Y && c.X == cur.X) || !list_points.Exists(c => c.Y < cur.Y && c.X == cur.X))
            {
                return false;
            }
            if (!list_points.Exists(c => c.X > cur.X && c.Y == cur.Y) || !list_points.Exists(c => c.X < cur.X && c.Y == cur.Y))
            {
                return false;
            }
            //cur nằm trong shape
            return true;
        }
        //kiểm tra điểm có nằm trên hình (nằm trên cạnh)
        public virtual bool isOnShape(Point cur)
        {
            Point current = new Point(cur.X, cur.Y);
            if (isCtrlPoint(cur) != -1)
            {
                return false;
            }
            return list_points.Contains(current);
        }

        //trả về index nếu thành công, ngược lại trả về -1
        public int isCtrlPoint(Point cur)
        {
            int n = ctrl_Points.Count();
            for (int i = 0; i < n; i++)
            {
                if (Math.Abs(ctrl_Points[i].X - cur.X) < 5 && Math.Abs(ctrl_Points[i].Y - cur.Y) < 5)
                {
                    return i;
                }
            }
            return -1;
        }        

        public List<Point> getVertices()
        {
            return (vertices == null) ? new List<Point>() : new List<Point>(vertices);
        }    

        public void setVertices(List<Point> vertices)
        {
            this.vertices = new List<Point>(vertices);
        }

        public List<Point> getPoints()
        {
            return (list_points == null) ? new List<Point>() : new List<Point>(list_points);
        }

        public List<Point> getCtrlPoints()
        {
            return (ctrl_Points == null) ? new List<Point>() : new List<Point>(ctrl_Points);
        }

        public float getThick()
        {
            return thick;
        }

        public Color GetColor()
        {
            return color;
        }

        public Color getFillColor()
        {
            return color_fill;
        }

        public Boolean getFillVal()
        {
            return isFill;
        }
        //trả về tâm của hình -> nhằm rotate và scale
        public virtual Point getCenter()
        {
            return new Point();
        }

        public virtual short getShapeType()
        {
            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using SharpGL;

namespace _19120237_BT2
{


    struct RGBColor
    {
        public byte r, g, b;
        public RGBColor(Color color)
        {
            this.r = color.R;
            this.g = color.G;
            this.b = color.B;
        }
        public RGBColor(byte[] color)
        {
            this.r = color[0];
            this.g = color[1];
            this.b = color[2];
        }
    }

    struct Edge
    {
        public Point plower, pupper;
        public Edge(Point Lower, Point Upper)
        {
            plower = Lower;
            pupper = Upper;
        }
    }


    //active edge list
    class AEL
    {
        public int y_upper;
        public float x_int, reci_slope;
        public AEL next;

        public AEL(AEL a)
        {
            y_upper = a.y_upper;
            x_int = a.x_int;
            reci_slope = a.reci_slope;
            next = a.next;
        }
        public AEL(int y_upper, float x_int, float reci_slope, AEL next = null)
        {
            this.y_upper = y_upper;
            //giao điểm giữa đường quét và cạnh
            this.x_int = x_int;
            //nghich đảo độ dốc
            this.reci_slope = reci_slope;
            this.next = next;
        }
    }



    class Fill
    {
        //Tô loang
        bool IsSameColor(RGBColor cl1, RGBColor cl2)
        {
            if (cl1.r == cl2.r && cl1.g == cl2.g && cl1.b == cl2.b)
                return true;
            return false;
        }
        RGBColor  GetPixel(int x,int y,OpenGL gl)
        {
            byte []pixelData = new byte[3];
            gl.ReadPixels(x, y, 1, 1, OpenGL.GL_RGB, OpenGL.GL_BYTE, pixelData);

            RGBColor color = new RGBColor(pixelData);

            return color;
        }

        void PutPixel(int x,int y,Color color,OpenGL gl)
        {
            byte[] ptr = new byte[3];
            ptr[0] = color.R;
            ptr[1] = color.G;
            ptr[2] = color.B;
            gl.RasterPos(x, y);
            gl.DrawPixels(1, 1, OpenGL.GL_RGB, ptr);
            gl.Flush();
        }
        public void Floodfill(int x, int y, Color fill_color, Color Boundary_color, OpenGL gl)
        {
            RGBColor currentColor = new RGBColor();
            RGBColor F_Color = new RGBColor(fill_color);
            RGBColor B_Color = new RGBColor(Boundary_color);
            Stack<Point> st = new Stack<Point>();
            st.Push(new Point(x, y));
            RGBColor Seed_color = GetPixel(x, y, gl);
            //kiểm tra nếu đã tô ảnh nghĩa là màu seed giống màu tô thì dừng
            if (IsSameColor(F_Color, Seed_color))
                return;
            while (st.Count() != 0)
            {
                Point p;// = new Point();
                p  = st.Pop();
                PutPixel(p.X, p.Y, fill_color, gl);

                for(int i = 0;i < 4; i++)
                {
                    Point pO = new Point();
                    if (i == 0)
                    {
                        pO = new Point(p.X, p.Y - 1);
                    }
                    else if (i == 1)
                    {
                        pO = new Point(p.X + 1, p.Y);
                    }
                    else if (i == 2)
                    {
                        pO = new Point(p.X, p.Y + 1);
                    }
                    else if (i == 3)
                    {
                        pO = new Point(p.X - 1, p.Y);
                    }

                    currentColor = GetPixel(pO.X, pO.Y, gl);
                    if (IsSameColor(currentColor, Seed_color))
                    {
                        st.Push(pO);
                    }
                }    

            }
        }

        public void ScanFill(List<Point> vertices,Color fill_color,OpenGL gl)
        {
            //xử lí dữ liệu đầu vào
            int nVertices = vertices.Count();

            if (nVertices < 3)
            {
                return;
            }

            List<Edge> edges = new List<Edge>();
            List<AEL> aels = new List<AEL>();


            int height = gl.RenderContextProvider.Height;

            for(int i = 0; i < nVertices; i++)
            {
                Point Lower = new Point();
                Point Upper = new Point();
                float reci_slope;

                Point prev, cur, next_cur, after_next;

                cur = vertices[i];

                //nếu là đỉnh đầu tiên thì đỉnh trước nó là đỉnh cuối cùng
                if (i == 0)
                {
                    prev = vertices[nVertices - 1];
                }
                else prev = vertices[i - 1];

                //nếu đỉnh đang xét là đỉnh cuối cùng thì đỉnh sau nó là đỉnh cuối cùng
                if (i == nVertices - 1)
                {
                    next_cur = vertices[0];
                }
                else next_cur = vertices[i + 1];

                //nếu đỉnh kế của next
                if (i + 2 > nVertices - 1)
                {
                    after_next = vertices[i + 2 - nVertices];
                }
                else after_next = vertices[i + 2];

                //xét cạnh Pcur - Pnext_cur
                //nếu // với Ox thì bỏ qua cạnh này
                if (cur.Y == next_cur.Y)
                {
                    continue;
                }

                if (cur.Y > next_cur.Y)
                {
                    Upper.X = cur.X;
                    Upper.Y = cur.Y;

                    Lower.X = next_cur.X;
                    Lower.Y = next_cur.Y;

                    //kiểm tra đỉnh cực trị
                    if (cur.Y <= prev.Y)
                    {
                        //làm ngắn cạnh
                        Upper.Y -= 1;
                    }
                }
                else
                {
                    Upper.X = next_cur.X;
                    Upper.Y = next_cur.Y;

                    Lower.X = cur.X;
                    Lower.Y = cur.Y;

                    //kiểm tra đỉnh cực trị
                    if (next_cur.Y <= after_next.Y)
                    {
                        Upper.Y -= 1;
                    }
                }
                //nghịch đảo độ dốc
                reci_slope = (Upper.X - Lower.X) * 1f / (Upper.Y - Lower.Y);

                edges.Add(new Edge(Lower, Upper));
                aels.Add(new AEL(Upper.Y, Lower.X, reci_slope));
            }


            //xác định Ymin,Ymax
            int ymin = int.MaxValue;
            int ymax = int.MinValue;
            for (int i = 0; i < nVertices; i++)
            {
                if (ymin > vertices[i].Y)
                {
                    ymin = vertices[i].Y;
                }
                if (ymax < vertices[i].Y)
                {
                    ymax = vertices[i].Y;
                }
            }
            //tạo bảng ET
            //Edge table
            List<List<AEL>> ET = new List<List<AEL>>();
            for(int i = 0; i < ymax - ymin; i++)
            {
                List<AEL> a = new List<AEL>();
                ET.Add(a);
            }

            //index 0 trong ET ứng với y = ymin
            int k = 0;
            int nEdges = edges.Count();
            for (int y = ymin; y < ymax; y++)
            {
                for(int i = 0; i < nEdges; i++)
                {
                    //nếu y_lower của cạnh bằng y dòng 
                    if(edges[i].plower.Y == y)
                    {
                        ET[k].Add(new AEL(aels[i]));
                    }
                }
                k++;
            }



            List<AEL> beglist = new List<AEL>();
            k = 0;
            //thực hiện tô quét
            for (int y = ymin; y < ymax; y++)
            {
                if(ET[k].Count() != 0)
                {
                    for (int i = 0; i < ET[k].Count(); i++)
                    {
                        beglist.Add(ET[k][i]);
                    }
                }
                if (beglist.Count() != 0)
                {
                    //sắp xếp
                    List<float> x_int_list = new List<float>();
                    for (int i = 0; i < beglist.Count(); i++)
                    {
                        x_int_list.Add(beglist[i].x_int);
                    }
                    //sắp xếp x theo thứ tự tăng dần
                    x_int_list.Sort();
                    //tô màu theo cặp giao điểm lẻ chẵn
                    if (x_int_list.Count() % 2 == 0)
                    {
                        for (int i = 0; i < x_int_list.Count(); i += 2)
                        {
                            Point st = new Point((int)Math.Round(x_int_list[i]), y);
                            Point end = new Point((int)Math.Round(x_int_list[i + 1]), y);

                            gl.Color(fill_color.R / 255.0, fill_color.G / 255.0, fill_color.B / 255.0);
                            gl.Begin(OpenGL.GL_LINES);
                            gl.Vertex(st.X, gl.RenderContextProvider.Height - st.Y);
                            gl.Vertex(end.X, gl.RenderContextProvider.Height - end.Y);
                            gl.End();
                        }
                    }

                    //loại bỏ cạnh có y = y_upper và cập nhật giá trị x_int
                    for (int i = 0; i < beglist.Count(); i++)
                    {
                        //nếu y_upper = y thì xóa khỏi beglist
                        if (beglist[i].y_upper == y)
                        {
                            beglist.Remove(beglist[i]);
                            i--;
                        }
                        //nếu không thì tăng x_int
                        else
                        {
                            beglist[i].x_int += beglist[i].reci_slope;
                        }
                    }
                }
                k++;
            }
        }    
    
    
    }
}

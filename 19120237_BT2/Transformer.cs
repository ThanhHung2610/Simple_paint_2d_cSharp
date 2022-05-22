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
using System.Drawing.Drawing2D;
namespace _19120237_BT2
{
    class Transformer
    {
        double[][] affineMatrix;
        //scale
        public Transformer(double sx, double sy, Point center)
        {
            affineMatrix = new double[3][];
            affineMatrix[0] = new double[]{ sx,0,-center.X*sx + center.X};
            affineMatrix[1] = new double[] { 0, sy, -center.Y * sy + center.Y };
            affineMatrix[2] = new double[] { 0, 0, 1 };
        }
        //rotate
        public Transformer(double teta,Point center)
        {
            affineMatrix = new double[3][];
            double cos = Math.Cos(teta), sin = Math.Sin(teta);
            affineMatrix[0] = new double[] {cos, -sin, -center.X * cos + center.Y*sin + center.X };
            affineMatrix[1] = new double[] {sin, cos, -center.X * sin - center.Y*cos + center.Y };
            affineMatrix[2] = new double[] { 0, 0, 1 };
        }
        //translate
        public Transformer(int dx,int dy)
        {
            affineMatrix = new double[3][];
            affineMatrix[0] = new double[] { 1, 0, dx };
            affineMatrix[1] = new double[] { 0, 1, dy };
            affineMatrix[2] = new double[] { 0, 0, 1 };
        }
        public Point TransformPoint(Point p)
        {
            Point result = new Point();
            result.X = (int)Math.Round(affineMatrix[0][0] * p.X + affineMatrix[0][1] * p.Y + affineMatrix[0][2]); 
            result.Y = (int)Math.Round(affineMatrix[1][0] * p.X + affineMatrix[1][1] * p.Y + affineMatrix[1][2]); 
            return result;
        }

        public List<Point> TransformListPoint(List<Point> lp)
        {
            int n = lp.Count();
            List<Point> result = new List<Point>();
            for(int i = 0; i < n; i++)
            {
                result.Add(TransformPoint(lp[i]));
            }
            return result;
        }

    }
}

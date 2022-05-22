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
    class Paint
    {
        public Color colorUserColor; //màu để vẽ hình
        //biến để nhận hình vẽ
        public short shShape; // 0 là đường thẳng,
                              // 1 là đường tròn,
                              // 2 là hình elip,
                              // 3 là hình chữ nhật,
                              // 4 là hình tam giác đều,
                              // 5 là ngũ giác đều,
                              // 6 là lục giác đều,
                              // 7 là đa giác
        // lưu lại loại hình đang vẽ khi edit
        public short oldShape;
        public Point pStart, pEnd;
        public bool isDrawing;     //cờ thông báo là đang vẽ hay không
        public bool isDrawDone;    //cờ thông báo là vẽ xong hay chưa
        public bool isEdit;        //cờ thông báo là đang chỉnh sửa hay không
        public bool isFree;        //cờ thông báo có đang ở chế độ nào không
        public float thick; //độ dày của nét
        public bool isClear; //yêu cầu xóa framebuffer
        public bool isRemove;   //yêu cầu hoàn tác
        public bool isSelect; //yêu cầu chọn shape
        public int indexEdit; //index của hình được chọn để chỉnh sửa, mặc định bằng -1

        public double timeSpan;


        //màu tô
        public Color color_fill;
        //chế độ tô màu
        public Boolean isFill;

        //buffer hình
        public List<Shape> shapes;
        // hình đang được thao tác hiện tại
        public Shape shape;

        // công cụ đo thời gian
        public Stopwatch stopwatch;

        //class affine
        public Transformer transformer;
        //danh sách các đỉnh 
        public List<Point> vertices;
        //số đỉnh đa giác
        public int nVertices;

        public List<Point> vertices_transform;

        //danh sách các điểm thuộc hình
        public List<Point> points;

        public List<Point> points_transform;
        //danh sách các điểm điều khiển
        public List<Point> controlPoints;

        public List<Point> controlPoints_transform;
        // 
        public Point oldPos;
        //cờ báo tịnh  tiện shape
        public bool isTranslate;
        //cờ báo scale shape
        public bool isScale;
        //cờ báo quay shape
        public bool isRotate;
        //cờ báo move 1 điểm điều khiển -> đối với đoạn thẳng và đa giác
        public bool isMove1CtrlPoint;
        //trong tâm của hình
        Point Center;
        Point Center_transform;

        public int indexCtrlPoint;
        //lưu góc khi thực hiện xoay trong khi edit
        double angle;

        // cờ báo hiệu mouseDown -> để thực hiện thao tác rotate khi edit
        public bool isDown;
        public Paint()
        {
            //màu mặc định là màu trắng, vì nền đen
            colorUserColor = Color.White;
            //màu tô mặc định là màu xanh lá
            color_fill = Color.Green;
            //mặc định vẽ đoạn thẳng
            shShape = 0;

            isDrawing = false;
            isDrawDone = false;
            isEdit = false;
            isFree = true;

            thick = 1f;
            //tạo list shape rỗng
            shapes = new List<Shape>();
            shape = null;

            isClear = false;
            isRemove = false;
            isSelect = false;
            indexEdit = -1;

            stopwatch = new Stopwatch();
            timeSpan = 0;
            isFill = false;

            vertices = new List<Point>();
            nVertices = 0;
            points = new List<Point>();
            controlPoints = new List<Point>();

            transformer = null;

            isTranslate = false;
            isScale = false;
            isRotate = false;
            isDown = false;
            isMove1CtrlPoint = false;
        }

        //vẽ danh sách đối tượng hình
        public void ShowListShapes(OpenGL gl)
        {
            int n = shapes.Count();
            for (int i = 0; i < n; i++)
            {
                shapes[i].showShape(gl);
            }
        }

        //nếu user yêu cầu làm mới ->  reset các giá trị về giá trị mặc định
        public void handleClear()
        {
            if (isClear)
            {
                if (isEdit)
                {
                    shape = null;
                    isEdit = false;
                    isFree = true;
                }
                shapes.Clear();
                isClear = false;
                indexEdit = -1;
                if (vertices.Count() != 0)
                {
                    vertices.Clear();
                }
                nVertices = 0;
            }
        }

        public void handleRemove()
        {
            if (isRemove)
            {
                //Nếu người dùng yêu cầu xóa hình đã chọn hay hình vẽ cuối cùng
                if (isFree)
                {
                    if (shapes.Count() > 0)
                    {
                        shapes.RemoveAt(shapes.Count() - 1);
                    }
                }
                if (isEdit)
                {
                    if (indexEdit != -1)
                    {
                        shapes.RemoveAt(indexEdit);
                        indexEdit = -1;
                    }
                    shape = null;
                    isEdit = false;
                    isFree = true;
                }
                isRemove = false;
                if (vertices.Count() != 0)
                {
                    vertices.Clear();
                }
                nVertices = 0;
            }
        }
        //nếu đang thao tác trên shape -> cập nhật shape
        public void createShape()
        {
            if (shShape == 0)
            {
                shape = new Line(vertices, pStart, pEnd, thick, colorUserColor, color_fill, isFill);
            }
            else if (shShape == 1)
            {
                shape = new Circle(vertices, pStart, pEnd, thick, colorUserColor, color_fill, isFill);
            }
            else if (shShape == 2)
            {
                shape = new Ellipse(vertices, pStart, pEnd, thick, colorUserColor, color_fill, isFill);
            }
            else if (shShape == 3)
            {
                shape = new Rectangle(vertices, pStart, pEnd, thick, colorUserColor, color_fill, isFill);
            }
            else if (shShape == 4)
            {
                shape = new equiangularTriangle(vertices, pStart, pEnd, thick, colorUserColor, color_fill, isFill);
            }
            else if (shShape == 5)
            {
                shape = new RegularPentagon(vertices, pStart, pEnd, thick, colorUserColor, color_fill, isFill);
            }
            else if (shShape == 6)
            {
                shape = new RegularHexagon(vertices, pStart, pEnd, thick, colorUserColor, color_fill, isFill);
            }
            else if (shShape == 7)
            {
                shape = new Polygon(vertices, pStart, pEnd, thick, colorUserColor, color_fill, isFill);
            }
        }

        public void handleDrawing(OpenGL gl)
        {
            createShape();
            //vẽ và đo thời gian khi vẽ
            stopwatch.Start();
            shape.showShape(gl);
            stopwatch.Stop();

            //tính khoảng thời gian vẽ
            timeSpan = stopwatch.Elapsed.TotalMilliseconds * 1000;
            timeSpan /= 1000;
            getProperties(shape);
        }
        public void handleEdit(OpenGL gl)
        {
            shape.editShape(vertices_transform, points_transform, controlPoints_transform, thick, colorUserColor, color_fill, isFill);
            shape.ShowEditShape(gl);
        }

        public void handleDrawDone()
        {
            if (indexEdit == -1)
            {
                shapes.Add(shape);
                shape = null;
            }
            else indexEdit = -1;
            //reset vertices
            if (vertices.Count() != 0)
            {
                vertices.Clear();
            }
            nVertices = 0;

            isDrawDone = false;
            isFree = true;
        }

        //xử lí các thao tác người dùng
        public void HandlePaint(OpenGL gl)
        {
            if (isDrawing || isEdit || isDrawDone)
            {
                if (shape != null && isDrawing == false)
                {
                    shape.showShape(gl);
                }
                if (isDrawing)
                    handleDrawing(gl);
                if (isEdit)
                    handleEdit(gl);
                if (isDrawDone)
                    handleDrawDone();

                stopwatch.Reset();
            }
        }

        public void getProperties(Shape sh)
        {
            vertices = sh.getVertices();
            vertices_transform = sh.getVertices();

            points = sh.getPoints();
            points_transform = sh.getPoints();

            controlPoints = sh.getCtrlPoints();
            controlPoints_transform = sh.getCtrlPoints();

            thick = sh.getThick();
            colorUserColor = sh.GetColor();
            color_fill = sh.getFillColor();
            isFill = sh.getFillVal();

            Center = shape.getCenter();
            Center_transform = shape.getCenter();

            oldShape = shShape;
            shShape = sh.getShapeType();
        }

        public void turnOffActiveMode()
        {
            if (isDrawing || isEdit)
            {
                isDrawDone = true;
                isDrawing = false;
                isEdit = false;
            }

            if (isSelect)
            {
                isSelect = false;
            }

            if (isRotate)
            {
                isRotate = false;
            }
        }

        public void handleTranslate()
        {
            //thực hiện affine lên tập đỉnh, tập điểm điều khiển, tập điểm (nếu cần)
            vertices_transform = transformer.TransformListPoint(vertices);
            controlPoints_transform = transformer.TransformListPoint(controlPoints);
            if (shShape == 1 || shShape == 2)
            {
                points_transform = transformer.TransformListPoint(points);
            }
            Center_transform = transformer.TransformPoint(Center);
        }

        public void finishTransform()
        {
            vertices = vertices_transform;
            controlPoints = controlPoints_transform;
            points = points_transform;
            Center = Center_transform;
            if (isRotate) 
                shape.angleRotating += angle;
        }


        public void handleScale(Point cur)
        {
            //vector cur_center
            Point VCC = new Point(cur.X - Center.X, cur.Y - Center.Y);
            double lenVCC = Math.Sqrt(VCC.X * VCC.X + VCC.Y * VCC.Y);
            //vector old_center
            Point VOC = new Point(oldPos.X - Center.X, oldPos.Y - Center.Y);
            double lenVOC = Math.Sqrt(VOC.X * VOC.X + VOC.Y * VOC.Y);

            double sx, sy;
            //các đa giác đều và hình tròn -> scale có Sx = Sy -> không bị biến dạng
            if (shShape == 1 || shShape == 4 || shShape == 5 || shShape == 6)
            {
                sy = sx = lenVCC / lenVOC;
            }
            else if (shShape == 3 || shShape == 2)
            {
                if (indexCtrlPoint == 1 || indexCtrlPoint == 5)
                {
                    sx = 1;
                    //sy = VCC.Y * 1.0 / VOC.Y;
                    sy = lenVCC / lenVOC;
                }
                //nếu là mid_left hay mid_right -> scale theo phương ngang
                else if (indexCtrlPoint == 7 || indexCtrlPoint == 3)
                {
                    //sx = VCC.X * 1.0 / VOC.X;
                    sx = lenVCC / lenVOC;
                    sy = 1;
                }
                // nếu là các điểm ở cac góc
                else
                {
                    sx = VCC.X * 1.0 / VOC.X;
                    sy = VCC.Y * 1.0 / VOC.Y;
                }
            }
            else
            {
                sx = VCC.X * 1.0 / VOC.X;
                sy = VCC.Y * 1.0 / VOC.Y;
            }

            //scale
            transformer = new Transformer(sx, sy, Center);
            //thực hiện affine lên tập đỉnh, tập điểm điều khiển, tập điểm (nếu cần)
            if (shShape == 2)
            {
                Transformer am = new Transformer(-shape.angleRotating, Center);
                List<Point> TempVertices = am.TransformListPoint(vertices);
                TempVertices = transformer.TransformListPoint(TempVertices);
                //tạo mới elip nếu scale
                Shape tempElip = new Ellipse(null, TempVertices[0], TempVertices[1], thick, colorUserColor, color_fill, isFill);
                List<Point> TempPoints = tempElip.getPoints();
                //tạo mới các điểm điều khiển tương ứng với elip mới
                shape.setControlPoints();
                List<Point> TempCtrlPoints = tempElip.getCtrlPoints();
                //xoay listPoints và list ControlPoints theo góc angleRotating
                transformer = new Transformer(shape.angleRotating, Center);
                vertices_transform = transformer.TransformListPoint(TempVertices);
                points_transform = transformer.TransformListPoint(TempPoints);
                controlPoints_transform = transformer.TransformListPoint(TempCtrlPoints);
            }
            else if (shShape == 3)
            {
                Transformer am = new Transformer(-shape.angleRotating, Center);
                List<Point> TempVertices = am.TransformListPoint(vertices);
                TempVertices = transformer.TransformListPoint(TempVertices);
                //tạo mới hcn nếu scale
                Shape tempRec = new Rectangle(null, TempVertices[0], TempVertices[2], thick, colorUserColor, color_fill, isFill);
                
                List<Point> TempCtrlPoints = tempRec.getCtrlPoints();
                //xoay listPoints và list ControlPoints theo góc angleRotating
                transformer = new Transformer(shape.angleRotating, Center);
                vertices_transform = transformer.TransformListPoint(TempVertices);
                controlPoints_transform = transformer.TransformListPoint(TempCtrlPoints);
            }   
            else
            {
                vertices_transform = transformer.TransformListPoint(vertices);
                controlPoints_transform = transformer.TransformListPoint(controlPoints);
                if (shShape == 1)
                {
                    shape.setVertices(vertices_transform);
                    shape.calculateListPoints();
                    points_transform = shape.getPoints();
                }
            }
        }

        public void handleRotate(Point cur)
        {
            //vector cur_center
            Point VCC = new Point(cur.X - Center.X, cur.Y - Center.Y);
            double lenVCC = Math.Sqrt(VCC.X * VCC.X + VCC.Y * VCC.Y);
            //vector old_center
            Point VOC = new Point(oldPos.X - Center.X, oldPos.Y - Center.Y);
            double lenVOC = Math.Sqrt(VOC.X * VOC.X + VOC.Y * VOC.Y);


            //tính toán góc
            angle = Math.Acos((VCC.X * VOC.X + VCC.Y * VOC.Y) / (lenVCC * lenVOC));
            //ngược chiều kim đồng hồ -> cur nằm bên trái VOC -> xét thành phần z của tích có hướng của VCC và VOC
            if (VOC.X * VCC.Y - VOC.Y * VCC.X < 0)
            {
                angle *= -1;
            }


            transformer = new Transformer(angle, Center);
            vertices_transform = transformer.TransformListPoint(vertices);
            if (shShape == 1 || shShape == 2)
            {
                points_transform = transformer.TransformListPoint(points);
            }
            controlPoints_transform = transformer.TransformListPoint(controlPoints);
        }

        public void handleMove1CtrlPoint(Point cur)
        {
            vertices_transform[indexCtrlPoint] = cur;
            controlPoints_transform[indexCtrlPoint] = cur;
        }
    }
}

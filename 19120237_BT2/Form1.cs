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
    public partial class ShapeGL : Form
    {
        Paint painter;
        public ShapeGL()
        {
            InitializeComponent();

            painter = new Paint();


            pb_color.BackColor = painter.colorUserColor;
            pb_fillColor.BackColor = painter.color_fill;
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            // Clear the color and depth buffer.
            //gl.ClearColor(1f, 1f, 1f, 0);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //nếu user yêu cầu làm mới 
            painter.handleClear();

            //nếu user yêu cầu xóa
            painter.handleRemove();

            //Vẽ list Shape
            painter.ShowListShapes(gl);


            Stopwatch stopwatch = new Stopwatch();
            // xử lí yêu cầu vẽ
            painter.HandlePaint(gl);
        }

        private void openGLControl_OpenGLInit(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the clear color.
            gl.ClearColor(0, 0, 0, 0);
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();

        }

        private void openGLControl_OpenGLResized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
            // Create a perspective transformation.
            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);
            gl.Ortho2D(0, openGLControl.Width, 0, openGLControl.Height);
        }
        private void bt_BangMau_Click(object sender, EventArgs e)
        {
            //Nếu người dùng chọn xong và bấm OK
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                painter.colorUserColor = colorDialog1.Color;
                pb_color.BackColor = painter.colorUserColor;
            }
        }
        private void bt_fillColor_Click(object sender, EventArgs e)
        {
            //Nếu người dùng chọn xong và bấm OK
            if (colorDialog2.ShowDialog() == DialogResult.OK)
            {
                painter.color_fill = colorDialog2.Color;
                pb_fillColor.BackColor = painter.color_fill;
            }
        }
        private void ctrl_OpenGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (painter.isTranslate)
            {
                painter.finishTransform();
                painter.isTranslate = false;
            }

            // nếu mode rotate tắt thì bật mode Scale
            if (painter.isScale)
            {
                painter.finishTransform();
                painter.isScale = false;
            }
            if (painter.isRotate && painter.isDown)
            {
                painter.finishTransform();
                painter.isDown = false;
                painter.isRotate = false;
            }
            if (painter.isMove1CtrlPoint)
            {
                painter.finishTransform();
                painter.isMove1CtrlPoint = false;
            }
            if (painter.shShape != 7)
            {
                String DrawingTime = String.Format("{0:0.000}", painter.timeSpan);
                tb_Timer.Text = "Drawing time: " + DrawingTime + " ms";
                if (painter.isDrawing)
                {
                    painter.isDrawing = false;
                    painter.isEdit = true;
                }
            }
        }

        private void ctrl_OpenGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (painter.isSelect)
            {
                //kiểm tra nếu ở chế độ select và nhấp chuôt ngoài hình
                bool flagOut = true;
                Point current = e.Location;
                int maxIndex = painter.shapes.Count() - 1;
                for (int i = maxIndex; i > -1 ; i--)
                {
                    if (painter.shapes[i].isInShape(current) || painter.shapes[i].isOnShape(current))
                    {
                        painter.shape = painter.shapes[i];
                        painter.getProperties(painter.shape);
                        painter.indexEdit = i;
                        //bật cờ edit và tắt cờ rảnh rỗi
                        painter.isEdit = true;
                        painter.isFree = false;

                        flagOut = false;
                        painter.isSelect = false;
                        break;
                    }
                }
                if (flagOut)
                {
                    return;
                }
            }
            //kiểm tra nếu bắt đầu vẽ thì bật cờ isDrawing
            if (painter.isFree)
            {
                if (painter.shShape != 7)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        painter.pStart = e.Location;
                        painter.pEnd = painter.pStart;
                        painter.isDrawing = true;
                        painter.isFree = false;
                    }
                }
                else
                {
                    //bật cờ isDrawing
                    painter.isDrawing = true;
                    painter.isFree = false;
                }
            }
            if (painter.isEdit)
            {
                Point current = e.Location;

                if (painter.shape.isCtrlPoint(current) != -1)
                {
                    painter.indexCtrlPoint = painter.shape.isCtrlPoint(current);
                    painter.oldPos = current;
                    // nếu mode rotate tắt thì bật mode Scale
                    if (!painter.isRotate)
                    {
                        if ((painter.shShape == 0 || painter.shShape == 7) && e.Button == MouseButtons.Right)
                        {
                            painter.isMove1CtrlPoint = true;
                        }
                        else
                        {
                            painter.isScale = true;
                        }
                    }
                    else
                    {
                        //bật cờ báo hiệu đã nhấn vào điểm điều khiển
                        painter.isDown = true;
                    }

                }
                //nếu click chuột ngoài shape thì dừng edit và lưu
                else if (!painter.shape.isInShape(current) && !painter.shape.isOnShape(current))
                {
                    painter.isEdit = false;
                    painter.isDrawDone = true;
                    painter.shShape = painter.oldShape;
                }
                //nếu tịnh tiến đoạn thẳng
                else if (painter.shShape == 0 && painter.shape.isOnShape(current))
                {
                    painter.oldPos = current;
                    painter.isTranslate = true;
                }
                //nếu tịnh tiến các hình còn lại
                else if (painter.shShape != 0 && painter.shape.isInShape(current))
                {
                    painter.oldPos = current;
                    painter.isTranslate = true;
                }
            }
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (painter.isSelect)
            {
                //kiểm tra nếu ở chế độ select và nhấp chuôt ngoài hình
                bool flagOut = true;
                Point current = e.Location;
                int maxIndex = painter.shapes.Count() - 1;
                for (int i = maxIndex; i > -1; i--)
                {
                    if (painter.shapes[i].isInShape(current) || painter.shapes[i].isOnShape(current))
                    {
                        openGLControl.Cursor = Cursors.Hand;
                        flagOut = false;
                        break;
                    }
                }
                if (flagOut)
                {
                    openGLControl.Cursor = Cursors.Default;
                }
            }

            //đang ở mode edit
            if (painter.isEdit)
            {
                Point current = new Point(e.Location.X, e.Location.Y);

                if (painter.isTranslate)
                {
                    //tạo ma trận affine
                    painter.transformer = new Transformer(current.X - painter.oldPos.X, current.Y - painter.oldPos.Y);
                    // thực hiện translate
                    painter.handleTranslate();
                }
                else if (painter.isScale)
                {
                    painter.handleScale(current);
                }
                else if (painter.isRotate && painter.isDown)
                {
                    painter.handleRotate(current);
                }
                else if (painter.isMove1CtrlPoint)
                {
                    painter.handleMove1CtrlPoint(current);
                }    
                else
                {
                    //đoạn thẳng
                    if (painter.shShape == 0)
                    {
                        if (painter.shape.isOnShape(current))
                        {
                            openGLControl.Cursor = Cursors.SizeAll;
                        }
                        else if (painter.shape.ctrl_Points != null && painter.shape.isCtrlPoint(current) != -1)
                        {
                            openGLControl.Cursor = Cursors.SizeNS;
                        }
                        else
                        {
                            openGLControl.Cursor = Cursors.Default;
                        }
                    }
                    //đa giác
                    else if (painter.shShape == 7)
                    {
                        if (painter.shape.isInShape(current))
                        {
                            openGLControl.Cursor = Cursors.SizeAll;
                        }
                        else if (painter.shape.ctrl_Points != null && painter.shape.isCtrlPoint(current) != -1)
                        {
                            openGLControl.Cursor = Cursors.SizeNS;
                        }
                        else
                        {
                            openGLControl.Cursor = Cursors.Default;
                        }
                    }
                    //hình còn lại
                    else
                    {
                        if (painter.shape.isInShape(current))
                        {
                            openGLControl.Cursor = Cursors.SizeAll;
                        }
                        else if (painter.shape.ctrl_Points != null && painter.shape.isCtrlPoint(current) != -1)
                        {
                            int index = painter.shape.isCtrlPoint(current);
                            if (index == 0 || index == 4)
                            {
                                openGLControl.Cursor = Cursors.SizeNWSE;
                            }
                            if (index == 1 || index == 5)
                            {
                                openGLControl.Cursor = Cursors.SizeNS;
                            }
                            if (index == 2 || index == 6)
                            {

                                openGLControl.Cursor = Cursors.SizeNESW;
                            }
                            if (index == 3 || index == 7)
                            {
                                openGLControl.Cursor = Cursors.SizeWE;
                            }
                        }
                        else
                        {
                            openGLControl.Cursor = Cursors.Default;
                        }
                    }
                }
            }

            // đang vẽ
            if (painter.isDrawing)
            {
                //các hình cơ bản
                if (painter.shShape != 7)
                {
                    //cập nhật pEnd
                    painter.pEnd = e.Location;
                }
                //đa giác
                else
                {
                    //xóa các đỉểm khi vẽ ở thời gian thực
                    painter.vertices.RemoveRange(painter.nVertices, painter.vertices.Count() - painter.nVertices);
                    //thêm điểm tiếp theo
                    painter.vertices.Add(new Point(e.Location.X, e.Location.Y));
                }
            }
        }

        private void ctrl_OpenGLControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (painter.shShape == 7)
            {
                if (e.Button == MouseButtons.Left)
                {
                    //nếu đang vẽ
                    if (painter.isDrawing)
                    {
                        Point v = new Point(e.Location.X, e.Location.Y);
                        painter.vertices.Add(v);
                        painter.nVertices++;
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    if (painter.isDrawing)
                    {
                        //nếu đa giác có 3 đỉnh trở lên
                        //nVertices = 2 thì ở thời gian thực đa giác có 3 đỉnh
                        if (painter.nVertices > 1)
                        {
                            //nếu không vẽ nữa thì chuyển sang chế độ edit
                            painter.isDrawing = false;
                            painter.isEdit = true;
                            //xử lí khi 2 điểm cuối cùng là 1 điểm
                            if (painter.nVertices != 0 && painter.nVertices == painter.vertices.Count() - 1 && painter.vertices[painter.nVertices].Equals(painter.vertices[painter.nVertices - 1]))
                            {
                                painter.vertices.RemoveAt(painter.nVertices);
                                painter.shape.setVertices(painter.vertices);
                            }
                        }
                    }
                }

            }
        }

        private void bt_Line_Clicked(object sender, EventArgs e)
        {
            painter.turnOffActiveMode();
            painter.shShape = 0;
        }

        private void bt_Circle_Click(object sender, EventArgs e)
        {
            painter.turnOffActiveMode();
            painter.shShape = 1;
        }

        private void bt_Ellipse_Click(object sender, EventArgs e)
        {
            painter.turnOffActiveMode();
            painter.shShape = 2;
        }

        private void bt_rectangle_click(object sender, EventArgs e)
        {
            painter.turnOffActiveMode();
            painter.shShape = 3;
        }

        private void bt_equiangularTriangle_click(object sender, EventArgs e)
        {
            painter.turnOffActiveMode();
            painter.shShape = 4;
        }

        private void bt_regularPentagon_Click(object sender, EventArgs e)
        {
            painter.turnOffActiveMode();
            painter.shShape = 5;
        }

        private void bt_regularHexagon_Click(object sender, EventArgs e)
        {
            painter.turnOffActiveMode();
            painter.shShape = 6;
        }

        private void bt_Polygon_Click(object sender, EventArgs e)
        {
            painter.turnOffActiveMode();
            painter.vertices.Clear();
            painter.nVertices = 0;
            painter.shShape = 7;
        }

        //Cập nhật độ dày của nét vẽ
        private void comboBox_SelectThick(object sender, EventArgs e)
        {
            int level = int.Parse(ThickLevel.Text);
            painter.thick = level;
        }

        private void bt_Clear_Click(object sender, EventArgs e)
        {
            painter.isClear = true;
        }

        private void openGLControl_Load_1(object sender, EventArgs e)
        {

        }

        private void bt_Fill_Click(object sender, EventArgs e)
        {
            if (painter.isFill)
            {
                painter.isFill = false;
                bt_Fill.BackgroundImage = Properties.Resources.fill_color;
            }
            else
            {
                painter.isFill = true;
                bt_Fill.BackgroundImage = Properties.Resources.no_filling;
            }
        }

        private void bt_Select_Click(object sender, EventArgs e)
        {
            if (painter.isEdit)
            {
                painter.isDrawDone = true;
                painter.isDrawing = false;
                painter.isEdit = false;
            }
            painter.isSelect = true;
        }

        private void bt_Remove_Click(object sender, EventArgs e)
        {
            painter.isRemove = true;
        }

        private void bt_Rotate_Click(object sender, EventArgs e)
        {
            painter.isRotate = true;
        }
    }
}

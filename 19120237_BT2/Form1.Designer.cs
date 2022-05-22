
namespace _19120237_BT2
{
    partial class ShapeGL
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Button bt_Line;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShapeGL));
            this.openGLControl = new SharpGL.OpenGLControl();
            this.tb_Timer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ThickLevel = new System.Windows.Forms.ComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pn_RegularPolygon = new System.Windows.Forms.Panel();
            this.pn_shape = new System.Windows.Forms.Panel();
            this.bt_Rotate = new System.Windows.Forms.Button();
            this.bt_rectangle = new System.Windows.Forms.Button();
            this.bt_Ellipse = new System.Windows.Forms.Button();
            this.bt_Polygon = new System.Windows.Forms.Button();
            this.bt_equiangularTriangle = new System.Windows.Forms.Button();
            this.bt_Circle = new System.Windows.Forms.Button();
            this.bt_regularHexagon = new System.Windows.Forms.Button();
            this.bt_regularPentagon = new System.Windows.Forms.Button();
            this.bt_Select = new System.Windows.Forms.Button();
            this.pb_color = new System.Windows.Forms.PictureBox();
            this.pb_fillColor = new System.Windows.Forms.PictureBox();
            this.bt_fillColor = new System.Windows.Forms.Button();
            this.bt_Fill = new System.Windows.Forms.Button();
            this.bt_Bangmau = new System.Windows.Forms.Button();
            this.bt_Remove = new System.Windows.Forms.Button();
            this.bt_Clear = new System.Windows.Forms.Button();
            bt_Line = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.pn_RegularPolygon.SuspendLayout();
            this.pn_shape.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_color)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_fillColor)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openGLControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.openGLControl.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.openGLControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.openGLControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.openGLControl.DrawFPS = false;
            this.openGLControl.Location = new System.Drawing.Point(0, 123);
            this.openGLControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(1810, 671);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInit);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_OpenGLResized);
            this.openGLControl.Load += new System.EventHandler(this.openGLControl_Load_1);
            this.openGLControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ctrl_OpenGLControl_MouseClick);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctrl_OpenGLControl_MouseDown);
            this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseMove);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ctrl_OpenGLControl_MouseUp);
            // 
            // tb_Timer
            // 
            this.tb_Timer.Location = new System.Drawing.Point(1494, 94);
            this.tb_Timer.Name = "tb_Timer";
            this.tb_Timer.Size = new System.Drawing.Size(203, 22);
            this.tb_Timer.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1363, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Thick: ";
            // 
            // ThickLevel
            // 
            this.ThickLevel.FormattingEnabled = true;
            this.ThickLevel.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.ThickLevel.Location = new System.Drawing.Point(1419, 94);
            this.ThickLevel.Name = "ThickLevel";
            this.ThickLevel.Size = new System.Drawing.Size(44, 24);
            this.ThickLevel.TabIndex = 13;
            this.ThickLevel.Text = "1";
            this.ThickLevel.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectThick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1501, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "fill color";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1652, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "color";
            // 
            // pn_RegularPolygon
            // 
            this.pn_RegularPolygon.Controls.Add(this.bt_equiangularTriangle);
            this.pn_RegularPolygon.Controls.Add(this.bt_Circle);
            this.pn_RegularPolygon.Controls.Add(this.bt_regularHexagon);
            this.pn_RegularPolygon.Controls.Add(this.bt_regularPentagon);
            this.pn_RegularPolygon.Location = new System.Drawing.Point(489, 7);
            this.pn_RegularPolygon.Name = "pn_RegularPolygon";
            this.pn_RegularPolygon.Size = new System.Drawing.Size(204, 111);
            this.pn_RegularPolygon.TabIndex = 22;
            // 
            // pn_shape
            // 
            this.pn_shape.Controls.Add(this.bt_rectangle);
            this.pn_shape.Controls.Add(this.bt_Ellipse);
            this.pn_shape.Controls.Add(bt_Line);
            this.pn_shape.Controls.Add(this.bt_Polygon);
            this.pn_shape.Location = new System.Drawing.Point(773, 7);
            this.pn_shape.Name = "pn_shape";
            this.pn_shape.Size = new System.Drawing.Size(208, 111);
            this.pn_shape.TabIndex = 23;
            // 
            // bt_Rotate
            // 
            this.bt_Rotate.BackgroundImage = global::_19120237_BT2.Properties.Resources.rotate_main;
            this.bt_Rotate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_Rotate.Location = new System.Drawing.Point(1063, 22);
            this.bt_Rotate.Name = "bt_Rotate";
            this.bt_Rotate.Size = new System.Drawing.Size(79, 69);
            this.bt_Rotate.TabIndex = 24;
            this.toolTip1.SetToolTip(this.bt_Rotate, "Rotate");
            this.bt_Rotate.UseVisualStyleBackColor = true;
            this.bt_Rotate.Click += new System.EventHandler(this.bt_Rotate_Click);
            // 
            // bt_rectangle
            // 
            this.bt_rectangle.BackgroundImage = global::_19120237_BT2.Properties.Resources.rectangle;
            this.bt_rectangle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_rectangle.Location = new System.Drawing.Point(105, 3);
            this.bt_rectangle.Name = "bt_rectangle";
            this.bt_rectangle.Size = new System.Drawing.Size(90, 56);
            this.bt_rectangle.TabIndex = 4;
            this.toolTip1.SetToolTip(this.bt_rectangle, "Rectangle");
            this.bt_rectangle.UseVisualStyleBackColor = true;
            this.bt_rectangle.Click += new System.EventHandler(this.bt_rectangle_click);
            // 
            // bt_Ellipse
            // 
            this.bt_Ellipse.BackgroundImage = global::_19120237_BT2.Properties.Resources.elip;
            this.bt_Ellipse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_Ellipse.Location = new System.Drawing.Point(105, 56);
            this.bt_Ellipse.Name = "bt_Ellipse";
            this.bt_Ellipse.Size = new System.Drawing.Size(90, 56);
            this.bt_Ellipse.TabIndex = 3;
            this.toolTip1.SetToolTip(this.bt_Ellipse, "Ellipse");
            this.bt_Ellipse.UseVisualStyleBackColor = true;
            this.bt_Ellipse.Click += new System.EventHandler(this.bt_Ellipse_Click);
            // 
            // bt_Line
            // 
            bt_Line.BackgroundImage = global::_19120237_BT2.Properties.Resources.line;
            bt_Line.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            bt_Line.Location = new System.Drawing.Point(3, 2);
            bt_Line.Name = "bt_Line";
            bt_Line.Size = new System.Drawing.Size(85, 56);
            bt_Line.TabIndex = 1;
            this.toolTip1.SetToolTip(bt_Line, "Line");
            bt_Line.UseVisualStyleBackColor = true;
            bt_Line.Click += new System.EventHandler(this.bt_Line_Clicked);
            // 
            // bt_Polygon
            // 
            this.bt_Polygon.BackgroundImage = global::_19120237_BT2.Properties.Resources.polygon;
            this.bt_Polygon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_Polygon.Location = new System.Drawing.Point(3, 56);
            this.bt_Polygon.Name = "bt_Polygon";
            this.bt_Polygon.Size = new System.Drawing.Size(85, 56);
            this.bt_Polygon.TabIndex = 14;
            this.toolTip1.SetToolTip(this.bt_Polygon, "Polygon");
            this.bt_Polygon.UseVisualStyleBackColor = true;
            this.bt_Polygon.Click += new System.EventHandler(this.bt_Polygon_Click);
            // 
            // bt_equiangularTriangle
            // 
            this.bt_equiangularTriangle.BackgroundImage = global::_19120237_BT2.Properties.Resources.triangle;
            this.bt_equiangularTriangle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_equiangularTriangle.Location = new System.Drawing.Point(3, 3);
            this.bt_equiangularTriangle.Name = "bt_equiangularTriangle";
            this.bt_equiangularTriangle.Size = new System.Drawing.Size(87, 55);
            this.bt_equiangularTriangle.TabIndex = 7;
            this.toolTip1.SetToolTip(this.bt_equiangularTriangle, "Triangle");
            this.bt_equiangularTriangle.UseVisualStyleBackColor = true;
            this.bt_equiangularTriangle.Click += new System.EventHandler(this.bt_equiangularTriangle_click);
            // 
            // bt_Circle
            // 
            this.bt_Circle.BackgroundImage = global::_19120237_BT2.Properties.Resources.circle;
            this.bt_Circle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_Circle.Location = new System.Drawing.Point(96, 4);
            this.bt_Circle.Name = "bt_Circle";
            this.bt_Circle.Size = new System.Drawing.Size(95, 55);
            this.bt_Circle.TabIndex = 2;
            this.toolTip1.SetToolTip(this.bt_Circle, "Circle");
            this.bt_Circle.UseVisualStyleBackColor = true;
            this.bt_Circle.Click += new System.EventHandler(this.bt_Circle_Click);
            // 
            // bt_regularHexagon
            // 
            this.bt_regularHexagon.BackgroundImage = global::_19120237_BT2.Properties.Resources.hexagon;
            this.bt_regularHexagon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_regularHexagon.Location = new System.Drawing.Point(3, 56);
            this.bt_regularHexagon.Name = "bt_regularHexagon";
            this.bt_regularHexagon.Size = new System.Drawing.Size(87, 53);
            this.bt_regularHexagon.TabIndex = 9;
            this.toolTip1.SetToolTip(this.bt_regularHexagon, "Hexagon");
            this.bt_regularHexagon.UseVisualStyleBackColor = true;
            this.bt_regularHexagon.Click += new System.EventHandler(this.bt_regularHexagon_Click);
            // 
            // bt_regularPentagon
            // 
            this.bt_regularPentagon.BackgroundImage = global::_19120237_BT2.Properties.Resources.pentagon;
            this.bt_regularPentagon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_regularPentagon.Location = new System.Drawing.Point(96, 56);
            this.bt_regularPentagon.Name = "bt_regularPentagon";
            this.bt_regularPentagon.Size = new System.Drawing.Size(95, 53);
            this.bt_regularPentagon.TabIndex = 8;
            this.toolTip1.SetToolTip(this.bt_regularPentagon, "Pentagon");
            this.bt_regularPentagon.UseVisualStyleBackColor = true;
            this.bt_regularPentagon.Click += new System.EventHandler(this.bt_regularPentagon_Click);
            // 
            // bt_Select
            // 
            this.bt_Select.BackgroundImage = global::_19120237_BT2.Properties.Resources.select;
            this.bt_Select.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_Select.Location = new System.Drawing.Point(1148, 22);
            this.bt_Select.Name = "bt_Select";
            this.bt_Select.Size = new System.Drawing.Size(72, 67);
            this.bt_Select.TabIndex = 21;
            this.toolTip1.SetToolTip(this.bt_Select, "Select");
            this.bt_Select.UseVisualStyleBackColor = true;
            this.bt_Select.Click += new System.EventHandler(this.bt_Select_Click);
            // 
            // pb_color
            // 
            this.pb_color.Location = new System.Drawing.Point(1580, 22);
            this.pb_color.Name = "pb_color";
            this.pb_color.Size = new System.Drawing.Size(52, 50);
            this.pb_color.TabIndex = 20;
            this.pb_color.TabStop = false;
            // 
            // pb_fillColor
            // 
            this.pb_fillColor.Location = new System.Drawing.Point(1436, 22);
            this.pb_fillColor.Name = "pb_fillColor";
            this.pb_fillColor.Size = new System.Drawing.Size(52, 50);
            this.pb_fillColor.TabIndex = 19;
            this.pb_fillColor.TabStop = false;
            // 
            // bt_fillColor
            // 
            this.bt_fillColor.BackgroundImage = global::_19120237_BT2.Properties.Resources.colormain;
            this.bt_fillColor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_fillColor.Location = new System.Drawing.Point(1494, 10);
            this.bt_fillColor.Name = "bt_fillColor";
            this.bt_fillColor.Size = new System.Drawing.Size(69, 62);
            this.bt_fillColor.TabIndex = 16;
            this.bt_fillColor.UseVisualStyleBackColor = true;
            this.bt_fillColor.Click += new System.EventHandler(this.bt_fillColor_Click);
            // 
            // bt_Fill
            // 
            this.bt_Fill.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bt_Fill.BackgroundImage")));
            this.bt_Fill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_Fill.Location = new System.Drawing.Point(1226, 24);
            this.bt_Fill.Name = "bt_Fill";
            this.bt_Fill.Size = new System.Drawing.Size(73, 68);
            this.bt_Fill.TabIndex = 15;
            this.toolTip1.SetToolTip(this.bt_Fill, "Fill");
            this.bt_Fill.UseVisualStyleBackColor = true;
            this.bt_Fill.Click += new System.EventHandler(this.bt_Fill_Click);
            // 
            // bt_Bangmau
            // 
            this.bt_Bangmau.BackgroundImage = global::_19120237_BT2.Properties.Resources.colormain;
            this.bt_Bangmau.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_Bangmau.Location = new System.Drawing.Point(1638, 11);
            this.bt_Bangmau.Name = "bt_Bangmau";
            this.bt_Bangmau.Size = new System.Drawing.Size(69, 62);
            this.bt_Bangmau.TabIndex = 10;
            this.bt_Bangmau.UseVisualStyleBackColor = true;
            this.bt_Bangmau.Click += new System.EventHandler(this.bt_BangMau_Click);
            // 
            // bt_Remove
            // 
            this.bt_Remove.BackgroundImage = global::_19120237_BT2.Properties.Resources.remove;
            this.bt_Remove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_Remove.Location = new System.Drawing.Point(149, 19);
            this.bt_Remove.Name = "bt_Remove";
            this.bt_Remove.Size = new System.Drawing.Size(92, 81);
            this.bt_Remove.TabIndex = 6;
            this.toolTip1.SetToolTip(this.bt_Remove, "Delete");
            this.bt_Remove.UseVisualStyleBackColor = true;
            this.bt_Remove.Click += new System.EventHandler(this.bt_Remove_Click);
            // 
            // bt_Clear
            // 
            this.bt_Clear.BackgroundImage = global::_19120237_BT2.Properties.Resources.clear;
            this.bt_Clear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_Clear.Location = new System.Drawing.Point(269, 19);
            this.bt_Clear.Name = "bt_Clear";
            this.bt_Clear.Size = new System.Drawing.Size(92, 79);
            this.bt_Clear.TabIndex = 5;
            this.toolTip1.SetToolTip(this.bt_Clear, "Clear");
            this.bt_Clear.UseVisualStyleBackColor = true;
            this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
            // 
            // ShapeGL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1739, 776);
            this.Controls.Add(this.bt_Rotate);
            this.Controls.Add(this.pn_shape);
            this.Controls.Add(this.pn_RegularPolygon);
            this.Controls.Add(this.bt_Select);
            this.Controls.Add(this.pb_color);
            this.Controls.Add(this.pb_fillColor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bt_fillColor);
            this.Controls.Add(this.bt_Fill);
            this.Controls.Add(this.ThickLevel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_Timer);
            this.Controls.Add(this.bt_Bangmau);
            this.Controls.Add(this.bt_Remove);
            this.Controls.Add(this.bt_Clear);
            this.Controls.Add(this.openGLControl);
            this.Name = "ShapeGL";
            this.Text = "ShapeGL";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.pn_RegularPolygon.ResumeLayout(false);
            this.pn_shape.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_color)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_fillColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.Button bt_Circle;
        private System.Windows.Forms.Button bt_Ellipse;
        private System.Windows.Forms.Button bt_rectangle;
        private System.Windows.Forms.Button bt_Clear;
        private System.Windows.Forms.Button bt_Remove;
        private System.Windows.Forms.Button bt_equiangularTriangle;
        private System.Windows.Forms.Button bt_regularPentagon;
        private System.Windows.Forms.Button bt_regularHexagon;
        private System.Windows.Forms.Button bt_Bangmau;
        private System.Windows.Forms.TextBox tb_Timer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ThickLevel;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button bt_Polygon;
        private System.Windows.Forms.Button bt_Fill;
        private System.Windows.Forms.Button bt_fillColor;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pb_fillColor;
        private System.Windows.Forms.PictureBox pb_color;
        private System.Windows.Forms.Button bt_Select;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel pn_RegularPolygon;
        private System.Windows.Forms.Panel pn_shape;
        private System.Windows.Forms.Button bt_Rotate;
    }
}


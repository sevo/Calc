using DMSoft;
namespace Calc
{
    partial class GrafForm
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
            grafOpened = false;
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GrafForm));
            this.xTrackBar = new System.Windows.Forms.TrackBar();
            this.functionDeclaration = new System.Windows.Forms.TextBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.yTrackBar = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.buttonInverseSellection = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonZoomIn = new System.Windows.Forms.Button();
            this.buttonZoomOut = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.YMinNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ZMinNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.YMaxNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.XMinNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ZMaxNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.XMaxNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.GrafSurface2D = new NPlot.Windows.PlotSurface2D();
            this.YnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.XnumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.xTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YMinNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZMinNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YMaxNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XMinNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZMaxNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XMaxNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YnumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XnumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // xTrackBar
            // 
            this.xTrackBar.Location = new System.Drawing.Point(326, 443);
            this.xTrackBar.Maximum = 400;
            this.xTrackBar.Name = "xTrackBar";
            this.xTrackBar.Size = new System.Drawing.Size(669, 45);
            this.xTrackBar.TabIndex = 1;
            this.xTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.xTrackBar.Value = 200;
            this.xTrackBar.Scroll += new System.EventHandler(this.xTrackBar_Scroll);
            // 
            // functionDeclaration
            // 
            this.functionDeclaration.Location = new System.Drawing.Point(12, 323);
            this.functionDeclaration.Name = "functionDeclaration";
            this.functionDeclaration.Size = new System.Drawing.Size(193, 20);
            this.functionDeclaration.TabIndex = 5;
            this.functionDeclaration.Text = "f(x)=";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(12, 30);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(193, 214);
            this.checkedListBox1.TabIndex = 8;
            // 
            // yTrackBar
            // 
            this.yTrackBar.Enabled = false;
            this.yTrackBar.Location = new System.Drawing.Point(284, 33);
            this.yTrackBar.Name = "yTrackBar";
            this.yTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.yTrackBar.Size = new System.Drawing.Size(45, 393);
            this.yTrackBar.TabIndex = 9;
            this.yTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(207, 401);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Min Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(207, 472);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Min Z";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(375, 472);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Min X";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(906, 468);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Max X";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Enabled = false;
            this.label8.Location = new System.Drawing.Point(913, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Max Z";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(205, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Max Y";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Location = new System.Drawing.Point(12, 250);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectAll.TabIndex = 22;
            this.buttonSelectAll.Text = "Select All";
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            // 
            // buttonInverseSellection
            // 
            this.buttonInverseSellection.Location = new System.Drawing.Point(105, 250);
            this.buttonInverseSellection.Name = "buttonInverseSellection";
            this.buttonInverseSellection.Size = new System.Drawing.Size(100, 23);
            this.buttonInverseSellection.TabIndex = 23;
            this.buttonInverseSellection.Text = "Inverse Selection";
            this.buttonInverseSellection.UseVisualStyleBackColor = true;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(12, 279);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 24;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            // 
            // buttonZoomIn
            // 
            this.buttonZoomIn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonZoomIn.BackgroundImage")));
            this.buttonZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("buttonZoomIn.Image")));
            this.buttonZoomIn.Location = new System.Drawing.Point(27, 403);
            this.buttonZoomIn.Name = "buttonZoomIn";
            this.buttonZoomIn.Size = new System.Drawing.Size(45, 35);
            this.buttonZoomIn.TabIndex = 25;
            this.buttonZoomIn.UseVisualStyleBackColor = true;
            this.buttonZoomIn.Click += new System.EventHandler(this.buttonZoomIn_Click);
            // 
            // buttonZoomOut
            // 
            this.buttonZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("buttonZoomOut.Image")));
            this.buttonZoomOut.Location = new System.Drawing.Point(120, 403);
            this.buttonZoomOut.Name = "buttonZoomOut";
            this.buttonZoomOut.Size = new System.Drawing.Size(45, 35);
            this.buttonZoomOut.TabIndex = 26;
            this.buttonZoomOut.UseVisualStyleBackColor = true;
            this.buttonZoomOut.Click += new System.EventHandler(this.buttonZoomOut_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(31, 468);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 35);
            this.button1.TabIndex = 27;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(117, 469);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(45, 35);
            this.button2.TabIndex = 28;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(74, 502);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(45, 35);
            this.button3.TabIndex = 29;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(74, 435);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(45, 35);
            this.button4.TabIndex = 30;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // YMinNumericUpDown
            // 
            this.YMinNumericUpDown.DecimalPlaces = 2;
            this.YMinNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.YMinNumericUpDown.Location = new System.Drawing.Point(239, 399);
            this.YMinNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.YMinNumericUpDown.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.YMinNumericUpDown.Name = "YMinNumericUpDown";
            this.YMinNumericUpDown.Size = new System.Drawing.Size(43, 20);
            this.YMinNumericUpDown.TabIndex = 31;
            this.YMinNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            this.YMinNumericUpDown.ValueChanged += new System.EventHandler(this.YMinNumericUpDown_ValueChanged);
            // 
            // ZMinNumericUpDown
            // 
            this.ZMinNumericUpDown.DecimalPlaces = 2;
            this.ZMinNumericUpDown.Enabled = false;
            this.ZMinNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ZMinNumericUpDown.Location = new System.Drawing.Point(239, 470);
            this.ZMinNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ZMinNumericUpDown.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.ZMinNumericUpDown.Name = "ZMinNumericUpDown";
            this.ZMinNumericUpDown.Size = new System.Drawing.Size(43, 20);
            this.ZMinNumericUpDown.TabIndex = 32;
            this.ZMinNumericUpDown.ValueChanged += new System.EventHandler(this.ZMinNumericUpDown_ValueChanged);
            // 
            // YMaxNumericUpDown
            // 
            this.YMaxNumericUpDown.DecimalPlaces = 2;
            this.YMaxNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.YMaxNumericUpDown.Location = new System.Drawing.Point(239, 42);
            this.YMaxNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.YMaxNumericUpDown.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.YMaxNumericUpDown.Name = "YMaxNumericUpDown";
            this.YMaxNumericUpDown.Size = new System.Drawing.Size(43, 20);
            this.YMaxNumericUpDown.TabIndex = 33;
            this.YMaxNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.YMaxNumericUpDown.ValueChanged += new System.EventHandler(this.YMaxNumericUpDown_ValueChanged);
            // 
            // XMinNumericUpDown
            // 
            this.XMinNumericUpDown.DecimalPlaces = 2;
            this.XMinNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.XMinNumericUpDown.Location = new System.Drawing.Point(334, 470);
            this.XMinNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.XMinNumericUpDown.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.XMinNumericUpDown.Name = "XMinNumericUpDown";
            this.XMinNumericUpDown.Size = new System.Drawing.Size(43, 20);
            this.XMinNumericUpDown.TabIndex = 34;
            this.XMinNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.XMinNumericUpDown.ValueChanged += new System.EventHandler(this.XMinNumericUpDown_ValueChanged);
            // 
            // ZMaxNumericUpDown
            // 
            this.ZMaxNumericUpDown.DecimalPlaces = 2;
            this.ZMaxNumericUpDown.Enabled = false;
            this.ZMaxNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ZMaxNumericUpDown.Location = new System.Drawing.Point(952, 5);
            this.ZMaxNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ZMaxNumericUpDown.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.ZMaxNumericUpDown.Name = "ZMaxNumericUpDown";
            this.ZMaxNumericUpDown.Size = new System.Drawing.Size(43, 20);
            this.ZMaxNumericUpDown.TabIndex = 35;
            this.ZMaxNumericUpDown.ValueChanged += new System.EventHandler(this.ZMaxNumericUpDown_ValueChanged);
            // 
            // XMaxNumericUpDown
            // 
            this.XMaxNumericUpDown.DecimalPlaces = 2;
            this.XMaxNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.XMaxNumericUpDown.Location = new System.Drawing.Point(943, 466);
            this.XMaxNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.XMaxNumericUpDown.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.XMaxNumericUpDown.Name = "XMaxNumericUpDown";
            this.XMaxNumericUpDown.Size = new System.Drawing.Size(43, 20);
            this.XMaxNumericUpDown.TabIndex = 36;
            this.XMaxNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.XMaxNumericUpDown.ValueChanged += new System.EventHandler(this.XMaxNumericUpDown_ValueChanged);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(130, 349);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(75, 23);
            this.buttonCreate.TabIndex = 37;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(544, 494);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(234, 56);
            this.listBox1.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(552, 466);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 22);
            this.label1.TabIndex = 39;
            this.label1.Text = "x=";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(675, 466);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 22);
            this.label2.TabIndex = 40;
            this.label2.Text = "y=";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 307);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Function declaration:";
            // 
            // GrafSurface2D
            // 
            this.GrafSurface2D.AutoScaleAutoGeneratedAxes = false;
            this.GrafSurface2D.AutoScaleTitle = false;
            this.GrafSurface2D.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.GrafSurface2D.DateTimeToolTip = false;
            this.GrafSurface2D.Legend = null;
            this.GrafSurface2D.LegendZOrder = -1;
            this.GrafSurface2D.Location = new System.Drawing.Point(309, 31);
            this.GrafSurface2D.Name = "GrafSurface2D";
            this.GrafSurface2D.RightMenu = null;
            this.GrafSurface2D.ShowCoordinates = true;
            this.GrafSurface2D.Size = new System.Drawing.Size(687, 407);
            this.GrafSurface2D.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.GrafSurface2D.TabIndex = 42;
            this.GrafSurface2D.Text = "plotSurface2D1";
            this.GrafSurface2D.Title = "";
            this.GrafSurface2D.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.GrafSurface2D.XAxis1 = null;
            this.GrafSurface2D.XAxis2 = null;
            this.GrafSurface2D.YAxis1 = null;
            this.GrafSurface2D.YAxis2 = null;
            // 
            // YnumericUpDown
            // 
            this.YnumericUpDown.DecimalPlaces = 2;
            this.YnumericUpDown.Enabled = false;
            this.YnumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.YnumericUpDown.Location = new System.Drawing.Point(702, 468);
            this.YnumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.YnumericUpDown.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.YnumericUpDown.Name = "YnumericUpDown";
            this.YnumericUpDown.Size = new System.Drawing.Size(55, 20);
            this.YnumericUpDown.TabIndex = 43;
            this.YnumericUpDown.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // XnumericUpDown
            // 
            this.XnumericUpDown.DecimalPlaces = 2;
            this.XnumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.XnumericUpDown.Location = new System.Drawing.Point(579, 469);
            this.XnumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.XnumericUpDown.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.XnumericUpDown.Name = "XnumericUpDown";
            this.XnumericUpDown.Size = new System.Drawing.Size(55, 20);
            this.XnumericUpDown.TabIndex = 44;
            this.XnumericUpDown.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // GrafForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.XnumericUpDown);
            this.Controls.Add(this.YnumericUpDown);
            this.Controls.Add(this.GrafSurface2D);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.XMaxNumericUpDown);
            this.Controls.Add(this.ZMaxNumericUpDown);
            this.Controls.Add(this.XMinNumericUpDown);
            this.Controls.Add(this.YMaxNumericUpDown);
            this.Controls.Add(this.ZMinNumericUpDown);
            this.Controls.Add(this.YMinNumericUpDown);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonZoomOut);
            this.Controls.Add(this.buttonZoomIn);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonInverseSellection);
            this.Controls.Add(this.buttonSelectAll);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.xTrackBar);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.yTrackBar);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.functionDeclaration);
            this.Name = "GrafForm";
            this.Text = "Graf";
            ((System.ComponentModel.ISupportInitialize)(this.xTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YMinNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZMinNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YMaxNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XMinNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZMaxNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XMaxNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YnumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XnumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar xTrackBar;
        private System.Windows.Forms.TextBox functionDeclaration;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TrackBar yTrackBar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonSelectAll;
        private System.Windows.Forms.Button buttonInverseSellection;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonZoomIn;
        private System.Windows.Forms.Button buttonZoomOut;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.NumericUpDown YMinNumericUpDown;
        private System.Windows.Forms.NumericUpDown ZMinNumericUpDown;
        private System.Windows.Forms.NumericUpDown YMaxNumericUpDown;
        private System.Windows.Forms.NumericUpDown XMinNumericUpDown;
        private System.Windows.Forms.NumericUpDown ZMaxNumericUpDown;
        private System.Windows.Forms.NumericUpDown XMaxNumericUpDown;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private NPlot.Windows.PlotSurface2D GrafSurface2D;
        //private SkinCrafter skin;
        private System.Windows.Forms.NumericUpDown YnumericUpDown;
        private System.Windows.Forms.NumericUpDown XnumericUpDown; 
    }
}
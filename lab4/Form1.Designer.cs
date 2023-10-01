namespace lab23
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ClearAll = new System.Windows.Forms.Button();
            this.DrawBaseC = new System.Windows.Forms.Button();
            this.RotateX = new System.Windows.Forms.Button();
            this.Scale = new System.Windows.Forms.Button();
            this.Move = new System.Windows.Forms.Button();
            this.rx = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.scx = new System.Windows.Forms.NumericUpDown();
            this.scy = new System.Windows.Forms.NumericUpDown();
            this.scz = new System.Windows.Forms.NumericUpDown();
            this.mz = new System.Windows.Forms.NumericUpDown();
            this.my = new System.Windows.Forms.NumericUpDown();
            this.mx = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ry = new System.Windows.Forms.NumericUpDown();
            this.RotateY = new System.Windows.Forms.Button();
            this.rz = new System.Windows.Forms.NumericUpDown();
            this.RotateZ = new System.Windows.Forms.Button();
            this.Projection = new System.Windows.Forms.Button();
            this.Reset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.rx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.my)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rz)).BeginInit();
            this.SuspendLayout();
            // 
            // ClearAll
            // 
            this.ClearAll.Location = new System.Drawing.Point(12, 12);
            this.ClearAll.Name = "ClearAll";
            this.ClearAll.Size = new System.Drawing.Size(83, 31);
            this.ClearAll.TabIndex = 0;
            this.ClearAll.Text = "Очистить";
            this.ClearAll.UseVisualStyleBackColor = true;
            this.ClearAll.Click += new System.EventHandler(this.ClearAll_Click);
            // 
            // DrawBaseC
            // 
            this.DrawBaseC.Location = new System.Drawing.Point(120, 12);
            this.DrawBaseC.Name = "DrawBaseC";
            this.DrawBaseC.Size = new System.Drawing.Size(170, 31);
            this.DrawBaseC.TabIndex = 1;
            this.DrawBaseC.Text = "Нарисовать корабль";
            this.DrawBaseC.UseVisualStyleBackColor = true;
            this.DrawBaseC.Click += new System.EventHandler(this.DrawBaseC_Click);
            // 
            // RotateX
            // 
            this.RotateX.Location = new System.Drawing.Point(357, 12);
            this.RotateX.Name = "RotateX";
            this.RotateX.Size = new System.Drawing.Size(167, 29);
            this.RotateX.TabIndex = 2;
            this.RotateX.Text = "Повернуть отн-но ОХ:";
            this.RotateX.UseVisualStyleBackColor = true;
            this.RotateX.Click += new System.EventHandler(this.RotateX_Click);
            // 
            // Scale
            // 
            this.Scale.Location = new System.Drawing.Point(670, 12);
            this.Scale.Name = "Scale";
            this.Scale.Size = new System.Drawing.Size(132, 29);
            this.Scale.TabIndex = 3;
            this.Scale.Text = "Масштабировать";
            this.Scale.UseVisualStyleBackColor = true;
            this.Scale.Click += new System.EventHandler(this.Scale_Click);
            // 
            // Move
            // 
            this.Move.Location = new System.Drawing.Point(969, 14);
            this.Move.Name = "Move";
            this.Move.Size = new System.Drawing.Size(134, 29);
            this.Move.TabIndex = 4;
            this.Move.Text = "Переместить на";
            this.Move.UseVisualStyleBackColor = true;
            this.Move.Click += new System.EventHandler(this.Move_Click);
            // 
            // rx
            // 
            this.rx.Location = new System.Drawing.Point(530, 16);
            this.rx.Name = "rx";
            this.rx.Size = new System.Drawing.Size(71, 22);
            this.rx.TabIndex = 5;
            this.rx.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(816, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "x:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(817, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "z:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(816, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "y:";
            // 
            // scx
            // 
            this.scx.DecimalPlaces = 2;
            this.scx.Location = new System.Drawing.Point(838, 12);
            this.scx.Name = "scx";
            this.scx.Size = new System.Drawing.Size(71, 22);
            this.scx.TabIndex = 9;
            this.scx.Value = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            // 
            // scy
            // 
            this.scy.DecimalPlaces = 2;
            this.scy.Location = new System.Drawing.Point(838, 43);
            this.scy.Name = "scy";
            this.scy.Size = new System.Drawing.Size(71, 22);
            this.scy.TabIndex = 10;
            this.scy.Value = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            // 
            // scz
            // 
            this.scz.DecimalPlaces = 2;
            this.scz.Location = new System.Drawing.Point(838, 71);
            this.scz.Name = "scz";
            this.scz.Size = new System.Drawing.Size(71, 22);
            this.scz.TabIndex = 11;
            this.scz.Value = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            // 
            // mz
            // 
            this.mz.Location = new System.Drawing.Point(1141, 71);
            this.mz.Name = "mz";
            this.mz.Size = new System.Drawing.Size(71, 22);
            this.mz.TabIndex = 17;
            this.mz.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // my
            // 
            this.my.Location = new System.Drawing.Point(1141, 43);
            this.my.Name = "my";
            this.my.Size = new System.Drawing.Size(71, 22);
            this.my.TabIndex = 16;
            this.my.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // mx
            // 
            this.mx.Location = new System.Drawing.Point(1141, 12);
            this.mx.Name = "mx";
            this.mx.Size = new System.Drawing.Size(71, 22);
            this.mx.TabIndex = 15;
            this.mx.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1119, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "y:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1120, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "z:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1119, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "x:";
            // 
            // ry
            // 
            this.ry.Location = new System.Drawing.Point(530, 51);
            this.ry.Name = "ry";
            this.ry.Size = new System.Drawing.Size(71, 22);
            this.ry.TabIndex = 19;
            this.ry.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // RotateY
            // 
            this.RotateY.Location = new System.Drawing.Point(357, 47);
            this.RotateY.Name = "RotateY";
            this.RotateY.Size = new System.Drawing.Size(167, 29);
            this.RotateY.TabIndex = 18;
            this.RotateY.Text = "Повернуть отн-но ОY:";
            this.RotateY.UseVisualStyleBackColor = true;
            this.RotateY.Click += new System.EventHandler(this.RotateY_Click);
            // 
            // rz
            // 
            this.rz.Location = new System.Drawing.Point(530, 86);
            this.rz.Name = "rz";
            this.rz.Size = new System.Drawing.Size(71, 22);
            this.rz.TabIndex = 21;
            this.rz.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // RotateZ
            // 
            this.RotateZ.Location = new System.Drawing.Point(357, 82);
            this.RotateZ.Name = "RotateZ";
            this.RotateZ.Size = new System.Drawing.Size(167, 29);
            this.RotateZ.TabIndex = 20;
            this.RotateZ.Text = "Повернуть отн-но ОZ:";
            this.RotateZ.UseVisualStyleBackColor = true;
            this.RotateZ.Click += new System.EventHandler(this.RotateZ_Click);
            // 
            // Projection
            // 
            this.Projection.Location = new System.Drawing.Point(120, 51);
            this.Projection.Name = "Projection";
            this.Projection.Size = new System.Drawing.Size(170, 57);
            this.Projection.TabIndex = 22;
            this.Projection.Text = "Нарисовать проекцию Кавалье";
            this.Projection.UseVisualStyleBackColor = true;
            this.Projection.Click += new System.EventHandler(this.Projection_Click);
            // 
            // Reset
            // 
            this.Reset.Location = new System.Drawing.Point(12, 53);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(83, 34);
            this.Reset.TabIndex = 23;
            this.Reset.Text = "Сбросить";
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1255, 692);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.Projection);
            this.Controls.Add(this.rz);
            this.Controls.Add(this.RotateZ);
            this.Controls.Add(this.ry);
            this.Controls.Add(this.RotateY);
            this.Controls.Add(this.mz);
            this.Controls.Add(this.my);
            this.Controls.Add(this.mx);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.scz);
            this.Controls.Add(this.scy);
            this.Controls.Add(this.scx);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rx);
            this.Controls.Add(this.Move);
            this.Controls.Add(this.Scale);
            this.Controls.Add(this.RotateX);
            this.Controls.Add(this.DrawBaseC);
            this.Controls.Add(this.ClearAll);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Компьютерная графика";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.rx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.my)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rz)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ClearAll;
        private System.Windows.Forms.Button DrawBaseC;
        private System.Windows.Forms.Button RotateX;
        private System.Windows.Forms.Button Scale;
        private System.Windows.Forms.Button Move;
        private System.Windows.Forms.NumericUpDown rx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown scx;
        private System.Windows.Forms.NumericUpDown scy;
        private System.Windows.Forms.NumericUpDown scz;
        private System.Windows.Forms.NumericUpDown mz;
        private System.Windows.Forms.NumericUpDown my;
        private System.Windows.Forms.NumericUpDown mx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown ry;
        private System.Windows.Forms.Button RotateY;
        private System.Windows.Forms.NumericUpDown rz;
        private System.Windows.Forms.Button RotateZ;
        private System.Windows.Forms.Button Projection;
        private System.Windows.Forms.Button Reset;
    }
}


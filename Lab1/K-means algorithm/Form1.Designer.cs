
namespace K_means_algorithm
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnKMeans = new System.Windows.Forms.Button();
            this.txtNumberOfShapes = new System.Windows.Forms.TextBox();
            this.txtNumberOfClass = new System.Windows.Forms.TextBox();
            this.shapeLabel = new System.Windows.Forms.Label();
            this.classLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBox.Location = new System.Drawing.Point(12, 81);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(471, 471);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(338, 15);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(138, 22);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Генерация";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnKMeans
            // 
            this.btnKMeans.Location = new System.Drawing.Point(338, 45);
            this.btnKMeans.Name = "btnKMeans";
            this.btnKMeans.Size = new System.Drawing.Size(138, 23);
            this.btnKMeans.TabIndex = 2;
            this.btnKMeans.Text = "Примениние алгоритм";
            this.btnKMeans.UseVisualStyleBackColor = true;
            // 
            // txtNumberOfShapes
            // 
            this.txtNumberOfShapes.Location = new System.Drawing.Point(141, 17);
            this.txtNumberOfShapes.Name = "txtNumberOfShapes";
            this.txtNumberOfShapes.Size = new System.Drawing.Size(180, 20);
            this.txtNumberOfShapes.TabIndex = 3;
            // 
            // txtNumberOfClass
            // 
            this.txtNumberOfClass.Location = new System.Drawing.Point(141, 47);
            this.txtNumberOfClass.Name = "txtNumberOfClass";
            this.txtNumberOfClass.Size = new System.Drawing.Size(180, 20);
            this.txtNumberOfClass.TabIndex = 4;
            // 
            // shapeLabel
            // 
            this.shapeLabel.AutoSize = true;
            this.shapeLabel.Location = new System.Drawing.Point(24, 20);
            this.shapeLabel.Name = "shapeLabel";
            this.shapeLabel.Size = new System.Drawing.Size(114, 13);
            this.shapeLabel.TabIndex = 5;
            this.shapeLabel.Text = "Количество образов:";
            // 
            // classLabel
            // 
            this.classLabel.AutoSize = true;
            this.classLabel.Location = new System.Drawing.Point(24, 50);
            this.classLabel.Name = "classLabel";
            this.classLabel.Size = new System.Drawing.Size(87, 13);
            this.classLabel.TabIndex = 6;
            this.classLabel.Text = "Число классов:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 570);
            this.Controls.Add(this.classLabel);
            this.Controls.Add(this.shapeLabel);
            this.Controls.Add(this.txtNumberOfClass);
            this.Controls.Add(this.txtNumberOfShapes);
            this.Controls.Add(this.btnKMeans);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.pictureBox);
            this.Name = "Form1";
            this.Text = "Алгоритм К-средних";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnKMeans;
        private System.Windows.Forms.TextBox txtNumberOfShapes;
        private System.Windows.Forms.TextBox txtNumberOfClass;
        private System.Windows.Forms.Label shapeLabel;
        private System.Windows.Forms.Label classLabel;
    }
}


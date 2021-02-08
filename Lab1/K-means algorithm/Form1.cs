using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K_means_algorithm
{
    public partial class Form1 : Form
    {
        private struct Shape
        {
            public Point point;
            public int numberOfClass;
        }

        int maxColorArg = 256; 
        Random random = new Random();

        public Form1()
        {
            InitializeComponent();

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Graphics gr;
            int numberOfClasses = Convert.ToInt32(txtNumberOfClass.Text);
            int numberOfShapes = Convert.ToInt32(txtNumberOfShapes.Text);

            Color[] colors = new Color[numberOfClasses];
            Shape[] shapes = new Shape[numberOfShapes];
            Shape[] kernels = new Shape[numberOfClasses];

            for (int i = 0; i < numberOfClasses; i++)
            {
                colors[i] = Color.FromArgb(random.Next(maxColorArg), random.Next(maxColorArg), random.Next(maxColorArg));
            }

            for (int i = 0; i < numberOfShapes; i++)
            {
                gr = pictureBox.CreateGraphics();
                shapes[i].point = new Point(random.Next(pictureBox.Width),random.Next(pictureBox.Height));
                gr.FillRectangle(new SolidBrush(Color.Gray), shapes[i].point.X, shapes[i].point.Y, 3, 3);
            }

            for (int i = 0; i < numberOfClasses; i++)
            {
                gr = pictureBox.CreateGraphics();
                kernels[i].point = new Point(random.Next(pictureBox.Width), random.Next(pictureBox.Height));
                kernels[i].numberOfClass = i;
                gr.FillEllipse(new SolidBrush(colors[i]), kernels[i].point.X, kernels[i].point.Y, 10, 10);
            }
        }
    }
}

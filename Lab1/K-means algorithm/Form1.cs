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
        Color[] colors;
        Shape[] shapes;
        Shape[] kernels;
        Shape[] newKernels;
        int numberOfClasses;
        int numberOfShapes;

        Graphics tmp;

        public Form1()
        {
            InitializeComponent();

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Graphics gr;
            gr = pictureBox.CreateGraphics();
            gr.Clear(Color.White);
            numberOfClasses = Convert.ToInt32(txtNumberOfClass.Text);
            numberOfShapes = Convert.ToInt32(txtNumberOfShapes.Text);

            colors = new Color[numberOfClasses];
            shapes = new Shape[numberOfShapes];
            kernels = new Shape[numberOfClasses];

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

            tmp = pictureBox.CreateGraphics();

            for (int i = 0; i < numberOfClasses; i++)
            {
                gr = pictureBox.CreateGraphics();
                kernels[i].point = new Point(random.Next(pictureBox.Width), random.Next(pictureBox.Height));
                kernels[i].numberOfClass = i;
                gr.FillEllipse(new SolidBrush(colors[i]), kernels[i].point.X, kernels[i].point.Y, 10, 10);
                gr.DrawEllipse(new Pen(Brushes.Black), kernels[i].point.X, kernels[i].point.Y, 10, 10);
            }
        }

        private void btnKMeans_Click(object sender, EventArgs e)
        {
            bool isReady = true;

            while (isReady)
            {

                Graphics gr;

              // pictureBox.

                for (int i = 0; i < numberOfClasses; i++)
                {
                    gr = pictureBox.CreateGraphics();
                    gr.FillEllipse(new SolidBrush(colors[i]), kernels[i].point.X, kernels[i].point.Y, 10, 10);
                }

                for (int i = 0; i < numberOfShapes; i++)
                {
                    double minDistanсe = 100000000;
                    double distance = 100000000;
                    for (int j = 0; j < numberOfClasses; j++)
                    {
                        distance = Math.Sqrt((shapes[i].point.X - kernels[j].point.X) * (shapes[i].point.X - kernels[j].point.X) + (shapes[i].point.Y - kernels[j].point.Y) * (shapes[i].point.Y - kernels[j].point.Y));
                        if (distance < minDistanсe)
                        {
                            minDistanсe = distance;
                            shapes[i].numberOfClass = j;
                        }
                    }
                }

                for (int i = 0; i < numberOfShapes; i++)
                {
                    gr = pictureBox.CreateGraphics();
                    gr.FillRectangle(new SolidBrush(colors[shapes[i].numberOfClass]), shapes[i].point.X, shapes[i].point.Y, 3, 3);
                }

                for (int i = 0; i < numberOfClasses; i++)
                {
                    gr = pictureBox.CreateGraphics();
                    gr.FillEllipse(new SolidBrush(colors[i]), kernels[i].point.X, kernels[i].point.Y, 10, 10);
                    gr.DrawEllipse(new Pen(Brushes.Black), kernels[i].point.X, kernels[i].point.Y, 10, 10);
                }

                int[,] coordSum = new int[numberOfClasses, 3];

                for (int i = 0; i < numberOfShapes; i++)
                {
                    coordSum[shapes[i].numberOfClass, 0] += shapes[i].point.X;
                    coordSum[shapes[i].numberOfClass, 1] += shapes[i].point.Y;
                    coordSum[shapes[i].numberOfClass, 2] += 1;
                }

                newKernels = new Shape[numberOfClasses];

                for (int i = 0; i < numberOfClasses; i++)
                {
                    newKernels[i].point.X = coordSum[i, 0]/coordSum[i,2];
                    newKernels[i].point.Y = coordSum[i, 1]/coordSum[i,2];
                    newKernels[i].numberOfClass = i;
                }

                isReady = false;
                for (int i = 0; i < numberOfClasses; i++)
                {
                    if ((newKernels[i].point.X != kernels[i].point.X) || (newKernels[i].point.Y != kernels[i].point.Y))
                    {
                        isReady = true;
                    }
                }

                kernels = newKernels;
                if (isReady)
                {
                    gr = pictureBox.CreateGraphics();
                    gr.Clear(Color.White);
                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maximin_s_algorithm
{
    public partial class Form1 : Form
    {
        private struct Shape
        {
            public Point point;
            public int numberOfClass;
        }

        int maxClasses = 20;
        int maxColorArg = 256;
        Random random = new Random();
        Shape[] shapes;
        Shape[] kernels;
        Shape[] newKernels;
        int numberOfShapes;
        int numberOfClasses;
        Graphics tmp;

        Color[] colors = new Color[20];
        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < 20; i++)
            {
                colors[i] = Color.FromArgb(random.Next(maxColorArg), random.Next(maxColorArg), random.Next(maxColorArg));
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Graphics gr;
            gr = pictureBox.CreateGraphics();
            gr.Clear(Color.White);
            numberOfShapes = Convert.ToInt32(txtNumberOfShapes.Text);

            shapes = new Shape[numberOfShapes];

            for (int i = 0; i < numberOfShapes; i++)
            {
                gr = pictureBox.CreateGraphics();
                shapes[i].point = new Point(random.Next(pictureBox.Width), random.Next(pictureBox.Height));
                gr.FillRectangle(new SolidBrush(Color.Gray), shapes[i].point.X, shapes[i].point.Y, 3, 3);
                shapes[i].numberOfClass = 0;
            }

            tmp = pictureBox.CreateGraphics();

            kernels = new Shape[maxClasses];
            int randomNumber = random.Next(numberOfShapes);
            kernels[0].point = shapes[randomNumber].point;

            for (int i = randomNumber; i < numberOfShapes - 1; i++)
            {
                shapes[i] = shapes[i + 1];
            }

            gr = pictureBox.CreateGraphics();
            gr.FillEllipse(new SolidBrush(Color.Black), kernels[0].point.X - 5, kernels[0].point.Y - 5, 10, 10);
            gr.DrawEllipse(new Pen(Brushes.Black), kernels[0].point.X - 5, kernels[0].point.Y - 5, 10, 10);

        }

        private void btnMaximin_Click(object sender, EventArgs e)
        {
            Graphics gr;

            double maxDistance = -1;
            double distance;
            int index = 0;

            for (int i = 0; i < numberOfShapes; i++)
            {
                distance = Math.Sqrt(Math.Pow(shapes[i].point.X - kernels[0].point.X, 2) + Math.Pow(shapes[i].point.Y - kernels[0].point.Y, 2));
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    kernels[1].point = shapes[i].point;
                    index = i;
                }
            }

            for (int i = index; i < numberOfShapes-1; i++)
            {
                shapes[i] = shapes[i + 1];
            }

            numberOfShapes -= 1;

            kernels[1].numberOfClass = 1;
            numberOfClasses = 2;

            bool isReady = true;
            
            while (isReady)
            {
                for (int i = 0; i < numberOfClasses; i++)
                {
                    gr = pictureBox.CreateGraphics();
                    gr.FillEllipse(new SolidBrush(Color.Black), kernels[i].point.X - 5, kernels[i].point.Y - 5, 10, 10);
                    gr.DrawEllipse(new Pen(Brushes.Black), kernels[i].point.X - 5, kernels[i].point.Y - 5, 10, 10);
                }

                for (int i = 0; i < numberOfShapes; i++)
                {
                    double minDistanсe = 100000000;
                    distance = 100000000;
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
                    gr.FillEllipse(new SolidBrush(Color.Black), kernels[i].point.X-5, kernels[i].point.Y-5, 10, 10);
                    gr.DrawEllipse(new Pen(Brushes.Black), kernels[i].point.X-5, kernels[i].point.Y-5, 10, 10);
                }

                double avrDistance = 0;
                int numberOfDistance = 0;
                for (int i = 0; i < numberOfClasses-1; i++)
                {
                    for (int j = i + 1; j < numberOfClasses; j++)
                    {
                        avrDistance += Math.Sqrt((kernels[i].point.X - kernels[j].point.X) * (kernels[i].point.X - kernels[j].point.X) + (kernels[i].point.Y - kernels[j].point.Y) * (kernels[i].point.Y - kernels[j].point.Y));
                        numberOfDistance += 1;
                    }
                }

                avrDistance = avrDistance / numberOfDistance;
                avrDistance = avrDistance / 2;

                maxDistance = -1;
                for (int i = 0; i < numberOfClasses; i++)
                {
                    for (int j = 0; j < numberOfShapes; j++)
                    {
                        if (shapes[j].numberOfClass == i)
                        {
                            distance = Math.Sqrt(Math.Pow(shapes[j].point.X - kernels[i].point.X, 2) + Math.Pow(shapes[j].point.Y - kernels[i].point.Y, 2));
                            if (distance > maxDistance)
                            {
                                maxDistance = distance;
                                kernels[numberOfClasses].point = shapes[j].point;
                                index = j;
                            }
                        }
                    }
                }

                if (maxDistance > avrDistance)
                {
                    numberOfClasses += 1;

                    for (int i = index; i < numberOfShapes - 1; i++)
                    {
                        shapes[i] = shapes[i + 1];
                    }

                    numberOfShapes -= 1;
                    gr = pictureBox.CreateGraphics();
                    gr.Clear(Color.White);
                }
                else
                {
                    isReady = false;
                }
                
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
                    gr.FillEllipse(new SolidBrush(Color.Black), kernels[i].point.X, kernels[i].point.Y, 10, 10);
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
                    gr.FillEllipse(new SolidBrush(Color.Black), kernels[i].point.X, kernels[i].point.Y, 10, 10);
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
                    newKernels[i].point.X = coordSum[i, 0] / coordSum[i, 2];
                    newKernels[i].point.Y = coordSum[i, 1] / coordSum[i, 2];
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

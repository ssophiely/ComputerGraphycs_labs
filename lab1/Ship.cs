using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public static class Ship
    {
        // коэффициент
        public static int K = 12;

        // матрица координат
        public static readonly float[,] coordinateMatrix = { { 4, -7, 1 }, { 4, -12, 1 }, { -4, -12, 1 }, { -4, -7, 1 }, { 7, -7, 1 },
        { -7,-7,1}, { 0.25F,12,1},{ 0.25F,-7,1},{ -0.25F,-7,1},{ -0.25F,12,1},{ 0.25F,10,1},{ 0.25F,-6,1},{ 6,-1,1}, { -0.25F,10,1},
        { -0.25F,-6,1},{ -4,-3,1},{ -0.25F,11,1},{ -3,11,1}};

        // матрица смежности
        public static readonly int[,] contiguityMatrix = { {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },{0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },{0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0 },{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,1,0,1,0,0,0,0,0,0,0,0 },{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },{0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },{0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0 },{0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0 },{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }};

        // Отрисовка корабля
        public static void DrawShip(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 2);


            for (int i = 0; i < contiguityMatrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < contiguityMatrix.GetUpperBound(0) + 1; j++)
                {
                    if (contiguityMatrix[i, j] == 1)
                        g.DrawLine(pen, coordinateMatrix[i, 0] * K, coordinateMatrix[i, 1] * K,
                            coordinateMatrix[j, 0] * K, coordinateMatrix[j, 1] * K);
                }
            }
        }

        // Поворот корабля
        public static void RotateShip(Graphics g, int degrees)
        {
            double angle = Math.PI * degrees / 180.0;
            Pen pen = new Pen(Color.Red, 2);

            // Матрица поворота
            float[,] rotateT = { { (float)Math.Cos(angle), (float)Math.Sin(angle) },
                { -(float)Math.Sin(angle), (float)Math.Cos(angle) } };
            float[,] newMatrix = new float[18, 2];

            // Перемножение матриц
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        newMatrix[i, j] += coordinateMatrix[i, k] * rotateT[k, j];
                    }
                }
            }

            // Отрисовка новых координат
            for (int i = 0; i < contiguityMatrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < contiguityMatrix.GetUpperBound(0) + 1; j++)
                {
                    if (contiguityMatrix[i, j] == 1)
                        g.DrawLine(pen, newMatrix[i, 0] * K, newMatrix[i, 1] * K,
                            newMatrix[j, 0] * K, newMatrix[j, 1] * K);
                }
            }
        }

        // Масштабирование корабля
        public static void ScaleShip(Graphics g, float x)
        {
            Pen pen = new Pen(Color.Blue, 2);

            // Матрица масштабирования
            float[,] scaleT = { { x, 0 }, { 0, x } };
            float[,] newMatrix = new float[18, 2];

            // Перемножение матриц
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        newMatrix[i, j] += coordinateMatrix[i, k] * scaleT[k, j];
                    }
                }
            }

            // Отрисовка новых координат
            for (int i = 0; i < contiguityMatrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < contiguityMatrix.GetUpperBound(0) + 1; j++)
                {
                    if (contiguityMatrix[i, j] == 1)
                        g.DrawLine(pen, newMatrix[i, 0] * K, newMatrix[i, 1] * K,
                            newMatrix[j, 0] * K, newMatrix[j, 1] * K);
                }
            }
        }

        // Перемещение корабля
        public static void MoveShip(Graphics g, int x, int y)
        {
            Pen pen = new Pen(Color.Green, 2);

            // Матрица перемещения
            float[,] moveT = { { 1, 0, 0 }, { 0, 1, 0 }, { x, y, 1 } };
            float[,] newMatrix = new float[18, 3];

            // Перемножение матриц
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        newMatrix[i, j] += coordinateMatrix[i, k] * moveT[k, j];
                    }
                }
            }

            // Отрисовка новых координат
            for (int i = 0; i < contiguityMatrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < contiguityMatrix.GetUpperBound(0) + 1; j++)
                {
                    if (contiguityMatrix[i, j] == 1)
                        g.DrawLine(pen, newMatrix[i, 0] * K, newMatrix[i, 1] * K,
                            newMatrix[j, 0] * K, newMatrix[j, 1] * K);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab23
{
    public static class Ship
    {
        // коэффициент увеличения
        public static int K = 12;

        // экранные координаты передней части 
        public static double[,] displayCoordinatesF;

        // экранные координаты задней части
        public static double[,] displayCoordinatesB;

        // матрица мировых координат
        public static readonly double[,] coordinateMatrixF = { { 4, -7, 0, 1 }, { 4, -12, 0, 1 }, { -4, -12, 0, 1 },
            { -4, -7, 0, 1 }, { 7, -7, 0, 1 }, { -7,-7,0, 1}, { 0.25F,12,0, 1},{ 0.25F,-7,0, 1}, { -0.25F,-7,0, 1},
            { -0.25F,12,0, 1},{ 0.25F,10,0, 1},{ 0.25F,-6,0, 1},{ 6,-1,0, 1}, { -0.25F,10,0, 1},
            { -0.25F,-6,0, 1},{ -4,-3,0, 1},{ -0.25F,11,0, 1},{ -3,11,0, 1}};

        // матрица мировых координат
        public static readonly double[,] coordinateMatrixB = { { 4, -7, 3, 1 }, { 4, -12, 3, 1 }, { -4, -12, 3, 1 },
            { -4, -7, 3, 1 }, { 7, -7, 3, 1 }, { -7,-7,3, 1}, { 0.25F,12,3, 1},{ 0.25F,-7,3, 1}, { -0.25F,-7,3, 1},
            { -0.25F,12,3, 1},{ 0.25F,10,3, 1},{ 0.25F,-6,3, 1},{ 6,-1,3, 1}, { -0.25F,10,3, 1},
            { -0.25F,-6,3, 1},{ -4,-3,3, 1},{ -0.25F,11,3, 1},{ -3,11,3, 1}};

        // матрица смежности
        public static readonly int[,] contiguityMatrix = { {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },{0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },{0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0 },{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,1,0,1,0,0,0,0,0,0,0,0 },{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },{0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },{0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0 },{0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0 },{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0 },
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }};

        static Ship()
        {
            // Инициализация экранных координат
            displayCoordinatesF = GetDisplayCoordinates(coordinateMatrixF);
            displayCoordinatesB = GetDisplayCoordinates(coordinateMatrixB);
        }

        // Сброс координат
        public static void Reset()
        {
            Array.Clear(displayCoordinatesF, 0, displayCoordinatesF.Length);
            Array.Clear(displayCoordinatesB, 0, displayCoordinatesB.Length);
            displayCoordinatesF = coordinateMatrixF.Clone() as double[,];
            displayCoordinatesB = coordinateMatrixB.Clone() as double[,];
        }


        // Перевод из мировых в экранные
        public static double[,] GetDisplayCoordinates(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i, j] *= K;
                }
            }

            return matrix;
        }

        // Отрисовка проекции XY
        public static void DrawXY(Graphics g, Pen pen,  double[,] matrix)
        {
            for (int i = 0; i < contiguityMatrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < contiguityMatrix.GetUpperBound(0) + 1; j++)
                {
                    if (contiguityMatrix[i, j] == 1) // смотрим по матрице смежности
                        g.DrawLine(pen, (float)matrix[i, 0], (float)matrix[i, 1],
                            (float)matrix[j, 0], (float)matrix[j, 1]);
                }
            }
        }

        // Отрисовка проекции Кавалье
        public static void DrawCavalier(Graphics g)
        {
            double angle = Math.PI * 30 / 180.0;

            // Матрица проекции
            double[,] projT = {{1,0, 0,0 },
                {0, 1, 0,0 },
                {-(float)Math.Cos(angle),-(float)Math.Sin(angle), 0, 0 },
                {0, 0, 0, 1 }};

            double[,] newMatrixF = new double[18, 4];

            // Перемножение матриц
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        newMatrixF[i, j] += displayCoordinatesF[i, k] * projT[k, j];
                    }
                }
            }

            DrawXY(g, new Pen(Color.Red, 2), newMatrixF);
        }

        // Отрисовка связей для объема фигуры
        public static void DrawConnection(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 2);

            for (int i = 0; i < displayCoordinatesF.GetUpperBound(0) + 1; i++)
            {
                g.DrawLine(pen, (float)displayCoordinatesF[i, 0], (float)displayCoordinatesF[i, 1],
                    (float)displayCoordinatesB[i, 0], (float)displayCoordinatesB[i, 1]);
            }
        }

        // Отрисовка корабля
        public static void DrawBase(Graphics g)
        {
            DrawXY(g, new Pen(Color.Black, 2), displayCoordinatesF);
            DrawXY(g, new Pen(Color.Black, 2), displayCoordinatesB);
            DrawConnection(g);
        }

        // Поворот корабля относительно OX
        public static void RotateShipX(Graphics g, int degrees)
        {
            // Преобразование угла
            double angle = Math.PI * degrees / 180.0;

            // Матрица поворота
            double[,] rotateT = {{1,0,0,0 },
                {0, (float)Math.Cos(angle), (float)Math.Sin(angle),0 },
                {0, -(float)Math.Sin(angle), (float)Math.Cos(angle),0 },
                {0, 0, 0, 1 }};

            double[,] newMatrixF = new double[18, 4];
            double[,] newMatrixB = new double[18, 4];

            // Перемножение матриц
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        newMatrixF[i, j] += displayCoordinatesF[i, k] * rotateT[k, j];
                        newMatrixB[i, j] += displayCoordinatesB[i, k] * rotateT[k, j];
                    }
                }
            }

            displayCoordinatesF = newMatrixF.Clone() as double[,];
            displayCoordinatesB = newMatrixB.Clone() as double[,];

            DrawXY(g, new Pen(Color.Black, 2), displayCoordinatesF);
            DrawXY(g, new Pen(Color.Black, 2), displayCoordinatesB);
            DrawConnection(g);
        }

        // Поворот корабля относительно OY
        public static void RotateShipY(Graphics g, int degrees)
        {
            // Преобразование угла
            double angle = Math.PI * degrees / 180.0;

            // Матрица поворота
            double[,] rotateT = {{(float)Math.Cos(angle),0,-(float)Math.Sin(angle),0 },
                {0, 1, 0, 0 },
                {(float)Math.Sin(angle), 0, (float)Math.Cos(angle), 0 },
                {0, 0, 0, 1 }};

            double[,] newMatrixF = new double[18, 4];
            double[,] newMatrixB = new double[18, 4];

            // Перемножение матриц
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        newMatrixF[i, j] += displayCoordinatesF[i, k] * rotateT[k, j];
                        newMatrixB[i, j] += displayCoordinatesB[i, k] * rotateT[k, j];
                    }
                }
            }

            displayCoordinatesF = newMatrixF.Clone() as double[,];
            displayCoordinatesB = newMatrixB.Clone() as double[,];

            DrawXY(g, new Pen(Color.Black, 2), displayCoordinatesF);
            DrawXY(g, new Pen(Color.Black, 2), displayCoordinatesB);
            DrawConnection(g);
        }

        // Поворот корабля относительно OY
        public static void RotateShipZ(Graphics g, int degrees)
        {
            // Преобразование угла
            double angle = Math.PI * degrees / 180.0;

            // Матрица поворота
            double[,] rotateT = {{(float)Math.Cos(angle), (float)Math.Sin(angle),0, 0 },
                {-(float)Math.Sin(angle), (float)Math.Cos(angle), 0, 0},
                {0, 0, 1, 0 },
                {0, 0, 0, 1 }};

            double[,] newMatrixF = new double[18, 4];
            double[,] newMatrixB = new double[18, 4];

            // Перемножение матриц
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        newMatrixF[i, j] += displayCoordinatesF[i, k] * rotateT[k, j];
                        newMatrixB[i, j] += displayCoordinatesB[i, k] * rotateT[k, j];
                    }
                }
            }

            displayCoordinatesF = newMatrixF.Clone() as double[,];
            displayCoordinatesB = newMatrixB.Clone() as double[,];

            DrawXY(g, new Pen(Color.Black, 2), displayCoordinatesF);
            DrawXY(g, new Pen(Color.Black, 2), displayCoordinatesB);
            DrawConnection(g);
        }


        // Масштабирование корабля
        public static void Scale(Graphics g, double x, double y, double z)
        {

            // Матрица масштабирования
            double[,] scaleT = {{x,0,0,0 },
                                {0,y, 0, 0},
                                {0, 0,z, 0 },
                                {0, 0, 0, 1 }};

            double[,] newMatrixF = new double[18, 4];
            double[,] newMatrixB = new double[18, 4];

            // Перемножение матриц
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        newMatrixF[i, j] += displayCoordinatesF[i, k] * scaleT[k, j];
                        newMatrixB[i, j] += displayCoordinatesB[i, k] * scaleT[k, j];
                    }
                }
            }

            displayCoordinatesF = newMatrixF.Clone() as double[,];
            displayCoordinatesB = newMatrixB.Clone() as double[,];

            DrawXY(g, new Pen(Color.Black, 2), displayCoordinatesF);
            DrawXY(g, new Pen(Color.Black, 2), displayCoordinatesB);
            DrawConnection(g);
        }

        // Перемещение корабля
        public static void Move(Graphics g, int dx, int dy, int dz)
        {
            // Матрица перемещения
            double[,] moveT = {{1,0,0,0 },
                                {0,1, 0, 0},
                                {0, 0,1, 0 },
                                {dx, dy, dz, 1 }};

            double[,] newMatrixF = new double[18, 4];
            double[,] newMatrixB = new double[18, 4];

            // Перемножение матриц
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        newMatrixF[i, j] += displayCoordinatesF[i, k] * moveT[k, j];
                        newMatrixB[i, j] += displayCoordinatesB[i, k] * moveT[k, j];
                    }
                }
            }

            displayCoordinatesF = newMatrixF.Clone() as double[,];
            displayCoordinatesB = newMatrixB.Clone() as double[,];

            DrawXY(g, new Pen(Color.Black, 2), displayCoordinatesF);
            DrawXY(g, new Pen(Color.Black, 2), displayCoordinatesB);
            DrawConnection(g);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public static class Ship
    {
        private const double xc = 1231 / 2;

        private const double yc = 501 / 2;

        // коэффициент увеличения
        public static int K = 12;

        // экранные координаты передней части 
        public static double[,] coordinateMatrixF;

        // экранные координаты задней части
        public static double[,] coordinateMatrixB;

        // матрица мировых координат
        private static readonly double[,] MatrixF = { { 4, -7, 0, 1 }, { 4, -12, 0, 1 }, { -4, -12, 0, 1 },
            { -4, -7, 0, 1 }, { 7, -7, 0, 1 }, { -7,-7,0, 1}, { 0.25F,12,0, 1},{ 0.25F,-7,0, 1}, { -0.25F,-7,0, 1},
            { -0.25F,12,0, 1},{ 0.25F,10,0, 1},{ 0.25F,-6,0, 1},{ 6,-1,0, 1}, { -0.25F,10,0, 1},
            { -0.25F,-6,0, 1},{ -4,-3,0, 1},{ -0.25F,11,0, 1},{ -3,11,0, 1}};

        // матрица мировых координат
        private static readonly double[,] MatrixB = { { 4, -7, 3, 1 }, { 4, -12, 3, 1 }, { -4, -12, 3, 1 },
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
            coordinateMatrixF = MatrixF.Clone() as double[,];
            coordinateMatrixB = MatrixB.Clone() as double[,];
        }


        // Сброс координат
        public static void Reset()
        {
            Array.Clear(coordinateMatrixF, 0, coordinateMatrixF.Length);
            Array.Clear(coordinateMatrixB, 0, coordinateMatrixB.Length);
            coordinateMatrixF = MatrixF.Clone() as double[,];
            coordinateMatrixB = MatrixB.Clone() as double[,];
        }


        // Перевод из мировых в экранные
        public static double[,] GetDisplayCoordinates(double[,] matrix)
        {
            var m = matrix.Clone() as double[,];
            for (int i = 0; i < m.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    m[i, j] *= K;
                }
            }

            return m;
        }

        // Настройка системы координат
        private static void SetGraphics(Graphics g)
        {
            // Преобразование системы отсчета
            g.TranslateTransform((float)xc, (float)yc);
            g.ScaleTransform(1.0F, -1.0F);
        }

        // Отрисовка 2D изображения
        public static void DrawXY(Graphics g, Pen pen, double[,] matrix)
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

        // Отрисовка связей для объема фигуры
        public static void DrawConnection(Graphics g, Pen pen, double[,] a, double[,] b)
        {

            for (int i = 0; i < a.GetUpperBound(0) + 1; i++)
            {
                g.DrawLine(pen, (float)a[i, 0], (float)a[i, 1],
                    (float)b[i, 0], (float)b[i, 1]);
            }
        }

        // Отрисовка проекции
        public static void Draw(Graphics g, Pen pen, double[,] ma, double[,] mb)
        {
            SetGraphics(g);
            var a = GetDisplayCoordinates(ma);
            var b = GetDisplayCoordinates(mb);
            DrawXY(g, pen, a);
            DrawXY(g, pen, b);
            DrawConnection(g, pen, a, b);
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
                        newMatrixF[i, j] += coordinateMatrixF[i, k] * rotateT[k, j];
                        newMatrixB[i, j] += coordinateMatrixB[i, k] * rotateT[k, j];
                    }
                }
            }

            coordinateMatrixF = newMatrixF.Clone() as double[,];
            coordinateMatrixB = newMatrixB.Clone() as double[,];

            Draw(g, new Pen(Color.Black, 2), coordinateMatrixF, coordinateMatrixB);
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
                        newMatrixF[i, j] += coordinateMatrixF[i, k] * rotateT[k, j];
                        newMatrixB[i, j] += coordinateMatrixB[i, k] * rotateT[k, j];
                    }
                }
            }

            coordinateMatrixF = newMatrixF.Clone() as double[,];
            coordinateMatrixB = newMatrixB.Clone() as double[,];

            Draw(g, new Pen(Color.Black, 2), coordinateMatrixF, coordinateMatrixB);
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
                        newMatrixF[i, j] += coordinateMatrixF[i, k] * rotateT[k, j];
                        newMatrixB[i, j] += coordinateMatrixB[i, k] * rotateT[k, j];
                    }
                }
            }

            coordinateMatrixF = newMatrixF.Clone() as double[,];
            coordinateMatrixB = newMatrixB.Clone() as double[,];

            Draw(g, new Pen(Color.Black, 2), coordinateMatrixF, coordinateMatrixB);
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
                        newMatrixF[i, j] += coordinateMatrixF[i, k] * scaleT[k, j];
                        newMatrixB[i, j] += coordinateMatrixB[i, k] * scaleT[k, j];
                    }
                }
            }

            coordinateMatrixF = newMatrixF.Clone() as double[,];
            coordinateMatrixB = newMatrixB.Clone() as double[,];

            Draw(g, new Pen(Color.Black, 2), coordinateMatrixF, coordinateMatrixB);
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
                        newMatrixF[i, j] += coordinateMatrixF[i, k] * moveT[k, j];
                        newMatrixB[i, j] += coordinateMatrixB[i, k] * moveT[k, j];
                    }
                }
            }

            coordinateMatrixF = newMatrixF.Clone() as double[,];
            coordinateMatrixB = newMatrixB.Clone() as double[,];

            Draw(g, new Pen(Color.Black, 2), coordinateMatrixF, coordinateMatrixB);
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
            double[,] newMatrixB = new double[18, 4];

            // Перемножение матриц
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        newMatrixF[i, j] += coordinateMatrixF[i, k] * projT[k, j];
                        newMatrixB[i, j] += coordinateMatrixB[i, k] * projT[k, j];
                    }
                }
            }

            Draw(g, new Pen(Color.Red, 2), newMatrixF, newMatrixB);
        }
    }
}

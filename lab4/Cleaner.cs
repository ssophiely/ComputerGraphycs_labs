using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace lab4
{
    // Удаление невидимых линий с помощью алгоритма Робертса
    internal class Cleaner
    {
        private static int[,] buffe;

        // Находим коэфф-т A 
        public static double FindA(double y1, double y2, double y3, double z1, double z2, double z3)
        {
            return y1 * z2 + y2 * z3 + z1 * y3 - (y3 * z2 + z3 * y1 + y2 * z1);
        }

        // Находим коэфф-т B
        public static double FindB(double x1, double x2, double x3, double z1, double z2, double z3)
        {
            return x1 * z2 + x2 * z3 + z1 * x3 - (x3 * z2 + z3 * x1 + x2 * z1);
        }

        // Находим коэфф-т C
        public static double FindC(double x1, double x2, double x3, double y1, double y2, double y3)
        {
            return x1 * y2 + x2 * y3 + y1 * x3 - (x3 * y2 + y3 * x1 + x2 * y1);
        }

        // Находим коэфф-т D
        public static double FindD(double x1, double x2, double x3, double y1, double y2, double y3, double z1, double z2, double z3)
        {
            return x1 * y2 * z3 + x2 * y3 * z1 + y1 * x3 * z2 - (x3 * y2 * z1 + y3 * x1 * z2 + x2 * y1 * z3);
        }

        // Составляем полную матрицу тела
        public static void CreateMatrix()
        {
            // Все плоскости фигуры
            // Передняя грань
            var A1 = FindA(Ship.coordinateMatrixF[0, 1], Ship.coordinateMatrixF[1, 1], Ship.coordinateMatrixF[13, 1],
                Ship.coordinateMatrixF[0, 2], Ship.coordinateMatrixF[1, 2], Ship.coordinateMatrixF[13, 2]);
            var B1 = -FindB(Ship.coordinateMatrixF[0, 0], Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixF[13, 0],
                Ship.coordinateMatrixF[0, 2], Ship.coordinateMatrixF[1, 2], Ship.coordinateMatrixF[13, 2]);
            var C1 = FindC(Ship.coordinateMatrixF[0, 0], Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixF[13, 0],
                Ship.coordinateMatrixF[0, 1], Ship.coordinateMatrixF[1, 1], Ship.coordinateMatrixF[13, 1]);
            var D1 = -FindD(Ship.coordinateMatrixF[0, 0], Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixF[13, 0],
                Ship.coordinateMatrixF[0, 1], Ship.coordinateMatrixF[1, 1], Ship.coordinateMatrixF[13, 1],
                Ship.coordinateMatrixF[0, 2], Ship.coordinateMatrixF[1, 2], Ship.coordinateMatrixF[13, 2]);

            // Задняя грань
            var A2 = -FindA(Ship.coordinateMatrixB[0, 1], Ship.coordinateMatrixB[1, 1], Ship.coordinateMatrixB[13, 1],
                Ship.coordinateMatrixB[0, 2], Ship.coordinateMatrixB[1, 2], Ship.coordinateMatrixB[13, 2]);
            var B2 = FindB(Ship.coordinateMatrixB[0, 0], Ship.coordinateMatrixB[1, 0], Ship.coordinateMatrixB[13, 0],
                Ship.coordinateMatrixB[0, 2], Ship.coordinateMatrixB[1, 2], Ship.coordinateMatrixB[13, 2]);
            var C2 = -FindC(Ship.coordinateMatrixB[0, 0], Ship.coordinateMatrixB[1, 0], Ship.coordinateMatrixB[13, 0],
                Ship.coordinateMatrixB[0, 1], Ship.coordinateMatrixB[1, 1], Ship.coordinateMatrixB[13, 1]);
            var D2 = FindD(Ship.coordinateMatrixB[0, 0], Ship.coordinateMatrixB[1, 0], Ship.coordinateMatrixB[13, 0],
                Ship.coordinateMatrixB[0, 1], Ship.coordinateMatrixB[1, 1], Ship.coordinateMatrixB[13, 1],
                Ship.coordinateMatrixB[0, 2], Ship.coordinateMatrixB[1, 2], Ship.coordinateMatrixB[13, 2]);

            // Верх судна
            var A3 = -FindA(Ship.coordinateMatrixF[4, 1], Ship.coordinateMatrixF[5, 1], Ship.coordinateMatrixB[5, 1],
                Ship.coordinateMatrixF[4, 2], Ship.coordinateMatrixF[5, 2], Ship.coordinateMatrixB[5, 2]);
            var B3 = FindB(Ship.coordinateMatrixF[4, 0], Ship.coordinateMatrixF[5, 0], Ship.coordinateMatrixB[5, 0],
                Ship.coordinateMatrixF[4, 2], Ship.coordinateMatrixF[5, 2], Ship.coordinateMatrixB[5, 2]);
            var C3 = -FindC(Ship.coordinateMatrixF[4, 0], Ship.coordinateMatrixF[5, 0], Ship.coordinateMatrixB[5, 0],
                Ship.coordinateMatrixF[4, 1], Ship.coordinateMatrixF[5, 1], Ship.coordinateMatrixB[5, 1]);
            var D3 = FindD(Ship.coordinateMatrixF[4, 0], Ship.coordinateMatrixF[5, 0], Ship.coordinateMatrixB[5, 0],
                Ship.coordinateMatrixF[4, 1], Ship.coordinateMatrixF[5, 1], Ship.coordinateMatrixB[5, 1],
                Ship.coordinateMatrixF[4, 2], Ship.coordinateMatrixF[5, 2], Ship.coordinateMatrixB[5, 2]);

            // Левая грань судна
            var A4 = -FindA(Ship.coordinateMatrixF[2, 1], Ship.coordinateMatrixF[5, 1], Ship.coordinateMatrixB[5, 1],
                Ship.coordinateMatrixF[2, 2], Ship.coordinateMatrixF[5, 2], Ship.coordinateMatrixB[5, 2]);
            var B4 = FindB(Ship.coordinateMatrixF[2, 0], Ship.coordinateMatrixF[5, 0], Ship.coordinateMatrixB[5, 0],
                Ship.coordinateMatrixF[2, 2], Ship.coordinateMatrixF[5, 2], Ship.coordinateMatrixB[5, 2]);
            var C4 = -FindC(Ship.coordinateMatrixF[2, 0], Ship.coordinateMatrixF[5, 0], Ship.coordinateMatrixB[5, 0],
                Ship.coordinateMatrixF[2, 1], Ship.coordinateMatrixF[5, 1], Ship.coordinateMatrixB[5, 1]);
            var D4 = FindD(Ship.coordinateMatrixF[2, 0], Ship.coordinateMatrixF[5, 0], Ship.coordinateMatrixB[5, 0],
                Ship.coordinateMatrixF[2, 1], Ship.coordinateMatrixF[5, 1], Ship.coordinateMatrixB[5, 1],
                Ship.coordinateMatrixF[2, 2], Ship.coordinateMatrixF[5, 2], Ship.coordinateMatrixB[5, 2]);

            // Правая грань судна
            var A5 = FindA(Ship.coordinateMatrixF[1, 1], Ship.coordinateMatrixF[4, 1], Ship.coordinateMatrixB[4, 1],
                Ship.coordinateMatrixF[1, 2], Ship.coordinateMatrixF[4, 2], Ship.coordinateMatrixB[4, 2]);
            var B5 = -FindB(Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixF[4, 0], Ship.coordinateMatrixB[4, 0],
                Ship.coordinateMatrixF[1, 2], Ship.coordinateMatrixF[4, 2], Ship.coordinateMatrixB[4, 2]);
            var C5 = FindC(Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixF[4, 0], Ship.coordinateMatrixB[4, 0],
                Ship.coordinateMatrixF[1, 1], Ship.coordinateMatrixF[4, 1], Ship.coordinateMatrixB[4, 1]);
            var D5 = -FindD(Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixF[4, 0], Ship.coordinateMatrixB[4, 0],
                Ship.coordinateMatrixF[1, 1], Ship.coordinateMatrixF[4, 1], Ship.coordinateMatrixB[4, 1],
                Ship.coordinateMatrixF[1, 2], Ship.coordinateMatrixF[4, 2], Ship.coordinateMatrixB[4, 2]);

            // Дно судна
            var A6 = FindA(Ship.coordinateMatrixF[2, 1], Ship.coordinateMatrixF[1, 1], Ship.coordinateMatrixB[1, 1],
                Ship.coordinateMatrixF[2, 2], Ship.coordinateMatrixF[1, 2], Ship.coordinateMatrixB[1, 2]);
            var B6 = -FindB(Ship.coordinateMatrixF[2, 0], Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixB[1, 0],
                Ship.coordinateMatrixF[2, 2], Ship.coordinateMatrixF[1, 2], Ship.coordinateMatrixB[1, 2]);
            var C6 = FindC(Ship.coordinateMatrixF[2, 0], Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixB[1, 0],
                Ship.coordinateMatrixF[2, 1], Ship.coordinateMatrixF[1, 1], Ship.coordinateMatrixB[1, 1]);
            var D6 = -FindD(Ship.coordinateMatrixF[2, 0], Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixB[1, 0],
                Ship.coordinateMatrixF[2, 1], Ship.coordinateMatrixF[1, 1], Ship.coordinateMatrixB[1, 1],
                Ship.coordinateMatrixF[2, 2], Ship.coordinateMatrixF[1, 2], Ship.coordinateMatrixB[1, 2]);

            // Низ левого паруса
            var A7 = -FindA(Ship.coordinateMatrixF[14, 1], Ship.coordinateMatrixF[15, 1], Ship.coordinateMatrixB[15, 1],
                Ship.coordinateMatrixF[14, 2], Ship.coordinateMatrixF[15, 2], Ship.coordinateMatrixB[15, 2]);
            var B7 = FindB(Ship.coordinateMatrixF[14, 0], Ship.coordinateMatrixF[15, 0], Ship.coordinateMatrixB[15, 0],
                Ship.coordinateMatrixF[14, 2], Ship.coordinateMatrixF[15, 2], Ship.coordinateMatrixB[15, 2]);
            var C7 = -FindC(Ship.coordinateMatrixF[14, 0], Ship.coordinateMatrixF[15, 0], Ship.coordinateMatrixB[15, 0],
                Ship.coordinateMatrixF[14, 1], Ship.coordinateMatrixF[15, 1], Ship.coordinateMatrixB[15, 1]);
            var D7 = FindD(Ship.coordinateMatrixF[14, 0], Ship.coordinateMatrixF[15, 0], Ship.coordinateMatrixB[15, 0],
                Ship.coordinateMatrixF[14, 1], Ship.coordinateMatrixF[15, 1], Ship.coordinateMatrixB[15, 1],
                Ship.coordinateMatrixF[14, 2], Ship.coordinateMatrixF[15, 2], Ship.coordinateMatrixB[15, 2]);

            // Верх левого паруса
            var A8 = FindA(Ship.coordinateMatrixF[13, 1], Ship.coordinateMatrixF[15, 1], Ship.coordinateMatrixB[15, 1],
                 Ship.coordinateMatrixF[13, 2], Ship.coordinateMatrixF[15, 2], Ship.coordinateMatrixB[15, 2]);
            var B8 = -FindB(Ship.coordinateMatrixF[13, 0], Ship.coordinateMatrixF[15, 0], Ship.coordinateMatrixB[15, 0],
                Ship.coordinateMatrixF[13, 2], Ship.coordinateMatrixF[15, 2], Ship.coordinateMatrixB[15, 2]);
            var C8 = FindC(Ship.coordinateMatrixF[13, 0], Ship.coordinateMatrixF[15, 0], Ship.coordinateMatrixB[15, 0],
                Ship.coordinateMatrixF[13, 1], Ship.coordinateMatrixF[15, 1], Ship.coordinateMatrixB[15, 1]);
            var D8 = -FindD(Ship.coordinateMatrixF[13, 0], Ship.coordinateMatrixF[15, 0], Ship.coordinateMatrixB[15, 0],
                Ship.coordinateMatrixF[13, 1], Ship.coordinateMatrixF[15, 1], Ship.coordinateMatrixB[15, 1],
                Ship.coordinateMatrixF[13, 2], Ship.coordinateMatrixF[15, 2], Ship.coordinateMatrixB[15, 2]);

            // Низ правого паруса
            var A9 = FindA(Ship.coordinateMatrixF[11, 1], Ship.coordinateMatrixF[12, 1], Ship.coordinateMatrixB[12, 1],
                Ship.coordinateMatrixF[11, 2], Ship.coordinateMatrixF[12, 2], Ship.coordinateMatrixB[12, 2]);
            var B9 = -FindB(Ship.coordinateMatrixF[11, 0], Ship.coordinateMatrixF[12, 0], Ship.coordinateMatrixB[12, 0],
                Ship.coordinateMatrixF[11, 2], Ship.coordinateMatrixF[12, 2], Ship.coordinateMatrixB[12, 2]);
            var C9 = FindC(Ship.coordinateMatrixF[11, 0], Ship.coordinateMatrixF[12, 0], Ship.coordinateMatrixB[12, 0],
                Ship.coordinateMatrixF[11, 1], Ship.coordinateMatrixF[12, 1], Ship.coordinateMatrixB[12, 1]);
            var D9 = -FindD(Ship.coordinateMatrixF[11, 0], Ship.coordinateMatrixF[12, 0], Ship.coordinateMatrixB[12, 0],
                Ship.coordinateMatrixF[11, 1], Ship.coordinateMatrixF[12, 1], Ship.coordinateMatrixB[12, 1],
                Ship.coordinateMatrixF[11, 2], Ship.coordinateMatrixF[12, 2], Ship.coordinateMatrixB[12, 2]);

            // Верх правого паруса
            var A10 = -FindA(Ship.coordinateMatrixF[10, 1], Ship.coordinateMatrixF[12, 1], Ship.coordinateMatrixB[12, 1],
                Ship.coordinateMatrixF[10, 2], Ship.coordinateMatrixF[12, 2], Ship.coordinateMatrixB[12, 2]);
            var B10 = FindB(Ship.coordinateMatrixF[10, 0], Ship.coordinateMatrixF[12, 0], Ship.coordinateMatrixB[12, 0],
                Ship.coordinateMatrixF[10, 2], Ship.coordinateMatrixF[12, 2], Ship.coordinateMatrixB[12, 2]);
            var C10 = -FindC(Ship.coordinateMatrixF[10, 0], Ship.coordinateMatrixF[12, 0], Ship.coordinateMatrixB[12, 0],
                Ship.coordinateMatrixF[10, 1], Ship.coordinateMatrixF[12, 1], Ship.coordinateMatrixB[12, 1]);
            var D10 = FindD(Ship.coordinateMatrixF[10, 0], Ship.coordinateMatrixF[12, 0], Ship.coordinateMatrixB[12, 0],
                Ship.coordinateMatrixF[10, 1], Ship.coordinateMatrixF[12, 1], Ship.coordinateMatrixB[12, 1],
                Ship.coordinateMatrixF[10, 2], Ship.coordinateMatrixF[12, 2], Ship.coordinateMatrixB[12, 2]);

            // Верх флага
            var A11 = FindA(Ship.coordinateMatrixF[9, 1], Ship.coordinateMatrixF[17, 1], Ship.coordinateMatrixB[17, 1],
                Ship.coordinateMatrixF[9, 2], Ship.coordinateMatrixF[17, 2], Ship.coordinateMatrixB[17, 2]);
            var B11 = -FindB(Ship.coordinateMatrixF[9, 0], Ship.coordinateMatrixF[17, 0], Ship.coordinateMatrixB[17, 0],
                Ship.coordinateMatrixF[9, 2], Ship.coordinateMatrixF[17, 2], Ship.coordinateMatrixB[17, 2]);
            var C11 = FindC(Ship.coordinateMatrixF[9, 0], Ship.coordinateMatrixF[17, 0], Ship.coordinateMatrixB[17, 0],
                Ship.coordinateMatrixF[9, 1], Ship.coordinateMatrixF[17, 1], Ship.coordinateMatrixB[17, 1]);
            var D11 = -FindD(Ship.coordinateMatrixF[9, 0], Ship.coordinateMatrixF[17, 0], Ship.coordinateMatrixB[17, 0],
                Ship.coordinateMatrixF[9, 1], Ship.coordinateMatrixF[17, 1], Ship.coordinateMatrixB[17, 1],
                Ship.coordinateMatrixF[9, 2], Ship.coordinateMatrixF[17, 2], Ship.coordinateMatrixB[17, 2]);

            // Низ флага
            var A12 = FindA(Ship.coordinateMatrixF[16, 1], Ship.coordinateMatrixF[17, 1], Ship.coordinateMatrixB[17, 1],
                Ship.coordinateMatrixF[16, 2], Ship.coordinateMatrixF[17, 2], Ship.coordinateMatrixB[17, 2]);
            var B12 = -FindB(Ship.coordinateMatrixF[16, 0], Ship.coordinateMatrixF[17, 0], Ship.coordinateMatrixB[17, 0],
                Ship.coordinateMatrixF[16, 2], Ship.coordinateMatrixF[17, 2], Ship.coordinateMatrixB[17, 2]);
            var C12 = FindC(Ship.coordinateMatrixF[16, 0], Ship.coordinateMatrixF[17, 0], Ship.coordinateMatrixB[17, 0],
                Ship.coordinateMatrixF[16, 1], Ship.coordinateMatrixF[17, 1], Ship.coordinateMatrixB[17, 1]);
            var D12 = -FindD(Ship.coordinateMatrixF[16, 0], Ship.coordinateMatrixF[17, 0], Ship.coordinateMatrixB[17, 0],
                Ship.coordinateMatrixF[16, 1], Ship.coordinateMatrixF[17, 1], Ship.coordinateMatrixB[17, 1],
                Ship.coordinateMatrixF[16, 2], Ship.coordinateMatrixF[17, 2], Ship.coordinateMatrixB[17, 2]);

            // Верхушка
            var A13 = -FindA(Ship.coordinateMatrixF[9, 1], Ship.coordinateMatrixF[6, 1], Ship.coordinateMatrixB[6, 1],
                Ship.coordinateMatrixF[9, 2], Ship.coordinateMatrixF[6, 2], Ship.coordinateMatrixB[6, 2]);
            var B13 = FindB(Ship.coordinateMatrixF[9, 0], Ship.coordinateMatrixF[6, 0], Ship.coordinateMatrixB[6, 0],
                Ship.coordinateMatrixF[9, 2], Ship.coordinateMatrixF[6, 2], Ship.coordinateMatrixB[6, 2]);
            var C13 = -FindC(Ship.coordinateMatrixF[9, 0], Ship.coordinateMatrixF[6, 0], Ship.coordinateMatrixB[6, 0],
                Ship.coordinateMatrixF[9, 1], Ship.coordinateMatrixF[6, 1], Ship.coordinateMatrixB[6, 1]);
            var D13 = FindD(Ship.coordinateMatrixF[9, 0], Ship.coordinateMatrixF[6, 0], Ship.coordinateMatrixB[6, 0],
                Ship.coordinateMatrixF[9, 1], Ship.coordinateMatrixF[6, 1], Ship.coordinateMatrixB[6, 1],
                Ship.coordinateMatrixF[9, 2], Ship.coordinateMatrixF[6, 2], Ship.coordinateMatrixB[6, 2]);

            // Верхняя правая часть мачты
            var A14 = FindA(Ship.coordinateMatrixF[10, 1], Ship.coordinateMatrixF[6, 1], Ship.coordinateMatrixB[6, 1],
                Ship.coordinateMatrixF[10, 2], Ship.coordinateMatrixF[6, 2], Ship.coordinateMatrixB[6, 2]);
            var B14 = -FindB(Ship.coordinateMatrixF[10, 0], Ship.coordinateMatrixF[6, 0], Ship.coordinateMatrixB[6, 0],
                Ship.coordinateMatrixF[10, 2], Ship.coordinateMatrixF[6, 2], Ship.coordinateMatrixB[6, 2]);
            var C14 = FindC(Ship.coordinateMatrixF[10, 0], Ship.coordinateMatrixF[6, 0], Ship.coordinateMatrixB[6, 0],
                Ship.coordinateMatrixF[10, 1], Ship.coordinateMatrixF[6, 1], Ship.coordinateMatrixB[6, 1]);
            var D14 = -FindD(Ship.coordinateMatrixF[10, 0], Ship.coordinateMatrixF[6, 0], Ship.coordinateMatrixB[6, 0],
                Ship.coordinateMatrixF[10, 1], Ship.coordinateMatrixF[6, 1], Ship.coordinateMatrixB[6, 1],
                Ship.coordinateMatrixF[10, 2], Ship.coordinateMatrixF[6, 2], Ship.coordinateMatrixB[6, 2]);

            // Верхняя левая часть мачты
            var A15 = FindA(Ship.coordinateMatrixF[13, 1], Ship.coordinateMatrixF[15, 1], Ship.coordinateMatrixB[15, 1],
                Ship.coordinateMatrixF[13, 2], Ship.coordinateMatrixF[15, 2], Ship.coordinateMatrixB[15, 2]);
            var B15 = -FindB(Ship.coordinateMatrixF[13, 0], Ship.coordinateMatrixF[15, 0], Ship.coordinateMatrixB[15, 0],
                Ship.coordinateMatrixF[13, 2], Ship.coordinateMatrixF[15, 2], Ship.coordinateMatrixB[15, 2]);
            var C15 = FindC(Ship.coordinateMatrixF[13, 0], Ship.coordinateMatrixF[15, 0], Ship.coordinateMatrixB[15, 0],
                Ship.coordinateMatrixF[13, 1], Ship.coordinateMatrixF[15, 1], Ship.coordinateMatrixB[15, 1]);
            var D15 = -FindD(Ship.coordinateMatrixF[13, 0], Ship.coordinateMatrixF[17, 0], Ship.coordinateMatrixB[17, 0],
                Ship.coordinateMatrixF[13, 1], Ship.coordinateMatrixF[15, 1], Ship.coordinateMatrixB[15, 1],
                Ship.coordinateMatrixF[13, 2], Ship.coordinateMatrixF[15, 2], Ship.coordinateMatrixB[15, 2]);

            // Нижняя левая часть мачты
            var A16 = FindA(Ship.coordinateMatrixF[14, 1], Ship.coordinateMatrixF[8, 1], Ship.coordinateMatrixB[8, 1],
                Ship.coordinateMatrixF[14, 2], Ship.coordinateMatrixF[8, 2], Ship.coordinateMatrixB[8, 2]);
            var B16 = -FindB(Ship.coordinateMatrixF[14, 0], Ship.coordinateMatrixF[8, 0], Ship.coordinateMatrixB[8, 0],
                Ship.coordinateMatrixF[14, 2], Ship.coordinateMatrixF[8, 2], Ship.coordinateMatrixB[8, 2]);
            var C16 = FindC(Ship.coordinateMatrixF[14, 0], Ship.coordinateMatrixF[8, 0], Ship.coordinateMatrixB[8, 0],
                Ship.coordinateMatrixF[14, 1], Ship.coordinateMatrixF[8, 1], Ship.coordinateMatrixB[8, 1]);
            var D16 = -FindD(Ship.coordinateMatrixF[14, 0], Ship.coordinateMatrixF[8, 0], Ship.coordinateMatrixB[8, 0],
                Ship.coordinateMatrixF[14, 1], Ship.coordinateMatrixF[8, 1], Ship.coordinateMatrixB[8, 1],
                Ship.coordinateMatrixF[14, 2], Ship.coordinateMatrixF[8, 2], Ship.coordinateMatrixB[8, 2]);

            // Нижняя правая часть мачты
            var A17 = -FindA(Ship.coordinateMatrixF[11, 1], Ship.coordinateMatrixF[7, 1], Ship.coordinateMatrixB[7, 1],
                Ship.coordinateMatrixF[11, 2], Ship.coordinateMatrixF[7, 2], Ship.coordinateMatrixB[7, 2]);
            var B17 = FindB(Ship.coordinateMatrixF[11, 0], Ship.coordinateMatrixF[7, 0], Ship.coordinateMatrixB[7, 0],
                Ship.coordinateMatrixF[11, 2], Ship.coordinateMatrixF[7, 2], Ship.coordinateMatrixB[7, 2]);
            var C17 = -FindC(Ship.coordinateMatrixF[11, 0], Ship.coordinateMatrixF[7, 0], Ship.coordinateMatrixB[7, 0],
                Ship.coordinateMatrixF[11, 1], Ship.coordinateMatrixF[7, 1], Ship.coordinateMatrixB[7, 1]);
            var D17 = FindD(Ship.coordinateMatrixF[11, 0], Ship.coordinateMatrixF[7, 0], Ship.coordinateMatrixB[7, 0],
                Ship.coordinateMatrixF[11, 1], Ship.coordinateMatrixF[7, 1], Ship.coordinateMatrixB[7, 1],
                Ship.coordinateMatrixF[11, 2], Ship.coordinateMatrixF[7, 2], Ship.coordinateMatrixB[7, 2]);

            double[] x = new double[] { 0, -250, -250, 1 };
            double[,] m = new double[,] { { A1,A2,A3,A4,A5,A6,A7,A8,A9,A10,A11,A12,A13,A14,A15,A16,A17 },
                { B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17},
                {C1,C2,C3,C4,C5,C6,C7,C8,C9,C10,C11,C12,C13,C14,C15,C16,C17 },
                { D1,D2,D3,D4,D5,D6,D7,D8,D9,D10,D11,D12,D13,D14,D15,D16,D17} };
            double[] result = new double[17];

            // Перемножение матриц
            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result[i] += x[j] * m[j, i];
                }
            }

            //for (int i = 0; i < 17; i++)
            //{
            //    if (result[i] < 0)
            //        ChangeLines(result, i, Form1.g, new Pen(Color.White, 2));
            //}

            //for (int i = 0; i < 17; i++)
            //{
            //    if (result[i] > 0)
            //        ChangeLines(result, i, Form1.g, new Pen(Color.Black, 2));
            //}
        }

        public static void DeleteLines(Bitmap main, Bitmap extra)
        {
            int w = extra.Width;
            int h = extra.Height;

            // буфер заполнили минимальными значениями
            int[,] buffer = new int[h, w];
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    buffer[i, j] = Int32.MinValue;
                }
            }

            Graphics eg = Graphics.FromImage(extra);
            eg.TranslateTransform((float)w / 2, (float)h / 2);
            eg.ScaleTransform(1.0F, -1.0F);
            eg.Clear(Color.White);

            Graphics mg = Graphics.FromImage(main);
            mg.TranslateTransform((float)w / 2, (float)h / 2);
            mg.ScaleTransform(1.0F, -1.0F);
            mg.Clear(Color.White);

            for (int i = 0; i < 17; i++)
            {
                DrawPlane(i, mg, eg, new Pen(Color.Black, 2));

                // просматриваем грань 
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        Color pix = extra.GetPixel(x, y);
                        if (pix.R == 0 && pix.G == 0 && pix.B == 0)
                        {
                            buffer[y, x] = 1;
                            int ind = x + 1;
                            for (int k = x + 1; k < w; k++)
                            {
                                Color px = extra.GetPixel(k, y);
                                if (px.R == 0 && px.G == 0 && px.B == 0)
                                {
                                    buffer[y, k] = 1;
                                    ind = k;
                                }
                            }
                            for (int k = x + 1; k < ind; k++)
                            {
                                buffer[y, k] = 1;
                            }
                            break;
                        }
                    }
                }
            }


        }

        // Отрисовка всех поверхностей корабля
        public static void DrawPlane(int n, Graphics g, Graphics ex, Pen pen)
        {
            var a = Ship.GetDisplayCoordinates(Ship.coordinateMatrixF);
            var b = Ship.GetDisplayCoordinates(Ship.coordinateMatrixB);

            switch (n)
            {
                case 0:
                    for (int i = 0; i < Ship.contiguityMatrix.GetUpperBound(0) + 1; i++)
                    {
                        for (int j = 0; j < Ship.contiguityMatrix.GetUpperBound(0) + 1; j++)
                        {
                            if (Ship.contiguityMatrix[i, j] == 1)  // смотрим по матрице смежности
                            {
                                g.DrawLine(pen, (float)a[i, 0], (float)a[i, 1],
                                    (float)a[j, 0], (float)a[j, 1]);
                                ex.DrawLine(pen, (float)a[i, 0], (float)a[i, 1],
                                    (float)a[j, 0], (float)a[j, 1]);
                            }
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < Ship.contiguityMatrix.GetUpperBound(0) + 1; i++)
                    {
                        for (int j = 0; j < Ship.contiguityMatrix.GetUpperBound(0) + 1; j++)
                        {
                            if (Ship.contiguityMatrix[i, j] == 1)  // смотрим по матрице смежности
                            {
                                g.DrawLine(pen, (float)b[i, 0], (float)b[i, 1],
                                    (float)b[j, 0], (float)b[j, 1]);
                                ex.DrawLine(pen, (float)b[i, 0], (float)b[i, 1],
                                    (float)b[j, 0], (float)b[j, 1]);
                            }
                        }
                    }
                    break;
                case 2:
                    g.DrawLine(pen, (float)a[5, 0], (float)a[5, 1], (float)a[4, 0], (float)a[4, 1]);
                    g.DrawLine(pen, (float)b[5, 0], (float)b[5, 1], (float)b[4, 0], (float)b[4, 1]);
                    g.DrawLine(pen, (float)a[5, 0], (float)a[5, 1], (float)b[5, 0], (float)b[5, 1]);
                    g.DrawLine(pen, (float)a[4, 0], (float)a[4, 1], (float)b[4, 0], (float)b[4, 1]);
                    g.DrawLine(pen, (float)a[3, 0], (float)a[3, 1], (float)b[3, 0], (float)b[3, 1]);
                    g.DrawLine(pen, (float)a[0, 0], (float)a[0, 1], (float)b[0, 0], (float)b[0, 1]);
                    g.DrawLine(pen, (float)a[7, 0], (float)a[7, 1], (float)b[7, 0], (float)b[7, 1]);
                    g.DrawLine(pen, (float)a[7, 0], (float)a[7, 1], (float)b[7, 0], (float)b[7, 1]);

                    ex.DrawLine(pen, (float)a[5, 0], (float)a[5, 1], (float)a[4, 0], (float)a[4, 1]);
                    ex.DrawLine(pen, (float)b[5, 0], (float)b[5, 1], (float)b[4, 0], (float)b[4, 1]);
                    ex.DrawLine(pen, (float)a[5, 0], (float)a[5, 1], (float)b[5, 0], (float)b[5, 1]);
                    ex.DrawLine(pen, (float)a[4, 0], (float)a[4, 1], (float)b[4, 0], (float)b[4, 1]);
                    ex.DrawLine(pen, (float)a[3, 0], (float)a[3, 1], (float)b[3, 0], (float)b[3, 1]);
                    ex.DrawLine(pen, (float)a[0, 0], (float)a[0, 1], (float)b[0, 0], (float)b[0, 1]);
                    ex.DrawLine(pen, (float)a[7, 0], (float)a[7, 1], (float)b[7, 0], (float)b[7, 1]);
                    ex.DrawLine(pen, (float)a[7, 0], (float)a[7, 1], (float)b[7, 0], (float)b[7, 1]);
                    break;
                case 3:
                    g.DrawLine(pen, (float)a[2, 0], (float)a[2, 1], (float)a[5, 0], (float)a[5, 1]);
                    g.DrawLine(pen, (float)b[2, 0], (float)b[2, 1], (float)b[5, 0], (float)b[5, 1]);
                    g.DrawLine(pen, (float)a[5, 0], (float)a[5, 1], (float)b[5, 0], (float)b[5, 1]);
                    g.DrawLine(pen, (float)a[2, 0], (float)a[2, 1], (float)b[2, 0], (float)b[2, 1]);

                    ex.DrawLine(pen, (float)a[2, 0], (float)a[2, 1], (float)a[5, 0], (float)a[5, 1]);
                    ex.DrawLine(pen, (float)b[2, 0], (float)b[2, 1], (float)b[5, 0], (float)b[5, 1]);
                    ex.DrawLine(pen, (float)a[5, 0], (float)a[5, 1], (float)b[5, 0], (float)b[5, 1]);
                    ex.DrawLine(pen, (float)a[2, 0], (float)a[2, 1], (float)b[2, 0], (float)b[2, 1]);
                    break;
                case 4:
                    g.DrawLine(pen, (float)a[1, 0], (float)a[1, 1], (float)a[4, 0], (float)a[4, 1]);
                    g.DrawLine(pen, (float)b[1, 0], (float)b[1, 1], (float)b[4, 0], (float)b[4, 1]);
                    g.DrawLine(pen, (float)a[1, 0], (float)a[1, 1], (float)b[1, 0], (float)b[1, 1]);
                    g.DrawLine(pen, (float)a[4, 0], (float)a[4, 1], (float)b[4, 0], (float)b[4, 1]);

                    ex.DrawLine(pen, (float)a[1, 0], (float)a[1, 1], (float)a[4, 0], (float)a[4, 1]);
                    ex.DrawLine(pen, (float)b[1, 0], (float)b[1, 1], (float)b[4, 0], (float)b[4, 1]);
                    ex.DrawLine(pen, (float)a[1, 0], (float)a[1, 1], (float)b[1, 0], (float)b[1, 1]);
                    ex.DrawLine(pen, (float)a[4, 0], (float)a[4, 1], (float)b[4, 0], (float)b[4, 1]);
                    break;
                case 5:
                    g.DrawLine(pen, (float)a[1, 0], (float)a[1, 1], (float)a[2, 0], (float)a[2, 1]);
                    g.DrawLine(pen, (float)b[1, 0], (float)b[1, 1], (float)b[2, 0], (float)b[2, 1]);
                    g.DrawLine(pen, (float)a[1, 0], (float)a[1, 1], (float)b[1, 0], (float)b[1, 1]);
                    g.DrawLine(pen, (float)a[2, 0], (float)a[2, 1], (float)b[2, 0], (float)b[2, 1]);

                    ex.DrawLine(pen, (float)a[1, 0], (float)a[1, 1], (float)a[2, 0], (float)a[2, 1]);
                    ex.DrawLine(pen, (float)b[1, 0], (float)b[1, 1], (float)b[2, 0], (float)b[2, 1]);
                    ex.DrawLine(pen, (float)a[1, 0], (float)a[1, 1], (float)b[1, 0], (float)b[1, 1]);
                    ex.DrawLine(pen, (float)a[2, 0], (float)a[2, 1], (float)b[2, 0], (float)b[2, 1]);
                    break;
                case 6:
                    g.DrawLine(pen, (float)a[14, 0], (float)a[14, 1], (float)a[15, 0], (float)a[15, 1]);
                    g.DrawLine(pen, (float)b[14, 0], (float)b[14, 1], (float)b[15, 0], (float)b[15, 1]);
                    g.DrawLine(pen, (float)a[14, 0], (float)a[14, 1], (float)b[14, 0], (float)b[14, 1]);
                    g.DrawLine(pen, (float)a[15, 0], (float)a[15, 1], (float)b[15, 0], (float)b[15, 1]);

                    ex.DrawLine(pen, (float)a[14, 0], (float)a[14, 1], (float)a[15, 0], (float)a[15, 1]);
                    ex.DrawLine(pen, (float)b[14, 0], (float)b[14, 1], (float)b[15, 0], (float)b[15, 1]);
                    ex.DrawLine(pen, (float)a[14, 0], (float)a[14, 1], (float)b[14, 0], (float)b[14, 1]);
                    ex.DrawLine(pen, (float)a[15, 0], (float)a[15, 1], (float)b[15, 0], (float)b[15, 1]);
                    break;
                case 7:
                    g.DrawLine(pen, (float)a[13, 0], (float)a[13, 1], (float)a[15, 0], (float)a[15, 1]);
                    g.DrawLine(pen, (float)b[13, 0], (float)b[13, 1], (float)b[15, 0], (float)b[15, 1]);
                    g.DrawLine(pen, (float)a[13, 0], (float)a[13, 1], (float)b[13, 0], (float)b[13, 1]);
                    g.DrawLine(pen, (float)a[15, 0], (float)a[15, 1], (float)b[15, 0], (float)b[15, 1]);

                    ex.DrawLine(pen, (float)a[13, 0], (float)a[13, 1], (float)a[15, 0], (float)a[15, 1]);
                    ex.DrawLine(pen, (float)b[13, 0], (float)b[13, 1], (float)b[15, 0], (float)b[15, 1]);
                    ex.DrawLine(pen, (float)a[13, 0], (float)a[13, 1], (float)b[13, 0], (float)b[13, 1]);
                    ex.DrawLine(pen, (float)a[15, 0], (float)a[15, 1], (float)b[15, 0], (float)b[15, 1]);
                    break;
                case 8: // правый низ
                    g.DrawLine(pen, (float)a[11, 0], (float)a[11, 1], (float)a[12, 0], (float)a[12, 1]);
                    g.DrawLine(pen, (float)b[11, 0], (float)b[11, 1], (float)b[12, 0], (float)b[12, 1]);
                    g.DrawLine(pen, (float)a[11, 0], (float)a[11, 1], (float)b[11, 0], (float)b[11, 1]);
                    g.DrawLine(pen, (float)a[12, 0], (float)a[12, 1], (float)b[12, 0], (float)b[12, 1]);

                    ex.DrawLine(pen, (float)a[11, 0], (float)a[11, 1], (float)a[12, 0], (float)a[12, 1]);
                    ex.DrawLine(pen, (float)b[11, 0], (float)b[11, 1], (float)b[12, 0], (float)b[12, 1]);
                    ex.DrawLine(pen, (float)a[11, 0], (float)a[11, 1], (float)b[11, 0], (float)b[11, 1]);
                    ex.DrawLine(pen, (float)a[12, 0], (float)a[12, 1], (float)b[12, 0], (float)b[12, 1]);
                    break;
                case 9:
                    g.DrawLine(pen, (float)a[10, 0], (float)a[10, 1], (float)a[12, 0], (float)a[12, 1]);
                    g.DrawLine(pen, (float)b[10, 0], (float)b[10, 1], (float)b[12, 0], (float)b[12, 1]);
                    g.DrawLine(pen, (float)a[10, 0], (float)a[10, 1], (float)b[10, 0], (float)b[10, 1]);
                    g.DrawLine(pen, (float)a[12, 0], (float)a[12, 1], (float)b[12, 0], (float)b[12, 1]);

                    ex.DrawLine(pen, (float)a[10, 0], (float)a[10, 1], (float)a[12, 0], (float)a[12, 1]);
                    ex.DrawLine(pen, (float)b[10, 0], (float)b[10, 1], (float)b[12, 0], (float)b[12, 1]);
                    ex.DrawLine(pen, (float)a[10, 0], (float)a[10, 1], (float)b[10, 0], (float)b[10, 1]);
                    ex.DrawLine(pen, (float)a[12, 0], (float)a[12, 1], (float)b[12, 0], (float)b[12, 1]);
                    break;
                case 10:
                    g.DrawLine(pen, (float)a[9, 0], (float)a[9, 1], (float)a[17, 0], (float)a[17, 1]);
                    g.DrawLine(pen, (float)b[9, 0], (float)b[9, 1], (float)b[17, 0], (float)b[17, 1]);
                    g.DrawLine(pen, (float)a[9, 0], (float)a[9, 1], (float)b[9, 0], (float)b[9, 1]);
                    g.DrawLine(pen, (float)a[17, 0], (float)a[17, 1], (float)b[17, 0], (float)b[17, 1]);

                    ex.DrawLine(pen, (float)a[9, 0], (float)a[9, 1], (float)a[17, 0], (float)a[17, 1]);
                    ex.DrawLine(pen, (float)b[9, 0], (float)b[9, 1], (float)b[17, 0], (float)b[17, 1]);
                    ex.DrawLine(pen, (float)a[9, 0], (float)a[9, 1], (float)b[9, 0], (float)b[9, 1]);
                    ex.DrawLine(pen, (float)a[17, 0], (float)a[17, 1], (float)b[17, 0], (float)b[17, 1]);
                    break;
                case 11:
                    g.DrawLine(pen, (float)a[16, 0], (float)a[16, 1], (float)a[17, 0], (float)a[17, 1]);
                    g.DrawLine(pen, (float)b[16, 0], (float)b[16, 1], (float)b[17, 0], (float)b[17, 1]);
                    g.DrawLine(pen, (float)a[16, 0], (float)a[16, 1], (float)b[16, 0], (float)b[16, 1]);
                    g.DrawLine(pen, (float)a[17, 0], (float)a[17, 1], (float)b[17, 0], (float)b[17, 1]);

                    ex.DrawLine(pen, (float)a[16, 0], (float)a[16, 1], (float)a[17, 0], (float)a[17, 1]);
                    ex.DrawLine(pen, (float)b[16, 0], (float)b[16, 1], (float)b[17, 0], (float)b[17, 1]);
                    ex.DrawLine(pen, (float)a[16, 0], (float)a[16, 1], (float)b[16, 0], (float)b[16, 1]);
                    ex.DrawLine(pen, (float)a[17, 0], (float)a[17, 1], (float)b[17, 0], (float)b[17, 1]);
                    break;
                case 12: // верхушка
                    g.DrawLine(pen, (float)a[9, 0], (float)a[9, 1], (float)a[6, 0], (float)a[6, 1]);
                    g.DrawLine(pen, (float)b[9, 0], (float)b[9, 1], (float)b[6, 0], (float)b[6, 1]);
                    g.DrawLine(pen, (float)a[9, 0], (float)a[9, 1], (float)b[9, 0], (float)b[9, 1]);
                    g.DrawLine(pen, (float)a[6, 0], (float)a[6, 1], (float)b[6, 0], (float)b[6, 1]);

                    ex.DrawLine(pen, (float)a[9, 0], (float)a[9, 1], (float)a[6, 0], (float)a[6, 1]);
                    ex.DrawLine(pen, (float)b[9, 0], (float)b[9, 1], (float)b[6, 0], (float)b[6, 1]);
                    ex.DrawLine(pen, (float)a[9, 0], (float)a[9, 1], (float)b[9, 0], (float)b[9, 1]);
                    ex.DrawLine(pen, (float)a[6, 0], (float)a[6, 1], (float)b[6, 0], (float)b[6, 1]);
                    break;
                case 13:
                    g.DrawLine(pen, (float)a[10, 0], (float)a[10, 1], (float)a[6, 0], (float)a[6, 1]);
                    g.DrawLine(pen, (float)b[10, 0], (float)b[10, 1], (float)b[6, 0], (float)b[6, 1]);
                    g.DrawLine(pen, (float)a[10, 0], (float)a[10, 1], (float)b[10, 0], (float)b[10, 1]);
                    g.DrawLine(pen, (float)a[6, 0], (float)a[6, 1], (float)b[6, 0], (float)b[6, 1]);

                    ex.DrawLine(pen, (float)a[10, 0], (float)a[10, 1], (float)a[6, 0], (float)a[6, 1]);
                    ex.DrawLine(pen, (float)b[10, 0], (float)b[10, 1], (float)b[6, 0], (float)b[6, 1]);
                    ex.DrawLine(pen, (float)a[10, 0], (float)a[10, 1], (float)b[10, 0], (float)b[10, 1]);
                    ex.DrawLine(pen, (float)a[6, 0], (float)a[6, 1], (float)b[6, 0], (float)b[6, 1]);
                    break;
                case 14:
                    g.DrawLine(pen, (float)a[16, 0], (float)a[16, 1], (float)a[13, 0], (float)a[13, 1]);
                    g.DrawLine(pen, (float)b[16, 0], (float)b[16, 1], (float)b[13, 0], (float)b[13, 1]);
                    g.DrawLine(pen, (float)a[16, 0], (float)a[16, 1], (float)b[16, 0], (float)b[16, 1]);
                    g.DrawLine(pen, (float)a[13, 0], (float)a[13, 1], (float)b[13, 0], (float)b[13, 1]);

                    ex.DrawLine(pen, (float)a[16, 0], (float)a[16, 1], (float)a[13, 0], (float)a[13, 1]);
                    ex.DrawLine(pen, (float)b[16, 0], (float)b[16, 1], (float)b[13, 0], (float)b[13, 1]);
                    ex.DrawLine(pen, (float)a[16, 0], (float)a[16, 1], (float)b[16, 0], (float)b[16, 1]);
                    ex.DrawLine(pen, (float)a[13, 0], (float)a[13, 1], (float)b[13, 0], (float)b[13, 1]);
                    break;
                case 15:
                    g.DrawLine(pen, (float)a[14, 0], (float)a[14, 1], (float)a[8, 0], (float)a[8, 1]);
                    g.DrawLine(pen, (float)b[14, 0], (float)b[14, 1], (float)b[8, 0], (float)b[8, 1]);
                    g.DrawLine(pen, (float)a[14, 0], (float)a[14, 1], (float)b[14, 0], (float)b[14, 1]);
                    g.DrawLine(pen, (float)a[8, 0], (float)a[8, 1], (float)b[8, 0], (float)b[8, 1]);

                    ex.DrawLine(pen, (float)a[14, 0], (float)a[14, 1], (float)a[8, 0], (float)a[8, 1]);
                    ex.DrawLine(pen, (float)b[14, 0], (float)b[14, 1], (float)b[8, 0], (float)b[8, 1]);
                    ex.DrawLine(pen, (float)a[14, 0], (float)a[14, 1], (float)b[14, 0], (float)b[14, 1]);
                    ex.DrawLine(pen, (float)a[8, 0], (float)a[8, 1], (float)b[8, 0], (float)b[8, 1]);
                    break;
                case 16:
                    g.DrawLine(pen, (float)a[11, 0], (float)a[11, 1], (float)a[7, 0], (float)a[7, 1]);
                    g.DrawLine(pen, (float)b[11, 0], (float)b[11, 1], (float)b[7, 0], (float)b[7, 1]);
                    g.DrawLine(pen, (float)a[11, 0], (float)a[11, 1], (float)b[11, 0], (float)b[11, 1]);
                    g.DrawLine(pen, (float)a[7, 0], (float)a[7, 1], (float)b[7, 0], (float)b[7, 1]);

                    ex.DrawLine(pen, (float)a[11, 0], (float)a[11, 1], (float)a[7, 0], (float)a[7, 1]);
                    ex.DrawLine(pen, (float)b[11, 0], (float)b[11, 1], (float)b[7, 0], (float)b[7, 1]);
                    ex.DrawLine(pen, (float)a[11, 0], (float)a[11, 1], (float)b[11, 0], (float)b[11, 1]);
                    ex.DrawLine(pen, (float)a[7, 0], (float)a[7, 1], (float)b[7, 0], (float)b[7, 1]);
                    break;
                default:
                    break;

            }

        }
    }
}

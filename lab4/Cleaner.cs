using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    // Удаление невидимых линий с помощью алгоритма Робертса
    internal class Cleaner
    {
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
            var A1 = -FindA(Ship.coordinateMatrixF[0, 1], Ship.coordinateMatrixF[1, 1], Ship.coordinateMatrixF[13, 1],
                Ship.coordinateMatrixF[0, 2], Ship.coordinateMatrixF[1, 2], Ship.coordinateMatrixF[13, 2]);
            var B1 = FindB(Ship.coordinateMatrixF[0, 0], Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixF[13, 0],
                Ship.coordinateMatrixF[0, 2], Ship.coordinateMatrixF[1, 2], Ship.coordinateMatrixF[13, 2]);
            var C1 = -FindC(Ship.coordinateMatrixF[0, 0], Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixF[13, 0],
                Ship.coordinateMatrixF[0, 1], Ship.coordinateMatrixF[1, 1], Ship.coordinateMatrixF[13, 1]);
            var D1 = FindD(Ship.coordinateMatrixF[0, 0], Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixF[13, 0],
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
            var A5 = -FindA(Ship.coordinateMatrixF[1, 1], Ship.coordinateMatrixF[4, 1], Ship.coordinateMatrixB[4, 1],
                Ship.coordinateMatrixF[1, 2], Ship.coordinateMatrixF[4, 2], Ship.coordinateMatrixB[4, 2]);
            var B5 = FindB(Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixF[4, 0], Ship.coordinateMatrixB[4, 0],
                Ship.coordinateMatrixF[1, 2], Ship.coordinateMatrixF[4, 2], Ship.coordinateMatrixB[4, 2]);
            var C5 = -FindC(Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixF[4, 0], Ship.coordinateMatrixB[4, 0],
                Ship.coordinateMatrixF[1, 1], Ship.coordinateMatrixF[4, 1], Ship.coordinateMatrixB[4, 1]);
            var D5 = FindD(Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixF[4, 0], Ship.coordinateMatrixB[4, 0],
                Ship.coordinateMatrixF[1, 1], Ship.coordinateMatrixF[4, 1], Ship.coordinateMatrixB[4, 1],
                Ship.coordinateMatrixF[1, 2], Ship.coordinateMatrixF[4, 2], Ship.coordinateMatrixB[4, 2]);

            // Дно судна
            var A6 = -FindA(Ship.coordinateMatrixF[2, 1], Ship.coordinateMatrixF[1, 1], Ship.coordinateMatrixB[1, 1],
                Ship.coordinateMatrixF[2, 2], Ship.coordinateMatrixF[1, 2], Ship.coordinateMatrixB[1, 2]);
            var B6 = FindB(Ship.coordinateMatrixF[2, 0], Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixB[1, 0],
                Ship.coordinateMatrixF[2, 2], Ship.coordinateMatrixF[1, 2], Ship.coordinateMatrixB[1, 2]);
            var C6 = -FindC(Ship.coordinateMatrixF[2, 0], Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixB[1, 0],
                Ship.coordinateMatrixF[2, 1], Ship.coordinateMatrixF[1, 1], Ship.coordinateMatrixB[1, 1]);
            var D6 = FindD(Ship.coordinateMatrixF[2, 0], Ship.coordinateMatrixF[1, 0], Ship.coordinateMatrixB[1, 0],
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
            var A8 = -FindA(Ship.coordinateMatrixF[13, 1], Ship.coordinateMatrixF[15, 1], Ship.coordinateMatrixB[15, 1],
                 Ship.coordinateMatrixF[13, 2], Ship.coordinateMatrixF[15, 2], Ship.coordinateMatrixB[15, 2]);
            var B8 = FindB(Ship.coordinateMatrixF[13, 0], Ship.coordinateMatrixF[15, 0], Ship.coordinateMatrixB[15, 0],
                Ship.coordinateMatrixF[13, 2], Ship.coordinateMatrixF[15, 2], Ship.coordinateMatrixB[15, 2]);
            var C8 = -FindC(Ship.coordinateMatrixF[13, 0], Ship.coordinateMatrixF[15, 0], Ship.coordinateMatrixB[15, 0],
                Ship.coordinateMatrixF[13, 1], Ship.coordinateMatrixF[15, 1], Ship.coordinateMatrixB[15, 1]);
            var D8 = FindD(Ship.coordinateMatrixF[13, 0], Ship.coordinateMatrixF[15, 0], Ship.coordinateMatrixB[15, 0],
                Ship.coordinateMatrixF[13, 1], Ship.coordinateMatrixF[15, 1], Ship.coordinateMatrixB[15, 1],
                Ship.coordinateMatrixF[13, 2], Ship.coordinateMatrixF[15, 2], Ship.coordinateMatrixB[15, 2]);

            // Низ правого паруса
            var A9 = -FindA(Ship.coordinateMatrixF[11, 1], Ship.coordinateMatrixF[12, 1], Ship.coordinateMatrixB[12, 1],
                Ship.coordinateMatrixF[11, 2], Ship.coordinateMatrixF[12, 2], Ship.coordinateMatrixB[12, 2]);
            var B9 = FindB(Ship.coordinateMatrixF[11, 0], Ship.coordinateMatrixF[12, 0], Ship.coordinateMatrixB[12, 0],
                Ship.coordinateMatrixF[11, 2], Ship.coordinateMatrixF[12, 2], Ship.coordinateMatrixB[12, 2]);
            var C9 = -FindC(Ship.coordinateMatrixF[11, 0], Ship.coordinateMatrixF[12, 0], Ship.coordinateMatrixB[12, 0],
                Ship.coordinateMatrixF[11, 1], Ship.coordinateMatrixF[12, 1], Ship.coordinateMatrixB[12, 1]);
            var D9 = FindD(Ship.coordinateMatrixF[11, 0], Ship.coordinateMatrixF[12, 0], Ship.coordinateMatrixB[12, 0],
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
            var A11 = -FindA(Ship.coordinateMatrixF[9, 1], Ship.coordinateMatrixF[17, 1], Ship.coordinateMatrixB[17, 1],
                Ship.coordinateMatrixF[9, 2], Ship.coordinateMatrixF[17, 2], Ship.coordinateMatrixB[17, 2]);
            var B11 = FindB(Ship.coordinateMatrixF[9, 0], Ship.coordinateMatrixF[17, 0], Ship.coordinateMatrixB[17, 0],
                Ship.coordinateMatrixF[9, 2], Ship.coordinateMatrixF[17, 2], Ship.coordinateMatrixB[17, 2]);
            var C11 = -FindC(Ship.coordinateMatrixF[9, 0], Ship.coordinateMatrixF[17, 0], Ship.coordinateMatrixB[17, 0],
                Ship.coordinateMatrixF[9, 1], Ship.coordinateMatrixF[17, 1], Ship.coordinateMatrixB[17, 1]);
            var D11 = FindD(Ship.coordinateMatrixF[9, 0], Ship.coordinateMatrixF[17, 0], Ship.coordinateMatrixB[17, 0],
                Ship.coordinateMatrixF[9, 1], Ship.coordinateMatrixF[17, 1], Ship.coordinateMatrixB[17, 1],
                Ship.coordinateMatrixF[9, 2], Ship.coordinateMatrixF[17, 2], Ship.coordinateMatrixB[17, 2]);

            // Низ флага
            var A12 = -FindA(Ship.coordinateMatrixF[16, 1], Ship.coordinateMatrixF[17, 1], Ship.coordinateMatrixB[17, 1],
                Ship.coordinateMatrixF[16, 2], Ship.coordinateMatrixF[17, 2], Ship.coordinateMatrixB[17, 2]);
            var B12 = FindB(Ship.coordinateMatrixF[16, 0], Ship.coordinateMatrixF[17, 0], Ship.coordinateMatrixB[17, 0],
                Ship.coordinateMatrixF[16, 2], Ship.coordinateMatrixF[17, 2], Ship.coordinateMatrixB[17, 2]);
            var C12 = -FindC(Ship.coordinateMatrixF[16, 0], Ship.coordinateMatrixF[17, 0], Ship.coordinateMatrixB[17, 0],
                Ship.coordinateMatrixF[16, 1], Ship.coordinateMatrixF[17, 1], Ship.coordinateMatrixB[17, 1]);
            var D12 = FindD(Ship.coordinateMatrixF[16, 0], Ship.coordinateMatrixF[17, 0], Ship.coordinateMatrixB[17, 0],
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
            var A14 = -FindA(Ship.coordinateMatrixF[10, 1], Ship.coordinateMatrixF[6, 1], Ship.coordinateMatrixB[6, 1],
                Ship.coordinateMatrixF[10, 2], Ship.coordinateMatrixF[6, 2], Ship.coordinateMatrixB[6, 2]);
            var B14 = FindB(Ship.coordinateMatrixF[10, 0], Ship.coordinateMatrixF[6, 0], Ship.coordinateMatrixB[6, 0],
                Ship.coordinateMatrixF[10, 2], Ship.coordinateMatrixF[6, 2], Ship.coordinateMatrixB[6, 2]);
            var C14 = -FindC(Ship.coordinateMatrixF[10, 0], Ship.coordinateMatrixF[6, 0], Ship.coordinateMatrixB[6, 0],
                Ship.coordinateMatrixF[10, 1], Ship.coordinateMatrixF[6, 1], Ship.coordinateMatrixB[6, 1]);
            var D14 = FindD(Ship.coordinateMatrixF[10, 0], Ship.coordinateMatrixF[6, 0], Ship.coordinateMatrixB[6, 0],
                Ship.coordinateMatrixF[10, 1], Ship.coordinateMatrixF[6, 1], Ship.coordinateMatrixB[6, 1],
                Ship.coordinateMatrixF[10, 2], Ship.coordinateMatrixF[6, 2], Ship.coordinateMatrixB[6, 2]);

            // Верхняя левая часть мачты
            var A15 = -FindA(Ship.coordinateMatrixF[13, 1], Ship.coordinateMatrixF[15, 1], Ship.coordinateMatrixB[15, 1],
                Ship.coordinateMatrixF[13, 2], Ship.coordinateMatrixF[15, 2], Ship.coordinateMatrixB[15, 2]);
            var B15 = FindB(Ship.coordinateMatrixF[13, 0], Ship.coordinateMatrixF[15, 0], Ship.coordinateMatrixB[15, 0],
                Ship.coordinateMatrixF[13, 2], Ship.coordinateMatrixF[15, 2], Ship.coordinateMatrixB[15, 2]);
            var C15 = -FindC(Ship.coordinateMatrixF[13, 0], Ship.coordinateMatrixF[15, 0], Ship.coordinateMatrixB[15, 0],
                Ship.coordinateMatrixF[13, 1], Ship.coordinateMatrixF[15, 1], Ship.coordinateMatrixB[15, 1]);
            var D15 = FindD(Ship.coordinateMatrixF[13, 0], Ship.coordinateMatrixF[17, 0], Ship.coordinateMatrixB[17, 0],
                Ship.coordinateMatrixF[13, 1], Ship.coordinateMatrixF[15, 1], Ship.coordinateMatrixB[15, 1],
                Ship.coordinateMatrixF[13, 2], Ship.coordinateMatrixF[15, 2], Ship.coordinateMatrixB[15, 2]);

            // Нижняя левая часть мачты
            var A16 = -FindA(Ship.coordinateMatrixF[14, 1], Ship.coordinateMatrixF[8, 1], Ship.coordinateMatrixB[8, 1],
                Ship.coordinateMatrixF[14, 2], Ship.coordinateMatrixF[8, 2], Ship.coordinateMatrixB[8, 2]);
            var B16 = FindB(Ship.coordinateMatrixF[14, 0], Ship.coordinateMatrixF[8, 0], Ship.coordinateMatrixB[8, 0],
                Ship.coordinateMatrixF[14, 2], Ship.coordinateMatrixF[8, 2], Ship.coordinateMatrixB[8, 2]);
            var C16 = -FindC(Ship.coordinateMatrixF[14, 0], Ship.coordinateMatrixF[8, 0], Ship.coordinateMatrixB[8, 0],
                Ship.coordinateMatrixF[14, 1], Ship.coordinateMatrixF[8, 1], Ship.coordinateMatrixB[8, 1]);
            var D16 = FindD(Ship.coordinateMatrixF[14, 0], Ship.coordinateMatrixF[8, 0], Ship.coordinateMatrixB[8, 0],
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

            double[] x = new double[] { 0, 0, 0, 1 };
            double[] m = new double[] { A5, B5, C5, D5 };
            double res = 0;
            // Перемножение матриц
            for (int i = 0; i < 4; i++)
            {
                res += x[i] * m[i];
            }
        }
    }
}

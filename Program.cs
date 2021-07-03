using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace StrashnaOchen
{
    class Program
    {
        static void raspredelenie(int[,] A, int pos, int pot, int[] posVec, int[] potVec)
        {
            string path = "Ответ.txt";
            int maxA = A[0, 0];
            for (int i = 0; i < pos; i++) //нахождение максимума А
            {
                for (int j = 0; j < pot; j++)
                {
                    if (maxA < A[i, j])
                    {
                        maxA = A[i, j];
                    }
                }
            }

            int[,] B = new int[pos, pot];
            for (int i = 0; i < pos; i++) //заполнение матрицы В
            {
                for (int j = 0; j < pot; j++)
                {
                    B[i, j] = (maxA + 1) - A[i, j];
                }
            }

            int[] M1 = new int[pos];
            int[] N1 = new int[pot];
            for (int i = 0; i < pos; i++)
            {
                M1[i] = posVec[i];
            }
            for (int i = 0; i < pos; i++)
            {
                N1[i] = potVec[i];
            }
            for (int i = 0; i < pos; i++)
            {
                for (int j = 0; j < pot; j++)
                {
                    A[i, j] = 0;
                }
            }

            for (int i = 0; i < pos; i++)
            {
                for (int j = 0; j < pot; j++)
                {
                    if (M1[i] == N1[j] && M1[i] != 0 && N1[j] != 0)
                    {
                        A[i, j] = M1[i];
                        M1[i] = 0;
                        M1[j] = 0;
                    }
                    else if (M1[i] < N1[j] && M1[i] != 0 && N1[j] != 0)
                    {
                        A[i, j] = M1[i];
                        N1[j] -= M1[i];
                        M1[i] = 0;
                    }
                    else if (M1[i] > N1[j] && M1[i] != 0 && N1[j] != 0)
                    {
                        A[i, j] = M1[i];
                        M1[i] -= N1[j];
                        N1[j] = 0;
                    }
                }
            }

            int raz = 1;

            for (int i = 0; i < potVec.Length; i++)
            {
                if ((int)Math.Log10(potVec[i]) + 1 > raz)
                    raz = (int)Math.Log10(potVec[i]) + 1;
            }
            for (int i = 0; i < posVec.Length; i++)
            {
                if ((int)Math.Log10(posVec[i]) + 1 > raz)
                    raz = (int)Math.Log10(posVec[i]) + 1;
            }

            for (int i = 0; i < pos; i++)
            {
                for (int j = 0; j < pot; j++)
                {
                    if ((int)Math.Log10(B[i, j]) + 1 > raz)
                        raz = (int)Math.Log10(B[i, j]) + 1;
                }
            }

            for (int i = 0; i < raz; i++) //Вывод распределения
            {
                Console.Write(" ");
            }

            for (int i = 0; i < potVec.Length; i++)
            {
                string str = Convert.ToString(potVec[i]);
                int raz1 = str.Length;
                if (raz + raz + 1 == 3) raz1 = 2;
                else raz1 = (raz + raz + 1) - raz1;
                for (int j = 0; j < raz1; j++)
                {
                    str += " ";
                }
            }

            Console.WriteLine();
            for (int i = 0; i < pos; i++)
            {
                string str = Convert.ToString(posVec[i]);
                int raz1 = str.Length;
                if (raz == 1) raz1 = 0;
                else raz1 = raz - raz1;
                for (int j = 0; j < pos; j++)
                {
                    str += " ";
                }
                Console.Write(str + "|");
                for (int j = 0; j < pot; j++)
                {
                    string str1 = Convert.ToString(B[i, j]);
                    if (A[i,j] != 0)
                    {
                        str1 = str1 + "/" + Convert.ToString(A[i, j]);
                        int raz2 = str1.Length;
                        if (raz + raz + 1 == 3) raz2 = 0;
                        else raz2 = (raz + raz + 1) - raz2;
                        for (int z = 0; z < raz2; z++)
                        {
                            str1 += " ";
                        }
                    }
                    else
                    {
                        int raz2 = str1.Length;
                        raz2 = (raz + raz + 1) - raz2;
                        for (int z = 0; z < raz2; z++)
                        {
                            str1 += " ";
                        }
                    }
                    Console.Write(str1 + "|");
                    File.AppendAllText(path, str1 + " "); //Запись данных в файл
                }
                File.AppendAllText(path, "\n");
                Console.WriteLine();
            }
        }



        static void Main(string[] args)
        {
            int pot, pos;
         m3:Console.Write("Поставщики: ");
            pos = Int32.Parse(Console.ReadLine()); //ввод поставщиков
            Console.Write("Потребители: ");
            pot = Int32.Parse(Console.ReadLine()); //ввод потребителей

            int[] posVec = new int[pos]; // вектор поставщиков
            int[] potVec = new int[pot]; // вектор потребителей
            Console.Write("Вектор поставщиков: "); 
         m1:string data = Console.ReadLine();
            for (int i = 0; i < posVec.Length; i++) //ввод вектора поставщиков и проверка на отрицательные элементы (в одну строку, каждая цифра через пробел)
            {
                posVec[i] = Int32.Parse(data.Split(' ')[i]);
                if (posVec[i] < 0) { Console.WriteLine("Есть отрицательыне значения, повторите ввод"); goto m1; }
            }

            Console.Write("Вектор потребителей: "); //ввод вектора потребителей и проверка на отрицательные элементы (в одну строку, каждая цифра через пробел)
        m2:data = Console.ReadLine();
            for (int i = 0; i < potVec.Length; i++)
            {
                potVec[i] = Int32.Parse(data.Split(' ')[i]);
                if (potVec[i] < 0) { Console.WriteLine("Есть отрицательные значения, повторите ввод"); goto m2; }
            }
            Console.Clear();

            Console.Write("Вектор поставщиков: "); //вывод вектора поставщиков
            foreach (int x in posVec)
            {
                Console.Write(x);
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.Write("Вектор потребителей: "); //вывод вектора потребителей
            foreach (int x in potVec)
            {
                Console.Write(x);
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.WriteLine();

            int sumPos = 0, sumPot = 0; 
            for (int i = 0; i < posVec.Length; i++) // расчет суммы вектора поставщиков
            {
                sumPos += posVec[i];
            }
            for (int i = 0; i < potVec.Length; i++) // расчет суммы вектора потребителей
            {
                sumPot += potVec[i];
            }
            if (sumPos != sumPot) //проверка на равность сумм
            {
                Console.WriteLine("Сумма не совпадает, повторите ввод векторов");
                goto m3;
            }

            Console.WriteLine("Введите стоимость перевозок: ");
            int[,] A = new int[pos, pot]; //матрица стоимости перевозок
            string stroka;
            for (int i = 0; i < A.GetLength(0); i++) //ввод матрицы (по строчке, каждая цифра через пробел)
            {
                stroka = Console.ReadLine();
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    A[i,j] = Int32.Parse(stroka.Split(' ')[i]);
                    if (A[i, j] < 0) { Console.WriteLine("Есть отрицательыне значения, повторите ввод"); i--; } //проверка на отрицательные элементы
                }
            }
            Console.Clear();

            Console.WriteLine("Стоимость перевозок: "); //вывод матрицы стоимости перевозок
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    Console.Write(A[i, j] + " ");
                }
                Console.WriteLine();
            }

            raspredelenie(A, pos, pot, posVec, potVec); //объявление метода распределения по северо-западу
            Console.ReadKey();


        }
    }
}

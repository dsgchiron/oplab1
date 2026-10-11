using System;
using System.IO;

namespace Lab1Variant26
{
    class Program
    {
        // Введення розміру масиву з перевіркою
        static int ReadN()
        {
            int n;
            Console.Write("Введіть розмір масиву N (N > 0): ");
            while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
                Console.Write("Некоректне значення. Повторіть введення N: ");
            return n;
        }

        // Заповнення з клавіатури
        static int[] FromKeyboard(int n)
        {
            int[] a = new int[n];
            for (int j = 0; j < n; j++)
            {
                Console.Write("a[{0}] = ", j);
                while (!int.TryParse(Console.ReadLine(), out a[j]))
                    Console.Write("Некоректне значення. Повторіть a[{0}] = ", j);
            }
            return a;
        }

        // Заповнення з текстового файлу (числа розділені пробілами / переносами рядків)
        static int[] FromFile(int n)
        {
            Console.Write("Введіть ім'я файлу: ");
            string path = Console.ReadLine();
            if (!File.Exists(path))
            {
                Console.WriteLine("Файл не знайдено!");
                return null;
            }
            string[] parts = File.ReadAllText(path)
                .Split(new char[] { ' ', '\t', '\r', '\n', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < n)
            {
                Console.WriteLine("У файлі лише {0} чисел, а потрібно {1}!", parts.Length, n);
                return null;
            }
            int[] a = new int[n];
            for (int j = 0; j < n; j++)
            {
                if (!int.TryParse(parts[j], out a[j]))
                {
                    Console.WriteLine("Елемент \"{0}\" у файлі не є цілим числом!", parts[j]);
                    return null;
                }
            }
            return a;
        }

        // Заповнення генератором випадкових чисел (від -50 до 50)
        static int[] FromRandom(int n)
        {
            Random rnd = new Random();
            int[] a = new int[n];
            for (int j = 0; j < n; j++)
                a[j] = rnd.Next(-50, 51);
            return a;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Варіант 26: сума елементів вектора з парними індексами\n");

            string again;
            do
            {
                int n = ReadN();
                int[] a = null;

                while (a == null)
                {
                    Console.WriteLine("\nСпосіб заповнення масиву:");
                    Console.WriteLine(" 1 - з клавіатури");
                    Console.WriteLine(" 2 - з текстового файлу");
                    Console.WriteLine(" 3 - генератором випадкових чисел");
                    Console.Write("Ваш вибір: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1": a = FromKeyboard(n); break;
                        case "2": a = FromFile(n); break;
                        case "3": a = FromRandom(n); break;
                        default: Console.WriteLine("Невірний вибір!"); break;
                    }
                }

                // Виведення масиву
                Console.WriteLine("\nВихідний масив:");
                for (int j = 0; j < n; j++)
                    Console.WriteLine("a[{0}] = {1}", j, a[j]);

                // Обчислення суми елементів з парними індексами (0, 2, 4, ...)
                long sum = 0;
                string expr = "";
                for (int j = 0; j < n; j += 2)
                {
                    sum += a[j];
                    expr += (expr == "" ? "" : " + ") + (a[j] < 0 ? "(" + a[j] + ")" : a[j].ToString());
                }

                Console.WriteLine("\nЕлементи з парними індексами: {0}", expr);
                Console.WriteLine("Сума елементів з парними індексами = {0}", sum);

                Console.Write("\nПовторити? (т - так, інша клавіша - вихід): ");
                again = Console.ReadLine();
                Console.WriteLine();
            } while (again == "т" || again == "Т" || again == "y" || again == "Y");
        }
    }
}
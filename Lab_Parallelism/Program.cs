using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    // Метод обчислення суми хвиль та відстеження відпрацьованих елементів
    static (int, HashSet<int>) SearchSum(int[] Array)
    {
        HashSet<int> ProcesElement = new HashSet<int>(); // Створення нового масиву для відстеження відпрацьованих елементів

        while (Array.Length > 1) // Поки актуальна частина більше 1
        {
            List<int> Result = new List<int>(); // Створення нового списку для зберігання результатів хвиль

            Parallel.For(0, Array.Length / 2, i => // Паралельний цикл для обчислення суми для пар елементів
            {
                int SumNumber = Array[i] + Array[Array.Length - i - 1]; // Обчислення суми для пар елементів
                Result.Add(SumNumber); // Додавання суми до списку результатів

                lock (ProcesElement) //Потрібен для відстеження відпрацьованих елементів використовується блокування(lock),
                                     //щоб уникнути проблем конкурентного доступу до множини ProcesElement.
                {
                    ProcesElement.Add(Array[Array.Length - i - 1]); // Додавання другого елементу пари до відстежуваної множини
                }
            });

            //if (Array.Length == 2) ;
            //{
            //    ProcesElement.Add(Array[1]);
            //}

            if (Array.Length % 2 != 0) // Якщо кількість елементів непарна
            {
                //Console.WriteLine("Hi");
                Result.Add(Array[Array.Length / 2]); // Додаємо центральний елемент до результату хвилі 
            }

            Array = Result.ToArray(); // Оновлення початкового масиву для наступної хвилі
            Console.WriteLine(string.Join(" ", Array)); //string.Join для об'єднання елементів у рядок
            //Console.WriteLine(string.Join(" ", ProcesElement));
        }

        return (Array[0], ProcesElement); // Повернення результату та відстежуваних елементів
    }

    static void Main()
    {
        int[] Array = { 1, 2, 3, 4, 5, 6 }; // Оголошення початкового масиву

        var (Result, Elements) = SearchSum(Array); // Обчислення суми хвиль та відстеження відпрацьованих елементів

        Console.WriteLine($"Результат: {Result}"); // Виведення значення результату
        Console.Write("Вiдпрацьованi елементи: ");

        foreach (var Element in Elements) // Цикл для виведення відпрацьованих елементів
        {
            Console.Write($"{Element} "); // Виведення кожного відпрацьованого елементу
        }
        Console.WriteLine();
    }
}
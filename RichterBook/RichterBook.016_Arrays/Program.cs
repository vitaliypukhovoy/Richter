﻿using System;

namespace RichterBook._016_Arrays
{
    class Program
    {
        private static void Main()
        {
            Console.ReadLine();
        }

        private static void MultidimensionalArray()
        {
            int[,] a = new int[10, 2];

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 2; j++)
                    a[i, j] = 1;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 2; j++)
                    Console.Write("{0}\t", a[i, j]);
                Console.WriteLine();
            }
        }

        private static void InitElementArray()
        {
            // Чудеса компилятора
            string[] s = new string[] { "a" };
            string[] s1 = new[] { "a" };
            var s2 = new[] { "a" };
            string[] s3 = { "a" };
            var anonymouseArr = new[] { new { Name = "Bob" }, new { Name = "Alice" } };
        }

        private static void CheckBoundSzArray()
        {
            // Вызов свойства Length (a.Length) в проверочном выражении цикла (i < a.Length) происходит
            // только 1-н раз, а не на каждой итерации цикла. JIT знает, что Length принадлежит System.Array
            // и делает трюк - вызывает его только 1 раз перед выполнением цыкла, для того чтобы присвоить
            // значение промежуточной переменной. Именно значение этой переменной и проверяется на каждой
            // итерации цикла. По этому самим тут оптимизировать ничего не нужно, это только снизит
            // производительность и ухудшит читаемость кода. Пусть Length вызывается автоматически.

            // Также, в случае с SZ-массивами для цикла for JIT знает, что все индексы, по которым будет
            // осуществляться доступ к элементам массива находятся в диапазоне от 0 до (Length-1).
            // Учитывая это, во время выполнения JIT производит код, проверяющий, что диапазон индексов
            // доступа лежит внутри диапазона массива. Эта проверка выглядит следующим образом:

            //          if(0 >= a.GetLowerBound(0)) && ((Length – 1) <= a.GetUpperBound(0))

            // Эта проверка произодится только 1-н раз, перед выполнением цикла, если выражение внутри if
            // вернет true, то JIT не будет генерировать внутри цикла код, который проверяет выход индекса
            // за пределы диапазона массива. Такая проверка позволяет выполнять доступ к элементам массива
            // очень быстро, ведь нам не надо на каждой итерации проверять попадание индекса в диапазон
            // массива.
            // Для многомерных массивов и массивов с ненулевой нижней границей эта проверка не выполняется
            // и по этому для таких массивов проверка попадания индекса в диапазон массива входит внутрь
            // цикла (выполняется на каждой итерации цикла). Кроме того, компилятор добавляет код, вычитающий
            // из текущего индекса нижнюю границу массива.
            // Если ввам нужны многомерные массивы и остро стоит вопрос производительности используйте
            // нерегулярные массивы.

            Int32[] a = new Int32[5];
            for (Int32 i = 0; i < a.Length; i++)
            {
                Console.WriteLine(a[i]);
            }
        }
    }
}

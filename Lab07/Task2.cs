using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Lab7
{
    class Task2
    {
        private string pathToDir = @"E:\Lab07";
        private string pathToFile = @"E:\Lab07\sorted.dat";
        private string pathToResults = @"E:\Lab07\runtimeResult.dat";

        private int[] array = new int[100000];
        private int[] copyArray = new int[100000];

        public struct comparison
        {
            public string runtime;
            public long counterComparison;
            public long counterSwap;
        }
        public comparison[] structureOfResults = new comparison[5];

        public void ChooseMethodOfSorting()
        {
            if (!Directory.Exists(pathToDir))
                Directory.CreateDirectory(pathToDir);

            Console.WriteLine("Считать массив из файла? (+ / -)");
            string randomChoice = Console.ReadLine();
            if (randomChoice == "+")
                ReadFromFile(array);
            else
                RandomFilling(array, copyArray, pathToFile);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Метод выбором\n" +
                    "2. Метод вставками\n" +
                    "3. Метод пузырьком\n" +
                    "4. Метод шейкера\n" +
                    "5. Метод Шелла\n" +
                    "6. Проверить массив\n" +
                    "7. Сгенерировать массив\n" +
                    "8. Сравнить время сортировки\n" +
                    "9. Сделать массив неотсортированным\n" +
                    "\nВыберите метод:");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        SortSelection(array);
                        Console.WriteLine("Завершено");
                        Thread.Sleep(3000);
                        break;
                    case "2":
                        Console.Clear();
                        SortInsertion(array);
                        Console.WriteLine("Завершено");
                        Thread.Sleep(3000);
                        break;
                    case "3":
                        Console.Clear();
                        BubbleSort(array);
                        Console.WriteLine("Завершено");
                        Thread.Sleep(3000);
                        break;
                    case "4":
                        Console.Clear();
                        ShakerSort(array);
                        Console.WriteLine("Завершено");
                        Thread.Sleep(3000);
                        break;
                    case "5":
                        Console.Clear();
                        ShellSort(array);
                        Console.WriteLine("Завершено");
                        Thread.Sleep(3000);
                        break;
                    case "6":
                        Console.Clear();
                        CheckSort(array);
                        Console.ReadKey();
                        break;
                    case "7":
                        Console.Clear();
                        RandomFilling(array, copyArray, pathToFile);
                        Console.WriteLine("Завершено");
                        Console.ReadKey();
                        break;
                    case "8":
                        Console.Clear();
                        ShowRuntimeResult(structureOfResults);
                        Console.ReadKey();
                        break;
                    case "9":
                        Console.Clear();
                        ReturnArray(array, copyArray);
                        Console.WriteLine("Завершено");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void SortSelection(int[] array)
        {
            Stopwatch stopWatchSelection = new Stopwatch();
            StreamWriter writerToFile = new StreamWriter(File.Open(pathToFile, FileMode.OpenOrCreate));
            int lengthOfArray = array.Length;
            int max = 0, tmp = 0;
            long counterSwap = 0, counterComparison = 0;

            stopWatchSelection.Start();
            #region Сортировка
            for (int i = 0; i < lengthOfArray - 1; i++)
            {
                max = i;
                for (int j = i + 1; j < lengthOfArray; j++)
                {
                    if (array[j] > array[max])
                    {
                        max = j;
                    }
                    counterComparison++;
                }
                if (max != i)
                    Swap(array, ref counterSwap, tmp, i, max);
                counterComparison++;
            }
            #endregion 
            stopWatchSelection.Stop();
            #region Запись в файл
            for (int i = 0; i < lengthOfArray; i++)
            {
                writerToFile.WriteLine(array[i]);
            }
            #endregion

            writerToFile.Close();

            TimeSpan runtime = stopWatchSelection.Elapsed;
            string strRuntime = String.Format("{0:00}:{1:00}", runtime.Seconds, runtime.Milliseconds / 10);
            structureOfResults[0].runtime = strRuntime;
            structureOfResults[0].counterComparison = counterComparison;
            structureOfResults[0].counterSwap = counterSwap;
        }
        private void SortInsertion(int[] array)
        {
            Stopwatch stopWatchInsertion = new Stopwatch();
            StreamWriter writerToFile = new StreamWriter(File.Open(pathToFile, FileMode.OpenOrCreate));
            int lengthOfArray = array.Length;
            int tmp = 0;
            long counterSwap = 0, counterComparison = 0;
            stopWatchInsertion.Start();
            #region Сортировка
            for (int i = 1; i < lengthOfArray; i++)
            {
                for (int j = i; j > 0 && array[j - 1] < array[j]; j--, counterComparison++)
                    Swap(array, ref counterSwap, tmp, j, j - 1);
            }
            #endregion
            stopWatchInsertion.Stop();
            #region Запись в файл
            for (int i = 0; i < lengthOfArray; i++)
            {
                writerToFile.WriteLine(array[i]);
            }
            #endregion

            writerToFile.Close();

            TimeSpan runtime = stopWatchInsertion.Elapsed;
            string strRuntime = String.Format("{0:00}:{1:00}", runtime.Seconds, runtime.Milliseconds / 10);
            structureOfResults[1].runtime = strRuntime;
            structureOfResults[1].counterComparison = counterComparison;
            structureOfResults[1].counterSwap = counterSwap;
        }
        private void BubbleSort(int[] array)
        {
            Stopwatch stopWatchBubble = new Stopwatch();
            StreamWriter writerToFile = new StreamWriter(File.Open(pathToFile, FileMode.OpenOrCreate));
            int lengthOfArray = array.Length;
            long counterSwap = 0, counterComparison = 0;
            int tmp = 0;
            stopWatchBubble.Start();
            #region Сортировка
            for (int i = 0; i < lengthOfArray - 1; i++)
            {
                for (int j = i + 1; j < lengthOfArray; j++)
                {
                    if (array[i] < array[j])
                        Swap(array, ref counterSwap, tmp, i, j);
                    counterComparison++;
                }
                if (counterSwap == 0)
                {
                    break;
                }
            }
            #endregion
            stopWatchBubble.Stop();
            #region Запись в файл
            for (int i = 0; i < lengthOfArray; i++)
            {
                writerToFile.WriteLine(array[i]);
            }
            #endregion

            writerToFile.Close();

            TimeSpan runtime = stopWatchBubble.Elapsed;
            string strRuntime = String.Format("{0:00}:{1:00}:{2:00}", runtime.Minutes, runtime.Seconds, runtime.Milliseconds / 10);
            structureOfResults[2].runtime = strRuntime;
            structureOfResults[2].counterComparison = counterComparison;
            structureOfResults[2].counterSwap = counterSwap;
        }
        private void ShakerSort(int[] array)
        {
            Stopwatch stopWatchShaker = new Stopwatch();
            StreamWriter writerToFile = new StreamWriter(File.Open(pathToFile, FileMode.OpenOrCreate));
            int lengthOfArray = array.Length;
            long counterSwap = 0, counterComparison = 0;
            int left = 0, right = lengthOfArray - 1;
            int tmp = 0;
            stopWatchShaker.Start();
            #region Сортировка
            while (left <= right)
            {
                for (int i = left; i < right - 1; i++)
                {
                    if (array[i] < array[i + 1])
                        Swap(array, ref counterSwap, tmp, i, i + 1);
                    counterComparison++;
                }
                left++;
                for (int i = right; i > left - 1; i--)
                {
                    if (array[i] > array[i - 1])
                        Swap(array, ref counterSwap, tmp, i, i - 1);
                    counterComparison++;
                }
                right--;
            }
            #endregion
            stopWatchShaker.Stop();
            #region Запись в файл
            for (int i = 0; i < lengthOfArray; i++)
            {
                writerToFile.WriteLine(array[i]);
            }
            #endregion

            writerToFile.Close();

            TimeSpan runtime = stopWatchShaker.Elapsed;
            string strRuntime = String.Format("{0:00}:{1:00}:{2:00}", runtime.Minutes, runtime.Seconds, runtime.Milliseconds / 10);
            structureOfResults[3].runtime = strRuntime;
            structureOfResults[3].counterComparison = counterComparison;
            structureOfResults[3].counterSwap = counterSwap;
        }
        private void ShellSort(int[] array)
        {
            Stopwatch stopWatchShell = new Stopwatch();
            StreamWriter writerToFile = new StreamWriter(File.Open(pathToFile, FileMode.OpenOrCreate));
            int lengthOfArray = array.Length;
            long counterSwap = 0, counterComparison = 0;
            int step = lengthOfArray / 2;
            int tmp = 0, j = 0;
            stopWatchShell.Start();
            #region Сортировка
            while (step > 0)
            {
                for (int i = 0; i < lengthOfArray - step; i++)
                {
                    j = i;
                    while (j >= 0 && array[j] < array[j + step])
                    {
                        Swap(array, ref counterSwap, tmp, j, j + step);
                        j -= step;
                        counterComparison++;
                    }
                }
                step = step / 2;
            }
            #endregion
            stopWatchShell.Stop();
            #region Запись в файл
            for (int i = 0; i < lengthOfArray; i++)
            {
                writerToFile.WriteLine(array[i]);
            }
            #endregion

            writerToFile.Close();

            TimeSpan runtime = stopWatchShell.Elapsed;
            string strRuntime = String.Format("{0:00}:{1:00}", runtime.Seconds, runtime.Milliseconds / 10);
            structureOfResults[4].runtime = strRuntime;
            structureOfResults[4].counterComparison = counterComparison;
            structureOfResults[4].counterSwap = counterSwap;
        }

        private void ReadFromFile(int[] array)
        {
            StreamReader readFromFile = new StreamReader(File.Open(pathToFile, FileMode.Open));
            int index = 0;
            int lengthOfArray = array.Length;
            for (string line; (line = readFromFile.ReadLine()) != null && index < lengthOfArray; index++)
            {
                array[index] = Convert.ToInt32(line);
            }
            readFromFile.Close();
        }
        private void CheckSort(int[] array)
        {
            int lengthOfArray = array.Length;
            bool isSorted = true;
            for (int i = 0; i < lengthOfArray - 1; i++)
            {
                if (array[i] < array[i + 1])
                {
                    isSorted = false;
                    break;
                }
            }
            if (isSorted)
            {
                Console.WriteLine("Массив отсортирован");
            }
            else
            {
                Console.WriteLine("Массив не отсортирован");
            }
        }
        private void Swap(int[] array, ref long counterSwap, int tmp, int i, int j)
        {
            counterSwap++;
            tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }
        private void ReturnArray(int[] array, int[] copyArray)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = copyArray[i];
        }
        private void RandomFilling(int[] array, int[] copyArray, string path)
        {
            Random rand = new Random();
            for (int i = 0; i < array.Length; i++)
                array[i] = rand.Next(0, 100000);
            for (int i = 0; i < array.Length; i++)
                copyArray[i] = array[i];
            File.Delete(path);
        }
        private void ShowRuntimeResult(comparison[] structureOfResults)
        {
            StreamWriter writerResults = new StreamWriter(File.Open(pathToResults, FileMode.OpenOrCreate));
            for (int i = 0; i < structureOfResults.Length; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine("\t{0,-30}", "Метод выборкой");
                    writerResults.WriteLine("\t{0,-30}", "Метод выборкой");
                }
                else if (i == 1)
                {
                    Console.WriteLine("\t{0,-30}", "Метод вставкой");
                    writerResults.WriteLine("\t{0,-30}", "Метод вставкой");
                }
                else if (i == 2)
                {
                    Console.WriteLine("\t{0,-30}", "Метод пузырьком");
                    writerResults.WriteLine("\t{0,-30}", "Метод пузырьком");
                }
                else if (i == 3)
                {
                    Console.WriteLine("\t{0,-30}", "Метод шейкером");
                    writerResults.WriteLine("\t{0,-30}", "Метод шейкером");
                }
                else if (i == 4)
                {
                    Console.WriteLine("\t{0,-30}", "Метод Шелла");
                    writerResults.WriteLine("\t{0,-30}", "Метод Шелла");
                }
                Console.WriteLine("Время выполнения - {0, -30}", structureOfResults[i].runtime);
                writerResults.WriteLine("Время выполнения - {0, -30}", structureOfResults[i].runtime);
                Console.WriteLine("Количество проверок - {0,-30}", structureOfResults[i].counterComparison);
                writerResults.WriteLine("Количество проверок - {0,-30}", structureOfResults[i].counterComparison);
                Console.WriteLine("Количество перестановок - {0,-30}\n", structureOfResults[i].counterSwap);
                writerResults.WriteLine("Количество перестановок - {0,-30}\n", structureOfResults[i].counterSwap);
            }
            writerResults.Close();
        }
    }
}
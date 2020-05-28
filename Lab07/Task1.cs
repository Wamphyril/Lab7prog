using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lab7
{
    class Task1
    {
        public struct tvShow
        {
            public string name;
            public string announcer;
            public byte raiting;
            public char type;
        }
        public struct log
        {
            public DateTime time;
            public string detailInfo;
            public string oldDetailInfo;
            public typeEvent tEvent;
        }
        public enum typeEvent
        {
            ADD,
            DELETE,
            UPDATE
        }

        private log[] dataLog = new log[50];
        private byte index = 0;

        private string pathDir = @"E:\Lab07";
        private string pathFromCopy = @"E:\Lab06\laba.dat";
        private string path = @"E:\Lab07\laba.dat";

        private string[] choices = {"1 – Просмотр таблицы",
                                    "2 – Добавить запись",
                                    "3 – Удалить запись",
                                    "4 – Обновить запись",
                                    "5 – Поиск записей",
                                    "6 – Просмотреть лог",
                                    "7 - Сортировка по рейтингу",
                                    "8 - Выход"};

        public tvShow[] data = new tvShow[0];
        private DateTime dt = new DateTime();

        public void Choose(ref tvShow[] data)
        {
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }
            if (!File.Exists(path))
            {
                File.Copy(pathFromCopy, path);
            }

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    int numElem = 0;
                    for (string line = String.Empty; (line = sr.ReadLine()) != null;)
                    {
                        numElem++;
                    }
                    Array.Resize(ref data, numElem);

                }
                using (StreamReader sr = new StreamReader(path))
                {
                    string[] dataLine = new string[4];
                    int i = 0;
                    for (string line; (line = sr.ReadLine()) != null;)
                    {
                        dataLine = line.Split(" ");
                        data[i].name = dataLine[0];
                        data[i].announcer = dataLine[1];
                        data[i].raiting = Convert.ToByte(dataLine[2]);
                        data[i].type = Convert.ToChar(dataLine[3]);
                        i += 1;
                    }
                }
            }

            while (true)
            {
                Console.Clear();
                foreach (string str in choices)
                {
                    Console.WriteLine(str);
                }
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ShowTable(data);
                        break;
                    case "2":
                        AddNote(ref data, ref index);
                        break;
                    case "3":
                        DeleteNote(ref data);
                        break;
                    case "4":
                        ChangeNote(data);
                        break;
                    case "5":
                        FindNote(data);
                        break;
                    case "6":
                        LogList(dataLog);
                        break;
                    case "7":
                        Sort(data);
                        break;
                    case "8":
                        using (StreamWriter sw = new StreamWriter(path, false))
                        {
                            foreach (var element in data)
                            {
                                sw.Write(element.name + " ");
                                sw.Write(element.announcer + " ");
                                sw.Write(element.raiting + " ");
                                sw.WriteLine(element.type);
                            }
                        }
                        Environment.Exit(0);
                        break;
                }
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        private void AddNote(ref tvShow[] data, ref byte index)
        {
            if (index == 50)
            {
                index = 0;
            }
            log log_add = new log();

            // add
            tvShow show = new tvShow();
            #region Ввод данных
            Console.WriteLine("Введите название тв-шоу");
            show.name = Console.ReadLine();
            Console.WriteLine("Введите Имя и фамилию ведущего");
            show.announcer = Console.ReadLine();
            Console.WriteLine("Введите рейтинг");
            show.raiting = Byte.Parse(Console.ReadLine());
            Console.WriteLine("Введите рейтинг (И, А, Т)");
            show.type = Char.Parse(Console.ReadLine());
            #endregion

            int size = data.Length + 1;
            Array.Resize(ref data, size);
            data[size - 1] = show;

            // log
            dt = DateTime.Now;
            log_add.time = dt;
            log_add.detailInfo = $"{show.name} | {show.announcer} | {show.raiting} | {show.type}";
            log_add.tEvent = typeEvent.ADD;

            dataLog[index].time = dt;
            dataLog[index].tEvent = log_add.tEvent;
            dataLog[index].detailInfo = log_add.detailInfo;
            index++;
        }
        private void ShowTable(tvShow[] data)
        {
            Console.WriteLine(" ___________________________________________________________________________________");
            Console.WriteLine("|Телепередачи_______________________________________________________________________|");
            Console.WriteLine("|{0,-20}|{1,-20}|{2,-20}|{3,-20}|", "Передача____________", "Ведущий_____________", "Рейтинг_____________", "Тип_________________");
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].raiting != 0)
                {
                    Console.WriteLine("|{0,-20}|{1,-20}|{2,-20}|{3,-20}|", $"{i + 1}. " + data[i].name, data[i].announcer, data[i].raiting, data[i].type);
                }
            }
            Console.WriteLine("|И-игровая; А-аналитическая; Т-токшоу_______________________________________________|");
        }
        private void DeleteNote(ref tvShow[] data)
        {
            if (index == 50)
            {
                index = 0;
            }
            log log_add = new log();

            // delete
            Console.WriteLine("Введите номер записи, которую хотите удалить");
            int numDel = Int32.Parse(Console.ReadLine());
            numDel--;
            log_add.detailInfo = $"{data[numDel].name} | {data[numDel].announcer} | {data[numDel].raiting} | {data[numDel].type}";

            Array.Clear(data, numDel, 1);
            if (numDel != data.Length - 1)
            {
                for (int i = numDel; i < data.Length - 1; i++)
                {
                    data[i] = data[i + 1];
                }
                Array.Resize(ref data, data.Length - 1);
            }

            // log
            dt = DateTime.Now;
            log_add.time = dt;
            log_add.tEvent = typeEvent.DELETE;

            dataLog[index].time = dt;
            dataLog[index].tEvent = log_add.tEvent;
            dataLog[index].detailInfo = log_add.detailInfo;
            index++;
        }
        private void ChangeNote(tvShow[] data)
        {
            if (index == 50)
            {
                index = 0;
            }
            log log_add = new log();

            Console.WriteLine("Введите номер записи, которую хотите изменить");
            int numChange = Int32.Parse(Console.ReadLine());
            numChange--;

            log_add.oldDetailInfo = $"{numChange++}. {data[numChange].name} | {data[numChange].announcer} | {data[numChange].raiting} | {data[numChange].type}";
            dataLog[index].oldDetailInfo = log_add.oldDetailInfo;

            Array.Clear(data, numChange, 1);
            tvShow show = new tvShow();
            #region Ввод данных
            Console.WriteLine("Введите название тв-шоу");
            show.name = Console.ReadLine();
            Console.WriteLine("Введите Имя и фамилию ведущего");
            show.announcer = Console.ReadLine();
            Console.WriteLine("Введите рейтинг");
            show.raiting = Byte.Parse(Console.ReadLine());
            Console.WriteLine("Введите рейтинг (И, А, Т)");
            show.type = Char.Parse(Console.ReadLine());
            #endregion
            data[numChange] = show;

            // log
            dt = DateTime.Now;
            log_add.time = dt;
            log_add.detailInfo = $"{show.name} | {show.announcer} | {show.raiting} | {show.type}";
            log_add.tEvent = typeEvent.UPDATE;

            dataLog[index].time = dt;
            dataLog[index].tEvent = log_add.tEvent;
            dataLog[index].detailInfo = log_add.detailInfo;
            index++;
        }
        private void FindNote(tvShow[] data)
        {
            Console.WriteLine("Искать по:\n1. Названию\n2. Имени ведущего\n" +
                                "3. Рейтингу\n4. Типу передачи");
            string mdSearch = Console.ReadLine();
            int index = 1;
            switch (mdSearch)
            {
                case "1":
                    Console.WriteLine("Введите часть или полное название");
                    string strN = Console.ReadLine();
                    index = 1;
                    foreach (tvShow item in data)
                    {
                        if (item.name.StartsWith(strN))
                        {
                            Console.WriteLine("{0,-20}{1,-20}{2,-20}{3,-20}", $"{index}. " + item.name, item.announcer, item.raiting, item.type);
                        }
                        index++;
                    }
                    break;
                case "2":
                    Console.WriteLine("Введите часть или полное название");
                    string strA = Console.ReadLine();
                    index = 1;
                    foreach (tvShow item in data)
                    {
                        if (item.announcer.StartsWith(strA))
                        {
                            Console.WriteLine("{0,-20}{1,-20}{2,-20}{3,-20}", $"{index}. " + item.name, item.announcer, item.raiting, item.type);
                        }
                        index++;
                    }
                    break;
                case "3":
                    Console.WriteLine("Введите рейтинг передачи");
                    byte bRaiting = Byte.Parse(Console.ReadLine());
                    index = 1;
                    foreach (tvShow item in data)
                    {
                        if (item.raiting == bRaiting)
                        {
                            Console.WriteLine("{0,-20}{1,-20}{2,-20}{3,-20}", $"{index}. " + item.name, item.announcer, item.raiting, item.type);
                        }
                        index++;
                    }
                    break;
                case "4":
                    Console.WriteLine("Введите тип передачи");
                    char cType = Char.Parse(Console.ReadLine());
                    index = 1;
                    foreach (tvShow item in data)
                    {
                        if (item.type == cType)
                        {
                            Console.WriteLine("{0,-20}{1,-20}{2,-20}{3,-20}", $"{index}. " + item.name, item.announcer, item.raiting, item.type);
                        }
                        index++;
                    }
                    break;
            }
        }
        private void LogList(log[] dataLog)
        {
            for (int i = 0; i < dataLog.Length; i++)
            {
                if (dataLog[i].time != new DateTime())
                {
                    if (dataLog[i].tEvent == typeEvent.ADD)
                    {
                        Console.WriteLine($"{dataLog[i].time.ToString("hh:mm:ss")} - Добавлена запись ({dataLog[i].detailInfo})");
                    }
                    else if (dataLog[i].tEvent == typeEvent.DELETE)
                    {
                        Console.WriteLine($"{dataLog[i].time.ToString("hh:mm:ss")} - Удалена запись ({dataLog[i].detailInfo})");
                    }
                    else if (dataLog[i].tEvent == typeEvent.UPDATE)
                    {
                        Console.WriteLine($"{dataLog[i].time.ToString("hh:mm:ss")} - Обновлена запись с ({dataLog[i].oldDetailInfo}) на ({dataLog[i].detailInfo})");
                    }
                }
            }
            Console.WriteLine();
        }
        private void Sort(tvShow[] data)
        {
            for (int i = 1; i < data.Length; i++)
            {
                for (int j = i; j > 0 && data[j - 1].raiting > data[j].raiting; j--)
                {
                    byte instant = data[j].raiting;
                    data[j].raiting = data[j - 1].raiting;
                    data[j - 1].raiting = instant;
                }
            }
        }
    }
}

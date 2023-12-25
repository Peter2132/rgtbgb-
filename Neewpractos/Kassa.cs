using System;
using static prog10.Enum;

namespace prog10
{
    internal class Kassa
    {
        public void ShowInteractiveMenu(string name, string role)
        {
            var data = convert.Jsonviser<List<KassaKlass>>($"Admin\\Склад.json", role);
            List<KassaKlass> kassas = new List<KassaKlass>();
            foreach (var item in data)
            {
                KassaKlass kassa = new KassaKlass();
                kassa.ID = item.ID;
                kassa.Name = item.Name;
                kassa.Money = item.Money;
                kassas.Add(kassa);
            }
            convert.Jsonser(kassas, $"Admin\\{role}.json");
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"\t\tДобрый день {name}");
                Console.SetCursorPosition(50, 0);
                Console.WriteLine($"Роль: {role}");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
                Console.SetCursorPosition(80, 2);
                Console.WriteLine("S - Завершить покупку");
                Console.SetCursorPosition(80, 3);
                Console.WriteLine("Escape - Вернуться к регистрации");
                int j = End($"Admin\\{role}.json", role);
                int pos = Arrow.Show(3, j - 1);
                if (pos == (int)button.Back)
                {
                    return;
                }
                if (pos == (int)button.Save)
                {
                    return;
                }
                Read(pos, role);
            }
        }

        private void Read(int pos, string role)
        {
            int colvo = 0;
            var data = convert.Jsonviser<List<SkladKlass>>($"Admin\\Склад.json", role);
            while (true)
            {
                Console.Clear();
                var datakas = convert.Jsonviser<List<KassaKlass>>($"Admin\\{role}.json", role);
                Console.WriteLine("\t\tДобрый день админ");
                Console.SetCursorPosition(50, 0);
                Console.WriteLine($"Роль: {role}");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
                Console.SetCursorPosition(80, 2);
                Console.WriteLine("S - Сохранить");
                Console.SetCursorPosition(80, 3);
                Console.WriteLine("+ - Увеличить количество товара");
                Console.SetCursorPosition(80, 4);
                Console.WriteLine("- - Уменьшить количество товар");
                Console.SetCursorPosition(80, 5);
                Console.WriteLine("Escape - Вернуться");
                int poos = ReadEnd(pos, role);
                if (poos == (int)button.Plus)
                {
                    colvo++;
                    if (data[pos - 3].Colvo <= colvo)
                    {
                        colvo = data[pos - 3].Colvo;
                    }
                }
                if (poos == (int)button.Minus)
                {
                    colvo--;
                    if (colvo <= 0)
                    {
                        colvo = 0;
                    }
                }
                if (poos == (int)button.Save)
                {
                    Console.Clear();
                    List<BuhgalterKlass> dataBuh2 = new List<BuhgalterKlass>();
                    List<BuhgalterKlass> dataBuh = convert.Jsonviser<List<BuhgalterKlass>>("Admin\\Бухгалтер.json", role);
                    BuhgalterKlass buh = new BuhgalterKlass();
                    buh.Name = datakas[pos - 3].Name;
                    buh.Summa = datakas[pos - 3].Tovarov * datakas[pos - 3].Money;
                    buh.DateNow = DateOnly.FromDateTime(DateTime.Now);
                    buh.Premiya = true;
                    if (dataBuh != null)
                    {
                        dataBuh.Add(buh);
                        convert.Jsonser(dataBuh, "Admin\\Бухгалтер.json");
                    }
                    else
                    {
                        dataBuh2.Add(buh);
                        convert.Jsonser(dataBuh2, "Admin\\Бухгалтер.json");
                    }
                    data[pos - 3].Colvo -= colvo;
                    convert.Jsonser(data, "Admin\\Склад.json");

                    return;

                }
                if (poos == (int)button.Back)
                {
                    break;
                }
                datakas[pos - 3].Tovarov = colvo;
                convert.Jsonser(datakas, $"Admin\\{role}.json");
            }
        }
        public int End(string path, string role)
        {
            int j = 3;
            var data = convert.Jsonviser<List<KassaKlass>>(path, role);
            Console.SetCursorPosition(5, 2);
            Console.WriteLine("ID:");
            Console.SetCursorPosition(10, 2);
            Console.WriteLine("Название:");
            Console.SetCursorPosition(25, 2);
            Console.WriteLine("Цена за штуку:");
            Console.SetCursorPosition(45, 2);
            Console.WriteLine("Количество:");
            if (data != null)
            {
                foreach (var item in data)
                {
                    Console.SetCursorPosition(5, j);
                    Console.WriteLine(item.ID);
                    Console.SetCursorPosition(10, j);
                    Console.WriteLine(item.Name);
                    Console.SetCursorPosition(25, j);
                    Console.WriteLine(item.Money);
                    Console.SetCursorPosition(50, j);
                    Console.WriteLine(item.Tovarov);
                    j++;
                }
            }
            return j;
        }
        public virtual int ReadEnd(int pos, string role)
        {
            Console.SetCursorPosition(0, 2);
            var data = convert.Jsonviser<List<KassaKlass>>($"Admin\\{role}.json", role);
            var info = data[pos - 3];
            Console.WriteLine("  ID: " + info.ID);
            Console.WriteLine("  Название: " + info.Name);
            Console.WriteLine("  Цена за штуку: " + info.Money);
            Console.WriteLine("  Количество: " + info.Tovarov);
            int poos = Arrow.Show(2, 5);
            return poos;
        }

    }
}
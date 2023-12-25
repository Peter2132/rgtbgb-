using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static prog10.Enum;

namespace prog10
{
    internal class Buhgalter : Adminprog
    {

        public override void CreateStroke(string role, string name)
        {
            List<BuhgalterKlass> buhalts = new List<BuhgalterKlass>();
            BuhgalterKlass buhal = new BuhgalterKlass();
            Console.WriteLine($"\t\tДобрый день {name}");
            Console.SetCursorPosition(50, 0);
            Console.WriteLine($"Роль: {role}");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(50, 2);
            Console.WriteLine("S - Сохранить");
            Console.SetCursorPosition(50, 3);
            Console.WriteLine("Escape - Вернуться в меню");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("  Введите ID: ");
            Console.WriteLine("  Введите название: ");
            Console.WriteLine("  Введите cумму: ");
            Console.WriteLine("  Время записи: ");
            Console.WriteLine("  Прибавка: ");
            while (true)
            {
                int pos = Arrow.Show(2, 6);
                if (pos == (int)button.Back)
                {
                    return;
                }
                else if (pos == 2)
                {
                    Console.SetCursorPosition(14, 2);
                    buhal.ID = Convert.ToInt32(Console.ReadLine());
                }
                else if (pos == 3)
                {
                    Console.SetCursorPosition(20, 3);
                    buhal.Name = Console.ReadLine();
                }
                else if (pos == 4)
                {
                    Console.SetCursorPosition(17, 4);
                    buhal.Summa = Convert.ToDouble(Console.ReadLine());
                }
                else if (pos == 6)
                {
                    Console.SetCursorPosition(24, 6);
                    string pribavka = Console.ReadLine();
                    if (pribavka == "True")
                    {
                        buhal.Premiya = true;
                    }
                    else if (pribavka == "False")
                    {
                        buhal.Summa *= (-1);
                        buhal.Premiya = false;
                    }
                }
                else if (pos == (int)button.Save)
                {
                    break;
                }
                buhal.DateNow = DateOnly.FromDateTime(DateTime.Now);
            }
            try
            {
                var data1 = convert.Jsonviser<List<BuhgalterKlass>>($"Admin\\{role}.json", role);
                if (data1 != null)
                {
                    data1.Add(buhal);
                    convert.Jsonser(data1, $"Admin\\{role}.json");
                }
                else
                {
                    List<BuhgalterKlass> buhalterLists = new List<BuhgalterKlass>
                    {
                        buhal
                    };
                    convert.Jsonser(buhalterLists, $"Admin\\{role}.json");

                }
            }
            catch (Exception)
            {
                buhalts.Add(buhal);
                convert.Jsonser(buhalts, $"Admin\\{role}.json");
            }
            return;
        }
        public void ShowInteractiveMenu(string name, string role)
        {
            Console.WriteLine($"\t\tДобрый день {name}");
            Console.SetCursorPosition(50, 0);
            Console.WriteLine($"Роль: {role}");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine(" ID");
            Console.WriteLine(" Название");
            Console.WriteLine(" Сумма");
            Console.WriteLine(" Время записи");
            Console.WriteLine(" Прибавка True False");
            Console.SetCursorPosition(0, 8);
            Console.WriteLine("Введите значение для поиска");
            Console.WriteLine(" 1. ID");
            Console.WriteLine(" 2. Название");
            Console.WriteLine(" 3. Сумма");
            Console.WriteLine(" 4. Время записи");
            Console.WriteLine(" 5. Прибавка True False ");

            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите значение");
            string value = Console.ReadLine();

            List<BuhgalterKlass> data = convert.Jsonviser<List<BuhgalterKlass>>($"Admin\\{role}.json", role);

            if (data != null)
            {
                foreach (var item in data)
                {
                    if (choice == 1 && item.ID.ToString() == value)
                    {
                        Chosejson(item);
                    }
                    else if (choice == 2 && item.Name == value)
                    {
                        Chosejson(item);
                    }
                    else if (choice == 3 && item.Summa.ToString() == value)
                    {
                        Chosejson(item);
                    }
                    else if (choice == 4 && item.DateNow.ToString() == value)
                    {
                        Chosejson(item);
                    }
                    else if (choice == 5 && item.Premiya.ToString() == value)
                    {
                        Chosejson(item);
                    }
                }
            }
            ShowInteractiveMenu(name, role, "Admin\\Search.json");
        }
        public override int Menu(string path, string role)
        {
            int j = 3;
            var data = convert.Jsonviser<List<BuhgalterKlass>>(path, role);
            Console.SetCursorPosition(5, 2);
            Console.WriteLine("ID:");
            Console.SetCursorPosition(10, 2);
            Console.WriteLine("Название:");
            Console.SetCursorPosition(25, 2);
            Console.WriteLine("Сумма:");
            Console.SetCursorPosition(35, 2);
            Console.WriteLine("Время записи:");
            Console.SetCursorPosition(65, 2);
            Console.WriteLine("Прибавка?");
            if (data != null)
            {
                foreach (var item in data)
                {
                    Console.SetCursorPosition(5, j);
                    Console.WriteLine(item.ID);
                    Console.SetCursorPosition(10, j);
                    Console.WriteLine(item.Name);
                    Console.SetCursorPosition(25, j);
                    Console.WriteLine(item.Summa);
                    Console.SetCursorPosition(35, j);
                    Console.WriteLine(item.DateNow);
                    Console.SetCursorPosition(65, j);
                    Console.WriteLine(item.Premiya);
                    j++;
                }
            }
            Console.SetCursorPosition(0, j);
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            var dataSum = convert.Jsonviser<List<BuhgalterKlass>>($"Admin\\{role}.json", role);
            double Itog = 0;
            if (dataSum != null)
            {
                foreach (var item in dataSum)
                {
                    Itog += item.Summa;
                }
            }
            Console.SetCursorPosition(65, j + 1);
            Console.WriteLine("Итог: " + Itog);
            return j;
        }
        public override int MenuRead(int pos, string role)
        {
            Console.SetCursorPosition(0, 2);
            var data = convert.Jsonviser<List<BuhgalterKlass>>($"Admin\\{role}.json", role);
            var info = data[pos];
            Console.WriteLine("  ID: " + info.ID);
            Console.WriteLine("  Название: " + info.Name);
            Console.WriteLine("  Сумма: " + info.Summa);
            Console.WriteLine("  Дата: " + info.DateNow);
            Console.WriteLine("  Прибавка: " + info.Premiya);
            int poos = Arrow.Show(2, 6);
            return poos;
        }
        public override void Update(int poos, int pos, string role)
        {
            Console.SetCursorPosition(50, 5);
            Console.WriteLine("Дату менять нельзя");
            Console.SetCursorPosition(50, 6);
            Console.WriteLine("Введите True/False");
            var data = convert.Jsonviser<List<BuhgalterKlass>>($"Admin\\{role}.json", role);
            BuhgalterKlass first = data[pos];
            if (poos - 2 == 0)
            {
                Console.SetCursorPosition(6, 2);
                first.ID = Convert.ToInt32(Console.ReadLine());
                data[pos] = first;
            }
            else if (poos - 2 == 1)
            {
                Console.SetCursorPosition(12, 3);
                first.Name = Console.ReadLine();
                data[pos] = first;
            }
            else if (poos - 2 == 2)
            {
                Console.SetCursorPosition(9, 4);
                first.Summa = Convert.ToDouble(Console.ReadLine());
                data[pos] = first;
            }
            else if (poos - 2 == 4)
            {
                Console.SetCursorPosition(12, 6);
                string pribavka = Console.ReadLine();
                if (pribavka == "True")
                {
                    first.Premiya = true;
                }
                else if (pribavka == "False")
                {
                    first.Summa *= (-1);
                    first.Premiya = false;
                }
                data[pos] = first;
            }
            convert.Jsonser(data, $"Admin\\{role}.json");
        }
        public override void Search(string name, string role)
        {
            var data = convert.Jsonviser<List<BuhgalterKlass>>($"Admin\\{role}.json", role);
            Console.WriteLine($"\t\tДобрый день {name}");
            Console.SetCursorPosition(50, 0);
            Console.WriteLine($"Роль: {role}");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("  ID");
            Console.WriteLine("  Название");
            Console.WriteLine("  Сумма");
            Console.WriteLine("  Время записи");
            Console.WriteLine("  Прибавка True False");
            int pos = Arrow.Show(2, 6);
            Console.SetCursorPosition(0, 8);
            Console.WriteLine("Введите значение для поиска");
            if (pos == 2)
            {
                int ID = Convert.ToInt32(Console.ReadLine());
                foreach (var item in data)
                {
                    if (ID == item.ID)
                    {
                        Chosejson(item);
                    }
                }
            }
            if (pos == 3)
            {
                string nameser = Console.ReadLine();
                foreach (var item in data)
                {
                    if (nameser == item.Name)
                    {
                        Chosejson(item);
                    }
                }
            }
            if (pos == 4)
            {
                int price = Convert.ToInt32(Console.ReadLine());
                foreach (var item in data)
                {
                    if (price == item.Summa)
                    {
                        Chosejson(item);
                    }
                }
            }
            if (pos == 5)
            {
                Console.WriteLine("Введите месяц:");
                int mounth = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите день:");
                int day = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите год:");
                int year = Convert.ToInt32(Console.ReadLine());
                DateOnly dateser = new DateOnly(year, mounth, day);
                foreach (var item in data)
                {
                    if (dateser == item.DateNow)
                    {
                        Chosejson(item);
                    }
                }
            }
            if (pos == 6)
            {
                bool pribav = true;
                string pribavka = Console.ReadLine();
                if (pribavka == "True")
                {
                    pribav = true;
                }
                else if (pribavka == "False")
                {
                    pribav = false;
                }
                foreach (var item in data)
                {
                    if (pribav == item.Premiya)
                    {
                        Chosejson(item);
                    }
                }
            }
            ShowInteractiveMenu(name, role, "Admin\\Search.json");
        }
        private void Chosejson(BuhgalterKlass item)
        {
            List<BuhgalterKlass> data = convert.Jsonviser<List<BuhgalterKlass>>("Admin\\Search.json", "Search");

            if (data == null)
            {
                data = new List<BuhgalterKlass> { item };
            }
            else
            {
                data.Add(item);
            }

            convert.Jsonser(data, "Admin\\Search.json");
        }
    }
}
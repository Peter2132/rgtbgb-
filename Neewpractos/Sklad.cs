using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static prog10.Enum;

namespace prog10
{
    internal class Sklad : Adminprog
    {
        public override void CreateStroke(string role, string name)
        {
            List<SkladKlass> sklads = new List<SkladKlass>();
            SkladKlass sklad = new SkladKlass();
            Console.WriteLine($"\t\tДобрый день {name}");
            Console.SetCursorPosition(50, 0);
            Console.WriteLine($"Роль: {role}");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(50, 2);
            Console.WriteLine("S - Сохранить");
            Console.SetCursorPosition(50, 3);
            Console.WriteLine("Escape - Вернуться к регистрации");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("  Введите ID: ");
            Console.WriteLine("  Введите Название: ");
            Console.WriteLine("  Введите Цену за штуку: ");
            Console.WriteLine("  Введите Количество на складе: ");
            while (true)
            {
                int pos = Arrow.Show(2, 5);
                if (pos == (int)button.Back)
                {
                    return;
                }
                else if (pos == 2)
                {
                    Console.SetCursorPosition(14, 2);
                    if (int.TryParse(Console.ReadLine(), out int idValue))
                    {
                        sklad.ID = idValue;
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 6);
                        Console.WriteLine("низя");

                    }
                }
                else if (pos == 3)
                {
                    Console.SetCursorPosition(20, 3);
                    string inputName = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(inputName) && !inputName.Any(char.IsDigit))
                    {
                        sklad.Name = inputName;
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 6);
                        Console.WriteLine("низя");

                    }
                }
                else if (pos == 4)
                {
                    Console.SetCursorPosition(25, 4);
                    if (int.TryParse(Console.ReadLine(), out int idValue))
                    {
                        sklad.Money = idValue;
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 6);
                        Console.WriteLine("низя");

                    }
                }
                else if (pos == 5)
                {
                    Console.SetCursorPosition(32, 5);
                    if (int.TryParse(Console.ReadLine(), out int idValue))
                    {
                        sklad.Colvo = idValue;
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 6);
                        Console.WriteLine("низя");

                    }
                }
                else if (pos == (int)button.Save)
                {
                    break;
                }
            }
            try
            {
                var data1 = convert.Jsonviser<List<SkladKlass>>($"Admin\\{role}.json", role);
                if (data1 == null)
                {
                    data1 = new List<SkladKlass>();
                }
                data1.Add(sklad);
                convert.Jsonser(data1, $"Admin\\{role}.json");
            }
            catch (Exception)
            {
                sklads.Add(sklad);
                convert.Jsonser(sklads, $"Admin\\{role}.json");
            }
            return;
        }
        public override int Menu(string path, string role)
        {
            int j = 3;
            var data = convert.Jsonviser<List<SkladKlass>>(path, role);
            Console.SetCursorPosition(5, 2);
            Console.WriteLine("ID:");
            Console.SetCursorPosition(10, 2);
            Console.WriteLine("Название:");
            Console.SetCursorPosition(25, 2);
            Console.WriteLine("Цена за штуку:");
            Console.SetCursorPosition(45, 2);
            Console.WriteLine("Кол-во на складе:");
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
                    Console.SetCursorPosition(45, j);
                    Console.WriteLine(item.Colvo);
                    j++;
                }
            }
            return j;
        }
        public override int MenuRead(int pos, string role)
        {

            Console.SetCursorPosition(0, 2);
            var data = convert.Jsonviser<List<SkladKlass>>($"Admin\\{role}.json", role);
            var info = data[pos];
            Console.WriteLine("  ID: " + info.ID);
            Console.WriteLine("  Название: " + info.Name);
            Console.WriteLine("  Цена за штуку: " + info.Money);
            Console.WriteLine("  Кол-во на складе: " + info.Colvo);
            int poos = Arrow.Show(2, 5);
            return poos;
        }
        public override void Update(int poos, int pos, string role)
        {
            var data = convert.Jsonviser<List<SkladKlass>>($"Admin\\{role}.json", role);
            SkladKlass first = data[pos];
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
                Console.SetCursorPosition(17, 4);
                first.Money = Convert.ToInt32(Console.ReadLine());
                data[pos] = first;
            }
            else if (poos - 2 == 3)
            {
                Console.SetCursorPosition(20, 5);
                first.Colvo = Convert.ToInt32(Console.ReadLine());
                data[pos] = first;
            }
            convert.Jsonser(data, $"Admin\\{role}.json");
        }
        public override void Delete(int pos, string role)
        {
            var data = convert.Jsonviser<List<SkladKlass>>($"Admin\\{role}.json", role);
            data.RemoveAt(pos);
            convert.Jsonser(data, $"Admin\\{role}.json");
        }
        public override void Search(string name, string role)
        {
            var data = convert.Jsonviser<List<SkladKlass>>($"Admin\\{role}.json", role);
            Console.WriteLine($"\t\tДобрый день {name}");
            Console.SetCursorPosition(50, 0);
            Console.WriteLine($"Роль: {role}");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("  ID");
            Console.WriteLine("  Название");
            Console.WriteLine("  Цена за штуку");
            Console.WriteLine("  Количество на складе");
            int pos = Arrow.Show(2, 5);
            Console.SetCursorPosition(0, 6);
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
                    if (price == item.Money)
                    {
                        Chosejson(item);
                    }
                }
            }
            if (pos == 5)
            {
                int colvo = Convert.ToInt32(Console.ReadLine());
                foreach (var item in data)
                {
                    if (colvo == item.Colvo)
                    {
                        Chosejson(item);
                    }
                }
            }
            ShowInteractiveMenu(name, role, "Admin\\Search.json");
        }
        public override void ShowInteractiveMenu(string name, string role, string path)
        {
            if (path == "Admin\\Search.json")
            {
                base.ShowInteractiveMenu(name, role, "Admin\\Search.json");
            }
            else
            {
                base.ShowInteractiveMenu(name, role, $"Admin\\{role}.json");
            }
        }
        private void Chosejson(SkladKlass item)
        {
            var data = convert.Jsonviser<List<SkladKlass>>("Admin\\Search.json", "Search");
            List<SkladKlass> list = new List<SkladKlass>();
            SkladKlass sklad = new SkladKlass();
            sklad.ID = item.ID;
            sklad.Name = item.Name;
            sklad.Money = item.Money;
            sklad.Colvo = item.Colvo;
            if (data == null)
            {
                list.Add(sklad);
                convert.Jsonser(list, "Admin\\Search.json");
            }
            else
            {
                data.Add(sklad);
                convert.Jsonser(data, "Admin\\Search.json");
            }
        }
    }

}
using prog10;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static prog10.Enum;


namespace prog10
{
    internal class Adminprog : ICrud2
    {
        public virtual void CreateStroke(string role, string name)
        {
            List<Admin> admin1 = new List<Admin>();
            Admin admin = new Admin();
            Console.WriteLine($"\t\tДобрый день {name}");
            Console.SetCursorPosition(50, 0);
            Console.WriteLine($"Роль: {role}");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(50, 2);
            Console.WriteLine("Роли:");
            Console.SetCursorPosition(50, 3);
            Console.WriteLine("Админ");
            Console.SetCursorPosition(50, 4);
            Console.WriteLine("Кадровик");
            Console.SetCursorPosition(50, 5);
            Console.WriteLine("Склад");
            Console.SetCursorPosition(50, 6);
            Console.WriteLine("Кассир");
            Console.SetCursorPosition(50, 7);
            Console.WriteLine("Бухгалтер");
            Console.SetCursorPosition(50, 8);
            Console.WriteLine("S - Сохранить");
            Console.SetCursorPosition(50, 9);
            Console.WriteLine("Escape - Вернуться к регистрации");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("  Введите ID: ");
            Console.WriteLine("  Введите Login: ");
            Console.WriteLine("  Введите Password: ");
            Console.WriteLine("  Введите Role: ");
            while (true)
            {
                int pos = Arrow.Show(2, 5);
                if (pos == (int)button.Back)
                {
                    return;
                }
                if (pos == (int)button.Save)
                {
                    break;
                }
                else if (pos == 2)
                {
                    Console.SetCursorPosition(14, 2);
                    if (int.TryParse(Console.ReadLine(), out int idValue))
                    {
                        admin.ID = idValue;
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 6);
                        Console.WriteLine("низя");

                    }
                }
                else if (pos == 3)
                {
                    Console.SetCursorPosition(17, 3);
                    admin.Login = Console.ReadLine();
                }
                else if (pos == 4)
                {
                    Console.SetCursorPosition(20, 4);
                    admin.Password = Console.ReadLine();
                }
                else if (pos == 5)
                {
                    Console.SetCursorPosition(15, 5);
                    admin.Role = Console.ReadLine();
                }
            }
            try
            {
                var newdata = convert.Jsonviser<List<Admin>>($"Admin\\{role}.json", role);
                newdata.Add(admin);
                convert.Jsonser(newdata, $"Admin\\{role}.json");
            }
            catch (Exception)
            {
                admin1.Add(admin);
                convert.Jsonser(admin1, $"Admin\\{role}.json");
            }
            return;
        }
        public virtual void Delete(int pos, string role)
        {
            var data = convert.Jsonviser<List<Admin>>($"Admin\\{role}.json", role);
            data.RemoveAt(pos);
            convert.Jsonser(data, $"Admin\\{role}.json");
        }
        public void Read(int pos, string role, string name)
        {
            while (true)
            {
                Console.WriteLine($"\t\tДобрый день {name}");
                Console.SetCursorPosition(50, 0);
                Console.WriteLine($"Роль: {role}");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
                Console.SetCursorPosition(50, 2);
                Console.WriteLine("S - Сохранить");
                Console.SetCursorPosition(50, 3);
                Console.WriteLine("F10 - Удалить");
                Console.SetCursorPosition(50, 4);
                Console.WriteLine("Escape - Вернуться");
                int poos = MenuRead(pos, role);
                if (poos == (int)button.Back)
                {
                    break;
                }
                else if (poos == (int)button.F10)
                {
                    Delete(pos, role);
                    return;
                }
                else
                {
                    Update(poos, pos, role);
                    Console.Clear();
                }
            }
            return;
        }
        public virtual void Update(int poos, int pos, string role)
        {
            var data = convert.Jsonviser<List<Admin>>($"Admin\\{role}.json", role);
            Admin first = data[pos];
            if (poos - 2 == 0)
            {
                Console.SetCursorPosition(7, 2);
                first.ID = Convert.ToInt32(Console.ReadLine());
                data[pos] = first;
            }
            else if (poos - 2 == 1)
            {
                Console.SetCursorPosition(10, 3);
                first.Login = Console.ReadLine();
                data[pos] = first;
            }
            else if (poos - 2 == 2)
            {
                Console.SetCursorPosition(13, 4);
                first.Password = Console.ReadLine();
                data[pos] = first;
            }
            else if (poos - 2 == 3)
            {
                Console.SetCursorPosition(10, 5);
                first.Role = Console.ReadLine();
                data[pos] = first;
            }
            convert.Jsonser(data, $"Admin\\{role}.json");
        }
        public virtual void ShowInteractiveMenu(string name, string role, string path)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"\t\tДобрый день {name}");
                Console.SetCursorPosition(50, 0);
                Console.WriteLine($"Роль: {role}");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
                Console.SetCursorPosition(80, 2);
                Console.WriteLine("F1 - Создать запись");
                Console.SetCursorPosition(80, 3);
                Console.WriteLine("F2 - Поиск...");
                Console.SetCursorPosition(80, 4);
                Console.WriteLine("Enter - Открыть пользователя");
                Console.SetCursorPosition(80, 5);
                Console.WriteLine("Escaape - Вернуться к регистрации");
                int o = Menu(path, role);
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                File.Create(desktop + "\\" + "Admin\\Search.json").Close();
                int pos = Arrow.Show(3, o - 1);
                if (pos == (int)button.Create)
                {
                    Console.Clear();
                    CreateStroke(role, name);
                }
                else if (pos == (int)button.Back)
                {
                    break;
                }
                else if (pos == (int)button.Search)
                {
                    Console.Clear();
                    Search(name, role);
                }
                else
                {
                    Console.Clear();
                    Read(pos - 3, role, name);
                }
            }
            return;
        }
        public virtual int Menu(string path, string role)
        {
            int j = 3;
            var data = convert.Jsonviser<List<Admin>>(path, role);
            Console.SetCursorPosition(5, 2);
            Console.WriteLine("ID:");
            Console.SetCursorPosition(15, 2);
            Console.WriteLine("Login:");
            Console.SetCursorPosition(25, 2);
            Console.WriteLine("Password:");
            Console.SetCursorPosition(40, 2);
            Console.WriteLine("Role:");
            if (data != null)
            {
                foreach (var item in data)
                {
                    Console.SetCursorPosition(5, j);
                    Console.WriteLine(item.ID);
                    Console.SetCursorPosition(15, j);
                    Console.WriteLine(item.Login);
                    Console.SetCursorPosition(25, j);
                    Console.WriteLine(item.Password);
                    Console.SetCursorPosition(40, j);
                    Console.WriteLine(item.Role);
                    j++;
                }
            }
            return j;
        }
        public virtual int MenuRead(int pos, string role)
        {
            Console.SetCursorPosition(0, 2);
            var data = convert.Jsonviser<List<Admin>>($"Admin\\{role}.json", role);
            var info = data[pos];
            Console.WriteLine("  ID: " + info.ID);
            Console.WriteLine("  Login: " + info.Login);
            Console.WriteLine("  Password: " + info.Password);
            Console.WriteLine("  Role: " + info.Role);
            int poos = Arrow.Show(2, 5);
            return poos;
        }
        public virtual void Search(string name, string role)
        {
            var data = convert.Jsonviser<List<Admin>>($"Admin\\{role}.json", role);
            Console.WriteLine($"\t\tДобрый день {name}");
            Console.SetCursorPosition(50, 0);
            Console.WriteLine($"Роль: {role}");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("  ID");
            Console.WriteLine("  Логин");
            Console.WriteLine("  Пароль");
            Console.WriteLine("  Роль");
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
                string login = Console.ReadLine();
                foreach (var item in data)
                {
                    if (login == item.Login)
                    {
                        Chosejson(item);
                    }
                }
            }
            if (pos == 4)
            {
                string parol = Console.ReadLine();
                foreach (var item in data)
                {
                    if (parol == item.Password)
                    {
                        Chosejson(item);
                    }
                }
            }
            if (pos == 5)
            {
                string rolesear = Console.ReadLine();
                foreach (var item in data)
                {
                    if (rolesear == item.Role)
                    {
                        Chosejson(item);
                    }
                }
            }
            ShowInteractiveMenu(name, role, "Admin\\Search.json");
        }
        private void Chosejson(Admin item)
        {
            var data = convert.Jsonviser<List<Admin>>("Admin\\Search.json", "Search");
            List<Admin> list = new List<Admin>();
            Admin admin = new Admin();
            admin.ID = item.ID;
            admin.Login = item.Login;
            admin.Password = item.Password;
            admin.Role = item.Role;
            if (data == null)
            {
                list.Add(admin);
                convert.Jsonser(list, "Admin\\Search.json");
            }
            else
            {
                data.Add(admin);
                convert.Jsonser(data, "Admin\\Search.json");
            }
        }
    }
}
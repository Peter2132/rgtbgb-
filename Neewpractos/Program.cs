using prog10;
using System;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string log = "";
        List<char> password = new List<char>();
        string role = "";
        string pasword = "";
        string name = "";
        while (true)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Directory.CreateDirectory(desktop + "\\Admin");
            List<Admin> data = convert.Jsonviser<List<Admin>>("Admin\\Админ.json", "Админ");
            if (data == null)
            {
                List<Admin> admins = new List<Admin>();
                Admin admin = new Admin();
                admin.Login = "admin";
                admin.Password = "admin";
                admin.Role = "Админ";
                admins.Add(admin);
                convert.Jsonser(admins, "Admin\\Админ.json");
                data = convert.Jsonviser<List<Admin>>("Admin\\Админ.json", "Админ");
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("\t\t\tИнформационная система Магазин продуктов.");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("  Логин: ");
            Console.WriteLine("  Пароль: ");
            Console.WriteLine("  Авторизоваться");
            int pos = Arrow.Show(2, 4);
            switch (pos)
            {
                case 2:
                    Console.SetCursorPosition(9, pos);
                    log = Console.ReadLine();
                    break;
                case 3:
                    ConsoleKeyInfo pasword1;
                    int i = 10;
                    Console.SetCursorPosition(10, pos);
                    while (true)
                    {
                        pasword1 = Console.ReadKey(true);
                        if (pasword1.Key == ConsoleKey.Enter)
                        {
                            foreach (char r in password)
                            {
                                pasword += r;
                            }
                            password.Clear();
                            Console.SetCursorPosition(0, pos);
                            break;
                        }
                        if (pasword1.Key == ConsoleKey.Backspace)
                        {
                            i--;
                            password.Remove(password.Last());
                            Console.SetCursorPosition(i, pos);
                            Console.Write(" ");
                        }
                        else
                        {
                            char charr = Convert.ToChar(pasword1.KeyChar);
                            password.Add(charr);
                            Console.Write("*");
                            i++;
                        }
                    }
                    break;
                case 4:
                    bool roleFound = false;
                    foreach (var item in data)
                    {
                        if (log == item.Login && pasword == item.Password)
                        {
                            role = item.Role;
                            pasword = "";

                            var datakad = convert.Jsonviser<List<KadrKlass>>("Admin\\Кадровик.json", "Кадровик");
                            if (datakad != null)
                            {
                                foreach (var k in datakad)
                                {
                                    if (item.ID == k.ID_2)
                                    {
                                        name = k.Name;
                                    }
                                }
                            }

                            roleFound = true;
                            break;
                        }
                    }

                    if (!roleFound)
                    {
                        Console.WriteLine("Ошибка: Данной роли не найдено.");
                        break;
                    }
                    if (role == "Админ")
                    {
                        Adminprog adminTable = new Adminprog();
                        if (name != "")
                        {
                            adminTable.ShowInteractiveMenu(name, role, $"Admin\\{role}.json");
                        }
                        else
                        {
                            adminTable.ShowInteractiveMenu("Админ", role, $"Admin\\{role}.json");
                        }
                    }

                    if (role == "Склад")
                    {
                        Sklad sklad = new Sklad();
                        if (name != "")
                        {
                            sklad.ShowInteractiveMenu(name, role, $"Admin\\{role}.json");
                        }
                        else
                        {
                            sklad.ShowInteractiveMenu("Склад", role, $"Admin\\{role}.json");
                        }
                    }
                    if (role == "Кассир")
                    {
                        Kassa Kassa = new Kassa();
                        if (name != "")
                        {
                            Kassa.ShowInteractiveMenu(name, role);
                        }
                        else
                        {
                            Kassa.ShowInteractiveMenu("Кассир", role);
                        }
                    }
                    if (role == "Бухгалтер")
                    {
                        Buhgalter buh = new Buhgalter();
                        if (name != "")
                        {
                            buh.ShowInteractiveMenu(name, role, $"Admin\\{role}.json");
                        }
                        else
                        {
                            buh.ShowInteractiveMenu("Бухгалтер", role, $"Admin\\{role}.json");
                        }
                    }
                    if (role == "Кадровик")
                    {
                        Kadr kadrov = new Kadr();
                        if (name != "")
                        {
                            kadrov.ShowInteractiveMenu(name, role, $"Admin\\{role}.json");
                        }
                        else
                        {
                            kadrov.ShowInteractiveMenu("Кадровик", role, $"Admin\\{role}.json");
                        }
                    }
                    break;
            }
        }
    }
}
using System;

namespace RestaurantOrder
{
    class Program
    {
        static string[] menu = {
            "Пицца",
            "Паста",
            "Салат",
            "Стейк",
            "Суп",
            "Рыба",
            "Торт",
            "Чай"
        };

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            byte orderBits = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ЗАКАЗ БЛЮД ===\n");
                ShowOrder(orderBits);
                Console.WriteLine("\nМЕНЮ:");
                ShowMenuWithStatus(orderBits);

                Console.WriteLine("\nДоступные команды:");
                Console.WriteLine("  номер_блюда (1-8) - добавить/удалить блюдо");
                Console.WriteLine("  0 - вывести заказ и выйти");
                Console.Write("\nВаш выбор: ");

                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice))
                {
                    if (choice == 0)
                    {
                        FinalizeOrder(orderBits);
                        break;
                    }
                    else if (choice >= 1 && choice <= 8)
                    {
                        orderBits = ToggleDish(orderBits, choice - 1);
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод. Нажмите Enter...");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Неверный ввод. Нажмите Enter...");
                    Console.ReadLine();
                }
            }
        }

        static void ShowOrder(byte orderBits)
        {
            Console.WriteLine("Заказанные блюда:");
            bool hasItems = false;
            for (int i = 0; i < menu.Length; i++)
            {
                if ((orderBits & (1 << i)) != 0)
                {
                    Console.WriteLine($"  - {menu[i]}");
                    hasItems = true;
                }
            }
            if (!hasItems)
                Console.WriteLine("  (ничего не заказано)");
        }

        static void ShowMenuWithStatus(byte orderBits)
        {
            for (int i = 0; i < menu.Length; i++)
            {
                string status = ((orderBits & (1 << i)) != 0) ? "[X] ЗАКАЗАНО" : "[ ] не заказано";
                Console.WriteLine($"{i + 1,2}. {menu[i],-20} {status}");
            }
        }

        static byte ToggleDish(byte orderBits, int dishIndex)
        {
            byte newBits = (byte)(orderBits ^ (1 << dishIndex));
            string action = ((orderBits & (1 << dishIndex)) != 0) ? "удалено" : "добавлено";
            Console.WriteLine($"\nБлюдо \"{menu[dishIndex]}\" {action}.");
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
            return newBits;
        }

        static void FinalizeOrder(byte orderBits)
        {
            Console.Clear();
            Console.WriteLine("=== ИТОГОВЫЙ ЗАКАЗ ===");
            Console.WriteLine("\nЗаказанные позиции:");
            bool hasOrder = false;
            for (int i = 0; i < menu.Length; i++)
            {
                if ((orderBits & (1 << i)) != 0)
                {
                    Console.WriteLine($"  - {menu[i]}");
                    hasOrder = true;
                }
            }
            if (!hasOrder)
            {
                Console.WriteLine("  (пусто)");
            }

            Console.WriteLine("\nНажмите Enter для выхода...");
            Console.ReadLine();
        }
    }
}
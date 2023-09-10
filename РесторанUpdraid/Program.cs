using System;
using System.Reflection.Metadata;
using System.Text;
using РесторанUpdraid;



namespace Res
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.UTF8;


            var sectoins = new List<Section>();
            var orders = new List<Order>();
            var rand = new Random();

            InitializeSection();

            bool exit = false;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t<<<Wellcome>>>");
            Console.ResetColor();
            while (!exit)
            {

                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Показать меню");
                Console.WriteLine("2. Сделать заказ");
                Console.WriteLine("3. Показать заказ");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("4. Для администратора");
                Console.ResetColor();

                Console.WriteLine("9. Выйти");

                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();

                Console.Clear();

                switch (choice)
                {
                    case "1":
                        ShowMenu();
                        break;
                    case "2":
                        MakeOrder();
                        break;
                    case "3":
                        ShowOrder();
                        break;

                    case "4":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\t<<<Доступ администратора>>>");
                        Console.ResetColor();
                        bool exit2 = false;
                        while (!exit2)
                        {

                            Console.WriteLine("1. Получить отчет по продажам за все время");
                            Console.WriteLine("2. Получить отчет по продажам за определеный период");
                            Console.WriteLine("3. Проверить какие блюда в наличии");
                            Console.WriteLine("9. Вернутся в главное меню");

                            Console.Write("Ваш выбор: ");
                            string choice2 = Console.ReadLine();
                            Console.Clear();
                            switch (choice2)
                            {
                                case "1":
                                    ShowSalesReportAll();
                                    Console.WriteLine();
                                    break;
                                case "2":
                                    ShowSalesReportTime();
                                    Console.WriteLine();
                                    break;
                                case "3":
                                    ChekAbsent();
                                    break;
                                case "9":
                                    exit2 = true;
                                    break;
                            }
                        }
                        break;
                    case "9":
                        exit = true;
                        break;
                }
            }

            void ShowSalesReportTime()
            {
                try
                {

                Console.Write("Введите начала (дд.мм.гг): ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Введите конец (дд.мм.гг): ");
                DateTime endDate = DateTime.Parse(Console.ReadLine());
                decimal totalSales = 0;
                foreach (Order order in orders)
                {
                    if (order.OrderTime >= startDate && order.OrderTime <= endDate)
                    {
                        totalSales += order.TotalPrice;
                    }
                }
                Console.WriteLine($"Прибыль от {startDate} до {endDate} : {totalSales}сом");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            void ChekAbsent()
            {
                foreach (var section in sectoins)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Категория - {section.Name}");
                    Console.ResetColor();
                    foreach (var dish in section.Dishes)
                    {
                        if (dish.Count == 0)
                        {
                            Console.ForegroundColor= ConsoleColor.Red;
                            Console.Write("Отсусвует: ");
                            Console.ResetColor();
                            Console.WriteLine($"{dish.Name} | {dish.Count}");
                        }
                        else
                        {
                            Console.WriteLine($"В наличии: {dish.Name} | {dish.Count}");
                        }
                    }
                }
            }
            void ShowSalesReportAll()
            {
                decimal totalSales = 0;
                foreach (var order in orders)
                {
                    totalSales += order.TotalPrice;
                }
                Console.WriteLine($"Общая прибыль - {totalSales} сом");
            }


            void ShowMenu()
            {
                foreach (var section in sectoins)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"<<< {section.Name} >>>");
                    Console.ResetColor();
                    foreach (var dish in section.Dishes)
                    {
                        Console.WriteLine($"Название: {dish.Name} | Стоимость: {dish.Price}сом | Наличие: {dish.Count} ");
                    }
                    Console.WriteLine();
                }
            }

            void MakeOrder()
            {
                int orderIDnumber = rand.Next(10000, 99999);
                List<Dish> dishesForNewOrder = new List<Dish>();
                decimal totalPrice = 0;

                Console.WriteLine("Введите название блюд которое хотите заказать:");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(0, 25);
                Console.WriteLine("Введите '9' для подтверждения заказа");
                Console.ResetColor();
                Console.SetCursorPosition(0, 1);
                Console.WriteLine();

                int i = 1;
                while (true)
                {
                    Console.Write($"{i}) ");
                    string dishName = Console.ReadLine();
                    if (dishName.ToLower() == "9")
                    {
                        break;
                    }

                    var dish = FindDish(dishName);
                    if (dish.Count == 0) { Console.WriteLine($"Нет в наличии"); break; }
                    if (dish == null)
                    {
                        Console.WriteLine("Неверный формат");
                        continue;
                    }

                    Console.Write("Введите количество: ");
                    int countenter = int.Parse(Console.ReadLine());

                    dishesForNewOrder.Add(dish);
                    totalPrice += dish.Price * countenter;
                    i++;
                }

                var newOrder = new Order(orderIDnumber, totalPrice, dishesForNewOrder, DateTime.Now);
                orders.Add(newOrder);
                Console.WriteLine("Заказ успешно добавлен\n");
            }

            void ShowOrder()
            {
                foreach (Order order in orders)
                {
                    Console.WriteLine($"Заказ №{order.OrderNumber} - {order.OrderTime}");
                    foreach (Dish dish in order.Dishes)
                    {
                        Console.WriteLine($"Название: {dish.Name} | Стоимость: {dish.Price} сом");
                    }
                    Console.WriteLine($"Общая сумма заказа: {order.TotalPrice} сом");
                    Console.WriteLine();
                }
            }


            Dish FindDish(string name)
            {
                foreach (var section in sectoins)
                {
                    foreach (Dish dish in section.Dishes)
                    {
                        if (dish.Name.ToLower() == name.ToLower())
                        {
                            return dish;
                        }
                    }
                }
                return null;
            }


            void InitializeSection()
            {
                var section1 = new Section("Завтраки и закуски", new List<Dish>
            {
                new Dish(1,"Омлет с салатом" ,180,rand.Next(0,20)),
                new Dish(2,"Творог с клубникой", 190,rand.Next(0,20)),
                new Dish(3,"Сырники со сметаной",280, rand.Next(0, 20)),
                new Dish(4,"Каша манная",130, rand.Next(0, 20)),
                new Dish(5,"Сэндвич",150, rand.Next(0, 20))
            });
                var section2 = new Section("Супы", new List<Dish>
            {
                new Dish(1,"Грибной суп", 250, rand.Next(0, 20)),
                new Dish(2,"Уха",350, rand.Next(0, 20)),
                new Dish(3,"Борщ",250, rand.Next(0, 20)),
                new Dish(4,"Суп куриный",250, rand.Next(0, 20)),
                new Dish(5,"Солянка мясная",300, rand.Next(0, 20))
            });
                var section3 = new Section("Напитки", new List<Dish>
            {
                new Dish(1,"Чай черный",100,rand.Next(0, 20)),
                new Dish(2, "Экспрессо",150,rand.Next(0, 20)),
                new Dish(3,"Сироп",50, rand.Next(0, 20)),
                new Dish(4,"Кола", 90, rand.Next(0, 20))
            });
                var section4 = new Section("Десерты", new List<Dish>
            {
                new Dish(1,"ЧисКейк",200, rand.Next(0, 20)),
                new Dish(2,"Пирог яблочный",250, rand.Next(0, 20)),
                new Dish(3,"Булочка с курицей",90, rand.Next(0, 20))
            });

                sectoins.Add(section1);
                sectoins.Add(section2);
                sectoins.Add(section3);
                sectoins.Add(section4);
            }
        }



    }


}

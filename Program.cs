using System.Windows.Markup;

namespace GarageConsoleApp;

public class Program 
{
    public static void Main(string[] args)
    {
        bool isCorrectValue = true;
        do
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Выберете действие: ");
            Console.WriteLine("1 - Получить данные о водителях;");
            Console.WriteLine("2 - Получить данные о типах автомобилей;");
            Console.WriteLine("3 - Просмотреть список машин;");
            Console.WriteLine("4 - Получить данные о категориях прав;");
            Console.WriteLine("5 - Получить данные о правах водителей");
            Console.WriteLine("6 - Просмотреть права водителя;");
            Console.WriteLine("7 - Получить данные о маршрутах;");
            Console.WriteLine("8 - Получить информацию о рейсах");
            Console.WriteLine("9 - Добавить нового водителя;");
            Console.WriteLine("10 - Добавить тип автомобиля;");
            Console.WriteLine("11 - Добавить автомобиль;");
            Console.WriteLine("12 - Добавить категорию прав;");
            Console.WriteLine("13 - Добавить категорию прав водителю;");
            Console.WriteLine("14 - Добавить маршрут;");
            Console.WriteLine("15 - Добавить рейс;");
            Console.WriteLine("0 - Выход.");
            Console.WriteLine("-------------------------------------");
            try
            {
                Console.Write("Ваш выбор: ");
                int temp = int.Parse(Console.ReadLine());
                switch (temp)
                {
                    case 1:
                        DatabaseRequests.GetDriverQuery();
                        Console.WriteLine();
                        break;
                    case 2:
                        DatabaseRequests.GetTypeCarQuery();
                        Console.WriteLine();
                        break;
                    case 3:
                        DatabaseRequests.GetCarQuery();
                        Console.WriteLine();
                        break;
                    case 4:
                        DatabaseRequests.GetRightsCategoryQuery();
                        Console.WriteLine();
                        break;
                    case 5:
                        DatabaseRequests.GetAllRightsCategoryQuery();
                        Console.WriteLine();
                        break;
                    case 6:
                        Console.WriteLine("Введите ID водителя: ");
                        int idDriver = int.Parse(Console.ReadLine());

                        DatabaseRequests.GetDriverRightsCategoryQuery(idDriver);
                        Console.WriteLine();
                        break;
                    case 7:
                        DatabaseRequests.GetItineraryQuery();
                        Console.WriteLine();
                        break;
                    case 8:
                        DatabaseRequests.GetRouteQuery();
                        break;
                    case 9:
                        Console.WriteLine("Введите имя: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Введите фамилию: ");
                        string surname = Console.ReadLine();
                        Console.WriteLine("Введите дату рождения: ");

                        DateTime birthday = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", null);
                        DatabaseRequests.AddDriverQuery(name, surname, birthday);
                        break;
                    case 10:
                        Console.WriteLine("Введите тип автомобиля: ");
                        string typeCar = Console.ReadLine();

                        DatabaseRequests.AddTypeCarQuery(typeCar);
                        Console.WriteLine("Тип автомобиля добавлен!");
                        break;
                    case 11:
                        Console.WriteLine("Введите ID типа автомобиля: ");
                        int idTypeCar = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите название автомобиля: ");
                        string nameCar = Console.ReadLine();
                        Console.WriteLine("Введите номер автомобиля: ");
                        string stateNumber = Console.ReadLine();
                        Console.WriteLine("Введите количество мест в автомобиле: ");
                        int numberPassengers = int.Parse(Console.ReadLine());

                        DatabaseRequests.AddCarQuery(idTypeCar, nameCar, stateNumber, numberPassengers);
                        break;
                    case 12:
                        Console.WriteLine("Введите название категории прав: ");
                        string nameCategory = Console.ReadLine();
                        DatabaseRequests.AddRightsCategoryQuery(nameCategory);
                        Console.WriteLine("Категория добавлена!");
                        break;
                    case 13:
                        Console.WriteLine("Введите ID водителя: ");
                        idDriver = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите ID Категории: ");
                        int rightsCategory = int.Parse(Console.ReadLine());

                        DatabaseRequests.AddDriverRightsCategoryQuery(idDriver, rightsCategory);
                        break;
                    case 14:
                        Console.WriteLine("Введите маршрут: ");
                        string nameIterary = Console.ReadLine();
                        DatabaseRequests.AddItineraryQuery(nameIterary);
                        Console.WriteLine("Маршрут добавлен!");
                        break;
                    case 15:
                        Console.WriteLine("Введите ID водителя: ");
                        idDriver = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите ID машины: ");
                        int idCar = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите ID маршрута: ");
                        int idItinerary = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите количество пассажиров: ");
                        numberPassengers = int.Parse(Console.ReadLine());

                        DatabaseRequests.AddRouteQuery(idDriver, idCar, idItinerary, numberPassengers);
                        break;
                    case 0:
                        isCorrectValue = false;
                        break;
                    default:
                        Console.WriteLine("Неккоректный ввод!");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine();
                Console.WriteLine("Неккоректный ввод!");
                Console.WriteLine();
            }
        } while (isCorrectValue);
    }
}
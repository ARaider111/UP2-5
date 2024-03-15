using Microsoft.EntityFrameworkCore.Internal;
using Npgsql;

namespace GarageConsoleApp;

/// Класс DatabaseRequests
/// содержит методы для отправки запросов к БД

public static class DatabaseRequests
{
    /// 1
    /// /// Метод GetDriverQuery:
    /// отправляет запрос в БД на получение списка водителей
    /// выводит в консоль данные о водителях
    public static void GetDriverQuery()
    {
        var querySql = "SELECT * FROM driver";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"Id: {reader[0]} Имя: {reader[1]} Фамилия: {reader[2]} Дата рождения: {reader[3]}");
        }
    }

    /// 2
    /// Метод GetTypeCarQuery:
    /// отправляет запрос в БД на получение списка типов машин
    /// выводит в консоль список типов машин
    public static void GetTypeCarQuery()
    {
        // Сохраняем в переменную запрос на получение всех данных и таблицы type_car
        var querySql = "SELECT * FROM type_car";
        // Создаем команду(запрос) cmd, передаем в нее запрос и соединение к БД
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        // Выполняем команду(запрос)
        // результат команды сохранится в переменную reader
        using var reader = cmd.ExecuteReader();
        
        // Выводим данные которые вернула БД
        while (reader.Read())
        {
            Console.WriteLine($"Id: {reader[0]} Название: {reader[1]}");
        }
    }
    
    /// 3
    /// Метод GetCarQuery:
    /// отправляет запрос в БД на получение автомобилей
    /// выводит в консоль информацию об автомобилях
    public static void GetCarQuery()
    {
        var querySql = "SELECT dr.name, car.name, car.state_number, car.number_passengers " +
                       "FROM car " +
                       "INNER JOIN type_car dr on car.id_type_car = dr.id; ";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"Тип автомобиля: {reader[0]} Название автомобиля: {reader[1]}  Номер: {reader[2]}  Количество мест: {reader[3]}");
        }
    } 

    /// 4
    /// Метод GetRightsCategoryQuery():
    /// отправляет запрос в БД на получение списка котегорий прав
    /// выводит в консоль список категорий 
    public static void GetRightsCategoryQuery()
    {
        var querySql = "SELECT * FROM rights_category";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"Id: {reader[0]} Категория: {reader[1]}");
        }
    }
    
    /// 5
    /// Метод GetAllRightsQuery:
    /// оправляет запрос в БД на получение всех прав волителей
    /// выводит в консоль список всех прав водителей
    public static void GetAllRightsCategoryQuery()
    {
        var querySql = "SELECT dr.first_name, dr.last_name, rc.name " +
                       "FROM driver_rights_category " +
                       "INNER JOIN driver dr on driver_rights_category.id_driver = dr.id " +
                       "INNER JOIN rights_category rc on rc.id = driver_rights_category.id_rights_category;";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"Имя: {reader[0]} Фамилия: {reader[1]} Категория прав: {reader[2]}");
        }
    }
    
    /// 6
    /// Метод GetDriverRightsCategoryQuery:
    /// отправляет запрос в БД на получение категории выбранного водителя
    /// выводит в консоль информацию о категории прав водителя
    public static void GetDriverRightsCategoryQuery(int driver)
    {
        try
        {
            var querySql = "SELECT dr.first_name, dr.last_name, rc.name " +
                           "FROM driver_rights_category " +
                           "INNER JOIN driver dr on driver_rights_category.id_driver = dr.id " +
                           "INNER JOIN rights_category rc on rc.id = driver_rights_category.id_rights_category " +
                           $"WHERE dr.id = {driver};";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            using var reader = cmd.ExecuteReader();

            reader.Read();
            Console.WriteLine($"Имя: {reader[0]} Фамилия: {reader[1]} Категория прав: {reader[2]}");
        }
        catch (PostgresException)
        {
            Console.WriteLine();
            Console.WriteLine("Данное ID отсутсвует!");
            Console.WriteLine();
        }
    }
    
    /// 7
    /// Метод GetItineraruQuery:
    /// Отправляет запрос БД на получение информации о всех маршрутах
    /// выводит список всех маршрутов
    public static void GetItineraryQuery()
    {
        var querySql = "SELECT * FROM itinerary";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"Id: {reader[0]} Маршрут: {reader[1]}");
        }
    }

    /// 8
    /// Метод GetRouteQuery:
    /// Отправляет запрос в БД на получение информации о рейсах
    /// выводит список всех рейсов
    public static void GetRouteQuery()
    {
        var querySql = "SELECT route.id, dr.first_name, dr.last_name, cr.name, cr.state_number, it.name  " +
                       "FROM route " +
                       "INNER JOIN driver dr on route.id_driver = dr.id " +
                       "INNER JOIN car cr on route.id_car = cr.id " +
                       "INNER JOIN itinerary it on route.id_itinerary = it.id";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"Id: {reader[0]} Имя: {reader[1]} Фамилия: {reader[2]} " +
                              $"Автомобиль: {reader[3]} Номер автомобиля: {reader[4]} Маршрут: {reader[5]}");
        }
    }
    
    /// 9
    /// Метод AddDriverQuery:
    /// отправляет запрос в БД на добавление водителей
    public static void AddDriverQuery(string firstName, string lastName, DateTime birthdate)
    {
        var querySql = $"INSERT INTO driver(first_name, last_name, birthdate) VALUES ('{firstName}', '{lastName}', '{birthdate}')";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }

    /// 10
    /// Метод AddTypeCarQuery
    /// отправляет запрос в БД на добавление типа машины
    public static void AddTypeCarQuery(string name)
    {
        var querySql = $"INSERT INTO type_car(name) VALUES ('{name}')";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }

    /// 11
    /// Метод AddCarQuery:
    /// отправляет запрос БД на добавление автомобиля
    public static void AddCarQuery(int idTypeCar, string nameCar, string stateNumber, int numberPassengers)
    {
        try
        {
            var querySql = $"INSERT INTO car(id_type_car, name, state_number, number_passengers) VALUES ({idTypeCar}, '{nameCar}', '{stateNumber}', {numberPassengers})";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            cmd.ExecuteNonQuery();
            
            Console.WriteLine("Автомобиль добавлен!");
        }
        catch (PostgresException)
        {
            Console.WriteLine();
            Console.WriteLine("Данное ID отсутсвует!");
            Console.WriteLine();
        }
    }

    /// 12
    /// Метод AddRightsCategoryQuery:
    /// отправляет запрос в БД на добавление категорий прав
    public static void AddRightsCategoryQuery(string name)
    {
        var querySql = $"INSERT INTO rights_category(name) VALUES ('{name}')";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }

    /// 13
    /// Метод AddDriverRightsCategoryQuery:
    /// отправляет запрос в БД на добавление категории прав водителю
    public static void AddDriverRightsCategoryQuery(int driver, int rightsCategory)
    {
        try
        {
            var querySql =
                $"INSERT INTO driver_rights_category(id_driver, id_rights_category) VALUES ({driver}, {rightsCategory})";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            cmd.ExecuteNonQuery();
            
            Console.WriteLine("Категория водителю добавлена!");
        }
        catch (PostgresException)
        {
            Console.WriteLine();
            Console.WriteLine("Данное ID отсутсвует!");
            Console.WriteLine();
        }
    }
    
    /// 14
    /// Метод AddItineraryQuery:
    /// отправляет запрос в БД на добавление маршрута
    public static void AddItineraryQuery(string nameItinerary)
    {
        var querySql = $"INSERT INTO itinerary(name) VALUES ('{nameItinerary}')";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }

    /// 15
    /// Метод AddRouteQuery:
    /// отправляет запрос в БД на добавление рейса
    public static void AddRouteQuery(int idDriver, int idCar, int idItinerary, int numberPassengers)
    {
        try
        {

            var querySql = $"INSERT INTO route(id_driver, id_car, id_itinerary, number_passengers) " +
                           $"VALUES ({idDriver}, {idCar}, {idItinerary}, {numberPassengers})";
            using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
            cmd.ExecuteNonQuery();
            
            Console.WriteLine("Рейс добавлен!");
        }
        catch (PostgresException)
        {
            Console.WriteLine();
            Console.WriteLine("Данное ID отсутсвует!");
            Console.WriteLine();
        }
    }
}
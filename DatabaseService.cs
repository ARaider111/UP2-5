using Npgsql;

namespace GarageConsoleApp;

/// Класс DatabaseService
/// отвечает за подключение и открытие соединения с БД 

public static class DatabaseService
{
    /// Переменная _connection
    /// хранит открытое соединение с БД

    private static NpgsqlConnection? _connection;
    /// Метод GetConnectionString()
    /// возвращает строку подключения к БД
    private static string GetConnectionString()
    {
        return @"Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=Sp!derman2005";
    }
    
    /// Метод GetSqlConnection()
    /// проверяет есть ли уже открытое соединение с БД
    /// если нет, то открывает соединение с БД
    public static NpgsqlConnection GetSqlConnection()
    {
        if (_connection is null)
        {
            _connection = new NpgsqlConnection(GetConnectionString());
            _connection.Open();
        }
        return _connection;
    }
}
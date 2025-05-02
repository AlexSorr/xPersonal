using System;

namespace Personal.Testing;

public static class Helper {

    /// <summary>
    /// Получить запрошенные данные с консоли
    /// </summary>
    /// <param name="target">Что запрашиваем</param>
    /// <param name="trimResult">Обрезать пробелы в результате</param>
    /// <returns>Строка, введенная с консоли</returns>
    public static string GetRequested(string target, bool trimResult = true) {
        Console.Clear();
        Console.Write($"Enter {target.ToLower()}: ");
        string result = Console.ReadLine() ?? string.Empty;
        return trimResult ? result.Trim() : result;
    }
}

using System;

namespace Personal.Testing;

public static class Helper {

    /// <summary>
    /// Получить запрошенные данные с консоли
    /// </summary>
    /// <param name="target">Что запрашиваем</param>
    /// <returns></returns>
    public static string GetRequested(string target) {
        Console.Clear();
        Console.Write($"Enter {target}: ");
        return Console.ReadLine() ?? string.Empty;
    }
}

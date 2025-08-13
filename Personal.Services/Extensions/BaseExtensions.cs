using System;

namespace Personal.Services.Extensions;

public static class BaseExtensions {

    public static bool IsNullOrEmpty(this string input) => string.IsNullOrEmpty(input);

    public static bool IsNullOrWhiteSpaces(this string input) => string.IsNullOrWhiteSpace(input);

}

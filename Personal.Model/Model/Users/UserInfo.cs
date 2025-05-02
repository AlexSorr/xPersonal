using Personal.Model.Base;

namespace Personal.Model.Users;

/// <summary>
/// Класс, описывающий общую информацию о пользователе
/// </summary>
public class UserInfo : UserAttribute {

    public UserInfo() {}

    public UserInfo(string firstName, string lastName, DateTime birthDate) {
        FirstName = Capitalize(firstName); LastName = Capitalize(lastName); BirthDate = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);
    }

    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; private set; } = string.Empty;

    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; private set; } = string.Empty;

    /// <summary>
    /// Полное имя
    /// </summary>
    public string FullName { get => $"{FirstName} {LastName}".Trim(); }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; private set; } = DateTime.MinValue;

    /// <summary>
    /// Возраст
    /// </summary>
    public int? Age => CalculateAge();

    /// <summary>
    /// Рассчитать возраст пользователя, отностиельно текущей даты
    /// </summary>
    /// <returns>Количество полных лет пользователя, если заполнена дата рождения, иначе - <c>null</c></returns>
    private int? CalculateAge() {
        if (BirthDate == DateTime.MinValue)
            return null;

        int yearsCount = DateTime.Today.Year - BirthDate.Year;
        if (DateTime.Today < BirthDate.AddYears(yearsCount)) 
            yearsCount--;
        return yearsCount;
    }

    public static string Capitalize(string input) {
        if (string.IsNullOrEmpty(input))
            return input;

        return input.Substring(0, 1).ToUpper() + input.Substring(1).ToLower();
    }

}
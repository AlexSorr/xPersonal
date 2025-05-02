using System.Text;
using System.Globalization;

using CsvHelper;
using CsvHelper.Configuration;

using Personal.Model.Wallet;
using Personal.Model.Wallet.DTO;

namespace Testing.Budget;

public class BudgetLoader {

    private Account[] Accounts { get; set; } = new Account[] { };

    private ActionCategory[] Categories { get; set; } = new ActionCategory[] { };

    private List<ExpenseAction> Expenses { get; set; } = new List<ExpenseAction>();

    private List<IncomeAction> Incomes { get; set; } = new List<IncomeAction>();

    private List<TransactionAction> Transactions { get; set; } = new List<TransactionAction>();

    private AccountActionDTO[] templateData = new AccountActionDTO[] {};

    /// <summary>
    /// Не забыть создать пользователя в БД
    /// </summary>
    /// <param name="path"></param>
    public BudgetLoader(string path) {
        templateData = LoadFromCSV(path);
    }

    private AccountActionDTO[] LoadFromCSV(string path) {
        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path) || !Path.GetExtension(path).Equals(".csv", StringComparison.OrdinalIgnoreCase)) 
            return new AccountActionDTO[] { };
        var config = new CsvConfiguration(CultureInfo.InvariantCulture) {
            Delimiter = ",",
            Encoding = Encoding.UTF8,
            HasHeaderRecord = true,
            MissingFieldFound = null,
            HeaderValidated = null
        };

        using StreamReader reader = new StreamReader(path);
        using CsvReader csv = new CsvReader(reader, config);
        return csv.GetRecords<AccountActionDTO>().ToArray();
    }

    //создать счета
    public void ImportAccounts() {
        if (templateData == null || !templateData.Any())
            return;
        this.Accounts = templateData.Where(x => !string.IsNullOrWhiteSpace(x.Account)).Select(x => x.Account).Distinct()
            .Select(x => new Account() { Name = x /*, CreationDate = DateTime.Now*/ }).ToArray(); 
    }

    //создать категории
    public void ImportCategories() {
        if (templateData == null || !templateData.Any())
            return;
        this.Categories = templateData.Where(x => !string.IsNullOrWhiteSpace(x.Category)).Select(x => x.Category).Distinct()
             .Select(x => new ActionCategory() { Name = x }).ToArray();
    }
    
}
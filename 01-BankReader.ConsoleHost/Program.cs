using System.Diagnostics.CodeAnalysis;
using Autofac;
using BankReader.Data.Csv;
using BankReader.Data.Excel;
using BankReader.Data.Json;
using BankReader.Data.Utilities;
using BankReader.Implementation.Models;
using BankReader.Implementation.Rules;
using BankReader.Implementation.Services;
using BankReader.Implementation.Transactions;
using BankReader.Implementation.Wrappers;

namespace BankReader.ConsoleHost
{
    [ExcludeFromCodeCoverage]
    static class Program
    {
        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();

            // Business logic
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<HousekeepingBookWriter>().As<IHousekeepingBookWriter>();

            // Input/Output
            builder.RegisterType<FileWrapper>().As<IFileWrapper>();
            builder.RegisterType<ConsoleScreen>().As<IConsoleScreen>();
            builder.RegisterType<TextStreamFactory>().As<ITextStreamFactory>();
            builder.RegisterType<TransactionReader>().As<ITransactionReader>();
            builder.RegisterType<JsonReader>().As<IJsonReader>();

            // Rules
            builder.RegisterType<JsonRuleService>().Keyed<IRuleService>(InputType.Json);
            builder.RegisterType<DatabaseRuleService>().Keyed<IRuleService>(InputType.Database);

            // Transactions
            builder.RegisterType<CsvTransactionService>().Keyed<ITransactionService>(InputType.Csv);
            builder.RegisterType<DatabaseTransactionService>().Keyed<ITransactionService>(InputType.Database);

            return builder.Build();
        }

        public static void Main()
        {
            CompositionRoot().Resolve<Application>().Run();
        }
    }
}

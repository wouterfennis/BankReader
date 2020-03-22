using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac;
using BankReader.Data.Csv;
using BankReader.Data.Excel;
using BankReader.Data.Json;
using BankReader.Data.Utilities;
using BankReader.Implementation.Providers;
using BankReader.Implementation.Services;
using BankReader.Implementation.Wrappers;

namespace BankReader.ConsoleHost
{
    [ExcludeFromCodeCoverage]
    static class Program
    {
        public static void Main(string[] arguments)
        {
            CompositionRoot(arguments).Resolve<Application>().Run();
        }

        private static IContainer CompositionRoot(string[] arguments)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();

            // Business logic
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<HousekeepingBookWriter>().As<IHousekeepingBookWriter>();

            // Input/Output
            builder.RegisterType<FileWrapper>().As<IFileWrapper>();
            builder.RegisterType<TextStreamFactory>().As<ITextStreamFactory>();
            builder.RegisterType<CsvTransactionReader>().As<ITransactionProvider>();
            builder.RegisterType<JsonCategoryRuleProvider>().As<ICategoryRuleProvider>();

            string transactionsLocation = arguments.ElementAt(0);
            string categoryRulesLocation = arguments.ElementAt(1);

            builder.Register(ctx => new TransactionsLocationProvider(transactionsLocation)).As<ITransactionsLocationProvider>();
            builder.Register(ctx => new CategoryRulesLocationProvider(categoryRulesLocation)).As<ICategoryRulesLocationProvider>();

            return builder.Build();
        }
    }
}

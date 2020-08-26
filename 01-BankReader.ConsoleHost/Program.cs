using Autofac;
using BankReader.ConsoleHost.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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
            builder.RegisterType<HouseholdService>().As<IHouseholdService>();
            builder.RegisterType<HousekeepingBookWriter>().As<IHouseholdBookWriter>();
            builder.RegisterType<TransactionCategorizer>().As<ITransactionCategorizer>();

            // Input/Output
            builder.RegisterType<FileWrapper>().As<IFileWrapper>();
            builder.RegisterType<TextStreamFactory>().As<ITextStreamFactory>();
            builder.RegisterType<CsvTransactionReader>().As<ITransactionProvider>();
            builder.RegisterType<JsonCategoryRuleProvider>().As<ICategoryRuleProvider>();

            string transactionsLocation = arguments.ElementAt(0);
            string categoryRulesLocation = arguments.ElementAt(1);
            string workbookLocation = arguments.ElementAt(2);

            builder.Register(ctx => new FileLocationProvider(categoryRulesLocation, transactionsLocation, workbookLocation)).As<IFileLocationProvider>();

            return builder.Build();
        }
    }
}

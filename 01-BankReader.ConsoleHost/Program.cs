using System.Diagnostics.CodeAnalysis;
using Autofac;
using BankReader.Data.Csv;
using BankReader.Data.Excel;
using BankReader.Data.Json;
using BankReader.Data.Utilities;
using BankReader.Implementation.Services;
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
            builder.RegisterType<TextStreamFactory>().As<ITextStreamFactory>();
            builder.RegisterType<CsvTransactionReader>().As<ICsvTransactionReader>();
            builder.RegisterType<JsonRuleReader>().As<IJsonRuleReader>();

            return builder.Build();
        }

        public static void Main()
        {
            CompositionRoot().Resolve<Application>().Run();
        }
    }
}

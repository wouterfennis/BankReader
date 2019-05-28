using System.Diagnostics.CodeAnalysis;
using Autofac;
using BankReader.Data;
using BankReader.Data.Csv;
using BankReader.Data.Excel;
using BankReader.Data.Json;
using BankReader.Data.Utilities;
using BankReader.Implementation;
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
            builder.RegisterType<ConsoleWrapper>().As<IConsoleWrapper>();
            builder.RegisterType<TextStreamFactory>().As<ITextStreamFactory>();
            builder.RegisterType<TransactionReader>().As<ITransactionReader>();
            builder.RegisterType<JsonReader>().As<IJsonReader>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<HousekeepingBookWriter>().As<IHousekeepingBookWriter>();

            return builder.Build();
        }

        public static void Main()
        {
            CompositionRoot().Resolve<Application>().Run();
        }
    }
}

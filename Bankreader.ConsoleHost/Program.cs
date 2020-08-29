using Bankreader.Application;
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Bankreader.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Bankreader.FileSystem.File;
using Bankreader.Application.Services;
using Bankreader.FileSystem.Excel;
using Bankreader.FileSystem.Json;
using Bankreader.Infrastructure.Files;
using BankReader.Data.Csv;

namespace Bankreader.ConsoleHost
{
    [ExcludeFromCodeCoverage]
    static class Program
    {
        public static void Main(string[] rawArguments)
        {
            var arguments = new ArgumentOptions(rawArguments);
            CompositionRoot(arguments).GetService<Controller>().Run();
        }

        private static IServiceProvider CompositionRoot(ArgumentOptions arguments)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddConsole())
                .AddTransient<IHouseholdService, HouseholdService>()
                .AddTransient<ITransactionCategorizer, TransactionCategorizeService>()
                .AddTransient<ICategoryRuleProvider, JsonCategoryRuleReader>()
                .AddTransient<ITransactionProvider, CsvTransactionReader>()
                .AddTransient<ITextStreamFactory, TextStreamFactory>()
                .AddTransient<IHousekeepingBookWriter, HousekeepingBookWriter>()
                .AddTransient<IFileLocationProvider>(x => 
                    new FileLocationProvider(arguments.CategoryRulesLocation, arguments.TransactionsLocation, arguments.WorkbookLocation)
                    )
                .AddScoped<Controller>()
                .BuildServiceProvider();
            //builder.Register(ctx => ).As<IFileLocationProvider>();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Controller>();
            logger.LogDebug("Starting program.");

            return serviceProvider;

            //var builder = new ContainerBuilder();
            //builder.RegisterType<Application>();

            //// Business logic
            //builder.RegisterType<HouseholdService>().As<IHouseholdService>();
            //builder.RegisterType<HousekeepingBookWriter>().As<IHouseholdBookWriter>();
            //builder.RegisterType<TransactionCategorizer>().As<ITransactionCategorizer>();

            //// Input/Output
            //builder.RegisterType<FileWrapper>().As<IFileWrapper>();
            //builder.RegisterType<TextStreamFactory>().As<ITextStreamFactory>();
            //builder.RegisterType<CsvTransactionReader>().As<ITransactionProvider>();
            //builder.RegisterType<JsonCategoryRuleProvider>().As<ICategoryRuleProvider>();

            //string transactionsLocation = arguments.ElementAt(0);
            //string categoryRulesLocation = arguments.ElementAt(1);
            //string workbookLocation = arguments.ElementAt(2);


            //return builder.Build();
        }
    }
}

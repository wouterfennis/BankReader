using BankReader.Data.Csv.Converters;
using CsvHelper.Configuration;

namespace BankReader.Data.Csv.Models
{
    public class TransactionMapping : ClassMap<Transaction>
    {
        public TransactionMapping()
        {
            Map(m => m.Date).TypeConverterOption.Format("yyyyMMdd").Name("Datum");
            Map(m => m.Description).Name("Naam / Omschrijving");
            Map(m => m.Accountnumber).Name("Rekening");
            Map(m => m.ContraAccountnumber).Name("Tegenrekening");
            Map(m => m.Code).Name("Code");
            Map(m => m.TransactionDirection).Name("Af Bij").TypeConverter<TransactionDirectionConverter>();
            Map(m => m.Amount).Name("Bedrag (EUR)");
            Map(m => m.MutationType).Name("MutatieSoort");
            Map(m => m.Comments).Name("Mededelingen");
        }
    }
}

namespace BankReader.Implementation.Providers
{
    public class TransactionsLocationProvider : ITransactionsLocationProvider
    {
        private readonly string _transactionsLocation;

        public TransactionsLocationProvider(string transactionsLocation)
        {
            _transactionsLocation = transactionsLocation;
        }

        public string GetTransactionsLocation()
        {
            return _transactionsLocation;
        }
    }
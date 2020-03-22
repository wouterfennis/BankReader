namespace BankReader.Data.Providers
{
    public class CategoryRulesLocationProvider : ICategoryRulesLocationProvider
    {
        private readonly string _categoryRulesLocation;

        public CategoryRulesLocationProvider(string categoryRulesLocation)
        {
            _categoryRulesLocation = categoryRulesLocation;
        }

        public string GetCategoryRulesLocation()
        {
            return _categoryRulesLocation;
        }
    }
}
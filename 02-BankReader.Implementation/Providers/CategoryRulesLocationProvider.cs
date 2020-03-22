namespace BankReader.Implementation.Providers
{
    public class CategoryRulesLocationProvider : ICategoryRulesLocationProvider
    {
        private readonly string _categoryRulesLocation;

        public CategoryRulesLocationProvider(string CategoryRulesLocation)
        {
            _categoryRulesLocation = CategoryRulesLocation;
        }

        public string GetCategoryRulesLocation()
        {
            return _categoryRulesLocation;
        }
    }
}